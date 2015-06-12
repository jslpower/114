using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：短信发送验证异步处理
    /// 开发人：杜桂云  开发时间：2010-08-03
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CheckSendByAjax : IHttpHandler
    {
        #region 处理请求
        public void ProcessRequest(HttpContext context)
        {
            /*
            IList<string> list = new List<string>();
            bool ReturnFlag = false;
            context.Response.ContentType = "text/plain";

            #region 获取传递过来的参数
            string SendTelGroup = "";
            decimal SMSDecimal = 0;//单条短信价格
            string SendContentInfo = "";
            string SendPeople = "";
            //获取请求页面传递过来的参数
            if (context.Request.Form["SendTelGroup"] != null)
            {
                string tmpSendTelGroup = context.Request.Form["SendTelGroup"];
                SendTelGroup = tmpSendTelGroup.Replace('，', ',');
                if (SendTelGroup.EndsWith(","))
                {
                    SendTelGroup = SendTelGroup.Substring(0, SendTelGroup.Length - 1);
                }
                string[] numberGroup = SendTelGroup.Split(',');
                for (int i = 0; i < numberGroup.Length; i++)
                {
                    if (StringValidate.IsMobilePhone(numberGroup[i]))
                    {
                        list.Add(numberGroup[i]);
                    }
                }

            }
            //单条短信的价格
            if (!string.IsNullOrEmpty(context.Request.Form["SMSDecimal"]))
            {
                SMSDecimal = decimal.Parse(context.Request.Form["SMSDecimal"]);
            }
            //发送内容
            if (context.Request.Form["SendContentInfo"] != null)
            {
                SendContentInfo = context.Request.Form["SendContentInfo"];
            }
            //发送人或公司名称
            if (context.Request.Form["SendCompanyName"] != null)
            {
                SendPeople = context.Request.Form["SendCompanyName"];
            }
            #endregion
            EyouSoft.IBLL.SMSStructure.ISendMessage sBll = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance();
            EyouSoft.Model.SMSStructure.SendMessageInfo SMS_Model = new  EyouSoft.Model.SMSStructure.SendMessageInfo ();
            SMS_Model.AcceptMobiles = list;
            SMS_Model.CompanyId = Utils.InputText(context.Request.Form["CompanyID"]);
            SMS_Model.UserFullName = SendPeople;
            SMS_Model.SMSContent = SendContentInfo;
            //SMS_Model.UseMoeny = SMSDecimal;
            EyouSoft.Model.SMSStructure.SendResultInfo sModel = sBll.ValidateSend(SMS_Model);
            if (sModel != null)
            {
                ReturnFlag = sModel.IsSucceed;
                if (ReturnFlag == true)
                {
                    int countNum = sModel.WaitSendMobileCount + sModel.WaitSendPHSCount;
                    //验证成功返回帐户剩余短信条数以及此次所要发送的短信条数
                    //context.Response.Write("1@" + sModel.AccountSMSNumber.ToString() + "@" + countNum.ToString());
                    context.Response.End();
                }
                else
                {
                    //验证短信失败返回底层错误消息
                    context.Response.Write("0@" + sModel.ErrorMessage);
                    context.Response.End();
                }
            }
            sModel = null;
            SMS_Model = null;
            sBll = null;*/
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
