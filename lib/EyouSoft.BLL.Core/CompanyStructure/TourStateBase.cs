using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-27
    /// 描述：推广类型 业务逻辑层
    /// </summary>
    public class TourStateBase : IBLL.CompanyStructure.ITourStateBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourStateBase() { }

        private readonly IDAL.CompanyStructure.ITourStateBase dal = ComponentFactory.CreateDAL<IDAL.CompanyStructure.ITourStateBase>();

        /// <summary>
        /// 创建IBLL对象
        /// </summary>
        /// <returns></returns>
        public static IBLL.CompanyStructure.ITourStateBase CreateInstance()
        {
            IBLL.CompanyStructure.ITourStateBase op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.CompanyStructure.ITourStateBase>();
            }
            return op;
        }

        #region ITourStateBase成员
        /// <summary>
        /// 修改推广类型
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="text">文字说明</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(int id, string text)
        {
            if (id == 0 || string.IsNullOrEmpty(text))
                return false;
            bool flag= dal.Update(id, text);

            #region 清空Cache
            if (flag)
            {
                EyouSoft.SSOComponent.Entity.UserInfo userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                if (userInfo != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.TourStateBase + userInfo.CompanyID);
                }
                userInfo = null;
            }
            #endregion

            return flag;
        }
        /// <summary>
        /// 获取推广类型实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>推广类型实体</returns>
        public EyouSoft.Model.CompanyStructure.TourStateBase GetModel(int id)
        {
            if(id>0)
                return dal.GetModel(id);
            return null;
        }
        /// <summary>
        /// 验证指定公司是否存在指定的推广类型
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="text">文字说明</param>
        /// <returns>true:存在 false:不存在</returns>
        public bool Exists(string CompanyID, string text)
        {
            if (string.IsNullOrEmpty(CompanyID) || string.IsNullOrEmpty(text))
                return false;
            return dal.Exists(CompanyID, text);
        }
        /*
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>0： 删除失败 1： 删除成功</returns>
        public int Delete(int id)
        {
            if (id == 0)
                return 0;
            return dal.Delete(id);
        }
         * */
        /// <summary>
        /// 获取指定公司的所有推广类型
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>推广类型列表</returns>
        public IList<EyouSoft.Model.CompanyStructure.TourStateBase> GetList(string CompanyID)
        {  
            if (string.IsNullOrEmpty(CompanyID))
                return null;
            object obj=EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.Company.TourStateBase + CompanyID);
            if (obj != null)
            {
                return (IList<EyouSoft.Model.CompanyStructure.TourStateBase>)obj;
            }
            else
            {
                IList<EyouSoft.Model.CompanyStructure.TourStateBase> list = dal.GetList(CompanyID);
                if(list!=null)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.Company.TourStateBase + CompanyID, list);
                return list;
                
            }
        }
        #endregion
    }
}
