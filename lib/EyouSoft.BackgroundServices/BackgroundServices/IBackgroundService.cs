using System;

namespace EyouSoft.Services.BackgroundServices
{
    public interface IBackgroundService : EyouSoft.Model.BackgroundServices.IPlugin
    {
        bool ExecuteOnAll { get; set; }
        TimeSpan Interval { get; set; }
        void Run();
    }
}
