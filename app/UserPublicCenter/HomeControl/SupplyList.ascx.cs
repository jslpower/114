using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common.Control;
namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页供求信息
    /// </summary>
    public partial class SupplyList : System.Web.UI.UserControl
    {
        protected string strAllSupplyList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.GetSupplyList();
            }
        }

        /// <summary>
        /// 获的供求信息列表
        /// </summary>
        protected void GetSupplyList()
        {
            FrontPage page = this.Page as FrontPage;
            int CityId = page.CityId;
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> ExchangeList= EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(15,null,CityId,null);

            if (ExchangeList != null && ExchangeList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                strAdvList.Append("<div class=\"newslistgq\">");
                foreach (EyouSoft.Model.CommunityStructure.ExchangeList item in ExchangeList)
                {
                    strAdvList.AppendFormat("<li><span>【{0}】</span><a href='{1}' target=\"_blank\">{2}</a></li>", item.ExchangeTag,EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(item.ID,CityId), EyouSoft.Common.Utils.GetText(item.ExchangeTitle, 30));
                }
                strAdvList.Append("</div>");
                strAllSupplyList = strAdvList.ToString();
            }
            else
            {
                strAllSupplyList = "<div class=\"newslistgq\"><li></li><li></li><li></li><li></li><li>暂无供求信息</li><li></li><li></li><li></li><li></li></div>";
            }

        }
    }
}