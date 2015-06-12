using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.URLREWRITE;
namespace UserPublicCenter
{
    /// <summary>
    /// 选择销售城市
    /// 开发人:刘玉灵   时间2010-7-5
    /// </summary>
    public partial class ToCutCity : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 所有销售城市
        /// </summary>
        protected string strAllCityList = "";
        /// <summary>
        /// 联盟LOGO
        /// </summary>
        protected string UnionLogo = "";
        /// <summary>
        /// 首页
        /// </summary>
        protected string DefaultUrl = string.Empty;
        protected string strAllHotCity = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CityId > 0)
            {
                DefaultUrl = SubStation.CityUrlRewrite(CityId);
            }
            else
            {
                DefaultUrl = Domain.UserPublicCenter + "/";
            }

            if (!Page.IsPostBack)
            {
                GetUnionLogo();
                GetAllSaleCity();
            }
            this.Page.Title = "选择城市";
        }

        /// <summary>
        /// 获的联盟LOGO
        /// </summary>
        protected void GetUnionLogo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (Model != null)
            {
                UnionLogo =EyouSoft.Common.Domain.FileSystem+Model.UnionLog;
            }
            else
            {
                UnionLogo = ImageServerPath + "/images/UserPublicCenter/indexlogo.gif";
            }
            Model = null;
        }

        /// <summary>
        /// 获的所有的销售城市
        /// </summary>
        protected void GetAllSaleCity()
        {
            int Index = EyouSoft.Common.Utils.GetInt(Request.QueryString["Index"]);
            string strUrl = "/Default.aspx";
            switch (Index) 
            { 
                case 2:
                    strUrl = "/RouteManage/Default.aspx";
                    break;
                case 3:
                    strUrl = "/PlaneInfo/PlaneListPage.aspx";
                    break;
                case 4:
                    strUrl = "/ScenicManage/ScenicDefalut.aspx";
                    break;
                case 5:
                    strUrl = "/HotelManage/Default.aspx";
                    break;
                case 6:
                    strUrl = "/CarInfo/CarListPage.aspx";
                    break;
                case 7:
                    strUrl = "/TravelManage/TravelDefault.aspx";
                    break;
                case 8:
                    strUrl = "/ShoppingInfo/ShoppingListPage.aspx";
                    break;
                case 9:
                    strUrl = "/SupplierInfo/SupplierInfo.aspx";
                    break;
            }

            StringBuilder strAllCity = new StringBuilder();

            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                EyouSoft.IBLL.SystemStructure.ISysCity Citybll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
                foreach (EyouSoft.Model.SystemStructure.SysProvince Province in ProvinceList)
                {
                    IList<EyouSoft.Model.SystemStructure.SysCity> Citylist = Citybll.GetSysCityList(Province.ProvinceId,null);
                    
                    if (Citylist != null && Citylist.Count > 0)
                    {
                        strAllCity.Append("<tr>");
                        strAllCity.Append("<td align=\"left\" style=\"border-bottom: 1px dashed rgb(238, 238, 238);\">");
                        strAllCity.AppendFormat("<h3>{0}：", Province.ProvinceName);
                        string allCity = "";
                        int Count = 0;
                        Citylist = (Citylist.OrderByDescending(pet => pet.IsEnabled)).ToList();
                                    

                        foreach (EyouSoft.Model.SystemStructure.SysCity City in Citylist)
                        {
                            if (Count!=0&& Count % 7 == 0)
                            {
                                allCity += "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            }
                            string Enabledstyle="";
                            if (!City.IsEnabled) {
                                Enabledstyle = "style='color:#6E90B6'";
                            }
                            allCity += string.Format("<nobr><a href=\"{0}\" nowrap=\"nowrap\" {2}>{1}</a></nobr> <span style='color:#eeeeee'>|</span> ",
                                SubStation.CityUrlRewrite(City),//strUrl + "?CityId=" + City.CityId + "&isCut=1", 
                                City.CityName, 
                                Enabledstyle);
                            Count++;
                        }
                        if (allCity != "")
                        {
                            allCity = allCity.Substring(0,allCity.Length-9);
                        }
                        strAllCity.Append(allCity);
                        strAllCity.Append("</h3>");
                        strAllCity.Append("</td>");
                        strAllCity.Append("</tr>");
                    }
                    Citylist = null;
                }
                Citybll = null;
            }
            ProvinceList = null;
            strAllCityList = strAllCity.ToString();
        }
    }
}
