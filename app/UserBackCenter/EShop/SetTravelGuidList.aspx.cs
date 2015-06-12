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
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.IO;
namespace UserBackCenter.EShop
{
    /// <summary>
    /// history: zhangzy   2010-11-10  整合"旅游资源推荐"到"出游指南"栏目中
    /// history: zhouwc   2010-12-10  整合"景点高级网店旅游资讯"到"出游指南"栏目中
    /// </summary>
    public partial class SetTravelGuidList : EyouSoft.Common.Control.BasePage
    {
        protected int pageSize = 15;
        protected int pageIndex = 1;

        protected int sortId = 0;//序号
        protected int GuideType = 0;
        protected int TypeId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            if (!Page.IsPostBack)
            {
                int intRecordCount = 0;
                pageIndex = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
                if (pageIndex <= 0)
                    pageIndex = 1;
                GuideType = Utils.GetIntSign(Request.QueryString["GuideType"], 0);
                TypeId = Utils.GetIntSign(Request.QueryString["TypeId"], 0);
                IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> guid_List = null;
                GetDataSource(GuideType, TypeId, ref intRecordCount, ref guid_List);
                if (intRecordCount > 0 && pageIndex > intRecordCount)
                    pageIndex = intRecordCount;
                if (guid_List != null && guid_List.Count > 0)
                {
                    guidRepeater.DataSource = guid_List;
                    guidRepeater.DataBind();
                    this.ExportPageInfo1.intPageSize = pageSize;
                    this.ExportPageInfo1.CurrencyPage = pageIndex;
                    this.ExportPageInfo1.intRecordCount = intRecordCount;
                    this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExportPageInfo1.UrlParams = Request.QueryString;
                }
                else
                {
                    div_NoDataMessage.Visible = true;
                    ExportPageInfo1.Visible = false;
                }

                guid_List = null;

            }
        }

        /// <summary>
        /// 根据页面参数类型获取对应的数据源
        /// </summary>
        /// <param name="GuideType">页面参数类型</param>
        /// <param name="guid_List">数据源</param>
        private void GetDataSource(int GuideType, int TypeId, ref int intRecordCount, ref IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> guid_List)
        {
            switch (GuideType)
            {
                case -1:  //目的地添加
                    guid_List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref intRecordCount, this.SiteUserInfo.CompanyID, "", EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍);
                    break;
                case 0:   //出游指南
                    guid_List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref intRecordCount, this.SiteUserInfo.CompanyID, "", EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.旅游资源推荐);
                    ltrEditTitle.Text = "出游指南添加";
                    ltrListTitle.Text = "出游指南列表";
                    break;
                case 1:   //景区资讯  门票政策无列表
                    EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType? TripGuideType = null;
                    if (TypeId > 0)
                        TripGuideType = (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)TypeId;
                    if (TypeId > 0 && TypeId == 8)
                    {
                        //景区攻略包含  景区攻略 8、景区美食 9、景区住宿 10、景区交通 11、景区购物 12
                        guid_List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref intRecordCount, this.SiteUserInfo.CompanyID, "", EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区美食, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区交通, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区购物);
                    }
                    else if (TripGuideType.HasValue && TripGuideType.Value != EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.门票政策)
                    {
                        guid_List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref intRecordCount, this.SiteUserInfo.CompanyID, "", TripGuideType.Value);
                    }
                    else
                    {
                        guid_List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetList(pageSize, pageIndex, ref intRecordCount, this.SiteUserInfo.CompanyID, "", null);
                    }

                    SetTitle(TripGuideType);
                    
                    break;
            }
        }

        #region 初始化页面标题

        /// <summary>
        /// 设置标题头
        /// </summary>
        /// <param name="intTypeId">类型枚举</param>
        private void SetTitle(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType? TripGuideType)
        {
            if (TripGuideType.HasValue)
            {
                ltrEditTitle.Text = TripGuideType.ToString() + "添加";
                ltrListTitle.Text = TripGuideType.ToString() + "列表";
            }
            else
            {
                ltrEditTitle.Text = "景区资讯添加";
                ltrListTitle.Text = "景区资讯列表";
            }
        }

        #endregion
    }
}
