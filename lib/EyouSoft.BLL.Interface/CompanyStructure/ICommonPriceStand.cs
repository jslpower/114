using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-08
    /// 描述：公用价格等级业务层接口
    /// </summary>
    public interface ICommonPriceStand
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="PriceStandName">价格等级名称</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(string PriceStandName);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string ID);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="PriceStandName">报价等级名称</param>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(string PriceStandName, string ID);
        /// <summary>
        /// 获取自定义公用价格等级列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> GetList();
        /// <summary>
        /// 获取所有公用价格等级列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> GetSysList();

    }
}
