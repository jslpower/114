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
    /// 描述：权限大类别业务层
    /// </summary>
    public class SysPermissionCategory:IBLL.SystemStructure.ISysPermissionCategory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermissionCategory() { }

        private readonly IDAL.SystemStructure.ISysPermissionCategory dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysPermissionCategory>();        
        private readonly IDAL.SystemStructure.ISysPermission permissiondal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysPermission>();

        /// <summary>
        /// 构造业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.SystemStructure.ISysPermissionCategory CreateInstance()
        {
            IBLL.SystemStructure.ISysPermissionCategory op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysPermissionCategory>();
            }
            return op;
        }

        #region ISysPermissionCategory成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">系统权限大类别实体类</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.SystemStructure.SysPermissionCategory model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">系统权限大类别实体类</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.SystemStructure.SysPermissionCategory model)
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
        public bool DeleteIDs(int[] Ids)
        {
            if (Ids.Length == 0)
                return false;
            StringBuilder strWhere = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                strWhere.AppendFormat("{0}{1}", i > 0 ? "," : "", Ids[i].ToString());
            }
            return dal.Delete(strWhere.ToString());
        }
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteByID(int Id)
        {
            if (Id == 0)
                return false;
            return dal.Delete(Id.ToString());
        }
        /// <summary>
        /// 批量设置启用状态
        /// </summary>
        /// <param name="Ids">权限编号(1,2,3,4)</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetEnableByIDs(int[] Ids, bool IsEnable)
        {
            if (Ids.Length == 0)
                return false;
            StringBuilder strWhere = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                strWhere.AppendFormat("{0}{1}", i > 0 ? "," : "", Ids[i].ToString());
            }
            return dal.SetEnable(strWhere.ToString(),IsEnable);
        }
        /// <summary>
        /// 单个设置启用状态
        /// </summary>
        /// <param name="ID">权限编号</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetEnableByID(int ID, bool IsEnable)
        {
            if (ID == 0)
                return false;
            return dal.SetEnable(ID.ToString(), IsEnable);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SysPermissionCategory GetModel(int ID)
        {
            if (ID == 0)
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 分页获取权限大类别列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="PermissionType">权限类别数组（如组团，地接等等）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(int pageSize, int pageIndex, ref int recordCount, int[] PermissionType)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, PermissionType);
        }
        /// <summary>
        /// 个人中心权限管理获取指定权限类别的权限详细列表
        /// </summary>
        /// <param name="PermissionType">公司类别数组（如组团，地接等等）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(int[] PermissionType)
        {
            IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> list = new List<EyouSoft.Model.SystemStructure.SysPermissionCategory>();
            list = dal.GetList(new int[1] { 2 }, false);//个人中心权限
            if(list!=null && list.Count>0)
                list[0].SysPermissionClass = list[0].SysPermissionClass.Where(item=>(PermissionType.Contains(item.Id) && item.Id<=3) || item.Id>3 ).ToList();
            foreach (EyouSoft.Model.SystemStructure.SysPermissionCategory CategoryModel in list)
            {
                foreach (EyouSoft.Model.SystemStructure.SysPermissionClass ClassModel in CategoryModel.SysPermissionClass)
                {
                    //专线、组团、地接权限类型IN(1,2,3)
                    if ((ClassModel.Id <= 3 && PermissionType.Contains(ClassModel.Id)) || ClassModel.Id > 3)
                    {
                        ClassModel.SysPermission = permissiondal.GetSysPermissionList(ClassModel.CategoryId, ClassModel.Id);
                    }                    
                }
            }
            return list;
        }
        /// <summary>
        /// 获取指定权限类别的权限详细列表
        /// </summary>
        /// <param name="PermissionType">系统权限类别</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(EyouSoft.Model.SystemStructure.PermissionType PermissionType)
        {
            return dal.GetList(new int[1]{(int)PermissionType},true);
        }
        #endregion
    }
}
