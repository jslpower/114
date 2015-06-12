using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 异步请求的供求信息页
    /// </summary>
    /// 周文超 2010-07-31
    public partial class AjaxSupplierInfo : EyouSoft.Common.Control.FrontPage
    {
        protected int intPageSize = 30, CurrencyPage = 1, intRecordCount = 0;
        /// <summary>
        /// 登录页面url
        /// </summary>
        private string strLoginUrl = string.Empty;

        private new int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CityId = Utils.GetInt(Request.QueryString["CityId"]);
            if (!IsPostBack)
            {
                InitExchangeList();
            }
        }

        /// <summary>
        /// 初始化供求信息列表
        /// </summary>
        private void InitExchangeList()
        {
            #region 参数实例化
            int Time = 0;
            int Tag = -1;
            int Type = -1;
            int ProvinceId = 0;
            int SearchType = 0;
            EyouSoft.Model.CommunityStructure.ExchangeTag? ExTag = null;
            EyouSoft.Model.CommunityStructure.ExchangeType? ExType = null;
            DateTime? IssueTime = null;
            string kw = string.Empty;
            if (Request.QueryString["SearchType"] != null)
            {
                int.TryParse(Request.QueryString["SearchType"].ToString(), out SearchType);
            }
            // 今天 昨天 前天
            if (Request.QueryString["time"] != null)
            {
                int.TryParse(Request.QueryString["time"].ToString(), out Time);
            }
            // 供求大类别
            if (Request.QueryString["type"] != null)
            {
                int.TryParse(Request.QueryString["type"].ToString(), out Type);
            }
            //供求小类别
            if (Request.QueryString["tag"] != null)
            {
                int.TryParse(Request.QueryString["tag"].ToString(), out Tag);
            }
            //省份ID
            if (Request.QueryString["pid"] != null)
            {
                int.TryParse(Request.QueryString["pid"].ToString(), out ProvinceId);
            }
            //供求标题关键字
            if (Request.QueryString["kw"] != null && Request.QueryString["kw"].ToString().Length > 0)
            {
                kw = Utils.InputText(Request.QueryString["kw"].ToString());
            }
            //每页条数
            if (Request.QueryString["psize"] != null)
            {
                int.TryParse(Request.QueryString["psize"].ToString(), out intPageSize);
            }
            //当前页数
            if (Request.QueryString["pindex"] != null)
            {
                int.TryParse(Request.QueryString["pindex"].ToString(), out CurrencyPage);
            }
            if (Type > -1)
            {
                ExType = (EyouSoft.Model.CommunityStructure.ExchangeType)Type;
            }
            if (Tag > -1)
            {
                ExTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Tag;
            }
            if (Time != -4)
            {
                IssueTime = DateTime.Now.AddDays(Time);
            }
            #endregion

            IList<EyouSoft.Model.CommunityStructure.ExchangeList> List = null;
            if (SearchType > 0)
            {
                List = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetWebSerachList(intPageSize, CurrencyPage, ref intRecordCount, ExType, ExTag,
                    ProvinceId, ProvinceId == 0 ? CityId : 0, kw, (EyouSoft.Model.CommunityStructure.SearchDateType)Time);
            }
            else
            {
                List = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetSerachList(intPageSize, CurrencyPage, ref intRecordCount, ExType,
                ExTag, IssueTime, ProvinceId, ProvinceId == 0 ? CityId : 0, kw, null, null);
            }
            if (List != null && List.Count > 0)
            {
                rptExchangeList.DataSource = List;
                rptExchangeList.DataBind();
            }
            else
            {
                Response.Write("<div style=\"color:red;width:100%;margin-top:40px;margin-bottom:20px;text-align:center;\">暂无供求信息<div>");
            }

        }

        #region 行绑定事件

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatorList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                System.Text.StringBuilder strTmp = null;
                EyouSoft.Model.CommunityStructure.ExchangeList Exchange = (EyouSoft.Model.CommunityStructure.ExchangeList)e.Item.DataItem;
                EyouSoft.Model.SystemStructure.SysProvince modelP = null;

                Literal ltr = (Literal)e.Item.FindControl("ltrTagHtml");
                if (ltr != null && Exchange != null && Exchange.ExchangeTagHtml != null)
                    ltr.Text = Exchange.ExchangeTagHtml.TagHTML;

                //初始化省份链接
                Literal ltrProvince = (Literal)e.Item.FindControl("ltrProvince");
                if (ltrProvince != null && Exchange != null)
                {
                    strTmp = new System.Text.StringBuilder();
                    modelP = new EyouSoft.Model.SystemStructure.SysProvince();
                    modelP = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(Exchange.ProvinceId);
                    if (IsLogin)
                    {
                        strTmp.AppendFormat("<a href='{0}'>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoListWrite(Exchange.ProvinceId, CityId));
                        //strTmp.AppendFormat("<a href='{0}'>", Utils.GeneratePublicCenterUrl(string.Format("/SupplierInfo/ExchangeList.aspx?PId={0}", Exchange.ProvinceId), CityId));
                        strTmp.AppendFormat("{0}</a>", modelP == null ? "" : "【" + modelP.ProvinceName + "】");
                    }
                    else
                    {
                        strTmp.AppendFormat("<a href='{0}'>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoListWrite(Exchange.ProvinceId, CityId));
                        //strTmp.AppendFormat("<a href='{0}'>", Utils.GeneratePublicCenterUrl(string.Format("/SupplierInfo/ExchangeList.aspx?PId={0}", Exchange.ProvinceId), CityId));
                        strTmp.AppendFormat("{0}</a>", modelP == null ? "" : "【" + modelP.ProvinceName + "】");
                    }
                    ltrProvince.Text = strTmp.ToString();
                    modelP = null;
                    strTmp = null;
                }
                //初始化标题链接
                Literal ltrTitle = (Literal)e.Item.FindControl("ltrTitle");
                if (ltrTitle != null && Exchange != null)
                {
                    strTmp = new System.Text.StringBuilder();
                    if (IsLogin)
                    {
                        strTmp.AppendFormat("<a href='{0}' title=\"{2}\">{1}</a>",
                            SiteUserInfo.IsEnable?
                                EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Exchange.ID,CityId)
                                :"javascript:alert('您的账户还没有启用，请联系管理员！');return false;",
                            Utils.GetText(Exchange.ExchangeTitle, 26), 
                            Exchange.ExchangeTitle);
                    }
                    else
                    {
                        //strLoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(
                            //EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Exchange.ID, CityId), 
                           // "你目前进行的操作，需要“登录”后才能继续……");
                        //strTmp.AppendFormat("<a href='{0}' title=\"{1}\">", strLoginUrl, Exchange.ExchangeTitle);
                        strTmp.AppendFormat("<a href='{0}' title=\"{1}\">", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Exchange.ID, CityId), Exchange.ExchangeTitle);
                        strTmp.AppendFormat("{0}</a>", Utils.GetText(Exchange.ExchangeTitle, 26));
                    }
                    ltrTitle.Text = strTmp.ToString();
                    strTmp = null;
                }
                //初始化MQ链接
                Literal ltrMQ = (Literal)e.Item.FindControl("ltrMQ");
                if (ltrMQ != null && Exchange != null)
                {
                    ltrMQ.Text = Utils.GetMQ(Exchange.OperatorMQ);
                }

                Exchange = null;
            }
        }

        #endregion
    }
}
