using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-诚信体系认证书汇总实体类
    /// </summary>
    public class RateCertificate
    {
        #region 私有变量
        private double _managescore = 0f;
        private string _managesrc = "";
        private double _licencescore = 0f;
        private string _licencesrc = "";
        private double _taxscore = 0f;
        private string _taxsrc = "";
        #endregion

        #region 公共属性
        /// <summary>
        /// 获得总的认证分值
        /// </summary>
        public double TotalScore
        {
            get { return this._managescore + this._licencescore + this._taxscore; }
        }
        /// <summary>
        /// 营业执照得分(为0表示无)
        /// </summary>
        public double ManageScore
        {
            get { return this._managescore; }
            set { this._managescore = value; }
        }

        /// <summary>
        /// 营业执照文件路径
        /// </summary>
        public string ManageSrc
        {
            get { return this._managesrc; }
            set { this._managesrc = value; }
        }

        /// <summary>
        /// 经营许可证得分(为0表示无)
        /// </summary>
        public double LicenceScore
        {
            get { return this._licencescore; }
            set { this._licencescore = value; }
        }

        /// <summary>
        /// 经营许可证文件路径
        /// </summary>
        public string LicenceSrc
        {
            get { return this._licencesrc; }
            set { this._licencesrc = value; }
        }

        /// <summary>
        /// 税务登记证得分(为0表示无)
        /// </summary>
        public double TaxScore
        {
            get { return this._taxscore; }
            set { this._taxscore = value; }
        }

        /// <summary>
        /// 税务登记证文件路径
        /// </summary>
        public string TaxSrc
        {
            get { return this._taxsrc; }
            set { this._taxsrc = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateCertificate() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="managescore">营业执照得分(为0表示无)</param>
        /// <param name="managessrc">营业执照文件路径</param>
        /// <param name="licencescore">经营许可证得分(为0表示无)</param>
        /// <param name="licencesrc">经营许可证文件路径</param>
        /// <param name="taxscore">税务登记证得分(为0表示无)</param>
        /// <param name="taxsrc">税务登记证文件路径</param>
        public RateCertificate(double managescore, string managessrc, double licencescore, string licencesrc, double taxscore, string taxsrc)
        {
            this._licencescore = licencescore;
            this._licencesrc = licencesrc;
            this._managescore = managescore;
            this._managesrc = managessrc;
            this._taxscore = taxscore;
            this._taxsrc = taxsrc;
        }
    }
}
