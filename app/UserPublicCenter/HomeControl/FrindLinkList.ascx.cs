using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页友情链接
    /// </summary>
    public partial class FrindLinkList : System.Web.UI.UserControl
    {
        #region 变量定义

        protected string strAllFriendLink="";
        //友情链接类型
        private string linkType;
        public string LinkType
        {
            get { return linkType; }
            set { linkType = value; }
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                this.GetFrindLink();
            }
        }
        #endregion

        #region 友情链接
        /// <summary>
        /// 获的友情链接
        /// </summary>
        protected void GetFrindLink()
        {
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> FrindLinkList = null;
            if (linkType != null)
            {
                //判断是否为资讯首页友情链接
                if (linkType == "LinkType")
                {
                    FrindLinkList = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.资讯首页);
                }
            }
            else
            {
                FrindLinkList = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.文字);
            }
            if (FrindLinkList != null && FrindLinkList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysFriendLink Model in FrindLinkList)
                {
                    strAllFriendLink += string.Format("<a href='{0}' target=\"_blank\">{1}</a> | ", Model.LinkAddress, Model.LinkName);
                }
            }
            if (strAllFriendLink != "")
            {
                strAllFriendLink = strAllFriendLink.Substring(0, strAllFriendLink.Length - 2);
            }
            FrindLinkList = null;
        }
        #endregion
    }
}