using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.BackgroundServices
{
    public class PluginService : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.BackgroundServices.IPluginService
    {
        #region IPluginService 成员

        public IList<EyouSoft.Model.BackgroundServices.IPlugin> GetPlugins()
        {
            IList<EyouSoft.Model.BackgroundServices.IPlugin> list = new List<EyouSoft.Model.BackgroundServices.IPlugin>();
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT * FROM tbl_SysPluging");
            using (IDataReader dr = DbHelper.ExecuteReader(dc , this.SystemStore))
            {
                EyouSoft.Model.BackgroundServices.IPlugin plugin = null;
                while (dr.Read())
                {
                    plugin = new EyouSoft.Model.BackgroundServices.Plugin();
                    plugin.Enabled = dr["Enabled"].ToString() == "1" ? true : false;
                    list.Add(plugin);
                    plugin = null;
                }
            }
            return list;
        }

        public EyouSoft.Model.BackgroundServices.IPlugin GetPlugin(Guid pluginID)
        {
            EyouSoft.Model.BackgroundServices.IPlugin plugin = null;
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT * FROM tbl_SysPluging WHERE PluginID=@PluginID");
            this.SystemStore.AddInParameter(dc, "PluginID", DbType.String, pluginID.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this.SystemStore))
            {
                while (dr.Read())
                {
                    plugin = new EyouSoft.Model.BackgroundServices.Plugin();
                    plugin.Enabled = dr["Enabled"].ToString() == "1" ? true : false;
                }
            }
            return plugin;
        }

        public System.Collections.Specialized.NameValueCollection LoadSettings(EyouSoft.Model.BackgroundServices.IPlugin plugin)
        {
            System.Collections.Specialized.NameValueCollection settings = new System.Collections.Specialized.NameValueCollection();
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT * FROM tbl_SysPlugingSetting WHERE PluginID=@PluginID");
            this.SystemStore.AddInParameter(dc, "PluginID", DbType.String, plugin.ID.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this.SystemStore))
            {
                while (dr.Read())
                {
                    settings.Add(dr["PluginSettingName"].ToString(), dr["PluginSettingValue"].ToString());
                }
            }
            return settings;
        }

        public void Save(EyouSoft.Model.BackgroundServices.IPlugin plugin)
        {
            throw new NotImplementedException();
        }

        public void SaveSetting(EyouSoft.Model.BackgroundServices.IPlugin plugin, string name, string value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
