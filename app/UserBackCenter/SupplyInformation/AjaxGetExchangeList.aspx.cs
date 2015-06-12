using System;
using System.Collections;
using System.Collections.Generic;
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

namespace UserBackCenter.SupplyInformation
{
    public partial class AjaxGetExchangeList : System.Web.UI.Page
    {
        protected string strDomain = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                strDomain = Domain.UserPublicCenter;
                InitExchangeList();
            }
        }

        private void InitExchangeList()
        {
            string ExchangeType = Utils.InputText(Request.QueryString["ExchangeType"]);
            EyouSoft.Model.CommunityStructure.ExchangeType? type = null;
            if (ExchangeType != "0")
            {
                type = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), ExchangeType);
            }
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(10, type, 0, null);
            if (list != null && list.Count > 0)
            {
                this.dlExchangeList.DataSource = list;
                this.dlExchangeList.DataBind();
            }
            list = null;
        }
    }
}
