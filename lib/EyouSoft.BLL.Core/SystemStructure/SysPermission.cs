using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 系统权限明细业务逻辑
    /// </summary>
    /// 周文超 2010-07-05
    public class SysPermission : IBLL.SystemStructure.ISysPermission
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermission() { }

        private readonly IDAL.SystemStructure.ISysPermission dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysPermission>();

        /// <summary>
        /// 构造系统权限明细业务逻辑接口
        /// </summary>
        /// <returns>系统权限明细业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysPermission CreateInstance()
        {
            IBLL.SystemStructure.ISysPermission op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysPermission>();
            }
            return op;
        }

        #region ISysPermission 成员

        /// <summary>
        /// 新增系统权限数据
        /// </summary>
        /// <param name="model">系统权限数据实体</param>
        /// <returns>返回受影响行数</returns>
        public int AddSysPermission(EyouSoft.Model.SystemStructure.SysPermission model)
        {
            if (model == null)
                return 0;

            return dal.AddSysPermission(model);
        }

        /// <summary>
        /// 修改系统权限数据
        /// </summary>
        /// <param name="model">系统权限数据实体</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateSysPermission(EyouSoft.Model.SystemStructure.SysPermission model)
        {
            if (model == null)
                return 0;

            return dal.UpdateSysPermission(model);
        }

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="SysPermissionId">系统权限ID</param>
        /// <param name="IsEnable">是否启用(1：启用；0：禁用)</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsEnable(int SysPermissionId, bool IsEnable)
        {
            if (SysPermissionId <= 0)
                return false;

            return dal.SetIsEnable(SysPermissionId.ToString(), IsEnable);
        }

        /// <summary>
        /// 批量设置是否启用
        /// </summary>
        /// <param name="SysPermissionIds">系统权限ID集合</param>
        /// <param name="IsEnable">是否启用(1：启用；0：禁用)</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsEnable(int[] SysPermissionIds, bool IsEnable)
        {
            if (SysPermissionIds == null || SysPermissionIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in SysPermissionIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');
            if (strIds.Equals(string.Empty))
                return false;
            else
                return dal.SetIsEnable(strIds, IsEnable);
        }

        /// <summary>
        /// 删除系统权限数据
        /// </summary>
        /// <param name="SysPermissionId">系统权限ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysPermission(int SysPermissionId)
        {
            if (SysPermissionId <= 0)
                return false;

            return dal.DeleteSysPermission(SysPermissionId.ToString());
        }

        /// <summary>
        /// 批量删除系统权限数据
        /// </summary>
        /// <param name="SysPermissionIds">系统权限ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysPermission(int[] SysPermissionIds)
        {
            if (SysPermissionIds == null || SysPermissionIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in SysPermissionIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');
            if (strIds.Equals(string.Empty))
                return false;
            else
                return dal.DeleteSysPermission(strIds);
        }

        /// <summary>
        /// 获取系统权限数据实体
        /// </summary>
        /// <param name="SysPermissionId">系统权限ID</param>
        /// <returns>返回系统权限数据实体</returns>
        public EyouSoft.Model.SystemStructure.SysPermission GetSysPermission(int SysPermissionId)
        {
            if (SysPermissionId <= 0)
                return null;

            return dal.GetSysPermission(SysPermissionId);
        }

        /// <summary>
        /// 获取用户后台所有的（已启用的）权限
        /// </summary>
        /// <param name="companyTypes">用户公司类型集合</param>
        /// <returns></returns>
        public Model.SystemStructure.SysPermissionCategory GetAllPermissionByUser(Model.CompanyStructure.CompanyType[] companyTypes)
        {
            return dal.GetAllPermissionByUser(companyTypes);
        }

        #endregion
    }
}
