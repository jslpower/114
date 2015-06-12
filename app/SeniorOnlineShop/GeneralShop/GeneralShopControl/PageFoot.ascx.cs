using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeniorOnlineShop.GeneralShop.GeneralShopControl
{
    /// <summary>
    ///  普通网店尾部控件
    /// </summary>
    /// 周文超 2011-11-14
    public partial class PageFoot : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetUnionRight();
            }
        }

        /// <summary>
        /// 版权
        /// </summary>
        protected string UnionRight = "";

        /// <summary>
        /// 获的版权
        /// </summary>
        protected void GetUnionRight()
        {
            EyouSoft.Model.SystemStructure.SystemInfo model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (model != null)
            {
                UnionRight = model.AllRight;
            }
        }
    }
}