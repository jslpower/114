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
using System.Collections.Generic;

namespace UserPublicCenter.AirTickets
{
    
    /// <summary>
    /// 机票登录页面，张新兵，20101019
    /// </summary>
    public partial class Login : EyouSoft.Common.Control.FrontPage
    {
        protected string FiveImagesNumber = "";
        protected string FiveAdvImages = "";
        protected string goToUrl = "/AirTickets/Default.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            AddMetaTag("description", PageTitle.Plane_Des);
            AddMetaTag("keywords", PageTitle.Plane_Keywords);
            this.Page.Title = PageTitle.Plane_Title;

            this.AddStylesheetInclude(CssManage.GetCssFilePath("gouwu"));
            this.AddStylesheetInclude(CssManage.GetCssFilePath("jipiaostyle"));
            this.AddStylesheetInclude(CssManage.GetCssFilePath("body"));
            this.AddJavaScriptInclude(JsManage.GetJsFilePath("jquery"), false, false);

            //初始化  现有运价数 ,平台用户数
            int freightCount = 0,platformUserCount = 0;
            EyouSoft.Model.SystemStructure.SummaryCount scount =
                EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            if (scount != null)
            {
                freightCount = scount.TicketFreightVirtual + scount.TicketFreight;
                platformUserCount = scount.User + scount.UserVirtual;
            }

            ltrFreightCount.Text = freightCount.ToString();
            ltrPlatformUserCount.Text = platformUserCount.ToString();

                       
            if(!IsPostBack)
            {
                string newUrl = Utils.GetQueryStringValue("return");
                if (newUrl == "PlaneListPage")
                {
                    goToUrl = "/PlaneInfo/PlaneListPage.aspx";
                }
                //供应商信息
                BindTicketAgu();

                //初始化  运价参考 列表
                BindTicketInfo();
            }            

            //五张轮换广告
            InitAdv();
            
        }
        /// <summary>
        /// 五张轮换广告
        /// </summary>
        protected void InitAdv()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.机票频道焦点广告翻屏);
            int Count = 1;
            if (AdvList != null && AdvList.Count > 0)
            {

                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    FiveImagesNumber += string.Format("<li>{0}</li>", Count);
                    Count++;
                    FiveAdvImages += string.Format("<li><a href=\"{1}\" {2}><img src=\"{0}\" height=\"219\" width=\"640\" /></a></li>", EyouSoft.Common.Domain.FileSystem + item.ImgPath, item.RedirectURL, item.RedirectURL == Utils.EmptyLinkCode ? "" : "target=\"_blank\"");
                }
            }
            AdvList = null;
        }

        /// <summary>
        /// 运价参考绑定
        /// </summary>
        protected void BindTicketInfo()
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelList = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(10, EyouSoft.Model.SystemStructure.AfficheType.运价参考);
            if (ModelList != null && ModelList.Count > 0)
            {
                this.rptNewList.DataSource = ModelList;
                this.rptNewList.DataBind();
            }
            //释放资源
            ModelList = null;
        }

        #region 获取列表标题信息
        protected string ShowTicketInfo(int AffiID, string AffiName, int typeID)
        {
            string returnVal = "";
            string linkUrl = Utils.GetWordAdvLinkUrl(0, AffiID, typeID, CityId);
            switch (typeID)
            {
                case 0://运价参考
                    returnVal = string.Format("<li><a href='{0}' title='{2}'  target='_blank'>{1}</a></li>", linkUrl, Utils.GetText(AffiName, 20, true), AffiName);
                    break;
                case 1://帮助信息
                    returnVal = string.Format("<li>·<a href='{0}' title='{2}' target='_blank'>{1}</a></li>", linkUrl, Utils.GetText(AffiName, 13, true), AffiName);
                    break;
                default://合作供应商信息
                    returnVal = string.Format("<li><a href='{0}' title='{2}' target='_blank'>{1}</a></li>", linkUrl, Utils.GetText(AffiName, 20, true), AffiName);
                    break;
            }
            return returnVal;
        }

        /// <summary>
        /// 绑定供应商信息
        /// </summary>
        public void BindTicketAgu()
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> ModelTicket = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(8, EyouSoft.Model.SystemStructure.AfficheType.合作供应商);
            if (ModelTicket != null && ModelTicket.Count > 0)
            {
                this.dal_PlaneAgu.DataSource = ModelTicket;
                this.dal_PlaneAgu.DataBind();
            }
            ModelTicket = null;
        }

        #endregion
    }


}
