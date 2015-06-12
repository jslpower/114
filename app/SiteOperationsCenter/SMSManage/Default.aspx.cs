using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
namespace SiteOperationsCenter.SMSManage
{
    public partial class Default : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }

            if (!Page.IsPostBack)
            {
                //判断是否有栏目查看权限
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.短信充值审核_管理该栏目))
                {
                    Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.短信充值审核_管理该栏目,false);
                    return;
                }
                this.GetSmsCompanyList();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["check"]))
            {
                string PayMoneyId = Utils.InputText(Request.QueryString["Id"]);
                int ckState = Utils.GetInt(Request.QueryString["State"]);
                Response.Write(this.CheckPayCompany(PayMoneyId, ckState));
                Response.End();
            }
        }

        /// <summary>
        /// 获的充值明细列表 
        /// </summary>
        protected void GetSmsCompanyList()
        {
            int recordCount = 0;
            int ? ProvinceId  = Utils.GetIntNull(Request.QueryString["ProvinceId"]);
            int ? CityId  = Utils.GetIntNull(Request.QueryString["CityId"]);
            if (ProvinceId < 1)
            {
                ProvinceId = null;
            }
            if (CityId < 1)
            {
                CityId = null;
            }
            if (ProvinceId !=null)
            {
                this.ProvinceAndCityList1.SetProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
                if (CityId !=null)
                {
                    this.ProvinceAndCityList1.SetCityId = Utils.GetInt(Request.QueryString["CityId"]);
                }
            }

            int CheckSate = Utils.GetInt(Request.QueryString["ckState"]);
            if (Request.QueryString["ckState"] == "-1")
            {
                CheckSate = -1;
            }
            this.dropCheckState.SelectedValue = CheckSate.ToString();
            string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);
            if (!string.IsNullOrEmpty(CompanyName))
            {
                this.txtCompanyName.Text = CompanyName.Trim();
            }
            DateTime? CheckTime = Utils.GetDateTimeNullable(Request.QueryString["ckTime"]);
            if (CheckTime!=null)
            {
                this.txtStartDate.Value = CheckTime.Value.ToShortDateString();
            }
            IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> AccountList = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetPayMoneys(PageSize, PageIndex, ref recordCount,CompanyName,CheckTime,ProvinceId,CityId,CheckSate);
            if (AccountList != null && AccountList.Count > 0)
            {
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.repCompanyList.DataSource = AccountList;
                this.repCompanyList.DataBind();
            }
            else {
                this.repCompanyList.EmptyText = string.Format( "<table width='100%' > <tr background=\"{0}\" class=\"white\" height=\"23\"><td width=\"20%\" align=\"center\" valign=\"middle\" background=\"{0}\"> <strong>单位名称</strong></td><td width=\"10%\" align=\"center\"  background=\"{0}\"> <strong>地区</strong></td><td width=\"10%\" align=\"center\" background=\"{0}\"> <strong>充值人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}\"> <strong>电话</strong> </td><td width=\"10%\" align=\"center\" background=\"{0}\"> <strong>手机</strong></td><td width=\"10%\" align=\"center\" background=\"{0}\"><strong>MQ</strong>  </td> <td align=\"center\" align=\"10\" background=\"{0}\"> <strong>充值金额/条数</strong> </td> <td width=\"10%\" align=\"center\" valign=\"middle\" background=\"{0}\"><strong>充值时间</strong> </td> <td width=\"10%\" align=\"center\" valign=\"middle\" background=\"{0}\">  <strong>操作</strong> </td>  </tr><td height='100px' colspan=\"10\" align='center'>暂无公司短信充值记录</td></tr></table>",ImageServerUrl+"/images/yunying/hangbg.gif");
            }
            AccountList = null;
        }

        /// <summary>
        /// 根据状态生成相关操作
        /// </summary>
        /// <param name="checkState">状态</param>
        /// <param name="Price">充值价格</param>
        /// <param name="Number">充值短信数量</param>
        /// <param name="PayMoneyId">充值明细ID</param>
        /// <returns></returns>
        protected string GetSmsCheck(int checkState,decimal Price,string PayMoneyId)
        {
            string strMessage = "";
            switch (checkState) { 
                case 0:
                    StringBuilder strCheck = new StringBuilder();

                    if (CheckMasterGrant(EyouSoft.Common.YuYingPermission.短信充值审核_管理该栏目) && CheckMasterGrant(EyouSoft.Common.YuYingPermission.短信充值审核_审核))
                    {
                        strCheck.AppendFormat("<a href='javascript:void(0);' onclick=\"CheckPay.OpenCheckdiv('{0}',this); return false;\">审核</a>", PayMoneyId);
                        strCheck.AppendFormat("<div style='text-align: right; z-index: 10000; display: none; '    class='white_content' id='div_{0}'> <a onclick=\" $('#div_{0}').hide(); return false;\" href='javascript:void(0)'>关闭</a>", PayMoneyId);
                        strCheck.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"> <tbody>");
                        strCheck.AppendFormat("<tr><td align=\"left\">&nbsp;充值金额：<input value='{0}' type='text' id='txtPayMoney' disabled='disabled'></td></tr>", Price.ToString("F2"));
                        strCheck.AppendFormat("<tr><td align=\"left\">&nbsp;可用金额：<input type='text' value='{0}' id='txtUseMoney'></td></tr>", Price.ToString("F2"));
                        strCheck.AppendFormat("<tr><td align=\"left\">&nbsp;审 核 人：<input value='{0}' type='text' disabled='disabled'></td></tr>", MasterUserInfo.ContactName);
                        strCheck.AppendFormat("<tr><td align=\"left\">&nbsp;审核时间：<input value='{0}' type='text' disabled='disabled'></td></tr>", DateTime.Today.ToShortDateString());
                        strCheck.AppendFormat("<tr><td align=\"center\"><input value='审核通过' type='button' onclick=\"CheckPay.ChekSms('{0}',true)\" >&nbsp;&nbsp;<input value='审核不通过' type='button' onclick=\"CheckPay.ChekSms('{0}',false)\"></td></tr>", PayMoneyId);
                        strCheck.Append("</table>");
                        strCheck.Append(" <iframe scrolling=\"no\" frameborder=\"0\" style=\"position: absolute; visibility: inherit;       top: 0px; left: 0px; width: 100%; height: 100%; z-index: -1;\" marginwidth=\"0\"      marginheight=\"0\"></iframe></div>");
                        strMessage = strCheck.ToString();
                    }
                    break;
                case 1:
                    strMessage = "审核通过";
                    break;
                case 2:
                    strMessage = "审核未通过";
                    break;
            }
            return strMessage;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="PayId">充值明细ID</param>
        /// <param name="checkState">状态 1：审核通过 2：审核不通过</param>
        protected bool CheckPayCompany(string PayId,int checkState)
        {
            bool isResult = false;
            EyouSoft.Model.SMSStructure.PayMoneyInfo Model = new EyouSoft.Model.SMSStructure.PayMoneyInfo();
            Model.CheckTime = DateTime.Today;
            Model.CheckUserFullName = MasterUserInfo.ContactName;
            Model.CheckUserName = MasterUserInfo.UserName;
            Model.IsChecked = checkState;
            Model.PayMoneyId = PayId;
            Model.UseMoney = Utils.GetDecimal(Request.QueryString["UseMoney"]);
            Model.PayMoney = Utils.GetDecimal(Request.QueryString["Money"]);
            isResult= EyouSoft.BLL.SMSStructure.Account.CreateInstance().CheckPayMoney(Model);
            Model = null;
            return isResult;
        }

        /// <summary>
        /// 获的省份-城市名称
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        protected string GetCityName(int ProvinceId, int CityId)
        {
            string strVal = "";

            IList<EyouSoft.Model.SystemStructure.SysCity> CityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(ProvinceId, 0, null, null, null);
            if (CityList != null)
            {
                CityList = (from c in CityList where c.CityId == CityId select c).ToList();
                if (CityList != null && CityList.Count > 0)
                {
                    strVal = CityList[0].ProvinceName + " " + CityList[0].CityName;
                }
            }
            CityList = null;

            return strVal;
        }
        
    }
}
