/*
qgroup.ashx处理程序
响应HTTP POST请求，非POST方式请求返回相应错误代码
响应完成后输出json格式数据 {"retcode":0,"sid":""}
其中retcode为int类型数据，0表示成功，其它失败
sid为成功时底层[EyouSoft.BLL.CommunityStructure.ExchangeList.QG_InsertQQGroupMessage()]返回MessageId，异常或错误时赋空值
POST过来的数据有：
title 消息标题
content 消息内容
time 消息发送时间
send_qg 群号
send_qu Q号
from_u 登录Q号
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace SOAP
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class qgroup : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //获取客户端使用的http数据传输方式
            string Method = context.Request.HttpMethod;
            if (Method == "post" || Method == "POST")
            {
                context.Response.Cache.SetNoStore();
                //标题
                string title = context.Request.Form["title"];
                //消息内容
                string content = context.Request.Form["content"];
                //消息发送时间
                string time = context.Request.Form["time"];
                //群号 
                string send_qg = context.Request.Form["send_qg"];
                //Q号
                string send_qu = context.Request.Form["send_qu"];
                string from_u = context.Request.Form["from_u"];
                      
                EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo messageinfo = new EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo();
                messageinfo.Title = title;
                messageinfo.Content = content;
                messageinfo.QMTime = time;
                messageinfo.QGID = send_qg;
                messageinfo.QUID = send_qu;
                messageinfo.IssueTime = DateTime.Now;                
                messageinfo.Status = EyouSoft.Model.CommunityStructure.QQGroupMessageStatus.未激活;
                messageinfo.FUID = from_u;

                try
                {
                    //失败 or 成功
                    int result = new EyouSoft.BLL.CommunityStructure.ExchangeList().QG_InsertQQGroupMessage(messageinfo);
                    
                    if (result ==1)
                    {
                        context.Response.Write("{\"retcode\":" + 0 + ",\"sid\":\"" + messageinfo.MessageId + "\"}");
                    }
                    else
                    {
                        context.Response.Write("{\"retcode\":-3,\"sid\":\"failure\"}");
                    }
                }
                catch
                {
                    context.Response.Write("{\"retcode\":-2,\"sid\":\"failure\"}");
                }                                              
            }
            else
            {
                context.Response.Write("{\"retcode\":\"-1\",\"sid\":\"failure\"}");
            }                                    
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
