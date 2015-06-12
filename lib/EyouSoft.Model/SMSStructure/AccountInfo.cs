using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    #region 账户信息业务实体 AccountInfo
    /// <summary>
    /// 账户信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public class AccountInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AccountInfo() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="accountMoney">账户余额</param>
        public AccountInfo(string accountId, string companyId, decimal accountMoney)
        {
            this.AccountId = accountId;
            this.CompanyId = companyId;
            this.AccountMoney = accountMoney;
        }

        /// <summary>
        /// 账户编号
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal AccountMoney { get; set; }
        /// <summary>
        /// 帐号不同通道的剩余短信条数
        /// </summary>
        public List<SMSNumber> AccountSMSNumber 
        {
            get
            {
                EyouSoft.Model.SMSStructure.SMSChannelList list = new EyouSoft.Model.SMSStructure.SMSChannelList();
                List<SMSNumber> nlist = new List<SMSNumber>();
                for (int i = 0; i < list.Count; i++)
                {
                    SMSNumber item = new SMSNumber();
                    item.ChannelName = list[i].ChannelName;
                    if (this.AccountMoney == 0)
                        item.Number = 0;
                    else
                        item.Number = Convert.ToInt32(Math.Floor(AccountMoney*100.0M/list[i].PriceOne));
                    nlist.Add(item);
                }

                return nlist;
            }
        }
        //public int AccountSMSNumber { get; set; }
    }

    /// <summary>
    /// 剩余短信条数
    /// </summary>
    /// Author:张志瑜 2010-09-20
    public class SMSNumber
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 剩余条数
        /// </summary>
        public int Number { get; set; }
    }
    #endregion

    #region 帐号明细信息 AccountDetailInfo
    /// <summary>
    /// 帐号明细信息
    /// </summary>
    public class AccountDetailInfo : AccountInfo
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// MQ
        /// </summary>
        public string MQId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int PrivinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
    }
    #endregion

    #region 消费明细汇总信息业务实体 AccountExpenseCollectInfo
    /// <summary>
    /// 消费明细汇总信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-30
    public class AccountExpenseCollectInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AccountExpenseCollectInfo() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="sentMessageCount"></param>
        /// <param name="expenseAmount"></param>
        public AccountExpenseCollectInfo(int sentMessageCount, decimal expenseAmount)
        {
            this.SentMessageCount = sentMessageCount;
            this.ExpenseAmount = expenseAmount;
        }

        /// <summary>
        /// 共发送信息数量
        /// </summary>
        public int SentMessageCount { get; set; }

        /// <summary>
        /// 共消费金额
        /// </summary>
        public decimal ExpenseAmount { get; set; }
    }
    #endregion
}
