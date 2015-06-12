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
    /// 景区动态、景区线路、 景区攻略、景区导游 列表
    /// Create:luofx  Date:2010-12-8
    /// </summary>
    public partial class ScenicInfo : EyouSoft.Common.Control.FrontPage
    {
        #region 变量
        protected string TabIndex = string.Empty;
        protected string TabName = string.Empty;
        protected string StrTitle = string.Empty;
        protected string CompanyId = string.Empty;
        private SeniorOnlineShop.master.SPOTT1TAB type = new SeniorOnlineShop.master.SPOTT1TAB();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {           
            CompanyId = ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CompanyId;
            TabIndex = Request.QueryString["st"];
            if (string.IsNullOrEmpty(TabIndex))
            {
                Response.Redirect("Default.aspx?cid="+CompanyId);
            }
            else{
                //导航样式制定
                type = (SeniorOnlineShop.master.SPOTT1TAB)Enum.Parse(typeof(SeniorOnlineShop.master.SPOTT1TAB), TabIndex);
                if (type == SeniorOnlineShop.master.SPOTT1TAB.景区攻略)
                {
                    ltrGL.Visible = true;
                    dplType.Visible = true;                    
                    dplType.Items.Insert(0, new ListItem("请选择", ""));
                    dplType.Items.Insert(1, new ListItem("攻略", ((int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略).ToString()));
                    dplType.Items.Insert(2, new ListItem("美食", ((int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区美食).ToString()));
                    dplType.Items.Insert(3, new ListItem("住宿", ((int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿).ToString()));
                    dplType.Items.Insert(4, new ListItem("交通", ((int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区交通).ToString()));
                    dplType.Items.Insert(5, new ListItem("购物", ((int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区购物).ToString()));
                    if (!string.IsNullOrEmpty(Request.QueryString["gl"]) && Request.QueryString["gl"] != "undefined")
                    {
                        int itemVal = Utils.GetInt(Request.QueryString["gl"], (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略);
                        dplType.Items.FindByValue(itemVal.ToString()).Selected = true;
                    }
                }
                ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CTAB = type;
                if (TabIndex == "0")
                {
                    TabName = "景区线路";
                }
                else
                {
                    TabName = Enum.GetName(typeof(SeniorOnlineShop.master.SPOTT1TAB), (int)type);
                }

            }
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
            
        }       
    }
}
