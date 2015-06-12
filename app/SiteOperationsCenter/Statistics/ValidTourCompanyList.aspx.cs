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

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 有有效产品批发商列表
    /// 罗丽娥    2010-09-19
    /// </summary>
    public partial class ValidTourCompanyList : EyouSoft.Common.Control.YunYingPage
    {
        protected bool Invalid = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Invalid = bool.Parse(Utils.GetQueryStringValue("Invalid"));

            if (!Page.IsPostBack)
            {
                if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_管理该栏目))
                {
                    Utils.ResponseNoPermit("对不起，您没有统计分析_管理该栏目权限!");
                    return;
                }
                else {
                    if (!Invalid)
                    {
                        if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_无有效产品专线商))
                        {
                            Utils.ResponseNoPermit("对不起，您没有统计分析_无有效产品专线商权限!");
                            return;
                        }
                    }
                    else {
                        if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.统计分析_有有效产品专线商))
                        {
                            Utils.ResponseNoPermit("对不起，您没有统计分析_有有效产品专线商权限!");
                            return;
                        }
                    }
                }
            }
        }
    }
}
