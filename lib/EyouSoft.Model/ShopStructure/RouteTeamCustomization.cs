using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ShopStructure
{
    /// <summary>
    /// 创建人：luofx 2010-11-09
    /// 描述：高级网店-团队制定信息实体
    /// </summary>
    public class RouteTeamCustomization
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 计划日期
        /// </summary>
        public DateTime PlanDate { get; set; }
        /// <summary>
        /// 计划人数 
        /// </summary>
        public int PlanPeopleNum { get; set; }
        /// <summary>
        /// 行程要求
        /// </summary>
        public string TravelContent { get; set; }
        /// <summary>
        /// 住宿要求
        /// </summary>
        public string ResideContent { get; set; }
        /// <summary>
        /// 用餐要求
        /// </summary>
        public string DinnerContent { get; set; }
        /// <summary>
        /// 用车要求
        /// </summary>
        public string CarContent { get; set; }
        /// <summary>
        /// 导游要求
        /// </summary>
        public string GuideContent { get; set; }
        /// <summary>
        /// 购物要求
        /// </summary>
        public string ShoppingInfo { get; set; }
        /// <summary>
        /// 其它个性要求
        /// </summary>
        public string OtherContent { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ContactCompanyName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
}
