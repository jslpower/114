using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Transactions;

namespace EyouSoft.AOPHandler
{
    public class MyHandler : ICallHandler
    {
        public int Order { get; set; }//这是ICallHandler的成员，表示执行顺序   
        public string MessageTemplate
        { get; set; }

        public MyHandler(string messageTemplate, int order)
        {
            MessageTemplate = messageTemplate;
            Order = order;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn returnMessage;

            System.Web.HttpContext.Current.Response.Write("<BR>MyHandler开始执行成功:" + MessageTemplate);
            using (TransactionScope scope = new TransactionScope())
            {
                returnMessage = getNext()(input, getNext);
                returnMessage.InvocationContext.Add("User", "adf");
                if (returnMessage.Exception == null)
                {
                    scope.Complete();
                    System.Web.HttpContext.Current.Response.Write("<BR>MyHandler执行成功，无异常,消息:" + MessageTemplate);
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<BR>Exxxxxx:" + returnMessage.Exception.Message);
                }
            }
            return returnMessage;
        }
    }
    public class MyHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new MyHandler(MessageTemplate = this.MessageTemplate,base.Order);//返回MyHandler   
        }

        public MyHandlerAttribute()
        { 
            this.MessageTemplate = "{Message}";
        }

        public string MessageTemplate
        { get; set; }
    }

    public class MyLogHandler : ICallHandler
    {
        public int Order { get; set; }//这是ICallHandler的成员，表示执行顺序   
        public string MessageTemplate
        { get; set; }

        public MyLogHandler(string messageTemplate,int order)
        {
            MessageTemplate = messageTemplate;
            Order = order;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            System.Web.HttpContext.Current.Response.Write("<BR>logging执行前,消息:" + MessageTemplate);
            var returnMessage = getNext()(input, getNext);
            if (returnMessage.Exception == null)
                System.Web.HttpContext.Current.Response.Write("<BR>logged执行成功，无异常,消息:" + MessageTemplate);
            else
            {
                System.Web.HttpContext.Current.Response.Write("<BR>logged执行成功，有异常发生,消息:" + MessageTemplate);
            }
            return returnMessage;
        }
    }
    public class MyLogHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new MyLogHandler(MessageTemplate = this.MessageTemplate , base.Order);//返回MyHandler   
        }

        public MyLogHandlerAttribute()
        {
            this.MessageTemplate = "{Message}";
        }

        public string MessageTemplate
        { get; set; }
    }

}
