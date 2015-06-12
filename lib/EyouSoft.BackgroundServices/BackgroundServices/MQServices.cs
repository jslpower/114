using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.BackgroundServices;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// MQ留言短信提醒
    /// </summary>
    public class MQServices : BackgroundServiceBase, IBackgroundService
    {
        private readonly IMQServices MQService;

        public MQServices(IPluginService pluginService, IMQServices mqService)
            : base(pluginService)
        {
            this.MQService = mqService;
            ID = new Guid("{5C3CC25C-7706-4716-9149-99263334696A}");
            Name = "MQ留言短信提醒";
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
                //string filename = "config/MQ_" + DateTime.Now.ToFileTime() + ".txt";
                //System.IO.StreamWriter sw = new System.IO.StreamWriter(EyouSoft.Common.Utility.GetMapPath(filename));
                //sw.Write("已执行");
                //sw.Close();
                foreach (EyouSoft.Model.MQStructure.IMMessage message in this.MQService.GetTodayLastMessage(0))
                {
                    this.MQService.SendMessage(message.src, message.dst);
                }
            }
            catch { }
        }
       

        #endregion
    }
}
