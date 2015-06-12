using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 营销公司业务操作
    /// </summary>
    /// Author:zhengfj 2011-5-26
    public class MarketingCompany : EyouSoft.IBLL.CommunityStructure.IMarketingCompany
    {
        private readonly EyouSoft.IDAL.CommunityStructure.IMarketingCompany dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CommunityStructure.IMarketingCompany>();
        /// <summary>
        /// 创建营销公司业务接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.IMarketingCompany CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IMarketingCompany op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IMarketingCompany>();
            }
            return op;
        }
        #region IMarketingCompany 成员
        /// <summary>
        /// 添加营销公司
        /// </summary>
        /// <param name="item">营销公司实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.CommunityStructure.MarketingCompany item)
        {
            if (item == null)
                return false;
            return dal.Add(item);
        }

        /// <summary>
        /// 单个/批量删除营销公司
        /// </summary>
        /// <param name="ids">编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(params int[] ids)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ids.Length; i++)
            {
                sb.Append(ids[i]);
                if (i + 1 != ids.Length)
                {
                    sb.Append(",");
                }
                
            }

            return dal.Delete(sb.ToString());
        }
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
        public IList<EyouSoft.Model.CommunityStructure.MarketingCompany> GetMarketingCompany(int pageSize, int pageIndex, ref int recordCount, int? provinceId, int? cityId, EyouSoft.Model.CommunityStructure.MarketingCompanyType? marketingCompanyType, string companyName, DateTime? startDate, DateTime? endDate)
        {
            return dal.GetMarketingCompany(pageSize, pageIndex, ref recordCount, provinceId, cityId, marketingCompanyType, companyName, startDate, endDate);
        }

        #endregion
    }
}
