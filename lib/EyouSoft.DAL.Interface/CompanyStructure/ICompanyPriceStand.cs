using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：公司价格等级接口
    /// </summary>
    public interface ICompanyPriceStand
    {
        /// <summary>
        /// 获取指定公司的价格等级列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> GetList(string companyId);
        /// <summary>
        /// 设置公司价格等级信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="items">公用价格等级(系统基础数据)[设置公共价格等级ID和等级名称即可]信息业务实体集合</param>
        /// <returns></returns>
        bool SetCompanyPriceStand(string companyId, IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> items);
        /// <summary>
        /// 判断指定公司,指定公共的价格等级ID有无在(团队/线路价格等级)使用(true:在使用  false:未使用)
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="commonPriceStandId">公共价格等级ID</param>
        /// <returns></returns>
        bool IsUsing(string companyId, string commonPriceStandId);
    }
}
