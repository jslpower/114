using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-诚信体系的配置实体类
    /// </summary>
    public class RateConfig
    {
        #region 定义私有变量
        private double _loginscore = 0f;
        private double _holdupscore = 0f;
        private double _orderbuyscore = 0f;
        private double _certificatescore = 0f;
        private int _rateexpireday = 0;
        private double _rategoodscore = 0f;
        private double _ratemiddlescore = 0f;
        private double _ratebadscore = 0f;
        #endregion

        #region 定义公共属性
        /// <summary>
        /// 登录1次获得的分值
        /// </summary>
        public double LoginScore
        {
            get { return this._loginscore; }
            set { this._loginscore = value; }
        }

        /// <summary>
        /// 推荐1次获得的分值
        /// </summary>
        public double HoldUpScore
        {
            get { return this._holdupscore; }
            set { this._holdupscore = value; }            
        }

        /// <summary>
        /// 订购1次获得的分值
        /// </summary>
        public double OrderBuyScore
        {
            get { return this._orderbuyscore; }
            set { this._orderbuyscore = value; }
        }

        /// <summary>
        /// 上传/删除1份证书获得的分值
        /// </summary>
        public double CertificateScore
        {
            get { return this._certificatescore; }
            set { this._certificatescore = value; }
        }

        /// <summary>
        /// 评价的有效时间(单位:天)
        /// </summary>
        public int RateExpireday
        {
            get { return this._rateexpireday; }
            set { this._rateexpireday = value; }
        }
        /// <summary>
        /// 获得好评的分值
        /// </summary>
        public double RateGoodScore
        {
            get { return this._rategoodscore; }
            set { this._rategoodscore = value; }
        }
        /// <summary>
        /// 获得中评的分值
        /// </summary>
        public double RateMiddleScore
        {
            get { return this._ratemiddlescore; }
            set { this._ratemiddlescore = value; }
        }
        /// <summary>
        /// 获得差评的分值
        /// </summary>
        public double RateBadScore
        {
            get { return this._ratebadscore; }
            set { this._ratebadscore = value; }
        }
        #endregion

        #region 定义公共方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public RateConfig() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="loginscore">登录1次获得的分值</param>
        /// <param name="holdupscore">推荐1次获得的分值</param>
        /// <param name="orderbuyscore">订购1次获得的分值</param>
        /// <param name="certificatescore">上传/删除1份证书获得的分值</param>
        /// <param name="rateexpireday">评价的有效时间(单位:天)</param>
        /// <param name="rategoodscore">获得好评的分值</param>
        /// <param name="ratemiddlescore">获得中评的分值</param>
        /// <param name="ratebadscore">获得差评的分值</param>
        public RateConfig(double loginscore, double holdupscore, double orderbuyscore, double certificatescore, int rateexpireday, double rategoodscore, double ratemiddlescore, double ratebadscore) 
        { 
            this._certificatescore = certificatescore;
            this._holdupscore = holdupscore;
            this._loginscore = loginscore;
            this._orderbuyscore = orderbuyscore;
            this._ratebadscore = ratebadscore;
            this._rateexpireday = rateexpireday;
            this._rategoodscore = rategoodscore;
            this._ratemiddlescore = ratemiddlescore;
        }
        #endregion
    }
}
