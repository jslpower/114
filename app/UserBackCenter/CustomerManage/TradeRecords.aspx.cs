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
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserBackCenter.CustomerManage
{   
    /// <summary>
    /// 页面功能：我的客户交易记录
    /// 开发人：xuty 开发时间2010-07-12
    /// </summary>
    public partial class TradeRecords : BasePage
    {
        protected int pageSize = 15;
        protected int pageIndex = 1;
        protected int recordCount;
        protected string companyId;
        protected int No = 1;//列表显示序号
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)//是否登录
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx");
                return;
            }
            //if (!CheckGrant(TravelPermission.客户管理_管理栏目))
            //{ 
            //    Utils.ResponseNoPermit();
            //    return;
            //}
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            //是否开通收费MQ
            if (!companyModel.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ))
            {
                Server.Transfer("/SystemSet/ApplyMQ.aspx?iscustomer=yes&urltype=tab", false);
                return;
            }
            companyId = Utils.GetQueryStringValue("companyid");
            //获取当前页
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            BindRecord();//绑定交易记录
         }

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("companyid",companyId);
            this.ExportPageInfo1.UrlParams.Add("iframeId", Utils.GetQueryStringValue("iframeId"));
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion

        #region 绑定交易记录
        protected void BindRecord()
        {
            EyouSoft.IBLL.TourStructure.ITourOrder tourOrderBll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourOrder> orderList;
            if (SiteUserInfo.CompanyRole.Length == 1)
            {
                orderList = tourOrderBll.GetRetailersOrderList(pageSize, pageIndex, ref recordCount, 1, SiteUserInfo.CompanyID, companyId,SiteUserInfo.CompanyRole.RoleItems[0], EyouSoft.Model.TourStructure.OrderState.已成交);
            }
            else
            {
                orderList = tourOrderBll.GetRetailersOrderList(pageSize, pageIndex, ref recordCount, 1, SiteUserInfo.CompanyID, companyId, null, EyouSoft.Model.TourStructure.OrderState.已成交);
            }
            if (orderList != null && orderList.Count > 0)
            {
                tr_rpt_TradeList.DataSource = orderList;
                tr_rpt_TradeList.DataBind();
                BindPage();
            }
            else
            {
                this.tr_noData.Style.Remove("display");
                this.ExportPageInfo1.Visible = false;
            }
            orderList = null;
        }
        #endregion 
    }
}
