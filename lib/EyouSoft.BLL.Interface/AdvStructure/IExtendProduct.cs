using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.AdvStructure
{
    /// <summary>
    /// 描述:首页推荐产品业务接口类
    /// 修改记录:
    /// 1 2011-05-10 曹胡生 创建
    /// </summary>
    public interface IExtendProduct
    {
        /// <summary>
        /// 得到所有首页推荐产品
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> GetExtendProductList(int pageSize, int pageIndex, ref int recordCount);

        /// <summary>
        /// 得到某城市下的所有推荐产品
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> GetExtendProductList(int CityId);

        /// <summary>
        /// 修改推荐产品
        /// </summary>
        /// <param name="ExtendProductInfo"></param>
        /// <returns></returns>
        bool UpdateExtendProduct(EyouSoft.Model.AdvStructure.ExtendProductInfo ExtendProductInfo);

        /// <summary>
        /// 批量修改推荐产品
        /// </summary>
        /// <param name="ExtendProductInfoList"></param>
        /// <returns></returns>
        bool UpdateExtendProduct(IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> ExtendProductInfoList);

        /// <summary>
        /// 添加推荐产品
        /// </summary>
        /// <param name="ExtendProductInfo"></param>
        /// <returns></returns>
        bool AddExtendProduct(EyouSoft.Model.AdvStructure.ExtendProductInfo ExtendProductInfo);

        /// <summary>
        /// 批量添加推荐产品
        /// </summary>
        /// <param name="ExtendProductInfoList"></param>
        /// <returns></returns>
        bool AddExtendProduct(IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> ExtendProductInfoList);

        /// <summary>
        /// 删除推荐产品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool DelExtendProduct(int ID);

        /// <summary>
        /// 判断某个产品是否推荐
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="ProdcutID"></param>
        /// <returns></returns>
        bool IsZixst(int CityId, string ProdcutID);
    }
}
