using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 系统类型定义业务逻辑(系统线路主题、景点主题、客户等级)
    /// </summary>
    /// 周文超 2010-06-23
    public class SysField : IBLL.SystemStructure.ISysField
    {
        private readonly IDAL.SystemStructure.ISysField dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysField>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysField() { }

        /// <summary>
        /// 构造系统类型定义业务逻辑接口
        /// </summary>
        /// <returns>系统类型定义业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysField CreateInstance()
        {
            IBLL.SystemStructure.ISysField op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysField>();
            }
            return op;
        }

        #region ISysField 成员
        /// <summary>
        /// 获取景点主题
        /// </summary>
        /// <returns></returns>
        public IList<Model.SystemStructure.SysFieldBase> GetSightTheme()
        {
            return GetSysField(Model.SystemStructure.SysFieldType.景点主题);
        }
        /// <summary>
        /// 获取客户等级
        /// </summary>
        /// <returns></returns>
        public IList<Model.SystemStructure.SysFieldBase> GetCustomerLevel()
        {
            return GetSysField(Model.SystemStructure.SysFieldType.客户等级);
        }
        /// <summary>
        /// 获取线路主题
        /// </summary>
        /// <returns></returns>
        public IList<Model.SystemStructure.SysFieldBase> GetRouteTheme()
        {
            return GetSysField(Model.SystemStructure.SysFieldType.线路主题);
        }
        /// <summary>
        /// 获取周边环境
        /// </summary>
        /// <returns></returns>
        public IList<Model.SystemStructure.SysFieldBase> GetHotelArea()
        {
            return GetSysField(Model.SystemStructure.SysFieldType.周边环境);
        }

        /// <summary>
        /// 根据系统类型定义ID获取名称
        /// </summary>
        /// <param name="FieldId">系统类型定义ID</param>
        /// <param name="SysFieldType">系统类型定义类型</param>
        /// <returns>系统类型定义名称</returns>
        public string GetFieldNameById(int FieldId, Model.SystemStructure.SysFieldType SysFieldType)
        {
            string strName = string.Empty;
            IList<Model.SystemStructure.SysFieldBase> tmpList = GetSysField(SysFieldType);
            if (tmpList == null && tmpList.Count <= 0)
                return strName;

            tmpList = tmpList.Where(Item => (Item.FieldId == FieldId)).ToList();
            if (tmpList == null && tmpList.Count <= 0)
                return strName;

            strName = tmpList[0].FieldName;
            if (tmpList != null) tmpList.Clear();
            tmpList = null;

            return strName;
        }

        /// <summary>
        /// 获取已启用的系统类型定义集合(缓存取值)
        /// </summary>
        /// <param name="SysFieldType">系统类型定义类型</param>
        /// <returns>系统类型定义集合</returns>
        public IList<Model.SystemStructure.SysFieldBase> GetSysFieldBaseList(Model.SystemStructure.SysFieldType SysFieldType)
        {
            return GetSysField(SysFieldType);
        }

        /// <summary>
        /// 获取系统类型定义集合(运营后台用)
        /// </summary>
        /// <param name="SysFieldType">系统类型定义类型(为null不作条件)</param>
        /// <returns>系统类型定义集合</returns>
        public IList<Model.SystemStructure.SysField> GetSysFieldList(Model.SystemStructure.SysFieldType? SysFieldType)
        {
            if (SysFieldType != null && SysFieldType == EyouSoft.Model.SystemStructure.SysFieldType.客户等级)
            {
                return dal.GetSysFieldList(SysFieldType, true, null);
            }
            return dal.GetSysFieldList(SysFieldType, null, null);
        }

        /// <summary>
        /// 新增系统类型定义
        /// </summary>
        /// <param name="model">系统类型定义实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysField(EyouSoft.Model.SystemStructure.SysField model)
        {
            if (model == null)
                return 0;

            int Result = dal.AddSysField(model);
            if (Result > 0)
            {
                RemoveCache(model.FieldType);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 修改系统类型定义(只修改名称)
        /// </summary>
        /// <param name="FieldName">系统类型定义名称</param>
        /// <param name="FieldId">系统类型定义ID</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateSysField(string FieldName, int FieldId, Model.SystemStructure.SysFieldType FieldType)
        {
            if (string.IsNullOrEmpty(FieldName))
                return 0;

            int Result = dal.UpdateSysField(FieldName, FieldId, FieldType);
            if (Result > 0)
            {
                RemoveCache(FieldType);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="FieldId">系统类型定义ID集合</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsEnabled(int[] FieldId, EyouSoft.Model.SystemStructure.SysFieldType FieldType, bool IsEnabled)
        {
            if (FieldId == null || FieldId.Length <= 0)
                return false;

            string strFieldIds = string.Empty;
            foreach (int i in FieldId)
            {
                strFieldIds += i.ToString() + ",";
            }
            strFieldIds = strFieldIds.TrimEnd(',');
            bool Result = dal.SetIsEnabled(strFieldIds, FieldType, IsEnabled);
            if (Result)
                RemoveCache(FieldType);

            return Result;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 获取系统类型定义列表
        /// </summary>
        /// <param name="SysFieldType">系统类型定义的类型</param>
        /// <returns>系统类型定义集合</returns>
        private IList<Model.SystemStructure.SysFieldBase> GetSysField(EyouSoft.Model.SystemStructure.SysFieldType SysFieldType)
        {
            IList<Model.SystemStructure.SysFieldBase> List = new List<Model.SystemStructure.SysFieldBase>();
            string strTag = string.Empty;
            switch (SysFieldType)
            {
                case EyouSoft.Model.SystemStructure.SysFieldType.景点主题:
                    strTag = EyouSoft.CacheTag.System.SightTheme;
                    break;
                case EyouSoft.Model.SystemStructure.SysFieldType.客户等级:
                    strTag = EyouSoft.CacheTag.System.CustomerLevel;
                    break;
                case EyouSoft.Model.SystemStructure.SysFieldType.线路主题:
                    strTag = EyouSoft.CacheTag.System.RouteTheme;
                    break;
                case EyouSoft.Model.SystemStructure.SysFieldType.周边环境:
                    strTag = EyouSoft.CacheTag.System.HotelArea;
                    break;
            }
            object CacheList = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(strTag);
            if (CacheList == null)
            {
                List = dal.GetSysFieldBaseList(SysFieldType, true, null);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(strTag, List);
            }
            else
                List = (IList<Model.SystemStructure.SysFieldBase>)CacheList;

            return List;
        }
        /// <summary>
        /// 真实删除客户等级
        /// </summary>
        /// <param name="FieldId">客户等级编号集合</param>
        /// <param name="FieldType">客户等级类型</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(int[] FieldId, Model.SystemStructure.SysFieldType FieldType)
        {
            if (FieldId == null || FieldId.Length == 0)
                return false;
            String strFields = string.Empty;
            for (int i = 0; i < FieldId.Length; i++)
            {
                strFields += FieldId[i] + ",";
            }
            strFields = strFields.TrimEnd(',');
            bool Result = dal.Delete(strFields,FieldType);
            if (Result)
                RemoveCache(FieldType);
            return Result;
        }
        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="SysFieldType">系统类型定义的类型</param>
        private void RemoveCache(EyouSoft.Model.SystemStructure.SysFieldType SysFieldType)
        {
            string strTag = string.Empty;
            switch (SysFieldType)
            {
                case EyouSoft.Model.SystemStructure.SysFieldType.景点主题:
                    strTag = EyouSoft.CacheTag.System.SightTheme;
                    break;
                case EyouSoft.Model.SystemStructure.SysFieldType.客户等级:
                    strTag = EyouSoft.CacheTag.System.CustomerLevel;
                    break;
                case EyouSoft.Model.SystemStructure.SysFieldType.线路主题:
                    strTag = EyouSoft.CacheTag.System.RouteTheme;
                    break;
                case EyouSoft.Model.SystemStructure.SysFieldType.周边环境:
                    strTag = EyouSoft.CacheTag.System.HotelArea;
                    break;
            }
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(strTag);
        }

        #endregion
    }
}
