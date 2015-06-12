using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace IMFrame.Search
{
    /// <summary>
    /// 查找公司
    /// 开发人：刘玉灵  时间：2009-11-16
    /// </summary>
    public partial class SearchFriend : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!IsCompanyCheck)
                {
                    tbIsCheck.Visible = true;
                }

                ChangeCheckType();
                CheckCity();
                //当前人数
                GetMQUserCount();

                #region 获得返回的url参数

                //说明是从列表页面返回的
                if (Utils.InputText(Request.QueryString["IsBack"]) == "1")
                {
                    string CompanyName = Utils.InputText(Server.UrlDecode(Request.QueryString["CompanyName"]));
                    string ContactName = Utils.InputText(Server.UrlDecode(Request.QueryString["ContactName"]));
                    int TypeId = Utils.GetInt(Request.QueryString["TypeId"], -1);
                    int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
                    int CityId = Utils.GetInt(Request.QueryString["CityId"]);
                    string LookUserName = Utils.InputText(Server.UrlDecode(Request.QueryString["UserName"]));
                    int UserId = Utils.GetInt(Request.QueryString["UserId"]);

                    //初始化数据
                    if (!string.IsNullOrEmpty(CompanyName))
                        this.txtCompanyName.Text = CompanyName;
                    if (!string.IsNullOrEmpty(ContactName))
                        this.txtContactName.Text = ContactName;
                    if (!string.IsNullOrEmpty(LookUserName))
                        this.txtUserName.Text = LookUserName;
                    if (UserId > 0)
                        this.txtUserId.Text = UserId.ToString();
                    if (ProvinceId > 0)
                        ProvinceAndCityList1.SetProvinceId = ProvinceId;
                    if (CityId > 0)
                        ProvinceAndCityList1.SetCityId = CityId;
                    StringBuilder strJs = new StringBuilder();
                    if (TypeId >= 0)
                    {
                        strJs.AppendFormat("$('#radCompanyType_{0}').attr('checked',true);", TypeId);
                        strJs.Append("SearchFriend.changeCompanyType();");
                        MessageBox.ResponseScript(this, strJs.ToString());
                    }

                }

                #endregion
            }
        }

        #region 获的当前平台用户人数

        /// <summary>
        ///当前平台用户人数
        /// </summary>
        private void GetMQUserCount()
        {
            EyouSoft.Model.SystemStructure.SummaryCount model = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            if (model != null)
                this.labSumNumber.Text = (model.MQUser + model.MQUserVirtual).ToString();
            model = null;
        }

        #endregion

        #region 根据登陆人帐号类型初始化查找方式

        /// <summary>
        /// 根据登陆人帐号类型初始化查找方式
        /// </summary>
        protected void ChangeCheckType()
        {
            int ChangTypeId = 0;
            StringBuilder strCompanyType = new StringBuilder();
            if (SiteUserInfo != null && SiteUserInfo.CompanyRole != null && SiteUserInfo.CompanyRole.RoleItems != null && SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                if (SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.专线) || SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.组团) || SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                {
                    if (SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                    {
                        ChangTypeId = 2;
                    }
                    else if (SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        ChangTypeId = 1;
                    }
                    else if (SiteUserInfo.CompanyRole.RoleItems.Contains(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                    {
                        ChangTypeId = 1;
                        strCompanyType.Append("SearchFriend.SetLocalSearchType();");
                    }
                }
                strCompanyType.AppendFormat("$('#radCompanyType_{0}').attr('checked',true);", ChangTypeId);
                strCompanyType.Append("SearchFriend.changeCompanyType();");
                MessageBox.ResponseScript(this, strCompanyType.ToString());
            }
        }

        #endregion

        #region 根据IP默认选择所在地省份城市

        /// <summary>
        /// 根据IP默认选择所在地省份城市
        /// </summary>
        private void CheckCity()
        {
            string Remote_IP = "";
            if (Request != null)
                Remote_IP = Request.UserHostAddress;
            //if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            //{
            //    Remote_IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            //}
            //else
            //{
            //    Remote_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //}

            EyouSoft.Model.SystemStructure.CityBase cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetClientCityByIp(Remote_IP);
            if (cityModel == null || cityModel.CityId <= 0)
            {
                //返回实体为空，默认浙江杭州
                ProvinceAndCityList1.SetProvinceId = 33;
                ProvinceAndCityList1.SetCityId = 362;
            }
            else if (cityModel != null && cityModel.CityId > 0)
            {
                ProvinceAndCityList1.SetProvinceId = cityModel.ProvinceId;
                ProvinceAndCityList1.SetCityId = cityModel.CityId;
            }
            cityModel = null;
        }

        #endregion
    }

}
