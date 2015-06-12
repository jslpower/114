using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 选择所有的销售城市
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class OtherAllSaleCity : System.Web.UI.Page
    {
        protected string strAllCityList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {  

                this.GetAllSaleCity();

            }
        }

        /// <summary>
        /// 获的所有的销售城市
        /// </summary>
        protected void GetAllSaleCity()
        {
            StringBuilder strAllCity = new StringBuilder();

            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                EyouSoft.IBLL.SystemStructure.ISysCity Citybll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
                foreach (EyouSoft.Model.SystemStructure.SysProvince Province in ProvinceList)
                {
                    IList<EyouSoft.Model.SystemStructure.SysCity> Citylist;
                    string isAll = Utils.GetQueryStringValue("isall");
                    if (isAll == "yes")
                    {
                        Citylist = Citybll.GetSysCityList(Province.ProvinceId, null);
                    }
                    else
                    {
                        Citylist = Citybll.GetSaleCity(null, new int[] { Province.ProvinceId });
                    }
                    if (Citylist != null && Citylist.Count > 0)
                    {
                        strAllCity.Append("<tr bgcolor=\"#DBF7FD\" >");
                        strAllCity.Append("<td align=\"center\">");
                        strAllCity.AppendFormat("<input  type=\"checkbox\" name='ckProvince' id=\"ckAllCity_{1}\" onclick=\"OtherAllSaleCity.ckAllProvinceCity(this);\"/><label for=\"ckAllCity_{1}\" style=\"cursor:pointer\">{0}</label>：", Province.ProvinceName, Province.ProvinceId);
                      
                        strAllCity.Append("</td>");
                        strAllCity.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">");
                        string allCity = "";
                        int Count = 0;
                        foreach (EyouSoft.Model.SystemStructure.SysCity City in Citylist)
                        {
                            allCity += string.Format("<input id=\"thisCity_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"thisCity_{0}\" style=\"cursor:pointer\">{1}</label> ", City.CityId, City.CityName);
                            Count++;
                            if (Count != 0 && Count % 8 == 0)
                            {
                                allCity += "<br/>";
                            }
                        }
                        strAllCity.Append(allCity);
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
