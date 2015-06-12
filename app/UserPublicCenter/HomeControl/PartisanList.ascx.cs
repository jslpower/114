using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页战略合作伙伴
    /// </summary>
    public partial class PartisanList : System.Web.UI.UserControl
    {
        protected string strPartisanList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.GetPartisanList();
            }

        }

        /// <summary>
        /// 绑定战略合作伙伴列表
        /// </summary>
        protected void GetPartisanList()
        {
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> LinkList = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.战略合作);
            if (LinkList != null && LinkList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysFriendLink Model in LinkList)
                {
                    if (Model.ImgPath != "")
                    {
                        strPartisanList += string.Format("<a href='{0}' target=\"_blank\"><image src='{1}' height='30' width='100' /></a>", Model.LinkAddress, EyouSoft.Common.Domain.FileSystem + Model.ImgPath);
                    }
                }
            }
            LinkList = null;
        }
    }
}