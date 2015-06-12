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
    public partial class QGroupActivation : EyouSoft.Common.Control.FrontPage
    {
        protected string activationid = string.Empty; 
        protected EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo QGroupMessageInfo = null;
        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> lastlist = null;
        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> otherlist = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitSelf();

            Page.Title = "激活您的QQ群供求信息_"+QGroupMessageInfo.Title + "_同业114供求信息频道";
            AddMetaTag("description", "杭州旅游供求频道,为您提供杭州地区最新旅游信息,旅游产品即时报价,特价旅游线路和旅行社收客信息查询,还可以发布团队询价,地接报价,组团拼团,旅游票务签证,车辆租赁等旅游供求信息.");
            AddMetaTag("keywords", "激活您的QQ群供求信息 " + QGroupMessageInfo.Title);
        }

        /// <summary>
        /// 初始化供求信息
        /// </summary>
        private void InitSelf()
        {
            activationid = EyouSoft.Common.Utils.GetQueryStringValue("activationid");
            QGroupMessageInfo = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().QG_GetQQGroupMessageInfo(activationid);

            if (QGroupMessageInfo == null)
            {
                Utils.ShowError("未找到您要查看的信息", "info");
                return;
            }

            if (QGroupMessageInfo.Status == EyouSoft.Model.CommunityStructure.QQGroupMessageStatus.已激活)
            {
                Response.Redirect(Domain.UserPublicCenter + "/info_" + activationid);
            }

            if (Utils.GetInt(Utils.GetQueryStringValue("a")) == 1)
            {
                Activation();
            }

            lastlist = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5, EyouSoft.Model.CommunityStructure.ExchangeType.团队询价, EyouSoft.Model.CommunityStructure.ExchangeTag.急急急, true);
            otherlist = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5, EyouSoft.Model.CommunityStructure.ExchangeType.团队询价, EyouSoft.Model.CommunityStructure.ExchangeTag.急急急, false);
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

        /// <summary>
        /// 激活
        /// </summary>
        private void Activation()
        {
            if (EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().QG_SetQQGroupMessageStatus(activationid
                , EyouSoft.Model.CommunityStructure.QQGroupMessageStatus.已激活))
            {
                //Response.Redirect(Domain.UserPublicCenter + "/info_" + activationid);
                string script = string.Format("alert(\"您已经激活了你的QQ群消息供求，点击确定查看供求信息！\");window.location.href=\"{0}\";",
                                              Domain.UserPublicCenter + "/info_" + activationid);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }
    }
}
