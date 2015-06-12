using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店特价用户控件
    /// 功能：显示酒店特价
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-13  
    /// </summary> 
    public partial class DiscountControl : System.Web.UI.UserControl
    {
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DiscountTickets();
            }
        }

        #region 酒店频道特价房
        protected void DiscountTickets()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> TicketsList = this.GetList(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.酒店频道特价房展示);
            if (TicketsList != null && TicketsList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();                
                for (int i = 0; i < TicketsList.Count; i++)
                {
                    string target = "";
                    if (TicketsList[i].RedirectURL == Utils.EmptyLinkCode)
                    {
                        target = "target=_self";
                    }
                    else
                    {
                        target = "target=_blank";
                    }
                    if (i == 0)
                    {
                        sb.Append("<div class=\"show1\" id=\"box1" + (i + 1) + "\" onmouseover=\"pucker_show('box1'," + (i + 1) + ",'hidden1','show1')\"  > ");
                    }
                    else
                    {
                        sb.Append("<div class=\"hidden1\" id=\"box1" + (i + 1) + "\" onmouseover=\"pucker_show('box1'," + (i + 1) + ",'hidden1','show1')\">");
                    }
                    sb.Append("<div class=\"left\"><a " + target + "  href=\"" + TicketsList[i].RedirectURL + "\">");
                    sb.Append("<img src=" + Utils.GetNewImgUrl(TicketsList[i].ImgPath, 3) + " width='75px' height='65px' style='border: 1px solid #ccc;' />");
                    sb.Append("</a></div>");
                    sb.Append("<div class=\"right\" style='margin-left:5px;'>");

                    sb.Append("<div class=\"mingc\" style=\"float:none; width:100%; vertical-align:middle;\"><a " + target + " title='" + TicketsList[i].Title + "' href='" + TicketsList[i].RedirectURL + "'><strong>" + Utils.GetText(TicketsList[i].Title, 7, true) + "</a></strong></div>"); 
                    
                    sb.Append("</div></div>");
                }
                this.lclTjf.Text = sb.ToString();
            }
            TicketsList = null;
        }
        #endregion

        #region 酒店集合方法
        /// <summary>
        /// 景区集合方法
        /// </summary>
        protected IList<EyouSoft.Model.AdvStructure.AdvInfo> GetList(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> List = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (List.Count > 0)
            {
                return List;
            }
            List = null;
            return null;
        }
        #endregion

    }
}