using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Function;
namespace IMFrame.LocalAgency
{
    public partial class AjaxDeleteRoute : System.Web.UI.Page
    {
        /// <summary>
        /// 页面功能:ajax调用删除线路
        /// 修改时间:2010-08-14 修改人:袁惠
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["RoleDelete"].ToLower() == "true")
            {
                bool result = false;
                string ids = Utils.GetString(HttpContext.Current.Request.QueryString["RouteIdList"], "");
                if (ids.Length > 0)
                {
                    string[] idlist = StringValidate.Split(ids, ",");
                    if (idlist.Length > 0)
                    {
                        for (int i = 0; i < idlist.Length; i++)
                        {
                            result = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().DeleteSiteRoute(idlist[i]);
                        }
                    }

                }
                if (result)
                {
                    HttpContext.Current.Response.Write("1");
                }
                else
                {
                    HttpContext.Current.Response.Write("0");
                }
            }
            else
            {
                HttpContext.Current.Response.Write("2");
            }
            HttpContext.Current.Response.End();
        }
    }
}
