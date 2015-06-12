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

namespace IMFrame
{
    /// <summary>
    /// MQ跳转到大平台页面中转页面
    /// 创建人：张新兵，创建时间：2010-08-14
    /// </summary>
    public partial class MQTransit : System.Web.UI.Page
    {
        protected string UserName;
        protected string Pwd;
        protected string DesUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            DesUrl = HttpUtility.UrlDecode(Utils.GetQueryStringValue(Utils.MQTransitDesUrlKey));
            int mqLoginId = Utils.GetInt(Request.QueryString[Utils.MQLoginIdKey]);
            Pwd = Utils.GetQueryStringValue(Utils.MQPwKey);

            EyouSoft.Model.CompanyStructure.CompanyUser companyUser =  EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(mqLoginId);

            if (companyUser == null)
            {
                Response.Clear();
                Response.Write("无效的用户信息");
                Response.End();
            }

            UserName = companyUser.UserName;

        }
    }
}
