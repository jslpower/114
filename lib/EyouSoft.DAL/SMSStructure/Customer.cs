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

namespace EyouSoft.DAL.SMSStructure
{
    /// <summary>
    /// 短信中心-客户信息及客户类型数据访问类
    /// </summary>
    /// Author:汪奇志 2010-06-10 
    ///  增加GetMobiles函数 ，根据条件获取接收短信号--李焕超--2010-12-17
    public class Customer:DALBase,EyouSoft.IDAL.SMSStructure.ICustomer
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_INSERTCATEGORY = "INSERT INTO [SMS_CustomerClass] ([CompanyID] ,[UserID] ,[ClassName]) VALUES(@COMPANYID,@USERID,@CLASSNAME);SELECT @@IDENTITY";
        private const string SQL_DELETE_DELETECATEGORY = "DELETE FROM [SMS_CustomerClass] WHERE [Id]=@CategoryId";
        private const string SQL_SELECT_GETCATEGORYS = "SELECT [ID] ,[CompanyID] ,[UserID] ,[ClassName] ,[IssueTime]  FROM [SMS_CustomerClass] WHERE [CompanyId]=@COMPANYID";
        private const string SQL_DELETE_DELETECUSTOMER = "DELETE FROM [SMS_CustomerList] WHERE [Id]=@CUSTOMERID";
        private const string SQL_SELECT_GETCUSTOMERINFO = "SELECT [ID] ,[CompanyID] ,[UserID] ,[CustomerCompanyName] ,[CustomerContactName] ,[ClassID] ,[ReMark] ,[MobileNumber] ,[IssueTime], ProvinceId, CityId  FROM [SMS_CustomerList] WHERE [ID]=@CUSTOMERID";
        private const string SQL_INSERT_InsertCustomer = "IF NOT EXISTS(SELECT 1 FROM [SMS_CustomerList] WHERE [CompanyID]=@COMPANYID AND [MobileNumber]=@MOBILE) BEGIN INSERT INTO [SMS_CustomerList] ([ID],[CompanyID] ,[UserID] ,[CustomerCompanyName] ,[CustomerContactName] ,[ClassID] ,[ReMark] ,[MobileNumber], [ProvinceId], [CityId]) VALUES(@ID,@COMPANYID,@USERID,@CUSTOMERCOMPANYNAME,@CUSTOMERCONTACTNAME,@CLASSID,@REMARK,@MOBILE,@ProvinceId,@CityId) END";
        private const string SQL_UPDATE_UpdateCustomer = "IF NOT EXISTS(SELECT 1 FROM [SMS_CustomerList] WHERE [CompanyID]=@COMPANYID AND [MobileNumber]=@MOBILE AND [Id]<>@CUSTOMERID) BEGIN UPDATE [SMS_CustomerList] SET [CustomerCompanyName]=@CUSTOMERCOMPANYNAME,[CustomerContactName]=@CUSTOMERCONTACTNAME,[ClassID]=@CLASSID,[ReMark]=@REMARK,[MobileNumber]=@MOBILE,[ProvinceId]=@ProvinceId,[CityId]=@CityId WHERE [Id]=@CUSTOMERID END";
        private const string SQL_SELECT_IsExistsCustomerMobile = "SELECT COUNT(*) FROM SMS_CustomerList WHERE CompanyId=@CompanyId AND MobileNumber=@Mobile ";
        #endregion static constants

        #region 成员方法
        /// <summary>
        /// 插入客户类型信息,返回0插入失败,>0时为插入的类型编号
        /// </summary>
        /// <param name="categoryInfo">客户类型业务实体</param>
        /// <returns></returns>
        public virtual int InsertCategory(EyouSoft.Model.SMSStructure.CustomerCategoryInfo categoryInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_INSERTCATEGORY);

            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.String, categoryInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "USERID", DbType.String, categoryInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "CLASSNAME", DbType.String, categoryInfo.CategoryName);

            object obj = DbHelper.GetSingle(cmd, base.SMSStore);

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 删除客户类型信息
        /// </summary>
        /// <param name="CategoryId">类型编号</param>
        /// <returns></returns>
        public virtual bool DeleteCategory(int CategoryId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_DELETE_DELETECATEGORY);

            base.SMSStore.AddInParameter(cmd, "CategoryId", DbType.Int32, CategoryId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;            
        }

        /// <summary>
        /// 根据指定的公司编号获取公司的所有客户类型信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.CustomerCategoryInfo> GetCategorys(string companyId)
        {
            IList<EyouSoft.Model.SMSStructure.CustomerCategoryInfo> categorys = new List<EyouSoft.Model.SMSStructure.CustomerCategoryInfo>();
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETCATEGORYS);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                while (rdr.Read())
                {
                    categorys.Add(new EyouSoft.Model.SMSStructure.CustomerCategoryInfo(rdr.GetInt32(rdr.GetOrdinal("Id"))
                        , rdr["ClassName"].ToString()
                        , rdr.GetString(rdr.GetOrdinal("CompanyID"))
                        , rdr.GetString(rdr.GetOrdinal("UserID"))
                        , rdr.GetDateTime(rdr.GetOrdinal("IssueTime"))));
                }
            }

            return categorys;
        }

        /// <summary>
        /// 插入客户信息
        /// </summary>
        /// <param name="customerInfo">客户信息业务实体</param>
        /// <returns>返回处理结果</returns>
        public virtual bool InsertCustomer(EyouSoft.Model.SMSStructure.CustomerInfo customerInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_InsertCustomer);

            base.SMSStore.AddInParameter(cmd, "ID", DbType.AnsiStringFixedLength, customerInfo.CustomerId);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.String, customerInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "USERID", DbType.String, customerInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "CUSTOMERCOMPANYNAME", DbType.String, customerInfo.CustomerCompanyName);
            base.SMSStore.AddInParameter(cmd, "CUSTOMERCONTACTNAME", DbType.String, customerInfo.CustomerContactName);
            base.SMSStore.AddInParameter(cmd, "CLASSID", DbType.String, customerInfo.CategoryId);
            base.SMSStore.AddInParameter(cmd, "REMARK", DbType.String, customerInfo.Remark);
            base.SMSStore.AddInParameter(cmd, "MOBILE", DbType.String, customerInfo.Mobile);
            base.SMSStore.AddInParameter(cmd, "ProvinceId", DbType.Int32, customerInfo.ProvinceId);
            base.SMSStore.AddInParameter(cmd, "CityId", DbType.Int32, customerInfo.CityId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 根据指定条件获取客户信息
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="customerCompanyName">客户公司名称 为空时不做为查询条件</param>
        /// <param name="customerUserFullName">客户联系人姓名 为空时不做为查询条件</param>
        /// <param name="mobile">手机号码 为空时不做为查询条件</param>
        /// <param name="categoryId">客户类型编号 -1时不做为查询条件</param>
        /// <param name="source">客户来源</param>
        /// <param name="provinceId">省份ID</param>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.CustomerInfo> GetCustomers(int pageSize, int pageIndex, ref int recordCount, string companyId, string customerCompanyName, string customerUserFullName, string mobile, int categoryId, EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource source, int provinceId, int cityId)
        {
            IList<EyouSoft.Model.SMSStructure.CustomerInfo> customers = new List<EyouSoft.Model.SMSStructure.CustomerInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_SMS_Customers";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = " ID, CompanyID, UserID, CustomerCompanyName, CustomerContactName, ClassID, ReMark, MobileNumber, IssueTime, ClassName,TongYCompanyId, ProvinceId, CityId, ProvinceName, CityName";

            cmdQuery.Append(" 1=1 ");

            switch (source)
            {
                case EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.所有客户:  //注意TongYCompanyId<>companyId是用来排除自己的
                    cmdQuery.AppendFormat(" AND (CompanyID='{0}' OR CompanyID='0') AND TongYCompanyId <> '{0}' ", companyId);
                    break;
                case EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.我的客户:
                    cmdQuery.AppendFormat(" AND CompanyID='{0}' ", companyId);
                    break;
                case EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.平台组团社客户://注意TongYCompanyId<>companyId是用来排除自己的
                    cmdQuery.AppendFormat(" AND CompanyID='0' AND TongYCompanyId <> '{0}' ", companyId);
                    break;
            }

            if(provinceId > 0)
                cmdQuery.AppendFormat(" AND ProvinceId={0}", provinceId);

            if (cityId > 0)
                cmdQuery.AppendFormat(" AND CityId={0}", cityId);

            if (!string.IsNullOrEmpty(customerCompanyName))
            {
                cmdQuery.AppendFormat(" AND (CustomerCompanyName LIKE '%{0}%')", customerCompanyName);
            }

            if (!string.IsNullOrEmpty(customerUserFullName))
            {
                cmdQuery.AppendFormat(" AND (CustomerContactName LIKE '%{0}%')", customerUserFullName);
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                cmdQuery.AppendFormat(" AND (MobileNumber LIKE '%{0}%')", mobile);
            }

            if (categoryId != -1&&categoryId!=0)
            {
                cmdQuery.AppendFormat(" AND ClassID={0}", categoryId.ToString());
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                EyouSoft.Model.SMSStructure.CustomerInfo model = null;

                while (rdr.Read())
                {
                    model = new EyouSoft.Model.SMSStructure.CustomerInfo();

                    model.CustomerId = rdr.GetString(rdr.GetOrdinal("ID"));
                    model.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    model.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));
                    model.CustomerCompanyName = rdr["CustomerCompanyName"].ToString();
                    model.CustomerContactName = rdr["CustomerContactName"].ToString();
                    model.CategoryId = rdr.GetInt32(rdr.GetOrdinal("ClassID"));
                    model.CategoryName = rdr["ClassName"].ToString();
                    model.Remark = rdr["ReMark"].ToString();
                    model.Mobile = rdr["MobileNumber"].ToString();
                    model.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    model.RetailersCompanyId = rdr.GetString(rdr.GetOrdinal("TongYCompanyId"));
                    model.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    model.ProvinceName = rdr.IsDBNull(rdr.GetOrdinal("ProvinceName")) ? "" : rdr.GetString(rdr.GetOrdinal("ProvinceName"));
                    model.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    model.CityName = rdr.IsDBNull(rdr.GetOrdinal("CityName")) ? "" : rdr.GetString(rdr.GetOrdinal("CityName"));

                    customers.Add(model);
                }
            }

            return customers;
        }

        /// <summary>
        /// 获取客户总数
        /// </summary>
        /// <returns></returns>
       public virtual int GetCustomersCount()
        {
            int RecordCount = 0;
            StringBuilder cmdQuery = new StringBuilder();
            cmdQuery.Append("select count(*) from view_SMS_Customers ");
            cmdQuery.Append("where id='0'");
           DbCommand cmd= base.SMSStore.GetSqlStringCommand(cmdQuery.ToString());
           using (IDataReader rdr = DbHelper.ExecuteReader(cmd,base.SMSStore))
            {
              
                if (rdr.Read())
                {
                    RecordCount = rdr.GetInt32(0);
                }

            }

           return RecordCount;
        }

        #region
        /// <summary>
        /// 根据指定条件获取接收短信号码
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="customerCompanyName">客户公司名称 为空时不做为查询条件</param>
        /// <param name="customerUserFullName">客户联系人姓名 为空时不做为查询条件</param>
        /// <param name="mobile">手机号码 为空时不做为查询条件</param>
        /// <param name="categoryId">客户类型编号 -1时不做为查询条件</param>
        /// <param name="source">客户来源</param>
        /// <param name="provinceId">省份ID</param>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.AcceptMobileInfo> GetMobiles(string companyId, string mobile, int categoryId, EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource source, int provinceId, int cityId)
        {
            IList<EyouSoft.Model.SMSStructure.AcceptMobileInfo> AcceptMobiles = new List<EyouSoft.Model.SMSStructure.AcceptMobileInfo>();
           
            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_SMS_Customers";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = " ID,ClassID,MobileNumber ";
            cmdQuery.AppendFormat(" select {0} from {1} where",fields,tableName); 
            cmdQuery.Append(" 1=1 ");

            switch (source)
            {
                case EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.所有客户:  //注意TongYCompanyId<>companyId是用来排除自己的
                    cmdQuery.AppendFormat(" AND (CompanyID='{0}' OR CompanyID='0') AND TongYCompanyId <> '{0}' ", companyId);
                    break;
                case EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.我的客户:
                    cmdQuery.AppendFormat(" AND CompanyID='{0}' ", companyId);
                    break;
                case EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.平台组团社客户://注意TongYCompanyId<>companyId是用来排除自己的
                    cmdQuery.AppendFormat(" AND CompanyID='0' AND TongYCompanyId <> '{0}' ", companyId);
                    break;
            }

            if (provinceId > 0)
                cmdQuery.AppendFormat(" AND ProvinceId={0}", provinceId);

            if (cityId > 0)
                cmdQuery.AppendFormat(" AND CityId={0}", cityId);

            if (!string.IsNullOrEmpty(mobile))
            {
                cmdQuery.AppendFormat(" AND (MobileNumber LIKE '%{0}%')", mobile);
            }

            if (categoryId != -1 && categoryId != 0)
            {
                cmdQuery.AppendFormat(" AND ClassID={0}", categoryId.ToString());
            }

            DbCommand cmd = base.SMSStore.GetSqlStringCommand(cmdQuery.ToString());
            cmd.CommandType=CommandType.Text;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,base.SMSStore))
            {
                EyouSoft.Model.SMSStructure.AcceptMobileInfo model = null;
               
                while (rdr.Read())
                {
                    model = new EyouSoft.Model.SMSStructure.AcceptMobileInfo();
                    model.Mobile = rdr.IsDBNull(rdr.GetOrdinal("MobileNumber")) ? "" : rdr.GetString(rdr.GetOrdinal("MobileNumber"));
                    if (rdr.GetString(rdr.GetOrdinal("id")) == "0")
                        model.IsEncrypt = true;
                    AcceptMobiles.Add(model);
                }
            }

            return AcceptMobiles;

        }
        #endregion
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="customerId">客户编号</param>
        /// <returns></returns>
        public virtual bool DeleteCustomer(string customerId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_DELETE_DELETECUSTOMER);
            base.SMSStore.AddInParameter(cmd, "CUSTOMERID", DbType.AnsiStringFixedLength, customerId);
            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="customerId">客户编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SMSStructure.CustomerInfo GetCustomerInfo(string customerId)
        {
            EyouSoft.Model.SMSStructure.CustomerInfo customerInfo = null;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETCUSTOMERINFO);
            base.SMSStore.AddInParameter(cmd, "CUSTOMERID", DbType.AnsiStringFixedLength, customerId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    customerInfo = new EyouSoft.Model.SMSStructure.CustomerInfo(rdr.GetString(rdr.GetOrdinal("ID"))
                        , rdr.GetString(rdr.GetOrdinal("CompanyID"))
                        , rdr.GetString(rdr.GetOrdinal("UserID"))
                        , rdr["CustomerCompanyName"].ToString()
                        , rdr["CustomerContactName"].ToString()
                        , rdr.GetInt32(rdr.GetOrdinal("ClassID"))
                        , string.Empty
                        , rdr["ReMark"].ToString()
                        , rdr["MobileNumber"].ToString()
                        , rdr.GetDateTime(rdr.GetOrdinal("IssueTime"))
                        , rdr.GetInt32(rdr.GetOrdinal("ProvinceId"))
                        , rdr.GetInt32(rdr.GetOrdinal("CityId"))
                        );
                }
            }

            return customerInfo;
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="customerInfo">客户信息业务实体</param>
        /// <returns>返回处理结果</returns>
        public virtual bool UpdateCustomer(EyouSoft.Model.SMSStructure.CustomerInfo customerInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_UPDATE_UpdateCustomer);

            base.SMSStore.AddInParameter(cmd, "CUSTOMERCOMPANYNAME", DbType.String, customerInfo.CustomerCompanyName);
            base.SMSStore.AddInParameter(cmd, "CUSTOMERCONTACTNAME", DbType.String, customerInfo.CustomerContactName);
            base.SMSStore.AddInParameter(cmd, "CLASSID", DbType.Int32, customerInfo.CategoryId);
            base.SMSStore.AddInParameter(cmd, "REMARK", DbType.String, customerInfo.Remark);
            base.SMSStore.AddInParameter(cmd, "MOBILE", DbType.String, customerInfo.Mobile);
            base.SMSStore.AddInParameter(cmd, "CUSTOMERID", DbType.String, customerInfo.CustomerId);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.String, customerInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "ProvinceId", DbType.Int32, customerInfo.ProvinceId);
            base.SMSStore.AddInParameter(cmd, "CityId", DbType.Int32, customerInfo.CityId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 判断客户手机号码是否存在
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mobile">客户号码</param>
        /// <param name="customerId">客户编号</param>
        /// <returns></returns>
        public virtual bool IsExistsCustomerMobile(string companyId, string mobile, string customerId)
        {
            bool isExists = false;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_IsExistsCustomerMobile);

            base.SMSStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            base.SMSStore.AddInParameter(cmd, "Mobile", DbType.String, mobile);

            if (!string.IsNullOrEmpty(customerId))
            {
                cmd.CommandText = SQL_SELECT_IsExistsCustomerMobile + " AND Id<>@CUSTOMERID";
                base.SMSStore.AddInParameter(cmd, "CUSTOMERID", DbType.AnsiStringFixedLength, customerId);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    isExists = rdr.GetInt32(0) > 0 ? true : false;
                }
            }

            return isExists;
        }
        #endregion
    }

}
