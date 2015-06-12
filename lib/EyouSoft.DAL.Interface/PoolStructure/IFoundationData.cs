using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.PoolStructure
{
    //Author:周文超 2010-11-26

    #region 易诺用户池适用产品类型数据接口

    /// <summary>
    /// 易诺用户池适用产品类型数据接口
    /// </summary>
    public interface ISuitProduct
    {
        /// <summary>
        /// 新增适用产品类型
        /// </summary>
        /// <param name="model">适用产品类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        int AddSuitProduct(EyouSoft.Model.PoolStructure.SuitProductInfo model);

        /// <summary>
        /// 修改适用产品类型
        /// </summary>
        /// <param name="model">适用产品类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        int UpdateSuitProduct(EyouSoft.Model.PoolStructure.SuitProductInfo model);

        /// <summary>
        /// 删除适用产品类型
        /// </summary>
        /// <param name="SuitProductIds">适用产品类型ID集合</param>
        /// <returns>1：成功；0：失败</returns>
        int DeleteSuitProduct(List<int> SuitProductIds);

        /// <summary>
        /// 根据ID获取适用产品类型
        /// </summary>
        /// <param name="SuitProductId">适用产品类型Id</param>
        /// <returns>适用产品类型实体</returns>
        EyouSoft.Model.PoolStructure.SuitProductInfo GetSuitProduct(int SuitProductId);

        /// <summary>
        /// 获取适用产品类型
        /// </summary>
        /// <returns>适用产品类型实体集合</returns>
        IList<EyouSoft.Model.PoolStructure.SuitProductInfo> GetSuitProductList();
    }

    #endregion

    #region 易诺用户池客户类型数据接口

    /// <summary>
    /// 易诺用户池客户类型数据接口
    /// </summary>
    public interface ICustomerType
    {
        /// <summary>
        /// 新增客户类型
        /// </summary>
        /// <param name="model">客户类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        int AddCustomerType(EyouSoft.Model.PoolStructure.CustomerTypeInfo model);

        /// <summary>
        /// 修改客户类型
        /// </summary>
        /// <param name="model">客户类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        int UpdateCustomerType(EyouSoft.Model.PoolStructure.CustomerTypeInfo model);

        /// <summary>
        /// 删除客户类型
        /// </summary>
        /// <param name="CustomerTypeIds">客户类型ID集合</param>
        /// <returns>1：成功；0：失败</returns>
        int DeleteCustomerType(List<int> CustomerTypeIds);

        /// <summary>
        /// 获取客户类型
        /// </summary>
        /// <param name="CustomerTypeId">客户类型Id</param>
        /// <returns>客户类型实体</returns>
        EyouSoft.Model.PoolStructure.CustomerTypeInfo GetCustomerType(int CustomerTypeId);
        
        /// <summary>
        /// 获取客户类型
        /// </summary>
        /// <returns>客户类型实体集合</returns>
        IList<EyouSoft.Model.PoolStructure.CustomerTypeInfo> GetCustomerTypeList();
    }

    #endregion
}
