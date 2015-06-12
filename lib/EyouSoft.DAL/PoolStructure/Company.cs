//Author:汪奇志 2010-11-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.PoolStructure
{
    /// <summary>
    /// 易诺用户池公司信息数据访问类
    /// </summary>
    public class Company : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.PoolStructure.ICompany
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_CreateCompanyInfo = "INSERT INTO [tbl_Company]([CompanyId],[CompanyName],[Star],[Address],[Remark],[ProvinceId],[CityId],[CountyId]) VALUES (@CompanyId,@CompanyName,@Star,@Address,@Remark,@ProvinceId,@CityId,@CountyId)";
        private const string SQL_INSERT_CreateContacter = "INSERT INTO [tbl_CompanyContacter]([CompanyId],[Fullname],[Birthday],[RememberDay],[JobTitle],[Mobile],[Email],[QQ],[Character],[Interest],[Remark]) VALUES (@CompanyId{0},@Fullname{0},@Birthday{0},@RememberDay{0},@JobTitle{0},@Mobile{0},@Email{0},@QQ{0},@Character{0},@Interest{0},@Remark{0});";
        private const string SQL_INSERT_CreateCustomerTypeConfig = "INSERT INTO [tbl_CompanyCustomerTypeConfig]([CompanyId],[TypeId]) VALUES (@CompanyId{0},@TypeId{0});";
        private const string SQL_INSERT_CreateSuitProductConfig = "INSERT INTO [tbl_CompanySuitProductConfig]([CompanyId],[SuitProductId]) VALUES (@CompanyId{0},@SuitProductId{0})";
        private const string SQL_Delete_CompanyContacterTypeConfigProductConfig = "DELETE FROM [tbl_CompanyContacter] WHERE [CompanyId]=@CompanyId;DELETE FROM [tbl_CompanyCustomerTypeConfig] WHERE [CompanyId]=@CompanyId;DELETE FROM [tbl_CompanySuitProductConfig] WHERE [CompanyId]=@CompanyId;";
        private const string SQL_UPDATE_UpdateCompanyInfo = "UPDATE [tbl_Company] SET [CompanyId]= @CompanyId,[CompanyName]=@CompanyName,[Star]=@Star,[Address]=@Address,[Remark]=@Remark,[ProvinceId]=@ProvinceId,[CityId]=@CityId,[CountyId]=@CountyId WHERE [CompanyId]=@CompanyId";
        private const string SQL_UPDATE_Delete = "UPDATE [tbl_Company] SET [IsDelete]=@IsDelete WHERE [CompanyId]=@CompanyId";
        private const string SQL_SELECT_GetCompanyInfo = "SELECT * FROM [tbl_Company] WHERE [CompanyId]=@CompanyId;SELECT * FROM [tbl_CompanyContacter] WHERE CompanyId=@CompanyId;SELECT * FROM [tbl_CompanyCustomerTypeConfig] WHERE [CompanyId]=@CompanyId;SELECT * FROM [tbl_CompanySuitProductConfig] WHERE [CompanyId]=@CompanyId;";
        #endregion

        #region constructor
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public Company() { this._db = this.PoolStore; }
        #endregion

        #region private memebers
        /// <summary>
        /// 写入用户池公司联系人信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="contacters">用户池公司联系人信息集合</param>
        /// <returns></returns>
        private bool CreateContacter(string companyId,IList<EyouSoft.Model.PoolStructure.ContacterInfo> contacters)
        {
            if (contacters == null || contacters.Count == 0) return true;

            DbCommand cmd = this._db.GetSqlStringCommand("SELECT 1");
            StringBuilder cmdText = new StringBuilder();
            int i = 0;

            foreach (var contacter in contacters)
            {
                cmdText.AppendFormat(SQL_INSERT_CreateContacter, i);
                this._db.AddInParameter(cmd, string.Format("CompanyId{0}", i.ToString()), DbType.AnsiStringFixedLength, companyId);
                this._db.AddInParameter(cmd, string.Format("Fullname{0}", i.ToString()), DbType.String, contacter.Fullname);
                this._db.AddInParameter(cmd, string.Format("Birthday{0}", i.ToString()), DbType.DateTime, contacter.Birthday);
                this._db.AddInParameter(cmd, string.Format("RememberDay{0}", i.ToString()), DbType.DateTime, contacter.RememberDay);
                this._db.AddInParameter(cmd, string.Format("JobTitle{0}", i.ToString()), DbType.String, contacter.JobTitle);
                this._db.AddInParameter(cmd, string.Format("Mobile{0}", i.ToString()), DbType.String, contacter.Mobile);
                this._db.AddInParameter(cmd, string.Format("Email{0}", i.ToString()), DbType.String, contacter.Email);
                this._db.AddInParameter(cmd, string.Format("QQ{0}", i.ToString()), DbType.String, contacter.QQ);
                this._db.AddInParameter(cmd, string.Format("Character{0}", i.ToString()), DbType.String, contacter.Character);
                this._db.AddInParameter(cmd, string.Format("Interest{0}", i.ToString()), DbType.String, contacter.Interest);
                this._db.AddInParameter(cmd, string.Format("Remark{0}", i.ToString()), DbType.String, contacter.Remark);
                i++;
            }

            cmd.CommandText = cmdText.ToString();

            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 写入用户池客户类型信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="types">用户池客户类型信息集合</param>
        /// <returns></returns>
        private bool CreateCustomerTypeConfig(string companyId,IList<EyouSoft.Model.PoolStructure.CustomerTypeInfo> types)
        {
            if (types == null || types.Count == 0) return true;

            DbCommand cmd = this._db.GetSqlStringCommand("SELECT 1");
            StringBuilder cmdText = new StringBuilder();
            int i = 0;

            foreach (var type in types)
            {
                cmdText.AppendFormat(SQL_INSERT_CreateCustomerTypeConfig, i);
                this._db.AddInParameter(cmd, string.Format("CompanyId{0}", i.ToString()), DbType.AnsiStringFixedLength, companyId);
                this._db.AddInParameter(cmd, string.Format("TypeId{0}", i.ToString()), DbType.Int32, type.TypeId);
                i++;
            }

            cmd.CommandText = cmdText.ToString();

            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 写入用户池客户适用产品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="products">用户池客户适用产品信息集合</param>
        /// <returns></returns>
        private bool CreateSuitProductConfig(string companyId, IList<EyouSoft.Model.PoolStructure.SuitProductInfo> products)
        {
            if (products == null || products.Count == 0) return true;

            DbCommand cmd = this._db.GetSqlStringCommand("SELECT 1");
            StringBuilder cmdText = new StringBuilder();
            int i = 0;

            foreach (var product in products)
            {
                cmdText.AppendFormat(SQL_INSERT_CreateSuitProductConfig, i);
                this._db.AddInParameter(cmd, string.Format("CompanyId{0}", i.ToString()), DbType.AnsiStringFixedLength, companyId);
                this._db.AddInParameter(cmd, string.Format("SuitProductId{0}", i.ToString()), DbType.Int32, product.ProuctId);
                i++;
            }

            cmd.CommandText = cmdText.ToString();

            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 删除用户池客户联系人、类型、适用产品
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private bool DeleteCompanyContacterTypeConfigProductConfig(string companyId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_Delete_CompanyContacterTypeConfigProductConfig);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据用户池公司联系人信息集合FOR XML AUTO, ROOT('root')后的字符串获取联系人信息集合
        /// </summary>
        /// <param name="s">FOR XML AUTO后的字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PoolStructure.ContacterInfo> GetContactersByXml(string s)
        {
            IList<EyouSoft.Model.PoolStructure.ContacterInfo> contacters = new List<EyouSoft.Model.PoolStructure.ContacterInfo>();

            if (string.IsNullOrEmpty(s)) return contacters;

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(s);

            System.Xml.XmlNodeList xnl = xd.GetElementsByTagName("tbl_CompanyContacter");

            if (xnl != null)
            {
                foreach (System.Xml.XmlNode xn in xnl)
                {
                    EyouSoft.Model.PoolStructure.ContacterInfo contacter = new EyouSoft.Model.PoolStructure.ContacterInfo();

                    contacter.ContacterId = int.Parse(xn.Attributes["ContactId"].Value);
                    contacter.Fullname = xn.Attributes["Fullname"] != null ? xn.Attributes["Fullname"].Value : string.Empty;                    
                    contacter.JobTitle = xn.Attributes["JobTitle"] != null ? xn.Attributes["JobTitle"].Value : string.Empty;
                    contacter.Mobile = xn.Attributes["Mobile"] != null ? xn.Attributes["Mobile"].Value : string.Empty;
                    if (xn.Attributes["Birthday"] != null)
                    {
                        contacter.Birthday = Convert.ToDateTime(xn.Attributes["Birthday"].Value);
                    }

                    contacters.Add(contacter);
                }
            }

            return contacters;
        }
        #endregion


        #region ICompany 成员
        /// <summary>
        /// 创建用户池公司信息
        /// </summary>
        /// <param name="info">用户池公司信息业务实体</param>
        /// <returns></returns>
        public virtual bool Create(EyouSoft.Model.PoolStructure.CompanyInfo info)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_INSERT_CreateCompanyInfo);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            this._db.AddInParameter(cmd, "CompanyName", DbType.String, info.CompanyName);
            this._db.AddInParameter(cmd, "Star", DbType.String, info.Star);
            this._db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            this._db.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            this._db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            this._db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            this._db.AddInParameter(cmd, "CountyId", DbType.Int32, info.CountyId);

            bool result = DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;

            if (result)
            {
                this.CreateContacter(info.CompanyId, info.Contacters);
                this.CreateCustomerTypeConfig(info.CompanyId, info.CustomerTypeConfig);
                this.CreateSuitProductConfig(info.CompanyId, info.SuitProductConfig);
            }

            return result;
        }

        /// <summary>
        /// 更新用户池公司信息
        /// </summary>
        /// <param name="info">用户池公司信息业务实体</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.PoolStructure.CompanyInfo info)
        {
            if (!this.DeleteCompanyContacterTypeConfigProductConfig(info.CompanyId)) return false;

            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdateCompanyInfo);            
            this._db.AddInParameter(cmd, "CompanyName", DbType.String, info.CompanyName);
            this._db.AddInParameter(cmd, "Star", DbType.String, info.Star);
            this._db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            this._db.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            this._db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            this._db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            this._db.AddInParameter(cmd, "CountyId", DbType.Int32, info.CountyId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);

            bool result = DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;

            if (result)
            {
                this.CreateContacter(info.CompanyId, info.Contacters);
                this.CreateCustomerTypeConfig(info.CompanyId, info.CustomerTypeConfig);
                this.CreateSuitProductConfig(info.CompanyId, info.SuitProductConfig);
            }

            return result;
        }

        /// <summary>
        /// 删除用户池公司信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="isDelete">是否删除</param>
        /// <returns></returns>
        public virtual bool Delete(string companyId, bool isDelete)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_Delete);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, isDelete ? "1" : "0");

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 获取公司信息集合，列表以公司信息为行集
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.PoolStructure.CompanyInfo> GetCompanys(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.PoolStructure.CompanySearchInfo searchInfo)
        {
            IList<EyouSoft.Model.PoolStructure.CompanyInfo> companys = new List<EyouSoft.Model.PoolStructure.CompanyInfo>();
            StringBuilder cmdText = new StringBuilder();
            string tableName = "view_Company_ItemsByCompany";
            string primaryKey = "CompanyId";
            string orderByString = "CreateTime DESC";
            string fields = "CompanyId,CompanyName,Contacters";

            #region 拼接查询
            cmdText.Append(" 1=1 ");

            if (searchInfo.CityId.HasValue)
            {
                cmdText.AppendFormat(" AND CityId={0} ", searchInfo.CityId.Value);
            }

            if (!string.IsNullOrEmpty(searchInfo.CompanyName))
            {
                cmdText.AppendFormat(" AND CompanyName LIKE '%{0}%' ", searchInfo.CompanyName);
            }

            if (searchInfo.CountyId.HasValue)
            {
                cmdText.AppendFormat(" AND CountyId={0} ", searchInfo.CountyId.Value);
            }

            if (searchInfo.ProvinceId.HasValue)
            {
                cmdText.AppendFormat(" AND ProvinceId={0} ", searchInfo.ProvinceId.Value);
            }

            if (!string.IsNullOrEmpty(searchInfo.ContacterFullname) || !string.IsNullOrEmpty(searchInfo.ContacterMobile))
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_CompanyContacter WHERE CompanyId=view_Company_ItemsByCompany.CompanyId ");

                if (!string.IsNullOrEmpty(searchInfo.ContacterFullname))
                {
                    cmdText.AppendFormat(" AND Fullname LIKE '%{0}%' ", searchInfo.ContacterFullname);
                }

                if (!string.IsNullOrEmpty(searchInfo.ContacterMobile))
                {
                    cmdText.AppendFormat(" AND Mobile='{0}' ", searchInfo.ContacterMobile);
                }
                cmdText.Append(")");
            }

            if (searchInfo.CustomerTypeId.HasValue)
            {
                cmdText.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanyCustomerTypeConfig WHERE CompanyId=view_Company_ItemsByCompany.CompanyId AND TypeId={0}) ", searchInfo.CustomerTypeId.Value);
            }

            if (searchInfo.SuitProductId.HasValue)
            {
                cmdText.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanySuitProductConfig WHERE CompanyId=view_Company_ItemsByCompany.CompanyId AND SuitProductId={0}) ", searchInfo.SuitProductId.Value);
            }

            if (searchInfo.UserCitys != null && searchInfo.UserCitys.Length > 0)
            {
                cmdText.Append(" AND CityId IN( ");
                cmdText.Append(searchInfo.UserCitys[0].ToString());

                for (int i = 1; i < searchInfo.UserCitys.Length; i++)
                {
                    cmdText.AppendFormat(",{0}", searchInfo.UserCitys[i].ToString());
                }

                cmdText.Append(" ) ");
            }

            if (searchInfo.UserCustomerTypes != null && searchInfo.UserCustomerTypes.Count > 0)
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_CompanyCustomerTypeConfig WHERE CompanyId=view_Company_ItemsByCompany.CompanyId AND TypeId IN( ");
                cmdText.Append(searchInfo.UserCustomerTypes[0].ToString());

                for (int i = 1; i < searchInfo.UserCustomerTypes.Count;i++ )
                {
                    cmdText.AppendFormat(",{0}", searchInfo.UserCustomerTypes[i].ToString());
                }

                cmdText.Append(" )) ");
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdText.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.PoolStructure.CompanyInfo company = new EyouSoft.Model.PoolStructure.CompanyInfo();

                    company.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    company.CompanyName = rdr["CompanyName"].ToString();
                    if(!rdr.IsDBNull(rdr.GetOrdinal("Contacters")))
                        company.Contacters = this.GetContactersByXml(rdr["Contacters"].ToString());
                    else
                        company.Contacters = new List<EyouSoft.Model.PoolStructure.ContacterInfo>();

                    companys.Add(company);
                }
            }

            return companys;
        }

        /// <summary>
        /// 获取公司信息集合，列表以公司联系人信息为行集
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.PoolStructure.ContacterInfo> GetCompanysItemByContacter(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.PoolStructure.CompanySearchInfo searchInfo)
        {
            IList<EyouSoft.Model.PoolStructure.ContacterInfo> contacters = new List<EyouSoft.Model.PoolStructure.ContacterInfo>();
            StringBuilder cmdText = new StringBuilder();
            string tableName = "view_Company_ItemsByContacter";
            string primaryKey = "ContactId";
            string orderByString = "CreateTime DESC";
            string fields = "ContactId,CompanyId,CompanyName,Fullname,Mobile,JobTitle,Birthday";

            #region 拼接查询
            cmdText.Append(" 1=1 ");

            if (searchInfo.CityId.HasValue)
            {
                cmdText.AppendFormat(" AND CityId={0} ", searchInfo.CityId.Value);
            }

            if (!string.IsNullOrEmpty(searchInfo.CompanyName))
            {
                cmdText.AppendFormat(" AND CompanyName LIKE '%{0}%' ", searchInfo.CompanyName);
            }

            if (searchInfo.CountyId.HasValue)
            {
                cmdText.AppendFormat(" AND CountyId={0} ", searchInfo.CountyId.Value);
            }

            if (searchInfo.ProvinceId.HasValue)
            {
                cmdText.AppendFormat(" AND ProvinceId={0} ", searchInfo.ProvinceId.Value);
            }

            if (!string.IsNullOrEmpty(searchInfo.ContacterFullname))
            {
                cmdText.AppendFormat(" AND Fullname LIKE '%{0}%' ", searchInfo.ContacterFullname);
            }

            if (!string.IsNullOrEmpty(searchInfo.ContacterMobile))
            {
                cmdText.AppendFormat(" AND Mobile='{0}' ", searchInfo.ContacterMobile);
            }

            if (searchInfo.CustomerTypeId.HasValue)
            {
                cmdText.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanyCustomerTypeConfig WHERE CompanyId=view_Company_ItemsByContacter.CompanyId AND TypeId={0}) ", searchInfo.CustomerTypeId.Value);
            }

            if (searchInfo.SuitProductId.HasValue)
            {
                cmdText.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanySuitProductConfig WHERE CompanyId=view_Company_ItemsByContacter.CompanyId AND SuitProductId={0}) ", searchInfo.SuitProductId.Value);
            }

            if (searchInfo.UserCitys != null && searchInfo.UserCitys.Length > 0)
            {
                cmdText.Append(" AND CityId IN( ");
                cmdText.Append(searchInfo.UserCitys[0].ToString());

                for (int i = 1; i < searchInfo.UserCitys.Length; i++)
                {
                    cmdText.AppendFormat(",{0}", searchInfo.UserCitys[i].ToString());
                }

                cmdText.Append(" ) ");
            }

            if (searchInfo.UserCustomerTypes != null && searchInfo.UserCustomerTypes.Count > 0)
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_CompanyCustomerTypeConfig WHERE CompanyId=view_Company_ItemsByContacter.CompanyId AND TypeId IN( ");
                cmdText.Append(searchInfo.UserCustomerTypes[0].ToString());

                for (int i = 1; i < searchInfo.UserCustomerTypes.Count; i++)
                {
                    cmdText.AppendFormat(",{0}", searchInfo.UserCustomerTypes[i].ToString());
                }

                cmdText.Append(" )) ");
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdText.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.PoolStructure.ContacterInfo contacter = new EyouSoft.Model.PoolStructure.ContacterInfo();

                    contacter.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    contacter.CompanyName = rdr["CompanyName"].ToString();
                    contacter.ContacterId = rdr.GetInt32(rdr.GetOrdinal("ContactId"));
                    contacter.Fullname = rdr["Fullname"].ToString();
                    contacter.Mobile = rdr["Mobile"].ToString();
                    contacter.JobTitle = rdr["JobTitle"].ToString();

                    if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday")))
                    {
                        contacter.Birthday = rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    }

                    contacters.Add(contacter);
                }
            }

            return contacters;
        }

        /// <summary>
        /// 验证手机号码是否存在
        /// </summary>
        /// <param name="companyId">公司编号 为null时不做为查询条件</param>
        /// <param name="mobiles">手机号码集合 添加时item.ContacterId设置0，修改时item.ContacterId设置相应的联系人编号</param>
        /// <returns>重复的号码信息集合</returns>
        public virtual IList<string> ExistsMobiles(string companyId, IList<EyouSoft.Model.PoolStructure.ContacterInfo> mobiles)
        {
            IList<string> existsMobiles = new List<string>();
            StringBuilder cmdText = new StringBuilder();
            StringBuilder cmdNotInText = new StringBuilder();
            cmdText.Append("SELECT DISTINCT [Mobile] FROM [tbl_CompanyContacter] WHERE 1=1 ");

            if (!string.IsNullOrEmpty(companyId))
            {
                cmdText.AppendFormat(" AND CompanyId='{0}' ", companyId);
            }

            if (mobiles != null && mobiles.Count > 0)
            {
                cmdText.AppendFormat(" AND Mobile IN('{0}' ", mobiles[0].Mobile);

                for (int i = 1; i < mobiles.Count; i++)
                {
                    cmdText.AppendFormat(",'{0}'", mobiles[i].Mobile);
                }
                cmdText.Append(")");
            }

            var notInContacter = mobiles.Where(item => item.ContacterId > 0);
            if (notInContacter != null && notInContacter.Count() > 0)
            {
                var i = 0;
                cmdNotInText.Append(" AND ContactId NOT IN( ");
                foreach (var tmp in notInContacter)
                {
                    cmdNotInText.AppendFormat("{0}{1} ", i == 0 ? string.Empty : ",", tmp.ContacterId);
                    i++;
                }
                cmdNotInText.Append(" ) ");
            }

            cmdText.Append(cmdNotInText.ToString());

            DbCommand cmd = this._db.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    existsMobiles.Add(rdr[0].ToString());
                }
            }

            return existsMobiles;
        }

        /// <summary>
        /// 获取用户池公司信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.PoolStructure.CompanyInfo GetCompanyInfo(string companyId)
        {
            EyouSoft.Model.PoolStructure.CompanyInfo info = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetCompanyInfo);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                #region 公司信息
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.PoolStructure.CompanyInfo();
                    info.Address = rdr["Address"].ToString();
                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CompanyName = rdr["CompanyName"].ToString();
                    //info.Contacters = null;
                    info.CountyId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    info.CreateTime = rdr.GetDateTime(rdr.GetOrdinal("CreateTime"));
                    //info.CustomerTypeConfig = null;
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.Remark = rdr["Remark"].ToString();
                    info.Star = rdr["Star"].ToString();
                    //info.SuitProductConfig = null;
                }

                if (info == null) return null;
                #endregion

                #region 联系人
                rdr.NextResult();
                info.Contacters = new List<EyouSoft.Model.PoolStructure.ContacterInfo>();
                while (rdr.Read())
                {
                    var contacter = new EyouSoft.Model.PoolStructure.ContacterInfo();
                    
                    contacter.Character = rdr["Character"].ToString();
                    contacter.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    contacter.ContacterId = rdr.GetInt32(rdr.GetOrdinal("ContactId"));
                    contacter.CreateTime = rdr.GetDateTime(rdr.GetOrdinal("CreateTime"));
                    contacter.Email = rdr["Email"].ToString();
                    contacter.Fullname = rdr["Fullname"].ToString();
                    contacter.Interest = rdr["Interest"].ToString();
                    contacter.JobTitle = rdr["JobTitle"].ToString();
                    contacter.Mobile = rdr["Mobile"].ToString();
                    contacter.QQ = rdr["QQ"].ToString();
                    contacter.Remark = rdr["Remark"].ToString();

                    if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday")))
                    {
                        contacter.Birthday = rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("RememberDay")))
                    {
                        contacter.RememberDay = rdr.GetDateTime(rdr.GetOrdinal("RememberDay"));
                    }

                    info.Contacters.Add(contacter);
                }
                #endregion

                #region  公司类型
                rdr.NextResult();
                info.CustomerTypeConfig=new List<EyouSoft.Model.PoolStructure.CustomerTypeInfo>();
                while (rdr.Read())
                {
                    info.CustomerTypeConfig.Add(new EyouSoft.Model.PoolStructure.CustomerTypeInfo()
                    {
                        TypeId=rdr.GetInt32(rdr.GetOrdinal("TypeId"))
                    });
                }
                #endregion

                #region 适用产品
                rdr.NextResult();
                info.SuitProductConfig = new List<EyouSoft.Model.PoolStructure.SuitProductInfo>();
                while (rdr.Read())
                {
                    info.SuitProductConfig.Add(new EyouSoft.Model.PoolStructure.SuitProductInfo()
                    {
                        ProuctId = rdr.GetInt32(rdr.GetOrdinal("SuitProductId"))
                    });
                }
                #endregion
            }

            return info;
        }
        #endregion
    }
}
