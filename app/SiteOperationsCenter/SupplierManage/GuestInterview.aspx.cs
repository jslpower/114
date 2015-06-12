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
    ///  同业嘉宾访谈固定内容
    /// </summary>
    /// 周文超  2010-07-27
    public partial class GuestInterview : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GuestInterviewMenu1.MenuIndex = 1;
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.嘉宾访谈_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                InitPageDate();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitPageDate()
        {
            txtGuestInfo.Value = EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance().GetInterview();
            txtTongye.Value = EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance().GetCommArea();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_修改))
            {
                MessageBox.Show(this.Page, "对不起，你没有该权限");
                return;
            }
            string strGuestInfo = Utils.GetText(txtGuestInfo.Value.Trim(), 1000);
            string strTongye = Utils.GetText(txtTongye.Value.Trim(), 1000);
            if (!string.IsNullOrEmpty(strGuestInfo))
                EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance().SetInterview(strGuestInfo);
            if (!string.IsNullOrEmpty(strTongye))
                EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance().SetCommArea(strTongye);
            MessageBox.ShowAndRedirect(this.Page, "保存成功！", Request.RawUrl);
        }
    }
}
