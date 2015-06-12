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
using System.Collections.Generic;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 运营后台 平台管理 专线区域维护
    /// 李晓欢 2011-12-28
    /// </summary>
    public partial class AddLineArea : EyouSoft.Common.Control.YunYingPage
    {
        //线路销售区域
        protected System.Text.StringBuilder RouteAreaList = new System.Text.StringBuilder();
        //线路区域所在的城市
        protected string strAreaCityHtml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitContinent();
                INitCountry();

                if (Utils.GetQueryStringValue("routeType") == "0")
                {
                    this.LitrouteType.Text = "国内";
                }
                if (Utils.GetQueryStringValue("routeType") == "1")
                {
                    this.LitrouteType.Text = "国际";
                }
                if (Utils.GetQueryStringValue("routeType") == "2")
                {
                    this.LitrouteType.Text = "周边";
                }

                if (Utils.GetQueryStringValue("action") == "update")
                {                           
                    if (Utils.GetQueryStringValue("AreaID") != null && !string.IsNullOrEmpty(Utils.GetQueryStringValue("AreaID")))
                    {
                        InitArea(Utils.GetQueryStringValue("AreaID"));
                        GetRouteArea(Convert.ToInt32(Utils.GetQueryStringValue("AreaID")));
                    }
                }
            }
        }

        #region 初始化洲
        private void InitContinent()
        {
            string JsonCountryHtmls = string.Empty;            
            if (Utils.GetQueryStringValue("Method") == "action")
            {
                JsonCountryHtmls = "{\"CountryList\":[";
                var list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.SystemStructure.Continent));
                if (list != null && list.Any())
                {
                    foreach (var t in list)
                    {
                        JsonCountryHtmls += "{\"CountryID\":\"" + t.Value + "\",\"CountryName\":\"" + t.Text + "\"},";
                    }
                }
                JsonCountryHtmls += "]}";
                Response.Clear();
                Response.Write(JsonCountryHtmls);
                Response.End();
            }
        }
        #endregion

        #region 初始化国家
        protected void INitCountry()
        {           
            string JsonCountryHtmlS = string.Empty;
            if (Utils.GetQueryStringValue("Method") == "actionC")
            {
                if (Utils.GetQueryStringValue("ID") != null && Utils.GetQueryStringValue("ID") != "")
                {
                    JsonCountryHtmlS = "{\"CountryList\":[";
                    IList<EyouSoft.Model.SystemStructure.MSysCountry> listCountry = new EyouSoft.BLL.SystemStructure.BSysCountry().GetCountryList((EyouSoft.Model.SystemStructure.Continent)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.Continent), Utils.GetQueryStringValue("ID")));
                    if (listCountry != null && listCountry.Count > 0)
                    {
                        for (int j = 0; j < listCountry.Count; j++)
                        {
                            JsonCountryHtmlS += "{\"CountryID\":\"" + listCountry[j].CountryId + "\",\"CountryName\":\"" + listCountry[j].CName + "\"},";
                        }
                    }
                }
                JsonCountryHtmlS += "]}";
                Response.Clear();
                Response.Write(JsonCountryHtmlS);
                Response.End();
            }
        }
        #endregion

        #region  获取线路销售区域
        protected string GetRouteArea(int AreaID)
        {
            IList<EyouSoft.Model.SystemStructure.CityBase> CityList = new EyouSoft.BLL.SystemStructure.SysArea().GetSalesCityByArea(AreaID);
            if (CityList != null && CityList.Count > 0)
            {
                for (int i = 0; i < CityList.Count; i++)
                {
                    RouteAreaList.Append("" + CityList[i].CityName + "&nbsp");
                }
            }
            return RouteAreaList.ToString();
        }
        #endregion

        #region 初始化线路区域
        protected void InitArea(string AreaID)
        {
            EyouSoft.Model.SystemStructure.SysArea AreaModel = new EyouSoft.BLL.SystemStructure.SysArea().GetSysAreaModel(Convert.ToInt32(Utils.GetQueryStringValue("AreaID")));
            if (AreaModel != null)
            {
                this.txtAreaName.Text = AreaModel.AreaName;
                if (AreaModel.VisitCity != null && AreaModel.VisitCity.Count > 0)
                {
                    for (int i = 0; i < AreaModel.VisitCity.Count; i++)
                    {
                        if (AreaModel.VisitCity[i].CountryId == 0) //国内
                        {
                            if (AreaModel.VisitCity[i].CountyId == 0) //选择城市没有选择县区
                            {
                                EyouSoft.Model.SystemStructure.SysCity CityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(AreaModel.VisitCity[i].CityId);
                                if (CityModel != null)
                                {
                                    strAreaCityHtml += "<input type=\"checkbox\" value=\"" + AreaModel.VisitCity[i].ProvinceId + "," + AreaModel.VisitCity[i].CityId + "\"  checked=\"checked\" name=\"cbxCity\"/>" + CityModel.CityName + "&nbsp";
                                }
                                this.hidProACityID.Value += AreaModel.VisitCity[i].CityId + ",";
                            }   
                            else //选择县区
                            {
                                EyouSoft.Model.SystemStructure.SysDistrictCounty County = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetModel(AreaModel.VisitCity[i].CountyId);
                                if (County != null)
                                {
                                    strAreaCityHtml += "<input type=\"checkbox\"  checked=\"checked\" value=\"" + AreaModel.VisitCity[i].ProvinceId + "," + AreaModel.VisitCity[i].CityId + "," + AreaModel.VisitCity[i].CountyId + "\" name=\"cbxCounty\"/>" + County.DistrictName + "&nbsp";
                                }
                                this.hidProACityACounID.Value += AreaModel.VisitCity[i].CountyId + ",";
                            }
                        }
                        else //国际
                        {
                            EyouSoft.Model.SystemStructure.MSysCountry CountryModel = EyouSoft.BLL.SystemStructure.BSysCountry.CreateInstance().GetCountry(AreaModel.VisitCity[i].CountryId);
                            if (CountryModel != null)
                            {
                                strAreaCityHtml += "<input type=\"checkbox\" value=\"" + (int)CountryModel.Continent + "," + AreaModel.VisitCity[i].CountryId + "\"  checked=\"checked\" name=\"cbxCity\"/>" + CountryModel.CName + "&nbsp";
                            }
                            this.hidProACityID.Value += AreaModel.VisitCity[i].CountryId + ",";
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存
        protected void btnsave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.SystemStructure.SysArea AreaModel = new EyouSoft.Model.SystemStructure.SysArea();
            //区域名称
            AreaModel.AreaName = this.txtAreaName.Text;
            //区域所属类型
            AreaModel.RouteType = (EyouSoft.Model.SystemStructure.AreaType)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.AreaType), Utils.GetQueryStringValue("routeType"));
            //浏览城市
            AreaModel.VisitCity = new List<EyouSoft.Model.SystemStructure.SysAreaVisitCity>();            

            string[] cbxCountyIdlist = Utils.GetFormValues("cbxCounty");
            if (cbxCountyIdlist.Length > 0)
            {
                for (int i = 0; i < cbxCountyIdlist.Length; i++)
                {
                    EyouSoft.Model.SystemStructure.SysAreaVisitCity VisitCity = new EyouSoft.Model.SystemStructure.SysAreaVisitCity();
                    VisitCity.ProvinceId = Convert.ToInt32(cbxCountyIdlist[i].Split(',')[0]);
                    VisitCity.CityId = Convert.ToInt32(cbxCountyIdlist[i].Split(',')[1]);
                    VisitCity.CountyId = Convert.ToInt32(cbxCountyIdlist[i].Split(',')[2]);
                    AreaModel.VisitCity.Add(VisitCity);
                }
            }

            string[] cbxCityIdlist = Utils.GetFormValues("cbxCity");
            if (cbxCityIdlist.Length > 0)
            {
                for (int i = 0; i < cbxCityIdlist.Length; i++)
                {
                    EyouSoft.Model.SystemStructure.SysAreaVisitCity VisitCity = new EyouSoft.Model.SystemStructure.SysAreaVisitCity();
                    if (Utils.GetQueryStringValue("routeType") == "1")
                    {
                        VisitCity.CountryId = Convert.ToInt32(cbxCityIdlist[i].Split(',')[1]);
                    }
                    else
                    {
                        VisitCity.ProvinceId = Convert.ToInt32(cbxCityIdlist[i].Split(',')[0]);
                        VisitCity.CityId = Convert.ToInt32(cbxCityIdlist[i].Split(',')[1]);
                    }
                    AreaModel.VisitCity.Add(VisitCity);
                }
            }

            if (Utils.GetQueryStringValue("AreaID") != null && !string.IsNullOrEmpty(Utils.GetQueryStringValue("AreaID")))
            {
                //修改                
                AreaModel.AreaId = Convert.ToInt32(Utils.GetQueryStringValue("AreaID"));
                int result = new EyouSoft.BLL.SystemStructure.SysArea().UpdateSysArea(AreaModel);
                if (result > 0)
                {
                    Response.Write("<script>alert('修改成功！');location.href=location.href;parent.location.href='UniversalLineManage.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('修改失败！');location.href=location.href;parent.location.href='UniversalLineManage.aspx';</script>");
                }
            }
            else
            {
                //添加                
                int message = new EyouSoft.BLL.SystemStructure.SysArea().AddSysArea(AreaModel);
                if (message > 0)
                {
                    Response.Write("<script>alert('添加成功！');location.href=location.href;parent.location.href='UniversalLineManage.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('线路区域已存在！');location.href=location.href;parent.location.href='UniversalLineManage.aspx';</script>");
                }
            }
        }
        #endregion
    }
}
