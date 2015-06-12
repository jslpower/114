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
using System.Text;
namespace WEB.OlServer
{
    /// <summary>
    /// 银桥网站-在线客服
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-23
    public partial class Default : System.Web.UI.Page
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
        private bool IsService = false;
        ///// <summary>
        ///// 最后获取到的消息记录编号
        ///// </summary>
        //private int LastMId = 0;
        private string _companyid=string.Empty;
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo _companyinfo = null;

        /// <summary>
        /// 公司Log
        /// </summary>
        protected string CompanyLogPath = Utils.NoLogoImage;

        #endregion Attributes

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCompanyId();

            //清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
            OlServerUtility.ClearUserOut();


            EyouSoft.Model.OnLineServer.OlServerUserInfo olInfo = null;
            EyouSoft.Model.OnLineServer.OlServerUserInfo olCookieInfo = OlServerUtility.GetOlCookieInfo(this.IsService);
            EyouSoft.Model.OnLineServer.OlServerConfig configInfo = null;
            EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
            UserInfo userInfo = null;
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out userInfo);
            if (!isLogin)
                olInfo = bll.GuestLogin(olCookieInfo, _companyid);
            else
                olInfo = bll.ServiceLogin(userInfo, this.IsService, _companyid);

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

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DEFAULTINFOS", string.Format(olserverinfo, userInfoJsonString, configInfoJsonString, messageInfoJsonString, DateTime.Now.ToString(),_companyinfo.CompanyName), true);

            #region 设置企业名称

            lbCompanyName1.Text = _companyinfo.CompanyName;
            lbCompanyName.Text = _companyinfo.CompanyName;
            ltrCompanyName.Text = _companyinfo.CompanyName;
            EyouSoft.Model.CompanyStructure.CompanyAttachInfo ImgModel = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(_companyid);
            if (ImgModel != null && ImgModel.CompanyLogo != null && !string.IsNullOrEmpty(ImgModel.CompanyLogo.ImagePath))
                CompanyLogPath = Domain.FileSystem + ImgModel.CompanyLogo.ImagePath;
            ImgModel = null;

            if (_companyinfo != null)
            {
                StringBuilder strCompanyInfo = new StringBuilder();
                if (!string.IsNullOrEmpty(_companyinfo.CompanyBrand))
                    strCompanyInfo.AppendFormat("<strong>品牌名称：</strong>{0}<br />",_companyinfo.CompanyBrand);
                if (_companyinfo.ContactInfo!=null && !string.IsNullOrEmpty(_companyinfo.ContactInfo.ContactName))
                    strCompanyInfo.AppendFormat("联系人：{0}<br />", _companyinfo.ContactInfo.ContactName);
                if (_companyinfo.ContactInfo != null && !string.IsNullOrEmpty(_companyinfo.ContactInfo.Mobile))
                    strCompanyInfo.AppendFormat("手机：{0}<br />", _companyinfo.ContactInfo.Mobile);
                if (_companyinfo.ContactInfo != null && !string.IsNullOrEmpty(_companyinfo.ContactInfo.Tel))
                    strCompanyInfo.AppendFormat("电话：{0}<br />", _companyinfo.ContactInfo.Tel);
                if (_companyinfo.ContactInfo != null && !string.IsNullOrEmpty(_companyinfo.ContactInfo.Fax))
                    strCompanyInfo.AppendFormat("传真：{0}<br />", _companyinfo.ContactInfo.Fax);
                if (!string.IsNullOrEmpty(_companyinfo.CompanyAddress))
                    strCompanyInfo.AppendFormat("地址：{0}", _companyinfo.CompanyAddress);
                if (strCompanyInfo.Length > 0)
                {
                    strCompanyInfo.Insert(0, "<li>");
                    strCompanyInfo.Append("</li>");
                }
                ltrCompanyInfo.Text = strCompanyInfo.ToString();
            }
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

        #region 设置公司编号
        /// <summary>
        /// 设置公司编号
        /// </summary>
        private void SetCompanyId()
        { 
            //初始化公司ID
            string currentUrlPath = HttpContext.Current.Request.ServerVariables["Http_Host"];

            //如果使用独立域名访问网店，则根据域名查询所属公司ID.
            if (Domain.SeniorOnlineShop.IndexOf(currentUrlPath) == -1)
            {
                EyouSoft.Model.SystemStructure.SysCompanyDomain cDomianModel = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().
                    GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, currentUrlPath);
                if (cDomianModel != null)
                {
                    this._companyid = cDomianModel.CompanyId;
                }
            }

            //如果没有使用独立域名访问网店，则从QueryString集合中查找cid参数，获取公司ID
            if (string.IsNullOrEmpty(this._companyid))
            {
                this._companyid = Utils.GetQueryStringValue("cid");
            }

            //获取高级网店信息和公司信息
            _companyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this._companyid);
            if (_companyinfo == null || _companyinfo.StateMore.IsDelete)
            {
                Utils.ShowError("高级网店不存在!", "SeniorShop");
                return;
            }

            //判断是否开通高级网店                
            if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) && !Utils.IsOpenHighShop(this._companyid))
            {
                Utils.ShowError("该公司未开通高级网店!", "SeniorShop");
                return;
            }
        }
        #endregion

    }
}
