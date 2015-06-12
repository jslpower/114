using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景区动态、景区线路、 景区攻略、景区导游列表AJAX
    /// Create:luofx  Date:2010-12-15
    /// </summary>
    public partial class AjaxScenicInfoList : System.Web.UI.Page
    {
        protected string TabIndex = string.Empty;
        protected string TabName = string.Empty;
        protected string StrTitle = string.Empty;
        protected string CompanyId = string.Empty;
        private int intPageSize = 16;
        private int CurrencyPage = 1;
        private int intRecordCount = 0; //总记录数
        private SeniorOnlineShop.master.SPOTT1TAB type = new SeniorOnlineShop.master.SPOTT1TAB();
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = Request.QueryString["cid"];
            TabIndex = Request.QueryString["st"];
            type = (SeniorOnlineShop.master.SPOTT1TAB)Enum.Parse(typeof(SeniorOnlineShop.master.SPOTT1TAB), TabIndex);
            StrTitle = EyouSoft.Common.Utils.GetQueryStringValue("title").Trim();
            CurrencyPage = StringValidate.GetIntValue(Request.QueryString["Page"], 1);
            InitPage();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] tripguidetype = { };
            switch (type)
            {
                case SeniorOnlineShop.master.SPOTT1TAB.景区动态:
                    tripguidetype = new EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] { EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区动态 };
                    break;
                case SeniorOnlineShop.master.SPOTT1TAB.景区攻略:
                    if (!string.IsNullOrEmpty(Request.QueryString["gl"]) && Request.QueryString["gl"] != "undefined")
                    {
                        tripguidetype = new EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] { (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)Enum.Parse(typeof(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType), Request.QueryString["gl"]) };
                    }
                    else
                    {
                        tripguidetype = new EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] { EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区购物, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区交通, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区美食, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿 };
                    }
                    break;
                case SeniorOnlineShop.master.SPOTT1TAB.景区导游:
                    tripguidetype = new EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] { EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区导游 };
                    break;
                default:
                    TabName = "景区线路";
                    tripguidetype = new EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] { EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区线路 };
                    break;
            }
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(intPageSize, CurrencyPage, ref intRecordCount, CompanyId, StrTitle, tripguidetype);
            if (list != null && list.Count > 0)
            {
                this.rptData.DataSource = list;
                this.rptData.DataBind();
                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.PageLinkURL = "ScenicInfoList.aspx?";
                this.ExporPageInfoSelect1.LinkType = 3;
            }
            else
            {
                NoData.Visible = true;
            }
            list = null;
        }
        /// <summary>
        /// 数据绑定时处理特殊情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrXH = (Literal)e.Item.FindControl("ltrXH");
                if (ltrXH != null)
                    ltrXH.Text = Convert.ToString(intPageSize * (CurrencyPage - 1) + (e.Item.ItemIndex + 1));
                EyouSoft.Model.ShopStructure.HighShopTripGuide model = (EyouSoft.Model.ShopStructure.HighShopTripGuide)e.Item.DataItem;
                int typeId = (int)model.TypeID;
                if (typeId >= 8 && typeId <= 12)
                {
                    Literal ltrType = (Literal)e.Item.FindControl("ltrType");
                    if (ltrType != null)
                        ltrType.Text = "<a><font class=\"C_blue\">[" + model.TypeID.ToString().Substring(2) + "]</font></a>";
                }
                model = null;
            }
        }
    }
}
