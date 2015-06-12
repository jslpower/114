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
namespace UserPublicCenter.Register
{
    /// <summary>
    /// 注册用户信息完善
    /// </summary>
    public partial class PerfectInfo : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "完善企业信息";
            }
            if (IsLogin)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (Model != null)
                {
                    bool isRouteAgency = SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                    string userManageUrl = null;//用户后台URL
                    if (Model.CompanyRole.Length == 0)//随便逛逛用户
                    {
                        Response.Redirect("~/Default.aspx");
                        return;
                    }
                    else
                    {
                        //进入后台管理的链接
                       
                        if (IsAirTicketSupplyUser)//如果是机票供应商用户
                        {
                            userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/TicketsCenter/";
                            trCuperous.Visible = true;
                        }
                        else//如果是旅行社用户
                        {
                            userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/";
                        }
                    }
                    hfdBackUrl.Value = userManageUrl;
                    Model = null;
                }
            }
                else
                {
                    Response.Redirect("Login.aspx");
                }
        }
        //保存并跳转
        protected void btnFulInBack_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);

             EyouSoft.Model.CompanyStructure.CompanyLogo logoModel = new EyouSoft.Model.CompanyStructure.CompanyLogo();
             logoModel.ImagePath = Utils.GetFormValue("ctl00$Main$sfuLogo$hidFileName");
             EyouSoft.Model.CompanyStructure.BusinessCertif busiModel = new EyouSoft.Model.CompanyStructure.BusinessCertif();
             busiModel.LicenceImg = Utils.GetFormValue("ctl00$Main$sfuYyzzImg$hidFileName");

             model.AttachInfo.BusinessCertif = busiModel;             
             model.AttachInfo.CompanyLogo = logoModel;

             model.Remark = Utils.EditInputText(Request.Form["txaCompanyDetail"]);

            if (trCuperous.Visible)
            {
                //机票铜牌
                model.AttachInfo.Bronze = Utils.GetFormValue("ctl00$Main$sfuCupreousImg$hidFileName");
            }
            
           EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().UpdateSelf(model);
            if (string.IsNullOrEmpty(hfdBackUrl.Value))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect(hfdBackUrl.Value);
            }
        }
    }
}
