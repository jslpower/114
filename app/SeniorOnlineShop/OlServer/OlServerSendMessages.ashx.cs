using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using EyouSoft.Common;
namespace WEB.OlServer
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-25
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OlServerSendMessages : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //获取发送的消息JSON字符串
            string msginfo = context.Request.Form["msginfo"];

            EyouSoft.Model.OnLineServer.OlServerMessageInfo messageInfo = null;
            //发送消息的结果信息
            EyouSoft.Model.OnLineServer.OlServerSendMessageResultInfo sendResultInfo = new EyouSoft.Model.OnLineServer.OlServerSendMessageResultInfo(string.Empty, DateTime.Now, false);

            //将JSON字符串反序列化
            try
            {
                messageInfo = (EyouSoft.Model.OnLineServer.OlServerMessageInfo)JsonConvert.DeserializeObject(msginfo, typeof(EyouSoft.Model.OnLineServer.OlServerMessageInfo));
            }
            catch{}

            //消息转化成功写入
            if (messageInfo != null)
            {
                messageInfo.SendTime=DateTime.Now;
                EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
                messageInfo.SendTime = sendResultInfo.SendTime;
                //消息内容过虑
                messageInfo.Message =Utils.InputText(messageInfo.Message);
                sendResultInfo.MessageId = bll.InsertMessage(messageInfo);

                messageInfo = null;
                bll = null;

                sendResultInfo.IsSuccess = true;
            }

            //返回字符串
            string sendResultInfoJsonString = JsonConvert.SerializeObject(sendResultInfo);

            context.Response.Write(sendResultInfoJsonString);
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }    
}
