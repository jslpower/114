using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

namespace ControlLibrary
{
    public class CustomRepeater : Repeater
    {
        protected string emptyText;
        public string EmptyText
        {
            set { emptyText = value; }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            IList dataSource;
            try
            {
                dataSource = base.DataSource as IList;
            }
            catch
            {
                dataSource = null;
            }
            if (dataSource == null || dataSource.Count == 0)
            {
                writer.Write(emptyText);
            }
        }
    }
}
