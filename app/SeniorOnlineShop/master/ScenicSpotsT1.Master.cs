/*Author:汪奇志 2010-12-08*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.master
{
    /// <summary>
    /// 景点高级网店模板1母版页
    /// </summary>
    public partial class ScenicSpotsT1 : System.Web.UI.MasterPage
    {
        #region 导航链接 static constants
        /// <summary>
        /// 首页
        /// </summary>
        private string TABLINKS_SY = "/jingqu_2";

        /// <summary>
        /// 景区动态
        /// </summary>
        private const string TABLINKS_JQDT = "/ScenicInfoList_2";
        /// <summary>
        /// 景区美图
        /// </summary>
        public string TABLINKS_JQMT = "/ScenicBeauties";

        /// <summary>
        /// 景区攻略
        /// </summary>
        private const string TABLINKS_JQGL = "/ScenicInfoList_4";
        /// <summary>
        /// 景区导游
        /// </summary>
        private const string TABLINKS_JQDY = "/ScenicInfoList_5";
        /// <summary>
        /// 联系我们
        /// </summary>
        public string TABLINKS_LXWM = "/ContactUS";
        /// <summary>
        /// 景区线路
        /// </summary>
        private const string TABLINKS_JQXL = "/ScenicInfoList_0";

        /// <summary>
        /// 景区门票
        /// </summary>
        private const string TABLINKS_JQMP = "/ScenicArea";

        #endregion

        #region Attributes
        /// <summary>
        /// 是否启用左侧的联系我们
        /// </summary>
        private bool isEnableLeftContactUs = true;
        /// <summary>
        /// 公司编号
        /// </summary>
        private string _companyid;
        /// <summary>
        /// 公司信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo _companyinfo = null;
        /// <summary>
        /// 高级网店信息
        /// </summary>
        private EyouSoft.Model.ShopStructure.HighShopCompanyInfo _companyeshopinfo = null;
        /// <summary>
        /// 是否启用左侧的Google Map
        /// </summary>
        private bool isEnableLeftGoogleMap = true;

        /// <summary>
        /// 图片服务器
        /// </summary>
        protected string ImageServerUrl;

        /// <summary>
        /// 当前所在导航TAB
        /// </summary>
        public SPOTT1TAB CTAB { get; set; }
        /// <summary>
        /// 是否启用左侧的联系我们
        /// </summary>
        public bool IsEnableLeftContactUs
        {
            get { return this.isEnableLeftContactUs; }
            set { this.isEnableLeftContactUs = value; this.phLeftContactUs.Visible = this.isEnableLeftContactUs; }
        }
        /// <summary>
        /// 是否启用左侧的Google Map
        /// </summary>
        public bool IsEnableLeftGoogleMap
        {
            get { return this.isEnableLeftGoogleMap; }
            set { this.isEnableLeftGoogleMap = value; this.phLeftGoogleMap.Visible = this.isEnableLeftGoogleMap; }
        }
        /// <summary>
        /// 公司信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfo
        {
            get { return this._companyinfo; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get { return this._companyid; }
        }
        /// <summary>
        /// 高级网店信息
        /// </summary>
        public EyouSoft.Model.ShopStructure.HighShopCompanyInfo CompanyEShopInfo
        {
            get { return this._companyeshopinfo; }
        }

        /// <summary>
        /// google 地图 key
        /// </summary>
        private string _googleMapKey = string.Empty;

        /// <summary>
        /// google 地图 key
        /// </summary>
        public string GoogleMapKey
        {
            get { return _googleMapKey; }
            set { _googleMapKey = value; }
        }

        #endregion

        #region page load
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitTabLinks();
            this.InitInfo();
            this.InitLinks();
            this.InitGoogleMap();

            this.InitTripGuide(this.rptJQDT, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区动态, 5);
            this.InitTripGuide(this.rptJQXL, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区线路, 5);
        }
        #endregion

        #region override oninit
        /// <summary>
        /// Override OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.ImageServerUrl = ImageManage.GetImagerServerUrl(1);

            //初始化公司ID
            string currentUrlPath = HttpContext.Current.Request.ServerVariables["Http_Host"];

            //如果使用独立域名访问网店，则根据域名查询所属公司ID.
            if (Domain.SeniorOnlineShop.IndexOf(currentUrlPath) == -1)
            {
                EyouSoft.Model.SystemStructure.SysCompanyDomain cDomianModel = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().
                    GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, currentUrlPath);
                if (cDomianModel != null)
                {
                    this._companyid = cDomianModel.CompanyId;
                    this.TABLINKS_SY = "/";
                }
            }

            //如果没有使用独立域名访问网店，则从QueryString集合中查找cid参数，获取公司ID
            if (string.IsNullOrEmpty(this._companyid))
            {
                this._companyid = Utils.GetQueryStringValue("cid");
            }

            //获取高级网店信息和公司信息
            this._companyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this._companyid);
            if (this._companyinfo == null)
            {
                Utils.ShowError("高级网店不存在!", "景区高级网店");
                return;
            }

            //判断是否开通高级网店                
            if (this._companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区) && !Utils.IsOpenHighShop(this._companyid))
            {
                Utils.ShowError("该公司未开通高级网店!", "景区高级网店");
                return;
            }

            this._companyeshopinfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this._companyid);

            //高级网店配置信息
            if (this._companyeshopinfo == null)
            {
                Utils.ShowError("高级网店未做任何配置，暂时不能访问!", "景区高级网店");
                return;
            }

            if (string.IsNullOrEmpty(this.CompanyEShopInfo.GoogleMapKey) || this.CompanyEShopInfo.GoogleMapKey.ToLower() == "undefined")
                GoogleMapKey =
                    EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
            else
                GoogleMapKey = this.CompanyEShopInfo.GoogleMapKey;

            base.OnInit(e);
        }
        #endregion

        #region private members
        /// <summary>
        /// 初始化导航链接
        /// </summary>
        private void InitTabLinks()
        {
            this.lnkJQDT.HRef = this.lnkJQDT1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JQDT, this.CompanyId);
            this.lnkJQDY.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JQDY, this.CompanyId);
            this.lnkJQGL.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JQGL, this.CompanyId);
            this.lnkJQMT.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JQMT, this.CompanyId);
            this.lnkLXWM.HRef = this.lnkLXWM1.HRef = Utils.GenerateShopPageUrl2(TABLINKS_LXWM, this.CompanyId);
            this.lnkSY.HRef = Utils.GenerateShopPageUrl2(this.TABLINKS_SY, this.CompanyId);
            this.lnkJQXL.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JQXL, this.CompanyId);
            lnkJQMP.HRef = Utils.GenerateShopPageUrl2(TABLINKS_JQMP, CompanyId);

            switch (this.CTAB)
            {
                case SPOTT1TAB.景区导游: this.lnkJQDY.Attributes.Add("class", "menu-on"); break;
                case SPOTT1TAB.景区动态: this.lnkJQDT.Attributes.Add("class", "menu-on"); break;
                case SPOTT1TAB.景区攻略: this.lnkJQGL.Attributes.Add("class", "menu-on"); break;
                case SPOTT1TAB.景区美图: this.lnkJQMT.Attributes.Add("class", "menu-on"); break;
                case SPOTT1TAB.联系我们: this.lnkLXWM.Attributes.Add("class", "menu-on"); break;
                case SPOTT1TAB.景区门票: this.lnkJQMP.Attributes.Add("class", "menu-on"); break;
                default: this.lnkSY.Attributes.Add("class", "menu-on"); break;
            }
        }

        /// <summary>
        /// 初始化网店头部 联系信息 版权
        /// </summary>
        private void InitInfo()
        {
            this.ltrHeadBanner.Text = Utils.GetEShopImgOrFalash(this.CompanyInfo.AttachInfo.CompanyShopBanner.ImagePath, "javascript:void(0)", 972, 233);//头部Banner
            this.imgLogo.Src = Domain.FileSystem + this.CompanyInfo.AttachInfo.CompanyShopBanner.CompanyLogo;//logo
            this.ltrContacter.Text = this.CompanyInfo.ContactInfo.ContactName;
            this.ltrTelephone.Text = this.CompanyInfo.ContactInfo.Tel;
            this.ltrFax.Text = this.CompanyInfo.ContactInfo.Fax;
            this.ltrAddress.Text = this.CompanyInfo.CompanyAddress;
            this.ltrWebsite.Text = this.CompanyInfo.WebSite;

            this.ltrMQ.Text = Utils.GetBigImgMQ(this.CompanyInfo.ContactInfo.MQ);
            this.ltrQQ.Text = Utils.GetQQ(this.CompanyInfo.ContactInfo.QQ, "在线客服QQ");

            this.ltrCopyright.Text = this.CompanyEShopInfo.ShopCopyRight;
        }

        /// <summary>
        ///  初始化友情链接
        /// </summary>
        private void InitLinks()
        {
            var links = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(this.CompanyId);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            string a = "<a href=\"{0}\">{1}</a>";
            if (links != null && links.Count > 0)
            {
                for (int i = 0; i < links.Count; i++)
                {
                    s.AppendFormat(a, links[i].LinkAddress, links[i].LinkName);

                    if (i != links.Count - 1) s.Append(" | ");
                }
            }

            this.ltrLinks.Text = s.ToString();
        }

        /// <summary>
        /// 初始化旅游资源
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="type">类型</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        private void InitTripGuide(Repeater rpt, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type, int expression)
        {
            var items = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(expression, this.CompanyId, (int)type, null);

            rpt.DataSource = items;
            rpt.DataBind();

        }

        /// <summary>
        /// 初始化Google Map
        /// </summary>
        private void InitGoogleMap()
        {
            this.GoogleMapControl1.GoogleMapKey = _googleMapKey;
            this.GoogleMapControl1.Longitude = this.CompanyEShopInfo.PositionInfo.Longitude;
            this.GoogleMapControl1.Latitude = this.CompanyEShopInfo.PositionInfo.Latitude;
            this.GoogleMapControl1.ZoomLevel = this.CompanyEShopInfo.PositionInfo.ZoomLevel;
            this.GoogleMapControl1.ShowMapWidth = 244;
            this.GoogleMapControl1.ShowMapHeight = 226;
            this.GoogleMapControl1.IsBorder = false;
            this.GoogleMapControl1.ShowTitleText = this.CompanyInfo.CompanyName;
        }
        #endregion
    }

    #region 景区高级网店模板1导航TAB
    /// <summary>
    /// 景区高级网店模板1导航TAB
    /// </summary>
    public enum SPOTT1TAB
    {
        /// <summary>
        /// 首页
        /// </summary>
        首页 = 0,
        /// <summary>
        /// 景区介绍
        /// </summary>
        景区门票,
        /// <summary>
        /// 景区动态
        /// </summary>
        景区动态,
        /// <summary>
        /// 景区美图
        /// </summary>
        景区美图,
        /// <summary>
        /// 景区攻略
        /// </summary>
        景区攻略,
        /// <summary>
        /// 景区导游
        /// </summary>
        景区导游,
        /// <summary>
        /// 联系我们
        /// </summary>
        联系我们
    }
    #endregion
}
