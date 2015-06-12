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

namespace SeniorOnlineShop.seniorshop
{
    public partial class MuDiDis : EyouSoft.Common.Control.FrontPage
    {
        protected string Guide1Html = string.Empty;
        protected string Guide2Html = string.Empty;
        protected string Guide3Html = string.Empty;
        //private string topNo1 = "<div {3}><table width=\"96%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"maintop5\"style=\"margin-bottom: 5px;\"><tr><td><a href=\"{0}\"><img src=\"{1}\" width=\"97\" height=\"73\" border=\"0\" /></a></td><td style=\"padding: 8px; text-align: left;\"><a href=\"{0}\" class=\"huizi\">&nbsp;&nbsp;&nbsp;&nbsp;{2}</a></td></tr></table></div>";
        //private string Other = "<div {3}><table  onmouseover=\"showonmouseover('{5}','{4}')\" width=\"96%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td class=\"linenew\">•&nbsp;<a href=\"{0}\" class=\"huizi\">{1}</a><td class=\"linenew\" align=\"right\"><span class=\"hui\">【{2} 】</span></td></td></tr></table></div>";

        private string topNo1 = "<table width=\"96%\" border=\"0\"cellpadding=\"0\" cellspacing=\"0\" class=\"maintop5\"style=\"margin-bottom: 5px;margin-left:5px;\"><tr><td width='17.5%'><a href=\"{0}\"><img alt=\"点击查看详细\" src=\"{1}\" width=\"97\" height=\"73\" border=\"0\" /></a></td><td style=\"padding: 8px; text-align: left;\" align=\"left\">&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"{0}\" class=\"huizi\">{2}</a></td></tr></table>";
        private string Other = "<table width=\"96%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td class=\"linenew\">•&nbsp;<a href=\"{0}\" class=\"huizi\">{1}</a><td class=\"linenew\" align=\"right\"><span class=\"hui\">【{2} 】</span></td></td></tr></table>";
        
        private string NoPicturePath =Domain.ServerComponents+"images/seniorshop/nopic.jpg";
        protected string CompanyId = string.Empty;
        protected string ArryId1 = string.Empty;
        protected string ArryId2 = string.Empty;
        protected string ArryId3 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
  
            if (!IsPostBack)
            {
                CompanyId = this.Master.CompanyId;
                string keyword =Utils.InputText(Request.QueryString["k"]);
              ShowGuides(keyword);
              Page.Title ="出游指南";
            }
        }

        private void ShowGuides(string keywords)
        {
          //  string idStr = "style=\"display:{0};\" id=\"{1}\"";
            EyouSoft.IBLL.ShopStructure.IHighShopTripGuide bll = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance();
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = null;
            
            #region  风土人情
            list = bll.GetWebList(6, Master.CompanyId, 1,keywords);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                   // ArryId1 += list[i].ID + ",";
                    string url = Utils.GenerateShopPageUrl2("/MuDiDi_" + list[i].ID, Master.CompanyId);
                    //if (i == 0)
                    //{
                    //    Guide1Html = string.Format(topNo1, url, GetImagePath(list[i].ImagePath), GetNewsTitle(list[i].ContentText, 220),string.Format(idStr,"block","show"+list[i].ID))+ string.Format(Other, url, GetNewsTitle(list[i].Title, 25), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format(idStr, "none", list[i].ID), 1,list[i].ID);
                       
                    //}
                    //else
                    //{
                    //    Guide1Html += string.Format(topNo1, url, GetImagePath(list[i].ImagePath), GetNewsTitle(list[i].ContentText, 220), string.Format(idStr, "none", "show" + list[i].ID))+ string.Format(Other, url, GetNewsTitle(list[i].Title, 25), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format(idStr, "block", list[i].ID), 1, list[i].ID);
                    //}
                    if (i == 0)
                    {
                        Guide1Html = string.Format(topNo1, url, Utils.GetLineShopImgPath(list[i].ImagePath,5), Utils.GetText(Utils.InputText( list[i].ContentText), 220));
                    }
                    else
                    {
                        Guide1Html += string.Format(Other, url, Utils.GetText(Utils.InputText( list[i].Title), 86), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"));
                    }
                }
            }
            else
            {
                Guide1Html = "<div style=\"text-align:center;margin-top:75px; margin-bottom:75px;\">暂无风土人情信息！</div>";
            }
            list = null;
            #endregion

            #region 温馨提醒
            list = bll.GetWebList(6, Master.CompanyId, 2,keywords);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                   // ArryId2 += list[i].ID + ",";
                    string url = Utils.GenerateShopPageUrl2("/MuDiDi_" + list[i].ID, Master.CompanyId);
                    //if (i == 0)
                    //{
                    //    Guide2Html= string.Format(topNo1, url, GetImagePath(list[i].ImagePath), GetNewsTitle(list[i].ContentText, 220), string.Format(idStr, "block", "show" + list[i].ID))+ string.Format(Other, url, GetNewsTitle(list[i].Title, 25), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format(idStr, "none", list[i].ID), 2, list[i].ID);
                    //}
                    //else
                    //{
                    //    Guide2Html += string.Format(topNo1, url, GetImagePath(list[i].ImagePath), GetNewsTitle(list[i].ContentText, 220), string.Format(idStr, "none", "show" + list[i].ID))+ string.Format(Other, url, GetNewsTitle(list[i].Title, 25), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format(idStr, "block", list[i].ID), 2, list[i].ID);
                    //}
                    if (i == 0)
                    {
                        Guide2Html = string.Format(topNo1, url, Utils.GetLineShopImgPath(list[i].ImagePath,5), Utils.GetText(Utils.InputText( list[i].ContentText), 220));
                    }
                    else
                    {
                        Guide2Html += string.Format(Other, url, Utils.GetText(Utils.InputText(list[i].Title), 44), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"));
                    }
                }
            }
            else
            {
                Guide2Html = "<div style=\"text-align:center;margin-top:75px; margin-bottom:75px;\">暂无温馨提醒信息！</div>";
            }
            list = null;
            #endregion

            #region 综合介绍 
            list = bll.GetWebList(6, Master.CompanyId, 3,keywords);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                   // ArryId3 += list[i].ID + ",";
                    string url = Utils.GenerateShopPageUrl2("/MuDiDi_" + list[i].ID, Master.CompanyId);
                    //if (i == 0)
                    //{
                    //    Guide3Html = string.Format(topNo1, url, GetImagePath(list[i].ImagePath), GetNewsTitle(list[i].ContentText, 220), string.Format(idStr, "block", "show" + list[i].ID))+ string.Format(Other, url, GetNewsTitle(list[i].Title, 25), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format(idStr, "none", list[i].ID), 3, list[i].ID);
                    //}
                    //else
                    //{
                    //    Guide3Html += string.Format(topNo1, url, GetImagePath(list[i].ImagePath), GetNewsTitle(list[i].ContentText, 220), string.Format(idStr, "none", "show" + list[i].ID))+string.Format(Other, url, GetNewsTitle(list[i].Title, 25), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format(idStr, "block", list[i].ID), 3, list[i].ID);
                    //}

                    if (i == 0)
                    {
                        Guide3Html = string.Format(topNo1, url, Utils.GetLineShopImgPath(list[i].ImagePath,5), Utils.GetText(Utils.InputText( list[i].ContentText), 220));
                    }
                    else
                    {
                        Guide3Html += string.Format(Other, url, Utils.GetText(Utils.InputText( list[i].Title), 44), list[i].IssueTime.ToString("yyyy-MM-dd HH:mm"));
                    }
                }
            }
            else
            {
                Guide3Html = "<div style=\"text-align:center;margin-top:75px; margin-bottom:75px;\">暂无综合介绍信息！</div>";
            }
            list = null;
            #endregion
        }
    }
}
