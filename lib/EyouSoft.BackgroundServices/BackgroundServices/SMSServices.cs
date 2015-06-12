using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.BackgroundServices;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// 短信服务
    /// </summary>
    /// Author:张志瑜 2010-09-21
    public class SMSServices : BackgroundServiceBase, IBackgroundService
    {
        private readonly ISMSServices MSMService;
        private Queue<EyouSoft.Model.SMSStructure.SendPlanInfo> _smsQueue = new Queue<EyouSoft.Model.SMSStructure.SendPlanInfo>();

        public SMSServices(IPluginService pluginService, ISMSServices smsService)
            : base(pluginService)
        {
            this.MSMService = smsService;
            ID = new Guid("{24F679A5-D9D9-461E-850A-E71E2FA6DB43}");
            Name = "短信定时发送服务";
            Category = "Background Services";
        }


        #region IBackgroundService 成员

        public bool ExecuteOnAll
        {
            get
            {
                return bool.Parse(GetSetting("ExecuteOnAll"));
            }
            set
            {
                SaveSetting("ExecuteOnAll", value.ToString());
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return new TimeSpan(long.Parse(GetSetting("Interval")));
            }
            set
            {
                SaveSetting("Interval", value.Ticks.ToString());
            }
        }

        public void Run()
        {
            try
            {
                //string filename = "config/SMS_" + DateTime.Now.ToFileTime() + ".txt";
                //System.IO.StreamWriter sw = new System.IO.StreamWriter(EyouSoft.Common.Utility.GetMapPath(filename));
                //sw.Write("已执行");
                //sw.Close();
                //接收
                if (this._smsQueue == null || this._smsQueue.Count == 0)
                    this._smsQueue = this.MSMService.Get();

                //发送
                if (this._smsQueue != null && this._smsQueue.Count > 0)
                {
                    while (true)
                    {
                        if (this._smsQueue.Count == 0)
                            break;
                        this.MSMService.Send(this._smsQueue.Dequeue());
                    }
                }
            }
            catch { }
        }
        #endregion 
    }
}
