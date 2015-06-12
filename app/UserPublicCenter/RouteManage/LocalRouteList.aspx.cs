using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;

namespace UserPublicCenter.RouteManage
{
    /// <summary>
    /// 地接社线路列表
    /// 罗丽娥  2010-07-23
    /// --------------------------
    /// 修改人：张新兵，修改时间：2011-3-8
    /// 修改内容：修改页面标题为：
    /// （公司名称）_旅游线路_联系方式,
    /// （公司名称）由 地接社公司名称 进行 格式化
    /// </summary>
    public partial class LocalRouteList : EyouSoft.Common.Control.FrontPage
    {
        private string CompanyID = "0";
        private int intPageSize = 10, CurrencyPage = 1;
        protected string strDomain = string.Empty;
        protected string CompanyRemark = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = Utils.InputText(Request.QueryString["cid"]);
            strDomain = Domain.UserBackCenter;
            if (!Page.IsPostBack)
            {
                InitBasicData();
                InitRouteList();
            }
        }

        #region 初始化公司基础数据
        private void InitBasicData()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyID);
            if (model == null || model.StateMore.IsCheck == false || model.StateMore.IsEnabled == false || model.StateMore.IsDelete)
            {
                Utils.ShowError("该网店不存在！请浏览其他页面。", "Tour");
                return;
            }

            if (model.AttachInfo != null)
                imgLogo.Src = Utils.GetNewImgUrl(model.AttachInfo.CompanyLogo.ImagePath, 3);
            this.ltrCompanyName.Text = model.CompanyName;
            this.ltrLicense.Text = model.License;
            CompanyRemark = model.Remark;
            if (model.Remark.Length > 130)
            {
                this.ltrRemark.Text = "<span id=\"spanRemark\">" + Utils.InputText(Utils.GetText(model.Remark, 130, false)) + "【<a href=\"#\">查看详细</a>】</span>";
                this.ltrRemark1.Text = "<span id=\"spanRemark1\">" + Utils.InputText(model.Remark) + "【<a href=\"#\">隐藏详细</a>】</span>";
            }
            else
            {
                this.ltrRemark.Text = Utils.InputText(model.Remark);
            }
            this.ltrCompanyName1.Text = model.CompanyName;
            this.ltrLicense1.Text = model.License;
            this.ltrBrandName.Text = model.CompanyBrand;
            if (model.ContactInfo != null)
            {
                this.ltrContactName.Text = model.ContactInfo.ContactName + Utils.GetMQ(model.ContactInfo.MQ);
                this.ltrMobile.Text = GetMoible(model.ContactInfo.Mobile);//手机号码中间4位显示*
                this.ltrTel.Text = model.ContactInfo.Tel;
                this.ltrFax.Text = model.ContactInfo.Fax;
            }
            this.ltrAddress.Text = model.CompanyAddress;

            //初始化页面标题
            this.Page.Title = string.Format("{0}_旅游线路_联系方式", model.CompanyName);

            model = null;
        }
        #endregion

        #region 获取公司下面的线路信息
        private void InitRouteList()
        {
            int intRecordCount = 0;
            CurrencyPage = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            IList<EyouSoft.Model.TourStructure.RouteBasicInfo> list = bll.GetLocaRoutes(intPageSize, CurrencyPage, ref intRecordCount, CompanyID, string.Empty, string.Empty, 0, string.Empty, null, null);
            if (intRecordCount > 0)
            {
                this.rptRouteList.DataSource = list;
                this.rptRouteList.DataBind();
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            }
            else
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.pnlNoData.Visible = true;
            }
            list = null;
            bll = null;
        }
        #endregion

        #region  手机号码中间四位显示*
        /// <summary>
        /// 手机号码中间四位显示*
        /// </summary>
        /// <param name="moible">手机号</param>
        /// <returns></returns>
        protected string GetMoible(string moible)
        {

            if (!string.IsNullOrEmpty(moible))
            {
                if (Regex.IsMatch(moible, @"^(13|15|18|14)\d{9}$"))
                {
                    return moible.Substring(0, 3) + "****" + moible.Substring(7, 4);
                }
            }
            return string.Empty;
        }
        #endregion
    }
}
