using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-02
    /// 描述：待新增公用价格等级业务逻辑接口
    /// </summary>
    public interface ICommonPriceStandAdd
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">待新增公用价格等级实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.CompanyStructure.CommonPriceStandAdd model);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CommonPriceStandAdd> GetList();       
    }
}
