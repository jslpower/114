using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;

namespace TourUnion.WEB.IM.Search
{
    /// <summary>
    /// 功能:mq在线人员
    /// </summary>
    /// 编写人:zhangzy  时间:2009-9-10
    public partial class OnLine : System.Web.UI.Page
    {
        private int CurrencyPage = 1;   //当前页
        private int intPageSize = 16;   //每页显示的记录数
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            int intRecordCount = 0;
            int intPageCount = 0;
            string sqlWhere = "im_status>11";

            string CompanyName = Server.UrlDecode(Request.QueryString["CompanyName"]);
            string ContactName = Server.UrlDecode(Request.QueryString["ContactName"]);
            string CityName = Server.UrlDecode(Request.QueryString["CityName"]);
            if (!string.IsNullOrEmpty(CompanyName))
            {
                sqlWhere += string.Format(" and CompanyName like '%{0}%'", CompanyName.Trim());
                this.txtCompanyName.Value = CompanyName.Trim();
            }
            if (!string.IsNullOrEmpty(ContactName))
            {
                sqlWhere += " and ContactName like '%" + ContactName + "%'";
                this.txtContactName.Value = ContactName.Trim();
            }
            if (!string.IsNullOrEmpty(CityName))
            {
                sqlWhere += " and CityName like '%" + CityName + "%'";
                this.txtCityName.Value = CityName.Trim();
            }

            int TypeId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["TypeId"]);
            ddlCompanyType.SelectedValue = TypeId.ToString();
            if (TypeId != 0)
            {
                sqlWhere += " and TypeId = " + TypeId;
            }

            TourUnion.BLL.TourUnion_AreaControl bll = new TourUnion.BLL.TourUnion_AreaControl();
            Adpost.Common.ExporPage.PageControl pagecontrol = new Adpost.Common.ExporPage.PageControl(intPageSize);
            DataTable dt = bll.GetAreaList("dt_SearchFriend", intPageSize, pagecontrol.CurrentPage, sqlWhere, "IsOnline DESC,TypeId,userid desc", out intRecordCount, out intPageCount);
            CurrencyPage = pagecontrol.CurrentPage;
            pagecontrol.SetPage(this.ExportPageInfo1, intRecordCount);
            pagecontrol = null;

            this.ExportPageInfo1.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.OldNoEveryPageStyle;
            this.ExportPageInfo1.IsInitJs = false;
            this.ExportPageInfo1.IsInitBaseCssStyle = false;

            if (intRecordCount > 0)
            {
                this.NoData.Visible = false;

                //绑定数据源
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();
            }
            else
            {
                this.NoData.Visible = true;
            }
        }

        #region 获得每一行的用户信息
        /// <summary>
        /// 获得每一行的用户信息,与OnLine.aspx页面中公用
        /// </summary>
        public static string GetListTrValue(DataRowView drv, List<int> myFriendIdList)

        {
            StringBuilder strVal = new StringBuilder();

            if (drv != null)
            {
                string im_uid = drv["im_uid"].ToString();
                string contactName = drv["ContactName"].ToString().Trim();
                string im_displayname = drv["im_displayname"].ToString().Trim();
                string userName = drv["im_username"].ToString().Trim();
                string im_status = drv["im_status"].ToString();
                string CompanyName = drv["CompanyName"].ToString().Trim();
                string CityName = drv["CityName"].ToString().Trim();
                string CompanyId = drv["CompanyId"].ToString().Trim();
                int CompanyTypeId = Adpost.Common.Function.ValidatorValueManage.GetIntValue(HttpContext.Current.Request.QueryString["CompanyTypeId"], 0);
                //显示的用户名称
                string showName = contactName;
                if (contactName == "")
                {
                    if (im_displayname != "")
                        showName = im_displayname;
                    else
                        showName = userName;
                }
                //用户名,联系人,昵称
                string userNameContactName = string.Format("用户名:{0},联系人:{1},昵称:{2}", userName, contactName, im_displayname);

                //获得用户的线路区域  用户id字段为UserId  每个线路区域使用空格分割
                //string strArea = drv["AreaName"].ToString();

                string icoSrc = "/IM/images/icounline.gif";   //头像src地址,默认不在线的

                if (Int32.Parse(im_status) > 11)   //表示用户不在线  >11在线  否则不在线
                {
                    icoSrc = "/IM/images/icoonline.gif";
                }

                #region 初始化tr

                strVal.Append("<tr>");
                strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><img src='{0}' title='{2}' width='14' height='17' /><span title='{2}'><a target=\"_blank\">{1}</a></span></td>", icoSrc, TourUnion.WEB.ProceFlow.StringManage.Substring(showName, 16), userNameContactName);

                //公司名称
                strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><span title='{1}'><a target=\"_blank\">{0}</a></span></td>", TourUnion.WEB.ProceFlow.StringManage.Substring(CompanyName, 34), CompanyName);

                //线路区域
                //if (strArea != "")
                //    strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><span title='{1}'>{0}</span></td>", TourUnion.WEB.ProceFlow.StringManage.Substring(strArea, 11), strArea);
                //else
                //    strVal.Append("<td align='left' bgcolor='#fbfbfb'>暂无线路区域</td>");

                //城市
                strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><span title='{1}'>{0}</span></td>", TourUnion.WEB.ProceFlow.StringManage.Substring(CityName, 6), CityName);

                strVal.AppendFormat("<td align='left' bgcolor='#fbfbfb'><img style='cursor:pointer' src='/IM/images/addFriend.gif' /></td>");

                strVal.Append("</tr>");
                #endregion
            }

            return strVal.ToString();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string TypeId = Request.Form["ddlCompanyType"];
            string CompanyName = this.txtCompanyName.Value;
            string ContactName = this.txtContactName.Value;
            string CityName = this.txtCityName.Value;
            string strPars = "?" + WEB.ProceFlow.UrlManage.BuildUrlQueryString("CompanyName", "ContactName", "CityName", "TypeId", "Page");

            if (!string.IsNullOrEmpty(CompanyName))
            {
                CompanyName = Server.UrlEncode(CompanyName.Trim());
                strPars += "&CompanyName=" + CompanyName;
            }
            if (!string.IsNullOrEmpty(ContactName))
            {
                ContactName = Server.UrlEncode(ContactName.Trim());
                strPars += "&ContactName=" + ContactName;

            }
            if (!string.IsNullOrEmpty(CityName))
            {
                CityName = Server.UrlEncode(CityName.Trim());
                strPars += "&CityName=" + CityName;

            }
            if (TypeId != "0")
            {
                strPars += "&TypeId=" + TypeId;
            }

            Response.Redirect(Request.ServerVariables["SCRIPT_NAME"] + strPars);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal LiteralTr = (Literal)e.Item.FindControl("LiteralTr");
                LiteralTr.Text = GetListTrValue((DataRowView)e.Item.DataItem, null);
            }
        }
    }
}
