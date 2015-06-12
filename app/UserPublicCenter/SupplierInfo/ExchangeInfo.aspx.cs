using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using System.Text.RegularExpressions;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 供求详细页
    /// author dj
    /// date 2011 5 13
    /// </summary>
    public partial class ExchangeNewInfo : EyouSoft.Common.Control.FrontPage
    {

        protected string id = string.Empty; ///信息ID
        protected EyouSoft.Model.CommunityStructure.ExchangeList emodel = null;

        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> lastlist = null;
        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> otherlist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //供求频道移除了分站的要求，修改了RewriteUrl，所以将老的ReWriter URL请求进行301响应处理。
            //this can tells a search engine to update their cache and reassign the old url to the new url.
            Regex re = new Regex(@"^/info_([a-zA-Z0-9-]+)_\d+$", RegexOptions.IgnoreCase);
            Match match = re.Match(Request.RawUrl);
            if (match.Success)
            {
                Utils.RedirectPermanent(EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(match.Result("$1"),0), true);
            }

            Bind();

            Page.Title = emodel.ExchangeTitle + "_同业114供求信息频道";
            AddMetaTag("description", "杭州旅游供求频道,为您提供杭州地区最新旅游信息,旅游产品即时报价,特价旅游线路和旅行社收客信息查询,还可以发布团队询价,地接报价,组团拼团,旅游票务签证,车辆租赁等旅游供求信息.");
            AddMetaTag("keywords", emodel.ExchangeTitle);
        }

        /// <summary>
        /// 初始化供求信息
        /// </summary>
        private void Bind()
        {
            id = EyouSoft.Common.Utils.GetQueryStringValue("ID");
            emodel = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetModel(id);

            if (emodel == null)
            {
                Utils.ShowError("未找到当前供求信息", "info");
                return;
            }


            lastlist = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5,emodel.TopicClassID,emodel.ExchangeTag,true);
            otherlist = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5,emodel.TopicClassID,emodel.ExchangeTag,false);
            SetClicks();
            if (IsLogin)//当前有用户登录
            {
                SetHistory();//写浏览记录
            }

            string fileName = "";
            string filePath = "";

            if (!string.IsNullOrEmpty(emodel.AttatchPath))
            {
                if (emodel.AttatchPath.Split('|').Length > 1)
                {
                    fileName = emodel.AttatchPath.Split('|')[0];
                    filePath = emodel.AttatchPath.Split('|')[1];
                }
                else
                {
                    fileName = "查看附件";
                    filePath = emodel.AttatchPath;
                }
                ltrDownLoad.Text = string.Format("<span class=\"lan14\"><strong>附 件:</strong></span><a target=\"_blank\" href=\"{0}{1}\">{3}<img src=\"{2}/images/UserPublicCenter/baocun.gif\" width=\"14\" height=\"13\" /><img src=\"{2}/images/UserPublicCenter/dakai.gif\" width=\"16\" height=\"14\" /></a>", Domain.FileSystem, filePath, Domain.ServerComponents, fileName);
            }
        }



        /// <summary>
        /// 获取连接地址
        /// </summary>
        public string GetReturnUrl()
        {
            string m = this.Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(this.Request.Url.ToString(), "_parent", "");
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool GetIsLogin()
        {
            return IsLogin;
        }

        protected void ibtnSave_Click(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                MessageBox.ShowAndReturnBack(this, "请先登录！", 1);
                return;
            }

            string strInfo = Utils.InputText(txtCommentInfo.Value.Trim());
            string strTopicId = id;
            string strCommentId = Utils.GetFormValue("hidCommentId");
            if (string.IsNullOrEmpty(strInfo))
            {
                MessageBox.ShowAndReturnBack(this, "回复内容不能为空！", 1);
                return;
            }
            if (strInfo.Length > 500)
            {
                MessageBox.ShowAndReturnBack(this, "回复内容应限制在500个字符以内！", 1);
                return;
            }

            EyouSoft.Model.CommunityStructure.ExchangeComment model = new EyouSoft.Model.CommunityStructure.ExchangeComment();
            model.CommentId = strCommentId;
            model.CommentIP = StringValidate.GetRemoteIP();
            model.CommentText = strInfo;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.IsAnonymous = false;
            model.IsCheck = false;
            model.IsDeleted = false;
            model.IsHasNextLevel = false;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = SiteUserInfo.ContactInfo == null ? "0" : SiteUserInfo.ContactInfo.MQ;
            model.OperatorName = SiteUserInfo.ContactInfo == null ? string.Empty : SiteUserInfo.ContactInfo.ContactName;
            model.TopicId = strTopicId;
            model.TopicType = EyouSoft.Model.CommunityStructure.TopicType.供求;

            if (EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance().AddExchangeComment(model) == 1)
            {
                MessageBox.ShowAndRedirect(this, "提交评论成功！", Request.RawUrl);
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "提交评论失败！", Request.RawUrl);
                return;
            }
        }



        /// <summary>
        /// 写浏览记录
        /// </summary>
        private void SetHistory()
        {
            EyouSoft.Model.CommunityStructure.ExchangeVisited model = new EyouSoft.Model.CommunityStructure.ExchangeVisited();
            model.CompanyId = SiteUserInfo.CompanyID;
            model.ExchangeId = id;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().SetVisited(model);
            model = null;
        }

        /// <summary>
        /// 更新浏览次数
        /// </summary>
        private void SetClicks()
        {
            if (Request.Cookies["ExchangeInfo" + id] == null)
            {
                bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().SetReadCount(id);
                if (Result)
                {
                    HttpCookie ExchangeInfoCookie = new HttpCookie("ExchangeInfo" + id);
                    ExchangeInfoCookie.Expires = DateTime.Now.AddMinutes(5);
                    ExchangeInfoCookie.Path = "/SupplierInfo";
                    Response.Cookies.Add(ExchangeInfoCookie);
                }
            }
        }

        /// <summary>
        /// 根据ID得到省份名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetProvNameById(int id)
        {
            if (id > 0)
            {
                EyouSoft.Model.SystemStructure.SysProvince p = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(id);
                if (p != null)
                {
                    return p.ProvinceName;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        
    }
}
