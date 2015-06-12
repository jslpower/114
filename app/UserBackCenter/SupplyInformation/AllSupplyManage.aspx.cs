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
    /// 客户后台-供求信息：我的供求
    /// luofx   2010-08-3
    /// </summary>
    public partial class AllSupplyManage : EyouSoft.Common.Control.BackPage
    {
        private int intPageSize = 15;
        protected int intPageIndex = 1;
        private IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_供求信息))
            {
                Utils.ResponseNoPermit();
                return;
            }
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
            int intRecordCount = 0;
            int provinceId = 0;
            EyouSoft.Model.CommunityStructure.ExchangeType? ExType = null;
            EyouSoft.Model.CommunityStructure.ExchangeTag? ExTag = null;

            #region dpl_ExchangeTag绑定标签枚举
            dpl_ExchangeTag.DataSource = Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag));
            dpl_ExchangeTag.DataBind();
            dpl_ExchangeTag.Items.Insert(0, new ListItem("请选择标签", ""));
            if (Request.QueryString["PageExchangeTag"] != null && !string.IsNullOrEmpty(Request.QueryString["PageExchangeTag"]))
            {
                dpl_ExchangeTag.Items.FindByText(Utils.GetQueryStringValue("PageExchangeTag")).Selected = true;
                ExTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), Utils.GetQueryStringValue("PageExchangeTag"), true);              
            }
            #endregion
            #region dpl_ExchangeType绑定类别枚举
            dpl_ExchangeType.DataSource = Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeType));
            dpl_ExchangeType.DataBind();
            dpl_ExchangeType.Items.Insert(0, new ListItem("请选择类别", ""));
            if (Request.QueryString["ExchangeType"] != null && !string.IsNullOrEmpty(Request.QueryString["ExchangeType"]))
            {
                dpl_ExchangeType.Items.FindByText(Utils.GetQueryStringValue("ExchangeType")).Selected = true;
                ExType = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), Utils.GetQueryStringValue("ExchangeType"), true);
            }
            #endregion

            #region 绑定省份
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.dpl_ProvinceList.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.dpl_ProvinceList.DataSource = ProvinceList;
                this.dpl_ProvinceList.DataTextField = "ProvinceName";
                this.dpl_ProvinceList.DataValueField = "ProvinceId";
                this.dpl_ProvinceList.DataBind();
            }
            this.dpl_ProvinceList.Items.Insert(0, new ListItem("请选择", ""));
            if (Request.QueryString["ProvinceList"] != null && !string.IsNullOrEmpty(Request.QueryString["ProvinceList"]))
            {
                dpl_ProvinceList.Items.FindByValue(Utils.GetQueryStringValue("ProvinceList")).Selected = true;
            }
            ProvinceList = null;
            #endregion
            #region 前台外部链接过来的时
            if (!string.IsNullOrEmpty(Request.QueryString["ExchangeTag"])) {
                switch (Request.QueryString["ExchangeTag"]) { 
                    case "1":
                        //ExTag = EyouSoft.Model.CommunityStructure.ExchangeTag.供应信息;
                        break;
                    case "2":
                        //ExTag = EyouSoft.Model.CommunityStructure.ExchangeTag.需求信息;
                        break;
                }
            }
            #endregion
            provinceId = Utils.GetInt(Request.QueryString["ProvinceList"]);
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> lists = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetListByOperator(intPageSize, intPageIndex, ref intRecordCount, ExType, ExTag, provinceId, 0, "", this.SiteUserInfo.ID, null, null);
            this.rpt_AllSupplyManage.DataSource = lists;
            rpt_AllSupplyManage.DataBind();
            lists = null;
            if (rpt_AllSupplyManage.Items.Count < 1)
            {
                this.Nodata.Visible = true;
            }
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_AllSupplyManage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EyouSoft.Model.CommunityStructure.ExchangeList model = (EyouSoft.Model.CommunityStructure.ExchangeList)e.Item.DataItem;
                Literal ltrExchangeTag = (Literal)e.Item.FindControl("ltrExchangeTag");
                if (ltrExchangeTag != null && model.ExchangeTagHtml!=null)
                {
                    if ((int)model.ExchangeTag == 0)
                    {
                        ltrExchangeTag.Text = string.Format("<div class=\"gqbiaoqian{0}\">{1}</div>", 1, "无");
                    }
                    else
                    {
                    ltrExchangeTag.Text =model.ExchangeTagHtml.TagHTML ;//string.Format("<div class=\"gqbiaoqian{0}\">{1}</div>", (int)model.ExchangeTag, model.ExchangeTag);
                
                    }
                }
            }
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
                if (!isExist&&item.ProvinceId == ProvinceId)
                {
                    Result = item.ProvinceName;
                }
            });
            return Result;
        }
        /// <summary>
        /// 删除供应信息 
        /// </summary>
        /// <param name="id"></param>
        private void Delete(string id)
        {
            bool isTrue = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().Delete(id);
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'删除成功！'}]");
                Response.End();
            }
        }
    }
}
