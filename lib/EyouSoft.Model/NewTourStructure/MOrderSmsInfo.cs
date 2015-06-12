//订单短信信息业务实体 汪奇志 2011-12-30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.NewTourStructure
{
    #region 订单短信信息业务实体
    /// <summary>
    /// 订单短信信息业务实体
    /// </summary>
    public class MOrderSmsInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MOrderSmsInfo() { }

        /// <summary>
        /// 接收公司编号*
        /// </summary>
        public string DSTCompanyId { get; set; }

        /// <summary>
        /// 预定公司编号
        /// </summary>
        public string BookCompanyId { get; set; }
        /// <summary>
        /// 预定公司名称
        /// </summary>
        public string BookCompanyName { get; set; }
        /// <summary>
        /// 预定联系电话
        /// </summary>
        public string BookPhone { get; set; }
        /// <summary>
        /// 预定出团时间
        /// </summary>
        public DateTime BookLTime { get; set; }
        /// <summary>
        /// 预定产品名称
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 预定产品数量
        /// </summary>
        public int BookNumber { get; set; }
        /// <summary>
        /// 订单编号*
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单类型*
        /// </summary>
        public EyouSoft.Model.NewTourStructure.OrderSource OrderType { get; set; }
    }
    #endregion

    #region 订单短信接收者信息业务实体
    /// <summary>
    /// 订单短信接收者信息业务实体
    /// </summary>
    public class MOrderSmsDstInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MOrderSmsDstInfo() { }

        /// <summary>
        /// 接收者公司编号
        /// </summary>
        public string DstComanyId { get; set; }
        /// <summary>
        /// 接收者公司名称
        /// </summary>
        public string DstCompanyName { get; set; }
        /// <summary>
        /// 接收号码集合
        /// </summary>
        public IList<string> DstPhones { get; set; }
        /// <summary>
        /// 接收者公司认证类型
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyLev DstCompanyType { get; set; }
    }
    #endregion
}
