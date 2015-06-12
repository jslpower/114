using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Model.CompanyStructure;
namespace UserPublicCenter.HomeControl
{
    public partial class GuestsVisit : System.Web.UI.UserControl
    {
        protected string guestsHtml;//成功访谈
        protected string PeerNewHtml = string.Empty;//同业资讯
        public bool isLogin = false;//是否登录
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSuccessGuest();
            GetIndustrylist();
        }

        #region 绑定成功故事
        /// <summary>
        /// 绑定成功故事
        /// </summary>
        protected void BindSuccessGuest()
        {
            //获取成功故事集合
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> guestList = EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance().GetListByNewType(3, 9);
            if (guestList != null && guestList.Count > 0)
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("<div class=\"fixed\">");
                int itemIndex = 1;
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews newModel in guestList)
                {
                    string url = (newModel.GotoUrl != null && newModel.GotoUrl.Length > 0) ? newModel.GotoUrl : EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(newModel.AfficheClass, newModel.Id);
                    if (itemIndex == 1)
                    {
                        strBuilder.AppendFormat(
                            "<div class=\"imgLAreapic imgLAreapic02\" style=\"width:77px;height:77px\"><a href=\"{0}\" target=\"_blank\" ><img src=\"{1}\" alt=\"{2}\" /></a></div><div class=\"imgRArea imgRArea02\"><p><a href=\"{0}\"><b style=\"color:#ff6600;\">{2}</b></a><br />{3}</p></div></div>", 
                            url, 
                            Utils.GetNewImgUrl(newModel.PicPath,3), 
                            Utils.GetText(newModel.AfficheTitle, 13), 
                            Utils.GetText(newModel.AfficheDesc, 40, true));
                        itemIndex++;
                        continue;
                    }
                    if (itemIndex == 2)
                    {
                        strBuilder.Append("<ul class=\"text-list\">");
                    }
                    strBuilder.AppendFormat("<li><a href=\"{0}\" title=\"{3}\" target=\"_blank\">·{1}</a><em style=\"color:#999;\">[{2}]</em></li>", url, Utils.GetText(newModel.AfficheTitle, 15), newModel.IssueTime.ToString("yyyy-MM-dd"),newModel.AfficheTitle);
                    itemIndex++;
                }
                if (itemIndex == 1)
                {
                    strBuilder.Append("</div");
                    strBuilder.Append("<ul class=\"text-list\"></ul>");
                }
                if (itemIndex == 2)
                {
                    strBuilder.Append("</ul");
                }
                guestsHtml = strBuilder.ToString();
            }
        }

        #endregion

        /// <summary>
        /// 绑定同业资讯
        /// </summary>
        private void GetIndustrylist()
        {
            EyouSoft.Model.NewsStructure.MQueryPeerNews mquery = new EyouSoft.Model.NewsStructure.MQueryPeerNews();
            IList<EyouSoft.Model.NewsStructure.MPeerNews> PeerNewList = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().GetGetPeerNewsList(5, null);
            if (PeerNewList != null && PeerNewList.Count > 0)
            {
                //Title   TypeId  AreaName   AreaId  ScenicId  CompanyId
                this.rptPeerNewList.DataSource = PeerNewList;
                this.rptPeerNewList.DataBind();
            }

        }

        /// <summary>
        /// 获取快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }
    }
}