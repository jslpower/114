using System;
using System.Collections;
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
using EyouSoft.Security.Membership;

namespace UserBackCenter.SupplyInformation
{
    /// <summary>
    /// 客户后台-供求信息：关注的商机
    /// luofx   2010-08-3
    /// </summary>
    public partial class HasSupplyFavorites : EyouSoft.Common.Control.BackPage
    {
        private int intPageSize = 15, intRecordCount = 0;
        protected int intPageIndex = 1;
        private string UserID = string.Empty;
        private IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_供求信息))
            {
                Utils.ResponseNoPermit();
                return;
            }
            UserID = this.SiteUserInfo.ID;           
            if (Request.QueryString["action"] == "delete" && !string.IsNullOrEmpty(Request.QueryString["ExchangeId"]))
            {
                Delete(Utils.GetQueryStringValue("ExchangeId"));
                return;
            }
            if (!IsPostBack)
            {
                SupplyInfoTab1.CityId = this.SiteUserInfo.CityId;
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //rpt_HasSupplyFavorites
            rpt_HasSupplyFavorites.DataSource = EyouSoft.BLL.CommunityStructure.ExchangeFavor.CreateInstance().GetListByUserID(intPageSize, intPageIndex, ref intRecordCount, UserID);
            rpt_HasSupplyFavorites.DataBind();
            if (rpt_HasSupplyFavorites.Items.Count < 1)
            {
                this.Nodata.Visible = true;
            }
            this.ExportPageInfo1.intPageSize = intPageSize;           
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 获取省份名称
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <returns></returns>
        protected string GetProvinceName(int ProvinceId)
        {
            string Result = "无";
            if (ProvinceList == null)
            {
                ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            }
            bool isExist = false;
            ((List<EyouSoft.Model.SystemStructure.SysProvince>)ProvinceList).ForEach(item =>
            {
                if (!isExist && item.ProvinceId == ProvinceId)
                {
                    Result = item.ProvinceName;
                }
            });
            return Result;
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_HasSupplyFavorites_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EyouSoft.Model.CommunityStructure.ExchangeFavor model = (EyouSoft.Model.CommunityStructure.ExchangeFavor)e.Item.DataItem;
                Literal ltrExchangeTag = (Literal)e.Item.FindControl("ltrExchangeTag");
                if (ltrExchangeTag != null)
                {
                    if (model.ExchangeTagHtml != null)
                    {
                        ltrExchangeTag.Text = model.ExchangeTagHtml.TagHTML;// string.Format("<div class=\"gqbiaoqian{0}\">{1}</div>", (int)model.ExchangeTag, model.ExchangeTag);
                    }
                }
            }
        }
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="ID"></param>
        private void Delete(string ID)
        {
            string[] ids = ID.Split(',');
            bool isTrue = EyouSoft.BLL.CommunityStructure.ExchangeFavor.CreateInstance().Delete(ids);
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'取消关注成功！'}]");
                Response.End();
            }
        }
    }
}
