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
using System.Collections.Generic;

namespace IMFrame.Card
{
    public partial class LookUserInfo : Page
    {
        protected string ImageServerUrl =EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int MqId = 0;
                if (Request.QueryString["im_username"] != null)
                {
                    int.TryParse(Request.QueryString["im_username"].ToString(), out MqId);
                }
                if (MqId == 0)
                {
                    EyouSoft.Common.Function.MessageBox.Show(this.Page, "alert('未能找到您要查看的用户信息！')");
                    return;
                }
                EyouSoft.Model.CompanyStructure.CompanyAndUserInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(MqId);
                if (model != null)
                {
                    this.Title = model.User.ContactInfo.ContactName + "的个人详细信息";
                    lblName.Text = model.User.ContactInfo.ContactName;
                    lblCompanyName.Text = model.Company.CompanyName;
                    lblUserName.Text = model.User.UserName;
                    lblContactTel.Text = model.User.ContactInfo.Tel;
                    lblContactName.Text = model.User.ContactInfo.ContactName;
                    lblContactFax.Text = model.User.ContactInfo.Fax;
                    lblContactEmail.Text = model.User.ContactInfo.Email;
                    lblCompanyInfo.Text = model.Company.Remark;
                    lblLicense.Text = model.Company.License;
                    lblContactMobile.Text = model.User.ContactInfo.Mobile;
                    string AreaName = string.Empty;
                    if (model.User.Area != null && model.User.Area.Count > 0)
                    {
                        foreach (EyouSoft.Model.SystemStructure.AreaBase Area in model.User.Area)
                        {
                            AreaName += Area.AreaName+",";
                        }
                    }
                    lblAreaName.Text = AreaName.TrimEnd(',');
                }

                model = null;
            }
        }
    }
}
