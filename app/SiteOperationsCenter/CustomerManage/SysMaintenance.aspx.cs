using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.CustomerManage
{
    public partial class SysMaintenance : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                bool isManager = CheckMasterGrant(EyouSoft.Common.YuYingPermission.客户资料_系统维护_管理该栏目);
                if (isManager)
                {
                    StringBuilder strJs = new StringBuilder();
                    //初始化数据绑定
                    strJs.Append("ajaxPages.init(0);");
                    strJs.Append("ajaxPages.init(1);");
                    MessageBox.ResponseScript(this, strJs.ToString());
                }
                else
                {
                    Utils.ResponseNoPermit();
                }
            }
        }
    }
}
