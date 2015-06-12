using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace IMFrame.ExchangeTopic
{
    /// <summary>
    /// 异步请求的供求信息页
    /// </summary>
    /// 周文超 2010-07-31
    public partial class AjaxSupplierInfo : EyouSoft.ControlCommon.Control.MQPage
    {
        protected int intPageSize = 15, CurrencyPage = 1, intRecordCount = 0;
        /// <summary>
        /// 登录页面url
        /// </summary>
        private string strLoginUrl = string.Empty;

        private new int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitExchangeList();
                string ClientIP = StringValidate.GetRemoteIP();
                if (!string.IsNullOrEmpty(ClientIP))
                {
                    EyouSoft.Model.SystemStructure.CityBase model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetClientCityByIp(ClientIP);
                    if (model != null)
                    {
                        CityId = model.CityId;
                    }
                    model = null;
                }
            }
        }

        /// <summary>
        /// 初始化供求信息列表
        /// </summary>
        private void InitExchangeList()
        {
            #region 参数实例化
            int ProvinceId = 0;
            //省份ID
            if (Request.QueryString["pid"] != null)
            {
                int.TryParse(Request.QueryString["pid"].ToString(), out ProvinceId);
            }
            /*int Time = 0;
            int Tag = -1;
            int Type = -1;
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
            //供求标题关键字
            if (Request.QueryString["kw"] != null && Request.QueryString["kw"].ToString().Length > 0)
            {
                kw = Utils.InputText(Request.QueryString["kw"].ToString());
            }*/
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
            /*if (Type > -1)
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
            }*/
            #endregion

            IList<EyouSoft.Model.CommunityStructure.ExchangeList> List = null;
            List = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetWebSerachList(intPageSize, CurrencyPage, ref intRecordCount, null, null,
                    ProvinceId, 0, string.Empty,EyouSoft.Model.CommunityStructure.SearchDateType.全部);  
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

                //初始化标题链接
                Literal ltrTitle = (Literal)e.Item.FindControl("ltrTitle");
                if (ltrTitle != null && Exchange != null)
                {
                    string TargetUrl=string.Format("{0}/SupplierInfo/ExchangeInfo.aspx?Id={1}&CityId={2}",Domain.UserPublicCenter,Exchange.ID,CityId);
                    strTmp = new System.Text.StringBuilder();
                    if (IsLogin)
                    {
                        strTmp.AppendFormat("<a href='{0}' target=\"_blank\">{1}</a>", SiteUserInfo.IsEnable ? GetDesPlatformUrl(TargetUrl): "javascript:;", Utils.GetText(Exchange.ExchangeTitle, 14));
                    }
                    //else
                    //{
                    //    strLoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Utils.GeneratePublicCenterUrl(string.Format("/SupplierInfo/ExchangeInfo.aspx?Id={0}", Exchange.ID), CityId), string.Empty);
                    //    strTmp.AppendFormat("<a target=\"_blank\" href='{0}'>", strLoginUrl);
                    //    strTmp.AppendFormat("{0}</a>", Utils.GetText(Exchange.ExchangeTitle, 6));
                    //}
                    ltrTitle.Text = strTmp.ToString();
                    strTmp = null;
                }
                Exchange = null;
            }
        }

        #endregion
    }
}
