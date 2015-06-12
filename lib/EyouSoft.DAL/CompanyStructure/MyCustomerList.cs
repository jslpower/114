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
    /// 描述：我的客户数据层
    /// </summary>
    public class MyCustomer : DALBase, IMyCustomer
    {
        #region SQL定义
        private const string SQL_MyCustomerList_INSERT = "INSERT INTO tbl_CompanyCustomerList(ID,CompanyID,OperatorID,CustomerCompanyID,CustomerCompanyName,CustomerLicense,CustomerContactName,CustomerContactTel,MQID) VALUES(@ID,@CompanyID,@OperatorID,@CustomerCompanyID,@CustomerCompanyName,@CustomerLicense,@CustomerContactName,@CustomerContactTel,@MQID)";
        private const string SQL_MyCustomer_DELETE = "DELETE tbl_CompanyCustomerList WHERE ID=@ID";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;
         
        /// <summary>
        /// 构造方法
        /// </summary>
        public MyCustomer()
        {
            this._database = base.CompanyStore;
        }

        #region 成员方法
        /// <summary>
        /// 获取我的客户列表
        /// </summary>
        /// <param name="currentCompanyId">当前登录人公司ID</param>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.MyCustomer> GetList(string currentCompanyId, EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.MyCustomer> list = new List<EyouSoft.Model.CompanyStructure.MyCustomer>();
            string tableName = "view_CompanyCustomerList";
            string fields = "*";
            string primaryKey = "ID";
            string orderByString = "IssueTime DESC";

            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            //设置查询条件
            strWhere.AppendFormat(" CompanyID='{0}' ", currentCompanyId);
            if (query != null)
            {
                if (query.PorvinceId > 0) //按省份查询
                {
                    strWhere.AppendFormat(" AND ProvinceId={0} ", query.PorvinceId);
                }
                if (query.CityId > 0) //按城市查询
                {
                    strWhere.AppendFormat(" AND CityId={0} ", query.CityId);
                }
                if (!string.IsNullOrEmpty(query.CompanyName))
                    strWhere.AppendFormat(" AND CompanyName LIKE '%{0}%' ", query.CompanyName);
                if (!string.IsNullOrEmpty(query.ContactName))
                    strWhere.AppendFormat(" AND ContactName LIKE '%{0}%' ", query.ContactName);
                if (!string.IsNullOrEmpty(query.CompanyBrand))
                    strWhere.AppendFormat(" AND CompanyBrand LIKE '%{0}%' ", query.CompanyBrand);
            }
            #endregion 查询条件

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey,fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.MyCustomer model = new EyouSoft.Model.CompanyStructure.MyCustomer();

                    #region 初始化model
                    //tbl_CompanyCustomerList表中的字段
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.CurrentCompanyID = dr.IsDBNull(dr.GetOrdinal("CompanyID")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyID"));
                    model.CurrentOperatorID = dr.IsDBNull(dr.GetOrdinal("OperatorID")) == true ? "" : dr.GetString(dr.GetOrdinal("OperatorID"));
                    model.CustomerCompanyID = dr.IsDBNull(dr.GetOrdinal("CustomerCompanyID")) == true ? "" : dr.GetString(dr.GetOrdinal("CustomerCompanyID"));


                    model.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
                    model.CompanyBrand = dr.IsDBNull(dr.GetOrdinal("CompanyBrand")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyBrand"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyName"));
                    //设置公司所有的身份//未实现//(未取该字段)
                    //string CompanyRoleListXml = dr.IsDBNull(dr.GetOrdinal("CompanyRoleList")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyRoleList"));
                    //if (!String.IsNullOrEmpty(CompanyRoleListXml))
                    //{
                    //    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    //    xmlDoc.LoadXml(CompanyRoleListXml);
                    //    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("TypeId");

                    //    for (int i = 0; i < NodeList.Count; i++)
                    //    {
                    //        model.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)int.Parse(NodeList[i].Attributes[0].Value));
                    //    }
                    //}
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("ContactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("ContactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("ContactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMobile"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("ContactMQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("ContactMSN")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMSN"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("ContactQQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactQQ"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.License = dr.IsDBNull(dr.GetOrdinal("License")) == true ? "" : dr.GetString(dr.GetOrdinal("License"));
                    model.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    model.OpCompanyId = dr.IsDBNull(dr.GetOrdinal("OpCompanyId"))
                                            ? 0
                                            : dr.GetInt32(dr.GetOrdinal("OpCompanyId"));
                    #endregion 初始化model

                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 批量设置我的客户
        /// </summary>
        /// <param name="currUserId">当前操作的用户ID</param>
        /// <param name="customerCompanyId">要设置的客户ID</param>
        /// <returns></returns>
        public virtual bool SetMyCustomer(string currUserId, params string[] customerCompanyId)
        {
            if (string.IsNullOrEmpty(currUserId) || currUserId == "0" || customerCompanyId == null || customerCompanyId.Length <= 0)
                return false;

            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in customerCompanyId)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    XMLRoot.AppendFormat("<SetMoreMyCustomer ID='{0}' ", Guid.NewGuid().ToString());
                    XMLRoot.AppendFormat(" CustomerCompanyID='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            DbCommand dc = this._database.GetStoredProcCommand("proc_MyCustomerList_Insert");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, currUserId);

            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 批量取消我的客户
        /// </summary>
        /// <param name="listId">我的客户列表的编号</param>
        /// <returns></returns>
        public virtual bool CancelMyCustomer(params string[] listId)
        {
            if (listId == null || listId.Length <= 0)
                return false;

            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in listId)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    XMLRoot.AppendFormat("<CancelMoreMyCustomer ID='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            DbCommand dc = this._database.GetStoredProcCommand("proc_MyCustomerList_Delete");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());

            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
                return true;
            else
                return false;
        }       
        #endregion
    }
}
