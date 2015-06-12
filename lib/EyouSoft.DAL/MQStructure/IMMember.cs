using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// MQ用户信息
    /// </summary>
    /// 周文超 2010-05-11
    public class IMMember : DALBase, IDAL.MQStructure.IIMMember
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMember()
        { }

        #region  static constants
        //static constants
        private const string Sql_IMMember_Select = " select im_uid,bs_uid,im_username,im_password,im_status,im_latest_time,im_displayname,IsAutoLogin from im_member ";
        private const string SQL_SELECT_GETMQUSERCOUNT = "select count(*) from im_member";
        private const string SQL_SELECT_LongOffLine = @"SELECT u.Id,u.CompanyId,u.UserName,u.ContactName,u.ContactSex,u.ContactTel,u.ContactFax,u.ContactMobile,u.ContactEmail,u.QQ,u.MQ,u.MSN,u.IsAdmin FROM im_member AS im,tbl_CompanyUser AS u WHERE im.bs_uid=u.Id AND im.im_latest_time>'0000-00-00 00:00:00' AND DATEDIFF(dd,CONVERT(DATETIME, im.im_latest_time),GETDATE())>=@days AND u.IsDeleted='0'&#
UNION ALL
SELECT u.Id,u.CompanyId,u.UserName,u.ContactName,u.ContactSex,u.ContactTel,u.ContactFax,u.ContactMobile,u.ContactEmail,u.QQ,u.MQ,u.MSN,u.IsAdmin FROM im_member AS im,tbl_CompanyUser AS u WHERE im.bs_uid=u.Id AND im.im_latest_time='0000-00-00 00:00:00' AND u.IsDeleted='0'&#";
        private const string MYSQL_UPDATE_SetGroupMember = "update cluster_info set max_member=@MaxMember where major_id = @MqId and minor_id=@GroupIndex;";
        private const string MYSQL_SELECT_GetGroupMember = "select max_member from cluster_info where major_id = @MqId and minor_id=@GroupIndex;";
        #endregion

        #region 成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>返回MQ用户信息实体</returns>
        public virtual Model.MQStructure.IMMember GetModel(int im_uid)
        {
            if (im_uid <= 0)
                return null;

            Model.MQStructure.IMMember model = new EyouSoft.Model.MQStructure.IMMember();

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_IMMember_Select + " where im_uid=@im_uid ");
            base.MQStore.AddInParameter(dc, "im_uid", DbType.Int32, im_uid);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                if (dr.Read())
                {
                    //im_uid,bs_uid,im_username,im_password,im_status,im_latest_time,im_displayname,IsAutoLogin
                    if (!dr.IsDBNull(dr.GetOrdinal("im_uid")))
                        model.im_uid = int.Parse(dr["im_uid"].ToString());
                    model.bs_uid = dr["bs_uid"].ToString();
                    model.im_username = dr["im_username"].ToString();
                    model.im_password = dr["im_password"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("im_status")))
                        model.im_status = int.Parse(dr["im_status"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("im_latest_time")) && dr["im_latest_time"].ToString()!="0000-00-00 00:00:00")
                        model.im_latest_time = DateTime.Parse(dr["im_latest_time"].ToString());
                    model.im_displayname = dr["im_displayname"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsAutoLogin")))
                    {
                        if (dr["IsAutoLogin"].ToString() == "1")
                        {
                            model.IsAutoLogin = true;
                        }
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// 通过MQ帐号获得MQId
        /// </summary>
        /// <param name="Im_UserName">MQ帐号</param>
        /// <returns>返回MQID</returns>
        public virtual int GetImId(string Im_UserName)
        {
            if (string.IsNullOrEmpty(Im_UserName))
                return 0;

            string strWhere = " select im_uid from im_member where im_username=@im_username ";
            DbCommand dc = base.MQStore.GetSqlStringCommand(strWhere);
            base.MQStore.AddInParameter(dc, "im_username", DbType.String, Im_UserName);

            object obj = DbHelper.GetSingle(dc, base.MQStore);
            if (obj != null)
                return int.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 获的在线人数
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>在线人数</returns>
        public virtual int GetOnLineNumber(int im_uid)
        {
            string strWhere = " Select count(*) from im_member where im_status>11 ";
            if (im_uid > 0)
                strWhere += " and im_uid != @im_uid ";
            DbCommand dc = base.MQStore.GetSqlStringCommand(strWhere);
            if (im_uid > 0)
                base.MQStore.AddInParameter(dc, "im_uid", DbType.Int32, im_uid);

            object obj = DbHelper.GetSingle(dc, base.MQStore);
            if (obj != null)
                return int.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 获取在线MQID
        /// </summary>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        public virtual string[] GetOnLineMQ(int ProvinceId, int CityId)
        {
            string[] MQ = null;
            string strWhere = " Select im_uid from view_im_member_SearchFriend where im_status>11 AND IsAutoLogin=0";
            if (ProvinceId != 0)
                strWhere += string.Format(" and ProvinceId = {0} ", ProvinceId);
            if (CityId != 0)
                strWhere += string.Format(" and CityId = {0} ", CityId);

            DbCommand dc = base.MQStore.GetSqlStringCommand(strWhere);

            DataTable dt = DbHelper.DataTableQuery(dc, base.MQStore);
            if (dt != null && dt.Rows.Count > 0)
            {
                MQ = new string[dt.Rows.Count];
                for (int i =0;i<dt.Rows.Count;i++)
                {
                    MQ[i] = dt.Rows[i][0].ToString();
                }
            }
            return MQ;
        }

        /// <summary>
        /// 获取MQ用户数量
        /// </summary>
        /// <returns>MQ用户数量</returns>
        public virtual int GetMQUserCount()
        {
            DbCommand dc = base.MQStore.GetSqlStringCommand(SQL_SELECT_GETMQUSERCOUNT);
            object obj = DbHelper.GetSingle(dc, base.MQStore);
            if (obj != null)
                return int.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// MQ查找好友
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="ContactName">联系人名称</param>
        /// <param name="UserName">用户名</param>
        /// <param name="ProvinceId">公司所在省份</param>
        /// <param name="CityId">公司所在城市</param>
        /// <param name="CityName">城市名称</param>
        /// <param name="MQID">MQID</param>
        /// <param name="FriendSearchType">查找方式</param>
        /// <param name="IsOnline">是否在线</param>
        /// <param name="CurrCompanyType">当前查找好友的公司类型数组</param>
        /// <returns>返回MQ查找好友实体集合</returns>
        public virtual IList<Model.MQStructure.IMMemberAndUser> GetSearchFriend(int PageSize, int PageIndex, ref int RecordCount
            , string CompanyName, string ContactName, string UserName, int ProvinceId, int CityId, string CityName, int MQID
            , EyouSoft.Model.MQStructure.MQFriendSearchType? FriendSearchType, bool IsOnline
            , params EyouSoft.Model.CompanyStructure.CompanyType[] CurrCompanyType)
        {
            IList<Model.MQStructure.IMMemberAndUser> List = new List<Model.MQStructure.IMMemberAndUser>();
            string strFiles = " im_uid,im_displayname,im_username,im_status,UserId,CompanyType,CompanyName,CityName,ProvinceId,CityId,IsOnline,ContactName,CompanyId ";
            StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
            //zhangzy  2010-11-17  修改了排序方式
            string strOrder = " IsOnline desc,[IsAutoLogin],[im_latest_time] desc ";

            if (!string.IsNullOrEmpty(CompanyName))
                strWhere.AppendFormat(" and CompanyName like '%{0}%' ", CompanyName);
            if (!string.IsNullOrEmpty(ContactName))
                strWhere.AppendFormat(" and ContactName like '%{0}%' ", ContactName);
            if (!string.IsNullOrEmpty(UserName))
                strWhere.AppendFormat(" and im_username like '%{0}%' ", UserName);
            if (ProvinceId > 0)
                strWhere.AppendFormat(" and ProvinceId = {0} ", ProvinceId);
            if (CityId > 0)
                strWhere.AppendFormat(" and CityId = {0} ", CityId);
            if (!string.IsNullOrEmpty(CityName))
                strWhere.AppendFormat(" and CityName like '%{0}%' ", CityName);
            if (MQID > 0)
                strWhere.AppendFormat(" and im_uid = {0} ", MQID);
            if (IsOnline)
                strWhere.Append(" and im_status>11 and IsAutoLogin=0 ");
            if (FriendSearchType.HasValue)
            {
                switch (FriendSearchType)
                {
                    case EyouSoft.Model.MQStructure.MQFriendSearchType.找专线商:
                        strWhere.AppendFormat(" and exists (select 1 from tbl_CompanyTypeList where tbl_CompanyTypeList.CompanyId = view_im_member_SearchFriend.CompanyId and TypeId = {0}) ", (int)EyouSoft.Model.CompanyStructure.CompanyType.专线);
                        break;
                    case EyouSoft.Model.MQStructure.MQFriendSearchType.找组团社:
                        strWhere.AppendFormat(" and exists (select 1 from tbl_CompanyTypeList where tbl_CompanyTypeList.CompanyId = view_im_member_SearchFriend.CompanyId and TypeId = {0}) ", (int)EyouSoft.Model.CompanyStructure.CompanyType.组团);
                        break;
                    case EyouSoft.Model.MQStructure.MQFriendSearchType.找地接社:
                        strWhere.AppendFormat(" and exists (select 1 from tbl_CompanyTypeList where tbl_CompanyTypeList.CompanyId = view_im_member_SearchFriend.CompanyId and TypeId = {0}) ", (int)EyouSoft.Model.CompanyStructure.CompanyType.地接);
                        break;
                    case EyouSoft.Model.MQStructure.MQFriendSearchType.找其他企业:
                        strWhere.AppendFormat(" and CompanyType > 1 ");
                        break;
                    case EyouSoft.Model.MQStructure.MQFriendSearchType.精确查找: //精确查找，地接不能查到组团
                        if (CurrCompanyType == null || CurrCompanyType.Length <= 0)
                            break;
                        if (CurrCompanyType.Contains(EyouSoft.Model.CompanyStructure.CompanyType.地接) && CurrCompanyType.Length == 1)
                        {
                            strWhere.Append(" and exists (select 1 from tbl_CompanyTypeList where tbl_CompanyTypeList.CompanyId = view_im_member_SearchFriend.CompanyId and TypeId IN (1,3,4,5,6,7,8)) ");
                        }
                        break;
                }
            }

            using (IDataReader dr = DbHelper.ExecuteReader(base.MQStore, PageSize, PageIndex, ref RecordCount, "view_im_member_SearchFriend", "im_uid", strFiles, strWhere.ToString(), strOrder))
            {
                EyouSoft.Model.MQStructure.IMMemberAndUser model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMMemberAndUser();
                    if (!dr.IsDBNull(dr.GetOrdinal("im_uid")))
                        model.MQId = dr.GetInt32(dr.GetOrdinal("im_uid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("im_username")))
                        model.MQUserName = dr.GetString(dr.GetOrdinal("im_username"));
                    if (!dr.IsDBNull(dr.GetOrdinal("im_displayname")))
                        model.MQDisplayName = dr.GetString(dr.GetOrdinal("im_displayname"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UserId")))
                        model.UserId = dr.GetString(dr.GetOrdinal("UserId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyName")))
                        model.CompanyName = dr.GetString(dr.GetOrdinal("CompanyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CityId")))
                        model.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProvinceId")))
                        model.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("im_status")))
                        model.MQStatus = dr.GetInt16(dr.GetOrdinal("im_status"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsOnline")) && dr["IsOnline"].ToString() == "1")
                        model.IsOnline = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("ContactName")))
                        model.ContactName = dr.GetString(dr.GetOrdinal("ContactName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CityName")))
                        model.CityName = dr.GetString(dr.GetOrdinal("CityName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyType")))
                        model.CompanyType = (EyouSoft.Model.CompanyStructure.BusinessProperties)dr.GetByte(dr.GetOrdinal("CompanyType"));

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }

        /// <summary>
        /// 获得长期未登录mq的用户信息
        /// </summary>
        /// <param name="days">未登录的天数</param>
        /// <param name="cityList">要查找的城市ID列表,若为null,则表示查找全国城市</param>
        /// <returns></returns>
        public virtual IList<Model.CompanyStructure.CompanyUserBase> GetLongOffLineList(int days, params int[] cityList)
        {
            IList<Model.CompanyStructure.CompanyUserBase> list = new List<Model.CompanyStructure.CompanyUserBase>();
            string SQL = SQL_SELECT_LongOffLine;
            StringBuilder strWhere = new StringBuilder();
            if (cityList != null && cityList.Length > 0)
            {
                foreach (int cityId in cityList)
                {
                    strWhere.AppendFormat("{0},", cityId);
                }
            }
            if (strWhere.Length > 0)
                SQL = SQL.Replace("&#", string.Format(" AND CityId IN ({0})", strWhere.ToString().TrimEnd(",".ToCharArray())));
            else
                SQL = SQL.Replace("&#", "");
            DbCommand dc = base.MQStore.GetSqlStringCommand(SQL);
            base.MQStore.AddInParameter(dc, "days", DbType.Int32, days);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                while (rdr.Read())
                {
                    Model.CompanyStructure.CompanyUserBase model = new EyouSoft.Model.CompanyStructure.CompanyUserBase();
                    model.CompanyID = rdr.IsDBNull(rdr.GetOrdinal("CompanyID")) == true ? "" : rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    model.ContactInfo.Email = rdr.IsDBNull(rdr.GetOrdinal("ContactEmail")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.Fax = rdr.IsDBNull(rdr.GetOrdinal("ContactFax")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile = rdr.IsDBNull(rdr.GetOrdinal("ContactMobile")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ContactMobile"));
                    model.ContactInfo.ContactName = rdr.IsDBNull(rdr.GetOrdinal("ContactName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ContactName"));
                    if (rdr.IsDBNull(rdr.GetOrdinal("ContactSex")))
                        model.ContactInfo.ContactSex = EyouSoft.Model.CompanyStructure.Sex.未知;
                    else
                        model.ContactInfo.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.Sex), rdr.GetString(rdr.GetOrdinal("ContactSex")));

                    model.ContactInfo.Tel = rdr.IsDBNull(rdr.GetOrdinal("ContactTel")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ContactTel"));
                    model.ID = rdr.IsDBNull(rdr.GetOrdinal("ID")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ID"));
                    model.IsAdmin = rdr.GetString(rdr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    model.ContactInfo.MQ = rdr.IsDBNull(rdr.GetOrdinal("MQ")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MQ"));
                    model.ContactInfo.MSN = rdr.IsDBNull(rdr.GetOrdinal("MSN")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MSN"));

                    //model.PassWordInfo.SetEncryptPassWord(rdr.IsDBNull(rdr.GetOrdinal("Password")) == true ? "" : rdr.GetString(rdr.GetOrdinal("Password")), rdr.IsDBNull(rdr.GetOrdinal("EncryptPassword")) == true ? "" : rdr.GetString(rdr.GetOrdinal("EncryptPassword")), rdr.IsDBNull(rdr.GetOrdinal("MD5Password")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MD5Password")));
                    model.ContactInfo.QQ = rdr.IsDBNull(rdr.GetOrdinal("QQ")) == true ? "" : rdr.GetString(rdr.GetOrdinal("QQ"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 调整MQ群人数
        /// </summary>
        /// <param name="maxMember">群人数上限</param>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupIndex">群序号</param>
        /// <returns></returns>
        public bool SetGroupMember(int maxMember, int mqId, int groupIndex)
        {
            DbCommand cmd = this.MySQLStore.GetSqlStringCommand(MYSQL_UPDATE_SetGroupMember);

            this.MySQLStore.AddInParameter(cmd, "MaxMember", DbType.Int32, maxMember);
            this.MySQLStore.AddInParameter(cmd, "MqId", DbType.Int32, mqId);
            this.MySQLStore.AddInParameter(cmd, "GroupIndex", DbType.Int32, groupIndex);

            return DbHelper.ExecuteSql(cmd, this.MySQLStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取MQ群人数
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupIndex">群序号</param>
        /// <returns></returns>
        public int GetGroupMember(int mqId, int groupIndex)
        {
            int number = 0;

            DbCommand cmd = this.MySQLStore.GetSqlStringCommand(MYSQL_SELECT_GetGroupMember);

            this.MySQLStore.AddInParameter(cmd, "MqId", DbType.Int32, mqId);
            this.MySQLStore.AddInParameter(cmd, "GroupIndex", DbType.Int32, groupIndex);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this.MySQLStore))
            {
                if (rdr.Read())
                {
                    number = rdr.GetInt32(0);
                }
            }

            return number;
        }

        /// <summary>
        /// /获取指定城市在线用户数量
        /// </summary>
        /// <param name="porvinceId">省份</param>
        /// <param name="cityId">城市</param>
        /// <returns></returns>
        public virtual int GetCityOnlineNumber(int porvinceId, int cityId)
        {
            int number = 0;
            string sql = "SELECT COUNT(*) FROM [view_im_member_SearchFriend] WHERE [ProvinceId]=@PID AND [CityId]=@CityId AND [im_status]>11 AND [IsAutoLogin]=0";
            DbCommand comm = this.MQStore.GetSqlStringCommand(sql);
            this.MQStore.AddInParameter(comm, "@PID", DbType.Int32, porvinceId);
            this.MQStore.AddInParameter(comm, "@CityId", DbType.Int32, cityId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this.MQStore))
            {
                if (reader.Read())
                {
                    number = reader.GetInt32(0);
                }
            }
            return number;
        }
        #endregion
    }
}
