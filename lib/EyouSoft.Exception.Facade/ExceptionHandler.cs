using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;

namespace EyouSoft.Exception.Facade
{
    [ConfigurationElementTypeAttribute(typeof(CustomHandlerData))]
    public class ExceptionHandler:IExceptionHandler
    {

        #region IExceptionHandler 成员

        
        public ExceptionHandler(System.Collections.Specialized.NameValueCollection c)
        {
            
        }

        public System.Exception HandleException(System.Exception exception, Guid handlingInstanceId)
        {
            new ExceptionWriter().WriteException(EyouSoft.Common.Utility.GetRemoteIP(), EyouSoft.Common.Utility.GetRequestUrl(), exception.Message,
                exception.Source, exception.StackTrace);           
            //System.Web.HttpContext.Current.Response.Write(exception.InnerException);
            //System.Web.HttpContext.Current.Response.End();
            throw exception;            
        }  
              
        #endregion
    }

    /// <summary>
    /// 系统异常记录
    /// </summary>
    public class ExceptionWriter:EyouSoft.Common.DAL.DALBase
    {
        const string SQL_EXCEPTION_ADD = "INSERT INTO tbl_LogException([ErrorID],[RemoteIP],[RequestUrl],[ErrorMessage],[ErrorSource],[StackTrace]" +
                                          ")VALUES(@ErrorID,@RemoteIP,@RequestUrl,@ErrorMessage,@ErrorSource,@StackTrace)";
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="RemoteIP">错误发生IP</param>
        /// <param name="PageUrl">错误发生页面</param>
        /// <param name="Message">错误信息</param>
        /// <param name="Source">错误程序集</param>
        /// <param name="StackTrace">错误发生位置</param>
        public void WriteException(string RemoteIP,string PageUrl,string Message,string Source,string StackTrace)
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_EXCEPTION_ADD);
            this.LogStore.AddInParameter(dc, "ErrorID", DbType.String, Guid.NewGuid().ToString());
            this.LogStore.AddInParameter(dc, "RemoteIP", DbType.String, RemoteIP);
            this.LogStore.AddInParameter(dc, "RequestUrl", DbType.String, PageUrl);
            this.LogStore.AddInParameter(dc, "ErrorMessage", DbType.String, Message);
            this.LogStore.AddInParameter(dc, "ErrorSource", DbType.String, Source);
            this.LogStore.AddInParameter(dc, "StackTrace", DbType.String, StackTrace);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
    }
}

