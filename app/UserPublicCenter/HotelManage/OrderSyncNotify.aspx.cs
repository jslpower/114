using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.HotelManage
{   
    /// <summary>
    /// 酒店异步通知
    /// xuty 2010/12/15
    /// </summary>
    public partial class OrderSyncNotify : System.Web.UI.Page
    {
        StreamWriter logWrite = null;//日志写入器
        const string logPath = "/HotelLog/";
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateLogFile();
            string orderXML = Utils.GetFormValue("messageXML");
            logWrite.WriteLine(string.Format("Content:{0}", orderXML));//写入日志内容
            Response.Clear();
            int result=EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().ExecOrderHint(orderXML);
            logWrite.WriteLine(string.Format("Result:{0}", result));//写入结果
            string code = string.Empty;
            string desc = string.Empty;
            Response.Write(EyouSoft.HotelBI.Utils.CreateOrderStatusNotifRS(code, desc));
            logWrite.WriteLine(string.Format("Code:{0}", code));//写入返回指令
            logWrite.WriteLine(string.Format("Desc:{0}", desc));//写入返回结果描述
            logWrite.Close();
            Response.End();
        }
        void Page_Error(object sender, EventArgs e)
        {
            try
            {
                if (logWrite == null)
                {
                    CreateLogFile();
                }
                logWrite.WriteLine(e.ToString());
                logWrite.Close();
            }
            catch
            {
                if (logWrite != null)
                {
                    logWrite.WriteLine(e.ToString());
                    logWrite.Close();
                }
            }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        void CreateLogFile()
        {
            string logPath2 = Server.MapPath(logPath);
            if (!Directory.Exists(logPath2))
            {
                Directory.CreateDirectory(logPath2);
            }
            if (logWrite == null)
            {
                logWrite = new StreamWriter(string.Format("{0}{1}.txt", logPath2, DateTime.Now.ToString("yyyyMMddHHmmssss")));
            }
        }
        
       
    }
}
