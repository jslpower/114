using System;
using System.Threading;
using Microsoft.Practices.Unity;

namespace EyouSoft.Services.BackgroundServices
{
    public class BackgroundServiceExecutor
    {
        private readonly IUnityContainer container;
        private readonly Type type;
        private int order;
        private Timer timer;
        private bool isExecuting;

        public BackgroundServiceExecutor(IUnityContainer container, Type type, int Order)
        {
            this.container = container;
            this.type = type;
            this.order = Order;
        }

        public void Start()
        {
            IBackgroundService backgroundService = (IBackgroundService)container.Resolve(type);

            backgroundService.RefreshSettings();

            //INFO: (erikpo) This check is just to make sure a value was provided for interval and that they can't put in an interval that will take down their server
            if (backgroundService.Interval.TotalSeconds > 10)
            {
#if DEBUG
                //if (backgroundService.Enabled)
                //{
                //    backgroundService.Run();
                //}
#endif

                isExecuting = false;

                timer = new Timer(
                    timerCallback,
                    null,
                    backgroundService.Interval,
                    new TimeSpan(200000000)
                    );
            }
        }

        public void Stop()
        {
            lock (timer)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
            }
        }

        private void timerCallback(object state)
        {
            
            if (!isExecuting)
            {
                IBackgroundService backgroundService = (IBackgroundService)container.Resolve(type);

                backgroundService.RefreshSettings();

                if (backgroundService.Enabled)
                {
                    isExecuting = true;

                    backgroundService.Run();

                    isExecuting = false;
                }
            }

            //TODO: (erikpo) Once background services have a cancel state and timeout interval, check their state and cancel if appropriate
        }
    }
}