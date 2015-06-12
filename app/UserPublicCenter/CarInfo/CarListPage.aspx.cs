using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common.Function;


namespace UserPublicCenter.CarInfo
{
    /// <summary>
    /// 页面功能：车队——车队列表页
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class CarListPage : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        protected bool isOpenHighShop = false;
        protected string GoToUrl = "";
        private int CurrencyPage = 1;   //当前页
        private int intPageSize = 10;   //每页显示的记录数
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddMetaTag("description",string.Format(PageTitle.Car_Des,CityModel.CityName));
                AddMetaTag("keywords", PageTitle.Car_Keywords);
                this.Page.Title = string.Format(PageTitle.Car_Title, CityModel.CityName);
                //绑定页面上方的六个广告位图片信息
                BindPicAdvInfo();
                BindListInfo();
            }
            this.CityAndMenu1.HeadMenuIndex = 6;
        }
        #endregion

        #region 处理相关绑定信息
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        protected void BindListInfo()
        {
            int intRecordCount = 0;
            int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceID"], 0);
            int S_CityID = Utils.GetInt(Request.QueryString["S_CityID"], 0);
            if (ProvinceId == 0)
            {
                //页面第一次加载的时候会根据销售城市绑定列表数据
                EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
                S_CityID = CityId;
                if (cityModel != null)
                {
                    ProvinceId = cityModel.ProvinceId;
                }
            }
            string CompanyName = Utils.InputText(Server.UrlDecode(Request.QueryString["CompanyName"])).Trim();

            #region 初始化查询条件
            this.ProvinceAndCityList1.SetProvinceId = Utils.GetInt(Request.QueryString["ProvinceID"], 0);
            this.ProvinceAndCityList1.SetCityId = Utils.GetInt(Request.QueryString["S_CityID"], 0); ;
            this.txt_CompanyName.Text = CompanyName;
            #endregion

            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            //实例化类的对象
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.SupplierInfo> ModelList = Ibll.GetList(intPageSize, CurrencyPage, ref intRecordCount, CompanyName, ProvinceId, S_CityID, EyouSoft.Model.CompanyStructure.BusinessProperties.车队);
            if (ModelList != null && ModelList.Count > 0)
            {
                //绑定数据源
                this.rpt_CarListInfo.DataSource = ModelList;
                this.rpt_CarListInfo.DataBind();
                //绑定分页控件
                if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
                {
                    this.ExporPageInfoSelect1.Placeholder = "#PageIndex#";
                    this.ExporPageInfoSelect1.IsUrlRewrite = true;
                    this.ExporPageInfoSelect1.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
                }
                else
                {
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                }
                this.ExporPageInfoSelect1.intPageSize = intPageSize;//每页显示记录数
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";                
                
                this.ExporPageInfoSelect1.LinkType = 3;

            }
            //释放资源
            ModelList = null;
            Ibll = null;
        }
        /// <summary>
        ///绑定页面上方的六个广告图片
        /// </summary>
        /// <returns></returns>
        protected void BindPicAdvInfo()
        {
            EyouSoft.IBLL.AdvStructure.IAdv iBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = iBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.车队频道精品推荐图文);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                this.dal_PicAdvList.DataSource = AdvListBanner;
                this.dal_PicAdvList.DataBind();
            }
            AdvListBanner = null;
            iBll = null;
        }
        /// <summary>
        ///  获取省份城市名称
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        protected string GetProCityName(int CityId)
        {
            string returnVal = "";
            //实例化类的对象
            EyouSoft.IBLL.SystemStructure.ISysCity cbll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            EyouSoft.Model.SystemStructure.SysCity cModel = cbll.GetSysCityModel(CityId);
            if (cModel != null)
            {
                returnVal = cModel.ProvinceName + " " + cModel.CityName;
            }
            //释放资源
            cModel = null;
            cbll = null;
            return returnVal;
        }
        //业务优势
        protected string GetContentRemark(string content, int WordsCount)
        {
            string returnVal = "";
            if (!string.IsNullOrEmpty(content))
            {
                returnVal = Utils.GetText(content, WordsCount, true);
            }

            return returnVal;
        }
        protected string GetContentRemarkTwo(string content, int WordsCount)
        {
            string returnVal = "";
            if (!string.IsNullOrEmpty(content))
            {
                returnVal = Utils.GetText(Utils.InputText(content), WordsCount, true);
            }

            return returnVal;
        }
        //给图片加连接
        protected string GetImageLink(string CompanyID, string imgUrl)
        {
            if (imgUrl == "")
            {
                imgUrl = Utils.NoLogoImage92_84;
            }
            else
            {
                imgUrl = Domain.FileSystem + imgUrl;
            }
            string returnVal = "";
            EyouSoft.Model.CompanyStructure.CompanyState CompanyStateModel = new EyouSoft.Model.CompanyStructure.CompanyState();
            GoToUrl = Utils.GetCompanyDomain(CompanyID, out CompanyStateModel, out isOpenHighShop, EyouSoft.Model.CompanyStructure.CompanyType.车队, CityId);
            //如果此公司申请了开通了高级网店则连接到高级网店，否则连接到普通网店
            returnVal = string.Format("<a href=\"{0}\" target=\"_blank\"><img src=\"" + imgUrl + "\" width=\"92\" height=\"84\" border=\"0\" /></a>", GoToUrl);
            CompanyStateModel = null;
            return returnVal;
        }
        //当公司开通了高级网店，则绑定联系信息
        protected string GetContentInfo(object ContactInfo)
        {
            string returnVal = "";
            if (isOpenHighShop)
            { //如果此公司申请了开通了高级网店则显示该公司的联系信息，否则隐藏
                if (ContactInfo != null)
                {
                    EyouSoft.Model.CompanyStructure.ContactPersonInfo model = (EyouSoft.Model.CompanyStructure.ContactPersonInfo)ContactInfo;
                    if (model != null)
                    {
                        returnVal = string.Format(" 联系人：{0} 电&nbsp;&nbsp;话：<span class=\"hong\">{1}</span> 传&nbsp;&nbsp;真：<span class=\"hong\">{2}</span>", model.ContactName, model.Tel, model.Fax);
                    }
                    //释放资源
                    model = null;
                }
            }
            return returnVal;
        }
        /// <summary>
        /// 显示页面上方的六个广告图片
        /// </summary>
        /// <returns></returns>
        protected string ShowPicAdvInfo(string RedirectURL, string ImagPath)
        {
            string PicImgUrl = "";
            if (Utils.EmptyLinkCode == RedirectURL)
            {
                PicImgUrl = string.Format("<image src='{0}' width='144' height='92' border='0'>", Domain.FileSystem + ImagPath);
            }
            else
            {
                PicImgUrl = string.Format("<a href='{0}' target='_blank'><image src='{1}' width='144' height='92' border='0'></a>", RedirectURL, Domain.FileSystem + ImagPath);
            }
            return PicImgUrl;
        }
        /// <summary>
        /// 显示图片文字链接
        /// </summary>
        /// <returns></returns>
        protected string ShowTitleAdvInfo(string Title, string RedirectURL)
        {
            string PicImgUrl = "";
            if (Utils.EmptyLinkCode == RedirectURL)
            {
                PicImgUrl = string.Format("<a href='{0}' title='{2}' target='_self'>{1}</a>", RedirectURL, Utils.GetText(Title, 11, true), Title);
            }
            else
            {
                PicImgUrl = string.Format("<a href='{0}' title='{2}' target='_blank'>{1}</a>", RedirectURL, Utils.GetText(Title, 11, true), Title);
            }
            return PicImgUrl;
        }
        #endregion
    }
}
