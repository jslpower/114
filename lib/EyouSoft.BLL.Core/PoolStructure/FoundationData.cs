using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.PoolStructure
{
    //Author:周文超 2010-11-26

    #region 易诺用户池适用产品类型业务逻辑

    /// <summary>
    /// 易诺用户池适用产品类型业务逻辑
    /// </summary>
    public class SuitProduct : IBLL.PoolStructure.ISuitProduct
    {
        private readonly IDAL.PoolStructure.ISuitProduct dal = ComponentFactory.CreateDAL<IDAL.PoolStructure.ISuitProduct>();

        /// <summary>
        /// 构造易诺用户池适用产品类型业务逻辑接口
        /// </summary>
        /// <returns>易诺用户池适用产品类型业务逻辑接口</returns>
        public static IBLL.PoolStructure.ISuitProduct CreateInstance()
        {
            IBLL.PoolStructure.ISuitProduct op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.PoolStructure.ISuitProduct>();
            }
            return op;
        }


        #region ISuitProduct 成员

        /// <summary>
        /// 新增适用产品类型
        /// </summary>
        /// <param name="model">适用产品类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public int AddSuitProduct(EyouSoft.Model.PoolStructure.SuitProductInfo model)
        {
            if (model == null)
                return 0;

            return dal.AddSuitProduct(model);
        }

        /// <summary>
        /// 修改适用产品类型
        /// </summary>
        /// <param name="model">适用产品类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public int UpdateSuitProduct(EyouSoft.Model.PoolStructure.SuitProductInfo model)
        {
            if (model == null || model.ProuctId <= 0)
                return 0;

            return dal.UpdateSuitProduct(model);
        }

        /// <summary>
        /// 删除适用产品类型
        /// </summary>
        /// <param name="SuitProductIds">适用产品类型ID集合</param>
        /// <returns>1：成功；0：失败</returns>
        public int DeleteSuitProduct(List<int> SuitProductIds)
        {
            if (SuitProductIds == null || SuitProductIds.Count <= 0)
                return 0;

            return dal.DeleteSuitProduct(SuitProductIds);
        }

        /// <summary>
        /// 根据ID获取适用产品类型
        /// </summary>
        /// <param name="SuitProductId">适用产品类型Id</param>
        /// <returns>适用产品类型实体</returns>
        public EyouSoft.Model.PoolStructure.SuitProductInfo GetSuitProduct(int SuitProductId)
        {
            if (SuitProductId <= 0)
                return null;

            return dal.GetSuitProduct(SuitProductId);
        }

        /// <summary>
        /// 获取适用产品类型
        /// </summary>
        /// <returns>适用产品类型实体集合</returns>
        public IList<EyouSoft.Model.PoolStructure.SuitProductInfo> GetSuitProductList()
        {
            return dal.GetSuitProductList();
        }

        #endregion
    }

    #endregion

    #region 易诺用户池客户类型业务逻辑

    /// <summary>
    /// 易诺用户池客户类型业务逻辑
    /// </summary>
    public class CustomerType : IBLL.PoolStructure.ICustomerType
    {
        private readonly IDAL.PoolStructure.ICustomerType dal = ComponentFactory.CreateDAL<IDAL.PoolStructure.ICustomerType>();

        /// <summary>
        /// 构造易诺用户池客户类型业务逻辑接口
        /// </summary>
        /// <returns>易诺用户池客户类型业务逻辑接口</returns>
        public static IBLL.PoolStructure.ICustomerType CreateInstance()
        {
            IBLL.PoolStructure.ICustomerType op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.PoolStructure.ICustomerType>();
            }
            return op;
        }

        #region ICustomerType 成员

        /// <summary>
        /// 新增客户类型
        /// </summary>
        /// <param name="model">客户类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public int AddCustomerType(EyouSoft.Model.PoolStructure.CustomerTypeInfo model)
        {
            if (model == null)
                return 0;

            return dal.AddCustomerType(model);
        }

        /// <summary>
        /// 修改客户类型
        /// </summary>
        /// <param name="model">客户类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public int UpdateCustomerType(EyouSoft.Model.PoolStructure.CustomerTypeInfo model)
        {
            if (model == null || model.TypeId <= 0)
                return 0;

            return dal.UpdateCustomerType(model);
        }

        /// <summary>
        /// 删除客户类型
        /// </summary>
        /// <param name="CustomerTypeIds">客户类型ID集合</param>
        /// <returns>1：成功；0：失败</returns>
        public int DeleteCustomerType(List<int> CustomerTypeIds)
        {
            if (CustomerTypeIds == null || CustomerTypeIds.Count <= 0)
                return 0;

            return dal.DeleteCustomerType(CustomerTypeIds);
        }

        /// <summary>
        /// 获取客户类型
        /// </summary>
        /// <param name="CustomerTypeId">客户类型Id</param>
        /// <returns>客户类型实体</returns>
        public EyouSoft.Model.PoolStructure.CustomerTypeInfo GetCustomerType(int CustomerTypeId)
        {
            if (CustomerTypeId <= 0)
                return null;

            return dal.GetCustomerType(CustomerTypeId);
        }

        /// <summary>
        /// 获取客户类型
        /// </summary>
        /// <returns>客户类型实体集合</returns>
        public IList<EyouSoft.Model.PoolStructure.CustomerTypeInfo> GetCustomerTypeList()
        {
            return dal.GetCustomerTypeList();
        }

        #endregion
    }

    #endregion
}
