using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.BackgroundServices;

namespace EyouSoft.Services.BackgroundServices
{
    public class AdvServices : BackgroundServiceBase, IBackgroundService
    {
        private readonly IAdvServices AdvService;

        public AdvServices(IPluginService pluginService, IAdvServices advService)
            : base(pluginService)
        {
            this.AdvService = advService;
            ID = new Guid("{A0EE9809-375F-407c-B797-DBFD352D914F}");
            Name = "广告数据同步";
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
            //每天晚上一点钟执行
            if (DateTime.Now.Hour != 1)
                return;
            AdvService.RunUpdate();
            IList<string> postion = AdvService.GetPostion();
            foreach (string pos in postion)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.CacheTag.Adv.SystemAdvPostion, pos), DateTime.Now);
            }
        }

        #endregion
    }
}
