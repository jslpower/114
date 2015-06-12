using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 附件图片基类
    /// </summary>
    /// 创建人：张志瑜 2010-06-24
    public class AttachImageBase
    {
        private string _imagepath = "";
        private string _thumbpath = "";
        private string _imagelink = "";

        /// <summary>
        /// 图片的文件路径
        /// </summary>
        public virtual string ImagePath { get { return this._imagepath; } set { this._imagepath = value; } }
        /// <summary>
        /// 缩略图的文件路径
        /// </summary>
        public virtual string ThumbPath { get { return this._thumbpath; } set { this._thumbpath = value; } }
        /// <summary>
        /// 点击图片的链接地址
        /// </summary>
        public virtual string ImageLink { get { return this._imagelink; } set { this._imagelink = value; } }

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public AttachImageBase() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="imagepath">图片的文件路径</param>
        /// <param name="thumbpath">缩略图的文件路径</param>
        /// <param name="imagelink">点击图片的链接地址</param>
        public AttachImageBase(string imagepath, string thumbpath, string imagelink)
        {
            this._imagepath = imagepath;
            this._thumbpath = thumbpath;
            this._imagelink = imagelink;
        }
    }

    /// <summary>
    /// 公司宣传图片
    /// </summary>
    public class CompanyImg : AttachImageBase { }

    /// <summary>
    /// 公司企业logo
    /// </summary>
    public class CompanyLogo : AttachImageBase { }

    /// <summary>
    /// 公司MQ广告
    /// </summary>
    public class CompanyMQAdv : AttachImageBase { }

    /// <summary>
    /// 公司高级网店头部
    /// </summary>
    public class CompanyShopBanner
    {
        private ShopBannerType _bannertype = ShopBannerType.Personalize;
        /// <summary>
        /// 公司logo
        /// </summary>
        public string CompanyLogo { get; set; }
        /// <summary>
        /// 公司高级网店个性化Banner
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 公司高级网店头部背景
        /// </summary>
        public string BannerBackground { get; set; }
        /// <summary>
        /// 高级网店头部类型(默认为个性化Banner)
        /// </summary>
        public ShopBannerType BannerType { get { return this._bannertype; } set { this._bannertype = value; } }
    }

    /// <summary>
    /// 高级网店头部
    /// </summary>
    public enum ShopBannerType
    {
        /// <summary>
        /// 默认(logo+背景) = 0
        /// </summary>
        Default = 0,

        /// <summary>
        /// 个性化Banner = 1
        /// </summary>
        Personalize = 1
    }

    /// <summary>
    /// 单位附件信息(宣传图片,企业LOGO,营业执照,经营许可证,税务登记证,承诺书,企业名片等)
    /// </summary>
    /// 创建人：张志瑜 2010-05-27
    [Serializable]
    public class CompanyAttachInfo
    {
        private string _companyid = "";
        private string _commitmentimg = "";
        private string _companysignet = "";

        /// <summary>
        /// 营业证书
        /// </summary>
        private BusinessCertif _businesscertif = new BusinessCertif();
        /// <summary>
        /// 公司名片
        /// </summary>
        private CardInfo _companycard = new CardInfo();
        /// <summary>
        /// 公司宣传图片
        /// </summary>
        private CompanyImg _companyimg = new CompanyImg();
        /// <summary>
        /// 公司企业logo
        /// </summary>
        private CompanyLogo _companylogo = new CompanyLogo();
        /// <summary>
        /// 公司企业logo
        /// </summary>
        private CompanyMQAdv _companymqadv = new CompanyMQAdv();
        /// <summary>
        /// 公司高级网店头部
        /// </summary>
        private CompanyShopBanner _companyshopbanner = new CompanyShopBanner();

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyAttachInfo() { }


        #region 属性
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get { return this._companyid; } set { this._companyid = value; } }
        /// <summary>
        /// 公司宣传图片
        /// </summary>
        public CompanyImg CompanyImg { get { return this._companyimg; } set { this._companyimg = value; } }
        /// <summary>
        /// 公司企业logo
        /// </summary>
        public CompanyLogo CompanyLogo { get { return this._companylogo; } set { this._companylogo = value; } }
        /// <summary>
        /// 营业证书
        /// </summary>
        public BusinessCertif BusinessCertif { get { return this._businesscertif; } set { this._businesscertif = value; } }
        /// <summary>
        /// 承诺书
        /// </summary>
        public string CommitmentImg { get { return this._commitmentimg; } set { this._commitmentimg = value; } }
        /// <summary>
        /// 公司名片
        /// </summary>
        public CardInfo CompanyCard { get { return this._companycard; } set { this._companycard = value; } }
        /// <summary>
        /// 公司图章
        /// </summary>
        public string CompanySignet { get { return this._companysignet; } set { this._companysignet = value; } }
        /// <summary>
        /// 公司MQ广告
        /// </summary>
        public CompanyMQAdv CompanyMQAdv { get { return this._companymqadv; } set { this._companymqadv = value; } }
        /// <summary>
        /// 公司高级网店头部
        /// </summary>
        public CompanyShopBanner CompanyShopBanner { get { return this._companyshopbanner; } set { this._companyshopbanner = value; } }

        /// <summary>
        /// 铜牌(即为营业证书中的经营许可证)
        /// </summary>
        public string Bronze { get { return this._businesscertif.BusinessCertImg; } set { this._businesscertif.BusinessCertImg = value; } }

        /// <summary>
        /// 行业奖项
        /// </summary>
        public string TradeAward { get; set; }

        /// <summary>
        /// 新版公司宣传照片（2011-12-20线路改版新增）
        /// </summary>
        public IList<CompanyPublicityPhoto> CompanyPublicityPhoto { get; set; }

        #endregion
    }

    /// <summary>
    /// 营业证书信息实体类
    /// </summary>
    public class BusinessCertif
    {
        private string _licenceimg = "";
        private string _businesscertimg = "";
        private string _taxregimg = "";

        /// <summary>
        /// 营业执照
        /// </summary>
        public string LicenceImg { get { return this._licenceimg; } set { this._licenceimg = value; } }
        /// <summary>
        /// 经营许可证(若为民航代理人许可证，即为铜牌)
        /// </summary>
        public string BusinessCertImg { get { return this._businesscertimg; } set { this._businesscertimg = value; } }
        /// <summary>
        /// 税务登记证
        /// </summary>
        public string TaxRegImg { get { return this._taxregimg; } set { this._taxregimg = value; } }

        /// <summary>
        /// 授权证书
        /// </summary>
        public string WarrantImg { get; set; }

        /// <summary>
        /// 负责人身份证
        /// </summary>
        public string PersonCardImg { get; set; }
    }

    /// <summary>
    /// 名片信息实体类
    /// </summary>
    public class CardInfo : AttachImageBase { }

    /// <summary>
    /// 产品展示信息实体类
    /// </summary>
    public class ProductInfo : AttachImageBase
    {
        private string _productremark = "";
        /// <summary>
        /// 产品编号
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProductRemark { get { return this._productremark; } set { this._productremark = value; } }

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public ProductInfo() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="productimgpath">产品图片路径</param>
        /// <param name="productimglink">产品图片点击后的链接地址</param>
        /// <param name="productremark">产品描述</param>
        public ProductInfo(string productimgpath, string productimglink, string productremark)
        {
            this.ImagePath = productimgpath;
            this.ImageLink = productimglink;
            this._productremark = productremark;

        }
    }

    #region 2011-12-20 线路改版新加实体

    /// <summary>
    /// 公司宣传照片
    /// </summary>
    public class CompanyPublicityPhoto : AttachImageBase
    {
        /// <summary>
        /// 宣传照片索引
        /// </summary>
        public int PhotoIndex { get; set; }
    }

    #endregion
}
