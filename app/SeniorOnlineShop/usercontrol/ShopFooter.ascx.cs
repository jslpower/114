using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeniorOnlineShop.usercontrol
{
    public partial class ShopFooter : System.Web.UI.UserControl
    {
        /// <summary>
        /// 版权
        /// </summary>
        protected string strAllRight = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.IBLL.SystemStructure.ISystemInfo bll = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance();
            EyouSoft.Model.SystemStructure.SystemInfo model = bll.GetSystemInfoModel();
            if (model != null)
            {
                strAllRight = model.AllRight;
            }
            model = null;
            bll = null;

        }
    }
}