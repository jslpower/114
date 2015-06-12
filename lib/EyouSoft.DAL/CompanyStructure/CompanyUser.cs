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
    /// 描述：公司用户信息数据层
    /// </summary>
    public class CompanyUser : DALBase, ICompanyUser
    {
        #region SQL变量定义

        private const string SQL_SELECT_Model_Fields =
            @" [Id],[OPUserId],[CompanyId],[ProvinceId],[CityId],[UserName],[Password],[EncryptPassword],[MD5Password],
                [ContactName],[ContactSex],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[QQ],[MQ],[MSN],
                [LastLoginIP],[LastLoginTime],[RoleID],[IsEnable],[IsAdmin],[DepartId],[DepartName],[Job],[IssueTime],
                (select im_displayname from im_member where im_member.bs_uid = [tbl_CompanyUser].ID) as im_displayname,
                (SELECT COUNT(*) FROM tbl_LogUserLogin AS A WHERE A.OperatorId=tbl_CompanyUser.Id) AS LoginTimes ";
        private const string SQL_CompanyUser_SELECT_ExistsEmail = "SELECT COUNT(*) FROM tbl_CompanyUser WHERE ContactEmail=@Email AND IsDeleted='0' ";
        private const string SQL_im_member_SELECT_Exists = "SELECT COUNT(*) FROM [im_member] WHERE im_username=@UserName";
        private const string SQL_CompanyUser_SELECT_Exists = "SELECT COUNT(*) FROM [tbl_CompanyUser] WHERE UserName=@UserName AND IsDeleted='0'";
        private const string SQL_Insert_CompanyUser = "INSERT INTO tbl_CompanyUser(Id,CompanyId,ProvinceId,CityId,UserName,Password,EncryptPassword,MD5Password,ContactName,ContactSex,ContactTel,ContactFax,ContactMobile,ContactEmail,QQ,MQ,MSN,RoleID,DepartId,DepartName) VALUES(@Id,@CompanyId,@ProvinceId,@CityId,@UserName,@Password,@EncryptPassword,@MD5Password,@ContactName,@ContactSex,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@QQ,@MQ,@MSN,@RoleID,@DepartId,@DepartName)";
        private const string SQL_UPDATE_CompanyUser = "UPDATE tbl_CompanyUser Password=@Password,EncryptPassword=@EncryptPassword,MD5Password=@MD5Password,ContactName=@ContactName,ContactSex=@ContactSex,ContactTel=@ContactTel,ContactFax=@ContactFax,ContactMobile=@ContactMobile,ContactEmail=@ContactEmail,QQ=@QQ,MQ=@MQ,MSN=@MSN,RoleID=@RoleID,DepartId=@DepartId,DepartName=@DepartName WHERE ID=@ID";
        private const string SQL_SetEnable_CompanyUser = "UPDATE tbl_CompanyUser SET isEnable=@isEnable where ID=@ID";
        /// <summary>
        /// 修改用户表和MQ表的密码
        /// </summary>
        private const string SQL_UPDATE_PassWord_CompanyUser = "UPDATE tbl_CompanyUser SET Password=@Password,EncryptPassword=@EncryptPassword,MD5Password=@MD5Password WHERE ID=@ID;UPDATE im_member SET im_password=@MD5Password WHERE bs_uid=@ID;";
        /// <summary>
        /// 获得用户信息实体
        /// </summary>
        private const string SQL_CompanyUser_SELECT_Model = "SELECT " + SQL_SELECT_Model_Fields + " FROM [tbl_CompanyUser] WHERE ID=@ID AND IsDeleted='0';";
        /// <summary>
        /// 获得用户信息实体
        /// </summary>
        private const string SQL_CompanyUser_SELECT_Model_ByMQID = "SELECT " + SQL_SELECT_Model_Fields + " FROM [tbl_CompanyUser] WHERE [MQ]=@MQ AND IsDeleted='0';";
        /// <summary>
        /// 获得用户信息实体
        /// </summary>
        private const string SQL_CompanyUser_SELECT_Model_ByUserName = "SELECT " + SQL_SELECT_Model_Fields + " FROM [tbl_CompanyUser] WHERE [UserName]=@UserName AND IsDeleted='0';";
        /// <summary>
        /// 获得用户信息实体
        /// </summary>
        private const string SQL_CompanyUser_SELECT_Model_ByOPUserId = "SELECT " + SQL_SELECT_Model_Fields + " FROM [tbl_CompanyUser] WHERE [OPUserId]=@OPUserId AND IsDeleted='0';";
        /// <summary>
        /// 获得管理员用户信息实体
        /// </summary>
        private const string SQL_CompanyUser_SELECT_AdminModel = "SELECT " + SQL_SELECT_Model_Fields + " FROM [tbl_CompanyUser] WHERE CompanyId=@CompanyId AND IsDeleted='0' AND IsAdmin='1';";

        /// <summary>
        /// 修改个人设置
        /// </summary>
        private const string SQL_CompanyUser_UpdatePersonal =
            @" UPDATE [tbl_CompanyUser] SET [ContactName]=@ContactName,[ContactSex]=@ContactSex,[ContactTel]=@ContactTel,
                [ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[QQ]=@ContactQQ,
                [MSN]=@ContactMSN,[Job] = @Job 
                WHERE Id=@UserId AND CompanyID=@CompanyID; ";

        /// <summary>
        /// 获得所有子帐号基本信息列表
        /// </summary>
        private const string SQL_CompanyUser_SELECT_List = "SELECT ID,IsAdmin,UserName,ContactName,ContactTel,ContactFax,ContactMobile,ContactEmail,QQ,MQ,MSN,Job FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsDeleted='0' ";
        /// <summary>
        /// 获得用户线路区域信息
        /// </summary>
        private const string SQL_CompanyUserAreaControl_SELECT = "SELECT [AreaId] FROM [tbl_CompanyUserAreaControl] WHERE UserId=@ID;";
        /// <summary>
        /// 获得子帐号数量
        /// </summary>
        private const string SQL_CompanyUser_SELECT_Count = "SELECT COUNT(*) FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsAdmin='0' AND IsDeleted='0' ";
        const string SQL_SELECT_IsExistsUserId = "SELECT COUNT(*) FROM [tbl_CompanyUser] WHERE [CompanyId]=@CID AND [Id]=@UID AND [IsDeleted]='0'";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyUser()
        {
            this._database = base.UserStore;
        }

        #region 成员方法
        /// <summary>
        /// 判断E-MAIL是否已存在
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="userId">当前修改Email的用户ID</param>
        /// <returns></returns>
        public virtual bool IsExistsEmail(string email, string userId)
        {
            string SQL = SQL_CompanyUser_SELECT_ExistsEmail;
            if (!string.IsNullOrEmpty(userId))
                SQL += " AND Id!=@UserId ";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "Email", DbType.String, email);
            if (!string.IsNullOrEmpty(userId))
                this._database.AddInParameter(dc, "UserId", DbType.String, userId);
            return DbHelper.Exists(dc, this._database);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public virtual bool IsExists(string userName)
        {
            bool isExists = false;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_im_member_SELECT_Exists);
            this._database.AddInParameter(dc, "UserName", DbType.String, userName);
            isExists = DbHelper.Exists(dc, this._database);

            //if (!isExists)
            //{
            //    dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Exists);
            //    this._database.AddInParameter(dc, "UserName", DbType.String, userName);
            //    isExists = DbHelper.Exists(dc, this._database);
            //}

            return isExists;
        }
        /// <summary>
        /// 添加帐号[添加帐号后自动生成MQ号码]
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ResultStructure.ResultInfo Add(ref EyouSoft.Model.CompanyStructure.CompanyUser model)
        {
            EyouSoft.Model.ResultStructure.ResultInfo result = EyouSoft.Model.ResultStructure.ResultInfo.Error;
            model.ID = Guid.NewGuid().ToString();
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyUserChild_Insert");
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyID);
            //this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            //this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "UserName", DbType.String, model.UserName);
            this._database.AddInParameter(dc, "Password", DbType.String, model.PassWordInfo.NoEncryptPassword);
            this._database.AddInParameter(dc, "EncryptPassword", DbType.String, model.PassWordInfo.SHAPassword);
            this._database.AddInParameter(dc, "MD5Password", DbType.String, model.PassWordInfo.MD5Password);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactSex", DbType.String, Convert.ToInt32(model.ContactInfo.ContactSex).ToString());
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "RoleID", DbType.AnsiStringFixedLength, model.RoleID);
            this._database.AddInParameter(dc, "DepartId", DbType.AnsiStringFixedLength, model.DepartId == null ? "" : model.DepartId);
            this._database.AddInParameter(dc, "DepartName", DbType.String, model.DepartName);

            #region 2011-11-12 线路改版添加字段

            _database.AddInParameter(dc, "Job", DbType.String, model.Job);
            _database.AddInParameter(dc, "MqNickName", DbType.String, model.MqNickName);

            #endregion

            //用户线路区域         
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

            //设置MQ返回值
            this._database.AddOutParameter(dc, "ReturnMQID", DbType.String, 250);
            //设置省份id返回值
            this._database.AddOutParameter(dc, "ReturnProvinceId", DbType.Int32, 4);
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
                        result = EyouSoft.Model.ResultStructure.ResultInfo.Error;
                        break;
                    case "1":  //操作成功
                        result = EyouSoft.Model.ResultStructure.ResultInfo.Succeed;
                        object MQIDObj = this._database.GetParameterValue(dc, "ReturnMQID");
                        if (MQIDObj != null)
                            model.ContactInfo.MQ = MQIDObj.ToString();
                        object ProvinceObj = this._database.GetParameterValue(dc, "ReturnProvinceId");
                        if (ProvinceObj != null)
                            model.ProvinceId = Convert.ToInt32(ProvinceObj);
                        #region 此处执行社区用户插入
                        DbCommand DiscuzDC = base.DiscuzStore.GetStoredProcCommand("Club_createuser");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "username", DbType.String, model.UserName);
                        base.DiscuzStore.AddInParameter(DiscuzDC, "nickname", DbType.String, "");
                        base.DiscuzStore.AddInParameter(DiscuzDC, "password", DbType.String, model.PassWordInfo.MD5Password);
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
                    case "2":  //数据已存在
                        result = EyouSoft.Model.ResultStructure.ResultInfo.Exists;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 修改帐号信息
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.CompanyUser model)
        {
            bool isTrue = false;
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyUser_Update");
            //this._database.AddInParameter(dc, "IsPersonalSettings", DbType.String, false);
            this._database.AddInParameter(dc, "Password", DbType.String, model.PassWordInfo.NoEncryptPassword);
            this._database.AddInParameter(dc, "EncryptPassword", DbType.String, model.PassWordInfo.SHAPassword);
            this._database.AddInParameter(dc, "MD5Password", DbType.String, model.PassWordInfo.MD5Password);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactSex", DbType.String, Convert.ToInt32(model.ContactInfo.ContactSex).ToString());
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "RoleID", DbType.AnsiStringFixedLength, model.RoleID);
            this._database.AddInParameter(dc, "DepartId", DbType.AnsiStringFixedLength, model.DepartId);
            this._database.AddInParameter(dc, "DepartName", DbType.String, model.DepartName);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);

            #region 2011-11-12 线路改版添加字段

            _database.AddInParameter(dc, "Job", DbType.String, model.Job);
            _database.AddInParameter(dc, "MqNickName", DbType.String, model.MqNickName);

            #endregion

            //用户线路区域         
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
                        #region 同步修改社区用户信息
                        EyouSoft.Model.CompanyStructure.CompanyUser CurrUserInfo = GetModel(model.ID);
                        if (CurrUserInfo != null)
                        {
                            string Sql_Discuz_UpdateUser = "update Club_users set password=@Club_password,gender=@gender,email=@email where username=@username and password=@pwd;";
                            Sql_Discuz_UpdateUser += "update Club_userfields set qq=@qq,msn=@msn,mobile=@mobile,phone=@phone where uid=(select top 1 uid from Club_users where username=@username and password=@pwd);";
                            DbCommand DiscuzDC = base.DiscuzStore.GetSqlStringCommand(Sql_Discuz_UpdateUser);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "Club_password", DbType.String, model.PassWordInfo.MD5Password);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "gender", DbType.Int16, model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.未知 ? 0 : (model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.男 ? 1 : 2));
                            base.DiscuzStore.AddInParameter(DiscuzDC, "email", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Email) ? "" : model.ContactInfo.Email);
                            if (!string.IsNullOrEmpty(model.ContactInfo.QQ) && model.ContactInfo.QQ.Length > 100)
                                model.ContactInfo.QQ = model.ContactInfo.QQ.Substring(0, 100);
                            if (!string.IsNullOrEmpty(model.ContactInfo.MSN) && model.ContactInfo.MSN.Length > 100)
                                model.ContactInfo.MSN = model.ContactInfo.MSN.Substring(0, 100);
                            if (!string.IsNullOrEmpty(model.ContactInfo.Mobile) && model.ContactInfo.Mobile.Length > 100)
                                model.ContactInfo.Mobile = model.ContactInfo.Mobile.Substring(0, 100);
                            if (!string.IsNullOrEmpty(model.ContactInfo.Tel) && model.ContactInfo.Tel.Length > 100)
                                model.ContactInfo.Tel = model.ContactInfo.Tel.Substring(0, 100);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "qq", DbType.String, string.IsNullOrEmpty(model.ContactInfo.QQ) ? "" : model.ContactInfo.QQ);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "msn", DbType.String, string.IsNullOrEmpty(model.ContactInfo.MSN) ? "" : model.ContactInfo.MSN);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "mobile", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Mobile) ? "" : model.ContactInfo.Mobile);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "phone", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Tel) ? "" : model.ContactInfo.Tel);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "username", DbType.String, CurrUserInfo.UserName);
                            base.DiscuzStore.AddInParameter(DiscuzDC, "pwd", DbType.String, CurrUserInfo.PassWordInfo.MD5Password);

                            DbHelper.ExecuteSqlTrans(DiscuzDC, base.DiscuzStore);
                        }
                        CurrUserInfo = null;
                        #endregion
                        break;
                }
            }

            return isTrue;
        }

        /// <summary>
        /// 修改个人设置信息(不修改密码,角色,部门)
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        public virtual bool UpdatePersonal(EyouSoft.Model.CompanyStructure.CompanyUserBase model)
        {
            bool isTrue = false;
            if (string.IsNullOrEmpty(model.MqNickName))
                model.MqNickName = model.ContactInfo.ContactName;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_UpdatePersonal +
                " UPDATE im_member SET [im_displayname]=@MqNickName WHERE bs_uid=@UserId; ");
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactSex", DbType.String, Convert.ToInt32(model.ContactInfo.ContactSex).ToString());
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            _database.AddInParameter(dc, "Job", DbType.String, model.Job);
            _database.AddInParameter(dc, "MqNickName", DbType.String, model.MqNickName);

            isTrue = DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;

            #region 同步修改社区用户信息
            EyouSoft.Model.CompanyStructure.CompanyUser CurrUserInfo = GetModel(model.ID);
            if (CurrUserInfo != null)
            {
                string Sql_Discuz_UpdateUser = "update Club_users set gender=@gender,email=@email where username=@username and password=@pwd;";
                Sql_Discuz_UpdateUser += "update Club_userfields set qq=@qq,msn=@msn,mobile=@mobile,phone=@phone where uid=(select top 1 uid from Club_users where username=@username and password=@pwd);";
                DbCommand DiscuzDC = base.DiscuzStore.GetSqlStringCommand(Sql_Discuz_UpdateUser);
                base.DiscuzStore.AddInParameter(DiscuzDC, "gender", DbType.Int16, model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.未知 ? 0 : (model.ContactInfo.ContactSex == EyouSoft.Model.CompanyStructure.Sex.男 ? 1 : 2));
                base.DiscuzStore.AddInParameter(DiscuzDC, "email", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Email) ? "" : model.ContactInfo.Email);
                base.DiscuzStore.AddInParameter(DiscuzDC, "qq", DbType.String, string.IsNullOrEmpty(model.ContactInfo.QQ) ? "" : model.ContactInfo.QQ);
                base.DiscuzStore.AddInParameter(DiscuzDC, "msn", DbType.String, string.IsNullOrEmpty(model.ContactInfo.MSN) ? "" : model.ContactInfo.MSN);
                base.DiscuzStore.AddInParameter(DiscuzDC, "mobile", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Mobile) ? "" : model.ContactInfo.Mobile);
                base.DiscuzStore.AddInParameter(DiscuzDC, "phone", DbType.String, string.IsNullOrEmpty(model.ContactInfo.Tel) ? "" : model.ContactInfo.Tel);
                base.DiscuzStore.AddInParameter(DiscuzDC, "username", DbType.String, CurrUserInfo.UserName);
                base.DiscuzStore.AddInParameter(DiscuzDC, "pwd", DbType.String, CurrUserInfo.PassWordInfo.MD5Password);
                DbHelper.ExecuteSqlTrans(DiscuzDC, base.DiscuzStore);
            }
            CurrUserInfo = null;
            #endregion

            return isTrue;
        }

        /// <summary>
        /// 真实删除用户表和MQ表信息
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        public virtual bool Delete(params string[] userIdList)
        {
            StringBuilder DiscuzDelUserSql = new StringBuilder();//用于存储社区用户删除的SQL
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyUser_Delete");
            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in userIdList)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    #region 构造同步删除社区用户的SQL
                    EyouSoft.Model.CompanyStructure.CompanyUser CurrUserInfo = GetModel(id);
                    if (CurrUserInfo != null)
                    {
                        DiscuzDelUserSql.AppendFormat("DELETE FROM Club_userfields where uid=(select uid from Club_users where username='{0}' and password='{1}');", CurrUserInfo.UserName, CurrUserInfo.PassWordInfo.MD5Password);
                        DiscuzDelUserSql.AppendFormat("DELETE FROM Club_users where username='{0}' and password='{1}';", CurrUserInfo.UserName, CurrUserInfo.PassWordInfo.MD5Password);
                    }
                    CurrUserInfo = null;
                    #endregion
                    XMLRoot.AppendFormat("<User UserId='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
            {
                #region 同步删除社区用户信息
                if (DiscuzDelUserSql.Length > 0)
                {
                    DiscuzDelUserSql.AppendFormat("UPDATE Club_statistics SET [totalusers]=[totalusers]-{0} ;", userIdList.Length);
                    DbCommand DiscuzDC = base.DiscuzStore.GetSqlStringCommand(DiscuzDelUserSql.ToString());
                    DbHelper.ExecuteSqlTrans(DiscuzDC, base.DiscuzStore);
                }
                #endregion
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 移除用户(即虚拟删除用户)
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        public virtual bool Remove(params string[] userIdList)
        {
            StringBuilder DiscuzDelUserSql = new StringBuilder();//用于存储社区用户删除的SQL
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyUser_Remove");
            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in userIdList)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    #region 构造同步删除社区用户的SQL
                    EyouSoft.Model.CompanyStructure.CompanyUser CurrUserInfo = GetModel(id);
                    if (CurrUserInfo != null)
                    {
                        DiscuzDelUserSql.AppendFormat("DELETE FROM Club_userfields where uid=(select uid from Club_users where username='{0}' and password='{1}');", CurrUserInfo.UserName, CurrUserInfo.PassWordInfo.MD5Password);
                        DiscuzDelUserSql.AppendFormat("DELETE FROM Club_users where username='{0}' and password='{1}';", CurrUserInfo.UserName, CurrUserInfo.PassWordInfo.MD5Password);
                    }
                    CurrUserInfo = null;
                    #endregion
                    XMLRoot.AppendFormat("<User UserId='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
            {
                #region 同步删除社区用户信息
                if (DiscuzDelUserSql.Length > 0)
                {
                    DiscuzDelUserSql.AppendFormat("UPDATE Club_statistics SET [totalusers]=[totalusers]-{0} ;", userIdList.Length);
                    DbCommand DiscuzDC = base.DiscuzStore.GetSqlStringCommand(DiscuzDelUserSql.ToString());
                    DbHelper.ExecuteSqlTrans(DiscuzDC, base.DiscuzStore);
                }
                #endregion
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得子帐号数量[不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual int GetSubUserCount(string companyId)
        {
            int count = 0;
            if (string.IsNullOrEmpty(companyId))
                return count;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Count);
            this._database.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (rdr.Read())
                {
                    count = rdr.GetInt32(0);
                }
            }
            return count;
        }

        /// <summary>
        /// 获取指定公司下的所有子帐号用户详细信息列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="Params">查询条件</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyUser> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser Params, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyUser> list = new List<EyouSoft.Model.CompanyStructure.CompanyUser>();

            string tableName = "tbl_CompanyUser";
            string fields = "ID,UserName,ContactName,ContactTel,ContactFax,ContactMobile,ContactEmail,QQ,MQ,MSN,IsEnable,IsAdmin";
            string primaryKey = "ID";
            string orderByString = "IssueTime";
            StringBuilder strWhere = new StringBuilder(string.Format(" Companyid='{0}' AND IsDeleted='0'", companyId));

            #region 查询拼接
            if (Params != null)
            {
                if (!string.IsNullOrEmpty(Params.ContactName))
                    strWhere.AppendFormat(" AND ContactName like '%{0}%' ", Params.ContactName);

                if (!string.IsNullOrEmpty(Params.UserName))
                    strWhere.AppendFormat(" AND UserName like '%{0}%' ", Params.UserName);

                if (!Params.IsShowAdmin)
                {
                    strWhere.Append(" AND IsAdmin ='0' ");
                }
            }
            else
            {
                strWhere.AppendFormat(" AND IsAdmin ='0' ");
            }

            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                //list = this.InitUserList(dr, false);
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyUser model = new EyouSoft.Model.CompanyStructure.CompanyUser();
                    model.CompanyID = companyId;
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true ? "" : dr.GetString(dr.GetOrdinal("UserName"));
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("ContactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("contactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("contactMobile"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("ContactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("QQ")) == true ? "" : dr.GetString(dr.GetOrdinal("QQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("MSN")) == true ? "" : dr.GetString(dr.GetOrdinal("MSN"));
                    model.IsEnable = dr.GetString(dr.GetOrdinal("IsEnable")) == "1" ? true : false;
                    model.IsAdmin = dr.GetString(dr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定公司下的所有子帐号用户基本信息列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="Params">查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser Params)
        {
            //ID,UserName,ContactName,ContactTel,ContactFax,ContactMobile,ContactEmail,QQ,MQ,MSN
            IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> list = new List<EyouSoft.Model.CompanyStructure.CompanyUserBase>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(SQL_CompanyUser_SELECT_List);

            #region 查询拼接
            if (Params != null)
            {
                if (!string.IsNullOrEmpty(Params.ContactName))
                    strSql.AppendFormat(" AND ContactName like '%{0}%' ", Params.ContactName);

                if (!string.IsNullOrEmpty(Params.UserName))
                    strSql.AppendFormat(" AND UserName like '%{0}%' ", Params.UserName);

                if (!Params.IsShowAdmin)
                {
                    strSql.Append(" AND IsAdmin ='0' ");
                }
            }
            else
            {
                strSql.AppendFormat(" AND IsAdmin ='0' ");
            }

            strSql.Append(" ORDER BY IssueTime ");
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "CompanyId", DbType.String, companyId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                //list = (IList<EyouSoft.Model.CompanyStructure.CompanyUserBase>)this.InitUserList(dr, true);
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyUserBase model = new EyouSoft.Model.CompanyStructure.CompanyUserBase();
                    model.CompanyID = companyId;
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                    model.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true ? "" : dr.GetString(dr.GetOrdinal("UserName"));
                    model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("ContactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("contactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("contactMobile"));
                    model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("ContactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("QQ")) == true ? "" : dr.GetString(dr.GetOrdinal("QQ"));
                    model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("MSN")) == true ? "" : dr.GetString(dr.GetOrdinal("MSN"));
                    model.ContactInfo.MQ = dr.IsDBNull(dr.GetOrdinal("MQ")) == true ? "" : dr.GetString(dr.GetOrdinal("MQ"));
                    model.IsAdmin = dr.GetString(dr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    model.Job = dr.IsDBNull(dr.GetOrdinal("Job")) ? string.Empty : dr.GetString(dr.GetOrdinal("Job"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 状态设置
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isEnable">是否停用,true:停用,false:启用</param>
        /// <returns></returns>
        public virtual bool SetEnable(string id, bool isEnable)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SetEnable_CompanyUser);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            this._database.AddInParameter(dc, "isEnable", DbType.AnsiStringFixedLength, isEnable ? "1" : "0");
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 修改用户表和MQ表的密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="password">密码实体类</param>
        /// <returns></returns>
        public virtual bool UpdatePassWord(string id, EyouSoft.Model.CompanyStructure.PassWord password)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_UPDATE_PassWord_CompanyUser);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            this._database.AddInParameter(dc, "Password", DbType.String, password.NoEncryptPassword);
            this._database.AddInParameter(dc, "EncryptPassword", DbType.String, password.SHAPassword);
            this._database.AddInParameter(dc, "MD5Password", DbType.String, password.MD5Password);
            bool isTrue = true;
            #region 同步修改社区用户密码
            EyouSoft.Model.CompanyStructure.CompanyUser CurrUserInfo = GetModel(id);
            if (CurrUserInfo != null)
            {
                string Sql_Discuz_UpdateUser = "update Club_users set password=@Club_password where username=@username and password=@pwd";
                DbCommand DiscuzDC = base.DiscuzStore.GetSqlStringCommand(Sql_Discuz_UpdateUser);
                base.DiscuzStore.AddInParameter(DiscuzDC, "Club_password", DbType.String, password.MD5Password);
                base.DiscuzStore.AddInParameter(DiscuzDC, "username", DbType.String, CurrUserInfo.UserName);
                base.DiscuzStore.AddInParameter(DiscuzDC, "pwd", DbType.String, CurrUserInfo.PassWordInfo.MD5Password);
                DbHelper.ExecuteSql(DiscuzDC, base.DiscuzStore);
            }
            CurrUserInfo = null;
            #endregion
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
            {
                isTrue = true;
            }
            else
            {
                isTrue = false;
            }
            return isTrue;
        }

        /// <summary>
        /// 获得实体信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyUser GetModel(string id)
        {
            //用户线路区域信息,改成在业务逻辑层,调用用户线路区域的业务逻辑层方法获得
            //DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Model + SQL_CompanyUserAreaControl_SELECT);
            if (string.IsNullOrEmpty(id))
                return null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Model);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            return this.InitUserModel(dc);
        }
        /// <summary>
        /// 获得实体信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyUser GetModelByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Model_ByUserName);
            this._database.AddInParameter(dc, "UserName", DbType.AnsiString, userName);
            return this.InitUserModel(dc);
        }
        /// <summary>
        /// 根据MQID获取用户信息
        /// </summary>
        /// <param name="MQId">用户对应MQID</param>
        /// <returns></returns>
        public virtual Model.CompanyStructure.CompanyUser GetModel(int MQId)
        {
            if (MQId <= 0)
                return null;

            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Model_ByMQID);
            this._database.AddInParameter(dc, "MQ", DbType.AnsiString, MQId);
            return this.InitUserModel(dc);
        }

        /// <summary>
        /// 根据oPUserID获取用户信息
        /// </summary>
        /// <param name="oPUserID">oPUserID</param>
        /// <returns></returns>
        public virtual Model.CompanyStructure.CompanyUser GetModelByOPUserID(int oPUserID)
        {
            if (oPUserID <= 0)
                return null;

            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_Model_ByOPUserId);
            this._database.AddInParameter(dc, "OPUserId", DbType.Int32, oPUserID);
            return this.InitUserModel(dc);
        }

        /// <summary>
        /// 获得管理员实体信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyUser GetAdminModel(string companyId)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUser_SELECT_AdminModel);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            return this.InitUserModel(dc);
        }

        /// <summary>
        /// 获取用户列表信息集合
        /// </summary>
        /// <param name="pageSize">每页记录灵敏</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.MLBYNUserInfo> GetUsers(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CompanyStructure.MLBYNUserSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.CompanyStructure.MLBYNUserInfo> items = new List<EyouSoft.Model.CompanyStructure.MLBYNUserInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_CompanyUserInfo";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = " [Id],[CompanyId],[UserName],[ContactName],[ContactSex],[ContactMobile],[QQ],[MQ],[IssueTime],[LastLoginTime],[IsDeleted],[CityId],[ProvinceId],[CompanyName],[LoginTimes] ";

            cmdQuery.Append(" IsDeleted='0' ");

            #region 拼接查询条件
            if (searchInfo != null)
            {
                if (!string.IsNullOrEmpty(searchInfo.CompanyId))
                {
                    cmdQuery.AppendFormat(" AND CompanyId = '{0}' ", searchInfo.CompanyId);
                }
                if (searchInfo.CityId.HasValue)
                {
                    cmdQuery.AppendFormat(" AND CityId={0} ", searchInfo.CityId.Value);
                }
                if (searchInfo.DistrictId.HasValue)
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanyInfo AS A WHERE A.Id=view_CompanyUserInfo.CompanyId AND CountyId={0}) ", searchInfo.DistrictId.Value);
                }
                if (!string.IsNullOrEmpty(searchInfo.Keyword))
                {
                    cmdQuery.Append(" AND (1=0 ");
                    cmdQuery.AppendFormat(" OR CompanyName LIKE '%{0}%' ", searchInfo.Keyword);
                    cmdQuery.AppendFormat(" OR ContactName LIKE '%{0}%' ", searchInfo.Keyword);
                    cmdQuery.AppendFormat(" OR UserName LIKE '%{0}%' ", searchInfo.Keyword);
                    cmdQuery.AppendFormat(" OR ContactMobile LIKE '%{0}%' ", searchInfo.Keyword);
                    cmdQuery.Append(" ) ");
                }
                if (searchInfo.ProvinceId.HasValue)
                {
                    cmdQuery.AppendFormat(" AND ProvinceId={0} ", searchInfo.ProvinceId.Value);
                }
            }
            #endregion

            #region ordering
            if (searchInfo != null && searchInfo.OrderingType.HasValue)
            {
                switch (searchInfo.OrderingType.Value)
                {
                    case EyouSoft.Model.CompanyStructure.MLBYNUserInfo.OrderingType.Default:
                        orderByString = "IssueTime DESC";
                        break;
                    case EyouSoft.Model.CompanyStructure.MLBYNUserInfo.OrderingType.RegTimeDesc:
                        orderByString = "IssueTime DESC";
                        break;
                    case EyouSoft.Model.CompanyStructure.MLBYNUserInfo.OrderingType.RegTimeAsc:
                        orderByString = "IssueTime ASC";
                        break;
                    case EyouSoft.Model.CompanyStructure.MLBYNUserInfo.OrderingType.LoginTimeDesc:
                        orderByString = "LastLoginTime DESC";
                        break;
                    case EyouSoft.Model.CompanyStructure.MLBYNUserInfo.OrderingType.LoginTimeAsc:
                        orderByString = "LastLoginTime ASC";
                        break;
                    default:
                        orderByString = "IssueTime DESC";
                        break;
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(_database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.CompanyStructure.MLBYNUserInfo item = new EyouSoft.Model.CompanyStructure.MLBYNUserInfo();

                    //item.Area = rdr["ProvinceName"].ToString() + "-" + rdr["CityName"].ToString();
                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    item.CompanyName = rdr["CompanyName"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ContactSex")))
                    {
                        item.Gender = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(rdr.GetString(rdr.GetOrdinal("ContactSex")));
                    }
                    else
                    {
                        item.Gender = EyouSoft.Model.CompanyStructure.Sex.未知;
                    }
                    item.LoginTimes = rdr.GetInt32(rdr.GetOrdinal("LoginTimes"));
                    item.Mobile = rdr["ContactMobile"].ToString();
                    item.MQ = rdr["MQ"].ToString();
                    item.Name = rdr["ContactName"].ToString();
                    item.QQ = rdr["QQ"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastLoginTime")))
                    {
                        item.RecentlyLoginTime = rdr.GetDateTime(rdr.GetOrdinal("LastLoginTime"));
                    }
                    item.RegisterTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.UserId = rdr.GetString(rdr.GetOrdinal("Id"));
                    item.Username = rdr.GetString(rdr.GetOrdinal("UserName"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 是否存在对应的公司及用户编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public virtual bool IsExistsUserId(string companyId, string userId)
        {
            DbCommand cmd = _database.GetSqlStringCommand(SQL_SELECT_IsExistsUserId);
            _database.AddInParameter(cmd, "CID", DbType.AnsiStringFixedLength, companyId);
            _database.AddInParameter(cmd, "UID", DbType.AnsiStringFixedLength, userId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _database))
            {
                if (rdr.Read())
                {
                    if (rdr.GetInt32(0) == 1) return true;
                }
            }

            return false;
        }
        #endregion 成员方法

        #region 私有方法
        /// <summary>
        /// 初始化用户实体
        /// </summary>
        /// <param name="dc">dc</param>
        /// <returns></returns>
        private Model.CompanyStructure.CompanyUser InitUserModel(DbCommand dc)
        {
            EyouSoft.Model.CompanyStructure.CompanyUser model = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                //用户信息
                if (rdr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyUser();
                    model.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
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
                    model.DepartId = rdr.IsDBNull(rdr.GetOrdinal("DepartId")) == true ? "" : rdr.GetString(rdr.GetOrdinal("DepartId"));
                    model.DepartName = rdr.IsDBNull(rdr.GetOrdinal("DepartName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("DepartName"));
                    model.ID = rdr.IsDBNull(rdr.GetOrdinal("ID")) == true ? "" : rdr.GetString(rdr.GetOrdinal("ID"));
                    model.IsAdmin = rdr.GetString(rdr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    model.IsEnable = rdr.GetString(rdr.GetOrdinal("IsEnable")) == "1" ? true : false;
                    model.ContactInfo.MQ = rdr.IsDBNull(rdr.GetOrdinal("MQ")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MQ"));
                    model.ContactInfo.MSN = rdr.IsDBNull(rdr.GetOrdinal("MSN")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MSN"));
                    model.OpUserId = rdr.GetInt32(rdr.GetOrdinal("OpUserId"));

                    model.PassWordInfo.SetEncryptPassWord(rdr.IsDBNull(rdr.GetOrdinal("Password")) == true ? "" : rdr.GetString(rdr.GetOrdinal("Password")), rdr.IsDBNull(rdr.GetOrdinal("EncryptPassword")) == true ? "" : rdr.GetString(rdr.GetOrdinal("EncryptPassword")), rdr.IsDBNull(rdr.GetOrdinal("MD5Password")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MD5Password")));
                    model.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    model.ContactInfo.QQ = rdr.IsDBNull(rdr.GetOrdinal("QQ")) == true ? "" : rdr.GetString(rdr.GetOrdinal("QQ"));
                    model.RoleID = rdr.IsDBNull(rdr.GetOrdinal("RoleID")) == true ? "" : rdr.GetString(rdr.GetOrdinal("RoleID"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Job")))
                        model.Job = rdr.GetString(rdr.GetOrdinal("Job"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("im_displayname")))
                        model.MqNickName = rdr.GetString(rdr.GetOrdinal("im_displayname"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastLoginTime")))
                        model.LastLoginTime = rdr.GetDateTime(rdr.GetOrdinal("LastLoginTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastLoginIP")))
                        model.LastLoginIp = rdr.GetString(rdr.GetOrdinal("LastLoginIP"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LoginTimes")))
                        model.LoginCount = rdr.GetInt32(rdr.GetOrdinal("LoginTimes"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IssueTime")))
                        model.JoinTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                }
            }

            return model;
        }

        /// <summary>
        /// 初始化用户列表(类型转换会出错)
        /// </summary>
        /// <param name="dr">数据源</param>
        /// <param name="isInitBaseInfo">是否只初始化用户的基础信息</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CompanyStructure.CompanyUser> InitUserList(IDataReader dr, bool isInitBaseInfo)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyUser> list = new List<EyouSoft.Model.CompanyStructure.CompanyUser>();
            while (dr.Read())
            {
                EyouSoft.Model.CompanyStructure.CompanyUser model = new EyouSoft.Model.CompanyStructure.CompanyUser();
                model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) == true ? "" : dr.GetString(dr.GetOrdinal("ID"));
                model.UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) == true ? "" : dr.GetString(dr.GetOrdinal("UserName"));
                model.ContactInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                model.ContactInfo.Tel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactTel"));
                model.ContactInfo.Fax = dr.IsDBNull(dr.GetOrdinal("ContactFax")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactFax"));
                model.ContactInfo.Mobile = dr.IsDBNull(dr.GetOrdinal("contactMobile")) == true ? "" : dr.GetString(dr.GetOrdinal("contactMobile"));
                model.ContactInfo.Email = dr.IsDBNull(dr.GetOrdinal("ContactEmail")) == true ? "" : dr.GetString(dr.GetOrdinal("ContactEmail"));
                model.ContactInfo.QQ = dr.IsDBNull(dr.GetOrdinal("QQ")) == true ? "" : dr.GetString(dr.GetOrdinal("QQ"));
                model.ContactInfo.MSN = dr.IsDBNull(dr.GetOrdinal("MSN")) == true ? "" : dr.GetString(dr.GetOrdinal("MSN"));
                if (!isInitBaseInfo)
                {
                    model.IsEnable = dr.GetString(dr.GetOrdinal("IsEnable")) == "1" ? true : false;
                    model.IsAdmin = dr.GetString(dr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                }
                list.Add(model);
                model = null;
            }
            return list;
        }
        #endregion 私有方法
    }
}
