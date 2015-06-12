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
    /// 景区动态、景区线路、 景区攻略、景区导游 详细页
    /// Create:luofx  Date:2010-12-8
    /// </summary>
    public partial class ScenicInfoDetail : EyouSoft.Common.Control.FrontPage
    {
        #region 变量
        protected string TabIndex = string.Empty;
        protected string TabName = string.Empty;
        protected string ImagePath = string.Empty;
        protected string StrTitle = string.Empty;
        protected string ContentText = string.Empty;
        protected string IssueTime = string.Empty;
        protected string CompanyId = string.Empty;
        /// <summary>
        /// 是否为门票政策
        /// </summary>
        protected bool IsAdmissionPolicy = false;
        private SeniorOnlineShop.master.SPOTT1TAB type = new SeniorOnlineShop.master.SPOTT1TAB();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //导航样式制定
            TabIndex = Request.QueryString["st"];
            CompanyId = ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CompanyId;
            if (string.IsNullOrEmpty(TabIndex))
            {
                Response.Redirect("/scenicspots/T1/Default.aspx?cid=" + CompanyId);
            }
            else
            {
                type = (SeniorOnlineShop.master.SPOTT1TAB)Enum.Parse(typeof(SeniorOnlineShop.master.SPOTT1TAB), TabIndex);
                ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CTAB = type;
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
            string SecnicId = Request.QueryString["id"];
            //if (type == SeniorOnlineShop.master.SPOTT1TAB.门票政策)
            //{
            //    TabName = "门票政策";
            //    likReturn.HRef = "ScenicInfoDetail.aspx?st=4&cid=" + CompanyId + "";
            //    IsAdmissionPolicy = true;
            //    IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> lists = new List<EyouSoft.Model.ShopStructure.HighShopTripGuide>();
            //    int TypeId = (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.门票政策;//门票政策
            //    lists = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(1, CompanyId, TypeId, string.Empty);
            //    if (lists != null && lists.Count>0)
            //    {
            //        ContentText=lists[0].ContentText;                    
            //    }
            //    lists = null;
            //}
            //else
            //{
            if (!string.IsNullOrEmpty(SecnicId))
            {
                EyouSoft.Model.ShopStructure.HighShopTripGuide model = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetModel(SecnicId);
                ImagePath = model.ImagePath;
                StrTitle = model.Title;
                ContentText = model.ContentText;
                IssueTime = model.IssueTime.ToShortDateString();
                TabName = Enum.GetName(typeof(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType), model.TypeID);
                likReturn.HRef = "ScenicInfoList.aspx?cid=" + CompanyId + "&st=" + TabIndex;
                model = null;
            }
            //}

        }
    }
}
