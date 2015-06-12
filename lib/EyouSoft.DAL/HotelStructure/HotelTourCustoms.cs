using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 酒店团队定制-业务数据层
    /// </summary>
    /// 鲁功源  2010-12-02
    public class HotelTourCustoms : DALBase, EyouSoft.IDAL.HotelStructure.IHotelTourCustoms
    {
        #region SQL变量
        private const string Sql_HotelTourCustoms_INSERT = "INSERT INTO tbl_HotelTourCustoms([CompanyId],[HotelCode],[HotelName],[HotelStar],[CityCode],[LocationAsk],[RoomAsk],[LiveStartDate],[LiveEndDate],[RoomCount],[PeopleCount],[BudgetMin],[BudgetMax],[Payment],[GuestType],[TourType],[OtherRemark],[TreatState]) VALUES(@CompanyId,@HotelCode,@HotelName,@HotelStar,@CityCode,@LocationAsk,@RoomAsk,@LiveStartDate,@LiveEndDate,@RoomCount,@PeopleCount,@BudgetMin,@BudgetMax,@Payment,@GuestType,@TourType,@OtherRemark,@TreatState);" +
            "INSERT im_message(src,dst,[message],flag,IssueTime) values (10000,35791,'收到一个酒店团队订单，请尽快处理',0,getdate())";
        private const string Sql_HotelTourCustoms_GETMODEL = "SELECT [Id],[CompanyId],[HotelCode],[HotelName],[HotelStar],[CityCode],[LocationAsk],[RoomAsk],[LiveStartDate],[LiveEndDate],[RoomCount],[PeopleCount],[BudgetMin],[BudgetMax],[Payment],[GuestType],[TourType],[OtherRemark],[TreatState],[TreateTime],[OperatorId] from tbl_HotelTourCustoms where Id=@Id;";
        private const string Sql_HotelTourCustoms_SETTREATESTATE = "UPDATE tbl_HotelTourCustoms set TreatState=@TreatState,OperatorId=@OperatorId,TreateTime=getdate() where Id=@Id";
        private const string Sql_HotelTourCustomsAsk_INSERT = "INSERT INTO tbl_HotelTourCustomsAsk(Id,TourOrderID,AskName,AskTime,AskContent) Values(@Id,@TourOrderID,@AskName,getdate(),@AskContent)";
        #endregion

        #region 构造函数
        /// <summary>
        /// 酒店库链接
        /// </summary>
        Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelTourCustoms()
        {
            _database = base.HotelStore;
        }
        #endregion

        #region IHotelTourCustoms 成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">团队定制实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.HotelStructure.HotelTourCustoms model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelTourCustoms_INSERT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "HotelCode", DbType.String, model.HotelCode);
            this._database.AddInParameter(dc, "HotelName", DbType.String, model.HotelName);
            this._database.AddInParameter(dc, "HotelStar", DbType.Int32, (int)model.HotelStar);
            this._database.AddInParameter(dc, "CityCode", DbType.String, model.CityCode);
            this._database.AddInParameter(dc, "LocationAsk", DbType.String, model.LocationAsk);
            this._database.AddInParameter(dc, "RoomAsk", DbType.String, model.RoomAsk);
            this._database.AddInParameter(dc, "LiveStartDate", DbType.DateTime, model.LiveStartDate);
            this._database.AddInParameter(dc, "LiveEndDate", DbType.DateTime, model.LiveEndDate);
            this._database.AddInParameter(dc, "RoomCount", DbType.Int32, model.RoomCount);
            this._database.AddInParameter(dc, "PeopleCount", DbType.Int32, model.PeopleCount);
            this._database.AddInParameter(dc, "BudgetMin", DbType.Decimal, model.BudgetMin);
            this._database.AddInParameter(dc, "BudgetMax", DbType.Decimal, model.BudgetMax);
            this._database.AddInParameter(dc, "Payment", DbType.Int32, (int)model.Payment);
            this._database.AddInParameter(dc, "GuestType", DbType.Int32, (int)model.GuestType);
            this._database.AddInParameter(dc, "TourType", DbType.Int32, (int)model.TourType);
            this._database.AddInParameter(dc, "OtherRemark", DbType.String, model.OtherRemark);
            this._database.AddInParameter(dc, "TreatState", DbType.Int32, (int)model.TreatState);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取团队定制实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>团队预定实体</returns>
        public EyouSoft.Model.HotelStructure.HotelTourCustoms GetModel(int Id)
        {
            EyouSoft.Model.HotelStructure.HotelTourCustoms model = null;
            var strSql = new StringBuilder();
            strSql.Append(
                @" SELECT [Id],[CompanyId],[HotelCode],[HotelName],[HotelStar],[CityCode],[LocationAsk],[RoomAsk],
                    [LiveStartDate],[LiveEndDate],[RoomCount],[PeopleCount],[BudgetMin],[BudgetMax],[Payment],[GuestType],
                    [TourType],[OtherRemark],[TreatState],[TreateTime],[OperatorId],
                    (select top 1 ContactName,ContactSex,ContactTel,ContactFax,ContactMobile,ContactEmail,QQ,MQ,MSN from 
                    tbl_CompanyUser where tbl_CompanyUser.CompanyId = tbl_HotelTourCustoms.CompanyId and IsDeleted = '0' and 
                    IsEnable = '1' and IsAdmin = '1' for xml raw,root('Root')) as Contact
                    from tbl_HotelTourCustoms where Id=@Id; ");
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "Id", DbType.Int32, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelTourCustoms();
                    if (!dr.IsDBNull(dr.GetOrdinal("BudgetMax")))
                        model.BudgetMax = dr.GetDecimal(dr.GetOrdinal("BudgetMax"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BudgetMin")))
                        model.BudgetMin = dr.GetDecimal(dr.GetOrdinal("BudgetMin"));
                    model.CityCode = dr[dr.GetOrdinal("CityCode")].ToString();
                    model.CompanyId = dr[dr.GetOrdinal("CompanyId")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("GuestType")))
                        model.GuestType = (EyouSoft.HotelBI.HBEGuestTypeIndicator)int.Parse(dr[dr.GetOrdinal("GuestType")].ToString());
                    model.HotelCode = dr[dr.GetOrdinal("HotelCode")].ToString();
                    model.HotelName = dr[dr.GetOrdinal("HotelName")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("HotelStar")))
                        model.HotelStar = (EyouSoft.HotelBI.HotelRankEnum)int.Parse(dr[dr.GetOrdinal("HotelStar")].ToString());
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LiveEndDate")))
                        model.LiveEndDate = dr.GetDateTime(dr.GetOrdinal("LiveEndDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LiveStartDate")))
                        model.LiveStartDate = dr.GetDateTime(dr.GetOrdinal("LiveStartDate"));
                    model.LocationAsk = dr[dr.GetOrdinal("LocationAsk")].ToString();
                    model.OtherRemark = dr[dr.GetOrdinal("OtherRemark")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Payment")))
                        model.Payment = (EyouSoft.HotelBI.HBEPaymentType)int.Parse(dr[dr.GetOrdinal("Payment")].ToString());
                    model.PeopleCount = dr.GetInt32(dr.GetOrdinal("PeopleCount"));
                    model.RoomAsk = dr[dr.GetOrdinal("RoomAsk")].ToString();
                    model.RoomCount = dr.GetInt32(dr.GetOrdinal("RoomCount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TourType")))
                        model.TourType = (EyouSoft.Model.HotelStructure.TourTypeList)int.Parse(dr[dr.GetOrdinal("TourType")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("TreatState")))
                        model.TreatState = (EyouSoft.Model.HotelStructure.OrderStateList)int.Parse(dr[dr.GetOrdinal("TreatState")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("TreateTime")))
                        model.TreateTime = dr.GetDateTime(dr.GetOrdinal("TreateTime"));
                    model.OperatorId = dr.GetInt32(dr.GetOrdinal("OperatorId"));

                    #region 2011-12-20线路改版增加

                    if (!dr.IsDBNull(dr.GetOrdinal("Contact")))
                    {
                        var xRoot = XElement.Parse(dr.GetString(dr.GetOrdinal("Contact")));
                        var xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            model.Contact = new Model.CompanyStructure.ContactPersonInfo();
                            foreach (var t in xRows)
                            {
                                if (t == null)
                                    continue;

                                model.Contact.ContactName = Common.Utility.GetXAttributeValue(t, "ContactName");
                                model.Contact.ContactSex =
                                    (Model.CompanyStructure.Sex)
                                    Common.Utility.GetInt(Common.Utility.GetXAttributeValue(t, "ContactSex"));
                                model.Contact.Tel = Common.Utility.GetXAttributeValue(t, "ContactTel");
                                model.Contact.Fax = Common.Utility.GetXAttributeValue(t, "ContactFax");
                                model.Contact.Mobile = Common.Utility.GetXAttributeValue(t, "ContactMobile");
                                model.Contact.Email = Common.Utility.GetXAttributeValue(t, "ContactEmail");
                                model.Contact.QQ = Common.Utility.GetXAttributeValue(t, "QQ");
                                model.Contact.MQ = Common.Utility.GetXAttributeValue(t, "MQ");
                                model.Contact.MSN = Common.Utility.GetXAttributeValue(t, "MSN");
                            }
                        }
                    }

                    #endregion
                }
            }
            return model;
        }
        /// <summary>
        /// 获取团队定制列表
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">团队定制查询实体</param>
        /// <returns>团队定制列表</returns>
        public IList<EyouSoft.Model.HotelStructure.HotelTourCustoms> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.SearchTourCustomsInfo SearchInfo)
        {
            IList<EyouSoft.Model.HotelStructure.HotelTourCustoms> list = new List<EyouSoft.Model.HotelStructure.HotelTourCustoms>();
            string tableName = "tbl_HotelTourCustoms";
            string fields = @" Id,CompanyId,HotelCode,HotelName,HotelStar,LiveStartDate,LiveEndDate,RoomCount,PeopleCount,Payment,
                                IssueTime,TreatState,RoomAsk,OtherRemark,LocationAsk,GuestType,TourType,BudgetMin,BudgetMax,
                                (select top 1 ContactName,ContactSex,ContactTel,ContactFax,ContactMobile,ContactEmail,QQ,MQ,MSN 
                                    from tbl_CompanyUser where tbl_CompanyUser.CompanyId = tbl_HotelTourCustoms.CompanyId 
                                    and IsDeleted = '0' and IsEnable = '1' and IsAdmin = '1' for xml raw,root('Root')) as Contact";
            string orderByStr = " IssueTime DESC ";
            string PrimaryKey = "Id";
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (SearchInfo != null)
            {
                if (!string.IsNullOrEmpty(SearchInfo.HotelCode))
                    strWhere.AppendFormat(" and HotelCode='{0}' ", SearchInfo.HotelCode);
                if (!string.IsNullOrEmpty(SearchInfo.HotelName))
                    strWhere.AppendFormat(" and HotelName like '%{0}%' ", SearchInfo.HotelName);
                if (SearchInfo.CheckInSDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',LiveStartDate) >= 0 ", SearchInfo.CheckInSDate.Value.ToString());
                if (SearchInfo.CheckInEDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',LiveStartDate) <= 0 ", SearchInfo.CheckInEDate.Value.ToString());
                if (SearchInfo.CheckOutSDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',LiveEndDate) >= 0 ", SearchInfo.CheckOutSDate.Value.ToString());
                if (SearchInfo.CheckOutEDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',LiveEndDate) <= 0 ", SearchInfo.CheckOutEDate.Value.ToString());
                if (!string.IsNullOrEmpty(SearchInfo.CompanyId))
                    strWhere.AppendFormat(" and CompanyId = '{0}' ", SearchInfo.CompanyId);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, PageSize, PageIndex, ref RecordCount, tableName, PrimaryKey, fields, strWhere.ToString(), orderByStr))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HotelStructure.HotelTourCustoms model = new EyouSoft.Model.HotelStructure.HotelTourCustoms();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.HotelName = dr[dr.GetOrdinal("HotelName")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("HotelCode")))
                        model.HotelCode = dr[dr.GetOrdinal("HotelCode")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("LiveStartDate")))
                        model.LiveStartDate = dr.GetDateTime(dr.GetOrdinal("LiveStartDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LiveEndDate")))
                        model.LiveEndDate = dr.GetDateTime(dr.GetOrdinal("LiveEndDate"));
                    model.RoomCount = dr.GetInt32(dr.GetOrdinal("RoomCount"));
                    model.PeopleCount = dr.GetInt32(dr.GetOrdinal("PeopleCount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Payment")))
                        model.Payment = (EyouSoft.HotelBI.HBEPaymentType)int.Parse(dr[dr.GetOrdinal("Payment")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("TreatState")))
                        model.TreatState = (EyouSoft.Model.HotelStructure.OrderStateList)int.Parse(dr[dr.GetOrdinal("TreatState")].ToString());
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.RoomAsk = dr["RoomAsk"].ToString();
                    model.OtherRemark = dr["OtherRemark"].ToString();
                    model.LocationAsk = dr["LocationAsk"].ToString();

                    if (!dr.IsDBNull(dr.GetOrdinal("GuestType")))
                        model.GuestType = (EyouSoft.HotelBI.HBEGuestTypeIndicator)dr.GetByte(dr.GetOrdinal("GuestType"));

                    if (!dr.IsDBNull(dr.GetOrdinal("TourType")))
                        model.TourType = (EyouSoft.Model.HotelStructure.TourTypeList)dr.GetByte(dr.GetOrdinal("TourType"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BudgetMin")))
                        model.BudgetMin = dr.GetDecimal(dr.GetOrdinal("BudgetMin"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BudgetMax")))
                        model.BudgetMax = dr.GetDecimal(dr.GetOrdinal("BudgetMax"));
                    model.HotelStar = (EyouSoft.HotelBI.HotelRankEnum)dr.GetByte(dr.GetOrdinal("HotelStar"));

                    #region 2011-12-20线路改版增加

                    if (!dr.IsDBNull(dr.GetOrdinal("Contact")))
                    {
                        var xRoot = XElement.Parse(dr.GetString(dr.GetOrdinal("Contact")));
                        var xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            model.Contact = new Model.CompanyStructure.ContactPersonInfo();
                            foreach (var t in xRows)
                            {
                                if (t == null)
                                    continue;

                                model.Contact.ContactName = Common.Utility.GetXAttributeValue(t, "ContactName");
                                model.Contact.ContactSex =
                                    (Model.CompanyStructure.Sex)
                                    Common.Utility.GetInt(Common.Utility.GetXAttributeValue(t, "ContactSex"));
                                model.Contact.Tel = Common.Utility.GetXAttributeValue(t, "ContactTel");
                                model.Contact.Fax = Common.Utility.GetXAttributeValue(t, "ContactFax");
                                model.Contact.Mobile = Common.Utility.GetXAttributeValue(t, "ContactMobile");
                                model.Contact.Email = Common.Utility.GetXAttributeValue(t, "ContactEmail");
                                model.Contact.QQ = Common.Utility.GetXAttributeValue(t, "QQ");
                                model.Contact.MQ = Common.Utility.GetXAttributeValue(t, "MQ");
                                model.Contact.MSN = Common.Utility.GetXAttributeValue(t, "MSN");
                            }
                        }
                    }

                    #endregion

                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 设置处理状态
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="TreatState">处理状态</param>
        /// <param name="OperatorId">操作人编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetTreatState(int Id, EyouSoft.Model.HotelStructure.OrderStateList TreatState, int OperatorId)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelTourCustoms_SETTREATESTATE);
            this._database.AddInParameter(dc, "Id", DbType.Int32, Id);
            this._database.AddInParameter(dc, "TreatState", DbType.Int32, (int)TreatState);
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, OperatorId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 添加酒店团队订单回复
        /// </summary>
        /// <param name="model">团队订单回复实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool AddAsk(EyouSoft.Model.HotelStructure.HotelTourCustomsAsk model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelTourCustomsAsk_INSERT);
            this._database.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "TourOrderID", DbType.Int32, model.TourOrderID);
            this._database.AddInParameter(dc, "AskName", DbType.String, model.AskName);
            this._database.AddInParameter(dc, "AskContent", DbType.String, model.AskContent);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取团队定制回复列表
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TourOrderId">团队定制订单ID</param>
        /// <returns>团队定制回复列表</returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelTourCustomsAsk> GetListAsk(int PageSize, int PageIndex, ref int RecordCount, int TourOrderId)
        {
            IList<EyouSoft.Model.HotelStructure.HotelTourCustomsAsk> list = new List<EyouSoft.Model.HotelStructure.HotelTourCustomsAsk>();
            string tableName = "tbl_HotelTourCustomsAsk";
            string fields = "Id,TourOrderID,AskName,AskTime,AskContent";
            string primaryKey = "Id";
            string orderByStr = "AskTime desc";
            string strWhere = string.Empty;
            if (TourOrderId > 0)
                strWhere = " TourOrderID=" + TourOrderId.ToString();
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, PageSize, PageIndex, ref RecordCount, tableName, primaryKey, fields, strWhere, orderByStr))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HotelStructure.HotelTourCustomsAsk model = new EyouSoft.Model.HotelStructure.HotelTourCustomsAsk();
                    model.ID = dr[dr.GetOrdinal("Id")].ToString();
                    model.AskContent = dr[dr.GetOrdinal("AskContent")].ToString();
                    model.AskName = dr[dr.GetOrdinal("AskName")].ToString();
                    model.AskTime = dr[dr.GetOrdinal("AskTime")].ToString();
                    model.TourOrderID = dr.GetInt32(dr.GetOrdinal("TourOrderID"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
