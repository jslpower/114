using System;
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
using System.IO;
namespace WEB
{
    public partial class TestByLugy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               IList<EyouSoft.Model.ShopStructure.HighShopAdv> list=EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5,"f6fc66b6-3381-4cc7-8508-81c85dea4cb4");
               EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().Update("f6fc66b6-3381-4cc7-8508-81c85dea4cb4", list);
            }
        }
        public void BindRp()
        {
            EyouSoft.IBLL.SystemStructure.ISysFriendLink Itest = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance();
            rp1.DataSource = Itest.GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.未知);
            rp1.DataBind();
        }
    }
}
