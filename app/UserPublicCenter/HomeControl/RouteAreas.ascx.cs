using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace UserPublicCenter.HomeControl
{
    public partial class RouteAreas : System.Web.UI.UserControl
    {
        protected string inAreaHtml;//国内长线区域
        protected string outAreaHtml;//国际长线区域
        protected string sideAreaHtml;//周边线路区域
        protected string ImageServerPath;
        protected StringBuilder InremmCompany = new StringBuilder();//国内品牌推荐
        protected StringBuilder outremmCompany = new StringBuilder();//国际品牌推荐
        protected StringBuilder sideremmCompany = new StringBuilder();//周边品牌推荐
        protected StringBuilder AdvImage1 = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsShow)
            {
                ImageServerPath = (this.Page as EyouSoft.Common.Control.FrontPage).ImageServerUrl;
                BindRouteArea();
                GetAdvImg();
            }
        }
        public int CityId;
        public bool IsShow;
        #region  绑定散拼线路区域
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        /// <returns></returns>
        protected void BindRouteArea()
        {
            EyouSoft.Model.SystemStructure.SysCity model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            EyouSoft.IBLL.AdvStructure.IAdv advBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            #region 国内长线
            IList<EyouSoft.Model.AdvStructure.AdvInfo> inAdvList = advBll.GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.国内长线5张logo图片);
            IEnumerable<EyouSoft.Model.SystemStructure.SysCityArea> inAreaList = model.CityAreaControls.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线 && i.IsDefaultShow == true).OrderBy(p => p.SortId);
            inAreaHtml = GetAreaHtml(inAdvList, inAreaList, 0);
            #endregion

            #region 国际长线
            IList<EyouSoft.Model.AdvStructure.AdvInfo> outAdvList = advBll.GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.国际长线5张logo图片);
            IEnumerable<EyouSoft.Model.SystemStructure.SysCityArea> outAreaList = model.CityAreaControls.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线 && i.IsDefaultShow == true).OrderBy(p => p.SortId);
            outAreaHtml = GetAreaHtml(outAdvList, outAreaList, 1);
            #endregion

            #region 周边线路
            IList<EyouSoft.Model.AdvStructure.AdvInfo> sideAdvList = advBll.GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.周边长线5张logo图片);
            IEnumerable<EyouSoft.Model.SystemStructure.SysCityArea> sideAreaList = model.CityAreaControls.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线 && i.IsDefaultShow == true).OrderBy(p => p.SortId);
            sideAreaHtml = GetAreaHtml(sideAdvList, sideAreaList, 2);
            #endregion
        }
        protected void GetAdvImg()
        {
            EyouSoft.IBLL.AdvStructure.IAdv advBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advList = advBll.GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页广告首页通栏banner2);

            if (advList != null && advList.Count > 0)
            {
                if (advList != null && advList.Count > 0)
                {
                    AdvImage1.AppendFormat("<div style=\"width:720px;height:79px;\"><a {0}><img width=\"720px\" height=\"79px\" src=\"{1}\" alt=\"{2}\" /></a></div>", (advList[0].RedirectURL == "javascript:void(0);" || string.IsNullOrEmpty(advList[0].RedirectURL)) ? string.Format("target=\"_self\" href=\"javascript:;\"") : string.Format("href=\"{0}\" target=\"_blank\"", advList[0].RedirectURL), EyouSoft.Common.Domain.FileSystem + advList[0].ImgPath, advList[0].Title);
                }
            }
        }
        /// <summary>
        /// 返回线路区域Html
        /// </summary>
        /// <param name="advList">区域类别广告集合</param>
        /// <param name="areaList">区域集合</param>
        /// <returns></returns>
        protected string GetAreaHtml(IList<EyouSoft.Model.AdvStructure.AdvInfo> advList, IEnumerable<EyouSoft.Model.SystemStructure.SysCityArea> areaList, int areaType)
        {
            if (advList != null && advList.Count > 0)
            {
                for (int i = 0; i < advList.Count; i++)
                {
                    switch (areaType)
                    {
                        case 0:
                            InremmCompany.AppendFormat("<a {0}>{1}</a>  ", (advList[i].RedirectURL == "javascript:void(0);" || string.IsNullOrEmpty(advList[i].RedirectURL)) ? string.Format("href=\"javascript:void(0);\"") : string.Format("href=\"{0}\" target=\"_blank\"", advList[i].RedirectURL), EyouSoft.Common.Utils.GetText2(advList[i].CompanyName, 8, false));
                            break;
                        case 1:
                            outremmCompany.AppendFormat("<a {0}>{1}</a>  ", (advList[i].RedirectURL == "javascript:void(0);" || string.IsNullOrEmpty(advList[i].RedirectURL)) ? string.Format("href=\"javascript:void(0);\"") : string.Format("href=\"{0}\" target=\"_blank\"", advList[i].RedirectURL), EyouSoft.Common.Utils.GetText2(advList[i].CompanyName, 8, false));
                            break;
                        case 2:
                            sideremmCompany.AppendFormat("<a {0}>{1}</a>  ", (advList[i].RedirectURL == "javascript:void(0);" || string.IsNullOrEmpty(advList[i].RedirectURL)) ? string.Format("href=\"javascript:void(0);\"") : string.Format("href=\"{0}\" target=\"_blank\"", advList[i].RedirectURL), EyouSoft.Common.Utils.GetText2(advList[i].CompanyName, 8, false));
                            break;
                    }
                    if (i == 5) break;
                }
            }

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<div class=\"glcx_lili\"><ul>");
            if (areaList != null)
            {
                int count = areaList.Count();
                int role = 20;
                if (areaType == 1)
                {
                    role = 13;
                }
                if (count <= role)
                {
                    //int remain = (int)Math.Ceiling(count / 7.0) * 7 - count;
                    foreach (EyouSoft.Model.SystemStructure.SysCityArea area in areaList)
                    {
                        strBuilder.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(area.AreaId, CityId), area.AreaName);
                    }
                }
                else
                {
                    int itemIndex = role;
                    foreach (EyouSoft.Model.SystemStructure.SysCityArea area in areaList)
                    {
                        strBuilder.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(area.AreaId, CityId), area.AreaName);
                        if (--itemIndex == 0)
                        {
                            strBuilder.AppendFormat("<li><a href=\"/TourManage/TourList.aspx?RouteType={0}\">更多>></a></li>", areaType);
                            break;
                        }

                    }
                }
            }
            strBuilder.Append("</ul></div>");
            return strBuilder.ToString();
        }
        #endregion
    }
}