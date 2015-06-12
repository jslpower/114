using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo.Ashx
{
    /// <summary>
    /// 供求信息收藏
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FavorExchange : IHttpHandler
    {
        private string ExchangeId = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            EyouSoft.SSOComponent.Entity.UserInfo SiteUserInfo = null;
            bool IsLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out SiteUserInfo);
            if (IsLogin && SiteUserInfo != null)
            {
                ExchangeId = Utils.InputText(context.Request.QueryString["Id"]);
                if (!string.IsNullOrEmpty(ExchangeId))
                {
                    EyouSoft.Model.CommunityStructure.ExchangeBase model = new EyouSoft.Model.CommunityStructure.ExchangeBase();
                    model.CompanyId = SiteUserInfo.CompanyID;
                    model.ExchangeId = ExchangeId;
                    model.OperatorId = SiteUserInfo.ID;
                    bool Result = EyouSoft.BLL.CommunityStructure.ExchangeFavor.CreateInstance().Add(model);
                    model = null;
                    if (Result)
                    {
                        context.Response.Write("1");
                        context.Response.End();
                    }
                    else
                    {
                        context.Response.Write("2");
                        context.Response.End();
                    }
                }
            }
            else
            {
                context.Response.Write("0");
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
