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
using EyouSoft.BLL.ScenicStructure;
using EyouSoft.Model.ScenicStructure;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.ScenicManage
{

    /// <summary>
    /// 景区特价门票用户控件
    /// 功能：显示景区特价门票
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-13  
    /// </summary>  
    public partial class TicketsControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 省份
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int? CountyId { get; set; }
        /// <summary>
        /// 获取条数
        /// </summary>
        public int TopNum { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DiscountTickets();
            }
        }

        /// <summary>
        /// 特价景区门票
        /// </summary>
        protected void DiscountTickets()
        {
            MSearchScenicTicketsSale search = new MSearchScenicTicketsSale()
            {
                ProvinceId = ProvinceId,
                CityId = CityId,
                CountyId = CountyId,
                B2B = ScenicB2BDisplay.侧边推荐
            };
            IList<MScenicTicketsSale> list = BScenicTickets.CreateInstance().GetList(TopNum, search);
             
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append("<div class=\"" + (i + 1 == 1 ? "show1" : "hidden1") + "\" id=\"bb" + (i + 1) + "\" onmouseover=\"pucker_show1('bb'," + (i + 1) + ",'hidden1','show1'," + list.Count + ")\"><div class=\"left\">");
                    sb.Append("<a href=\"/jingquinfo_" + list[i].Id + "\" title=\"" + list[i].TicketsName + "\"><img src=\"" + Utils.GetNewImgUrl(list[i].Address, 3) + "\" alt=\"" + list[i].TicketsName + "\" width=\"80\" height=\"70\" border=\"0\" style=\"border:1px solid #ccc; padding:1px;\"/></a></div>");
                    sb.Append("<div class=\"right_xin\">");
                    sb.Append("<div class=\"mingc1\"><a href=\"/jingquinfo_" + list[i].Id + "\"><strong>" + Utils.GetText2(list[i].TicketsName, 7, false) + "</strong></a></div>");
                    sb.Append("<div class=\"mingc\">门市价：<span class=\"hong\"><strong>" + list[i].RetailPrice.ToString("F0") + "元</strong></span></div>");
                    sb.Append("</div></div>");
                    //sb.Append("<div class=\"jiage\">门市价：<span class=\"huihua\"><strong>" + list[i].RetailPrice.ToString("F0") + "元</strong></span></div></div></div>");
                }
            }
            this.lclTjmp.Text = sb.ToString();
        }

    }
}