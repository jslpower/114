using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 网站地图
    /// 功能：按城市生成网站地图
    /// 创建人：郑知远
    /// 创建时间： 2011-02-22  
    public partial class Sitemap : EyouSoft.Common.Control.FrontPage
    {
        protected string strRouteList = "";
        protected string strRouteList1 = "";
        protected string strRouteList2 = "";
        protected string strRouteList3 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AboutUsHeadControl1.SetImgType = "8";
                this.AboutUsLeftControl1.SetIndex = "8";
                //this.DefaultRouteControl1.IsIndex = false;
                GetRouteList();
            }
        }

        /// <summary>
        /// 取得热点城市网站地图
        /// </summary>
        protected void GetRouteList()
        {
            //取得北京网站地图
            GetURL(19);
            this.Literal1.Text = strRouteList1;
            this.Literal2.Text = strRouteList2;
            this.Literal3.Text = strRouteList3;
            //取得广州网站地图
            GetURL(48);
            this.Literal4.Text = strRouteList1;
            this.Literal5.Text = strRouteList2;
            this.Literal6.Text = strRouteList3;
            //取得杭州网站地图
            GetURL(362);
            this.Literal7.Text = strRouteList1;
            this.Literal8.Text = strRouteList2;
            this.Literal9.Text = strRouteList3;
            //取得上海网站地图
            GetURL(292);
            this.Literal10.Text = strRouteList1;
            this.Literal11.Text = strRouteList2;
            this.Literal12.Text = strRouteList3;
            //取得南京网站地图
            GetURL(192);
            this.Literal13.Text = strRouteList1;
            this.Literal14.Text = strRouteList2;
            this.Literal15.Text = strRouteList3;
            //取得济南网站地图
            GetURL(257);
            this.Literal16.Text = strRouteList1;
            this.Literal17.Text = strRouteList2;
            this.Literal18.Text = strRouteList3;
            //取得宁波网站地图
            GetURL(367);
            this.Literal19.Text = strRouteList1;
            this.Literal20.Text = strRouteList2;
            this.Literal21.Text = strRouteList3;
            //取得昆明网站地图
            GetURL(352);
            this.Literal22.Text = strRouteList1;
            this.Literal23.Text = strRouteList2;
            this.Literal24.Text = strRouteList3;
            //取得沈阳网站地图
            GetURL(225);
            this.Literal25.Text = strRouteList1;
            this.Literal26.Text = strRouteList2;
            this.Literal27.Text = strRouteList3;
        }

        /// <summary>
        /// 取得某热点城市网站地图
        /// </summary>
        /// <param name="intCityId">城市ID</param>
        protected void GetURL(int intCityId)
        {
            EyouSoft.Model.SystemStructure.SysCity Model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(intCityId);

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
            strRouteList1 = "";
            strRouteList2 = "";
            strRouteList3 = "";
            foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList1)
            {
                //根据规则生成新的线路的URL
                strRouteList1 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, intCityId) + "\">{0}</a> ", item.AreaName);
            }
            foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList2)
            {
                //根据规则生成新的线路的URL
                strRouteList2 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, intCityId) + "\">{0}</a> ", item.AreaName);
            }
            foreach (EyouSoft.Model.SystemStructure.SysCityArea item in AreaList3)
            {
                //根据规则生成新的线路的URL
                strRouteList3 += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, intCityId) + "\">{0}</a> ", item.AreaName);
            }

            AreaList1 = null;
            AreaList2 = null;
            AreaList3 = null;
        }        
    }
}
