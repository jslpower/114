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
using AlipayClass;
using tenpay;

namespace UserPublicCenter.QianYue
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AlipayClass.QianYue qianyue = new AlipayClass.QianYue(
                AlipayParameters.Partner, 
                AlipayParameters.SignType, 
                AlipayParameters.Key, 
                AlipayParameters.Input_Charset);

            HyperLink1.NavigateUrl = qianyue.Create_url();

            TrustRefund trust = new TrustRefund(TenpayParameters.Bargainor_ID);
            HyperLink2.NavigateUrl = trust.CreateUrl();
        }
    }
}
