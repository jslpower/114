using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：团队负责人数据层
    /// </summary>
    public class TourContactInfo:DALBase,ITourContactInfo
    {

        #region SQL定义
        //private const string SQL_INSERT_TourContactInfo = "INSERT INTO tbl_TourContactInfo(CompanyId,ContactName,ContactTel,ContactQQ,ContactMQId,UserName) VALUES(@CompanyId,@ContactName,@ContactTel,@ContactQQ,@ContactMQId,@UserName)";
        private const string SQL_TourContactList_SELECT = @"SELECT Id,CompanyId,ContactName,ContactTel,ContactQQ,ContactMQId,UserName FROM tbl_TourContactInfo WHERE CompanyId=@CompanyId and ContactName like '%@ContactName%'
UNION
SELECT 0 AS Id,CompanyId,ContactName,ContactTel,QQ AS ContactQQ,MQ AS ContactMQId,UserName FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsDeleted=0 AND ContactName like '%@ContactName%'";
        private const string SQL_TourContactList_SELECT_All = @"SELECT Id,CompanyId,ContactName,ContactTel,ContactQQ,ContactMQId,UserName FROM tbl_TourContactInfo WHERE CompanyId=@CompanyId
UNION
SELECT 0 AS Id,CompanyId,ContactName,ContactTel,QQ AS ContactQQ,MQ AS ContactMQId,UserName FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsDeleted=0";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public TourContactInfo()
        {
            this._database = base.CompanyStore;
        }

        /// <summary>
        /// 添加团队负责人[若负责人已存在,则修改负责人信息]
        /// </summary>
        /// <param name="model">团队负责人实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.TourContactInfo model)
        {
            int rows = 0;
            DbCommand dc = this._database.GetStoredProcCommand("proc_TourContactInfo_Insert");
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactQQ);
            this._database.AddInParameter(dc, "ContactMQId", DbType.String, model.ContactMQId);
            this._database.AddInParameter(dc, "UserName", DbType.String, model.UserName);
            this._database.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, this._database);
            object obj = this._database.GetParameterValue(dc, "ReturnValue");
            if (obj != null)
                rows = Convert.ToInt32(obj);
            if (rows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取列表[团队负责人表+未存在于团队负责人表中的子帐号信息]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="contactName">联系人姓名(若为空,则不作为查询条件)</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.TourContactInfo> GetList(string companyId, string contactName)
        {
            IList<EyouSoft.Model.CompanyStructure.TourContactInfo> list = new List<EyouSoft.Model.CompanyStructure.TourContactInfo>();
            DbCommand dc = null;
            if (string.IsNullOrEmpty(contactName))
                dc = this._database.GetSqlStringCommand(SQL_TourContactList_SELECT_All);
            else
                dc = this._database.GetSqlStringCommand(SQL_TourContactList_SELECT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "ContactName", DbType.String, contactName);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.TourContactInfo model = new EyouSoft.Model.CompanyStructure.TourContactInfo();
                    model.CompanyID = dr.IsDBNull(dr.GetOrdinal("CompanyID")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyID"));
                    model.ContactMQId = dr.IsDBNull(dr.GetOrdinal("ContactMQId")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMQId"));
                    model.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactQQ = dr.IsDBNull(dr.GetOrdinal("ContactQQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactQQ"));
                    model.ContactTel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true ? "" : dr.GetString(dr.GetOrdinal("UserName"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
    }
}
