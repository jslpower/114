using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 嘉宾访谈详细页
    /// </summary>
    /// 周文超 2010-08-03
    public partial class HonoredGuestInfo : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 嘉宾访谈ID
        /// </summary>
        private string HonoredGuestId = string.Empty;
        private string HonoredGuestUrl = "/SupplierInfo/HonoredGuest.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            HonoredGuestId = Utils.InputText(Request.QueryString["Id"]);
            Supplier site = (Supplier)this.Master;
            if(site!=null)
                site.MenuIndex = 3;
            if (string.IsNullOrEmpty(HonoredGuestId))
            {
                MessageBox.ShowAndRedirect(this, "未找到您要查看的信息！", HonoredGuestUrl);
                return;
            }

            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPageData()
        {
            EyouSoft.Model.CommunityStructure.HonoredGuest model = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().GetModel(HonoredGuestId);
            if (model == null)
            {
                MessageBox.ShowAndRedirect(this, "未找到您要查看的信息！", HonoredGuestUrl);
                return;
            }

            ltrImg.Text = string.Format("<img src=\"{0}{1}\" width=\"455\" height=\"181\" />", Domain.FileSystem, model.ImgPath);
            ltrTitle.Text = model.Title;
            ltrInfo.Text = model.Content;
            ltrView1.Text = model.Opinion1;
            ltrView2.Text = model.Opinion2;
            ltrView3.Text = model.Opinion3;
            ltrSummary.Text = model.Summary;
        }
    }
}
