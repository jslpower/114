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

namespace TourUnion.WEB.IM.TourAgency
{
    /// <summary>
    /// 页面功能:ajax调用设置关注批发商
    /// 开发人：刘玉灵  时间：2009-9-9
    /// </summary>
    public partial class AjaxSetAttention : System.Web.UI.Page
    {
        protected int UnionId = 0;
        protected int SiteId = 0;
        protected int CityId = 0;
        protected int CompanyId = 0;
        protected int OpertorId = 0;
        protected int IsSetttionCompany = 0;
        protected string strUrl = "";
        protected string CompanyName = "";
        private int MQId = 0;
        private string Md5 = "";
        private string UserValue = "";
        private string Md5Value = "";
        private int AreaId =0;
        protected int NationType = -1;
        protected int AgencyId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["NationType"]))
            //{
            //    NationType = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["NationType"]);
            //}
            //if (!Page.IsPostBack)
            //{
            //    #region 初始化变量
            //    UnionId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["UnionId"]);
            //    SiteId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["SiteId"]);
            //    CityId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["CityId"]);
            //    AreaId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["AreaId"]);

            //    CompanyId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["CompanyId"]);
            //    OpertorId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["OpertorId"]);
            //    IsSetttionCompany = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["IsSetttionCompany"]);

            //    strUrl = TourUnion.WEB.ProceFlow.LoginManage.GetAgencyAttentionTourUrl();
            //    CompanyName = Request.QueryString["CompanyName"];

            //    TourUnion.Account.Model.Account account = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.TourAgency, TourUnion.Account.Enum.SystemMedia.MQ).AccountUser;
            //    if (account != null)
            //    {
            //        AgencyId = account.CompanyId;
            //        MQId = account.ContactMQ;
            //        Md5 = account.MD5Password;
            //    }
            //    account = null;

            //    TourUnion.Account.Model.MQUrlPar mqurlpar = TourUnion.Account.Model.UserManage.MQUrlParModel;
            //    UserValue = mqurlpar.IDMyUrlName;
            //    Md5Value = mqurlpar.PwMyUrlName;
            //    mqurlpar = null;

            //    #endregion

            //    GetCompanyList();
            //}

        }

        #region  绑定列表 
        protected void GetCompanyList()
        {
            //DataSet ds = null;
            //TourUnion.BLL.TourUnion_CompanyInfo bll = new TourUnion.BLL.TourUnion_CompanyInfo();
            //string SqlWhere = " UnionId=" + UnionId;
            //if (SiteId != 0)
            //{
            //    SqlWhere += " and SiteId=" + SiteId.ToString();
            //}

            //if (AreaId != 0)
            //{
            //    SqlWhere += " and AreaId=" + AreaId;
            //}
            //else
            //{
            //    this.NoDate.Visible = true;
            //    return;
            //}
            //if (!string.IsNullOrEmpty(CompanyName))
            //{
            //    SqlWhere += " and CompanyName like '%" + Server.UrlDecode(CompanyName.Trim()) + "%'";
            //}

            //SqlWhere += " Order by AreaId,IsOpen Desc ";

            //ds = bll.GetCompanylistByAreaIdDs(SqlWhere, false);
           
            //if (ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "LoadData(this)", 0);
            //    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "LoadData(this)", 1);

            //    Adpost.Common.ExporPage.PageControlSelect pagecontrol1 = new Adpost.Common.ExporPage.PageControlSelect(20);
            //    this.ExporPageInfoSelect1.PageLinkCount = 7;
            //    this.ExporPageInfoSelect1.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.MostEasyNewButtonStyle;
            //    this.repCompanyList.DataSource = pagecontrol1.GetPageDataSource(ds.Tables[0], this.ExporPageInfoSelect1, Adpost.Common.ExporPage.HrefTypeEnum.JsHref);
            //    this.repCompanyList.DataBind();
            //    pagecontrol1 = null;
            //    this.NoDate.Visible = false;
            //}
            //else
            //{
            //    this.NoDate.Visible = true;
            //}
            //if (ds != null)
            //{
            //    ds.Dispose();
            //}
            //ds = null;
            //bll = null;

        }
        #endregion 


        #region 前台方法
        /// <summary>
        /// 获得每一家公司的信息
        /// </summary>
        protected string GetstrCompany(int Id, string CompanyName, string ContactName, string ContactTel, string ContactMobile, string strAreaId,string TendCompanyId, string isTendCompany)
        {
            string strReturn = "";
            string Name = ContactName;
            if (Name.Length > 12)
            {
                string tmp1 = Name.Substring(0, 12);
                string tmp2 = Name.Substring(12, Name.Length - 12);
                Name = tmp1 + "<br />" + tmp2;
            }
            string Tel = ContactTel;
            if (Tel.Length > 18)
            {
                string tmp1 = Tel.Substring(0, 18);
                string tmp2 = Tel.Substring(18, Tel.Length - 18);
                Tel = tmp1 + "<br />" + tmp2;
            }
            string Mobile = ContactMobile;
            if (Mobile.Length > 18)
            {
                string tmp1 = Mobile.Substring(0, 18);
                string tmp2 = Mobile.Substring(12, Mobile.Length - 18);
                Mobile = tmp1 + "<br />" + tmp2;
            }
            string strContact = "联系人:" + Name + "<br />电 话:" + Tel + "<br />手 机:" + Mobile + "<br />";

            bool isNotttentionArea = false;
            string strIsChecked = "";
            string strCompanyId = Id.ToString();
            if (TendCompanyId != "0")
            {
                strCompanyId = TendCompanyId;
            }
            string url = "/IM/LoginWeb.aspx?" + UserValue + "=" + MQId + "&" + Md5Value + "=" + Md5 + "&CsToBsRedirectUrl=" + Server.UrlEncode("/TourAgency/TourManger/CompanyTourDateList.aspx?CompanyId=" + strCompanyId + "&isTendCompany=" + isTendCompany);

            strIsChecked = " checked='true'";

             strReturn += string.Format("<td width=\"13%\" align=\"center\" bgcolor=\"#FFFFFF\"><input type=\"checkbox\" id=\"input_"+SiteId+"_"+AreaId+"_"+Id+"\" name=\"checkbox\" value=\"checkbox\" {4} onclick=\"IsSettionCompany(this,'{0}','{1}','{2}','{3}')\" /></td>", Id, AreaId, SiteId, CityId, strIsChecked);
            strReturn += string.Format("<td width=\"87%\" bgcolor=\"#FFFFFF\"><a target=\"_blank\"  href=\"{2}\" class=\"cliewh\" onmouseover=\"wsug(event, '{0}')\" onmouseout=\"wsug(event, 0)\">{1}</a></td>", strContact, CompanyName, url);
            return strReturn;

        }
        #endregion
    }
}
