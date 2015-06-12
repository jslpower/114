using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间:2010-10-21
    /// 描述:机票供应商扩展信息实体基类
    /// </summary>
    [Serializable]
    public class TicketWholesalersInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketWholesalersInfo() { }

        #region 属性

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 代理级别
        /// </summary>
        public string ProxyLev
        {
            get;
            set;
        }
        /// <summary>
        /// 出票成功率
        /// </summary>
        public decimal SuccessRate
        {
            get
            {
                if (this.SubmitNum == 0)
                    return 0;

                return (decimal)this.HandleNum / (decimal)this.SubmitNum;
            }
        }
        /// <summary>
        /// ICP备案号
        /// </summary>
        public string ICPNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            get;
            set;
        }
        /// <summary>
        /// 退票自愿平均时间
        /// </summary>
        public decimal RefundAvgTime
        {
            get;
            set;
        }
        /// <summary>
        /// 退票非自愿平均时间
        /// </summary>
        public decimal NoRefundAvgTime
        {
            get;
            set;
        }
        /// <summary>
        /// 处理数(出票数)
        /// </summary>
        public int HandleNum
        {
            get;
            set;
        }
        /// <summary>
        /// 提交数(下单数 )
        /// </summary>
        public int SubmitNum
        {
            get;
            set;
        }
        /// <summary>
        /// 行程单服务价格
        /// </summary>
        public decimal ServicePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 上班时间
        /// </summary>
        public string WorkStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 下班时间
        /// </summary>
        public string WorkEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 快递费
        /// </summary>
        public decimal DeliveryPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 分成比例
        /// </summary>
        public decimal IntoRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人MQ
        /// </summary>
        public string ContactMQ
        {
            get;
            set;
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite
        {
            get;
            set;
        }
        /// <summary>
        /// 公司备注信息
        /// </summary>
        public string CompanyRemark
        {
            get;
            set;
        }
        /// <summary>
        /// OFFICE号
        /// </summary>
        public string OfficeNumber { get; set; }
        #endregion
    }

    /// <summary>
    /// 机票供应商完整扩展信息实体
    /// </summary>
    /// 鲁功源 2010-10-29
    public class TicketWholesalersAllInfo : TicketWholesalersInfo
    {
        #region 构造函数
        public TicketWholesalersAllInfo() { }
        #endregion

        #region 属性
        /// <summary>
        /// 单位附件信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyAttachInfo AttachInfo
        {
            get;
            set;
        }
        #endregion
    }
}
