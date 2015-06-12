using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
namespace IMFram.TourAgency.TourManger
{
    /// <summary>
    /// 页面功能:ajax调用所关注的线路区域列表
    /// 修改时间:2010-08-14 修改人:xuty
    /// </summary>
    public partial class AjaxAreaList : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string strArea = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {
                Response.Clear();
                Response.Write("对不起，你未开通组团服务！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                InitRouteArea();//初始化线路区域
            }
        }

        #region 初始化区域
        private void InitRouteArea()
        {
           
            StringBuilder strVal = new StringBuilder();//线路区域拼接
            int areaType =Utils.GetInt(Request.QueryString["areaType"]);//线路类型
            int cityId = Utils.GetInt(Request.QueryString["cityId"]);//销售城市
            EyouSoft.Model.SystemStructure.SysCity city = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cityId);
            if (city != null)
            {   //获取该销售城市下的线路区域
                IList<EyouSoft.Model.SystemStructure.SysCityArea> sysAreaList = city.CityAreaControls.Where(i => i.RouteType == ((AreaType)(areaType))).ToList<EyouSoft.Model.SystemStructure.SysCityArea>();
                if (sysAreaList != null && sysAreaList.Count > 0)
                {
                    strVal.Append("<table width=\"205\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
                    strVal.AppendFormat("<tr ><td width='95' height=\"23\" class=\"aun\" ><a target=\"_blank\" href='{0}'><nobr>所 有</nobr></a></td><td width=\"5\"></td>", Domain.UserPublicCenter + "/RouteManage/Default.aspx?CityId=" + cityId);
                    int rowCount = sysAreaList.Count;   //实际的记录数
                    int intAllCount = 0;

                    if ((rowCount + 1) % 2 == 0)
                        intAllCount = rowCount + 1;
                    else
                        intAllCount = ((rowCount + 1) / 2 + 1) * 2;
                    if (rowCount == 0)
                    {
                        strVal.Append("<td width='95' height=\"23\"></td></tr></table>");
                    }
                    else
                    {
                        //除去全部后总共显示intAllCount-1个 
                        for (int i = 0; i < intAllCount - 1; i++)
                        {
                            if (i < rowCount)
                            {
                                int AreaId = sysAreaList[i].AreaId;
                                string AreaName = sysAreaList[i].AreaName;
                                string tmpName = AreaName;
                                if (tmpName.Length > 6)
                                    tmpName = tmpName.Substring(0, 6);
                                string className = "class=\"aun\"";
                                string strUrl = GetDesPlatformUrl(Domain.UserPublicCenter + "/TourManage/TourList.aspx?CityId=" + city.CityId + "&TourAreaId=" + AreaId);
                                strVal.AppendFormat("<td height=\"23\" width='95' " + className + "><a target=\"_blank\" title='{0}' href='{1}'><nobr>{2}</nobr></a></td><td width=\"5\"></td>", AreaName, strUrl, tmpName);
                            }
                            //判断是否换行或者结束
                            if ((i + 1) >= 1 && i != (intAllCount - 1) && (i + 2) % 2 == 0)
                            {
                                strVal.Append("</tr><tr>");

                                strVal.Append("<td colspan=\"8\" height=\"5\"></td>");
                                strVal.Append("</tr>");
                                strVal.Append("<tr>");
                            }
                        }
                        strVal.Append("</tr></table>");
                    }
                }
                else
                {
                    strVal.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='1' bgcolor='#A2CFE5'><tr><td height='100' align='center' bgcolor='#FFFFFF'>当前分站未指定合作供应商</td></tr></table>");
                }
            }
            strArea = strVal.ToString();//显示线路区域
        }
        #endregion
    }
}