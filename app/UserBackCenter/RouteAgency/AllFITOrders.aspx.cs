using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 所有散客订单
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-21
    public partial class AllFITOrders : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            #region 权限判断
            if (!CheckGrant(TravelPermission.专线_线路散客订单管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            #endregion
            Key = "AllFITOrders" + Guid.NewGuid().ToString();
            #region 配置用户控件
            ILine1.Key = Key;
            ILine1.SelectFunctionName = "AllFITOrders.GetList";
            ILine1.UserId = SiteUserInfo.ID;
            #endregion
            InitPage();
        }
        /// <summary>
        /// 绑定专线下拉
        /// </summary>
        private void BindZX()
        {
            ICompanyUser companyUserBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = companyUserBLL.GetModel(SiteUserInfo.ID);
            if (companyUserModel != null && companyUserModel.Area != null && companyUserModel.Area.Count > 0)
            {
                ddl_ZX.AppendDataBoundItems = true;
                ddl_ZX.DataTextField = "AreaName";
                ddl_ZX.DataValueField = "AreaId";
                ddl_ZX.DataSource = companyUserModel.Area;
                ddl_ZX.DataBind();
            }
        }
        /// <summary>
        /// 绑定订单状态
        /// </summary>
        private void BindPowderOrderStatus()
        {
            List<EnumObj> ls = new List<EnumObj>();
            string[] objtxt = { "待处理", "未确定", "预留", "已确定", "结单", "取消" };
            string[] objval = { 
                                ((int)PowderOrderStatus.专线商待处理).ToString(),
                                //((int)PowderOrderStatus.专线商已阅).ToString() + "," + ((int)PowderOrderStatus.预留过期).ToString(), 
                                ((int)PowderOrderStatus.专线商已阅).ToString() + "," + ((int)PowderOrderStatus.组团社已阅).ToString()+","+((int)PowderOrderStatus.预留过期).ToString(), 
                                ((int)PowderOrderStatus.专线商预留).ToString(), 
                                ((int)PowderOrderStatus.专线商已确定).ToString(), 
                                ((int)PowderOrderStatus.结单).ToString(), 
                                ((int)PowderOrderStatus.取消).ToString()
                              };
            for (int i = 0; i < 6; i++)
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
        /// <summary>
        /// 绑定支付状态
        /// </summary>
        private void BindPaymentStatus()
        {
            List<EnumObj> ls = new List<EnumObj>();
            string[] objtxt = { "已付定金", "已付全款", "退款" };
            string[] objval = { 
                                  ((int)PaymentStatus.组团社已付定金).ToString(), 
                                  ((int)PaymentStatus.组团社已付全款).ToString(), 
                                  ((int)PaymentStatus.申请退款).ToString() + "," + ((int)PaymentStatus.已退款).ToString() 
                              };
            for (int i = 0; i < 3; i++)
            {
                ls.Add(new EnumObj(objtxt[i], objval[i]));
            }

            rpt_paymentStatus.DataSource = ls;
            rpt_paymentStatus.DataBind();

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            //绑定专线下啦
            BindZX();
            //绑定订单状态
            BindPowderOrderStatus();
            //绑定支付状态
            BindPaymentStatus();
            //分页参数
            int intRecordCount = 0,//记录条数
             CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),//页码
             intPageSize = 15;//每页条数
            MTourOrderSearch queryModel = new MTourOrderSearch();
            #region 查询实体
            queryModel.TourId = Utils.GetQueryStringValue("tourId");
            //专线ID
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"));
            if (queryModel.AreaId != 0)
            {
                ddl_ZX.SelectedValue = queryModel.AreaId.ToString();
                ILine1.CheckedId = queryModel.AreaId.ToString();
            }
            //排序字段(出发时间排序)
            queryModel.Order = Utils.GetInt(Utils.GetQueryStringValue("sortType"), 2);
            //排序规则
            queryModel.IsDesc = Utils.GetQueryStringValue("sort").Length > 0 ? Utils.GetQueryStringValue("sort") == "true" : true;
            //订单区域
            queryModel.AreaType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("area"), -1) > 0)
            {
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
            string[] status;
            int i;
            //订单状态
            if (Utils.GetQueryStringValue("status").Length > 0 && Utils.GetQueryStringValue("status") != "-1")
            {
                status = Utils.GetQueryStringValue("status").Split(',');
                i = status.Length;
                IList<PowderOrderStatus> lsPowderOrderStatus = new List<PowderOrderStatus>();
                while (i-- > 0)
                {
                    lsPowderOrderStatus.Add((PowderOrderStatus)Utils.GetInt(status[i]));
                }
                queryModel.PowderOrderStatus = lsPowderOrderStatus;
            }
            //支付状态
            if (Utils.GetQueryStringValue("paymentStatus").Length > 0)
            {
                IList<PaymentStatus> lsPaymentStatus = new List<PaymentStatus>();
                status = Utils.GetQueryStringValue("paymentStatus").Split(',');
                i = status.Length;
                while (i-- > 0)
                {
                    lsPaymentStatus.Add((PaymentStatus)Utils.GetInt(status[i]));
                }
                queryModel.PaymentStatus = lsPaymentStatus;
            }
            #endregion
            ITourOrder bll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();

            IList<MTourOrder> list = bll.GetPublishersAllList(intPageSize, CurrencyPage, ref intRecordCount, SiteUserInfo.CompanyID, queryModel);
            pnlNodata.Visible = false;
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";

                this.ExportPageInfo1.UrlParams.Add("tourId", Utils.GetQueryStringValue("tourId"));
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("area", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("paymentStatus", Utils.GetQueryStringValue("paymentStatus"));
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
    }
}
