using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using EyouSoft.Common;
using Newtonsoft.Json;
using EyouSoft.SSOComponent.Entity;

namespace WEB.OlServer
{
    /// <summary>
    /// 银桥网站-在线客服
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-23
    public partial class Default : EyouSoft.Common.Control.BackPage
    {
        #region Attributes
        /// <summary>
        /// 客户cookie名称
        /// </summary>
        private const string OLSGUESTCOOKIENAME = "OlServer/OLSGC/";
        /// <summary>
        /// 客服cookie名称
        /// </summary>
        private const string OLSSERVICECOOKIENAME = "OlServer/OLSSC/";
        /// <summary>
        /// 默认客服
        /// </summary>
        private bool IsService = true;
        ///// <summary>
        ///// 最后获取到的消息记录编号
        ///// </summary>
        //private int LastMId = 0;

        /// <summary>
        /// 公司Log图片
        /// </summary>
        protected string CompanyLogPath = Utils.NoLogoImage;

        #endregion Attributes

        protected void Page_Load(object sender, EventArgs e)
        {
            //清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
            OlServerUtility.ClearUserOut();


            EyouSoft.Model.OnLineServer.OlServerUserInfo olInfo = null;
            EyouSoft.Model.OnLineServer.OlServerUserInfo olCookieInfo = OlServerUtility.GetOlCookieInfo(this.IsService);
            EyouSoft.Model.OnLineServer.OlServerConfig configInfo = null;
            EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
            olInfo = bll.ServiceLogin(base.SiteUserInfo, true, base.SiteUserInfo.CompanyID);

            bll = null;

            //获取在线客服配置信息
            configInfo = OlServerUtility.GetOlServerConfig();

            string olserverinfo = "var olserverinfos={{uInfo:{0},configInfo:{1},mInfo:{2},lastTime:\"{3}\",CompanyName:\"{4}\"}};";

            string userInfoJsonString = JsonConvert.SerializeObject(olInfo);
            string configInfoJsonString = JsonConvert.SerializeObject(configInfo);
            EyouSoft.Model.OnLineServer.OlServerMessageInfo MessageInfo= new EyouSoft.Model.OnLineServer.OlServerMessageInfo();
            MessageInfo.AcceptId = string.Empty;
            MessageInfo.AcceptName = string.Empty;
            MessageInfo.Message = string.Empty;
            MessageInfo.MessageId = string.Empty;
            MessageInfo.SendId = string.Empty;
            MessageInfo.SendName = string.Empty;
            MessageInfo.SendTime = DateTime.Now;
            string messageInfoJsonString = JsonConvert.SerializeObject(MessageInfo);

            //设置cookie
            this.SetOlCookieInfo(this.IsService, userInfoJsonString);

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DEFAULTINFOS", string.Format(olserverinfo, userInfoJsonString, configInfoJsonString, messageInfoJsonString, DateTime.Now.ToString(),SiteUserInfo.CompanyName), true);

            #region 设置企业名称

            lbCompanyName1.Text = SiteUserInfo.CompanyName;
            lbCompanyName.Text = SiteUserInfo.CompanyName;

            EyouSoft.Model.CompanyStructure.CompanyAttachInfo ImgModel = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (ImgModel != null && ImgModel.CompanyLogo != null && !string.IsNullOrEmpty(ImgModel.CompanyLogo.ImagePath))
                CompanyLogPath = Domain.FileSystem + ImgModel.CompanyLogo.ImagePath;

            #endregion
        }

        /// <summary>
        /// 设置用户cookie信息
        /// </summary>
        /// <param name="isService">是否客服</param>
        /// <param name="cookieJsonString">cookie信息</param>
        private void SetOlCookieInfo(bool isService,string cookieJsonString)
        {
            string cookieName = isService ? OLSSERVICECOOKIENAME : OLSGUESTCOOKIENAME;

            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = Server.UrlEncode(cookieJsonString);

            Response.Cookies.Add(cookie);
        }

    }
}
