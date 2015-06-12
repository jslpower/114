using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;
namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-06
    /// 描述：系统权限子类别业务层
    /// </summary>
    public class SysPermissionClass : IBLL.SystemStructure.ISysPermissionClass
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermissionClass() { }

        private readonly IDAL.SystemStructure.ISysPermissionClass dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysPermissionClass>();

        /// <summary>
        /// 构造业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.SystemStructure.ISysPermissionClass CreateInstance()
        {
            IBLL.SystemStructure.ISysPermissionClass op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysPermissionClass>();
            }
            return op;
        }

        #region ISysPermissionClass成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">系统权限子类别实体类</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.SystemStructure.SysPermissionClass model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">系统权限子类别实体类</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.SystemStructure.SysPermissionClass model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteByMultiID(int[] Ids)
        {
            if (Ids.Length == 0|| Ids==null)
                return false;
            StringBuilder strWhere = new StringBuilder();
            for (int i = 0; i < Ids.Length - 1; i++)
            {
                strWhere.AppendFormat("{0}{1}", i > 0 ? "," : "", Ids[i].ToString());
            }
            return dal.Delete(strWhere.ToString());
        }
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public bool DeleteByID(int Id)
        {
            if (Id == 0)
                return false;
            return dal.Delete(Id.ToString());
        }
        /// <summary>
        /// 批量设置启用状态
        /// </summary>
        /// <param name="Ids">权限编号</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetEnableByMultiID(int[] Ids, bool IsEnable)
        {
            if (Ids.Length == 0)
                return false;
            StringBuilder strWhere = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                strWhere.AppendFormat("{0}{1}", i > 0 ? "," : "", Ids[i].ToString());
            }
            return dal.SetEnable(strWhere.ToString(), IsEnable);
        }
        /// <summary>
        /// 单个设置启用状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        public bool SetEnableByID(int Id, bool IsEnable)
        {
            if (Id == 0)
                return false;
            return dal.SetEnable(Id.ToString(), IsEnable);
        }
        /// <summary>
        /// 根据权限类别获取数据
        /// </summary>
        /// <param name="PermissionTypes">权限类别集合</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysPermissionClass> GetList(int[] PermissionTypes)
        {
            if (PermissionTypes.Length == 0 || PermissionTypes == null)
                return null;
            StringBuilder strWhere = new StringBuilder();
            for (int i = 0; i < PermissionTypes.Length; i++)
            {
                strWhere.AppendFormat("{0}{1}", i > 0 ? "," : "", PermissionTypes[i].ToString());
            }
            return dal.GetList(strWhere.ToString(), 0, true);
        }
        /// <summary>
        /// 根据权限大类别获取数据
        /// </summary>
        /// <param name="CategroyType">权限大类别</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysPermissionClass> GetList(int CategroyType)
        {
            if (CategroyType == 0)
                return null ;
            return dal.GetList(string.Empty, CategroyType, true);
        }
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SysPermissionClass GetModel(int ID)
        {
            if (ID == 0)
                return null;
            return dal.GetModel(ID);
        }
        #endregion

    }
}
