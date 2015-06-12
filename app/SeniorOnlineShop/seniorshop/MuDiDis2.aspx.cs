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
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.IO;

namespace SeniorOnlineShop.seniorshop
{
    public partial class MuDiDis2 : EyouSoft.Common.Control.FrontPage
    {
        protected string GuideHtml = string.Empty;     
        protected string guidType = string.Empty;
        private string NoPicturePath =Domain.ServerComponents+ "/images/seniorshop/nopic.jpg";
        public int intPageSize = 20;
        public int CurrencyPage = 1;
        private int intRecordCount = 0; //总记录数
        protected string CompanyId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int TypeId = Utils.GetInt(Request.QueryString["TypeId"]);
                if (TypeId == 0)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "window.history.back().location.reload();");
                }
                else
                {
                    ShowGuides(TypeId);
                }
                CompanyId = Master.CompanyId;
            }
            
        }
        private void ShowGuides(int TypeId)
        {
            EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType tripguidetype = EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情;
            switch (TypeId)
            {
                case 1:
                   tripguidetype= EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情;
                    break;
                case 2:
                    tripguidetype = EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒;
                    break;
                case 3:
                    tripguidetype = EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍;
                    break;
            }
            guidType = tripguidetype.ToString();
                CurrencyPage =StringValidate.GetIntValue(Request.QueryString["Page"],1);
                IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(intPageSize, CurrencyPage, ref intRecordCount, Master.CompanyId, "", tripguidetype);
            if (list != null && list.Count > 0)
            {
                this.rptGuides.DataSource = list;
                this.rptGuides.DataBind();
                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.LinkType = 3;

            }
            else
            {
                GuideHtml = "<div style=\"text-align:center;margin-top:75px; margin-bottom:75px;\">暂无风土人情信息！</div>";
            }
            list = null;
        }
        #region 邦定时触发事件，判断是否为第一条数据项,如果是则显示图片
        protected void repeater_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //如果邦定的是第一条数据，则显示缩略图和标题
            if (e.Item.ItemIndex == 0)
            {
                EyouSoft.Model.ShopStructure.HighShopTripGuide guidInfo = e.Item.DataItem as EyouSoft.Model.ShopStructure.HighShopTripGuide;
                HtmlTable tableTitleModel = (HtmlTable)e.Item.FindControl("table1");

                tableTitleModel.Rows[0].Cells[0].InnerHtml = string.Format("• <a href=\"{0}\" class=\"huizi\">{1}</a>", Utils.GenerateShopPageUrl2("/MuDiDi_" + guidInfo.ID, Master.CompanyId), guidInfo.ID, Utils.GetText(Utils.InputText( guidInfo.Title), 200));
                tableTitleModel.Rows[0].Cells[1].InnerHtml = string.Format("<span class=\"hui\">【{0} 】</span>", guidInfo.IssueTime.ToString("yyyy-MM-dd"));
                tableTitleModel.Style.Add("display", "none");

                guidInfo.ImagePath=Utils.GetLineShopImgPath(guidInfo.ImagePath,5);

                string innerHTML = "<table width=\"96%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"  class=\"maintop5\" style=\"margin-bottom:5px;overflow:hidden\" id=\"table" + guidInfo.ID+ "\" >" +
              "<tr>" +
                "<td width=\"16%\" style=\"padding-top:3px;\"><a href=\"" + Utils.GenerateShopPageUrl2("/MuDiDi_" + guidInfo.ID, Master.CompanyId) + "\"><img src=\"" + guidInfo.ImagePath + "\" width=\"97\" height=\"73\" border=\"0\" /></a></td>" +
                "<td style=\"padding:8px; text-align:left;vertical-align:top;word-wrap:break-word\" width=\"84%\">" +
                "<div style=\"width:520px;overflow-x:hidden;word-wrap:break-word;\"><a  href=\"" + Utils.GenerateShopPageUrl2("/MuDiDi_" + guidInfo.ID, Master.CompanyId) + "\" class=\"huizi\">&nbsp;&nbsp;&nbsp;&nbsp;" + Utils.GetText(Utils.InputText( guidInfo.ContentText), 200) + "</a></div></td>" +
              "</tr>" +
          "</table>";
                LiteralControl c = new LiteralControl(innerHTML);
                e.Item.Controls.Add(c);
            }
        }
        #endregion
        protected string GetNewTitle(string text, int maxLength)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            else
            {
                if (text.Length >= maxLength)
                {
                    return Utils.InputText(text).Substring(0, maxLength) + "...";
                }
                else
                {
                    return text;
                }
            }
        }
    }
}
