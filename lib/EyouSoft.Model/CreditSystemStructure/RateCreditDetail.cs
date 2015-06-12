using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-诚信体系信用汇总,明细实体类
    /// </summary>
    public class RateCreditDetail : RateJudgeLevelCount
    {
        #region 私有变量
        private RateCertificate _ratecertificate = new RateCertificate();
        private double _judgescore = 0f;
        private double _buyscore = 0f;
        private int _buycount = 0;
        private double _servicequalitynum = 0;
        private double _pricequalitynum = 0;
        private double _travelplannum = 0;
        #endregion

        #region 公共属性
        /// <summary>
        /// 诚信体系证书汇总
        /// </summary>
        public RateCertificate RateCertificate
        {
            get { return this._ratecertificate; }
            set { this._ratecertificate = value; }
        }

        /// <summary>
        /// 获得总的交易和评价分值
        /// </summary>
        public double TotalBuyJudegScore
        {
            get { return this._buyscore + this._judgescore; }
        }

        /// <summary>
        /// 设置评价所得分值
        /// </summary>
        public double JudgeScore
        {
            get { return this._judgescore; }
            set { this._judgescore = value; }
        }

        /// <summary>
        /// 设置成功交易所得分值
        /// </summary>
        public double BuyScore
        {
            get { return this._buyscore; }
            set { this._buyscore = value; }
        }

        /// <summary>
        /// 成功交易次数
        /// </summary>
        public int BuyCount
        {
            get { return this._buycount; }
            set { this._buycount = value; }
        }

        /// <summary>
        /// 设置服务品质总的星星个数
        /// </summary>
        public double ServiceQualityNum
        {
            //get { return this._servicequalitynum; }
            set { this._servicequalitynum = value; }
        }

        /// <summary>
        /// 获得服务品质平均的星星个数
        /// </summary>
        public double ServiceQualityAverageNum
        {
            get
            {
                int totalcount = base.GoodCount + base.MiddleCount + base.BadCount;
                return Math.Round(this._servicequalitynum * 1.0 / totalcount * 1.0, MidpointRounding.ToEven);
            }
        }

        /// <summary>
        /// 设置性价比总的星星个数
        /// </summary>
        public double PriceQualityNum
        {
            //get { return this._pricequalitynum; }
            set { this._pricequalitynum = value; }
        }

        /// <summary>
        /// 获得性价比平均的星星个数
        /// </summary>
        public double PriceQualityAverageNum
        {
            get
            {
                int totalcount = base.GoodCount + base.MiddleCount + base.BadCount;
                return Math.Round(this._pricequalitynum * 1.0 / totalcount * 1.0, MidpointRounding.ToEven);
            }
        }

        /// <summary>
        /// 设置旅游内容安排总的星星个数
        /// </summary>
        public double TravelPlanNum
        {
            //get { return this._travelplannum; }
            set { this._travelplannum = value; }
        }

        /// <summary>
        /// 获得旅游内容安排平均的星星个数
        /// </summary>
        public double TravelPlanAverageNum
        {
            get
            {
                int totalcount = base.GoodCount + base.MiddleCount + base.BadCount;
                return Math.Round(this._travelplannum * 1.0 / totalcount * 1.0, MidpointRounding.ToEven);
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateCreditDetail() : base() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ratecertificate">诚信体系证书汇总</param>
        /// <param name="judgescore">评价所得分值</param>
        /// <param name="buyscore">成功交易所得分值</param>
        /// <param name="buycount">成功交易次数</param>
        /// <param name="servicequalitynum">服务品质总的星星个数</param>
        /// <param name="pricequalitynum">性价比总的星星个数</param>
        /// <param name="travelplannum">旅游内容安排总的星星个数</param>
        /// <param name="goodcount">好评次数</param>
        /// <param name="middlecount">中评次数</param>
        /// <param name="badcount">差评次数</param>
        public RateCreditDetail(RateCertificate ratecertificate, double judgescore, double buyscore, int buycount, int servicequalitynum, int pricequalitynum, int travelplannum, int goodcount, int middlecount, int badcount)
            : base(goodcount, middlecount, badcount)
        {
            this._buycount = buycount;
            this._buyscore = buyscore;
            this._judgescore = judgescore;
            this._pricequalitynum = pricequalitynum;
            this._ratecertificate = ratecertificate;
            this._servicequalitynum = servicequalitynum;
            this._travelplannum = travelplannum;
        }
    }
}
