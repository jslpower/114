using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SiteOperationsCenter.SupplierManage
{
    /// <summary>
    /// 学堂介绍
    /// </summary>
    /// 周文超 2010-07-29
    public partial class SchoolIntroduction : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.同业学堂_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        private void InitPageData()
        {
            txtIntroduction.Value = EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance().GetSchool();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strIntroduction = Utils.GetText(txtIntroduction.Value.Trim(), 1000);
            if (!string.IsNullOrEmpty(strIntroduction))
            {
                EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance().SetSchool(strIntroduction);
                MessageBox.ShowAndRedirect(this.Page, "保存成功！", Request.RawUrl);
            }
        }
    }
}
