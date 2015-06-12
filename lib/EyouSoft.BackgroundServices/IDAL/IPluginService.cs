using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using EyouSoft.Model.BackgroundServices;

namespace EyouSoft.IDAL.BackgroundServices
{
    public interface IPluginService
    {
        IList<IPlugin> GetPlugins();
        IPlugin GetPlugin(Guid pluginID);
        NameValueCollection LoadSettings(IPlugin plugin);
        void Save(IPlugin plugin);
        void SaveSetting(IPlugin plugin, string name, string value);
    }
}
