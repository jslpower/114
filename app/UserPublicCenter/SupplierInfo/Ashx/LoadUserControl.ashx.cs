using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using EyouSoft.Common;
using System.IO;
namespace UserPublicCenter.SupplierInfo.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LoadUserControl : PageProxy, IHttpHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            string ControlUrl = string.Empty;
            if (context.Request.QueryString["controlurl"] != null && context.Request.QueryString["controlurl"].ToString().Trim().Length > 0)
            {
                ControlUrl =Utils.InputText(context.Request.QueryString["controlurl"].ToString());
            }
            string StrHtml = string.Empty;
            StrHtml = PageProxy.RenderUserControl(ControlUrl);
            context.Response.Write(StrHtml);
            return;
        }

        public new bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
