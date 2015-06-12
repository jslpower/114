using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.WebControl
{
    public partial class DefaultRouteControl : System.Web.UI.UserControl
    {
        private bool _isIndex = false;

        /// <summary>
        /// 是否是首页
        /// 首页页面用div布局，团队显示页用table布局
        /// </summary>
        public bool IsIndex
        {
            get { return _isIndex; }
            set { _isIndex = value; }
        }
        protected string strRouteList = "";
        protected string strRouteList1 = "";
        protected string strRouteList2 = "";
        protected string strRouteList3 = "";
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetRouteList();
            }
        }
        protected void GetRouteList()
        {
            string strUrlParms = "";

            FrontPage page = this.Page as FrontPage;

            int CityId = page.CityId;

            if (CityId > 0)
            {
                strUrlParms = "&CityId=" + CityId;
            }


            EyouSoft.Model.SystemStructure.SysCity Model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);

            IList<EyouSoft.Model.SystemStructure.SysCityArea> AreaList1 = new List<EyouSoft.Model.SystemStructure.SysCityArea>();
            IList<EyouSoft.Model.SystemStructure.SysCityArea> AreaList2 = new List<EyouSoft.Model.SystemStructure.SysCityArea>();
            IList<EyouSoft.Model.SystemStructure.SysCityArea> AreaList3 = new List<EyouSoft.Model.SystemStructure.SysCityArea>();
            if (Model != null)
            {
                IList<EyouSoft.Model.SystemStructure.SysCityArea> Citylist = Model.CityAreaControls;
                if (Citylist != null && Citylist.Count > 0)
                {
                    foreach (EyouSoft.Model.SystemStructure.SysCityArea list in Citylist)
                    {
                        if (list.IsDefaultShow)
                        {
                            if (list.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线)
                            {
                                AreaList1.Add(list);
                            }
                            if (list.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                            {
                                AreaList2.Add(list);
                            }
                            if (list.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线)
                            {
                                AreaList3.Add(list);
                            }
                        }
                    }
                }
                Citylist = null;

            }
            Model = null;

            int TourCount1 = AreaList1.Count;
            int TourCount2 = AreaList2.Count;
            int TourCount3 = AreaList3.Count;

            if (IsIndex)
            {
                if (TourCount1 > 0)
                {
                    if (TourCount1 >= 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            //根据规则生成新的线路的URL
                            strRouteList1 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(AreaList1[i].AreaId,CityId) + "\">{0}</a>", AreaList1[i].AreaName);
                        }
                        strRouteList1 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(0, CityId) + "\">{1}</a>", CityId, "更多>>");

                    }
                    else
                    {
                        foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList1)
                        {
                            //根据规则生成新的线路的URL
                            strRouteList1 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", item.AreaName);
                        }

                        for (int i = 0; i < 20 - TourCount1; i++)
                        {
                            strRouteList1 += string.Format("<a>预定位置</a>");
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        strRouteList1 += string.Format("<a>预定位置</a>");
                    }
                }

                if (TourCount2 > 0)
                {
                    if (TourCount2 >= 13)
                    {
                        for (int i = 0; i < 13; i++)
                        {
                            //根据规则生成新的线路的URL
                            strRouteList2 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(AreaList2[i].AreaId, CityId) + "\">{0}</a>", AreaList2[i].AreaName);
                        }

                        strRouteList2 += string.Format("<a href=\"/RouteManage/Default.aspx?CityId={0}\">{1}</a>", CityId, "更多>>");

                    }
                    else
                    {
                        foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList2)
                        {
                            //根据规则生成新的线路的URL
                            strRouteList2 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", item.AreaName);
                        }

                        for (int i = 0; i < 13 - TourCount2; i++)
                        {
                            strRouteList2 += string.Format("<a>预定位置</a>");
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 13; i++)
                    {
                        strRouteList2 += string.Format("<a>预定位置</a>");
                    }
                }
                if (TourCount3 > 0)
                {
                    if (TourCount3 >= 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            //根据规则生成新的线路的URL
                            strRouteList3 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(AreaList3[i].AreaId, CityId) + "\">{0}</a>", AreaList3[i].AreaName);
                        }
                        strRouteList3 += string.Format("<a href=\"/RouteManage/Default.aspx?CityId={0}\">{1}</a>", CityId, "更多>>");

                    }
                    else
                    {
                        foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList3)
                        {
                            //根据规则生成新的线路的URL
                            strRouteList3 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", item.AreaName);
                        }

                        for (int i = 0; i < 20 - TourCount3; i++)
                        {
                            strRouteList3 += string.Format("<a>预定位置</a>");
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        strRouteList3 += string.Format("<a>预定位置</a>");
                    }
                }
            }
            else
            {
                foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList1)
                {
                    //根据规则生成新的线路的URL
                    strRouteList1 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", item.AreaName);
                }
                foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList2)
                {
                    //根据规则生成新的线路的URL
                    strRouteList2 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", item.AreaName);
                }
                foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList3)
                {
                    //根据规则生成新的线路的URL
                    strRouteList3 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", item.AreaName);
                }
            }
            AreaList1 = null;
            AreaList2 = null;
            AreaList3 = null;


            //团队页用Table布局
            if (!IsIndex)
            {
                strRouteList += "<table bgcolor=\"#FFFFFF\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td valign=\"top\">";
                strRouteList += "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr><td width=\"3%\" valign=\"top\" style=\"padding-top:8px;\"> <img src=\"" + ImageServerPath + "/images/UserPublicCenter/gncx.gif\" width=\"24\" height=\"77\" /></td> <td width=\"97%\" valign=\"top\"> <div class=\"linestye1\">";
                strRouteList += strRouteList1 + "</div> </td> </tr> </table>";
                strRouteList += "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr><td width=\"3%\" valign=\"top\" style=\"padding-top:10px;\"> <img src=\"" + ImageServerPath + "/images/UserPublicCenter/gjx.gif\" width=\"24\" height=\"77\" /></td> <td width=\"97%\" valign=\"top\"> <div class=\"linestye1\">";
                strRouteList += strRouteList2 + "</div> </td> </tr> </table>";
                strRouteList += "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr><td width=\"3%\" valign=\"top\" style=\"padding-top:10px;\"> <img src=\"" + ImageServerPath + "/images/UserPublicCenter/zby.gif\" width=\"24\" height=\"77\" /></td> <td width=\"97%\" valign=\"top\"> <div class=\"linestye1\">";
                strRouteList += strRouteList3 + "</div> </td> </tr> </table>";

                strRouteList += "</td></tr></table>";

            }
            else
            {
                strRouteList = "<div class=\"linetype\"><a href=\"#\" class=\"nei\">国内长线</a><a href=\"#\" class=\"ji\">国际线</a><a href=\"#\" class=\"zhou\">周边游</a></div>";
                strRouteList += "<div class=\"linestye1\">" + strRouteList1 + "</div>";
                strRouteList += "<div class=\"linestye2\">" + strRouteList2 + "</div>";
                strRouteList += "<div class=\"linestye3\">" + strRouteList3 + "</div>";
            }
        }
    }
}