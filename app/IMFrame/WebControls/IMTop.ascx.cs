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
using System.ComponentModel;
using EyouSoft.ControlCommon.Control;
using EyouSoft.Common;

namespace IMFrame.WebControls
{
    ///MQ修改 luofx 2010-8-16
    public partial class IMTop : System.Web.UI.UserControl
    {
        /// <summary>
        /// 图片路径地址
        /// </summary>
        protected string ImageServerUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 当前MQ号
        /// </summary>
        public int MQLoginId
        {
            get;
            set;
        }
        /// <summary>
        /// 当前MQ用户MD5密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }        
        private bool _PageType;
        /// <summary>
        /// 页面类型，专线页面设置为True,组团页面设置为false
        /// </summary>
        [Bindable(true)]
        public bool PageType
        {
            set {
                _PageType = value;
                if (_PageType)
                {
                    a_zt.Visible = true;
                    a_zx.Visible = false;
                }
                else
                {
                    a_zt.Visible = false;
                    a_zx.Visible = true;
                }
            }
        }
        protected string DefaultUrl = Domain.UserBackCenter + "/Default.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MQPage mqPage = this.Page as MQPage;
                ImageServerUrl = mqPage.ImageServerUrl;
                if (mqPage.SiteUserInfo != null)
                {
                    if (!mqPage.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                    {
                        a_zx.Visible = false;
                    }
                    if (!mqPage.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团)) {
                        a_zt.Visible = false;
                    }
                }
                DefaultUrl = EyouSoft.Common.Utils.GetDesPlatformUrlForMQMsg(DefaultUrl, MQLoginId.ToString(), Password);
                mqPage = null;
            }
        }
    }
}