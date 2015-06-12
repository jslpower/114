using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml.Linq;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 公司信息数据层
    /// </summary>
    /// 创建人：张志瑜 2010-05-31
    /// -------------------------
    /// 修改人：鲁功源 2011-04-06
    /// 内容：新增方法GetTopNumNewCompanys(int TopNum,bool? IsHighShop)
    /// -------------------------
    /// 修改人：徐全浩 2011-5-10
    /// 内容：新增方法GetTongHangList
    /// -------------------------
    /// 修改人：华磊   2011-5-19
    /// 内  容：修改方法GetTongHangList
    /// ------------------------
    /// 修改人：郑付杰 2011-12-23
    /// 内容：新增方法 GetNewComapnyScenic(int topNum,EyouSoft.Model.CompanyStructure.CompanyType companyType);
    public class CompanyDetailInfo : DALBase, ICompanyInfo
    {
        /// <summary>
        /// 所在数据库
        /// </summary>
        protected Database _database;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyDetailInfo()
        {
            this._database = base.CompanyStore;
        }

        #region 新增数据SQL语句
        /// <summary>
        /// 新增公司
        /// </summary>
        private const string SQL_CompanyInfo_INSERT = "INSERT INTO tbl_CompanyInfo([Id],[ProvinceId],[CityId],[CountyId],[TypeId],[CompanyName],[CompanyBrand],[CompanyAddress],[License],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[ContactMQ],[ContactQQ],[ContactMSN],[Remark]) VAlUES(@ID,@ProvinceId,@CityId,@CountyId,@TypeId,@CompanyName,@CompanyBrand,@CompanyAddress,@License,@ContactName,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactMQ,@ContactQQ,@ContactMSN,@Remark) ";
        #endregion 新增数据SQL语句

        #region 修改数据SQL语句
        /// <summary>
        /// 修改公司状态
        /// </summary>
        private const string SQL_CompanyInfo_UPDATE_IsEnabled = "UPDATE tbl_CompanyInfo SET isEnabled=@isEnabled WHERE id=@id";
        /// <summary>
        /// 通过注册
        /// </summary>
        private const string SQL_CompanyInfo_UPDATE_PassRegister = @" 
            UPDATE tbl_CompanyInfo SET IsCheck='1',CheckTime=GETDATE(),CompanyLev = @CompanyLev WHERE id=@id;
            UPDATE tbl_ExchangeList SET IsCheck=1 WHERE CompanyId=@id;";
        /// <summary>
        /// 修改公司承诺书状态
        /// </summary>
        private const string SQL_CompanyInfo_UPDATE_CertificateCheck = "UPDATE tbl_CompanyInfo SET isCertificateCheck=@isCheck WHERE id=@id";
        /// <summary>
        /// 修改公司诚信档案状态
        /// </summary>
        private const string SQL_CompanyInfo_UPDATE_CreditCheck = "UPDATE tbl_CompanyInfo SET isCreditCheck=@isCheck WHERE id=@id";
        /// <summary>
        /// 修改公司付费项目
        /// </summary>
        private const string SQL_CompanyPayService_UPDATE = "IF(SELECT COUNT(*) FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=@ServiceId)=0 INSERT INTO tbl_CompanyPayService (CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,@ServiceId,@IsEnabled) ELSE UPDATE tbl_CompanyPayService SET IsEnabled=@IsEnabled WHERE CompanyId=@CompanyId AND ServiceId=@ServiceId";
        #endregion 修改数据SQL语句

        #region 删除数据SQL语句
        /// <summary>
        /// 真实删除公司
        /// </summary>
        private const string SQL_CompanyInfo_DELETE_Real = "DELETE tbl_CompanyInfo WHERE id=@CompanyId";
        /// <summary>
        /// 虚拟删除公司
        /// </summary>
        private const string SQL_CompanyInfo_DELETE_Virtual = "UPDATE tbl_CompanyInfo SET IsDeleted='1' WHERE id=@CompanyId";
        #endregion 删除数据SQL语句

        #region 查询数据SQL语句

        /// <summary>
        /// 查询用户信息
        /// </summary>
        private const string SQL_CompanyUser_SELECT =
            " SELECT Id,UserName FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsAdmin=1;";

        /// <summary>
        /// 查询公司信息
        /// </summary>
        private const string SQL_CompanyInfo_SELECT =
            @" SELECT Id,ProvinceId,CityId,CountyId,CompanyName,CompanyBrand,CompanyAddress,License,ContactName,ContactTel,
                ContactFax,ContactMobile,ContactEmail,ContactMQ,ContactQQ,ContactMSN,CommendPeople,Remark,CommendCount,
                IsEnabled,IssueTime,IsCertificateCheck,IsCreditCheck,LoginCount,LastLoginTime,IsCheck,CompanyType,CheckTime,
                WebSite,ShortRemark,[OPCompanyId],[Introduction],[Scale],[Longitude],[Latitude],[PeerContact],[AlipayAccount], 
                [B2BDisplay],[B2BSort],[B2CDisplay],[B2CSort],[ClickNum],[CompanyLev],[InfoFull],[ContractStart],[ContractEnd]
                ,[IsDeleted]
                FROM tbl_CompanyInfo WHERE ID=@CompanyId;";

        /// <summary>
        /// 查询公司身份信息
        /// </summary>
        private const string SQL_CompanyTypeList_SELECT =
            " SELECT TypeId FROM tbl_CompanyTypeList WHERE CompanyId=@CompanyId;";

        /// <summary>
        /// 查询银行帐号信息
        /// </summary>
        private const string SQL_BankAccount_SELECT =
            @" SELECT [Id],[CompanyId],[BankAccountName],[AccountNumber],[BankName],[TypeId] FROM [tbl_BankAccount] 
                WHERE CompanyId=@CompanyId;";
        /// <summary>
        /// 查询附件信息
        /// </summary>
        private const string SQL_CompanyAttachInfo_SELECT = "SELECT [FieldName],[FieldValue] FROM [tbl_CompanyAttachInfo] WHERE CompanyId=@CompanyId;";

        /// <summary>
        /// 查询公司开通的高级服务信息
        /// </summary>
        private const string SQL_CompanyPayService_SELECT =
            " SELECT [ServiceId],[IsEnabled] FROM [tbl_CompanyPayService] WHERE CompanyId=@CompanyId; ";
        /// <summary>
        /// 查询目的地接社城市列表
        /// </summary>
        private const string SQL_Company_LocalCity_SELECT = "SELECT DISTINCT c.ProvinceId FROM tbl_CompanyInfo AS c,tbl_CompanyTypeList t WHERE c.ID=t.CompanyId AND t.TypeId=3 AND c.IsCheck='1' AND c.IsDeleted='0' AND c.IsEnabled='1'";
        /// <summary>
        /// 查询所有的公司列表
        /// </summary>
        private const string SQL_Company_SELECT = "SELECT top {0} [ID],[CompanyName],[IssueTime] from [view_CompanyInfo_All] where [IsEnabled]='1' {1}";

        /// <summary>
        /// 公司资质查询sql
        /// </summary>
        private const string SqlCompanyQualificationSelect =
            " select [CompanyId],[Qualification] from tbl_CompanyQualification where [CompanyId] = @CompanyId; ";

        #endregion 查询数据SQL语句

        const string SQL_SELECT_GetOrderSmsDstInfo = "SELECT [Id],[CompanyName],[CompanyLev] FROM [tbl_CompanyInfo] WHERE [Id]=@CID;SELECT [ContactMobile] FROM [tbl_CompanyUser] WHERE [CompanyId]=@CID [ContactMobile]>'' AND IsDeleted='0' AND [IsEnable]='1' ORDER BY [IsAdmin] DESC";

        #region 成员方法
        /// <summary>
        /// 获取最新加盟的公司列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="IsHighShop">是否高级网店 =null返回所有</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> GetTopNumNewCompanys(int TopNum, bool? IsHighShop)
        {
            IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> list = new List<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo>();
            string strSql = string.Format(SQL_Company_SELECT, TopNum, IsHighShop.HasValue ? " and id in(select companyid from tbl_CompanyPayService where serviceid=2 and isenabled=1) order by CheckTime DESC" : " order by CheckTime DESC ");
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo model = new EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo();
                    model.ID = dr[dr.GetOrdinal("Id")].ToString();
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) ? "" : dr[dr.GetOrdinal("CompanyName")].ToString();
                    model.RegTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 添加公司(添加公司后将自动生成公司基础信息,公司总帐号,总帐号的MQ号等信息)
        /// </summary>
        /// <param name="model">公司实体</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ResultStructure.UserResultInfo Add(ref EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            EyouSoft.Model.ResultStructure.UserResultInfo result = EyouSoft.Model.ResultStructure.UserResultInfo.Error;
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyInfo_Insert");
            //生成用户ID,公司ID
            model.AdminAccount.ID = Guid.NewGuid().ToString();
            model.ID = Guid.NewGuid().ToString();
            //总帐号信息
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.AdminAccount.ID);
            this._database.AddInParameter(dc, "UserName", DbType.String, model.AdminAccount.UserName);
            this._database.AddInParameter(dc, "MD5Password", DbType.String, model.AdminAccount.PassWordInfo.MD5Password);
            this._database.AddInParameter(dc, "Password", DbType.String, model.AdminAccount.PassWordInfo.NoEncryptPassword);
            this._database.AddInParameter(dc, "EncryptPassword", DbType.String, model.AdminAccount.PassWordInfo.SHAPassword);
            //公司基本信息
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "CountyId", DbType.Int32, model.CountyId);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            this._database.AddInParameter(dc, "CompanyBrand", DbType.String, model.CompanyBrand);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "License", DbType.String, model.License);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CommendPeople", DbType.String, model.CommendPeople);
            this._database.AddInParameter(dc, "ShortRemark", DbType.String, model.ShortRemark);

            #region 2012-12-20 线路改版注册时新增的字段

            _database.AddInParameter(dc, "Introduction", DbType.String, model.Introduction);
            _database.AddInParameter(dc, "Scale", DbType.Byte, (int)model.Scale);
            _database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            _database.AddInParameter(dc, "CompanyLev", DbType.Byte, (int)model.CompanyLev);
            _database.AddInParameter(dc, "InfoFull", DbType.Decimal, model.InfoFull);

            //公司资质
            var xmlRootQualification = new StringBuilder("<ROOT>");
            if (model.Qualification != null && model.Qualification.Count > 0)
            {
                foreach (var t in model.Qualification)
                {
                    xmlRootQualification.Append("<CompanyQualification ");
                    xmlRootQualification.AppendFormat(" Qualification='{0}' ", (int)t);
                    xmlRootQualification.Append(" />");
                }
            }
            xmlRootQualification.Append("</ROOT>");
            _database.AddInParameter(dc, "CompanyQualification", DbType.String, xmlRootQualification.ToString());

            #endregion

            //公司销售城市         
            StringBuilder XMLRootSaleCity = new StringBuilder("<ROOT>");
            if (model.SaleCity != null && model.SaleCity.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.CityBase city in model.SaleCity)
                {
                    if (city != null)
                    {
                        XMLRootSaleCity.AppendFormat("<SaleCity ProvinceId='{0}' ", city.ProvinceId);
                        XMLRootSaleCity.AppendFormat(" CityId='{0}' ", city.CityId);
                        XMLRootSaleCity.Append(" />");
                    }
                }
            }
            XMLRootSaleCity.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRootSaleCity", DbType.String, XMLRootSaleCity.ToString());
            //公司线路区域         
            StringBuilder XMLRootArea = new StringBuilder("<ROOT>");
            if (model.Area != null && model.Area.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.AreaBase area in model.Area)
                {
                    if (area != null)
                    {
                        XMLRootArea.AppendFormat("<Area AreaId='{0}' ", area.AreaId);
                        XMLRootArea.Append(" />");
                    }
                }
            }
            XMLRootArea.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRootArea", DbType.String, XMLRootArea.ToString());
            //企业性质
            this._database.AddInParameter(dc, "BusinessProperties", DbType.Int16, Convert.ToInt16(model.BusinessProperties));
            //公司身份类型
            StringBuilder XMLRootCompanyType = new StringBuilder("<ROOT>");
            if (model.CompanyRole != null && model.CompanyRole.RoleItems != null && model.CompanyRole.Length > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyType type in model.CompanyRole.RoleItems)
                {
                    if (Convert.ToInt32(type) != 0)
                    {
                        XMLRootCompanyType.AppendFormat("<CompanyTypeList TypeId='{0}' ", Convert.ToInt32(type));
                        XMLRootCompanyType.Append(" />");
                    }
                }
            }
            XMLRootCompanyType.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRootCompanyType", DbType.String, XMLRootCompanyType.ToString());
            //设置MQ返回值
            this._database.AddOutParameter(dc, "ReturnMQID", DbType.String, 250);
            //设置返回值
            this._database.AddOutParameter(dc, "ReturnValue", DbType.String, 1);

            //执行存储过程
            DbHelper.RunProcedure(dc, this._database);

            //获取返回值
            object obj = this._database.GetParameterValue(dc, "ReturnValue");
            if (obj != null)
            {
                string ReturnValue = Convert.ToString(obj);
                switch (ReturnValue)
                {
                    case "0":  //操作失败
                        result = EyouSoft.Model.ResultStructure.UserResultInfo.Error;
                        break;
                    case "1":  //操作成功
                        result = EyouSoft.Model.ResultStructure.UserResultInfo.Succeed;
                        object MQIDObj = this._database.GetParameterValue(dc, "ReturnMQID");
                        if (MQIDObj != null)
                            model.ContactInfo.MQ = MQIDObj.ToString();
                        #region 此处执行社区用户插入
                        DbCommand DiscuzDC = base.DiscuzStore.GetStoredProcCommand("Club_createuser");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "username", DbType.String, model.AdminAccount.UserName);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "nickname", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "password", DbType.String, model.AdminAccount.PassWordInfo.MD5Password);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "secques", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "gender", DbType.Int16, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "adminid", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "groupid", DbType.Int32, 10);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "groupexpiry", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extgroupids", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "regip", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "joindate", DbType.String, DateTime.Now.ToString());
                        base.DiscuzStore.AddInParameter(DiscuzDC, "lastip", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "lastvisit", DbType.String, DateTime.Now.ToString());
                        base.DiscuzStore.AddInParameter(DiscuzDC, "lastactivity", DbType.String, DateTime.Now.ToString());
                        base.DiscuzStore.AddInParameter(DiscuzDC, "lastpost", DbType.String, DateTime.Now.ToString());
                        base.DiscuzStore.AddInParameter(DiscuzDC, "lastpostid", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "lastposttitle", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "posts", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "digestposts", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "oltime", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "pageviews", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "credits", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits1", DbType.Int32, 10);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits2", DbType.Int32, 10);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits3", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits4", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits5", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits6", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits7", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "extcredits8", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "avatarshowid", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "email", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Email) ? "" : model.ContactInfo.Email);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "bday", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "sigstatus", DbType.Int32, 1);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "tpp", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "ppp", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "templateid", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "pmsound", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "showemail", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "newsletter", DbType.Int32, 3);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "invisible", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "newpm", DbType.Int32, 1);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "accessmasks", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "salt", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "website", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "icq", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "qq", DbType.String, string.IsNullOrEmpty(model.ContactInfo.QQ) ? "" : model.ContactInfo.QQ);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "yahoo", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "msn", DbType.String, string.IsNullOrEmpty(model.ContactInfo.MSN) ? "" : model.ContactInfo.MSN);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "skype", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "location", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "customstatus", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "avatar", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "avatarwidth", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "avatarheight", DbType.Int32, 0);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "medals", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "bio", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "signature", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "sightml", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "authstr", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "realname", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "idcard", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "mobile", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Mobile) ? "" : model.ContactInfo.Mobile);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "phone", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Tel) ? "" : model.ContactInfo.Tel);
                        DbHelper.RunProcedure(DiscuzDC, base.DiscuzStore);
                        #endregion
                        break;
                    case "2":  //用户名已存在
                        result = EyouSoft.Model.ResultStructure.UserResultInfo.ExistsUserName;
                        break;
                    case "3":  //Email已存在
                        result = EyouSoft.Model.ResultStructure.UserResultInfo.ExistsEmail;
                        break;
                }
            }

            return result;
        }

        public bool AddTest()
        {
            DbCommand dc = this._database.GetStoredProcCommand("AUTORegCompany");
            //this._database.AddInParameter(dc, "AutoUserId", DbType.String, Guid.NewGuid().ToString());
            //this._database.AddInParameter(dc, "AutoCompanyId", DbType.String, Guid.NewGuid().ToString());
            //this._database.AddInParameter(dc, "AutoUserName", DbType.String, DateTime.Now.ToString("MMddhhmmss"));
            //执行存储过程
            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 运营后台修改公司资料,该方法会设置分站,线路区域信息(若不修改密码,请设置密码字段为空)
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        public virtual bool Update(ref EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            return this.UpdateCompanyInfoFun(ref model, 1);
        }
        /// <summary>
        /// 单位信息管理,修改自己公司的档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        public virtual bool UpdateSelf(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            return this.UpdateCompanyInfoFun(ref model, 2);
        }
        /// <summary>
        /// 高级网店修改公司档案资料
        /// </summary>
        /// <param name="model">公司档案信息实体</param>
        /// <returns></returns>
        public virtual bool UpdateArchive(EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model)
        {
            return this.UpdateCompanyInfoFun(ref model, 3);
        }

        /// <summary>
        /// 真实删除公司
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        public virtual bool Delete(params string[] companyIdList)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyInfo_Delete");
            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in companyIdList)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    XMLRoot.AppendFormat("<Company CompanyId='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 移除公司(即虚拟删除公司)
        /// </summary>
        /// <param name="companyIdList">公司ID列表</param>
        /// <returns></returns>
        public virtual bool Remove(params string[] companyIdList)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyInfo_Remove");
            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in companyIdList)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    XMLRoot.AppendFormat("<Company CompanyId='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取公司明细信息实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetModel(string companyid)
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = null;

            //注意:请勿随意调换sql语句的顺序,因为下面读取数据的顺序是与该sql语句一致的
            //公司附件信息,改成在业务逻辑层,调用附件的业务逻辑层获得
            //DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyInfo_SELECT + SQL_CompanyUser_SELECT + SQL_CompanyTypeList_SELECT + SQL_BankAccount_SELECT + SQL_CompanyAttachInfo_SELECT + SQL_CompanyPayService_SELECT);
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyInfo_SELECT + SQL_CompanyUser_SELECT + SQL_CompanyTypeList_SELECT + SQL_BankAccount_SELECT + SQL_CompanyPayService_SELECT + SqlCompanyQualificationSelect);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyid);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                //公司基本信息
                if (dr.Read())
                {
                    //"WebSite,ShortRemark"

                    model = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.ProvinceId = dr.GetInt32(dr.GetOrdinal("provinceId"));
                    model.CityId = dr.GetInt32(dr.GetOrdinal("cityId"));
                    model.CountyId = dr.GetInt32(dr.GetOrdinal("CountyId"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("companyName")) == true ? "" : dr.GetString(dr.GetOrdinal("companyName"));
                    model.CompanyBrand = dr.IsDBNull(dr.GetOrdinal("companyBrand")) == true ? "" : dr.GetString(dr.GetOrdinal("companyBrand"));
                    model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("companyAddress")) == true ? "" : dr.GetString(dr.GetOrdinal("companyAddress"));
                    model.License = dr.IsDBNull(dr.GetOrdinal("license")) == true ? "" : dr.GetString(dr.GetOrdinal("license"));
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("contactName")) == true ? "" : dr.GetString(dr.GetOrdinal("contactName"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("contactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("contactTel"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("contactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("contactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("contactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("contactMobile"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("contactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("contactEmail"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("contactMQ")) == true ? "" : dr.GetString(dr.GetOrdinal("contactMQ"));



                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("commendCount"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("contactQQ")) == true ? "" : dr.GetString(dr.GetOrdinal("contactQQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("contactMSN")) == true ? "" : dr.GetString(dr.GetOrdinal("contactMSN"));
                    model.CommendPeople = dr.IsDBNull(dr.GetOrdinal("CommendPeople")) == true ? "" : dr.GetString(dr.GetOrdinal("CommendPeople"));
                    model.Remark = dr.IsDBNull(dr.GetOrdinal("remark")) == true ? "" : dr.GetString(dr.GetOrdinal("remark"));
                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("CommendCount"));
                    //model.ManageArea = dr.IsDBNull(dr.GetOrdinal("ManageArea")) == true ? "" : dr.GetString(dr.GetOrdinal("ManageArea"));
                    model.StateMore.IsEnabled = dr.GetString(dr.GetOrdinal("isEnabled")) == "1" ? true : false;

                    //判断有无审核通过,并设置加入时间
                    model.StateMore.IsCheck = dr.GetString(dr.GetOrdinal("IsCheck")) == "1" ? true : false;
                    if (model.StateMore.IsCheck)
                    {
                        model.StateMore.JoinTime = dr.IsDBNull(dr.GetOrdinal("CheckTime")) == true ? dr.GetDateTime(dr.GetOrdinal("IssueTime")) : dr.GetDateTime(dr.GetOrdinal("CheckTime"));  //已审核通过
                    }
                    else
                        model.StateMore.JoinTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));   //未审核通过

                    model.StateMore.IsCertificateCheck = dr.GetString(dr.GetOrdinal("isCertificateCheck")) == "1" ? true : false;
                    model.StateMore.IsCreditCheck = dr.GetString(dr.GetOrdinal("isCreditCheck")) == "1" ? true : false;
                    model.StateMore.LoginCount = dr.GetInt32(dr.GetOrdinal("LoginCount"));
                    model.StateMore.LastLoginTime = dr.IsDBNull(dr.GetOrdinal("LastLoginTime")) ? DateTime.MaxValue : dr.GetDateTime(dr.GetOrdinal("LastLoginTime"));
                    model.WebSite = dr.IsDBNull(dr.GetOrdinal("WebSite")) == true ? "" : dr.GetString(dr.GetOrdinal("WebSite"));
                    model.ShortRemark = dr.IsDBNull(dr.GetOrdinal("ShortRemark")) == true ? "" : dr.GetString(dr.GetOrdinal("ShortRemark"));

                    #region 2011-12-20 线路改版新加字段

                    if (!dr.IsDBNull(dr.GetOrdinal("OPCompanyId")))
                        model.OpCompanyId = dr.GetInt32(dr.GetOrdinal("OPCompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Introduction")))
                        model.Introduction = dr.GetString(dr.GetOrdinal("Introduction"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Scale")))
                        model.Scale = (Model.CompanyStructure.CompanyScale)dr.GetByte(dr.GetOrdinal("Scale"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Longitude")))
                        model.Longitude = dr.GetDecimal(dr.GetOrdinal("Longitude"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Latitude")))
                        model.Latitude = dr.GetDecimal(dr.GetOrdinal("Latitude"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PeerContact")))
                        model.PeerContact = dr.GetString(dr.GetOrdinal("PeerContact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AlipayAccount")))
                        model.AlipayAccount = dr.GetString(dr.GetOrdinal("AlipayAccount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay = (Model.CompanyStructure.CompanyB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BSort")))
                        model.B2BSort = dr.GetInt32(dr.GetOrdinal("B2BSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CDisplay")))
                        model.B2CDisplay = (Model.CompanyStructure.CompanyB2CDisplay)dr.GetByte(dr.GetOrdinal("B2CDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CSort")))
                        model.B2CSort = dr.GetInt32(dr.GetOrdinal("B2CSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        model.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLev")))
                        model.CompanyLev = (Model.CompanyStructure.CompanyLev)dr.GetByte(dr.GetOrdinal("CompanyLev"));
                    if (!dr.IsDBNull(dr.GetOrdinal("InfoFull")))
                        model.InfoFull = dr.GetDecimal(dr.GetOrdinal("InfoFull"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContractStart")))
                        model.ContractStart = dr.GetDateTime(dr.GetOrdinal("ContractStart"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContractEnd")))
                        model.ContractEnd = dr.GetDateTime(dr.GetOrdinal("ContractEnd"));

                    model.StateMore.IsDelete = dr.GetString(dr.GetOrdinal("IsDeleted")) == "1" ? true : false;

                    #endregion
                }
                else
                {
                    return model;
                }

                dr.NextResult(); //用户管理员信息
                if (dr.Read())
                {
                    //取管理员帐号信息
                    model.AdminAccount.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.AdminAccount.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true ? "" : dr.GetString(dr.GetOrdinal("UserName"));
                }

                dr.NextResult();//公司身份                 
                while (dr.Read())
                {
                    model.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)dr.GetByte(dr.GetOrdinal("TypeId")));
                }

                dr.NextResult();//公司银行账户
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.BankAccount bank = new EyouSoft.Model.CompanyStructure.BankAccount();
                    bank.AccountNumber = dr.IsDBNull(dr.GetOrdinal("accountNumber")) == true ? "" : dr.GetString(dr.GetOrdinal("accountNumber"));
                    bank.BankAccountName = dr.IsDBNull(dr.GetOrdinal("bankAccountName")) == true ? "" : dr.GetString(dr.GetOrdinal("bankAccountName"));
                    bank.BankName = dr.IsDBNull(dr.GetOrdinal("BankName")) == true ? "" : dr.GetString(dr.GetOrdinal("BankName"));
                    bank.CompanyID = dr.IsDBNull(dr.GetOrdinal("CompanyID")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyID"));
                    bank.AccountType = (EyouSoft.Model.CompanyStructure.BankAccountType)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.BankAccountType), dr.GetByte(dr.GetOrdinal("TypeId")).ToString());
                    model.BankAccounts.Add(bank);
                    bank = null;
                }

                dr.NextResult();  //开通的公司服务
                while (dr.Read())
                {
                    bool IsEnabled = dr.GetString(dr.GetOrdinal("IsEnabled")) == "1" ? true : false;
                    model.StateMore.CompanyService.SetServiceItem(new EyouSoft.Model.CompanyStructure.CompanyServiceItem((EyouSoft.Model.CompanyStructure.SysService)dr.GetInt32(dr.GetOrdinal("ServiceId")), IsEnabled));
                }

                #region 2011-12-20 线路改版新加子表

                //公司资质
                dr.NextResult();
                model.Qualification = new List<Model.CompanyStructure.CompanyQualification>();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("Qualification")))
                        model.Qualification.Add(
                            (Model.CompanyStructure.CompanyQualification)dr.GetByte(dr.GetOrdinal("Qualification")));
                }

                #endregion
            }
            return model;
        }
        /// <summary>
        /// 运营后台获取未审核以及审核通过的单位明细信息列表(不包含银行,附件信息,销售城市,出港城市,线路区域信息)
        /// </summary>
        /// <param name="checkState">审核状态</param>
        /// <param name="query">查询条件实体</param>
        /// <param name="sysUserManageArea">运营后台系统用户所能管理的区域实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetList(EyouSoft.Model.ResultStructure.CheckState checkState, EyouSoft.Model.CompanyStructure.QueryParamsAllCompany query, EyouSoft.Model.SystemStructure.QueryParamsSysUserArea sysUserManageArea, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> list = new List<EyouSoft.Model.CompanyStructure.CompanyDetailInfo>();
            string tableName = "view_CompanyInfo_All";
            string fields = "*";
            string primaryKey = "ID";
            string orderByString = "IssueTime DESC";  //查询未审核的时候的排序条件
            //判断排序条件
            if (checkState == EyouSoft.Model.ResultStructure.CheckState.已通过)
                orderByString = "CheckTime DESC";  //查询已审核的时候的排序条件

            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            //设置查询条件
            //审核状态
            strWhere.AppendFormat(" IsCheck='{0}' ", Convert.ToInt32(checkState));
            //企业性质
            if (query.BusinessProperties != EyouSoft.Model.CompanyStructure.BusinessProperties.全部)
                strWhere.AppendFormat(" AND CompanyType={0} ", Convert.ToInt32(query.BusinessProperties));
            //公司类型
            if (query.CompanyType != EyouSoft.Model.CompanyStructure.CompanyType.全部)
                strWhere.AppendFormat(" AND ID IN (SELECT t.CompanyId FROM tbl_CompanyTypeList AS t WHERE t.CompanyId=view_CompanyInfo_All.ID AND t.TypeId={0})", Convert.ToInt32(query.CompanyType));
            #region 不要的代码
            //switch (queryCityType)
            //{
            //    case 0:   //0:按出港城市查询
            //        StringBuilder inWhere = new StringBuilder();  //in内的查询条件
            //        if (provinceId > 0)
            //            inWhere.AppendFormat("SELECT DISTINCT SITE.CompanyId FROM WHERE tbl_CompanySiteControl AS SITE WHERE SITE.CompanyId=view_CompanyInfo_AdminUser_All.Id AND SITE.ProvinceId={0}", provinceId);
            //        if (provinceId > 0 && portCityId > 0)
            //            inWhere.AppendFormat(" AND SITE.CityId={0})", portCityId);
            //        else if (provinceId == 0 && portCityId > 0)
            //            inWhere.AppendFormat("SELECT DISTINCT SITE.CompanyId FROM WHERE tbl_CompanySiteControl AS SITE WHERE SITE.CompanyId=view_CompanyInfo_AdminUser_All.Id AND SITE.CityId={0})", portCityId);
            //        if (inWhere.Length > 0)
            //            strWhere.AppendFormat(" AND ID IN ({0}) ", inWhere);
            //        break;
            //    case 1:  //1:按注册所在地城市查询
            //        if (provinceId > 0) //按省份,所在地城市查询
            //        {
            //            strWhere.AppendFormat(" AND ProvinceId={0} ", provinceId);
            //        }
            //        if (cityId > 0) //按省份,所在地城市查询
            //        {
            //            strWhere.AppendFormat(" AND ProvinceId={0} ", cityId);
            //        }
            //        break;
            //}
            #endregion 不要的代码
            //1:按注册所在地城市查询
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
            if (!string.IsNullOrEmpty(query.CommendName))
                strWhere.AppendFormat(" AND CommendPeople LIKE '%{0}%' ", query.CommendName);
            if (!string.IsNullOrEmpty(query.ContactName))
                strWhere.AppendFormat(" AND ContactName LIKE '%{0}%' ", query.ContactName);
            if (query.IsQueryOnlineUser.HasValue && query.IsQueryOnlineUser.Value)
                strWhere.AppendFormat(" AND ID IN (SELECT distinct CompanyId FROM [tbl_SysOnlineUser])");
            if (query.LoginStartDate.HasValue)
                strWhere.AppendFormat(" AND ID IN (SELECT distinct CompanyId FROM [tbl_LogUserLogin] WHERE EventTime>'{0}') ", query.LoginStartDate);
            if (query.LoginEndDate.HasValue)
                strWhere.AppendFormat(" AND ID IN (SELECT distinct CompanyId FROM [tbl_LogUserLogin] WHERE EventTime<'{0}') ", query.LoginEndDate);
            //if (systemUserId > 0)
            //    strWhere.AppendFormat(" AND CityId IN (SELECT AreaId FROM tbl_SysUserAreaControl WHERE UserId={0}) ", systemUserId);

            #region 与运营后台用户相关的条件
            //用户可管理的城市区域
            if (sysUserManageArea.ManageCity != null && sysUserManageArea.ManageCity.Length > 0)
            {
                strWhere.Append(" AND CityId IN (");
                int index = 0;
                foreach (int cityId in sysUserManageArea.ManageCity)
                {
                    if (index == 0)
                    {
                        strWhere.Append(cityId);
                        index++;
                    }
                    else
                        strWhere.AppendFormat(",{0}", cityId);
                }
                strWhere.Append(") ");
            }

            //用户可管理的公司类型
            if (sysUserManageArea.ManageCompanyType != null && sysUserManageArea.ManageCompanyType.Count > 0)
            {
                strWhere.Append(" AND ID IN (SELECT t.CompanyId FROM tbl_CompanyTypeList AS t WHERE t.CompanyId=view_CompanyInfo_All.ID AND t.TypeId IN (");

                int index = 0;
                foreach (EyouSoft.Model.CompanyStructure.CompanyType companyType in sysUserManageArea.ManageCompanyType)
                {
                    if (index == 0)
                    {
                        strWhere.Append(Convert.ToInt32(companyType));
                        index++;
                    }
                    else
                        strWhere.AppendFormat(",{0}", Convert.ToInt32(companyType));
                }

                strWhere.Append(")) ");
            }

            if (!string.IsNullOrEmpty(query.Username))
            {
                strWhere.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanyUser AS A WHERE A.CompanyId=view_CompanyInfo_All.Id AND A.Username like '{0}%' ) ", query.Username);
            }

            #endregion 与运营后台用户相关的条件
            #endregion 查询条件

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();

                    #region 初始化model
                    //model.AdminAccount.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true ? "" : dr.GetString(dr.GetOrdinal("UserName"));
                    model.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("CommendCount"));
                    model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("CompanyAddress")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyAddress"));
                    model.CompanyBrand = dr.IsDBNull(dr.GetOrdinal("CompanyBrand")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyBrand"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyName"));
                    //设置公司所有的身份
                    string CompanyRoleListXml = dr.IsDBNull(dr.GetOrdinal("CompanyRoleList")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyRoleList"));
                    if (!String.IsNullOrEmpty(CompanyRoleListXml))
                    {
                        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                        xmlDoc.LoadXml(CompanyRoleListXml);
                        System.Xml.XmlNodeList NodeTypeList = xmlDoc.GetElementsByTagName("tbl_CompanyTypeList");

                        if (NodeTypeList != null && NodeTypeList.Count > 0)
                            foreach (System.Xml.XmlNode node in NodeTypeList)
                                model.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)int.Parse(node.Attributes["TypeId"].Value));
                    }
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("ContactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("ContactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("ContactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMobile"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("ContactMQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("ContactMSN")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMSN"));
                    model.CommendPeople = dr.IsDBNull(dr.GetOrdinal("CommendPeople")) == true ? "" : dr.GetString(dr.GetOrdinal("CommendPeople"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("ContactQQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactQQ"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.StateMore.IsCertificateCheck = dr.GetString(dr.GetOrdinal("isCertificateCheck")) == "1" ? true : false;
                    model.StateMore.IsCreditCheck = dr.GetString(dr.GetOrdinal("isCreditCheck")) == "1" ? true : false;
                    model.StateMore.IsEnabled = dr.GetString(dr.GetOrdinal("isEnabled")) == "1" ? true : false;

                    //判断有无审核通过,并设置加入时间
                    model.StateMore.IsCheck = dr.GetString(dr.GetOrdinal("IsCheck")) == "1" ? true : false;
                    if (model.StateMore.IsCheck)
                    {
                        model.StateMore.JoinTime = dr.IsDBNull(dr.GetOrdinal("CheckTime")) == true ? dr.GetDateTime(dr.GetOrdinal("IssueTime")) : dr.GetDateTime(dr.GetOrdinal("CheckTime"));  //已审核通过
                    }
                    else
                        model.StateMore.JoinTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));   //未审核通过

                    model.License = dr.IsDBNull(dr.GetOrdinal("License")) == true ? "" : dr.GetString(dr.GetOrdinal("License"));
                    model.StateMore.LoginCount = dr.GetInt32(dr.GetOrdinal("LoginCount"));
                    model.StateMore.LastLoginTime = dr.IsDBNull(dr.GetOrdinal("LastLoginTime")) ? DateTime.MaxValue : dr.GetDateTime(dr.GetOrdinal("LastLoginTime"));
                    model.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));

                    //设置公司所有可用或不可用的收费项目
                    string PayServiceXml = dr.IsDBNull(dr.GetOrdinal("PayServiceList")) == true ? "" : dr.GetString(dr.GetOrdinal("PayServiceList"));
                    if (!String.IsNullOrEmpty(PayServiceXml))
                    {
                        System.Xml.XmlDocument xmlDocPayService = new System.Xml.XmlDocument();
                        xmlDocPayService.LoadXml(PayServiceXml);
                        System.Xml.XmlNodeList NodeListPayService = xmlDocPayService.GetElementsByTagName("tbl_CompanyPayService");

                        if (NodeListPayService != null && NodeListPayService.Count > 0)
                            foreach (System.Xml.XmlNode node in NodeListPayService)
                                model.StateMore.CompanyService.SetServiceItem(new EyouSoft.Model.CompanyStructure.CompanyServiceItem((EyouSoft.Model.CompanyStructure.SysService)int.Parse(node.Attributes["ServiceId"].Value), Convert.ToBoolean(Convert.ToInt32(node.Attributes["IsEnabled"].Value))));
                    }
                    #endregion 初始化model

                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取批发商单位列表基本信息
        /// </summary>
        /// <param name="companyIdList">公司ID集合</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetList(IList<string> companyIdList)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> items = new List<EyouSoft.Model.CompanyStructure.CompanyInfo>();
            var strSql = new StringBuilder();
            strSql.Append(" select ");
            strSql.Append(" Id,CompanyName,CompanyBrand,Introduction ");
            strSql.Append(" from tbl_CompanyInfo ");
            strSql.Append(" where IsDeleted = '0' and IsEnabled = '1' ");
            if (companyIdList != null && companyIdList.Any())
            {
                if (companyIdList.Count == 1)
                {
                    strSql.AppendFormat(" and Id = '{0}' ", companyIdList[0]);
                }
                else
                {
                    string strTmp = string.Empty;
                    foreach (var t in companyIdList)
                    {
                        if (t == null || string.IsNullOrEmpty(t))
                            continue;

                        strTmp += "'" + t + "',";
                    }
                    if (!string.IsNullOrEmpty(strTmp))
                    {
                        strTmp = strTmp.TrimEnd(',');
                        strSql.AppendFormat(" and Id in ({0}) ", strTmp);
                    }
                }
            }
            DbCommand dc = _database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, _database))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.CompanyStructure.CompanyInfo();
                    item.CompanyName = rdr.IsDBNull(rdr.GetOrdinal("CompanyName")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("CompanyName"));
                    item.CompanyBrand = rdr.IsDBNull(rdr.GetOrdinal("CompanyBrand")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("CompanyBrand"));
                    item.Introduction = rdr.IsDBNull(rdr.GetOrdinal("Introduction")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("Introduction"));
                    item.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    items.Add(item);
                }
            }
            return items;
        }
        /// <summary>
        /// 获取批发商单位列表图文信息
        /// </summary>
        /// <param name="companyIdList">公司ID集合</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> GetListPicTxt(IList<string> companyIdList)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> items = new List<EyouSoft.Model.CompanyStructure.CompanyPicTxt>();
            using (IDataReader rdr = this.GetListPicOrTxtFun(companyIdList, 2))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyPicTxt item = new EyouSoft.Model.CompanyStructure.CompanyPicTxt();
                    item.CompanyName = rdr.IsDBNull(rdr.GetOrdinal("CompanyName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("CompanyName"));
                    item.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    item.CompanyLogo.ImagePath = rdr.IsDBNull(rdr.GetOrdinal("CompanyLogo")) == true ? "" : rdr.GetString(rdr.GetOrdinal("CompanyLogo"));
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 外部客户获取已审核通过的地接社单位列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListLocalAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            return this.GetListTravelAgencyFun(EyouSoft.Model.CompanyStructure.CompanyType.地接, query.PorvinceId, query.CityId, query.AreaId, query.CompanyName, query.ContactName, query.CompanyBrand, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 外部客户获取已审核通过的所有旅行社单位[专线/批发商,组团/零售商,地接]列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListTravelAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            return this.GetListTravelAgencyFun(EyouSoft.Model.CompanyStructure.CompanyType.全部, query.PorvinceId, query.CityId, 0, query.CompanyName, query.ContactName, query.CompanyBrand, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 外部客户,指定单位类型,获取已审核通过的单位列表(批发商单位可按销售省份,销售城市进行查询/组团社,地接社...单位可按注册所在地的省份,城市进行查询)
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="companyType">公司类型</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListAgency(EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query, EyouSoft.Model.CompanyStructure.CompanyType companyType, int pageSize, int pageIndex, ref int recordCount)
        {
            return this.GetListTravelAgencyFun(companyType, query.PorvinceId, query.CityId, query.AreaId, query.CompanyName, "", "", pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获得目的地地接社城市列表[省份ID]
        /// </summary>
        /// <returns></returns>
        public virtual IList<int> GetListLocalCity()
        {
            IList<int> items = new List<int>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Company_LocalCity_SELECT);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    items.Add(rdr.GetInt32(rdr.GetOrdinal("ProvinceId")));
                }
            }
            return items;
        }
        /// <summary>
        /// 设置公司状态
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isEnabled">true:启用  false:停用</param>
        /// <returns></returns>
        public virtual bool SetState(string companyid, bool isEnabled)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyInfo_UPDATE_IsEnabled);
            this._database.AddInParameter(dc, "isEnabled", DbType.String, isEnabled ? "1" : "0");
            this._database.AddInParameter(dc, "id", DbType.AnsiStringFixedLength, companyid);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 通过注册
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual bool PassRegister(string companyId)
        {
            DbCommand dc =
                this._database.GetSqlStringCommand(SQL_CompanyInfo_UPDATE_PassRegister);
            this._database.AddInParameter(dc, "id", DbType.AnsiStringFixedLength, companyId);
            _database.AddInParameter(dc, "CompanyLev", DbType.Byte, (int)Model.CompanyStructure.CompanyLev.审核商户);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 审核公司承诺书
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        public virtual bool SetCertificateCheck(string companyid, bool isCheck)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyInfo_UPDATE_CertificateCheck);
            this._database.AddInParameter(dc, "isCheck", DbType.AnsiStringFixedLength, isCheck ? "1" : "0");
            this._database.AddInParameter(dc, "id", DbType.AnsiStringFixedLength, companyid);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 审核诚信档案中的证书(营业执照,经营许可证,税务登记证)
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="isCheck">是否审核通过 true:通过  false:不通过</param>
        /// <returns></returns>
        public virtual bool SetCreditCheck(string companyid, bool isCheck)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyInfo_UPDATE_CreditCheck);
            this._database.AddInParameter(dc, "isCheck", DbType.AnsiStringFixedLength, isCheck ? "1" : "0");
            this._database.AddInParameter(dc, "id", DbType.AnsiStringFixedLength, companyid);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 按有无有效产品获取批发商信息集合(统计分析)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="isValid">true:有有效产品 false:无有效产品</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> GetCompanysByValidTour(int pageSize, int pageIndex, ref int recordCount
            , bool isValid, int? cityId, int? areaId, string companyName, string userAreas)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> items = new List<EyouSoft.Model.CompanyStructure.CompanyDetailInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_CompanyInfo";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = "Id,ProvinceId,CityId,CompanyName,ContactName,ContactTel,ContactMobile,ContactMQ,ContactQQ,(SELECT UserName FROM tbl_CompanyUser WHERE CompanyId=tbl_CompanyInfo.Id AND IsAdmin='1') AS UserName ";

            #region 拼接查询条件
            cmdQuery.Append(" IsDeleted='0' AND IsEnabled='1' AND IsCheck='1' ");
            cmdQuery.Append(" AND CompanyType=1 AND EXISTS(SELECT 1 FROM tbl_CompanyTypeList WHERE CompanyId=tbl_CompanyInfo.Id AND TypeId=1) ");

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND CityId IN({0}) ", userAreas);
            }

            if (cityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanyCityControl WHERE CompanyId=tbl_CompanyInfo.Id AND CityId={0}) ", cityId.Value);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanyAreaConfig WHERE CompanyId=tbl_CompanyInfo.Id AND AreaId={0}) ", areaId.Value);
            }

            cmdQuery.AppendFormat(" AND {0} EXISTS(SELECT 1 FROM tbl_TourList WHERE CompanyId=tbl_CompanyInfo.Id AND LeaveDate>=GETDATE() AND IsDelete='0' AND TourState=1 {1} ) ", isValid ? "" : " NOT ", areaId.HasValue ? " and AreaId=" + areaId.ToString() : "");
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo tmp = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();

                    tmp.ID = rdr.GetString(rdr.GetOrdinal("ID"));
                    tmp.CompanyName = rdr["CompanyName"].ToString();
                    tmp.AdminAccount = new EyouSoft.Model.CompanyStructure.UserAccount();
                    tmp.AdminAccount.UserName = rdr["UserName"].ToString();
                    tmp.ContactInfo = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
                    tmp.ContactInfo.ContactName = rdr["ContactName"].ToString();
                    tmp.ContactInfo.Tel = rdr["ContactTel"].ToString();
                    tmp.ContactInfo.Mobile = rdr["ContactMobile"].ToString();
                    tmp.ContactInfo.MQ = rdr["ContactMQ"].ToString();
                    tmp.ContactInfo.QQ = rdr["ContactQQ"].ToString();

                    items.Add(tmp);
                }
            }

            return items;
        }

        /// <summary>
        /// 分页获取同行列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总数</param>
        /// <param name="queryEntity">查询实体</param>
        /// <returns></returns>
        public IList<Model.CompanyStructure.CompanyTongHang> GetTongHangList(int pageSize, int pageIndex,
            ref int recordCount, Model.CompanyStructure.QueryNewCompany queryEntity)
        {
            var ls = new List<Model.CompanyStructure.CompanyTongHang>();
            string tableName = "View_TongHang";
            string primaryKey = "Id";
            string orderByString = " B2BDisplay desc,B2BSort asc,CompanyLev desc,InfoFull desc,LastLoginTime desc ";
            string fields =
                @" [Id],[CompanyName],[ContactQQ],[ContactMQ],[CompanyAddress],[ContactTel],[ContactFax]
                    ,[ContactName],[IssueTime],[ProvinceId],[CityId],[CountyId],[CompanyLev],[B2BDisplay],[B2BSort]
                    ,[InfoFull],[LastLoginTime],[ProvinceName],[CityName],[CountyName],[CompanyLogo],[serviceid]
                    ,[CompanyTypes],[OPCompanyId] ";
            var cmdQuery = new StringBuilder();
            cmdQuery.AppendFormat(" B2BDisplay <> {0} ", (int)Model.CompanyStructure.CompanyB2BDisplay.隐藏);

            #region Where子句拼接

            if (queryEntity.PorvinceId > 0)
                cmdQuery.AppendFormat(" and ProvinceId = {0} ", queryEntity.PorvinceId);
            if (queryEntity.CityId > 0)
                cmdQuery.AppendFormat(" and CityId = {0} ", queryEntity.CityId);
            if (queryEntity.CountyId > 0)
                cmdQuery.AppendFormat(" and CountyId = {0} ", queryEntity.CountyId);
            if (queryEntity.CompanyLev.HasValue)
                cmdQuery.AppendFormat(" and CompanyLev = {0} ", (int)queryEntity.CompanyLev.Value);
            if (queryEntity.B2BDisplay.HasValue)
                cmdQuery.AppendFormat(" and B2BDisplay = {0} ", (int)queryEntity.B2BDisplay.Value);
            if (queryEntity.B2CDisplay.HasValue)
                cmdQuery.AppendFormat(" and B2CDisplay = {0} ", (int)queryEntity.B2CDisplay.Value);
            if (!string.IsNullOrEmpty(queryEntity.CompanyName))
                cmdQuery.AppendFormat(" and CompanyName like '%{0}%' ", queryEntity.CompanyName);
            if (queryEntity.CompanyTypes != null && queryEntity.CompanyTypes.Any())
            {
                string strTmp = string.Empty;
                foreach (var t in queryEntity.CompanyTypes)
                {
                    if (!t.HasValue)
                        continue;

                    strTmp += Convert.ToString((int)t.Value) + ",";
                }
                if (!string.IsNullOrEmpty(strTmp))
                {
                    strTmp = strTmp.TrimEnd(',');
                    cmdQuery.AppendFormat(
                        @" and exists (select 1 from tbl_CompanyTypeList 
                        where tbl_CompanyTypeList.CompanyId = View_TongHang.Id 
                        and tbl_CompanyTypeList.[TypeId] in ({0})) ",
                        strTmp);
                }
            }
            if (!string.IsNullOrEmpty(queryEntity.KeyWord))
            {
                cmdQuery.AppendFormat(
                    @" and isnull(CompanyName,'') + isnull(CompanyAddress,'') like '%{0}%' ",
                    queryEntity.KeyWord);
            }

            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(TourStore, pageSize, pageIndex, ref recordCount, tableName
                , primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    var model = new Model.CompanyStructure.CompanyTongHang();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.ID = dr.GetString(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyName")))
                        model.CompanyName = dr.GetString(dr.GetOrdinal("CompanyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContactMQ")))
                        model.ContactInfo.MQ = dr.GetString(dr.GetOrdinal("ContactMQ"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContactQQ")))
                        model.ContactInfo.QQ = dr.GetString(dr.GetOrdinal("ContactQQ"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyAddress")))
                        model.CompanyAddress = dr.GetString(dr.GetOrdinal("CompanyAddress"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContactTel")))
                        model.ContactInfo.Tel = dr.GetString(dr.GetOrdinal("ContactTel"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContactFax")))
                        model.ContactInfo.Fax = dr.GetString(dr.GetOrdinal("ContactFax"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContactName")))
                        model.ContactInfo.ContactName = dr.GetString(dr.GetOrdinal("ContactName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.StateMore.JoinTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProvinceId")))
                        model.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CityId")))
                        model.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CountyId")))
                        model.CountyId = dr.GetInt32(dr.GetOrdinal("CountyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLev")))
                        model.CompanyLev = (Model.CompanyStructure.CompanyLev)dr.GetByte(dr.GetOrdinal("CompanyLev"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay = (Model.CompanyStructure.CompanyB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BSort")))
                        model.B2BSort = dr.GetInt32(dr.GetOrdinal("B2BSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("InfoFull")))
                        model.InfoFull = dr.GetDecimal(dr.GetOrdinal("InfoFull"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LastLoginTime")))
                        model.StateMore.LastLoginTime = dr.GetDateTime(dr.GetOrdinal("LastLoginTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProvinceName")))
                        model.ProvinceName = dr.GetString(dr.GetOrdinal("ProvinceName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CityName")))
                        model.CityName = dr.GetString(dr.GetOrdinal("CityName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CountyName")))
                        model.CountyName = dr.GetString(dr.GetOrdinal("CountyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLogo")))
                    {
                        model.AttachInfo.CompanyLogo = new Model.CompanyStructure.CompanyLogo
                        {
                            ImageLink = string.Empty,
                            ImagePath = dr.GetString(dr.GetOrdinal("CompanyLogo")),
                            ThumbPath = dr.GetString(dr.GetOrdinal("CompanyLogo"))
                        };
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("serviceid")))
                        model.ServiceId = dr.GetInt32(dr.GetOrdinal("serviceid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OPCompanyId")))
                        model.OpCompanyId = dr.GetInt32(dr.GetOrdinal("OPCompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyTypes")))
                    {
                        XElement xElement = XElement.Parse(dr["CompanyTypes"].ToString());
                        var companyTypes = xElement.Elements("tbl_CompanyTypeList");
                        if (companyTypes.Any())
                        {
                            foreach (var companyType in companyTypes)
                            {
                                model.CompanyRole.SetRole((Model.CompanyStructure.CompanyType)int.Parse(HotelBI.Utils.GetXAttributeValue(companyType, "typeid")));
                            }
                        }
                    }

                    ls.Add(model);
                }
            }

            return ls;
        }

        /// <summary>
        /// 根据公司编号获取当期公司用户的登录记录列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总的记录数</param>
        /// <param name="CompanyId">公司编号 =""返回全部</param>
        /// <param name="startTime">起始时间 为null时不做为查询条件</param>
        /// <param name="finishTime">截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.LogUserLogin> GetLoginList(int pageSize, int pageIndex, ref int recordCount, string CompanyId, DateTime? startTime, DateTime? finishTime)
        {
            IList<EyouSoft.Model.CompanyStructure.LogUserLogin> list = new List<EyouSoft.Model.CompanyStructure.LogUserLogin>();
            string tableName = "tbl_LogUserLogin";
            string fields = "EventID,OperatorName,ContactName,EventArea,EventIP,EventTime";
            string primaryKey = "EventID";
            string orderByString = " EventTime desc ";
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strWhere.AppendFormat(" and CompanyId='{0}' ", CompanyId);
            }

            if (startTime.HasValue)
            {
                strWhere.AppendFormat(" AND EventTime>='{0}' ", startTime.Value);
            }

            if (finishTime.HasValue)
            {
                strWhere.AppendFormat(" AND EventTime<='{0}' ", finishTime.Value);
            }

            using (IDataReader dr = DbHelper.ExecuteReader(this.LogStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.LogUserLogin model = new EyouSoft.Model.CompanyStructure.LogUserLogin();
                    model.EventID = dr.GetString(0);
                    model.OperatorName = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ContactName = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.EventArea = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.EventIP = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.EventTime = dr.IsDBNull(5) ? DateTime.Now : dr.GetDateTime(5);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        /// <summary>
        /// 根据公司ID获取某段时间段内该公司的登录次数
        /// </summary>
        /// <param name="CompanyId">公司ID(必须传值)</param>
        /// <param name="StartTime">开始时间(为null不作条件)</param>
        /// <param name="EndTime">结束时间(为null不作条件)</param>
        /// <returns>登录次数</returns>
        public virtual int GetLoginCountByTime(string CompanyId, DateTime? StartTime, DateTime? EndTime)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return 0;

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select count(*) from tbl_LogUserLogin where CompanyId = '{0}' ", CompanyId);
            if (StartTime.HasValue)
                strSql.AppendFormat(" AND EventTime >= '{0}' ", StartTime.Value);
            if (EndTime.HasValue)
                strSql.AppendFormat(" AND EventTime <= '{0}' ", EndTime.Value);

            DbCommand dc = base.CompanyStore.GetSqlStringCommand(strSql.ToString());

            object obj = DbHelper.GetSingle(dc, base.CompanyStore);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
                return 0;
        }

        /// <summary>
        /// 设置公司付费项目状态
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="serviceItem">某项服务项目</param>
        /// <returns></returns>
        public virtual bool SetCompanyPayService(string companyId, EyouSoft.Model.CompanyStructure.CompanyServiceItem serviceItem)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyPayService_UPDATE);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "ServiceId", DbType.Int32, Convert.ToInt32(serviceItem.Service));
            this._database.AddInParameter(dc, "IsEnabled", DbType.AnsiStringFixedLength, serviceItem.IsEnabled ? "1" : "0");
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 修改公司明细资料(若不修改密码,请设置密码字段为空)
        /// </summary>
        /// <param name="model">公司实体</param>
        /// <param name="UpdateType">修改类型:1:运营后台修改公司资料,2:单位信息管理修改自己公司资料,3:高级网店修改公司基本档案)</param>
        /// <returns></returns>
        private bool UpdateCompanyInfoFun(ref EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model, int UpdateType)
        {
            bool isTrue = false;
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyInfo_Update");
            //总帐号信息
            if (string.IsNullOrEmpty(model.AdminAccount.PassWordInfo.MD5Password))
                model.AdminAccount.PassWordInfo.SetEncryptPassWord("", "", "");
            this._database.AddInParameter(dc, "UpdateType", DbType.String, UpdateType);
            this._database.AddInParameter(dc, "MD5Password", DbType.String, model.AdminAccount.PassWordInfo.MD5Password);
            this._database.AddInParameter(dc, "Password", DbType.String, model.AdminAccount.PassWordInfo.NoEncryptPassword);
            this._database.AddInParameter(dc, "EncryptPassword", DbType.String, model.AdminAccount.PassWordInfo.SHAPassword);
            //公司基本信息
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "CountyId", DbType.Int32, model.CountyId);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            this._database.AddInParameter(dc, "CompanyBrand", DbType.String, model.CompanyBrand);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "License", DbType.String, model.License);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._database.AddInParameter(dc, "ShortRemark", DbType.String, model.ShortRemark);
            //公司附件信息            
            this._database.AddInParameter(dc, "CompanyImg", DbType.String, model.AttachInfo.CompanyImg.ImagePath);
            this._database.AddInParameter(dc, "CompanyLogo", DbType.String, model.AttachInfo.CompanyLogo.ImagePath);
            this._database.AddInParameter(dc, "CompanySignet", DbType.String, model.AttachInfo.CompanySignet);
            this._database.AddInParameter(dc, "BusinessCertImg", DbType.String, model.AttachInfo.BusinessCertif.BusinessCertImg);
            this._database.AddInParameter(dc, "LicenceImg", DbType.String, model.AttachInfo.BusinessCertif.LicenceImg);
            this._database.AddInParameter(dc, "TaxRegImg", DbType.String, model.AttachInfo.BusinessCertif.TaxRegImg);

            #region 2012-12-20 线路改版修改公司信息时新增的字段

            _database.AddInParameter(dc, "Introduction", DbType.String, model.Introduction);
            _database.AddInParameter(dc, "Scale", DbType.Byte, (int)model.Scale);
            _database.AddInParameter(dc, "CompanyLev", DbType.Byte, (int)model.CompanyLev);
            _database.AddInParameter(dc, "InfoFull", DbType.Decimal, model.InfoFull);
            _database.AddInParameter(dc, "AlipayAccount", DbType.String, model.AlipayAccount);
            _database.AddInParameter(dc, "Longitude", DbType.Decimal, model.Longitude);
            _database.AddInParameter(dc, "Latitude", DbType.Decimal, model.Latitude);
            _database.AddInParameter(dc, "PeerContact", DbType.String, model.PeerContact);
            _database.AddInParameter(dc, "WarrantImg", DbType.String, model.AttachInfo.BusinessCertif.WarrantImg);
            _database.AddInParameter(dc, "PersonCardImg", DbType.String, model.AttachInfo.BusinessCertif.PersonCardImg);
            _database.AddInParameter(dc, "B2BDisplay", DbType.Byte, (int)model.B2BDisplay);
            _database.AddInParameter(dc, "B2BSort", DbType.Int32, model.B2BSort);
            _database.AddInParameter(dc, "B2CDisplay", DbType.Byte, (int)model.B2CDisplay);
            _database.AddInParameter(dc, "B2CSort", DbType.Int32, model.B2CSort);
            _database.AddInParameter(dc, "ContractStart", DbType.DateTime, model.ContractStart);
            _database.AddInParameter(dc, "ContractEnd", DbType.DateTime, model.ContractEnd);
            _database.AddInParameter(dc, "WebSite", DbType.String, model.WebSite);

            //公司资质
            var xmlRootQualification = new StringBuilder("<ROOT>");
            if (model.Qualification != null && model.Qualification.Count > 0)
            {
                foreach (var t in model.Qualification)
                {
                    xmlRootQualification.Append("<CompanyQualification ");
                    xmlRootQualification.AppendFormat(" Qualification='{0}' ", (int)t);
                    xmlRootQualification.Append(" />");
                }
            }
            xmlRootQualification.Append("</ROOT>");
            _database.AddInParameter(dc, "CompanyQualification", DbType.String, xmlRootQualification.ToString());

            //新版公司宣传图片
            var xmlCompanyPublicityPhoto = new StringBuilder("<ROOT>");
            if (model.AttachInfo.CompanyPublicityPhoto != null && model.AttachInfo.CompanyPublicityPhoto.Count > 0)
            {
                foreach (var t in model.AttachInfo.CompanyPublicityPhoto)
                {
                    xmlCompanyPublicityPhoto.Append("<CompanyPublicityPhoto ");
                    xmlCompanyPublicityPhoto.AppendFormat(" PhotoIndex='{0}' ", t.PhotoIndex);
                    xmlCompanyPublicityPhoto.AppendFormat(" ImagePath='{0}' ", t.ImagePath);
                    xmlCompanyPublicityPhoto.AppendFormat(" ThumbPath='{0}' ", t.ThumbPath);
                    xmlCompanyPublicityPhoto.Append(" />");
                }
            }
            xmlCompanyPublicityPhoto.Append("</ROOT>");
            _database.AddInParameter(dc, "CompanyPublicityPhoto", DbType.String, xmlCompanyPublicityPhoto.ToString());

            #endregion

            //公司银行帐号信息
            StringBuilder strSqlBankRoot = new StringBuilder("<ROOT>");
            if (model.BankAccounts != null && model.BankAccounts.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.BankAccount bankModel in model.BankAccounts)
                {
                    if (bankModel != null)
                    {
                        strSqlBankRoot.AppendFormat("<BankAccountsUpdate AccountNumber='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(bankModel.AccountNumber));
                        strSqlBankRoot.AppendFormat(" TypeId='{0}' ", Convert.ToInt32(bankModel.AccountType));
                        strSqlBankRoot.AppendFormat(" BankAccountName='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(bankModel.BankAccountName));
                        strSqlBankRoot.AppendFormat(" BankName='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(bankModel.BankName));
                        strSqlBankRoot.Append(" />");
                    }
                }
            }
            strSqlBankRoot.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLBankRoot", DbType.String, strSqlBankRoot.ToString());

            //公司销售城市         
            StringBuilder XMLRootSaleCity = new StringBuilder("<ROOT>");
            if (model.SaleCity != null && model.SaleCity.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.CityBase city in model.SaleCity)
                {
                    if (city != null)
                    {
                        XMLRootSaleCity.AppendFormat("<SaleCity ProvinceId='{0}' ", city.ProvinceId);
                        XMLRootSaleCity.AppendFormat(" CityId='{0}' ", city.CityId);
                        XMLRootSaleCity.Append(" />");
                    }
                }
            }
            XMLRootSaleCity.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRootSaleCity", DbType.String, XMLRootSaleCity.ToString());
            //公司线路区域         
            StringBuilder XMLRootArea = new StringBuilder("<ROOT>");
            if (model.Area != null && model.Area.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.AreaBase area in model.Area)
                {
                    if (area != null)
                    {
                        XMLRootArea.AppendFormat("<Area AreaId='{0}' ", area.AreaId);
                        XMLRootArea.Append(" />");
                    }
                }
            }
            XMLRootArea.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRootArea", DbType.String, XMLRootArea.ToString());
            //////////////公司类型未实现完/////////////(换了公司身份后,公司身份只允许新增,不允许删除,管理员角色权限要同步修改)
            /////////////修改公司的线路区域的时候,不能对原数据进行删除,不然原设置过的线路区域设置就无效了
            //企业性质
            this._database.AddInParameter(dc, "BusinessProperties", DbType.Int16, Convert.ToInt16(model.BusinessProperties));
            //公司身份类型
            StringBuilder XMLRootCompanyType = new StringBuilder("<ROOT>");
            if (model.CompanyRole != null && model.CompanyRole.RoleItems != null && model.CompanyRole.Length > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyType type in model.CompanyRole.RoleItems)
                {
                    if (Convert.ToInt32(type) != 0)
                    {
                        XMLRootCompanyType.AppendFormat("<CompanyTypeList TypeId='{0}' ", Convert.ToInt32(type));
                        XMLRootCompanyType.Append(" />");
                    }
                }
            }
            XMLRootCompanyType.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRootCompanyType", DbType.String, XMLRootCompanyType.ToString());

            //设置返回值,管理员用户ID
            this._database.AddOutParameter(dc, "UserId", DbType.String, 36);
            //设置返回值
            this._database.AddOutParameter(dc, "ReturnValue", DbType.String, 1);

            //执行存储过程
            DbHelper.RunProcedure(dc, this._database);

            //获取返回值
            object obj = this._database.GetParameterValue(dc, "ReturnValue");
            if (obj != null)
            {
                switch (Convert.ToString(obj))
                {
                    case "0":  //操作失败
                        isTrue = false;
                        break;
                    case "1":  //操作成功
                        isTrue = true;
                        //获取UserId返回值
                        object UserIdObj = this._database.GetParameterValue(dc, "UserId");
                        if (UserIdObj != null && !string.IsNullOrEmpty(UserIdObj.ToString()) && model.AdminAccount != null)
                            model.AdminAccount.ID = UserIdObj.ToString();
                        break;
                }
            }

            return isTrue;
        }
        /// <summary>
        /// 获取批发商单位列表图文信息或基本信息
        /// </summary>
        /// <param name="companyIdList">公司ID集合</param>
        /// <param name="TypeId">查询类型 1:查询公司ID,名称  2:查询公司ID,名称,企业LOGO</param>
        /// <returns></returns>
        private IDataReader GetListPicOrTxtFun(IList<string> companyIdList, int TypeId)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyIDNAME_Select");
            StringBuilder XMLRoot = new StringBuilder();
            XMLRoot.Append("<ROOT>");
            if (companyIdList != null && companyIdList.Count > 0)
            {
                int i = 0;
                foreach (string companyId in companyIdList)
                {
                    XMLRoot.AppendFormat("<CompanyInfo CompanyId='{0}' SortId='{1}' ", companyId, i++);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            this._database.AddInParameter(dc, "TypeId", DbType.Int32, TypeId);
            //执行存储过程,并返回
            return DbHelper.RunReaderProcedure(dc, this._database);
        }
        /// <summary>
        /// 获得外部客户获取已审核通过的旅行社单位[专线/批发商,组团/零售商,地接]列表
        /// </summary>
        /// <param name="companyType">公司类型 0: 所有旅行社  1:批发商,按批发商的销售城市ID查询  2:组团社 3:地接</param>
        /// <param name="porvinceId">省份ID</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="contactName">联系人</param>
        /// <param name="companyBrand">公司品牌</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListTravelAgencyFun(EyouSoft.Model.CompanyStructure.CompanyType companyType, int porvinceId, int cityId, int areaId, string companyName, string contactName, string companyBrand, int pageSize, int pageIndex, ref int recordCount)
        {
            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            //设置查询条件
            strWhere.Append("IsDeleted='0' AND IsCheck='1' AND IsEnabled='1'");
            //根据companyType查询
            switch (companyType)
            {
                case EyouSoft.Model.CompanyStructure.CompanyType.全部:    //所有旅行社
                    strWhere.Append("  AND CompanyType=1");
                    if (porvinceId > 0) //按注册省份查询
                        strWhere.AppendFormat(" AND ProvinceId={0} ", porvinceId);
                    if (cityId > 0) //按注册城市查询
                        strWhere.AppendFormat(" AND CityId={0} ", cityId);
                    break;
                case EyouSoft.Model.CompanyStructure.CompanyType.专线:   //批发商,未完成这两句sql语句的效率测试比较
                    strWhere.Append("  AND CompanyType=1");
                    strWhere.Append(" AND ID IN (SELECT T.CompanyId FROM tbl_CompanyTypeList AS T WHERE T.TypeId=1 AND T.CompanyId=tbl_CompanyInfo.ID)");
                    //strWhere.Append(" AND ID IN (SELECT T.CompanyId FROM tbl_CompanyTypeList AS T WHERE T.TypeId=1)");
                    if (porvinceId > 0) //按销售省份查询
                    {
                        strWhere.AppendFormat(" AND ID IN (SELECT salecity.CompanyId FROM tbl_CompanyCityControl AS salecity WHERE salecity.CompanyId=tbl_CompanyInfo.ID AND salecity.ProvinceId={0}", porvinceId);
                        if (cityId > 0) //再按销售城市查询
                            strWhere.AppendFormat(" AND salecity.CityId={0}) ", cityId);
                        else
                            strWhere.Append(") ");
                    }
                    else if (cityId > 0) //只按销售城市查询
                        strWhere.AppendFormat(" AND ID IN (SELECT salecity.CompanyId FROM tbl_CompanyCityControl AS salecity WHERE salecity.CompanyId=tbl_CompanyInfo.ID AND salecity.CityId={0}) ", cityId);
                    break;
                default:    //其他类型都按注册省份,注册城市查询
                    strWhere.AppendFormat(" AND ID IN (SELECT T.CompanyId FROM tbl_CompanyTypeList AS T WHERE T.TypeId={0} AND T.CompanyId=tbl_CompanyInfo.ID)", Convert.ToInt32(companyType));
                    if (porvinceId > 0) //按注册省份查询
                        strWhere.AppendFormat(" AND ProvinceId={0} ", porvinceId);
                    if (cityId > 0) //按注册城市查询
                        strWhere.AppendFormat(" AND CityId={0} ", cityId);
                    break;
            }
            if (areaId > 0) //按线路区域查询
            {
                //未完成这两句sql语句的效率测试比较
                strWhere.AppendFormat(" AND ID IN (SELECT area.CompanyId FROM tbl_CompanyAreaConfig area WHERE area.AreaId={0} AND area.CompanyId=tbl_CompanyInfo.ID) ", areaId);
                //strWhere.AppendFormat(" AND ID IN (SELECT area.CompanyId FROM tbl_CompanyAreaConfig AS area WHERE area.AreaId={0}) ", areaId);
            }
            if (!string.IsNullOrEmpty(companyName))
                strWhere.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            if (!string.IsNullOrEmpty(contactName))
                strWhere.AppendFormat(" AND ContactName LIKE '%{0}%' ", contactName);
            if (!string.IsNullOrEmpty(companyBrand))
                strWhere.AppendFormat(" AND CompanyBrand LIKE '%{0}%' ", companyBrand);
            #endregion 查询条件

            return this.GetListAgencyFun(strWhere.ToString(), pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获得外部客户获取已审核通过的单位列表内部函数
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListAgencyFun(string strWhere, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> list = new List<EyouSoft.Model.CompanyStructure.CompanyInfo>();
            string tableName = "tbl_CompanyInfo";
            string fields = " Id,ProvinceId,CityId,CompanyName,CompanyBrand,CompanyAddress,License,ContactName,ContactTel,ContactFax,ContactMobile,ContactEmail,ContactMQ,ContactQQ,ContactMSN,Remark,WebSite,ShortRemark,OPCompanyId ";
            string primaryKey = "ID";
            string orderByString = "CheckTime DESC";

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere, orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyInfo model = new EyouSoft.Model.CompanyStructure.CompanyInfo();

                    #region 初始化model
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    model.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyName"));
                    model.CompanyBrand = dr.IsDBNull(dr.GetOrdinal("CompanyBrand")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyBrand"));
                    model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("CompanyAddress")) == true ? "" : dr.GetString(dr.GetOrdinal("CompanyAddress"));
                    model.License = dr.IsDBNull(dr.GetOrdinal("License")) == true ? "" : dr.GetString(dr.GetOrdinal("License"));
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("ContactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("ContactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMobile"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("ContactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("ContactMQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMQ"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("ContactQQ")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactQQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("ContactMSN")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactMSN"));
                    model.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) == true ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    model.ShortRemark = dr.IsDBNull(dr.GetOrdinal("ShortRemark")) == true ? "" : dr.GetString(dr.GetOrdinal("ShortRemark"));
                    model.WebSite = dr.IsDBNull(dr.GetOrdinal("WebSite")) == true ? "" : dr.GetString(dr.GetOrdinal("WebSite"));
                    model.OpCompanyId = dr.IsDBNull(dr.GetOrdinal("OPCompanyId"))
                                            ? 0
                                            : dr.GetInt32(dr.GetOrdinal("OPCompanyId"));

                    #endregion 初始化model

                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion 内部方法

        #region 2011-12-20 线路改版新增方法

        /// <summary>
        /// 根据公司自增编号获取公司明细信息实体
        /// </summary>
        /// <param name="opCompanyId">公司自增编号</param>
        /// <returns></returns>
        public virtual Model.CompanyStructure.CompanyDetailInfo GetModel(int opCompanyId)
        {
            Model.CompanyStructure.CompanyDetailInfo model = null;
            if (opCompanyId <= 0)
                return model;

            var strSql = new StringBuilder();
            //根据自增编号查询guid编号
            strSql.Append(@" declare @CompanyId char(36); ");
            strSql.Append(@" select @CompanyId = Id FROM tbl_CompanyInfo WHERE OPCompanyId = @OPCompanyId ; ");
            //公司信息
            strSql.Append(
                @" SELECT Id,ProvinceId,CityId,CountyId,CompanyName,CompanyBrand,CompanyAddress,License,ContactName,ContactTel,
                ContactFax,ContactMobile,ContactEmail,ContactMQ,ContactQQ,ContactMSN,CommendPeople,Remark,CommendCount,
                IsEnabled,IssueTime,IsCertificateCheck,IsCreditCheck,LoginCount,LastLoginTime,IsCheck,CompanyType,CheckTime,
                WebSite,ShortRemark,[OPCompanyId],[Introduction],[Scale],[Longitude],[Latitude],[PeerContact],[AlipayAccount], 
                [B2BDisplay],[B2BSort],[B2CDisplay],[B2CSort],[ClickNum],[CompanyLev],[InfoFull],[ContractStart],[ContractEnd]
                FROM tbl_CompanyInfo WHERE ID=@CompanyId; ");
            //用户信息
            strSql.Append(@" SELECT Id,UserName FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsAdmin=1; ");
            //公司身份
            strSql.Append(@" SELECT TypeId FROM tbl_CompanyTypeList WHERE CompanyId=@CompanyId; ");
            //公司账户信息
            strSql.Append(@" SELECT [Id],[CompanyId],[BankAccountName],[AccountNumber],[BankName],[TypeId] FROM [tbl_BankAccount] 
                WHERE CompanyId=@CompanyId; ");
            //公司开通的高级服务
            strSql.Append(@" SELECT [ServiceId],[IsEnabled] FROM [tbl_CompanyPayService] WHERE CompanyId=@CompanyId; ");
            //公司资质
            strSql.Append(@" select [CompanyId],[Qualification] from tbl_CompanyQualification where [CompanyId] = @CompanyId; ");

            DbCommand dc = _database.GetSqlStringCommand(strSql.ToString());
            _database.AddInParameter(dc, "OPCompanyId", DbType.Int32, opCompanyId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                //公司基本信息
                if (dr.Read())
                {
                    //"WebSite,ShortRemark"

                    model = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.ProvinceId = dr.GetInt32(dr.GetOrdinal("provinceId"));
                    model.CityId = dr.GetInt32(dr.GetOrdinal("cityId"));
                    model.CountyId = dr.GetInt32(dr.GetOrdinal("CountyId"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("companyName")) == true
                                            ? ""
                                            : dr.GetString(dr.GetOrdinal("companyName"));
                    model.CompanyBrand = dr.IsDBNull(dr.GetOrdinal("companyBrand")) == true
                                             ? ""
                                             : dr.GetString(dr.GetOrdinal("companyBrand"));
                    model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("companyAddress")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("companyAddress"));
                    model.License = dr.IsDBNull(dr.GetOrdinal("license")) == true
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("license"));
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("contactName")) == true
                                                        ? ""
                                                        : dr.GetString(dr.GetOrdinal("contactName"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("contactTel")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("contactTel"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("contactFax")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("contactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("contactMobile")) == true
                                                   ? ""
                                                   : dr.GetString(dr.GetOrdinal("contactMobile"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("contactEmail")) == true
                                                  ? ""
                                                  : dr.GetString(dr.GetOrdinal("contactEmail"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("contactMQ")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("contactMQ"));



                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("commendCount"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("contactQQ")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("contactQQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("contactMSN")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("contactMSN"));
                    model.CommendPeople = dr.IsDBNull(dr.GetOrdinal("CommendPeople")) == true
                                              ? ""
                                              : dr.GetString(dr.GetOrdinal("CommendPeople"));
                    model.Remark = dr.IsDBNull(dr.GetOrdinal("remark")) == true
                                       ? ""
                                       : dr.GetString(dr.GetOrdinal("remark"));
                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("CommendCount"));
                    //model.ManageArea = dr.IsDBNull(dr.GetOrdinal("ManageArea")) == true ? "" : dr.GetString(dr.GetOrdinal("ManageArea"));
                    model.StateMore.IsEnabled = dr.GetString(dr.GetOrdinal("isEnabled")) == "1" ? true : false;

                    //判断有无审核通过,并设置加入时间
                    model.StateMore.IsCheck = dr.GetString(dr.GetOrdinal("IsCheck")) == "1" ? true : false;
                    if (model.StateMore.IsCheck)
                    {
                        model.StateMore.JoinTime = dr.IsDBNull(dr.GetOrdinal("CheckTime")) == true
                                                       ? dr.GetDateTime(dr.GetOrdinal("IssueTime"))
                                                       : dr.GetDateTime(dr.GetOrdinal("CheckTime")); //已审核通过
                    }
                    else
                        model.StateMore.JoinTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")); //未审核通过

                    model.StateMore.IsCertificateCheck = dr.GetString(dr.GetOrdinal("isCertificateCheck")) == "1"
                                                             ? true
                                                             : false;
                    model.StateMore.IsCreditCheck = dr.GetString(dr.GetOrdinal("isCreditCheck")) == "1" ? true : false;
                    model.StateMore.LoginCount = dr.GetInt32(dr.GetOrdinal("LoginCount"));
                    model.StateMore.LastLoginTime = dr.IsDBNull(dr.GetOrdinal("LastLoginTime"))
                                                        ? DateTime.MaxValue
                                                        : dr.GetDateTime(dr.GetOrdinal("LastLoginTime"));
                    model.WebSite = dr.IsDBNull(dr.GetOrdinal("WebSite")) == true
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("WebSite"));
                    model.ShortRemark = dr.IsDBNull(dr.GetOrdinal("ShortRemark")) == true
                                            ? ""
                                            : dr.GetString(dr.GetOrdinal("ShortRemark"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OPCompanyId")))
                        model.OpCompanyId = dr.GetInt32(dr.GetOrdinal("OPCompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Introduction")))
                        model.Introduction = dr.GetString(dr.GetOrdinal("Introduction"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Scale")))
                        model.Scale = (Model.CompanyStructure.CompanyScale)dr.GetByte(dr.GetOrdinal("Scale"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Longitude")))
                        model.Longitude = dr.GetDecimal(dr.GetOrdinal("Longitude"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Latitude")))
                        model.Latitude = dr.GetDecimal(dr.GetOrdinal("Latitude"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PeerContact")))
                        model.PeerContact = dr.GetString(dr.GetOrdinal("PeerContact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AlipayAccount")))
                        model.AlipayAccount = dr.GetString(dr.GetOrdinal("AlipayAccount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay =
                            (Model.CompanyStructure.CompanyB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BSort")))
                        model.B2BSort = dr.GetInt32(dr.GetOrdinal("B2BSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CDisplay")))
                        model.B2CDisplay =
                            (Model.CompanyStructure.CompanyB2CDisplay)dr.GetByte(dr.GetOrdinal("B2CDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CSort")))
                        model.B2CSort = dr.GetInt32(dr.GetOrdinal("B2CSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        model.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLev")))
                        model.CompanyLev = (Model.CompanyStructure.CompanyLev)dr.GetByte(dr.GetOrdinal("CompanyLev"));
                    if (!dr.IsDBNull(dr.GetOrdinal("InfoFull")))
                        model.InfoFull = dr.GetDecimal(dr.GetOrdinal("InfoFull"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContractStart")))
                        model.ContractStart = dr.GetDateTime(dr.GetOrdinal("ContractStart"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContractEnd")))
                        model.ContractEnd = dr.GetDateTime(dr.GetOrdinal("ContractEnd"));
                }
                else
                {
                    return model;
                }

                dr.NextResult(); //用户管理员信息
                if (dr.Read())
                {
                    //取管理员帐号信息
                    model.AdminAccount.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("ID"));
                    model.AdminAccount.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true
                                                      ? ""
                                                      : dr.GetString(dr.GetOrdinal("UserName"));
                }

                dr.NextResult(); //公司身份                 
                while (dr.Read())
                {
                    model.CompanyRole.SetRole(
                        (EyouSoft.Model.CompanyStructure.CompanyType)dr.GetByte(dr.GetOrdinal("TypeId")));
                }

                dr.NextResult(); //公司银行账户
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.BankAccount bank = new EyouSoft.Model.CompanyStructure.BankAccount();
                    bank.AccountNumber = dr.IsDBNull(dr.GetOrdinal("accountNumber")) == true
                                             ? ""
                                             : dr.GetString(dr.GetOrdinal("accountNumber"));
                    bank.BankAccountName = dr.IsDBNull(dr.GetOrdinal("bankAccountName")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("bankAccountName"));
                    bank.BankName = dr.IsDBNull(dr.GetOrdinal("BankName")) == true
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("BankName"));
                    bank.CompanyID = dr.IsDBNull(dr.GetOrdinal("CompanyID")) == true
                                         ? ""
                                         : dr.GetString(dr.GetOrdinal("CompanyID"));
                    bank.AccountType =
                        (EyouSoft.Model.CompanyStructure.BankAccountType)
                        Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.BankAccountType),
                                   dr.GetByte(dr.GetOrdinal("TypeId")).ToString());
                    model.BankAccounts.Add(bank);
                    bank = null;
                }

                dr.NextResult(); //开通的公司服务
                while (dr.Read())
                {
                    bool IsEnabled = dr.GetString(dr.GetOrdinal("IsEnabled")) == "1" ? true : false;
                    model.StateMore.CompanyService.SetServiceItem(
                        new EyouSoft.Model.CompanyStructure.CompanyServiceItem(
                            (EyouSoft.Model.CompanyStructure.SysService)dr.GetInt32(dr.GetOrdinal("ServiceId")),
                            IsEnabled));
                }
                //公司资质
                dr.NextResult();
                model.Qualification = new List<Model.CompanyStructure.CompanyQualification>();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("Qualification")))
                        model.Qualification.Add(
                            (Model.CompanyStructure.CompanyQualification)dr.GetByte(dr.GetOrdinal("Qualification")));
                }
            }

            return model;
        }

        /// <summary>
        /// 最新加盟公司
        /// </summary>
        /// <param name="topNum">获取数量</param>
        /// <param name="businessProperties">企业性质</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> GetNewComapnyScenic(int topNum
            , EyouSoft.Model.CompanyStructure.BusinessProperties businessProperties)
        {
            string sql = string.Format("select top({0}) Id,CompanyName from tbl_CompanyInfo where companyType = {1}  and IsEnabled = '1' and IsCheck = '1' and IsDeleted = '0' order by CheckTime desc ", topNum, (int)businessProperties);
            DbCommand comm = this._database.GetSqlStringCommand(sql);
            IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> list = new List<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo>();
            EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    list.Add(item = new EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo()
                    {
                        ID = reader["Id"].ToString(),
                        CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="topNum">top数量</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns></returns>
        public virtual IList<Model.CompanyStructure.CompanyDetailInfo> GetAllCompany(int topNum,
                                                                      Model.CompanyStructure.QueryNewCompany queryModel)
        {
            IList<Model.CompanyStructure.CompanyDetailInfo> list = null;
            var strSql = new StringBuilder();
            strSql.AppendFormat(" select {0} ", topNum > 0 ? string.Format(" top {0} ", topNum) : string.Empty);
            strSql.Append(@" Id,ProvinceId,CityId,CountyId,CompanyName,CompanyBrand,CompanyAddress,License,ContactName,ContactTel,
                ContactFax,ContactMobile,ContactEmail,ContactMQ,ContactQQ,ContactMSN,CommendPeople,Remark,CommendCount,
                IsEnabled,IssueTime,IsCertificateCheck,IsCreditCheck,LoginCount,LastLoginTime,IsCheck,CompanyType,CheckTime,
                WebSite,ShortRemark,[OPCompanyId],[Introduction],[Scale],[Longitude],[Latitude],[PeerContact],[AlipayAccount], 
                [B2BDisplay],[B2BSort],[B2CDisplay],[B2CSort],[ClickNum],[CompanyLev],[InfoFull],[ContractStart],[ContractEnd] ");
            strSql.Append(
                @" ,(SELECT FieldValue FROM tbl_CompanyAttachInfo 
                    WHERE tbl_CompanyAttachInfo.CompanyId = tbl_CompanyInfo.ID 
                    and tbl_CompanyAttachInfo.FieldName = 'CompanyLogo') as CompanyLogo ");
            strSql.Append(" from tbl_CompanyInfo where IsDeleted = '0' and IsEnabled = '1' ");

            #region Where子句拼接

            if (!queryModel.IsShowNoCheck)
                strSql.Append(" and IsCheck = '1' ");
            if (queryModel.PorvinceId > 0)
                strSql.AppendFormat(" and ProvinceId = {0} ", queryModel.PorvinceId);
            if (queryModel.CityId > 0)
                strSql.AppendFormat(" and CityId = {0} ", queryModel.CityId);
            if (queryModel.CountyId > 0)
                strSql.AppendFormat(" and CountyId = {0} ", queryModel.CountyId);
            if (queryModel.CompanyLev.HasValue)
                strSql.AppendFormat(" and CompanyLev = {0} ", (int)queryModel.CompanyLev.Value);
            if (queryModel.B2BDisplay.HasValue)
                strSql.AppendFormat(" and B2BDisplay = {0} ", (int)queryModel.B2BDisplay.Value);
            if (queryModel.B2CDisplay.HasValue)
                strSql.AppendFormat(" and B2CDisplay = {0} ", (int)queryModel.B2CDisplay.Value);
            if (queryModel.CompanyTypes != null && queryModel.CompanyTypes.Any())
            {
                string strTmp = string.Empty;
                foreach (var t in queryModel.CompanyTypes)
                {
                    if (!t.HasValue)
                        continue;

                    strTmp += Convert.ToString((int)t.Value) + ",";
                }
                if (!string.IsNullOrEmpty(strTmp))
                {
                    strTmp = strTmp.TrimEnd(',');
                    strSql.AppendFormat(
                        @" and exists (select 1 from tbl_CompanyTypeList 
                        where tbl_CompanyTypeList.CompanyId = tbl_CompanyInfo.Id 
                        and tbl_CompanyTypeList.[TypeId] in ({0})) ",
                        strTmp);
                }
            }
            if (queryModel.AreaId > 0)
            {
                strSql.AppendFormat(
                    @" and exists (select 1 from tbl_CompanyAreaConfig 
                        where tbl_CompanyAreaConfig.CompanyId = tbl_CompanyInfo.Id 
                        and tbl_CompanyAreaConfig.[AreaId] = {0}) ",
                    queryModel.AreaId);
            }
            if (!string.IsNullOrEmpty(queryModel.KeyWord))
            {
                strSql.AppendFormat(
                    @" and isnull(CompanyName,'') + isnull(CompanyAddress,'') 
                    + isnull(Introduction,'') + isnull(CompanyBrand,'') like '%{0}%' ",
                    queryModel.KeyWord);
            }

            #endregion

            strSql.Append(" order by ");
            switch (queryModel.OrderIndex)
            {
                case 0:
                    strSql.Append(" CheckTime desc  ");
                    break;
                case 1:
                    strSql.Append(" CheckTime asc  ");
                    break;
                case 2:
                    strSql.Append(" B2BSort desc  ");
                    break;
                case 3:
                    strSql.Append(" B2BSort asc  ");
                    break;
                default:
                    strSql.Append(" CheckTime desc  ");
                    break;
            }

            DbCommand dc = _database.GetSqlStringCommand(strSql.ToString());

            Model.CompanyStructure.CompanyDetailInfo model;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _database))
            {
                list = new List<Model.CompanyStructure.CompanyDetailInfo>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.ProvinceId = dr.GetInt32(dr.GetOrdinal("provinceId"));
                    model.CityId = dr.GetInt32(dr.GetOrdinal("cityId"));
                    model.CountyId = dr.GetInt32(dr.GetOrdinal("CountyId"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("companyName")) == true
                                            ? ""
                                            : dr.GetString(dr.GetOrdinal("companyName"));
                    model.CompanyBrand = dr.IsDBNull(dr.GetOrdinal("companyBrand")) == true
                                             ? ""
                                             : dr.GetString(dr.GetOrdinal("companyBrand"));
                    model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("companyAddress")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("companyAddress"));
                    model.License = dr.IsDBNull(dr.GetOrdinal("license")) == true
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("license"));
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("contactName")) == true
                                                        ? ""
                                                        : dr.GetString(dr.GetOrdinal("contactName"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("contactTel")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("contactTel"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("contactFax")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("contactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("contactMobile")) == true
                                                   ? ""
                                                   : dr.GetString(dr.GetOrdinal("contactMobile"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("contactEmail")) == true
                                                  ? ""
                                                  : dr.GetString(dr.GetOrdinal("contactEmail"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("contactMQ")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("contactMQ"));



                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("commendCount"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("contactQQ")) == true
                                               ? ""
                                               : dr.GetString(dr.GetOrdinal("contactQQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("contactMSN")) == true
                                                ? ""
                                                : dr.GetString(dr.GetOrdinal("contactMSN"));
                    model.CommendPeople = dr.IsDBNull(dr.GetOrdinal("CommendPeople")) == true
                                              ? ""
                                              : dr.GetString(dr.GetOrdinal("CommendPeople"));
                    model.Remark = dr.IsDBNull(dr.GetOrdinal("remark")) == true
                                       ? ""
                                       : dr.GetString(dr.GetOrdinal("remark"));
                    model.StateMore.CommendCount = dr.GetInt32(dr.GetOrdinal("CommendCount"));
                    //model.ManageArea = dr.IsDBNull(dr.GetOrdinal("ManageArea")) == true ? "" : dr.GetString(dr.GetOrdinal("ManageArea"));
                    model.StateMore.IsEnabled = dr.GetString(dr.GetOrdinal("isEnabled")) == "1" ? true : false;

                    //判断有无审核通过,并设置加入时间
                    model.StateMore.IsCheck = dr.GetString(dr.GetOrdinal("IsCheck")) == "1" ? true : false;
                    if (model.StateMore.IsCheck)
                    {
                        model.StateMore.JoinTime = dr.IsDBNull(dr.GetOrdinal("CheckTime")) == true
                                                       ? dr.GetDateTime(dr.GetOrdinal("IssueTime"))
                                                       : dr.GetDateTime(dr.GetOrdinal("CheckTime")); //已审核通过
                    }
                    else
                        model.StateMore.JoinTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")); //未审核通过

                    model.StateMore.IsCertificateCheck = dr.GetString(dr.GetOrdinal("isCertificateCheck")) == "1"
                                                             ? true
                                                             : false;
                    model.StateMore.IsCreditCheck = dr.GetString(dr.GetOrdinal("isCreditCheck")) == "1" ? true : false;
                    model.StateMore.LoginCount = dr.GetInt32(dr.GetOrdinal("LoginCount"));
                    model.StateMore.LastLoginTime = dr.IsDBNull(dr.GetOrdinal("LastLoginTime"))
                                                        ? DateTime.MaxValue
                                                        : dr.GetDateTime(dr.GetOrdinal("LastLoginTime"));
                    model.WebSite = dr.IsDBNull(dr.GetOrdinal("WebSite")) == true
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("WebSite"));
                    model.ShortRemark = dr.IsDBNull(dr.GetOrdinal("ShortRemark")) == true
                                            ? ""
                                            : dr.GetString(dr.GetOrdinal("ShortRemark"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OPCompanyId")))
                        model.OpCompanyId = dr.GetInt32(dr.GetOrdinal("OPCompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Introduction")))
                        model.Introduction = dr.GetString(dr.GetOrdinal("Introduction"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Scale")))
                        model.Scale = (Model.CompanyStructure.CompanyScale)dr.GetByte(dr.GetOrdinal("Scale"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Longitude")))
                        model.Longitude = dr.GetDecimal(dr.GetOrdinal("Longitude"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Latitude")))
                        model.Latitude = dr.GetDecimal(dr.GetOrdinal("Latitude"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PeerContact")))
                        model.PeerContact = dr.GetString(dr.GetOrdinal("PeerContact"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AlipayAccount")))
                        model.AlipayAccount = dr.GetString(dr.GetOrdinal("AlipayAccount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay =
                            (Model.CompanyStructure.CompanyB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BSort")))
                        model.B2BSort = dr.GetInt32(dr.GetOrdinal("B2BSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CDisplay")))
                        model.B2CDisplay =
                            (Model.CompanyStructure.CompanyB2CDisplay)dr.GetByte(dr.GetOrdinal("B2CDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CSort")))
                        model.B2CSort = dr.GetInt32(dr.GetOrdinal("B2CSort"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        model.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLev")))
                        model.CompanyLev = (Model.CompanyStructure.CompanyLev)dr.GetByte(dr.GetOrdinal("CompanyLev"));
                    if (!dr.IsDBNull(dr.GetOrdinal("InfoFull")))
                        model.InfoFull = dr.GetDecimal(dr.GetOrdinal("InfoFull"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContractStart")))
                        model.ContractStart = dr.GetDateTime(dr.GetOrdinal("ContractStart"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ContractEnd")))
                        model.ContractEnd = dr.GetDateTime(dr.GetOrdinal("ContractEnd"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLogo")))
                    {
                        model.AttachInfo.CompanyLogo.ImagePath = dr.GetString(dr.GetOrdinal("CompanyLogo"));
                    }

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取已审核的所有公司
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns></returns>
        public virtual IList<Model.CompanyStructure.CompanyAndUserInfo> GetAllCompany(int pageSize, int pageIndex, ref int recordCount,
                                                                       Model.CompanyStructure.QueryNewCompany queryModel)
        {
            IList<Model.CompanyStructure.CompanyAndUserInfo> list = null;
            var cmdQuery = new StringBuilder();
            string tableName = "tbl_CompanyInfo";
            string primaryKey = "Id";
            string orderByString = " CompanyLev desc,IssueTime desc ";
            string fields =
                @" Id,ProvinceId,CityId,CountyId,CompanyName,LoginCount,IssueTime,CompanyLev,ClickNum,B2BDisplay,B2CDisplay
                    ,CommendPeople,
                    (SELECT TypeId FROM tbl_CompanyTypeList WHERE tbl_CompanyTypeList.CompanyId = tbl_CompanyInfo.Id 
                        for xml raw,root('Root')) as CompanyTypeList,
                    (SELECT top 1 ContactName,ContactMobile,ContactTel,ContactFax,QQ FROM tbl_CompanyUser 
                        WHERE tbl_CompanyUser.CompanyId = tbl_CompanyInfo.Id 
                        AND tbl_CompanyUser.IsAdmin = '1' and tbl_CompanyUser.IsDeleted = '0' 
                        and tbl_CompanyUser.IsEnable = '1' order by IssueTime asc for xml raw,root('Root')) as UserInfo,
                    (SELECT [ServiceId],[IsEnabled] FROM [tbl_CompanyPayService] 
                        WHERE tbl_CompanyPayService.CompanyId = tbl_CompanyInfo.Id for xml raw,root('Root')) as CompanyServices
                ";

            #region Where子句拼接

            cmdQuery.Append(" IsDeleted = '0' ");
            if (!queryModel.IsShowNoCheck)
                cmdQuery.Append(" and IsCheck = '1' ");
            if (queryModel.PorvinceId > 0)
                cmdQuery.AppendFormat(" and ProvinceId = {0} ", queryModel.PorvinceId);
            if (queryModel.CityId > 0)
                cmdQuery.AppendFormat(" and CityId = {0} ", queryModel.CityId);
            if (queryModel.CountyId > 0)
                cmdQuery.AppendFormat(" and CountyId = {0} ", queryModel.CountyId);
            if (queryModel.CompanyLev.HasValue)
                cmdQuery.AppendFormat(" and CompanyLev = {0} ", (int)queryModel.CompanyLev.Value);
            if (queryModel.B2BDisplay.HasValue)
                cmdQuery.AppendFormat(" and B2BDisplay = {0} ", (int)queryModel.B2BDisplay.Value);
            if (queryModel.B2CDisplay.HasValue)
                cmdQuery.AppendFormat(" and B2CDisplay = {0} ", (int)queryModel.B2CDisplay.Value);
            if (queryModel.CompanyTypes != null && queryModel.CompanyTypes.Any())
            {
                string strTmp = string.Empty;
                foreach (var t in queryModel.CompanyTypes)
                {
                    if (!t.HasValue)
                        continue;

                    strTmp += Convert.ToString((int)t.Value) + ",";
                }
                if (!string.IsNullOrEmpty(strTmp))
                {
                    strTmp = strTmp.TrimEnd(',');
                    cmdQuery.AppendFormat(
                        @" and exists (select 1 from tbl_CompanyTypeList 
                        where tbl_CompanyTypeList.CompanyId = tbl_CompanyInfo.Id 
                        and tbl_CompanyTypeList.[TypeId] in ({0})) ",
                        strTmp);
                }
            }
            if (queryModel.AreaId > 0)
            {
                cmdQuery.AppendFormat(
                    @" and exists (select 1 from tbl_CompanyAreaConfig 
                        where tbl_CompanyAreaConfig.CompanyId = tbl_CompanyInfo.Id 
                        and tbl_CompanyAreaConfig.[AreaId] = {0}) ",
                    queryModel.AreaId);
            }
            if (!string.IsNullOrEmpty(queryModel.KeyWord))
            {
                cmdQuery.AppendFormat(
                    @" and isnull(CompanyName,'') + isnull(CompanyAddress,'') 
                    + isnull(Introduction,'') + isnull(CompanyBrand,'') like '%{0}%' ",
                    queryModel.KeyWord);
            }

            #endregion

            list = new List<Model.CompanyStructure.CompanyAndUserInfo>();
            XElement xRoot;
            IEnumerable<XElement> xRows;
            using (IDataReader dr = DbHelper.ExecuteReader(_database, pageSize, pageIndex, ref recordCount, tableName,
                primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    var tmp = new Model.CompanyStructure.CompanyAndUserInfo
                                  {
                                      Company = new Model.CompanyStructure.CompanyDetailInfo(),
                                      User = new Model.CompanyStructure.CompanyUser()
                                  };

                    #region 公司信息

                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        tmp.Company.ID = dr.GetString(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProvinceId")))
                        tmp.Company.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CityId")))
                        tmp.Company.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CountyId")))
                        tmp.Company.CountyId = dr.GetInt32(dr.GetOrdinal("CountyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyName")))
                        tmp.Company.CompanyName = dr.GetString(dr.GetOrdinal("CompanyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LoginCount")))
                        tmp.Company.StateMore.LoginCount = dr.GetInt32(dr.GetOrdinal("LoginCount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        tmp.Company.StateMore.JoinTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyLev")))
                        tmp.Company.CompanyLev = (Model.CompanyStructure.CompanyLev)dr.GetByte(dr.GetOrdinal("CompanyLev"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        tmp.Company.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        tmp.Company.B2BDisplay = (Model.CompanyStructure.CompanyB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2CDisplay")))
                        tmp.Company.B2CDisplay = (Model.CompanyStructure.CompanyB2CDisplay)dr.GetByte(dr.GetOrdinal("B2CDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CommendPeople")))
                        tmp.Company.CommendPeople = dr.GetString(dr.GetOrdinal("CommendPeople"));

                    #endregion

                    #region 用户信息赋值

                    if (!dr.IsDBNull(dr.GetOrdinal("UserInfo")))
                    {
                        xRoot = XElement.Parse(dr.GetString(dr.GetOrdinal("UserInfo")));
                        xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                tmp.User.ContactInfo = new Model.CompanyStructure.ContactPersonInfo
                                                           {
                                                               ContactName = Common.Utility.GetXAttributeValue(t, "ContactName"),
                                                               Mobile = Common.Utility.GetXAttributeValue(t, "ContactMobile"),
                                                               Tel = Common.Utility.GetXAttributeValue(t, "ContactTel"),
                                                               Fax = Common.Utility.GetXAttributeValue(t, "ContactFax"),
                                                               QQ = Common.Utility.GetXAttributeValue(t, "QQ")
                                                           };
                                break;
                            }
                        }
                    }


                    #endregion

                    #region 公司身份

                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyTypeList")))
                    {
                        xRoot = XElement.Parse(dr.GetString(dr.GetOrdinal("CompanyTypeList")));
                        xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                tmp.Company.CompanyRole.SetRole(
                                    (Model.CompanyStructure.CompanyType)
                                    Common.Utility.GetInt(Common.Utility.GetXAttributeValue(t, "TypeId")));
                            }
                        }
                    }

                    #endregion

                    #region 公司高级服务（高级网店、企业MQ）

                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyServices")))
                    {
                        xRoot = XElement.Parse(dr.GetString(dr.GetOrdinal("CompanyServices")));
                        xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                tmp.Company.StateMore.CompanyService.SetServiceItem(
                                    new Model.CompanyStructure.CompanyServiceItem
                                        {
                                            IsEnabled =
                                                (Common.Utility.GetXAttributeValue(t, "IsEnabled") == "1" ||
                                                 Common.Utility.GetXAttributeValue(t, "IsEnabled").ToLower() == "true")
                                                    ? true
                                                    : false,
                                            Service =
                                                (Model.CompanyStructure.SysService)
                                                Common.Utility.GetInt(Common.Utility.GetXAttributeValue(t, "ServiceId"))
                                        });

                            }
                        }
                    }

                    #endregion

                    list.Add(tmp);
                }
            }

            return list;
        }

        /// <summary>
        /// 设置公司网店的点击量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="number">累计的数量</param>
        /// <returns></returns>
        public virtual bool SetShopClickNum(string companyId, int number)
        {
            if (string.IsNullOrEmpty(companyId) || number == 0)
                return false;

            DbCommand dc =
                _database.GetSqlStringCommand(
                    " update tbl_CompanyInfo set ClickNum = ClickNum + @num where Id = @CompanyId; ");
            _database.AddInParameter(dc, "num", DbType.Int32, number);
            _database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, _database) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 获取订单短信接收者信息业务实体
        /// </summary>
        /// <param name="dstCompanyId">接收者公司编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.NewTourStructure.MOrderSmsDstInfo GetOrderSmsDstInfo(string dstCompanyId)
        {
            EyouSoft.Model.NewTourStructure.MOrderSmsDstInfo info = null;
            DbCommand cmd = _database.GetSqlStringCommand(SQL_SELECT_GetOrderSmsDstInfo);
            _database.AddInParameter(cmd, "CID", DbType.AnsiStringFixedLength, dstCompanyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _database))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.NewTourStructure.MOrderSmsDstInfo();
                    info.DstComanyId = rdr.GetString(0);
                    info.DstCompanyName = rdr[1].ToString();
                    info.DstCompanyType = (EyouSoft.Model.CompanyStructure.CompanyLev)rdr.GetByte(2);
                }

                if (info != null && rdr.NextResult())
                {
                    info.DstPhones = new List<string>();
                    while (rdr.Read())
                    {
                        info.DstPhones.Add(rdr.GetString(0));
                    }
                }
            }

            return info;
        }
    }
}
