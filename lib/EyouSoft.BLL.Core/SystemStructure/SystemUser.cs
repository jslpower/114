using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;
using System.Transactions;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 系统用户业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SystemUser : IBLL.SystemStructure.ISystemUser
    {
        private readonly IDAL.SystemStructure.ISystemUser dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISystemUser>();

        /// <summary>
        /// 构造系统用户业务逻辑接口
        /// </summary>
        /// <returns>系统用户业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISystemUser CreateInstance()
        {
            IBLL.SystemStructure.ISystemUser op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISystemUser>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemUser() { }


        #region ISystemUser 成员

        /// <summary>
        /// 新增系统用户(管理员区域ID集合为null或者count小于等于0不添加)
        /// </summary>
        /// <param name="model">系统用户实体</param>
        /// <returns>-2:新增管理员区域失败;-1:新增用户失败;0:用户实体为空;1:Success</returns>
        public int AddSystemUser(EyouSoft.Model.SystemStructure.SystemUser model)
        {
            if (model == null)
                return 0;

            //设置所有的密码
            model.PassWordInfo = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(model.PassWordInfo.NoEncryptPassword);

            int Result = 0;
            using (TransactionScope AddTran = new TransactionScope())
            {
                Result = dal.AddSystemUser(model);
                model.ID = Result;
                if (Result <= 0)
                    return -1;
                if (model.AreaId != null && model.AreaId.Count > 0)
                {
                    Result = dal.AddSysUserAreaControl(Result, model.AreaId);
                    if (Result <= 0)
                        return -2;
                }
                if (model.CustomerTypeIds != null && model.CustomerTypeIds.Count > 0)
                {
                    Result = dal.AddSysUserCustomerType(model.ID, model.CustomerTypeIds);
                    if (Result <= 0)
                        return -3;
                }

                Result = 1;
                AddTran.Complete();
            }

            return Result;
        }

        /// <summary>
        /// 修改系统用户
        /// </summary>
        /// <param name="model">系统用户实体(区域ID集合为null或者count小于等于0不修改用户和区域的关系)</param>
        /// <returns>-3:新增管理员区域失败;-2:删除旧用户和区域关系失败;-1:新增用户失败;0:用户实体为空;1:Success</returns>
        public int UpdateSystemUser(EyouSoft.Model.SystemStructure.SystemUser model)
        {
            if (model == null)
                return 0;

            int Result = 0;
            using (TransactionScope AddTran = new TransactionScope())
            {
                Result = dal.UpdateSystemUser(model);
                if (Result <= 0)
                    return -1;

                IList<int> Ids = new List<int>();
                Ids.Add(model.ID);
                if (model.AreaId != null && model.AreaId.Count > 0)
                {
                    Result = dal.DeleteSysUserAreaControl(Ids);
                    Result = dal.AddSysUserAreaControl(model.ID, model.AreaId);
                    if (Result <= 0)
                        return -3;
                }
                if (model.CustomerTypeIds != null && model.CustomerTypeIds.Count > 0)
                {
                    Result = dal.DeleteSysUserCustomerType(Ids);
                    Result = dal.AddSysUserCustomerType(model.ID, model.CustomerTypeIds);
                    if (Result <= 0)
                        return -4;
                }
                Ids.Clear();
                Ids = null;

                Result = 1;
                AddTran.Complete();
            }
            return Result;
        }

        /// <summary>
        ///  根据用户ID删除用户
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSystemUser(IList<int> SystemUserIds)
        {
            if (SystemUserIds == null || SystemUserIds.Count <= 0)
                return false;

            bool IsDelUser = false;
            using (TransactionScope DelTran = new TransactionScope())
            {
                dal.DeleteSysUserCustomerType(SystemUserIds);
                dal.DeleteSysUserAreaControl(SystemUserIds);
                IsDelUser =dal.DeleteSystemUser(SystemUserIds);
                if (!IsDelUser)
                    return false;
                DelTran.Complete();
            }
            return IsDelUser;
        }

        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0添加时间升序，其他添加时间降序</param>
        /// <param name="AreaId">管理员区域ID</param>
        /// <param name="UserName">用户名(模糊查询)</param>
        /// <param name="ContactName">联系人(模糊查询)</param>
        /// <returns>系统用户实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SystemUser> GetSystemUserList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, int AreaId, string UserName, string ContactName)
        {
            return dal.GetSystemUserList(PageSize, PageIndex, ref RecordCount, OrderIndex, AreaId, UserName, ContactName);
        }

        /// <summary>
        /// 根据用户名和密码修改密码
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="OldPassWord">原来的密码</param>
        /// <param name="NewPassWord">新密码</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateUserPassWord(string UserName, string OldPassWord, string NewPassWord)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(OldPassWord) || string.IsNullOrEmpty(NewPassWord))
                return 0;

            int Result = 0;


            //设置所有的密码
            EyouSoft.Model.CompanyStructure.PassWord OldPassword = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(OldPassWord);
            EyouSoft.Model.CompanyStructure.PassWord NewPassword = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(NewPassWord);

            Result = dal.UpdateUserPassWord(UserName, OldPassword, NewPassword);
            if (Result > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 根据用户ID修改密码
        /// </summary>
        /// <param name="SystemUserId">用户id</param>
        /// <param name="PassWord">密码</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateUserPassWord(int SystemUserId, string PassWord)
        {
            if (SystemUserId <= 0 || string.IsNullOrEmpty(PassWord))
                return 0;

            int Result = 0;

            //设置所有的密码
            EyouSoft.Model.CompanyStructure.PassWord password = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().InitPassWordModel(PassWord);

            Result = dal.UpdateUserPassWord(SystemUserId, password);
            if (Result > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="SystemUserId">用户ID</param>
        /// <returns>返回用户实体</returns>
        public EyouSoft.Model.SystemStructure.SystemUser GetSystemUserModel(int SystemUserId)
        {
            if (SystemUserId <= 0)
                return null;

            return dal.GetSystemUserModel(SystemUserId);
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public bool ExistsByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
                return false;

            return dal.ExistsByUserName(UserName);
        }

        /// <summary>
        /// 验证用户是否存在(存在返回该用户实体，不存在返回null)
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns>存在返回该用户实体，不存在返回null</returns>
        public Model.SystemStructure.SystemUser ExistsByUserName(string UserName, string PassWord)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord))
                return null;

            return dal.ExistsByUserName(UserName, PassWord);
        }

        /// <summary>
        /// 设置该用户是否停用/启用
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="IsDisable">停用(true)/启用(false)</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsDisable(int UserId, bool IsDisable)
        {
            if (UserId <= 0)
                return false;

            return dal.SetIsDisable(UserId, IsDisable);
        }

        #endregion
    }
}
