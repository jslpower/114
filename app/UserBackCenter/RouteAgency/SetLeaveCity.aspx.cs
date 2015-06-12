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

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 设置常用出港城市
    /// 罗丽娥  2010-07-12
    /// </summary>
    public partial class SetLeaveCity : EyouSoft.Common.Control.BasePage
    {
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected string strSelectLeaveCity = string.Empty, strLeaveCity = string.Empty;
        protected string ReleaseType = string.Empty, ContainerID = string.Empty, GetType = string.Empty, Key = string.Empty, callBack = string.Empty, inputclass = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/Default.aspx");
            }
            else
            {
                if (SiteUserInfo != null)
                {
                    UserInfoModel = SiteUserInfo;
                }
            }
            if (!Page.IsPostBack)
            {
                GetType = Utils.GetQueryStringValue("GetType");
                Key = Utils.GetQueryStringValue("Key");
                callBack = Utils.GetQueryStringValue("callBack");
                inputclass = Utils.GetQueryStringValue("inputclass");
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
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(UserInfoModel.CompanyID);
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
            IList<EyouSoft.Model.SystemStructure.SysCity> list = bll.GetCityList(int.Parse(ProvinceID), 0, string.Empty, true, null);
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
                    if (bll.SetCompanyPortCity(UserInfoModel.CompanyID, list))
                    {
                        tmpVal = BindLeaveCity(Utils.GetQueryStringValue("GetType"));
                    }
                }
            }
            return tmpVal;
        }

        private string BindLeaveCity(string type)
        {
            //拼接字符串容器
            StringBuilder str = new StringBuilder();

            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();

            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(UserInfoModel.CompanyID);

            if (list != null && list.Count > 0)
            {
                //随机生成同组
                string nameval = Guid.NewGuid().ToString();
                switch (type)
                {
                    case "a":
                        foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
                        {
                            str.AppendFormat("<a class=\"a_City\" href=\"javascript:void(0);\" value=\"{0}\"cname=\"{1}\">{1}</a>", model.CityId, model.CityName);
                        }
                        break;
                    case "radio":
                        foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
                        {
                            str.AppendFormat("<input name={0} type=radio value={1} cname={2} class={3} />{2}", nameval, model.CityId, model.CityName, Utils.GetQueryStringValue("inputclass"));
                        }
                        break;

                }
                return str.ToString();
            }
            return string.Empty;
            //StringBuilder str1 = new StringBuilder();  // 当前公司注册时所在省份的出港城市
            //StringBuilder str2 = new StringBuilder();  // 设置过的出港城市

            //EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            //IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(UserInfoModel.CompanyID);
            //if (list != null && list.Count > 0)
            //{
            //    int i = 0;
            //    foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
            //    {
            //        if (model.ProvinceId == UserInfoModel.ProvinceId)
            //        {
            //            if (i == 0)
            //            {
            //                str1.AppendFormat("<label for=\"" + ReleaseType + "_radPortCity{0}\"><input type=\"radio\" name=\"" + ReleaseType + "_radPortCity\" id=\"" + ReleaseType + "_radPortCity{0}\" value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\"" + ReleaseType + "_radPortCity\" />{1}</label>", model.CityId, model.CityName);
            //            }
            //            else
            //            {
            //                str1.AppendFormat("<label for=\"" + ReleaseType + "_radPortCity{0}\"><input type=\"radio\" name=\"" + ReleaseType + "_radPortCity\" id=\"" + ReleaseType + "_radPortCity{0}\" value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
            //            }
            //        }
            //        else
            //        {
            //            if (i == 0)
            //            {
            //                str2.AppendFormat("<label for=\"" + ReleaseType + "_radPortCity{0}\"><input type=\"radio\" name=\"" + ReleaseType + "_radPortCity\" id=\"" + ReleaseType + "_radPortCity{0}\" value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\"" + ReleaseType + "_radPortCity\" />{1}</label>", model.CityId, model.CityName);
            //            }
            //            else
            //            {
            //                str2.AppendFormat("<label for=\"" + ReleaseType + "_radPortCity{0}\"><input type=\"radio\" name=\"" + ReleaseType + "_radPortCity\" id=\"" + ReleaseType + "_radPortCity{0}\" value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
            //            }
            //        }
            //        i++;
            //    }
            //}

            //if (str1.ToString() != string.Empty)
            //{
            //    if (str2.ToString() != string.Empty)
            //    {
            //        return str1.ToString() + "|" + str2.ToString(); //+ "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardTour&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
            //    }
            //    else
            //    {
            //        return str1.ToString();// +"| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardTour&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
            //    }
            //}
            //else
            //{
            //    if (str2.ToString() != string.Empty)
            //    {
            //        return str2.ToString();// +"| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardTour&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
            //    }
            //    else
            //    {
            //        return string.Empty;//"<a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardTour&ContainerID=" + ContainerID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
            //    }
            //}
        }
        #endregion
    }
}
