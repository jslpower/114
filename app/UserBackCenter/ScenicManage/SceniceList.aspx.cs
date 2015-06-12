using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.ScenicStructure;
using System.Text;


namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 景区门票列表页
    /// 功能：显示查询以后获得景区门票的列表
    /// 创建人：lxh
    /// 创建时间： 2011-10-29 
    /// 修改人：徐从栎
    /// 修改时间：2011-12-20
    /// 修改说明：换界面等
    /// </summary>
    public partial class SceniceList : EyouSoft.Common.Control.BackPage
    {
        #region 参数
        protected int intPageIndex = 1;
        private int intPageSize = 5;
        private int RecordCount = 0;
        protected string mapApiCode = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断用户是否登录，如果没有登录跳转到登录页面
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
            }
            if (!IsPostBack)
            {
                this.mapApiCode = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
                BindProVince();

                if (Utils.GetQueryStringValue("SceniceName") != "")
                {
                    this.SceniceName.Value = Utils.GetQueryStringValue("SceniceName");
                }
                if (Utils.GetQueryStringValue("ProvinceId") != "" && this.ProvinceList.Items.FindByValue(Utils.GetInt(Utils.GetQueryStringValue("ProvinceId")).ToString()) != null)
                {
                    this.ProvinceList.Items.FindByValue(Utils.GetInt(Utils.GetQueryStringValue("ProvinceId")).ToString()).Selected = true;
                    if (Utils.GetQueryStringValue("CityId") != null)
                    {
                        EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("SetList('{0}', '{1}','{2}');", CityList.ClientID, Utils.GetInt(Utils.GetQueryStringValue("ProvinceId")), Utils.GetInt(Utils.GetQueryStringValue("CityId"))));
                        if (Utils.GetQueryStringValue("CountyID") != null)
                        {
                            EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("SetList('{0}', '{1}','{2}','{3}');", CountyList.ClientID, Utils.GetInt(Utils.GetQueryStringValue("ProvinceId")), Utils.GetInt(Utils.GetQueryStringValue("CityId")), Utils.GetInt(Utils.GetQueryStringValue("CountyID"))));
                        }
                    }
                }
                BingPageList();
            }
        }

        //绑定页面列表
        protected void BingPageList()
        {
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //景区门票查询实体
            EyouSoft.Model.ScenicStructure.MSearchSceniceArea SearchTicket = new EyouSoft.Model.ScenicStructure.MSearchSceniceArea();
            if (Utils.GetQueryStringValue("LevelID") != "")
            {
                SearchTicket.Level = (EyouSoft.Model.ScenicStructure.ScenicLevel)Enum.Parse(typeof(EyouSoft.Model.ScenicStructure.ScenicLevel), Utils.GetQueryStringValue("LevelID"));
            }
            //景区主题
            SearchTicket.ThemeId = Utils.GetIntNull(Utils.GetQueryStringValue("themeId"));

            //省份
            SearchTicket.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ProvinceId"));
            if (Utils.GetIntNull(Utils.GetQueryStringValue("ProvinceId")) == 0)
            {
                SearchTicket.ProvinceId = null;
            }
            //城市
            SearchTicket.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("CityId"));
            if (Utils.GetIntNull(Utils.GetQueryStringValue("CityId")) == 0)
            {
                SearchTicket.CityId = null;
            }

            //县区
            SearchTicket.CountyId = Utils.GetIntNull(Utils.GetQueryStringValue("CountyID"));
            if (Utils.GetIntNull(Utils.GetQueryStringValue("CountyID")) == 0)
            {
                SearchTicket.CountyId = null;
            }
            //排序方式
            if (Utils.GetQueryStringValue("sortType") != "")
            {
                SearchTicket.Type = Convert.ToInt32(Utils.GetQueryStringValue("sortType"));
            }
            //景区名称
            SearchTicket.ScenicName = Utils.GetQueryStringValue("SceniceName");
            SearchTicket.Status = EyouSoft.Model.ScenicStructure.ExamineStatus.已审核;
            //绑定列表
            //这个到时候底层会在加一个参数----易诺景区编号（char（36））的  到时候你从webconfig中取值后传给底层
            SearchTicket.YiNuo=EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "TongYe114SightId");
            System.Collections.Generic.IList<EyouSoft.Model.ScenicStructure.MScenicArea> Ticketslist = new EyouSoft.BLL.ScenicStructure.BScenicArea().GetListAndTickets(intPageSize, intPageIndex, ref RecordCount, SearchTicket);

            if (Ticketslist != null && Ticketslist.Count > 0)
            {
                this.CustomRepeater1.DataSource = Ticketslist;
                this.CustomRepeater1.DataBind();
                InitPage();
            }
            else
            {
                this.ExportPageInfo1.Visible = false;
                this.lit_msg.Text = "对不起,暂时没有景区门票信息！";
            }
        }

        //分页
        protected void InitPage()
        {
            var Params = Request.QueryString;
            var tmpParams = new System.Collections.Specialized.NameValueCollection();
            if (Params != null && Params.Count > 0)
            {
                foreach (var s in Params.AllKeys)
                {
                    if (!string.IsNullOrEmpty(s) && s.ToLower() != "urltype" && s.ToLower() != "requestsource" && s.ToLower() != "t")
                    {
                        tmpParams.Add(s, Params[s]);
                    }
                }
            }
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = RecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = tmpParams;
        }

        /// <summary>
        /// 绑定省份
        /// </summary>
        protected void BindProVince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.ProvinceList.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.ProvinceList.DataSource = ProvinceList;
                this.ProvinceList.DataTextField = "ProvinceName";
                this.ProvinceList.DataValueField = "ProvinceId";
                this.ProvinceList.DataBind();
            }
            this.ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.CityList.Items.Insert(0, new ListItem("请选择", "0"));
            this.CountyList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ProvinceList.Attributes.Add("onchange", string.Format("SetList('{0}',$(this).val(),'0');", this.CityList.ClientID) + string.Format("SetList('{0}',1,$('#{1}').val(),'0');", this.CountyList.ClientID, this.CityList.ClientID));
            this.CityList.Attributes.Add("onchange", string.Format("SetList('{0}',1,$(this).val(),'0');", this.CountyList.ClientID));
        }

        /// <summary>
        /// 获取门票详细信息
        /// </summary>
        /// <param name="o">景区门票集合</param>
        /// <param name="ScenicId">景区id</param>
        /// <returns></returns>
        protected string GetSenicce(object o, string ScenicName, string ID)
        {
            System.Collections.Generic.IList<EyouSoft.Model.ScenicStructure.MScenicTickets> Seniccelist = (List<EyouSoft.Model.ScenicStructure.MScenicTickets>)o;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (Seniccelist != null && Seniccelist.Count > 0)
            {
                sb.Append("<table width=\"98%\" border=\"1\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#9dc4dc\">");
                sb.Append("<tr class=\"list_basicbg\">");
                sb.Append("<th>门票类型</th><th>门市价</th><th>优惠价</th><th>有效时间</th><th>功能</th>");
                sb.Append("</tr>");
                for (int i = 0; i < Seniccelist.Count; i++)
                {
                    sb.Append("<tr><td align=\"left\">" + Seniccelist[i].TypeName + "</td><td align=\"center\">" + Utils.FilterEndOfTheZeroString(Seniccelist[i].RetailPrice.ToString("c2")) + "</td><td align=\"center\">" + Utils.FilterEndOfTheZeroString(Seniccelist[i].WebsitePrices.ToString("c2")) + "</td><td align=\"center\">" + DateTimeStr(Seniccelist[i].StartTime, Seniccelist[i].EndTime) + "</td><td align=\"center\"><a href=\"/ScenicManage/SceniceInfo.aspx?Id=" + ID + "&SceniceId=" + Seniccelist[i].TicketsId + "&ScenicName=" + ScenicName + "\" class=\"showinfo\">详细</a></td>");
                }
                sb.Append("</table>");
            }
            else
            {
                sb.Append("<div style=\"text-align:center; margin-top:10px; margin-bottom:10px\">暂无门票信息！</div>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 取得有效时间
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        protected string DateTimeStr(DateTime? start, DateTime? end)
        {
            string str = string.Empty;

            if (start == null || end == null)
            {
                str = "长期有效";
            }
            else
            {
                if (Convert.ToDateTime(start).ToString("yyyy-MM-dd").Equals("0001-01-01")
                || Convert.ToDateTime(end).ToString("yyyy-MM-dd").Equals("0001-01-01"))
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

        /// <summary>
        /// 获取景区主题
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetThemeHtml(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Collections.Generic.IList<EyouSoft.Model.ScenicStructure.MScenicTheme> Themelist = (List<EyouSoft.Model.ScenicStructure.MScenicTheme>)o;
            if (Themelist != null && Themelist.Count > 0)
            {
                foreach (EyouSoft.Model.ScenicStructure.MScenicTheme theme in Themelist)
                {
                    sb.Append("" + theme.ThemeName + "&nbsp;&nbsp;&nbsp;&nbsp;");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 景区等级
        /// </summary>
        /// <returns></returns>
        protected string GetSencinceLevel()
        {
            string LevelID = Utils.GetQueryStringValue("LevelID");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=\">全部</a> ");
            List<EnumObj> Level = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicLevel));
            if (Level != null && Level.Count > 0)
            {
                for (int i = 0; i < Level.Count; i++)
                {
                    if (Level[i].Value == LevelID)
                    {
                        sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Level[i].Value + "&themeId=" + Utils.GetQueryStringValue("themeId") + "&ProvinceId=" + Utils.GetQueryStringValue("ProvinceId") + "&CityId=" + Utils.GetQueryStringValue("CityId") + "&CountyID=" + Utils.GetQueryStringValue("CountyID") + "&sortType=" + Utils.GetQueryStringValue("sortType") + "\" class=\"ff0000\">" + Level[i].Text + "</a> ");
                    }
                    else
                    {
                        sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Level[i].Value + "&themeId=" + Utils.GetQueryStringValue("themeId") + "&ProvinceId=" + Utils.GetQueryStringValue("ProvinceId") + "&CityId=" + Utils.GetQueryStringValue("CityId") + "&CountyID=" + Utils.GetQueryStringValue("CountyID") + "&sortType=" + Utils.GetQueryStringValue("sortType") + "&SceniceName=" + Utils.GetQueryStringValue("SceniceName") + "\">" + Level[i].Text + "</a> ");
                    }
                }
                sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=0\">其他</a>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 景区主题
        /// </summary>
        /// <returns></returns>
        protected string GetTheme()
        {
            string themeId = Utils.GetQueryStringValue("themeId");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Collections.Generic.IList<EyouSoft.Model.ScenicStructure.MScenicTheme> themelist = new EyouSoft.BLL.ScenicStructure.BScenicTheme().GetList();
            sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Utils.GetQueryStringValue("LevelID") + "&themeId=\">全部</a> ");
            if (themelist != null && themelist.Count > 0)
            {
                for (int i = 0; i < themelist.Count; i++)
                {
                    if (themelist[i].ThemeId.ToString() == themeId)
                    {
                        sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Utils.GetQueryStringValue("LevelID") + "&themeId=" + themelist[i].ThemeId + "&ProvinceId=" + Utils.GetQueryStringValue("ProvinceId") + "&CityId=" + Utils.GetQueryStringValue("CityId") + "&CountyID=" + Utils.GetQueryStringValue("CountyID") + "&sortType=" + Utils.GetQueryStringValue("sortType") + "&SceniceName=" + Utils.GetQueryStringValue("SceniceName") + "\" class=\"ff0000\">" + themelist[i].ThemeName + "</a> ");
                    }
                    else
                    {
                        sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Utils.GetQueryStringValue("LevelID") + "&themeId=" + themelist[i].ThemeId + "&ProvinceId=" + Utils.GetQueryStringValue("ProvinceId") + "&CityId=" + Utils.GetQueryStringValue("CityId") + "&CountyID=" + Utils.GetQueryStringValue("CountyID") + "&sortType=" + Utils.GetQueryStringValue("sortType") + "&SceniceName=" + Utils.GetQueryStringValue("SceniceName") + "\">" + themelist[i].ThemeName + "</a> ");
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 所在地区
        /// </summary>
        /// <returns></returns>
        protected string GetRegionHtml()
        {
            string ProvinceId = Utils.GetQueryStringValue("ProvinceId");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Collections.Generic.IList<EyouSoft.Model.SystemStructure.SysProvince> proviceList = new EyouSoft.BLL.SystemStructure.SysProvince().GetProvinceList();
            sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Utils.GetQueryStringValue("LevelID") + "&themeId=" + Utils.GetQueryStringValue("themeId") + "&ProvinceId=\">全部</a> ");
            if (proviceList != null && proviceList.Count > 0)
            {
                for (int i = 0; i < proviceList.Count; i++)
                {
                    if (proviceList[i].ProvinceId.ToString() == ProvinceId)
                    {
                        sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Utils.GetQueryStringValue("LevelID") + "&themeId=" + Utils.GetQueryStringValue("themeId") + "&ProvinceId=" + proviceList[i].ProvinceId + "&CityId=" + Utils.GetQueryStringValue("CityId") + "&CountyID=" + Utils.GetQueryStringValue("CountyID") + "&sortType=" + Utils.GetQueryStringValue("sortType") + "&SceniceName=" + Utils.GetQueryStringValue("SceniceName") + "\" class=\"ff0000\">" + proviceList[i].ProvinceName + "</a> ");
                    }
                    else
                    {
                        sb.Append("<a href=\"/ScenicManage/SceniceList.aspx?LevelID=" + Utils.GetQueryStringValue("LevelID") + "&themeId=" + Utils.GetQueryStringValue("themeId") + "&ProvinceId=" + proviceList[i].ProvinceId + "&CityId=" + Utils.GetQueryStringValue("CityId") + "&CountyID=" + Utils.GetQueryStringValue("CountyID") + "&sortType=" + Utils.GetQueryStringValue("sortType") + "&SceniceName=" + Utils.GetQueryStringValue("SceniceName") + "\">" + proviceList[i].ProvinceName + "</a> ");
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 处理景区列表景区等级
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetSceniceLevel(object o)
        {
            string level = string.Empty;
            if (o.ToString() == "0")
            {
                level = "";
            }
            else
            {
                level = o.ToString();
            }
            return level;
        }

        /// <summary>
        /// 景区供应商 
        /// </summary>
        /// <returns></returns>
        protected string GetSeniceCom(object strSceniceId)
        {
            if (strSceniceId == null || string.IsNullOrEmpty(strSceniceId.ToString()))
                return string.Empty;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.ScenicStructure.MScenicArea Area = new EyouSoft.BLL.ScenicStructure.BScenicArea().GetModel(strSceniceId.ToString());
            if (Area != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(Area.Company.ID);

                if (CompanyDetail != null)
                {
                    sb.Append("<a href=\"" + Utils.GetShopUrl(CompanyDetail.ID, EyouSoft.Model.CompanyStructure.CompanyType.景区, -1) + "\"  target=\"_blank\">" + CompanyDetail.CompanyName + "</a>");

                    if (CompanyDetail.ContactInfo != null && !string.IsNullOrEmpty(CompanyDetail.ContactInfo.MQ))
                    {
                        sb.Append(Utils.GetMQ(CompanyDetail.ContactInfo.MQ));
                    }
                }
            }

            return sb.ToString();
        }
        protected string getPic(object o)
        {
            string str = "暂无图片";
            if (null != o)
            {
                IList<MScenicImg> lst = o as IList<MScenicImg>;
                if (null != lst && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(lst[i].Address))
                        {
                            str = string.Format("<img border=\"0\" class=\"imgbk\" src=\"{0}\" style=\"width:100px;height:75px;\"/>", Domain.FileSystem + lst[i].Address);
                            break;
                        }
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 价格列表
        /// </summary>
        /// <param name="t">0：简介中的门票   1：显示列表</param>
        /// <param name="id">景区ID</param>
        /// <param name="name">景区名</param>
        protected string getTicketPrice(object o, int t, object id, object name)
        {
            string str = (t == 0) ? "无" : "<td colspan='6' align='center'>暂无信息</td>";
            if (null != o)
            {
                IList<MScenicTickets> lst = o as IList<MScenicTickets>;
                if (null != lst && lst.Count > 0)
                {
                    switch (t)
                    {
                        case 0:
                            str = Convert.ToString(lst[0].RetailPrice);
                            break;
                        case 1:
                            StringBuilder strb = new StringBuilder();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                strb.AppendFormat(@"<tr {6}>
                                                                       <td align=""center"">{0}</td>
                                                                       <td align=""center"">￥{1}</td>
                                                                       <td align=""center""><strong class=""lvse"">￥{2}</strong></td>
                                                                       <td align=""center"" class=""TicketEndPrice""><strong class=""ff0000"">￥{3}</strong></td>
                                                                       <td align=""center"">{4}</td>
                                                                       <td align=""center""><a class=""basic_btn"" href=""javascript:void(0);"" onclick=""SceniceList.showInfo('/ScenicManage/SceniceInfo.aspx?TicketId={5}&SceniceId={7}&ScenicName={8}');""><span>详细</span></a></td>
                                                                  </tr>", lst[i].TypeName, (int)lst[i].RetailPrice, (int)lst[i].WebsitePrices, (int)lst[i].MarketPrice,
                                                                        (string.IsNullOrEmpty(Convert.ToString(lst[i].StartTime)) && string.IsNullOrEmpty(Convert.ToString(lst[i].EndTime))) ? "长期有效" : string.Format("{0:yyyy-MM-dd}至{1:yyyy-MM-dd}", lst[i].StartTime, lst[i].EndTime), lst[i].TicketsId,
                                                                        (i > 2) ? "class=\"showOrHideTr\"" : "", Convert.ToString(id), Convert.ToString(name));//前3个数据显示，后面的隐藏掉
                            }
                            str = strb.ToString();
                            break;
                    }
                }
            }
            return str;
        }
        protected string getImgFlag(object o)
        {
            string str = string.Empty;
            if (null != o)
            {
                EyouSoft.Model.CompanyStructure.CompanyLev flag = (EyouSoft.Model.CompanyStructure.CompanyLev)o;
                str = Utils.GetCompanyLevImg(flag);
            }
            return str;
        }
    }
}

