using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.StaticPage
{
    /// <summary>
    /// 孙川   2010-5-26 营销提交
    /// </summary>
    public partial class DoMarketDialog : EyouSoft.Common.Control.FrontPage
    {
        protected int type = 0;          //提交类型

        protected void Page_Load(object sender, EventArgs e)
        {
            string strSave = EyouSoft.Common.Utils.GetFormValue("hidSave");
            int sType = EyouSoft.Common.Utils.GetInt(Request.Form["hidType"], 0);
            if (!IsPostBack)
            {
                type = EyouSoft.Common.Utils.GetInt(Request.QueryString["type"]);
            }
            if (strSave == "save" && sType > 0)//保存
            {
                string strCompantyName = EyouSoft.Common.Utils.GetFormValue("txtCompanyName", 250);
                int provinceID = EyouSoft.Common.Utils.GetInt(Request.Form[ProvinceAndCityList1.FindControl("ddl_ProvinceList").UniqueID], 0);
                int cityID = EyouSoft.Common.Utils.GetInt(Request.Form[ProvinceAndCityList1.FindControl("ddl_CityList").UniqueID], 0);
                string strName = EyouSoft.Common.Utils.GetFormValue("txtName", 250);
                string strTel = EyouSoft.Common.Utils.GetFormValue("txtTel", 250);
                string strContent = EyouSoft.Common.Utils.GetFormValue("txtContent");

                EyouSoft.Model.CommunityStructure.MarketingCompany modelMarket = new EyouSoft.Model.CommunityStructure.MarketingCompany();
                modelMarket.CompanyName = strCompantyName;
                modelMarket.ProvinceId = provinceID;
                modelMarket.CityId = cityID;
                modelMarket.Contact = strName;
                modelMarket.ContactWay = strTel;
                modelMarket.Question = strContent;
                modelMarket.RegisterDate = DateTime.Now;
                modelMarket.MarketingCompanyType = (EyouSoft.Model.CommunityStructure.MarketingCompanyType)(sType);

                bool result = EyouSoft.BLL.CommunityStructure.MarketingCompany.CreateInstance().Add(modelMarket);

                if (result)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, string.Format("alert('提交成功！');window.parent.Boxy.getIframeDialog('{0}').hide();", EyouSoft.Common.Utils.GetQueryStringValue("iframeId")));
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, string.Format("alert('提交失败！');window.parent.Boxy.getIframeDialog('{0}').hide();", EyouSoft.Common.Utils.GetQueryStringValue("iframeId")));
                }
            }
        }
    }
}
