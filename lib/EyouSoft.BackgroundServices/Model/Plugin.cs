using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.BackgroundServices
{
    public interface IPlugin
    {
        Guid ID { get; }
        string Name { get; }
        string Category { get; }
        NameValueCollection Settings { get; }
        bool Enabled { get; set; }
        void RefreshSettings();
    }

    public class Plugin : IPlugin
    {
        #region IPlugin Members

        public Guid ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public NameValueCollection Settings
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public void RefreshSettings()
        {
        }

        #endregion
    }
}
