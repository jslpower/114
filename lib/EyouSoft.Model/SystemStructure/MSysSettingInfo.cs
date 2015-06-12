//平台配置信息 汪奇志 2011-12-30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 平台配置信息业务实体
    /// </summary>
    [Serializable]
    public class MSysSettingInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSysSettingInfo() { }

        /// <summary>
        /// 订单短信控制-发送公司类型
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyLev[] OrderSmsCompanyTypes { get; set; }
        /// <summary>
        /// 订单短信控制-发送订单类型
        /// </summary>
        public EyouSoft.Model.NewTourStructure.OrderSource[] OrderSmsOrderTypes { get; set; }
        /// <summary>
        /// 订单短信控制-发送内容模板
        /// </summary>
        public string OrderSmsTemplate { get; set; }
        /// <summary>
        /// 订单短信控制-发送短信公司编号
        /// </summary>
        public string OrderSmsCompanyId { get; set; }
        /// <summary>
        /// 订单短信控制-发送短信公司的用户编号
        /// </summary>
        public string OrderSmsUserId { get; set; }
        /// <summary>
        /// 订单短信控制-是否启用
        /// </summary>
        public bool OrderSmsIsEnable { get; set; }
        /// <summary>
        /// 订单短信控制-发送短信通道索引
        /// </summary>
        public byte OrderSmsChannelIndex { get; set; }
    }
}
