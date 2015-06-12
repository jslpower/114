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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.BLL.ScenicStructure;
using EyouSoft.Common;
using System.Text;

namespace SeniorOnlineShop.scenicspots.T1
{
    public partial class ScenicTickets : EyouSoft.Common.Control.FrontPage
    {
        #region 变量
        public long Id = 0;
        #region 地图变量
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GoogleMapKey { get; set; }
        public string ShowTitleText { get; set; }
        #endregion
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //导航样式制定
            ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CTAB = SeniorOnlineShop.master.SPOTT1TAB.景区门票;
            if (!IsPostBack)
            {
                long.TryParse(Utils.GetQueryStringValue("scenic"), out Id);
                Databind();
            }
        }

        /// <summary>
        /// 获取景区详细记录
        /// </summary>
        protected void Databind()
        {
            MScenicArea item = BScenicArea.CreateInstance().GetModel(Id);
            if (item != null)
            {
                BScenicArea.CreateInstance().UpdateClickNum(Id);

                lblScenicName.InnerText = Utils.GetText2(item.ScenicName, 30, true);
                lblScenicLevel.InnerText = item.ScenicLevel.ToString().Equals("0") ? string.Empty : item.ScenicLevel.ToString();
                lblTelephone.InnerText = Utils.GetText2(item.Telephone, 13, false);
                lblOpenTime.InnerText = item.OpenTime;
                //门票
                StringBuilder tickets = new StringBuilder();
                tickets.Append("<ul class=\"lianxi_di1\"><li>票型</li><li>票价时限</li><li>门市价</li></ul>");
                if (item.TicketsList != null && item.TicketsList.Count > 0)
                {
                    foreach (MScenicTickets obj in item.TicketsList)
                    {
                        tickets.Append("<ul class=\"lianxi_di2\">");
                        tickets.AppendFormat("<li>{0}</li>", obj.TypeName);
                        tickets.AppendFormat("<li>{0}</li>", DateTimeStr(obj.StartTime, obj.EndTime));
                        tickets.AppendFormat("<li>{0}</li></ul>", obj.RetailPrice.ToString("C0"));
                    }
                }
                else
                {
                    tickets.Append("<ul class=\"lianxi_di2\"><li></li><li>该景区未设置门票信息</li><li></li></ul>");
                }
                ltlScenicTickets.Text = tickets.ToString();
                lblDescription.InnerHtml = item.Traffic;//交通说明
                lblFacilities.InnerHtml = item.Facilities; 

                lblDiqu.InnerHtml = item.ProvinceName + "&nbsp;&nbsp;" + item.CityName + "&nbsp;&nbsp;" + item.CountyName;
                lblContact.InnerHtml = item.ContactName + "&nbsp;TEL:" + item.ContactTel + "&nbsp;Mobile:" + item.ContactMobile;
                lblAddress.InnerText = item.CnAddress;
                lblDescriptionDetail.InnerHtml = item.Description;
                StringBuilder themeStr = new StringBuilder();
                if (item.ThemeId != null && item.ThemeId.Count > 0)
                {
                    foreach (MScenicTheme theme in item.ThemeId)
                    {
                        themeStr.Append(theme.ThemeName + " ");
                    }
                    lblTheme.InnerHtml = Utils.GetText2(themeStr.ToString(), 22, false);

                }
                else
                {
                    lblTheme.InnerText = "无";
                }
                //google map
                ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).IsEnableLeftGoogleMap = false;
                double x;
                double y;
                bool isGn = double.TryParse(item.X, out x);
                bool isTn = double.TryParse(item.Y, out y);
                InitGoogleMap(x, y, item.ScenicName);

                //美景美图
                ScenicImg(Id);

                #region 设置标题

                //设置Title.....
                this.Title = string.Format("{0}", item.ScenicName);
                if (!string.IsNullOrEmpty(item.Description))
                    AddMetaTag("description", string.Format("{0}", Utils.GetText2(Utils.TextToHtml(item.Description), 100, false)));
                else
                    AddMetaTag("description", "");
                AddMetaTag("keywords", string.Format("{0}-{1}-{2}-{3}-{4}", item.ScenicName, item.ScenicName + "门票", item.ScenicName + "门票价格", item.ScenicName + "特价门票", item.ScenicName + "门票预定", item.ScenicName + "介绍"));

                #endregion
            }

        }
        /// <summary>
        /// 初始化Google Map
        /// </summary>
        private void InitGoogleMap(double x, double y, string scenicName)
        {
            Latitude = y;
            Longitude = x;
            ShowTitleText = scenicName;
            if (base.Master != null)
                GoogleMapKey = ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).GoogleMapKey;
        }
        /// <summary>
        /// 景区美图美景
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        protected void ScenicImg(long Id)
        {
            IList<MScenicImg> list = BScenicImg.CreateInstance().GetList(Id);
            if (list != null && list.Count > 0)
            {
                rpt_ScenicImg.DataSource = list;
                rpt_ScenicImg.DataBind();
                //景区形象图片
                var img = from c in list where c.ImgType == ScenicImgType.景区形象 select c;
                if (img != null && img.Count() > 0)
                {
                    imgScenicImg.Src = Utils.GetNewImgUrl(img.ToList()[0].ThumbAddress, 3);
                    imgScenicImg.Alt = img.ToList()[0].ScenicName;
                    hrefImg.HRef = Utils.GetNewImgUrl(img.ToList()[0].Address, 3);
                    hrefImg.Title = img.ToList()[0].ScenicName;
                }

            }
            else
            {
                imgScenicImg.Src = Utils.GetNewImgUrl(string.Empty, 3);
            }
        }

        protected string DateTimeStr(DateTime? start, DateTime? end)
        {
            string str = string.Empty;

            if (start == null || end == null)
            {
                str = "长期有效";
            }
            else
            {
                if (start == DateTime.MinValue || end == DateTime.MinValue)
                {
                    str = "长期有效";
                }
                else
                {
                    str = Convert.ToDateTime(start).ToString("yyyy-MM-dd") + "至" + Convert.ToDateTime(end).ToString("yyyy-MM-dd");
                }
            }

            return str;
        }
    }
}
