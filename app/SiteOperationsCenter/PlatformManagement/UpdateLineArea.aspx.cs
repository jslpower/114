using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    public partial class UpdateLineArea : EyouSoft.Common.Control.YunYingPage
    {
        protected string areaid = "";
        protected string save = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            areaid = Utils.GetQueryStringValue("AreaId");
            save = Utils.GetQueryStringValue("type");
            if (!IsPostBack)
            {
                Initinfo();
            }
            else
            {
                if (!string.IsNullOrEmpty(save) && save == "Save")
                {
                    Btn_Save();
                }

            }
        }

        #region 初始化信息
        /// <summary>
        /// 初始化信息
        /// </summary>
        protected void Initinfo()
        {
            if (!string.IsNullOrEmpty(areaid))
            {
                txt_LineName.Value = "";
                txt_Subsite.Value = "";

            }
            else
            {
                MessageBox.Show(Page, "初始化数据失败");
            }

        }
        #endregion

        #region 保存修改的数据
        /// <summary>
        /// 保存修改的数据
        /// </summary>
        protected void Btn_Save()
        {
            bool resultb = false;
            var a = txt_LineName.Value;
            StringBuilder strLineAreaId = new StringBuilder();
            foreach (var item in dropSecled.Items)
            {
                strLineAreaId.Append(item);
            }
            MessageBox.Show(Page, strLineAreaId.ToString());
            //此处省略若干字
            Response.Clear();
            if (resultb)
            {
                Response.Write("success");
            }
            else
                Response.Write("failure");
            Response.End();
        }

        #endregion
    }
}
