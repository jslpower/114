using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 短信订单管理
    /// 创建时间：2011-12-22 方琪
    /// </summary>
    public partial class OrderMsgManage : EyouSoft.Common.Control.YunYingPage
    {

        protected string SendBusinessType = string.Empty;
        protected string CompanyId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
            #region 处理前台请求
            string Type = Utils.GetQueryStringValue("Type");
            CompanyId = Utils.GetQueryStringValue("CompanyId");
            if (Type == "GetContactMoney")
            {
                Response.Clear();
                Response.Write(BindContact(CompanyId));
                Response.End();
            }
            #endregion
        }

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void InitPage()
        {
            EyouSoft.Model.SystemStructure.MSysSettingInfo SettingInfoModel = new EyouSoft.Model.SystemStructure.MSysSettingInfo();
            SettingInfoModel = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSysSetting();
            GetSendBusiness();
            InitSendChannel();
            //发送供应商类型
            this.hid_BusinessType.Value = Getstr(SettingInfoModel.OrderSmsCompanyTypes);
            //发送内容
            this.SendContent.Value = SettingInfoModel.OrderSmsTemplate;
            //公司编号
            this.txt_CompanyId.Value = SettingInfoModel.OrderSmsCompanyId;
            //发送通道
            this.ddlSendChannel.SelectedIndex = (int)SettingInfoModel.OrderSmsChannelIndex;
        }
        #endregion

        protected string Getstr(CompanyLev[] arr)
        {
            string sb = string.Empty;
            if (arr != null && arr.Length > 0)
            {
                int i = arr.Length;
                for (int j = 0; j < i; j++)
                {
                    sb += ((int)arr[j]).ToString() + ",";

                }
                return sb.Substring(0, sb.Length - 1);
            }
            return string.Empty;

        }

        #region 绑定供应商类型
        /// <summary>
        /// 绑定供应商类型
        /// </summary>
        protected void GetSendBusiness()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            List<EnumObj> BusinessList = EnumObj.GetList(typeof(EyouSoft.Model.CompanyStructure.CompanyLev));
            foreach (var item in BusinessList)
            {
                i++;
                sb.AppendFormat("<input type=\"checkbox\" name=\"BusinessType\" value=\"{0}\" id=\"cbx_{1}\" valid=\"requireChecked\" min=\"1\" errmsgend=\"BusinessType\" errmsg=\"至少选择一个供应商类型！\"/><label for=\"cbx_{1}\">{2}</label>  &nbsp;", item.Value, i, item.Text);
            }
            SendBusinessType = sb.ToString();

        }
        #endregion

        #region 绑定公司用户
        /// <summary>
        /// 绑定公司用户
        /// </summary>
        protected string BindContact(string CompanyId)
        {
            if (!string.IsNullOrEmpty(CompanyId))
            {
                EyouSoft.Model.CompanyStructure.QueryParamsUser modelUser = new EyouSoft.Model.CompanyStructure.QueryParamsUser { IsShowAdmin = true };
                IList<CompanyUserBase> CompanyUserList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(CompanyId, modelUser);
                EyouSoft.Model.SMSStructure.AccountInfo Accountmodel = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetAccountInfo(CompanyId);

                if (CompanyUserList.Count > 0)
                {
                    StringBuilder strB = new StringBuilder("[");
                    string Operator = string.Empty;
                    string CompanyMoney = "0.00";
                    if (Accountmodel != null)
                    {
                        CompanyMoney = Accountmodel.AccountMoney.ToString("f2");
                    }

                    foreach (var model in CompanyUserList)
                    {
                        if (model.IsAdmin)
                        {
                            Operator += model.UserName;
                        }
                        strB.Append("{\"UserName\":\"" + model.ContactInfo.ContactName + "|" + model.UserName + "\",\"UserId\":\"" + model.ID + "\"},");

                    }
                    strB.Remove(strB.Length - 1, 1);
                    strB.Append("]");

                    return strB.ToString() + "$" + Operator + "$" + CompanyMoney;
                }
                else
                {
                    return "error";
                }
            }
            else
            {
                return "error";
            }

        }
        #endregion

        #region 初始化发送通道
        /// <summary>
        /// 初始化发送通道
        /// </summary>
        private void InitSendChannel()
        {
            EyouSoft.Model.SMSStructure.SMSChannelList channel = new EyouSoft.Model.SMSStructure.SMSChannelList();
            IList<EyouSoft.Model.SMSStructure.SMSChannel> channels = new List<EyouSoft.Model.SMSStructure.SMSChannel>();

            for (int i = 0; i < channel.Count; i++)
            {
                channels.Add(channel[i]);

                this.ddlSendChannel.Items.Add(new ListItem(channel[i].ChannelName, channel[i].Index.ToString()));
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            OrderSource[] ordersoure = new OrderSource[1];
            ordersoure[0] = OrderSource.线路散拼订单;
            EyouSoft.Model.SystemStructure.MSysSettingInfo SettingInfoModel = new EyouSoft.Model.SystemStructure.MSysSettingInfo();
            SettingInfoModel = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSysSetting();
            SettingInfoModel.OrderSmsChannelIndex = Convert.ToByte(Utils.GetFormValue(ddlSendChannel.UniqueID));
            SettingInfoModel.OrderSmsCompanyId = Utils.GetFormValue(txt_CompanyId.UniqueID);
            SettingInfoModel.OrderSmsCompanyTypes = getArry();
            SettingInfoModel.OrderSmsIsEnable = string.IsNullOrEmpty(Utils.GetFormValue(txt_CompanyId.UniqueID));
            SettingInfoModel.OrderSmsOrderTypes = ordersoure;
            SettingInfoModel.OrderSmsTemplate = Utils.GetFormValue("SendContent");
            SettingInfoModel.OrderSmsUserId = Utils.GetFormValue(CompanyContact.UniqueID);
            int flag = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().SetSysSettings(SettingInfoModel);
            if (flag == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('提交成功！');window.location.href='/LineManage/OrderMsgManage.aspx' </script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('提交失败！'); </script>");
            }
        }

        #region 获取供应商类型数组
        /// <summary>
        /// 获取供应商类型数组
        /// </summary>
        /// <returns></returns>
        protected CompanyLev[] getArry()
        {
            string[] strArray = Utils.GetFormValues("BusinessType");
            CompanyLev[] arry = new CompanyLev[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                arry[i] = (CompanyLev)Enum.Parse(typeof(CompanyLev), strArray[i], true);
            }
            return arry;
        }
        #endregion
    }
}
