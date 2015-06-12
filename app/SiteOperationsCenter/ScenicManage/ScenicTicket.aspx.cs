using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.ScenicManage
{
    /// <summary>
    /// 景区门票-门票查看
    /// </summary>
    public partial class ScenicTicket : EyouSoft.Common.Control.YunYingPage
    {
        private int _pageSize = 20;   //每页显示条数
        private int _pageIndex = 1;   //当前页
        private int _recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _pageIndex = Utils.GetInt(Request.QueryString["page"], 1);
                string action = Utils.GetQueryStringValue("action");

                switch (action.ToLower())
                {
                    case "del":
                        DelTicket();
                        break;
                    default:
                        BindDropDownList();
                        InitData();
                        break;
                }
            }
        }

        #region 初始化

        /// <summary>
        /// 绑定查询下拉框 
        /// </summary>
        private void BindDropDownList()
        {
            ddlState.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicTicketsStatus));
            ddlState.DataTextField = "Text";
            ddlState.DataValueField = "Value";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("请选择", "0"));

            ddlCheck.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ExamineStatus));
            ddlCheck.DataTextField = "Text";
            ddlCheck.DataValueField = "Value";
            ddlCheck.DataBind();
            ddlCheck.Items.Insert(0, new ListItem("请选择", "0"));

            ddlB2B.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicB2BDisplay));
            ddlB2B.DataTextField = "Text";
            ddlB2B.DataValueField = "Value";
            ddlB2B.DataBind();
            ddlB2B.Items.Insert(0, new ListItem("请选择", "-1"));

            ddlB2C.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicB2CDisplay));
            ddlB2C.DataTextField = "Text";
            ddlB2C.DataValueField = "Value";
            ddlB2C.DataBind();
            ddlB2C.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        /// <summary>
        /// 初始化门票列表
        /// </summary>
        private void InitData()
        {
            var qmodel = new EyouSoft.Model.ScenicStructure.MSearchScenicTickets();
            string kw = Utils.GetQueryStringValue("sn");//门票类型
            int ic = Utils.GetInt(Utils.GetQueryStringValue("ic"));//是否审核
            int st = Utils.GetInt(Utils.GetQueryStringValue("st"));//上下架
            string sid = Utils.GetQueryStringValue("sid");//景区编号
            int b2B = Utils.GetInt(Utils.GetQueryStringValue("b2b"), -1);//b2b
            int b2C = Utils.GetInt(Utils.GetQueryStringValue("b2c"), -1);//b2c

            if (!string.IsNullOrEmpty(sid))
                qmodel.ScenicId = sid;
            if (!string.IsNullOrEmpty(kw))
                qmodel.TypeName = kw;
            if (ic <= 0)
                qmodel.ExamineStatus = null;
            else
                qmodel.ExamineStatus = (EyouSoft.Model.ScenicStructure.ExamineStatus)ic;
            if (st <= 0)
                qmodel.Status = null;
            else
                qmodel.Status = (EyouSoft.Model.ScenicStructure.ScenicTicketsStatus)st;
            if (b2B > -1)
                qmodel.B2B = (EyouSoft.Model.ScenicStructure.ScenicB2BDisplay)b2B;
            else
                qmodel.B2B = null;
            if (b2C > -1)
                qmodel.B2C = (EyouSoft.Model.ScenicStructure.ScenicB2CDisplay)b2C;
            else
                qmodel.B2C = null;

            txtSightName.Value = qmodel.ScenicName;
            if (qmodel.Status.HasValue)
            {
                if (ddlState.Items.FindByValue(st.ToString()) != null)
                    ddlState.Items.FindByValue(st.ToString()).Selected = true;
            }
            if (qmodel.ExamineStatus.HasValue)
            {
                if (ddlCheck.Items.FindByValue(ic.ToString()) != null)
                    ddlCheck.Items.FindByValue(ic.ToString()).Selected = true;
            }
            if (qmodel.B2B.HasValue)
            {
                if (ddlB2B.Items.FindByValue(b2B.ToString()) != null)
                    ddlB2B.Items.FindByValue(b2B.ToString()).Selected = true;
            }
            if (qmodel.B2C.HasValue)
            {
                if (ddlB2C.Items.FindByValue(b2C.ToString()) != null)
                    ddlB2C.Items.FindByValue(b2C.ToString()).Selected = true;
            }

            rptTicket.DataSource =
                EyouSoft.BLL.ScenicStructure.BScenicTickets.CreateInstance().GetList(_pageSize, _pageIndex,
                                                                                     ref _recordCount, qmodel);
            rptTicket.DataBind();

            ExportPageInfo1.intPageSize = _pageSize;
            ExportPageInfo1.intRecordCount = _recordCount;
            ExportPageInfo1.CurrencyPage = _pageIndex;
            ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExportPageInfo1.UrlParams = Request.QueryString;
            ExportPageInfo1.LinkType = 3;
        }

        /// <summary>
        /// 取得有效时间
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        protected string GetGoodTime(object sTime, object eTime)
        {
            if (sTime == null)
            {
                if (eTime == null)
                    return "长期有效";

                return Utils.GetDateTime(eTime.ToString()).ToShortDateString() + "前有效";
            }

            if (eTime == null)
                return "长期有效";

            return Utils.GetDateTime(sTime.ToString()).ToShortDateString() + "至" +
                   Utils.GetDateTime(eTime.ToString()).ToShortDateString();
        }

        /// <summary>
        /// 取得门票状态
        /// </summary>
        /// <param name="isCheck">审核状态</param>
        /// <param name="state">门票状态</param>
        /// <returns></returns>
        protected string GetState(object isCheck, object state)
        {
            if (isCheck == null || state == null)
                return string.Empty;

            var ic = (EyouSoft.Model.ScenicStructure.ExamineStatus)isCheck;
            var st = (EyouSoft.Model.ScenicStructure.ScenicTicketsStatus)state;

            if (ic == EyouSoft.Model.ScenicStructure.ExamineStatus.待审核)
                return ic.ToString();

            return st.ToString();
        }

        /// <summary>
        /// 是否显示删除按钮（只有待审核才可以删除）
        /// </summary>
        /// <param name="isCheck">审核状态</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        protected bool GetVisible(object isCheck, object status)
        {
            if (isCheck == null)
                return true;

            if ((EyouSoft.Model.ScenicStructure.ExamineStatus)isCheck == EyouSoft.Model.ScenicStructure.ExamineStatus.待审核)
            {
                return true;
            }
            else
            {
                if (status != null && (EyouSoft.Model.ScenicStructure.ScenicTicketsStatus)status == EyouSoft.Model.ScenicStructure.ScenicTicketsStatus.申请删除)
                    return true;
            }

            return false;
        }

        #endregion

        #region 删除门票

        /// <summary>
        /// 删除门票
        /// </summary>
        private void DelTicket()
        {
            string id = Utils.GetQueryStringValue("id");
            string cid = Utils.GetQueryStringValue("cid");
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(cid))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicTicket.aspx");
                return;
            }

            if (EyouSoft.BLL.ScenicStructure.BScenicTickets.CreateInstance().Delete(id, cid))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "删除成功", "/ScenicManage/ScenicTicket.aspx");
                return;
            }

            EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "删除失败", "/ScenicManage/ScenicTicket.aspx");
        }

        #endregion
    }
}
