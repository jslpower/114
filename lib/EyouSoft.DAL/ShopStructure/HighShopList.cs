using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.ShopStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：高级网店基本信息数据层
    /// </summary>
    public class HighShopList:DALBase,IHighShopList
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopList() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopList_Add = "INSERT INTO tbl_HighShopList(ID,CompanyID,CompanyName,ProvinceId,CityId,ProvinceName,CityName,ApplyUserID,ApplyContactName,ApplyContactTel,ApplyContactMobile,ApplyContactMQ,ApplyContactQQ,OperatorID,ApplyDomainName,ApplyTime) VALUES(@ID,@CompanyID,@CompanyName,@ProvinceId,@CityId,@ProvinceName,@CityName,@ApplyUserID,@ApplyContactName,@ApplyContactTel,@ApplyContactMobile,@ApplyContactMQ,@ApplyContactQQ,@OperatorID,@ApplyDomainName,@ApplyTime)";
        const string SQL_HighShopList_SELECTBYID = "SELECT ID,CompanyID,CompanyName,ProvinceID,CityID,ApplyDomainName,CheckTime,ExpireTime,ApplyContactTel,ApplyContactMobile,ApplyContactMQ,ApplyContactQQ,IsCheck,ApplyTime from tbl_HighShopList WHERE ID=@ID";
        const string SQL_HighShopList_CHECKINFO = "UPDATE tbl_HighShopList set OperatorID=@OperatorID,IsCheck=@IsCheck,CheckTime=@CheckTime,ExpireTime=@ExpireTime where ID=@ID";
        //const string SQL_HighShopList_UPDATE="UPDATE tbl_HighShopList set "
        #endregion

        #region IHighShopList成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">高级网店基本信息实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.ShopStructure.HighShopList model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopList_Add);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc,"CompanyName",DbType.String,model.CompanyName);
            this._database.AddInParameter(dc,"ProvinceId",DbType.Int32,model.ProvinceId);
            this._database.AddInParameter(dc,"CityId",DbType.Int32,model.CityId);
            this._database.AddInParameter(dc,"ProvinceName",DbType.String,model.ProvinceName);
            this._database.AddInParameter(dc,"CityName",DbType.String,model.CityName);
            this._database.AddInParameter(dc, "ApplyUserID", DbType.AnsiStringFixedLength, model.ApplyUserID);
            this._database.AddInParameter(dc,"ApplyContactName",DbType.String,model.ApplyContactName);
            this._database.AddInParameter(dc,"ApplyContactTel",DbType.String,model.ApplyContactTel);
            this._database.AddInParameter(dc,"ApplyContactMobile",DbType.String,model.ApplyContactMobile);
            this._database.AddInParameter(dc,"ApplyContactMQ",DbType.String,model.ApplyContactMQ);
            this._database.AddInParameter(dc,"ApplyContactQQ",DbType.String,model.ApplyContactQQ);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc,"ApplyDomainName",DbType.String,model.ApplyDomainName);
            this._database.AddInParameter(dc,"ApplyTime",DbType.String,DateTime.Now);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;            
        }
        /// <summary>
        /// 后台获取高级网店列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当期页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>高级网店列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopList> GetList(int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopList> list = new List<EyouSoft.Model.ShopStructure.HighShopList>();
            string tableName = "tbl_HighShopList";
            string fields = "ID,CompanyName,ProvinceName,CityName,ApplyContactName,ApplyContactTel,ApplyContactMobile,ApplyContactMQ,ApplyContactQQ,IsCheck,ApplyTime";
            string primaryKey = "ID";
            string orderByString = "ApplyTime DESC";
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, string.Empty, orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopList model = new EyouSoft.Model.ShopStructure.HighShopList();
                    model.ID = dr.GetString(0);
                    model.CompanyName = dr.GetString(1);
                    model.ProvinceName = dr.GetString(2);
                    model.CityName = dr.GetString(3);
                    model.ApplyContactName = dr.GetString(4);
                    model.ApplyContactTel = dr.GetString(5);
                    model.ApplyContactMobile = dr.GetString(6);
                    model.ApplyContactMQ = dr.GetString(7);
                    model.ApplyContactQQ = dr.GetString(8);
                    model.IsCheck = dr.GetString(9) == "1" ? true : false;
                    model.ApplyTime = dr.GetDateTime(10);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取高级网店实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>高级网店实体</returns>
        public virtual EyouSoft.Model.ShopStructure.HighShopList GetModel(string ID)
        {
            EyouSoft.Model.ShopStructure.HighShopList model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopList_SELECTBYID);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.ShopStructure.HighShopList();
                    model.ID=dr.GetString(0);
                    model.CompanyID=dr.GetString(1);
                    model.CompanyName=dr.GetString(2);
                    model.ProvinceId=dr.GetInt32(3);
                    model.CityId=dr.GetInt32(4);
                    model.ApplyDomainName=dr.GetString(5);
                    if(!dr.IsDBNull(6))
                        model.CheckTime=dr.GetDateTime(6);
                    if(!dr.IsDBNull(7))
                        model.ExpireTime=dr.GetDateTime(7);
                    model.ApplyContactTel=dr.GetString(8);
                    model.ApplyContactMobile=dr.GetString(9);
                    model.ApplyContactMQ=dr.GetString(10);
                    model.ApplyContactQQ=dr.GetString(11);
                    model.IsCheck=dr.GetString(12)=="1"?true:false;
                    if(!dr.IsDBNull(13))
                        model.ApplyTime=dr.GetDateTime(13);
                }
            }
            return model;
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="OperatorID">操作员ID</param>
        /// <param name="ExpireTime">网店到期时间</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool CheckInfo(string ID, bool IsCheck, string OperatorID, DateTime ExpireTime)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopList_CHECKINFO);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsCheck", DbType.AnsiStringFixedLength, IsCheck ? "1" : "0");
            this._database.AddInParameter(dc, "ExpireTime", DbType.DateTime, ExpireTime);
            this._database.AddInParameter(dc, "CheckTime", DbType.DateTime, DateTime.Now);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改高级网店基本信息
        /// </summary>
        /// <param name="model">高级网店实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool UpdateBasicInfo(EyouSoft.Model.ShopStructure.HighShopList model)
        {
            return true;
        }
        /// <summary>
       /// 获取快到期的高级网店
       /// </summary>
       /// <param name="pageSize">每页显示条数</param>
       /// <param name="pageIndex">当前页码</param>
       /// <param name="recordCount">总记录数</param>
       /// <param name="ProvinceId">所在省份ID >0返回指定省份 =0返回全部</param>
       /// <param name="CityId">所在城市ID >0返回指定城市 =0返回全部</param>
       /// <param name="CompanyName">公司名称 模糊匹配</param>
       /// <param name="StartExpireDate">到期日期起始值</param>
       /// <param name="EndExpireDate">到期时间结束值</param>
       /// <returns></returns>
       public virtual IList<EyouSoft.Model.ShopStructure.HighShopList> GetExpireList(int pageSize, int pageIndex, ref int recordCount, int ProvinceId, int CityId, string CompanyName, DateTime? StartExpireDate, DateTime? EndExpireDate)
       {
            IList<EyouSoft.Model.ShopStructure.HighShopList> list = new List<EyouSoft.Model.ShopStructure.HighShopList>();
            string tableName = "tbl_HighShopList";
            string fields = "ID,CompanyName,ProvinceName,CityName,ApplyContactName,ApplyContactTel,ApplyContactMobile,ApplyContactMQ,ApplyContactQQ,IsCheck,ApplyTime,ExpireTime";
            string primaryKey = "ID";
            string orderByString = "ExpireTime DESC";

            #region 生成SQL查询条件
            StringBuilder strWhere = new StringBuilder();
            if (ProvinceId > 0)
            {
                strWhere.AppendFormat(" ProvinceId={0} ", ProvinceId);
            }
            if (CityId > 0)
            {
                strWhere.AppendFormat(" AND CityId={0} ", CityId);
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" AND CompanyName like '%{0}%' ");
                else
                    strWhere.AppendFormat(" CompanyName like '%{0}%' ");
            }
            if (StartExpireDate.HasValue || EndExpireDate.HasValue)
            {
                if (strWhere.Length > 0)
                {
                    if (StartExpireDate.HasValue)
                    {
                        strWhere.AppendFormat(" AND datediff(dd,ExpireTime,'{0}')>-1 ", StartExpireDate.Value.ToString());
                    }
                    if (EndExpireDate.HasValue)
                    {
                        strWhere.AppendFormat(" AND datediff(dd,'{0}',ExpireTime)<=0 ", StartExpireDate.Value.ToString());
                    }
                }
                else
                {
                    if (StartExpireDate.HasValue)
                    {
                        strWhere.AppendFormat(" datediff(dd,ExpireTime,'{0}')>-1 ", StartExpireDate.Value.ToString());
                    }
                    if (EndExpireDate.HasValue)
                    {
                        if(strWhere.Length>0)
                            strWhere.AppendFormat(" AND datediff(dd,'{0}',ExpireTime)<=0 ", EndExpireDate.Value.ToString());
                        else
                            strWhere.AppendFormat(" datediff(dd,'{0}',ExpireTime)<=0 ", EndExpireDate.Value.ToString());
                    }
                }
            }
            else  //默认取一个月之内到期的数据
            {
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" AND ExpireTime between '{0}' and '{1}' ", DateTime.Now.ToString(), DateTime.Now.AddDays(-30).ToString());
                else
                    strWhere.AppendFormat(" ExpireTime between '{0}' and '{1}' ", DateTime.Now.ToString(), DateTime.Now.AddDays(-30).ToString());
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopList model = new EyouSoft.Model.ShopStructure.HighShopList();
                    model.ID = dr.GetString(0);
                    model.CompanyName = dr.GetString(1);
                    model.ProvinceName = dr.GetString(2);
                    model.CityName = dr.GetString(3);
                    model.ApplyContactName = dr.GetString(4);
                    model.ApplyContactTel = dr.GetString(5);
                    model.ApplyContactMobile = dr.GetString(6);
                    model.ApplyContactMQ = dr.GetString(7);
                    model.ApplyContactQQ = dr.GetString(8);
                    model.IsCheck = dr.GetString(9) == "1" ? true : false;
                    model.ApplyTime = dr.GetDateTime(10);
                    if (!dr.IsDBNull(11))
                        model.ExpireTime = dr.GetDateTime(11);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
