using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.usercontrol.FinancialManagement
{
    public partial class FinancialManageTab : System.Web.UI.UserControl
    {
        protected string ImageServerUrl = ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 设置停留的Tab项
        /// </summary>
        private int _TabIndex = 0;
        /// <summary>
        /// 设置停留的Tab项
        /// </summary>
        public int TabIndex
        {
            get
            {
                return _TabIndex;
            }
            set
            {
                _TabIndex = value;
            }
        }
        protected bool IsGrant = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            IsGrant = ((EyouSoft.Common.Control.BasePage)this.Page).CheckGrant(EyouSoft.Common.TravelPermission.营销工具_财务管理);
        }
    }
}