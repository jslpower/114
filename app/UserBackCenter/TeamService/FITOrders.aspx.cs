using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using System.Text;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-我的散客订单
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-26
    public partial class FITOrders : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 线路区域
        /// </summary>
        protected int area = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (!CheckGrant(TravelPermission.组团_线路散客订单管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            Key = "FITOrders" + Guid.NewGuid().ToString();
            BindPowderOrderStatus();
            InitPage();

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            //记录条数
            int intRecordCount = 0;
            //页码
            int CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            int intPageSize = 15;
            MTourOrderSearch queryModel = new MTourOrderSearch();
            #region 查询实体
            queryModel.RouteId = Utils.GetQueryStringValue("routeId");
            queryModel.Order = 1;
            //订单区域
            queryModel.AreaType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("area"), -1) > 0)
            {
                area = Utils.GetInt(Utils.GetQueryStringValue("area"));
                queryModel.AreaType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("area"));
            }
            //出团时间开始
            queryModel.LeaveDateS = Utils.GetQueryStringValue("goTimeS");
            txt_goTimeS.Value = queryModel.LeaveDateS;
            //出团时间结束
            queryModel.LeaveDateE = Utils.GetQueryStringValue("goTimeE");
            txt_goTimeE.Value = queryModel.LeaveDateE;
            //关键字
            queryModel.OrderKey = Utils.GetQueryStringValue("keyWord");
            txt_keyWord.Value = queryModel.OrderKey;
            //订单状态
            if (Utils.GetQueryStringValue("status").Length > 0 && Utils.GetQueryStringValue("status") != "-1")
            {
                string[] status = Utils.GetQueryStringValue("status").Split(',');
                int i = status.Length;
                IList<PowderOrderStatus> lsPowderOrderStatus = new List<PowderOrderStatus>();
                while (i-- > 0)
                {
                    lsPowderOrderStatus.Add((PowderOrderStatus)Utils.GetInt(status[i]));
                }
                queryModel.PowderOrderStatus = lsPowderOrderStatus;
            }
            #endregion
            ITourOrder bll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();

            IList<MTourOrder> list = bll.GetTravelList(intPageSize, CurrencyPage, ref intRecordCount, SiteUserInfo.CompanyID, queryModel);
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";

                this.ExportPageInfo1.UrlParams.Add("routeId", queryModel.RouteId);
                this.ExportPageInfo1.UrlParams.Add("area", Utils.GetQueryStringValue("area"));
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDateS.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.LeaveDateE.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.OrderKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }
        /// <summary>
        /// 绑定订单状态
        /// </summary>
        private void BindPowderOrderStatus()
        {
            List<EnumObj> ls = new List<EnumObj>();
            string[] objtxt = { "未确定", "预留", "已确定", "结单", "取消" };
            string[] objval = { 
                                ((int)PowderOrderStatus.专线商已阅).ToString() + "," + ((int)PowderOrderStatus.组团社已阅).ToString()+","+((int)PowderOrderStatus.预留过期).ToString(), 
                                ((int)PowderOrderStatus.专线商预留).ToString(), 
                                ((int)PowderOrderStatus.专线商已确定).ToString(), 
                                ((int)PowderOrderStatus.结单).ToString(), 
                                ((int)PowderOrderStatus.取消).ToString()
                              };
            for (int i = 0; i < 5; i++)
            {
                ls.Add(new EnumObj(objtxt[i], objval[i]));
            }

            sel_status.DataTextField = "Text";
            sel_status.DataValueField = "Value";
            sel_status.AppendDataBoundItems = true;
            sel_status.DataSource = ls;
            sel_status.DataBind();

            rpt_powderOrderStatus.DataSource = ls;
            rpt_powderOrderStatus.DataBind();
        }
    }
}
