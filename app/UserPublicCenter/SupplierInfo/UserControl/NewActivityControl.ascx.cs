using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.SupplierInfo.UserControl
{
    /// <summary>
    /// 最新活动（新闻中心）控件
    /// </summary>
    /// 周文超 2010-08-06
    public partial class NewActivityControl : EyouSoft.Common.Control.BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitPageData()
        {
            rptNewActivity.DataSource = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(4, EyouSoft.Model.SystemStructure.AfficheType.最新活动);
            rptNewActivity.DataBind();
        }
    }
}