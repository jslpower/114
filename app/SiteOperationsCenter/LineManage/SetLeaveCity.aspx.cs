using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 设置常用出港城市
    /// 罗丽娥  2010-07-12
    /// </summary>
    public partial class SetLeaveCity : EyouSoft.Common.Control.YunYingPage
    {
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected string strSelectLeaveCity = string.Empty, strLeaveCity = string.Empty;
        protected string ReleaseType = string.Empty, ContainerID = string.Empty;
        protected string CompanyId = string.Empty;
        protected string SelectedVal = string.Empty;
        protected string type = string.Empty;
        protected bool IsCompanyCheck = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = Utils.GetQueryStringValue("CompanyId");
            SelectedVal = Utils.GetQueryStringValue("CityId");
            type = Utils.GetQueryStringValue("type");
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.SiteOperationsCenter + "/Login.aspx");
            }
            IsCompanyCheck = true;
            if (!Page.IsPostBack)
            {
                strSelectLeaveCity = InitSelectLeaveCity();
                InitLeaveCity();
            }
            ReleaseType = Utils.GetQueryStringValue("ReleaseType");
            ContainerID = Utils.GetQueryStringValue("ContainerID");

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("save", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(SaveData());
                    Response.End();
                }
            }
        }

        #region 初始化公司已经设置的出港城市
        private string InitSelectLeaveCity()
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(CompanyId);
            if (list != null && list.Count > 0)
            {
                int i = 1;
                foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
                {
                    if (i != 0 && i % 3 == 1)
                    {
                        str.Append("<tr class=\"baidi\">");
                    }
                    str.AppendFormat("<td width=\"10%\" align=\"right\" bgcolor=\"#FFFFFF\"><input name=\"chkSelectLeaveCityID\" type=\"checkbox\" value=\"{0}|{1}\" checked=\"checked\" /></td>", model.CityId, model.CityName);
                    str.AppendFormat("<td width=\"20%\" align=\"left\" bgcolor=\"#FFFFFF\">{0}</td>", model.CityName);
                    if (i != 0 && i % 3 == 0)
                    {
                        str.Append("</tr>");
                    }
                    i++;
                }
            }
            list = null;
            bll = null;
            return str.ToString();
        }
        #endregion

        #region 初始化所有的出港城市
        /// <summary>
        /// 初始化所有的出港城市
        /// </summary>
        /// <returns></returns>
        private void InitLeaveCity()
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.SystemStructure.ISysCity bll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysCity> list = bll.GetCityList();
            if (list != null && list.Count > 0)
            {
                IList<EyouSoft.Model.SystemStructure.SysCity> listProvince = new List<EyouSoft.Model.SystemStructure.SysCity>();
                Hashtable hashP = new Hashtable();
                foreach (EyouSoft.Model.SystemStructure.SysCity model in list)
                {
                    if (model.IsSite && model.IsEnabled)
                    {
                        if (!hashP.Contains(model.ProvinceId))
                        {
                            hashP.Add(model.ProvinceId, model);
                            listProvince.Add(model);
                        }
                    }
                }
                if (listProvince != null && listProvince.Count > 0)
                {
                    this.rptProvinceID.DataSource = listProvince;
                    this.rptProvinceID.DataBind();
                }
                listProvince = null;
            }
            list = null;
            bll = null;
        }

        protected string GetIsSiteCity(string ProvinceID)
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.SystemStructure.ISysCity bll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysCity> list = bll.GetCityList(int.Parse(ProvinceID), 0, string.Empty, true, true);
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysCity model in list)
                {
                    str.AppendFormat("<label for=\"chkLeaveCityID{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"chkLeaveCityID\" id=\"chkLeaveCityID{0}\" value=\"{0}|{1}\" />{1}&nbsp;&nbsp;", model.CityId, model.CityName);
                }
            }
            list = null;
            bll = null;
            return str.ToString();
        }
        #endregion

        #region 保存设置
        private string SaveData()
        {
            string tmpVal = string.Empty;
            // 先把选中的城市设置为出港城市，然后查出现在公司的出港城市并初始化到父页面
            string[] IdList = (!String.IsNullOrEmpty(Request.QueryString["IdList"])) ? Request.QueryString["IdList"].ToString().Split(',') : null;
            string[] NameList = (!String.IsNullOrEmpty(Request.QueryString["NameList"])) ? Request.QueryString["NameList"].ToString().Split(',') : null;
            if (IdList == null)
            {
                MessageBox.ShowAndReturnBack(this, "请选择要设置的出港城市!", 1);
            }
            else
            {
                EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
                IList<EyouSoft.Model.SystemStructure.CityBase> list = new List<EyouSoft.Model.SystemStructure.CityBase>();
                if (IdList.Length > 0)
                {
                    for (int i = 0; i < IdList.Length; i++)
                    {
                        EyouSoft.Model.SystemStructure.CityBase model = new EyouSoft.Model.SystemStructure.CityBase();
                        model.CityId = int.Parse(IdList[i]);
                        model.CityName = NameList[i];
                        model.ProvinceId = 0;
                        model.ProvinceName = string.Empty;
                        list.Add(model);
                    }
                    if (bll.SetCompanyPortCity(CompanyId, list))
                    {
                        tmpVal = BindLeaveCity(Utils.GetInt(SelectedVal), CompanyId);
                    }
                }
            }
            return tmpVal;
        }



        private string BindLeaveCity(int LeaveCity, string CompanyId)
        {
            StringBuilder str1 = new StringBuilder();  // 当前公司注册时所在省份的出港城市
            StringBuilder str2 = new StringBuilder();  // 固定城市

            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            var modelcompanyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(CompanyId);

            if (list != null && list.Count > 0)
            {
                string[] defaultcity = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("DefaultCity").Split(',');
                foreach (CityBase modelcity in list)
                {
                    if (!defaultcity.Contains(modelcity.CityId.ToString()))
                    {
                        if (modelcity.CityId == LeaveCity)
                            str1.AppendFormat("<label for=\"AddStandardTour_rad{2}{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardTour_rad{2}\" id=\"AddStandardTour_rad{2}{0}\" checked value=\"{0}\" />{1}</label>", modelcity.CityId, modelcity.CityName, type);
                        else
                            str1.AppendFormat("<label for=\"AddStandardTour_rad{2}{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardTour_rad{2}\" id=\"AddStandardTour_rad{2}{0}\" value=\"{0}\" />{1}</label>", modelcity.CityId.ToString(), modelcity.CityName, type);
                    }
                }

                foreach (string str in defaultcity)
                {
                    SysCity item = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(Utils.GetInt(str));
                    if (str == LeaveCity.ToString())
                        str2.AppendFormat("<label for=\"AddStandardTour_rad{2}{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardTour_rad{2}\" id=\"AddStandardTour_rad{2}{0}\" checked  value=\"{0}\" />{1}</label>", item.CityId, item.CityName, type);
                    else
                        str2.AppendFormat("<label for=\"AddStandardTour_rad{2}{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardTour_rad{2}\" id=\"AddStandardTour_rad{2}{0}\" value=\"{0}\" />{1}</label>", item.CityId, item.CityName, type);
                }
            }
            if (!string.IsNullOrEmpty(str1.ToString()))
            {
                return str1.ToString() + "|" + str2.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&type=BackCity&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
            }
            else
                return str2.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&type=BackCity&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";



        }

        //private string BindLeaveCity()
        //{
        //    StringBuilder str1 = new StringBuilder();  // 当前公司注册时所在省份的出港城市
        //    StringBuilder str2 = new StringBuilder();  // 设置过的出港城市

        //    EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
        //    var modelCompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
        //    IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(CompanyId);
        //    if (list != null && list.Count > 0)
        //    {
        //        int i = 0;
        //        foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
        //        {
        //            if (model.ProvinceId == modelCompanyInfo.ProvinceId)
        //            {
        //                if (i == 0)
        //                {
        //                    str1.AppendFormat("<label for=\""+ ReleaseType +"_radPortCity{0}\"><input type=\"radio\" name=\""+ ReleaseType +"_radPortCity\" id=\""+ ReleaseType +"_radPortCity{0}\" value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\""+ ReleaseType +"_radPortCity\" />{1}</label>", model.CityId, model.CityName);
        //                }
        //                else
        //                {
        //                    str1.AppendFormat("<label for=\""+ ReleaseType +"_radPortCity{0}\"><input type=\"radio\" name=\""+ ReleaseType +"_radPortCity\" id=\""+ ReleaseType +"_radPortCity{0}\" value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
        //                }
        //            }
        //            else
        //            {
        //                if (i == 0)
        //                {
        //                    str2.AppendFormat("<label for=\""+ ReleaseType +"_radPortCity{0}\"><input type=\"radio\" name=\""+ ReleaseType +"_radPortCity\" id=\""+ ReleaseType +"_radPortCity{0}\" value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\""+ ReleaseType +"_radPortCity\" />{1}</label>", model.CityId, model.CityName);
        //                }
        //                else
        //                {
        //                    str2.AppendFormat("<label for=\""+ ReleaseType +"_radPortCity{0}\"><input type=\"radio\" name=\""+ ReleaseType +"_radPortCity\" id=\""+ ReleaseType +"_radPortCity{0}\" value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
        //                }
        //            }
        //            i++;
        //        }
        //    }
        //    if (str1.ToString() != string.Empty)
        //    {
        //        if (str2.ToString() != string.Empty)
        //        {
        //            return str1.ToString() + "|" + str2.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
        //        }
        //        else
        //        {
        //            return str1.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
        //        }
        //    }
        //    else
        //    {
        //        if (str2.ToString() != string.Empty)
        //        {
        //            return str2.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
        //        }
        //        else
        //        {
        //            return "<a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/LineManage/SetLeaveCity.aspx?ReleaseType=AddStandardTour&CompanyId=" + CompanyId + "&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
        //        }
        //    }
        //}
        #endregion
    }
}
