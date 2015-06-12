using System;
using System.Collections;
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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.IBLL.NewTourStructure;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：短信发送页面
    /// 开发人：杜桂云  开发时间：2010-08-03
    /// 修改人：徐从栎
    /// 修改时间：2011-12-30
    /// 修改说明：接受线路传过来的信息，把相关信息放到短信内容中。
    /// </summary>
    public partial class SendSMS : EyouSoft.Common.Control.BackPage
    {
        #region 成员变量
        protected string CompanyID = "0";//公司编号
        protected double Show114PhoneCount = 0.0;//
        //protected decimal SMSDecimal = 0;//单条短信价格
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;
            //计算现有社团电话号码数
            Show114PhoneCount = EyouSoft.BLL.SMSStructure.Customer.CreateInstance().GetCustomersCount();

            //设置公司帐户的基础数据
            EyouSoft.BLL.SMSStructure.Account.CreateInstance().SetAccountBaseInfo(CompanyID);
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["btn_Submit"])))
            {
                this.SendSMSMethod(Utils.GetInt(Request.QueryString["btn_Submit"]));
            }
            string routeIDs = Utils.GetQueryStringValue("id");
            int planType = Utils.GetInt(Utils.GetQueryStringValue("type"));
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(routeIDs))
                {
                    this.GetRouteInfo(routeIDs, planType);
                }
                this.txt_SendPeople.Text = this.SiteUserInfo.CompanyName;

                //用户账户余额判断
                decimal accountMoney = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetAccountMoney(CompanyID);
                if (accountMoney <= 0)
                {
                    //this.imb_pay.Visible = true;                    
                    //this.imb_pay.Src = ImageServerUrl + "/images/result.gif";

                    this.lab_NoMoney.Visible = true;
                    this.div_pay.Style.Add("display", "block");
                }
                else
                {
                    //this.imb_pay.Visible = false;
                    this.div_pay.Style.Add("display", "none");
                }
            }

            this.ddl_SendType.Attributes.Add("onchange", "return SendSMS.ChangeTimeShow();");
            this.btn_Send.Attributes.Add("onclick", "return SendSMS.CheckTheSMSSend();");

            this.InitSendChannel();
        }
        #endregion

        #region 短信发送
        protected void SendSMSMethod(int typeID)
        {
            #region 获取表单数据
            //单条短信价格
            //SMSDecimal = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigDecimal("SMS_COSTOFSINGLE");
            EyouSoft.IBLL.SMSStructure.ISendMessage sBll = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance();
            string returnVal = "";
            //bool flag = false;
            List<EyouSoft.Model.SMSStructure.AcceptMobileInfo> list = new List<EyouSoft.Model.SMSStructure.AcceptMobileInfo>();
            string strErr = "";
            decimal teleNum = 0;//号码数量
            string erroNumber = "";
            string sendCustomer = "";
            if (!string.IsNullOrEmpty(Utils.GetFormValue(this.txt_SendPeople.UniqueID).Trim()) && ShowSender.Checked)
            {
                sendCustomer = Utils.GetFormValue(this.txt_SendPeople.UniqueID).Trim();
            }
            string tmpPhoneNo = Utils.GetFormValue(this.txt_TelePhoneGroup.UniqueID).Trim();
            string tmpSendInfo = "";
            if (!string.IsNullOrEmpty(Utils.GetFormValue(this.txt_SendContent.UniqueID).Trim()))
            {
                tmpSendInfo = Utils.GetFormValue(this.txt_SendContent.UniqueID).Trim();
            }
            DateTime sendTime = DateTime.Now;
            if (!string.IsNullOrEmpty(Utils.GetFormValue("txt_SendSMS_OrderTime")) && StringValidate.IsDateTime(Utils.GetFormValue("txt_SendSMS_OrderTime")))
            {
                sendTime = DateTime.Parse(Utils.GetFormValue("txt_SendSMS_OrderTime"));
            }

            //发送类型
            EyouSoft.Model.SMSStructure.SendType sendType = (EyouSoft.Model.SMSStructure.SendType)Utils.GetInt(Utils.GetFormValue(this.ddl_SendType.UniqueID));
            //发送通道
            EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[Utils.GetInt(Utils.GetFormValue(this.ddlSendChannel.UniqueID))];

            //加密手机号码
            string strEncryMobile = Utils.GetFormValue("EncryMobile");
            //有关发送所有的数据
            //isSend: false, dataSource:2, companyName:"",customerType:0, provinceId:0, cityId:0
            bool sendAll_isSend = Convert.ToBoolean(Utils.GetFormValue("isSendAll"));
            EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource sendAll_dataSource = EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.平台组团社客户;
            string sendAll_companyName = "";
            int sendAll_customerType = 0;
            int sendAll_provinceId = 0;
            int sendAll_cityId = 0;
            if (sendAll_isSend)
            {
                sendAll_dataSource = (EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource)Convert.ToInt32(Utils.GetFormValue("sendAll.dataSource"));
                sendAll_customerType = Convert.ToInt32(Utils.GetFormValue("sendAll.customerType"));
                sendAll_companyName = Utils.GetFormValue("sendAll.companyName");
                sendAll_provinceId = Convert.ToInt32(Utils.GetFormValue("sendAll.provinceId"));
                sendAll_cityId = Convert.ToInt32(Utils.GetFormValue("sendAll.cityId"));
            }

            #endregion

            #region 数据验证
            if (string.IsNullOrEmpty(tmpSendInfo))
            {
                strErr += "请输入或导入发送内容!" + "\n";
            }
            //验证手机号码
            if (!string.IsNullOrEmpty(tmpPhoneNo))
            {
                tmpPhoneNo = tmpPhoneNo.Replace('，', ',');
                if (tmpPhoneNo.EndsWith(","))
                {
                    tmpPhoneNo = tmpPhoneNo.Substring(0, tmpPhoneNo.Length - 1);
                }
                string[] numberGroup = tmpPhoneNo.Split(',');
                teleNum = numberGroup.Length;
                for (int i = 0; i < teleNum; i++)
                {
                    if (StringValidate.IsMobileOrPHS(numberGroup[i]) == false)
                    {
                        erroNumber += numberGroup[i] + "\n";
                    }
                    else
                    {
                        list.Add(new EyouSoft.Model.SMSStructure.AcceptMobileInfo() { Mobile = numberGroup[i], IsEncrypt = false });
                    }
                }

            }
            //添加加密手机号码
            if (!string.IsNullOrEmpty(strEncryMobile))
            {
                string[] numberGroup = strEncryMobile.Split(',');
                teleNum = numberGroup.Length;
                for (int i = 0; i < teleNum; i++)
                {
                    if (StringValidate.IsMobileOrPHS(numberGroup[i]) == false)
                    {
                        erroNumber += numberGroup[i] + "\n";
                    }
                    else
                    {
                        list.Add(new EyouSoft.Model.SMSStructure.AcceptMobileInfo() { Mobile = numberGroup[i], IsEncrypt = true });
                    }
                }
            }
            //添加发送所有的号码
            if (sendAll_isSend)
            {
                IList<EyouSoft.Model.SMSStructure.AcceptMobileInfo> sendAll_list = EyouSoft.BLL.SMSStructure.Customer.CreateInstance().GetMobiles(CompanyID, "", sendAll_customerType, sendAll_dataSource, sendAll_provinceId, sendAll_cityId);
                if (sendAll_list != null && sendAll_list.Count > 0)
                    list.AddRange(sendAll_list);
            }
            if (list == null || list.Count <= 0)
            {
                strErr += "请输入或导入手机号码!" + "\n";
            }
            if (erroNumber != "")
            {
                strErr += erroNumber + "以上号码格式不正确!" + "\n";
            }
            #endregion

            if (strErr != "")
            {
                returnVal = "0@" + strErr;
            }
            else
            {
                if (typeID == 0)
                {
                    #region 发送短信之前确认发送信息
                    EyouSoft.Model.SMSStructure.SendMessageInfo SMS_Model = new EyouSoft.Model.SMSStructure.SendMessageInfo();

                    SMS_Model.Mobiles = list;
                    SMS_Model.CompanyId = CompanyID;
                    SMS_Model.SMSContent = tmpSendInfo;
                    SMS_Model.UserFullName = sendCustomer;
                    SMS_Model.SendChannel = sendChannel;
                    SMS_Model.SendType = sendType;
                    SMS_Model.SendTime = sendTime;

                    EyouSoft.Model.SMSStructure.SendResultInfo sModel = sBll.ValidateSend(SMS_Model);
                    if (sModel != null)
                    {
                        if (sModel.IsSucceed == true)
                        {
                            //验证成功返回帐户余额以及此次所要发送的短信条数
                            returnVal = string.Format("1@您的账户当前余额为：{0}，此次消费金额为：{1}，将共发送短信{2}条！是否确定发送短信？"
                                , sModel.AccountMoney.ToString("C2")
                                , sModel.CountFee.ToString("C2")
                                , (sModel.WaitSendMobileCount + sModel.WaitSendPHSCount).ToString());
                        }
                        else if (sModel.ErrorMessage.IndexOf("余额不足") > -1)
                        {
                            returnVal = "2@" + sModel.ErrorMessage;
                        }
                        else
                        {
                            returnVal = "0@" + sModel.ErrorMessage;
                        }
                    }
                    sModel = null;
                    SMS_Model = null;
                    #endregion
                }
                else
                {
                    #region 发送操作

                    EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo = new EyouSoft.Model.SMSStructure.SendMessageInfo();
                    sendMessageInfo.CompanyId = CompanyID;
                    sendMessageInfo.CompanyName = this.SiteUserInfo.CompanyName;
                    sendMessageInfo.UserId = this.SiteUserInfo.ID;
                    sendMessageInfo.UserFullName = sendCustomer;
                    sendMessageInfo.SMSContent = tmpSendInfo;
                    sendMessageInfo.SendTime = sendTime;
                    sendMessageInfo.Mobiles = list;
                    sendMessageInfo.SendChannel = sendChannel;
                    sendMessageInfo.SendType = sendType;

                    EyouSoft.Model.SMSStructure.SendResultInfo SendResultModel = sBll.Send(sendMessageInfo);

                    if (SendResultModel.IsSucceed)
                    {
                        string isHasSendUser = "";

                        if (!string.IsNullOrEmpty(sendMessageInfo.UserFullName))
                        {
                            isHasSendUser = "（包括发信人）";
                        }

                        decimal costFee = sendMessageInfo.SendType == EyouSoft.Model.SMSStructure.SendType.直接发送 ? SendResultModel.SendFee : SendResultModel.CountFee;
                        returnVal = "1@您本次共发送短信" + sendMessageInfo.SMSContentSendComplete.Length + "个字" + isHasSendUser + "!\n发送移动、联通共" + SendResultModel.MobileSendNumberCount + "个号码、\n发送小灵通共" + SendResultModel.PHSSendNumberCount + "个号码、\n实际共消费金额为:" + costFee.ToString("C2") + "、\n实际发送短信" + (SendResultModel.MobileSendCount + SendResultModel.PHSSendCount) + "条";
                    }
                    else if (SendResultModel.ErrorMessage.IndexOf("余额不足") > -1)
                    {
                        returnVal = "2@" + SendResultModel.ErrorMessage;
                    }
                    else
                    {
                        returnVal = "0@" + SendResultModel.ErrorMessage;
                    }

                    SendResultModel = null;
                    sendMessageInfo = null;

                    #endregion
                }
            }
            Response.Clear();
            Response.Write(returnVal);
            Response.End();
            sBll = null;
        }
        #endregion

        /// <summary>
        /// 初始化发送通道
        /// </summary>
        private void InitSendChannel()
        {
            EyouSoft.Model.SMSStructure.SMSChannelList channel = new EyouSoft.Model.SMSStructure.SMSChannelList();
            IList<EyouSoft.Model.SMSStructure.SMSChannel> channels = new List<EyouSoft.Model.SMSStructure.SMSChannel>();

            for (int i = 1; i < channel.Count; i++)
            {
                channels.Add(channel[i]);

                this.ddlSendChannel.Items.Add(new ListItem(channel[i].ChannelName, channel[i].Index.ToString()));
            }

            var obj = channels.Select(tmp => new { index = tmp.Index, name = tmp.ChannelName, isLong = tmp.IsLong });

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("SendSMS.channel={0};", Newtonsoft.Json.JsonConvert.SerializeObject(obj)), true);
        }
        /// <summary>
        /// 接受线路传过来的ID，得到线路相关信息，放到短信内容中。
        /// 信息来源：楼
        /// 散拼计划和线路库稍微有点不同，不过都是跳到发送的页面，写入短信内容，让他自己再编辑了
        /// 线路的时间，直接读取计划的最后和最近有效时间；散拼的直接读取打勾的散拼团队了
        /// 线路如果没有计划，就不显示时间了
        ///</summary>
        ///<param name="id">线路ID</param>
        ///<param name="type">0:线路库计划  1:散拼计划</param>
        protected void GetRouteInfo(string id, int type)
        {
            StringBuilder str = new StringBuilder();
            string[] ids = id.Split(',');
            string strRouteName = string.Empty;//线路名称
            string strTime = string.Empty;//时间
            string strPrice = string.Empty;//价格
            string strCon = string.Empty;//线路特色
            if (ids.Length == 0)
                return;
            switch (type)
            {
                case 0://我的线路库
                    IRoute routeBLL = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
                    EyouSoft.Model.NewTourStructure.MRoute routeModel;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        routeModel = routeBLL.GetModel(ids[i]);
                        if (null != routeModel)
                        {
                            strRouteName = routeModel.RouteName;
                            if (!this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                            {
                                strTime = routeModel.TeamPlanDes;
                            }
                            strPrice = string.Format("特价[{0}元]起", Utils.FilterEndOfTheZeroString(routeModel.RetailAdultPrice.ToString("0.00")));
                            strCon = routeModel.Characteristic;
                            str.AppendFormat("{0} {1} {2} {3} ;", strRouteName, strTime, strPrice, strCon);
                        }
                    }
                    break;
                case 1://我的散拼
                    EyouSoft.IBLL.NewTourStructure.IPowderList planBLL = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
                    EyouSoft.Model.NewTourStructure.MPowderList planModel;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        planModel = planBLL.GetModel(ids[i]);
                        if (null != planModel)
                        {
                            strRouteName = planModel.RouteName;
                            strTime = string.Format("{0:MM-dd}", planModel.LeaveDate);
                            strPrice = string.Format("特价[{0}元]起", Utils.FilterEndOfTheZeroString(planModel.RetailAdultPrice.ToString("0.00")));
                            strCon = planModel.Characteristic;
                            str.AppendFormat("{0} {1} {2} {3} ;", strRouteName, strTime, strPrice, strCon);
                        }
                    }
                    break;
            }
            if (str.Length > 0)
            {
                this.txt_SendContent.Text = str.ToString().Trim();
            }
        }
    }
}
