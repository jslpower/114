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
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Common;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——基础数据维护
    /// 开发人：杜桂云      开发时间：2010-06-24
    /// </summary>
    public partial class BasicDataManage : EyouSoft.Common.Control.YunYingPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //权限验证
                YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_基础数据维护 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.平台管理_基础数据维护, true);
                    return;
                }
                else
                {
                    StringBuilder strJs = new StringBuilder();
                    //初始化数据绑定
                    strJs.Append("ajaxPages.init(0);");
                    strJs.Append("ajaxPages.init(1);");
                    strJs.Append("ajaxPages.init(2);");
                    MessageBox.ResponseScript(this, strJs.ToString());
                }
            }
        }
        #endregion
    }
}
