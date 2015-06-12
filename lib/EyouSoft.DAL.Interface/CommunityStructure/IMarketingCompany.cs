using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 营销公司接口
    /// </summary>
    /// Author:zhengfj 2011-5-26
    public interface IMarketingCompany
    {
        /// <summary>
        /// 添加营销公司
        /// </summary>
        /// <param name="item">营销公司实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.CommunityStructure.MarketingCompany item);

        /// <summary>
        /// 单个/批量删除营销公司
        /// </summary>
        /// <param name="ids">编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string ids);

        /// <summary>
        /// 分页获取营销公司数据
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="provinceId">省份,null不作条件</param>
        /// <param name="cityId">城市,null不作条件</param>
        /// <param name="marketingCompany">营销公司类型</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="startDate">开始时间(注册)</param>
        /// <param name="endDate">结束时间(注册)</param>
        /// <returns>营销公司集合</returns>
        IList<EyouSoft.Model.CommunityStructure.MarketingCompany> GetMarketingCompany(int pageSize, int pageIndex,
            ref int recordCount, int? provinceId, int? cityId,EyouSoft.Model.CommunityStructure.MarketingCompanyType? marketingCompanyType,
            string companyName, DateTime? startDate, DateTime? endDate);
    }
}
