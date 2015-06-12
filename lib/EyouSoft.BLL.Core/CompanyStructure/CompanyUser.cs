using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 用户信息业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public class CompanyUser : EyouSoft.IBLL.CompanyStructure.ICompanyUser
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyUser idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyUser>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyUser CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyUser op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyUser>();
            }
            return op;
        }

        /// <summary>
        /// 判断E-MAIL是否已存在
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns></returns>
        public bool IsExistsEmail(string email)
        {
            return this.IsExistsEmail(email, "");
        }

        /// <summary>
        /// 判断E-MAIL是否已存在
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="userId">当前修改Email的用户ID</param>
        /// <returns></returns>
        public bool IsExistsEmail(string email, string userId)
        {
            if (string.IsNullOrEmpty(email))
                return true;
            return idal.IsExistsEmail(email.Trim(), userId);
        }

        /// <summary>
        /// 判断用户名是否已存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool IsExists(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return true;
            return idal.IsExists(userName.Trim());
        }

        /// <summary>
        /// 根据未加密密码初始化密码信息实体类(若明文密码为空,则返回所有密码字段都为空)
        /// </summary>
        /// <param name="noEncryptPassword">未加密密码</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.PassWord InitPassWordModel(string noEncryptPassword)
        {
            EyouSoft.Model.CompanyStructure.PassWord model = new EyouSoft.Model.CompanyStructure.PassWord();
            //设置密码字段都为空
            if (string.IsNullOrEmpty(noEncryptPassword))
                model.SetEncryptPassWord("", "", "");
            else
            {
                //设置所有的密码
                EyouSoft.Common.EncryptUtility.HashCrypto encrypt = new EyouSoft.Common.EncryptUtility.HashCrypto();
                model.SetEncryptPassWord(noEncryptPassword, encrypt.SHAEncrypt(noEncryptPassword, 384), encrypt.MD5Encrypt(noEncryptPassword));
                encrypt = null;
            }
            return model;
        }

        /// <summary>
        /// 添加帐号[添加帐号后自动生成MQ号码]
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        public EyouSoft.Model.ResultStructure.ResultInfo Add(EyouSoft.Model.CompanyStructure.CompanyUser model)
        {
            EyouSoft.Model.ResultStructure.ResultInfo result = EyouSoft.Model.ResultStructure.ResultInfo.Error;
            if (model == null || string.IsNullOrEmpty(model.UserName))
                return result;

            model.UserName = model.UserName.Trim();
            //设置所有的密码
            model.PassWordInfo = this.InitPassWordModel(model.PassWordInfo.NoEncryptPassword);

            result = idal.Add(ref model);
            //写入用户后要写入MQ客服号
            if (result == EyouSoft.Model.ResultStructure.ResultInfo.Succeed)
            {
                try
                {
                    EyouSoft.BLL.MQStructure.IMServiceMQ.CreateInstance().InsertFriendServiceMQ(Convert.ToInt32(model.ContactInfo.MQ), model.ProvinceId);
                }
                catch { }
            }

            #region 同步添加其他平台用户信息（暂时不参与同步）   zwc  2011-04-07

            //大平台加子帐号暂时不参与同步
            //if (result == EyouSoft.Model.ResultStructure.ResultInfo.Succeed)
            //    BLL.SyncStructure.SyncUser.SyncAddUserInfo(model);

            #endregion

            return result;
        }

        /// <summary>
        /// 修改个人设置信息(不修改密码,角色,部门)
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        public bool UpdatePersonal(EyouSoft.Model.CompanyStructure.CompanyUserBase model)
        {
            bool isTrue = false;
            //if (model == null || string.IsNullOrEmpty(model.ContactInfo.Email))
            if (model == null)   //不验证email是否为空(企业名片中老数据中是无email的)
                return isTrue;

            model.ContactInfo.Email = model.ContactInfo.Email.Trim();  //去除email空格
            isTrue = idal.UpdatePersonal(model);

            #region 同步修改其他平台用户信息   zwc  2011-04-07

            if (isTrue)
                this.SyncUpdateUserInfo(model.ID);

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 修改子帐号信息(公司编号,其它平台用户编号,省份编号,城市编号,用户名,是否停用,是否管理员不做修改)
        /// 若设置的密码为null或为空,则密码不进行修改,若有修改密码,则已同步修改了mq,机票等所有的相关密码
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        public bool UpdateChild(EyouSoft.Model.CompanyStructure.CompanyUser model)
        {
            bool isTrue = false;
            if (model == null)
                return isTrue;
            //设置所有的密码
            model.PassWordInfo = this.InitPassWordModel(model.PassWordInfo.NoEncryptPassword);

            isTrue = idal.Update(model);

            if (isTrue)
            {
                //若密码不为空,则要修改机票的密码
                //if (!string.IsNullOrEmpty(model.PassWordInfo.NoEncryptPassword))
                //{
                //    this.UpdateTicketUserPwd(model.ID, model.PassWordInfo.MD5Password);
                //}
            }

            #region 同步修改其他平台用户信息   zwc  2011-04-07

            if (isTrue)
                this.SyncUpdateUserInfo(model.ID);

            #endregion

            return isTrue;
        }

        /// <summary>
        /// 修改密码,若有修改密码,则已同步修改了mq,机票等所有的相关密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="password">密码实体类</param>
        /// <returns></returns>
        public bool UpdatePassWord(string id, EyouSoft.Model.CompanyStructure.PassWord password)
        {
            bool isTrue = false;
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password.NoEncryptPassword))
                return isTrue;

            //设置所有的密码
            password = this.InitPassWordModel(password.NoEncryptPassword);

            isTrue = idal.UpdatePassWord(id, password);

            if (isTrue)
            {
                //若密码不为空,则要修改机票的密码
                //if (!string.IsNullOrEmpty(password.NoEncryptPassword))
                //    this.UpdateTicketUserPwd(id, password.MD5Password);

                #region 同步修改其他平台用户信息   zwc  2011-04-07

                BLL.SyncStructure.SyncUser.SyncUpdateUserInfo(id);

                #endregion
            }

            return isTrue;
        }

        /// <summary>
        /// 更新机票用户的密码
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <param name="md5Pw">MD5密码</param>
        /// <returns></returns>
        public bool UpdateTicketUserPwd(string userId, string md5Pw)
        {
            bool isTrue = false;

            EyouSoft.Model.CompanyStructure.CompanyUser model = idal.GetModel(userId);
            if (model != null)
            {
                try
                {
                    isTrue = Adpost.Ticket.BLL.Ticket.TicketUpdateUserPwd(model.OpUserId.ToString(), md5Pw.ToUpper());
                }
                catch { }
            }
            model = null;

            return isTrue;
        }

        /// <summary>
        /// 真实删除用户表和MQ表信息
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        public bool Delete(params string[] userIdList)
        {
            if (userIdList == null || userIdList.Length == 0)
                return false;

            bool isTrue = idal.Delete(userIdList);

            #region 同步删除其他平台用户信息   zwc  2011-04-07

            if (isTrue)
            {
                BLL.SyncStructure.SyncUser.SyncDeleteUserInfo(userIdList);
            }

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 移除用户(即虚拟删除用户)
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        public bool Remove(params string[] userIdList)
        {
            if (userIdList == null || userIdList.Length == 0)
                return false;

            bool isTrue = idal.Remove(userIdList);

            #region 同步删除其他平台用户信息   zwc  2011-04-07

            if (isTrue)
            {
                BLL.SyncStructure.SyncUser.SyncDeleteUserInfo(userIdList);
            }

            #endregion

            return isTrue;
        }
        /// <summary>
        /// 获得子帐号数量[不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public int GetSubUserCount(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;
            return idal.GetSubUserCount(companyId);
        }
        /// <summary>
        /// 获取指定公司下的所有子帐号用户详细信息列表[注;不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyUser> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount)
        {
            EyouSoft.Model.CompanyStructure.QueryParamsUser Params = new EyouSoft.Model.CompanyStructure.QueryParamsUser();

            IList<EyouSoft.Model.CompanyStructure.CompanyUser> items = idal.GetList(companyId, Params, pageSize, pageIndex, ref recordCount);

            return GetUserArea(items); ;
        }
        /// <summary>
        /// 获取指定公司下的所有帐号用户详细信息列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="queryParams">查询参数实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyUser> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser queryParams, int pageSize, int pageIndex, ref int recordCount)
        {

            IList<EyouSoft.Model.CompanyStructure.CompanyUser> items = idal.GetList(companyId, queryParams, pageSize, pageIndex, ref recordCount);

            return GetUserArea(items); ;
        }
        /// <summary>
        /// 获取指定公司下的所有子帐号用户基本信息列表[注;不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> GetList(string companyId)
        {
            EyouSoft.Model.CompanyStructure.QueryParamsUser Params = new EyouSoft.Model.CompanyStructure.QueryParamsUser();
            return idal.GetList(companyId, Params);
        }
        /// <summary>
        /// 获取指定公司下的所有帐号用户基本信息列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="queryParams">查询参数实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser queryParams)
        {
            return idal.GetList(companyId, queryParams);
        }
        /// <summary>
        /// 状态设置
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isEnable">是否停用,true:停用,false:启用</param>
        /// <returns></returns>
        public bool SetEnable(string id, bool isEnable)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            return idal.SetEnable(id, isEnable);
        }

        /// <summary>
        /// 获得实体信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyUser GetModel(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            EyouSoft.Model.CompanyStructure.CompanyUser model = idal.GetModel(id);
            if (model != null)
            {
                //获得用户的线路区域信息
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetUserArea(id);
                if (areaList != null && areaList.Count > 0)
                    model.Area = this.GetCacheAreaAllInfo(areaList);
            }
            return model;
        }
        /// <summary>
        /// 获得实体信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyUser GetModelByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            EyouSoft.Model.CompanyStructure.CompanyUser model = idal.GetModelByUserName(userName);
            if (model != null)
            {
                //获得用户的线路区域信息
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetUserArea(model.ID);
                if (areaList != null && areaList.Count > 0)
                    model.Area = this.GetCacheAreaAllInfo(areaList);
            }
            return model;
        }
        /// <summary>
        /// 获得管理员实体信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyUser GetAdminModel(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;
            EyouSoft.Model.CompanyStructure.CompanyUser model = idal.GetAdminModel(companyId);
            if (model != null)
            {
                //获得用户的线路区域信息
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetUserArea(model.ID);
                if (areaList != null && areaList.Count > 0)
                    model.Area = this.GetCacheAreaAllInfo(areaList);
            }
            return model;
        }

        /// <summary>
        /// 根据MQID获取用户信息
        /// </summary>
        /// <param name="MQId">用户对应MQID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyUser GetModel(int MQId)
        {
            if (MQId <= 0)
                return null;
            EyouSoft.Model.CompanyStructure.CompanyUser model = idal.GetModel(MQId);
            if (model != null)
            {
                //获得用户的线路区域信息
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetUserArea(model.ID);
                if (areaList != null && areaList.Count > 0)
                    model.Area = this.GetCacheAreaAllInfo(areaList);
            }
            return model;
        }

        /// <summary>
        /// 根据oPUserID获取用户信息
        /// </summary>
        /// <param name="oPUserID">用户对应oPUserID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyUser GetModelByOPUserID(int oPUserID)
        {
            if (oPUserID <= 0)
                return null;
            EyouSoft.Model.CompanyStructure.CompanyUser model = idal.GetModelByOPUserID(oPUserID);
            if (model != null)
            {
                //获得用户的线路区域信息
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetUserArea(model.ID);
                if (areaList != null && areaList.Count > 0)
                    model.Area = this.GetCacheAreaAllInfo(areaList);
            }
            return model;
        }

        /// <summary>
        /// 获得缓存中指定线路区域ID所有的线路区域信息
        /// </summary>
        /// <param name="items">城市ID集合</param>
        private List<EyouSoft.Model.SystemStructure.AreaBase> GetCacheAreaAllInfo(List<EyouSoft.Model.SystemStructure.AreaBase> items)
        {
            //从缓存中获取省份ID,省份名称,城市名称
            if (items != null && items.Count > 0)
            {
                int count = items.Count;
                EyouSoft.Model.SystemStructure.SysArea areaModel = null;
                for (int index = 0; index < count; index++)
                {
                    if (items[index] != null)
                    {
                        areaModel = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(items[index].AreaId);
                        if (areaModel != null)
                        {
                            items[index].AreaName = areaModel.AreaName;
                            items[index].RouteType = areaModel.RouteType;
                        }
                    }
                }
                areaModel = null;
            }

            return items;
        }

        /// <summary>
        /// 获得用户的线路区域信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CompanyStructure.CompanyUser> GetUserArea(IList<EyouSoft.Model.CompanyStructure.CompanyUser> items)
        {
            if (items != null && items.Count > 0)
            {
                int len = items.Count;
                //获得用户的线路区域信息
                for (int index = 0; index < len; index++)
                {
                    List<EyouSoft.Model.SystemStructure.AreaBase> areaList = (List<EyouSoft.Model.SystemStructure.AreaBase>)EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetUserArea(items[index].ID);
                    if (areaList != null && areaList.Count > 0)
                        items[index].Area = this.GetCacheAreaAllInfo(areaList);
                }
            }
            return items;
        }

        /// <summary>
        /// 同步修改其他平台用户信息   zwc  2011-04-07
        /// </summary>
        /// <param name="PlamUserId">用户Id</param>
        private void SyncUpdateUserInfo(string PlamUserId)
        {
            BLL.SyncStructure.SyncUser.SyncUpdateUserInfo(PlamUserId);
        }

        /// <summary>
        /// 获取用户列表信息集合
        /// </summary>
        /// <param name="pageSize">每页记录灵敏</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.MLBYNUserInfo> GetUsers(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CompanyStructure.MLBYNUserSearchInfo searchInfo)
        {
            return idal.GetUsers(pageSize, pageIndex, ref recordCount, searchInfo);
        }
        /// <summary>
        /// 是否存在对应的公司及用户编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public bool IsExistsUserId(string companyId, string userId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(userId) || companyId.Length != 36 || userId.Length != 36) return false;

            return idal.IsExistsUserId(companyId, userId);
        }
    }
}
