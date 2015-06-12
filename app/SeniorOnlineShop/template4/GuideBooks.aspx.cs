using System;
using System.Collections.Generic;
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
namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 旅游指南列表
    /// </summary>
    /// 鲁功源 2010-11-11
    public partial class GuideBooks : EyouSoft.Common.Control.FrontPage
    {
        protected SeniorOnlineShop.master.T4 cMaster = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            cMaster = (SeniorOnlineShop.master.T4)this.Master;
            if (!IsPostBack)
            {
                if(cMaster==null)
                    return;
                #region 设置导航栏Tab索引
                cMaster.CTAB = master.T4TAB.出游指南;
                #endregion

                #region 初始化风土人情列表
                LocalCultures.DataSource = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(9, cMaster.CompanyId, 1, "");
                LocalCultures.DataBind();
                #endregion

                #region 初始化温馨提醒列表 
                IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(10, cMaster.CompanyId, 2, "");
                if (list != null && list.Count > 0)
                {
                    ltrTopOneWarm.Text = string.Format("<a target=\"_blank\" href=\"/GuideBookInfo_{1}_{0}\"><img src=\"{2}\" width=\"79\" height=\"67\" border=\"0\"/></a>", cMaster.CompanyId,list[0].ID, Utils.GetLineShopImgPath(list[0].ImagePath, 4));
                    lbTopOneTitle.Text = string.Format("<a target=\"_blank\" href=\"/GuideBookInfo_{1}_{0}\">{2}</a>", cMaster.CompanyId, list[0].ID, Utils.GetText2(list[0].Title, 8, false));
                    lbTopOneContent.Text = string.Format("<a style=\"color:#999;\" target=\"_blank\" href=\"/GuideBookInfo_{1}_{0}\">{2}</a>", cMaster.CompanyId, list[0].ID, Utils.GetText2(StringValidate.LoseHtml(list[0].ContentText), 38, true));
                    list.RemoveAt(0);
                    rpWarmInfos.DataSource = list;
                    rpWarmInfos.DataBind();
                }
                #endregion

                #region 初始化综合介绍列表
                CompositeReferral.DataSource = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(10, cMaster.CompanyId, 3, "");
                CompositeReferral.DataBind();
                #endregion

                #region 初始化旅游资源推荐
                HotResource.DataSource = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(30, cMaster.CompanyId, 4, "");
                HotResource.DataBind();
                #endregion

                Page.Title = "出游指南";
            }
        }
    }
}
