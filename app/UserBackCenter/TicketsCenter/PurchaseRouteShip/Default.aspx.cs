﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.TicketsCenter.PurchaseRouteShip
{
    /// <summary>
    /// 功能：购买运价航线（常规购买）
    /// 开发人 ：刘玉灵   时间：2010-11-01
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.BackPage
    {
        #region 分页变量
        protected int pageSize = 10;
        public int pageIndex = 1;
        protected int recordCount;
        #endregion

        protected string companyId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                companyId = SiteUserInfo.CompanyID;
                
                //数据初始化
                DataInit();
            }
        }

        #region 常规 列表初始化
        /// <summary>
        /// 常规 数据初始化
        /// </summary>
        protected void DataInit()
        {
            IList<EyouSoft.Model.TicketStructure.TicketFreightPackageInfo> list = EyouSoft.BLL.TicketStructure.FreightPackageInfo.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, EyouSoft.Model.TicketStructure.PackageTypes.常规, false);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                BindPage();
            }
            else
            {
                this.cg_lblMsg.Text = "未找到相关数据";
                this.ExportPageInfo1.Visible = false;
            }
            list = null;
        }
        #endregion

        #region 获得该年可选月
        /// <summary>
        /// 获得可选月
        /// </summary>
        /// <returns></returns>
        protected string GetOptionByMonth()
        {
            string str = "";
            int nowMonth = DateTime.Now.Month;
            for (int i = 1; i <= 12; i++)
            {
                if (i >= nowMonth)
                {
                    str += "<option value='" + i + "'>" + i + "</option>";
                }
            }
                return str;
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
            this.ExportPageInfo1.PageLinkURL = "#/PurchaseRouteShip/Default.aspx" + "?";
        }
        #endregion
    }
}