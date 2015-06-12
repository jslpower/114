using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 营销公司
    /// </summary>
    /// Author:zhengfj 2011-5-26
    public class MarketingCompany
    {
        public MarketingCompany() { }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactWay { get; set; }

        /// <summary>
        /// 企业碰到的问题 
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 营销公司类型
        /// </summary>
        public MarketingCompanyType MarketingCompanyType { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterDate { get; set; }
    }

    /// <summary>
    /// 营销公司枚举类型
    /// </summary>
    /// Author:zhengfj 2011-5-26
    public enum MarketingCompanyType
    {
        /// <summary>
        /// 宣传企业
        /// </summary>
        宣传企业 = 1,
        /// <summary>
        /// 同行订单
        /// </summary>
        同行订单 = 2,
        /// <summary>
        /// 在线销售平台
        /// </summary>
        在线销售平台 = 3
    }
}
