using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace EyouSoft.Services.BackgroundServices
{
    public class BackgroundServicesExecutor
    {
        private IUnityContainer container;
        private readonly List<BackgroundServiceExecutor> executors;

        public BackgroundServicesExecutor(IUnityContainer container)
        {
            this.container = container;

            //TODO: (erikpo) Once we have a plugin framework in place to load types from different assemblies, get rid of the below hardcoded values and load up all background services dynamically
            executors = new List<BackgroundServiceExecutor>(3);
            executors.Add(new BackgroundServiceExecutor(container, typeof(SMSServices), 1));
            executors.Add(new BackgroundServiceExecutor(container, typeof(MQServices), 2));
            //executors.Add(new BackgroundServiceExecutor(container, typeof(AdvServices)));
        }

        public void Start()
        {
            foreach (BackgroundServiceExecutor executor in executors)
            {
                executor.Start();
            }
        }

        public void Stop()
        {
            foreach (BackgroundServiceExecutor executor in executors)
            {
                executor.Stop();
            }
        }
    }
}
