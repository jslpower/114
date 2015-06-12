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
    /// 供求信息详细页
    /// </summary>
    /// 周文超 2010-08-03
    public partial class ExchangeInfo : EyouSoft.Common.Control.FrontPage
    {
        #region 页面级变量(私有)
        /// <summary>
        /// 供求ID
        /// </summary>
        protected string ExchangeId = string.Empty;
        /// <summary>
        /// 供求列表页
        /// </summary>
        private string ExchangeListPageUrl = string.Empty;
        /// <summary>
        /// 当前供求的类别(为null，使用前要实例化)
        /// </summary>
        private EyouSoft.Model.CommunityStructure.ExchangeType? CurrExchangeType = null;
        /// <summary>
        /// MQ链接
        /// </summary>
        //private string strMQUrl = "<a href='javascript:void(0);' onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&uid={0}');return false;\"><img src=\"" + Domain.ServerComponents + "/images/UserPublicCenter/MQWORD.gif\" width=\"49\" height=\"16\" /></a>";

        /// <summary>
        /// 省份实体(为null，使用前要实例化)
        /// </summary>
        private EyouSoft.Model.SystemStructure.SysProvince ProvinceModel = null;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //未登录
            //if (!IsLogin)
            //{
            //    EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserPublicCenter + Request.RawUrl);
            //    return;
            //}
            ExchangeListPageUrl = Utils.GeneratePublicCenterUrl(Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx", CityId);
            //未审核
            //if (!IsCompanyCheck)
            //{
            //    MessageBox.ShowAndRedirect(this, "抱歉：您所在的公司没有通过审核，暂时不能浏览供求信息！", Utils.GeneratePublicCenterUrl(Domain.UserPublicCenter + "/SupplierInfo/SupplierInfo.aspx", CityId));
            //    return;
            //}
            this.tabSeeExchange.Visible = false;
            ExchangeLeft1.CurrCityId = CityId;
            ExchangeLeft1.ImageServerPath = ImageServerPath;
            ExchangeLeft1.IsLogin = IsLogin;
            ibtnSave.ImageUrl = Domain.ServerComponents + "/images/UserPublicCenter/20090716tijiao.gif";
            if(IsLogin)
                ltrUserName.Text = SiteUserInfo.UserName;
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 1;

            ExchangeId = Utils.InputText(Request.QueryString["Id"]);

            if (string.IsNullOrEmpty(ExchangeId))
            {
                MessageBox.ShowAndRedirect(this, "未找到您要查看的信息！", ExchangeListPageUrl);
                return;
            }

            if (!IsPostBack)
            {
                InitExchange();
            }
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool GetIsLogin()
        {
            return IsLogin;
        }

        /// <summary>
        /// 获取连接地址
        /// </summary>
        public string GetReturnUrl()
        {
            string m = this.Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(this.Request.Url.ToString(), "_parent", "");
        }

        #region 初始化函数

        /// <summary>
        /// 写浏览记录
        /// </summary>
        private void SetHistory()
        {
            EyouSoft.Model.CommunityStructure.ExchangeVisited model = new EyouSoft.Model.CommunityStructure.ExchangeVisited();
            model.CompanyId = SiteUserInfo.CompanyID;
            model.ExchangeId = ExchangeId;
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
            if (Request.Cookies["ExchangeInfo" + ExchangeId] == null)
            {
                bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().SetReadCount(ExchangeId);
                if (Result)
                {
                    HttpCookie ExchangeInfoCookie = new HttpCookie("ExchangeInfo" + ExchangeId);
                    ExchangeInfoCookie.Expires = DateTime.Now.AddMinutes(5);
                    ExchangeInfoCookie.Path = "/SupplierInfo";
                    Response.Cookies.Add(ExchangeInfoCookie);
                }
            }
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitPageData()
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> ExchangeList = null;

            //最新供需
            ExchangeList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5, null, CityId, null);
            rptNewExchange.DataSource = ExchangeList;
            rptNewExchange.DataBind();

            //同类其他供需
            ExchangeList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5, CurrExchangeType, CityId, null);
            rptOtherExchange.DataSource = ExchangeList;
            rptOtherExchange.DataBind();

            if (ExchangeList != null) ExchangeList.Clear();
            ExchangeList = null;

            //查看过的供需信息
            if (IsLogin)
            {
                this.tabSeeExchange.Visible = true;
                IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> ExchangeListBase = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopVisitedList(5, SiteUserInfo.ID);
                rptSeeExchange.DataSource = ExchangeListBase;
                rptSeeExchange.DataBind();
                if (ExchangeListBase != null) ExchangeListBase.Clear();
                ExchangeListBase = null;
            }
        }

        /// <summary>
        /// 初始化供求信息
        /// </summary>
        private void InitExchange()
        {
            EyouSoft.Model.CommunityStructure.ExchangeList model = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetModel(ExchangeId);
            if (model == null)
            {
                MessageBox.ShowAndRedirect(this, "未找到您要查看的信息！", ExchangeListPageUrl);
                return;
            }
            if (model.IsCheck == false)
            {
                MessageBox.ShowAndRedirect(this, "您要查看的信息没有通过审核！", ExchangeListPageUrl);
                return;
            }

            //根据供求标题 初始化 页面标题  zxb update 20110118

            //(标题)_(类型)_(地区)旅游信息发布
            Page.Title = model.ExchangeTitle + "_" + model.ExchangeTag + "_" + CityModel.CityName + "旅游信息发布";
            //Page.Title = model.ExchangeTitle + "_同业114供求信息频道";

            CurrExchangeType = model.TopicClassID;
            ltrTitle.Text = model.ExchangeTitle;
            ltrTime.Text = model.IssueTime.ToString("yyyy年MM月dd日 hh:mm");
            ltrCompanyName.Text = model.CompanyName;
            ltrMQ.Text = Utils.GetMQ(model.OperatorMQ);
            if (model.WriteBackCount >= 0)
                ltrCommentCount.Text = string.Format("已有{0}评论", model.WriteBackCount);
            if (model.ViewCount >= 0)
            {
                if (string.IsNullOrEmpty(ltrCommentCount.Text))
                    ltrViewCount.Text = string.Format("{0}预览", model.ViewCount);
                else
                    ltrViewCount.Text = string.Format("/{0}预览", model.ViewCount);
            }
            rptExchangeImgList.DataSource = model.ExchangePhotoList;
            rptExchangeImgList.DataBind();
            ltrInfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ExchangeText);
            if (!string.IsNullOrEmpty(model.AttatchPath))
            {
                string strFileName = model.AttatchPath.Substring(model.AttatchPath.LastIndexOf("/") + 1, (model.AttatchPath.Length - model.AttatchPath.LastIndexOf("/") - 1));
                ltrDownLoad.Text = string.Format("<span class=\"lan14\"><strong>附 件:</strong></span><a target=\"_blank\" href=\"{0}{1}\">{3}<img src=\"{2}/images/UserPublicCenter/baocun.gif\" width=\"14\" height=\"13\" /><img src=\"{2}/images/UserPublicCenter/dakai.gif\" width=\"16\" height=\"14\" /></a>", Domain.FileSystem, model.AttatchPath, Domain.ServerComponents, strFileName);
            }

            SetClicks();
            if(IsLogin)
                SetHistory();
            InitPageData();
        }

        #region 前台函数

        /// <summary>
        /// 获取标签的Html
        /// </summary>
        /// <param name="ExchangeTagHtml">标签类</param>
        /// <returns></returns>
        protected string GetTagHtml(EyouSoft.Model.CommunityStructure.Tag ExchangeTagHtml)
        {
            if (ExchangeTagHtml == null)
                return "";

            return ExchangeTagHtml.TagHTML;
        }

        /// <summary>
        /// 根据省份ID获取省份名称
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns></returns>
        protected string GetProvinceNameById(string ProvinceId)
        {
            if (string.IsNullOrEmpty(ProvinceId) || StringValidate.IsInteger(ProvinceId.Trim()) == false || Convert.ToInt32(ProvinceId.Trim()) <= 0)
                return string.Empty;

            ProvinceModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(int.Parse(ProvinceId));
            if (ProvinceModel != null)
            {
                return string.Format("【{0}】", ProvinceModel.ProvinceName);
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取MQ链接
        /// </summary>
        /// <param name="OperatorMQ">MQ编号</param>
        /// <returns></returns>
        protected string GetMQUrl(string OperatorMQ)
        {
            if (string.IsNullOrEmpty(OperatorMQ) || StringValidate.IsInteger(OperatorMQ.Trim()) == false || Convert.ToInt32(OperatorMQ.Trim()) <= 0)
                return string.Empty;

            return Utils.GetMQ(OperatorMQ);
        }

        #endregion

        /// <summary>
        /// 提交回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibtnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsLogin)
            {
                MessageBox.ShowAndReturnBack(this, "请先登录！", 1);
                return;
            }

            string strInfo = Utils.InputText(txtCommentInfo.Value.Trim());
            string strTopicId = ExchangeId;
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

        #endregion
    }
}
