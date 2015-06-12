using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SOAP
{
    /// <summary>
    /// SmsAPI 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SmsAPI : System.Web.Services.WebService
    {
        public SOAP.APISoapHeader soapHeader = new SOAP.APISoapHeader();

        /// <summary>
        /// 发送短信的web服务
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <returns>返回发送结果</returns>
        [System.Web.Services.Protocols.SoapHeader("soapHeader")]
        [WebMethod]
        public EyouSoft.Model.SMSStructure.SendResultInfo Send(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo)
        {
            if (!soapHeader.IsSafeCall)
                throw new Exception("对不起，您没有权限调用此服务！");

            return EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance().Send(sendMessageInfo);
        }
    }
}
