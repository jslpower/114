using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.BackgroundServices;

namespace EyouSoft.Services.BackgroundServices
{
    public class TourServices : BackgroundServiceBase, IBackgroundService
    {
        private readonly ITourServices tourService;

        public TourServices(IPluginService pluginService, ITourServices tourService)
            : base(pluginService)
        {
            this.tourService = tourService;
            ID = new Guid("{0015C18A-6EF0-41AE-8AEE-F31C6CD7E63D}");
            Name = "团队数量、公司团队数量统计";
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
            foreach (System.Data.DataRow dr in tourService.GetNextOutbound(ExecuteOnAll))
            {
                tourService.Save(dr);
            }
        }

        #endregion
    }
}
