using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
namespace UserPublicCenter.RouteManage
{
    /// <summary>
    /// 平台线路栏目
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 当前销售城市名称
        /// </summary>
        protected string thisCityName =string.Empty;
        /// <summary>
        /// 今日统计数
        /// </summary>
        protected int SumTodayRoute = 0;
        /// <summary>
        /// 全国核计数
        /// </summary>
        protected int SumAllRoute = 0;
        /// <summary>
        /// 栏目下的广告列表
        /// </summary>
        protected string strAllAdv = string.Empty;
        /// <summary>
        /// 所有地接社城市列表
        /// </summary>
        protected string strAllLocalCity = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RouteList1.IsNew = false;
            if (!Page.IsPostBack)
            {
                this.CityAndMenu1.HeadMenuIndex = 2;
                string keyWord = Utils.GetQueryStringValue("keyWord");
                GetdjList();
                GetAdvList();
                BindTheme();
            }
            this.Page.Title = string.Format(PageTitle.Route_Title, CityModel.CityName, CityModel.CityName, CityModel.CityName);
            AddMetaTag("description", string.Format(PageTitle.Scenic_Des, CityModel.CityName));
            AddMetaTag("keywords", PageTitle.Route_Keywords);

        }
        /// <summary>
        /// 目的地接社省份列表
        /// </summary>
        private void GetdjList()
        {

            //统计
            EyouSoft.Model.SystemStructure.SummaryCount SumModel = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            int CityCount = 0;
            if (SumModel != null)
            {
                SumAllRoute = SumModel.RouteVirtual + SumModel.Route;
                CityCount = SumModel.CityRouteVirtual;
            }
            SumModel = null;
            if (CityModel != null)
            {
                SumTodayRoute = CityModel.ParentTourCount + CityCount;
                thisCityName = CityModel.CityName;

            }


            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinIdList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListLocalCity();
            if (ProvinIdList != null)
            {
                foreach (EyouSoft.Model.SystemStructure.SysProvince item in ProvinIdList)
                {
                    strAllLocalCity +=
                        string.Format("<li><a href='{0}'><nobr>{1}</nobr></a></li>",
                        EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyListUrl(item.ProvinceId),
                        item.ProvinceName);
                }
            }
            ProvinIdList = null;

        }

        /// <summary>
        /// 获的线路栏目下的精品推荐广告
        /// </summary>
        private void GetAdvList()
        {
            //线路频道banner
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner1 = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.线路频道通栏banner1);
            AdvListBanner1 = null;

            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.线路频道精品推荐);
            if (AdvList != null && AdvList.Count > 0)
            {
                StringBuilder AllAdv = new StringBuilder();
                int Count = 0;
                AllAdv.Append("<tr>");
                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    AllAdv.Append("<td width=\"25%\" valign=\"top\" >");
                    AllAdv.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    AllAdv.Append("<tr><td align=\"center\">");
                    AllAdv.AppendFormat("<a href=\"{0}\" {2}><img src=\"{1}\" width=\"160\" height=\"160\" border=\"0\" /></a>", item.RedirectURL, Utils.GetNewImgUrl(item.ImgPath, 2), item.RedirectURL == Utils.EmptyLinkCode ? "" : "target=\"_blank\"");
                    AllAdv.Append("</tr></td><tr><td align=\"center\">");
                    AllAdv.AppendFormat(" <a href=\"{0}\" {2}>{1}</a>", item.RedirectURL, Utils.GetText(item.Title, 25), item.RedirectURL == Utils.EmptyLinkCode ? "" : "target=\"_blank\"");
                    AllAdv.Append("</td> </tr></table>");
                    AllAdv.Append("</td>");
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        AllAdv.Append("</tr><tr>");
                    }

                }
                AllAdv.Append("</tr>");
                strAllAdv = AllAdv.ToString();
            }
            AdvList = null;
        }

        /// <summary>
        /// 绑定主题
        /// </summary>
        private void BindTheme()
        {
            IList<EyouSoft.Model.SystemStructure.SysFieldBase> ThemeList = EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetSysFieldBaseList(EyouSoft.Model.SystemStructure.SysFieldType.线路主题);
            if (ThemeList != null && ThemeList.Count > 0)
            {
                ListItem listitem = new ListItem("全部", "0");

                Dpltheme.Items.Add(listitem);
                for (int i = 0; i < ThemeList.Count; i++)
                {
                    Dpltheme.Items.Add(new ListItem(ThemeList[i].FieldName, ThemeList[i].FieldId.ToString()));
                }
                Dpltheme.SelectedIndex = 0;
            }
            else
            {
                Dpltheme.Items.Add("无主题");
            }
        }
       
    }
}
