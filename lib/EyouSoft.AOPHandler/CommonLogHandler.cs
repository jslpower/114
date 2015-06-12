using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;
using EyouSoft.BusinessLogWriter;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

/*
 * 通用日志记录组件
 * 开发人：蒋胜蓝   开发时间：2010-05-24
 */

#region 操作日志模板参数
namespace EyouSoft.AOPHandler.LogAttributeHelper
{
    /// <summary>
    /// 日志模板参数
    /// </summary>
    [DataContract]
    public class LogAttribute
    {
        /// <summary>
        /// 参数位置
        /// </summary>
        [DataMember(Name="Index")]
        public int Index { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        [DataMember(Name = "Attribute")]
        public string Attribute { get; set; }
        /// <summary>
        /// 参数类型(val:单值，class:类，array:数组)
        /// </summary>
        [DataMember(Name = "AttributeType")]
        public string AttributeType
        { get; set; }
    }
}
#endregion

#region 操作日志记录器

namespace EyouSoft.AOPHandler
{
    using EyouSoft.AOPHandler.LogAttributeHelper;
    
    /// <summary>
    /// 日志记录器参数
    /// </summary>
    public class CommonLogHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            if (this.LogWriterType == typeof(EyouSoft.BusinessLogWriter.TourLog))
            {
                this.LogWriter = new EyouSoft.BusinessLogWriter.TourLog();
            }
            else if (this.LogWriterType == typeof(EyouSoft.BusinessLogWriter.OrderLog))
            {
                this.LogWriter = new EyouSoft.BusinessLogWriter.OrderLog();
            }
            else if (this.LogWriterType == typeof(EyouSoft.BusinessLogWriter.RouteLog))
            {
                this.LogWriter = new EyouSoft.BusinessLogWriter.RouteLog();
            }
            else if (this.LogWriterType == typeof(EyouSoft.BusinessLogWriter.WebMasterLog))
            {
                this.LogWriter = new EyouSoft.BusinessLogWriter.WebMasterLog();
            }
            else if (this.LogWriterType == typeof(EyouSoft.BusinessLogWriter.CompanyLog))
            {
                this.LogWriter = new EyouSoft.BusinessLogWriter.CompanyLog();
            }
            else if (this.LogWriterType == typeof(EyouSoft.BusinessLogWriter.ServiceLog))
            {
                this.LogWriter = new EyouSoft.BusinessLogWriter.ServiceLog();
            }
            if(!string.IsNullOrEmpty(this.LogAttribute))
                this.LogAttributeList = EyouSoft.Common.SerializationHelper.InvertJSON<List<LogAttribute>>(this.LogAttribute);
            return new CommonLogHandler(LogWriter = LogWriter, LogTitle = this.LogTitle, LogMessage = this.LogMessage,
                LogAttributeList = this.LogAttributeList, EventCode=this.EventCode, base.Order);   
        }

        public CommonLogHandlerAttribute()
        {
        }

        /// <summary>
        /// 事件类型号
        /// </summary>
        public int EventCode
        {
            get;
            set;
        }

        /// <summary>
        /// 错误事件类型号
        /// </summary>
        public int ErrorCode
        {
            get { return 0 - EventCode; }
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogMessage
        { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle
        { get; set; }

        /// <summary>
        /// 日志记录器
        /// </summary>
        private BusinessLog LogWriter
        { get; set; }

        /// <summary>
        /// 日志内容参数
        /// </summary>
        private List<LogAttribute> LogAttributeList
        { get; set; }

        /// <summary>
        /// 日志内容模板参数,JSON类型为LogAttributeHelper.LogAttribute
        /// 参数数量必须与日志内容模板中标记数量一致
        /// </summary>
        public string LogAttribute
        { get; set; }

        /// <summary>
        /// 日志记录器类型(EyouSoft.BusinessLogWriter)
        /// </summary>
        public Type LogWriterType 
        { get; set; }

    }

    /// <summary>
    /// 日志记录器
    /// </summary>
    public class CommonLogHandler:ICallHandler
    {
        #region ICallHandler 成员

        /// <summary>
        /// 日志参数循环计数
        /// </summary>
        int i = 0;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        bool EventOK = true;

        public CommonLogHandler(BusinessLog _logWriter, string _logTitle, string _logMessage, List<LogAttributeHelper.LogAttribute> _logAttribute, int _eventCode, int _order)
        {
            this.LogWriter = _logWriter;
            this.LogTitle = _logTitle;
            this.LogMessage = _logMessage;
            this.Order = _order;
            this.LogAttribute = _logAttribute;
            this.EventCode = _eventCode;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var returnMessage = getNext()(input, getNext);
            if (returnMessage.Exception == null)
            {   
                EyouSoft.BusinessLogWriter.Model.LogWriter model = new EyouSoft.BusinessLogWriter.Model.LogWriter();
                model.EventCode = this.EventCode;
                #region 操作结果判断
                if (returnMessage.ReturnValue is int && (int)returnMessage.ReturnValue!=1)
                {
                    LogMessage = "操作失败";
                    model.EventCode = this.ErrorCode;
                    EventOK = false;
                }
                else if (returnMessage.ReturnValue is bool && !(bool)returnMessage.ReturnValue)
                {
                    LogMessage = "操作失败";
                    model.EventCode = this.ErrorCode;
                    EventOK = false;
                }
                else if (returnMessage.ReturnValue is EyouSoft.Model.ResultStructure.ResultInfo && (int)returnMessage.ReturnValue != 1)
                {
                    LogMessage = "操作失败";
                    model.EventCode = this.ErrorCode;
                    EventOK = false;
                }
                else if (returnMessage.ReturnValue is EyouSoft.Model.ResultStructure.UserResultInfo && (int)returnMessage.ReturnValue != 1)
                {
                    LogMessage = "操作失败";
                    model.EventCode = this.ErrorCode;
                    EventOK = false;
                }
                #endregion 
                #region 日志内容拼接
                if (EventOK && LogAttribute!=null)
                {
                    foreach (LogAttribute attribute in this.LogAttribute)
                    {
                        object obj = new object();
                        if (attribute.AttributeType == "class")
                        {
                            Type ArguType = input.Arguments[attribute.Index].GetType();
                            System.Reflection.PropertyInfo pis = ArguType.GetProperty(attribute.Attribute);
                            if (pis != null)
                            {
                                obj = pis.GetValue(input.Arguments[attribute.Index], null);
                            }
                        }
                        else if (attribute.AttributeType == "array")
                        {
                            string logArug = "";
                            if (input.Arguments[attribute.Index] is int[])
                            {
                                int[] target = (int[])input.Arguments[attribute.Index];
                                foreach (int arg in target)
                                {
                                    logArug += arg.ToString() + ",";
                                }
                            }
                            else
                            {
                                object[] target = (object[])input.Arguments[attribute.Index];
                                foreach (object arg in target)
                                {
                                    logArug += arg.ToString() + ",";
                                }
                            }
                            if (logArug.EndsWith(","))
                                logArug = logArug.Substring(0, logArug.Length - 1);
                            obj = logArug.ToString();
                        }
                        else
                        {
                            obj = input.Arguments[attribute.Index].ToString();
                        }
                        LogMessage = LogMessage.Replace("{" + i + "}", obj.ToString());
                        i++;
                    }
                }
                #endregion
                
                
                if (!(LogWriter is EyouSoft.BusinessLogWriter.WebMasterLog))
                {
                    EyouSoft.SSOComponent.Entity.UserInfo UserInfo = null;
                    if (UserInfo == null)
                    {
                        UserInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                    }                    
                    if(UserInfo==null)
                    {
                        UserInfo = new EyouSoft.Security.Membership.UserProvider().GetMQUser();
                    }
                    model.CompanyId = "";
                    model.OperatorId = "";
                    model.OperatorName = "";
                    if (UserInfo != null)
                    {
                        model.CompanyId = UserInfo.CompanyID;
                        model.OperatorId = UserInfo.ID;
                        model.OperatorName = UserInfo.UserName;
                    }
                }
                else if (LogWriter is EyouSoft.BusinessLogWriter.WebMasterLog)
                {
                    EyouSoft.SSOComponent.Entity.MasterUserInfo MasterUserInfo = new EyouSoft.Security.Membership.UserProvider().GetMaster();
                    if (MasterUserInfo != null)
                    {
                        model.CompanyId = string.Empty;
                        model.OperatorId = MasterUserInfo.ID.ToString();
                        model.OperatorName = MasterUserInfo.UserName;
                    }
                }
                model.EventTitle = this.LogTitle;
                if (EventOK)
                {
                    model.EventMessage = this.LogMessage.Replace("[人员]", model.OperatorName).Replace("[时间]", DateTime.Now.ToString());
                }
                else
                {
                    model.EventMessage = LogMessage;
                }
                model.EventIP = EyouSoft.Common.Utility.GetRemoteIP();
                model.EventUrl = EyouSoft.Common.Utility.GetRequestUrl();
                model.EventID = System.Guid.NewGuid().ToString();
                model.EventTime = DateTime.Now;
                LogWriter.WriteLog(model);
            }
            return returnMessage;
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogMessage
        { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle
        { get; set; }

        /// <summary>
        /// 事件类型号
        /// </summary>
        public int EventCode
        {
            get;
            set;
        }

        /// <summary>
        /// 错误事件类型号
        /// </summary>
        public int ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public BusinessLog LogWriter
        { get; set; }

        /// <summary>
        /// 日志模板参数
        /// </summary>
        public List<LogAttribute> LogAttribute
        { get; set; }

        public int Order
        {
            get;
            set;
        }

        #endregion
    }
}
#endregion