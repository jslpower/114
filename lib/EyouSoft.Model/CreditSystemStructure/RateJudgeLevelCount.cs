using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-诚信体系评价等级次数(好评/中评/差评)汇总实体类
    /// </summary>
    public class RateJudgeLevelCount
    {
        #region 私有变量,公共属性
        private int _goodcount = 0;
        private int _middlecount = 0;
        private int _badcount = 0;

        /// <summary>
        /// 好评次数
        /// </summary>
        public int GoodCount
        {
            get { return this._goodcount; }
            set { this._goodcount = value; }
        }

        /// <summary>
        /// 中评次数
        /// </summary>
        public int MiddleCount
        {
            get { return this._middlecount; }
            set { this._middlecount = value; }
        }

        /// <summary>
        /// 差评次数
        /// </summary>
        public int BadCount
        {
            get { return this._badcount; }
            set { this._badcount = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateJudgeLevelCount() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="goodcount">好评次数</param>
        /// <param name="middlecount">中评次数</param>
        /// <param name="badcount">差评次数</param>
        public RateJudgeLevelCount(int goodcount, int middlecount, int badcount)
        {
            this._badcount = badcount;
            this._goodcount = goodcount;
            this._middlecount = middlecount;
        }
    }
}
