using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Web.SessionState;
using EyouSoft.Services.BackgroundServices;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using EyouSoft.IDAL.BackgroundServices;
using EyouSoft.DAL.BackgroundServices;


namespace EyouSoft.WEB
{
    public class EyouSoftApplication : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            registerControllerFactory();
            launchBackgroundServices();
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg.ToLower() == "sessionid")
            {
                if (Session["CacheItem"] == null)
                    Session["CacheItem"] = 1;
                return Session["CacheItem"].ToString();
            }
            return base.GetVaryByCustomString(context, arg);
        }

        private void registerControllerFactory()
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = null;
            System.Configuration.Configuration config = null;
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();

            map.ExeConfigFilename = EyouSoft.Common.Utility.GetMapPath("/Config/IBLL.Configuration.xml");
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            section = (UnityConfigurationSection)config.GetSection("unity");
            section.Containers.Default.Configure(container);

            map.ExeConfigFilename = EyouSoft.Common.Utility.GetMapPath("/Config/IDAL.Configuration.xml");
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            section = (UnityConfigurationSection)config.GetSection("unity");
            section.Containers.Default.Configure(container);

            container
                .RegisterType<ISMSServices, EyouSoft.DAL.BackgroundServices.SMSServices>()
                .RegisterType<IMQServices, EyouSoft.DAL.BackgroundServices.MQServices>()
                .RegisterType<IPluginService, PluginService>();   

            Application.Add("container", container);
        }

        private void launchBackgroundServices()
        {
            if (!System.IO.File.Exists(EyouSoft.Common.Utility.GetMapPath("/Config/BackgroundServices.txt")))
                return;

            IUnityContainer container = (IUnityContainer)Application["container"];

            BackgroundServicesExecutor backgroundServicesExecutor = (BackgroundServicesExecutor)Application["backgroundServicesExecutor"];

            if (backgroundServicesExecutor == null)
            {
                backgroundServicesExecutor = new BackgroundServicesExecutor(container);

                Application.Add("backgroundServicesExecutor", backgroundServicesExecutor);

                backgroundServicesExecutor.Start();
            }
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

            EyouSoft.Exception.Facade.ApplicationException.ProcessException(Server.GetLastError().GetBaseException(), "MyPolicy");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            BackgroundServicesExecutor backgroundServicesExecutor = (BackgroundServicesExecutor)Application["backgroundServicesExecutor"];

            if (backgroundServicesExecutor != null)
                backgroundServicesExecutor.Stop();
        }
    }
}