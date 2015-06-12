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

namespace SeniorOnlineShop.seniorshop
{
    public partial class ZiYuan : EyouSoft.Common.Control.FrontPage
    {
        private int intPageSize = 30;
        private int intRecordCount = 0;
        private int PageIndex = 0;
        protected string CompanyId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompanyId=this.Master.CompanyId;
                //if(Request.QueryString["cid"]==Master.CompanyId)
                //{
                string keyword = Utils.GetQueryStringValue("k");
                    InitResource(keyword);   
                //}
                    Page.Title = "旅游资源推荐";
            }  
        }
        /// <summary>
        /// 初始化资源信息
        /// </summary>
        private void InitResource(string keyword)
        {   
            PageIndex =Utils.GetInt(Request.QueryString["Page"],1);
            IList<EyouSoft.Model.ShopStructure.HighShopResource> list = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().GetList(intPageSize, PageIndex, ref intRecordCount, Master.CompanyId, keyword);
            if (list != null && list.Count > 0)
            {
                this.rptZiYuanList.DataSource = list;
                this.rptZiYuanList.DataBind();              

                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;//每页显示记录数
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.LinkType = 3;
                resoNoMessage.Visible = false;
                list = null;
            }
            else
            {
                resoNoMessage.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

        public string ValiaImage(string imgPath)
        {
            if (!string.IsNullOrEmpty(imgPath))
            {
                return  Domain.FileSystem+imgPath;
            }
            else
                return Domain.ServerComponents + "/images/seniorshop/nopic.jpg";
        }
        
        protected void rptZiYuanList_ItemCreated(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                string Id = DataBinder.Eval(e.Item.DataItem, "ID").ToString();
                HtmlAnchor link = e.Item.FindControl("linkZiYuan") as HtmlAnchor;
                if (link != null)
                {
                    link.Title = DataBinder.Eval(e.Item.DataItem, "Title").ToString();
                    link.HRef = Utils.GenerateShopPageUrl2("/ZiYuanShow_" + Id,Master.CompanyId);
                    HtmlImage img = new HtmlImage ();
                    img.Src =Utils.GetLineShopImgPath(DataBinder.Eval(e.Item.DataItem, "ImagePath").ToString(),4);
                    img.Height=82;
                    img.Width=123;
                    img.Border=0;                    
                    link.Controls.Add(img);
                }
                HtmlAnchor link1 = e.Item.FindControl("linkZiYuan1") as HtmlAnchor;
                if (link1 != null)
                {
                    link1.InnerText = Utils.GetText( Utils.InputText( DataBinder.Eval(e.Item.DataItem, "Title").ToString()),9);
                    link1.HRef = Utils.GenerateShopPageUrl2("/ZiYuanShow_" + Id,Master.CompanyId);
                }

            }
        }
    }
}
