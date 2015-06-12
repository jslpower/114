using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-公司的信用体系分值业务实体
    /// </summary>
    public class RateScore
    {
        #region 定义私有变量
        /// <summary>
        /// 是否存在承诺书
        /// </summary>
        private bool _ishaspromises = false;

        /// <summary>
        /// 信用分值
        /// </summary>
        private double _creditscore = 0f;

        /// <summary>
        /// 活跃分值
        /// </summary>
        private double _activescore = 0f;

        /// <summary>
        /// 推荐次数
        /// </summary>
        private int _holdupcount = 0;

        /// <summary>
        /// 公司ID
        /// </summary>
        private string _companyid = "";
        #endregion

        #region 定义公共属性
        /// <summary>
        /// 获取/设置是否存在已审核通过的承诺书  默认为false
        /// </summary>
        public bool IsHasPromises
        {
            get { return this._ishaspromises; }
            set { this._ishaspromises = value; }
        }

        /// <summary>
        /// 获取/设置信用分值
        /// </summary>
        public double CreditScore
        {
            get { return this._creditscore; }
            set { this._creditscore = value; }
        }

        /// <summary>
        /// 获取/设置活跃分值
        /// </summary>
        public double ActiveScore
        {
            get { return this._activescore; }
            set { this._activescore = value; }
        }

        /// <summary>
        /// 获取/设置推荐次数
        /// </summary>
        public int HoldUpCount
        {
            get { return this._holdupcount; }
            set { this._holdupcount = value; }
        }

        /// <summary>
        /// 获取/设置公司ID
        /// </summary>
        public string CompanyId
        {
            get { return this._companyid; }
            set { this._companyid = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateScore() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ishaspromises">是否存在承诺书</param>
        /// <param name="creditscore">信用分值</param>
        /// <param name="activescore">活跃分值</param>
        /// <param name="holdupcount">推荐次数</param>
        /// <param name="companyid">公司ID</param>
        public RateScore(bool ishaspromises, double creditscore, double activescore, int holdupcount, string companyid)
        {
            this._ishaspromises = ishaspromises;
            this._creditscore = creditscore;
            this._activescore = activescore;
            this._holdupcount = holdupcount;
            this._companyid = companyid;
        }
    }
}
