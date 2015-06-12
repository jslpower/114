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
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：销售区域维护——第三步选择批发商
    /// 开发人：杜桂云      开发时间：2010-06-28
    /// </summary>
    public partial class ChooseRouteAgency : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int CityID = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取页面参数
            CityID = Utils.GetInt(Request.QueryString["CityID"]);

            #region 初始化绑定数据
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityID);
            if (cityModel != null)
            {
                IList<EyouSoft.Model.SystemStructure.SysCityArea> cityAreaList = cityModel.CityAreaControls;
                if (cityAreaList != null && cityAreaList.Count > 0)
                {
                    this.dl_GNLongRouteList.DataSource = cityAreaList.Where(Item => (Item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线)).ToList();
                    this.dl_GNLongRouteList.DataBind();
                    this.dl_GNShortRouteList.DataSource = cityAreaList.Where(Item => (Item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线)).ToList();
                    this.dl_GNShortRouteList.DataBind();
                    this.dl_OutRouteList.DataSource = cityAreaList.Where(Item => (Item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)).ToList(); ;
                    this.dl_OutRouteList.DataBind();

                }
            }
            //释放资源
            cityModel = null;
            #endregion
        }
        #endregion

        #region 获取线路区域下的批发商个数
        //长线
        protected string GetAgencyCount(string AreaID)
        {
            int num = 0;
            string returnVal = "";
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityID);
            if (cityModel != null)
            {
                Dictionary<int, IList<string>> dicData = cityModel.AreaAdvNum;
                if (dicData != null && dicData.ContainsKey(int.Parse(AreaID)) && dicData[int.Parse(AreaID)] != null)
                {
                    num = dicData[int.Parse(AreaID)].Count;
                }
            }
            //释放资源
            cityModel = null;
            string linkUrl = "RouteAgencyList.aspx?CityID=" + CityID.ToString() + "&RouteAreaId=" + AreaID;
            returnVal = string.Format("<a href='javascript:void(0);' style='cursor: pointer' onclick='openDialog(\"{0}\");return false;'>{1}</a>", linkUrl, num.ToString());
            return returnVal;
        }
        //短线
        protected string GetShortAgencyCount(string AreaID)
        {
            int num = 0;
            string returnVal = "";
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityID);
            if (cityModel != null)
            {
                Dictionary<int, IList<string>> dicData = cityModel.AreaAdvNum;
                if (dicData != null && dicData.ContainsKey(int.Parse(AreaID)) && dicData[int.Parse(AreaID)] != null)
                {
                    num = dicData[int.Parse(AreaID)].Count;
                }
            }
            //释放资源
            cityModel = null;
            string linkUrl = "RouteAgencyList.aspx?CityID=" + CityID.ToString() + "&RouteAreaId=" + AreaID;
            returnVal = string.Format("<a href='javascript:void(0);' style='cursor: pointer' onclick='openDialog(\"{0}\");return false;'>{1}</a>", linkUrl, num.ToString());
            return returnVal;
        }
        //出境线路
        protected string GetOutAgencyCount(string AreaID)
        {
            int num = 0;
            string returnVal = "";
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityID);
            if (cityModel != null)
            {
                Dictionary<int, IList<string>> dicData = cityModel.AreaAdvNum;
                if (dicData != null && dicData.ContainsKey(int.Parse(AreaID)) && dicData[int.Parse(AreaID)] != null)
                {
                    num = dicData[int.Parse(AreaID)].Count;
                }
            }
            //释放资源
            cityModel = null;
            string linkUrl = "RouteAgencyList.aspx?CityID=" + CityID.ToString() + "&RouteAreaId=" + AreaID;
            returnVal = string.Format("<a href='javascript:void(0);' style='cursor: pointer' onclick='openDialog(\"{0}\");return false;'>{1}</a>", linkUrl, num.ToString());
            return returnVal;
        }
        #endregion

    }
}
