using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.AirTickets
{
    /// <summary>
    /// 机票新首页
    /// 功能：显示机票信息
    /// 创建人：戴银柱
    /// 创建时间： 2011-07-22 
    /// </summary>
    public partial class TicketNew : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 供求信息
        /// </summary>
        protected string supplysHtml;
        /// <summary>
        /// 机票信息
        /// </summary>
        protected string ticketsHtml;
        /// <summary>
        /// 图片数量
        /// </summary>
        protected string FiveImagesNumber;
        /// <summary>
        /// 图片地址
        /// </summary>
        protected string FiveAdvImages;
        //mq图标
        protected string strMQhtml = string.Empty;
        //qq
        protected string strQQhtml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = PageTitle.Plane_Title;
            if (!IsPostBack)
            {
                strMQhtml = EyouSoft.Common.Utils.GetMQ("27440");
                strQQhtml = "<a href=\"javascript:\">" + EyouSoft.Common.Utils.GetQQ("1305218445") + "</a> <a href=\"javascript:\">" + EyouSoft.Common.Utils.GetQQ("34737111") + "</a><br />";

                (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 3;

                BindSupply();

                TicketInit();

                InitAdv();


            }
        }

        /// <summary>
        /// 五张轮换广告
        /// </summary>
        protected void InitAdv()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.机票频道焦点广告翻屏);
            int Count = 1;
            if (AdvList != null && AdvList.Count > 0)
            {

                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    FiveImagesNumber += string.Format("<li>{0}</li>", Count);
                    Count++;
                    FiveAdvImages += string.Format("<li><a href=\"{1}\" {2}><img src=\"{0}\" height=\"219\" width=\"640\" /></a></li>", EyouSoft.Common.Domain.FileSystem + item.ImgPath, item.RedirectURL, item.RedirectURL == Utils.EmptyLinkCode ? "" : "target=\"_blank\"");
                }
            }
            AdvList = null;
        }

        #region 绑定供求列表
        /// <summary>
        /// 绑定供求列表
        /// </summary>
        protected void BindSupply()
        {
            StringBuilder strBuilder = new StringBuilder();
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> excList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(20, null, -1, true);
            if (excList != null && excList.Count > 0)
            {
                for (int i = 0; i < excList.Count; i++)
                {
                    if (i <= 6)
                    {
                        if (excList[i].ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
                        {
                            strBuilder.AppendFormat("<li class=\"gong\"><s></s><a href=\"{0}\">{1}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(excList[i].ID, CityId), Utils.GetText(excList[i].ExchangeTitle, 13));
                        }
                        else
                        {
                            strBuilder.AppendFormat(" <li class=\"qiu\"><a href=\"{0}\"><s></s>{1}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(excList[i].ID, CityId), Utils.GetText(excList[i].ExchangeTitle, 13));
                        }
                    }
                }
            }
            supplysHtml = strBuilder.ToString();
        }
        #endregion


        #region 绑定特价机票
        protected void TicketInit()
        {
            IList<EyouSoft.Model.TicketStructure.SpecialFares> ticketList = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().GetTopSpecialFares(30);
            StringBuilder strBuilder = new StringBuilder();
            if (ticketList != null && ticketList.Count > 0)
            {
                for (int i = 0; i < ticketList.Count; i++)
                {
                    if (i <= 13)
                    {
                        strBuilder.AppendFormat("<li><a href=\"{0}\" title=\"{4}\" target=\"_blank\"><font color=\"#074387\">[{2}]</font> {1}</a><em>[{3}]</em></li>",
                            ticketList[i].IsJump ? (IsLogin ? Domain.UserPublicCenter + "/PlaneInfo/PlaneListPage.aspx" : Domain.UserPublicCenter + "/AirTickets/Login.aspx?return=PlaneListPage") : EyouSoft.Common.URLREWRITE.Plane.SpecialFaresUrl(ticketList[i].ID, CityId),
                            EyouSoft.Common.Utils.GetText(ticketList[i].Title, 10, false),
                            ticketList[i].SpecialFaresType,
                            ticketList[i].AddTime.ToString("MM-dd"), ticketList[i].Title);
                    }
                }
            }
            ticketsHtml = strBuilder.ToString();
        }
        #endregion
    }
}
