using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 高级网店-团队定制
    ///Create: luofx  Date:2010-11-11
    /// </summary>
    public partial class TeamCustomization : EyouSoft.Common.Control.FrontPage
    {
        protected string CompanyId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //导航样式制定
            ((SeniorOnlineShop.master.T4)base.Master).CTAB = SeniorOnlineShop.master.T4TAB.团队定制;
            CompanyId = ((SeniorOnlineShop.master.T4)base.Master).CompanyId;
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "add")
            {
                Add();
            }
        }
        /// <summary>
        /// 添加团队制定信息
        /// </summary>
        private void Add()
        {
            string PlanDate = Request.Form["PlanDate"];
            string ContactTel = Request.Form["ContactTel"];
            string ContactCompanyName = Request.Form["ContactCompanyName"];
            string ContactName = Request.Form["ContactName"];
            int PlanPeopleNum = Utils.GetInt(Request.Form["PlanPeopleNum"]);
            #region 验证
            string StrErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(PlanDate))
            {
                StrErrorMsg += "-计划日期不能为空！\n";
            }
            if (string.IsNullOrEmpty(ContactTel))
            {
                StrErrorMsg += "-联系电话不能为空！\n";
            }
            if (string.IsNullOrEmpty(ContactCompanyName))
            {
                StrErrorMsg += "-单位名称不能为空! \n";
            }
            if (string.IsNullOrEmpty(ContactName))
            {
                StrErrorMsg += "-联系人不能为空！\n";
            }
            if (PlanPeopleNum < 1)
            {
                StrErrorMsg += "-计划人数必须大于0！\n";
            }
            if (!string.IsNullOrEmpty(StrErrorMsg))
            {
                StrErrorMsg = "[{isSuccess:false,Message:'" + StrErrorMsg + "'}]";
                Response.Clear();
                Response.Write(StrErrorMsg);
                Response.End();
            }
            #endregion
            EyouSoft.Model.ShopStructure.RouteTeamCustomization model = new EyouSoft.Model.ShopStructure.RouteTeamCustomization();
            model.CarContent = Utils.GetFormValue("CarContent");
            model.CompanyId = CompanyId;
            model.ContactCompanyName = Utils.GetFormValue("ContactCompanyName");
            model.ContactName = Utils.GetFormValue("ContactName");
            model.ContactTel = Utils.GetFormValue("ContactTel");
            model.DinnerContent = Utils.GetFormValue("DinnerContent");
            model.GuideContent = Utils.GetFormValue("GuideContent");
            model.IssueTime = DateTime.Now;
            if (this.SiteUserInfo != null)
            {
                model.OperatorId = this.SiteUserInfo.ID;
            }
            else
            {
                model.OperatorId = "";
            }
            model.OtherContent = Utils.GetFormValue("OtherContent");
            model.PlanDate = Utils.GetDateTime(PlanDate);
            model.PlanPeopleNum = PlanPeopleNum;
            model.ResideContent = Utils.GetFormValue("ResideContent");
            model.ShoppingInfo = Utils.GetFormValue("ShoppingInfo");
            model.TravelContent = Utils.GetFormValue("ctl00$MainPlaceHolder$SingleFileUpload1$hidFileName");
            bool IsTrue = EyouSoft.BLL.ShopStructure.RouteTeamCustomization.CreateInstance().Add(model);
            if (IsTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,Message:'提交成功，我们将尽快联系你'}]");
                Response.End();
            }
        }
    }
}
