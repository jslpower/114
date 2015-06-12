using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-08
    /// 描述：公用价格等级数据层接口
    /// </summary>
    public interface ICommonPriceStand
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="PriceStandName">价格等级名称</param>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(string PriceStandName, EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="PriceStandName">价格等级名称</param>
        /// <param name="ID">主键编号</param>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(string PriceStandName, string ID, EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string ID, EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID);
        /// <summary>
        /// 获取公用价格等级列表
        /// </summary>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> GetList(EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID);
    }
}
