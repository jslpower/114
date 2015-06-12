using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;

namespace UserPublicCenter.WebControl
{
    public partial class GongQiuPublish : System.Web.UI.UserControl
    {
        protected int pqtotalcount;
        protected FrontPage ppage = new FrontPage();
        protected Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int> typecountdic = new Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ppage = this.Page as FrontPage;
            Bind();
        }

        private void Bind()
        {
            pqtotalcount = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetExchangeListCount(true);

            typecountdic = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetExchangeTypeCount(true);
        }
    }
}