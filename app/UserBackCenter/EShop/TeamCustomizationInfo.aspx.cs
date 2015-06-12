using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店-团队定制详细信息
    ///Create: luofx  Date:2010-11-15
    /// </summary>
    public partial class TeamCustomizationInfo : System.Web.UI.Page
    {
        #region protected 变量
        //     用车要求
        protected string CarContent = string.Empty;
        //     所属公司编号
        protected string CompanyId = string.Empty;
        //     单位名称
        protected string ContactCompanyName = string.Empty;
        //     联系人
        protected string ContactName = string.Empty;
        //     电话
        protected string ContactTel = string.Empty;
        //     用餐要求
        protected string DinnerContent = string.Empty;
        //     导游要求
        protected string GuideContent = string.Empty;
        //     添加时间
        protected string IssueTime = string.Empty;
        //     操作人编号
        protected string OperatorId = string.Empty;
        //     其它个性要求
        protected string OtherContent = string.Empty;
        //     计划日期
        protected string PlanDate = string.Empty;
        //     计划人数
        protected int PlanPeopleNum = 0;
        //     住宿要求
        protected string ResideContent = string.Empty;
        //     购物要求
        protected string ShoppingInfo = string.Empty;
        //     行程要求
        protected string TravelContent = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            int TeamId = EyouSoft.Common.Utils.GetInt(Request.QueryString["id"]);
            if (TeamId > 0)
            {
                EyouSoft.Model.ShopStructure.RouteTeamCustomization model = EyouSoft.BLL.ShopStructure.RouteTeamCustomization.CreateInstance().GetModel(TeamId);
                CarContent = model.CarContent;
                ContactCompanyName = model.ContactCompanyName;
                ContactName = model.ContactName;
                ContactTel = model.ContactTel;
                DinnerContent = model.DinnerContent;
                GuideContent = model.GuideContent;
                IssueTime = model.IssueTime.ToShortDateString();
                OtherContent = model.OtherContent;
                PlanDate = model.PlanDate.ToShortDateString();
                PlanPeopleNum = model.PlanPeopleNum;
                ResideContent = model.ResideContent;
                ShoppingInfo = model.ShoppingInfo;
                if (!string.IsNullOrEmpty(model.TravelContent))
                {
                    TravelContent = EyouSoft.Common.Domain.FileSystem + model.TravelContent;
                }
                model = null;
            }
        }
    }
}
