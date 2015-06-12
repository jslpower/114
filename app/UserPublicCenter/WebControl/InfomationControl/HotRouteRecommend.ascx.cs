using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;

namespace UserPublicCenter.WebControl.InfomationControl
{
    /// <summary>
    /// 热门线路推荐
    /// mk 2010-4-1
    /// </summary>
    public partial class HotRouteRecommend : UserControl
    {
        protected int CityId = 0;
        protected string HotRouteRecList = "";//热门线路推荐html

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FrontPage page = this.Page as FrontPage;
                CityId = page.CityId;
                GetHotRouteRecommend();
            }
        }

        /// <summary>
        /// 获得热门线路推荐信息列表
        /// </summary>
        private void GetHotRouteRecommend()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> hotRouteList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetExistsTourAreas();
            IEnumerable<EyouSoft.Model.SystemStructure.SysArea> areaList1 = null;
            IEnumerable<EyouSoft.Model.SystemStructure.SysArea> areaList2 = null;
            IEnumerable<EyouSoft.Model.SystemStructure.SysArea> areaList3 = null;
            areaList1 = hotRouteList.Where(item => item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线);
            areaList2 = hotRouteList.Where(item => item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线);
            areaList3 = hotRouteList.Where(item => item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线);

            HotRouteRecList += GetSingleString(EyouSoft.Model.SystemStructure.AreaType.国内长线.ToString(), areaList1);
            HotRouteRecList += GetSingleString(EyouSoft.Model.SystemStructure.AreaType.国际线.ToString(), areaList2);
            HotRouteRecList += GetSingleString(EyouSoft.Model.SystemStructure.AreaType.国内短线.ToString(), areaList3);
        }

        /// <summary>
        /// 根据区域类型获取显示的html
        /// </summary>
        /// <param name="areaType">区域类型</param>
        /// <param name="list">列表</param>
        /// <returns></returns>
        private string GetSingleString(string areaType, IEnumerable<EyouSoft.Model.SystemStructure.SysArea> list)
        {
            string result = "";
            string temp = "<li><b>{0}</b>{1}</li>";

            if (areaType == EyouSoft.Model.SystemStructure.AreaType.国际线.ToString())
            {
                areaType = "国际长线";
            }

            if (areaType == EyouSoft.Model.SystemStructure.AreaType.国内短线.ToString())//国内短线的样式不一样
                temp = "<li style=\"margin-bottom: 0px;\"><b>{0}</b>{1}</li>";
            StringBuilder strList = new StringBuilder();
            if (list != null && list.Count() > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysArea item in list)
                {
                    strList.AppendFormat("<a href='{0}' target=\"_blank\" style=\"white-space:nowrap;\"> {1}</a> | ", EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, CityId), EyouSoft.Common.Utils.GetText(item.AreaName, 30));
                }
                result = string.Format(temp, areaType, strList.ToString().Substring(0, strList.ToString().LastIndexOf('|')));
            }
            else
            {
                result = string.Format(temp, areaType, "    暂无热门线路推荐信息   "); ;
            }
            return result;
        }
    }
}