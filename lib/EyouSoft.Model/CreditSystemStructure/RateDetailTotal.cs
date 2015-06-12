using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-诚信体系汇总实体类
    /// </summary>
    public class RateDetailTotal : EyouSoft.Model.CreditSystemStructure.RateScore
    {
        #region 私有变量
        private int _activitycount = 0;
        private double _holdupscore = 0f;
        private RateCreditDetail _ratecreditdetail = new RateCreditDetail();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获得获得综合得分
        /// </summary>
        public double TotalScore
        {
            get { return base.CreditScore + base.ActiveScore + this._holdupscore; }
        }

        /// <summary>
        /// 活跃次数
        /// </summary>
        public int ActivityCount
        {
            get { return this._activitycount; }
            set { this._activitycount = value; }
        }

        /// <summary>
        /// 设置推荐得分
        /// </summary>
        public double HoldUpScore
        {
            //get { return this._holdupscore; }
            set { this._holdupscore = value; }
        }

        /// <summary>
        /// 诚信体系信用汇总明细
        /// </summary>
        public RateCreditDetail RateCreditDetail
        {
            get { return this._ratecreditdetail; }
            set { this._ratecreditdetail = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateDetailTotal() : base() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="activitycount">活跃次数</param>
        /// <param name="ratecreditdetail">诚信体系信用汇总明细</param>
        /// <param name="ishaspromises">是否存在承诺书</param>
        /// <param name="creditscore">信用分值</param>
        /// <param name="activescore">活跃分值</param>
        /// <param name="holdupcount">推荐次数</param>
        /// <param name="holdupscore">推荐得分</param>
        /// <param name="companyid">公司ID</param>
        public RateDetailTotal(int activitycount, double holdupscore, RateCreditDetail ratecreditdetail, bool ishaspromises, double creditscore, double activescore, int holdupcount, string companyid) : base(ishaspromises, creditscore, activescore, holdupcount, companyid)
        {
            this._activitycount = activitycount;
            this._holdupscore = holdupscore;
            this._ratecreditdetail = ratecreditdetail;            
        }
    }
}
