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
using EyouSoft.Common;
using System.Text;

namespace UserPublicCenter.RouteManage
{
    /// <summary>
    /// 地接社列表
    /// 罗丽娥    2010-07-23
    /// --------------------------
    /// 修改人：张新兵，修改时间：2011-3-8
    /// 修改内容：修改页面标题为：
    /// （地名）地接社_(地名)地接电话_(地名)旅行社大全，
    /// （地名）由 当前列表中地接社所在的省份名称或者城市名称 进行 格式化
    /// </summary>
    public partial class LocalAgencyList : EyouSoft.Common.Control.FrontPage
    {
        protected string strCityList = string.Empty;

        protected int RequestCityID = 0, ProvinceID = 0, SaleCityID = 0;
        private int intPageSize = 10, CurrencyPage = 1;
        protected string strURL = string.Empty;
        private IList<EyouSoft.Model.CompanyStructure.CompanyAttachInfo> AttachList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestCityID = Utils.GetInt(Request.QueryString["RequestCityID"], 0);//目的地地接社所在城市
            SaleCityID = CityId;
            ProvinceID = Utils.GetInt(Request.QueryString["ProvinceID"], 0);//目的地接社所在省份
            strURL = Utils.GeneratePublicCenterUrl("/RouteManage/LocalAgencyList.aspx?ProvinceID=" + ProvinceID + "&RequestCityID=" + RequestCityID, CityId);
            if (!Page.IsPostBack)
            {
                InitCityList();

                InitLocalAgencyList();
            }

            //如果 地接社所在城市 条件 不为空，则显示 城市名称
            //如果 地接社所在城市条件 为空，则显示 所在省份名称
            string placeName = string.Empty;//地名
            if (RequestCityID != 0)
            {
                EyouSoft.Model.SystemStructure.SysCity tmpCityModel = null;
                EyouSoft.IBLL.SystemStructure.ISysCity bll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
                tmpCityModel = bll.GetSysCityModel(RequestCityID);
                if (tmpCityModel != null)
                {
                    placeName = tmpCityModel.CityName;
                }
            }
            else if (ProvinceID != 0)
            {
                EyouSoft.Model.SystemStructure.SysProvince tmpProModel = null;
                EyouSoft.IBLL.SystemStructure.ISysProvince proBll = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance();
                tmpProModel = proBll.GetProvinceModel(ProvinceID);
                if (tmpProModel != null)
                {
                    placeName = tmpProModel.ProvinceName;
                }
            }
            
            //根据 地名 格式化 页面标题
            this.Page.Title = string.Format("{0}地接社_{0}地接电话_{0}旅行社大全",placeName);
        }

        #region 获取目的地地接社城市列表
        /// <summary>
        /// 获取目的地地接社城市列表
        /// </summary>
        private void InitCityList()
        {
            StringBuilder str = new StringBuilder();
            string tmp = string.Empty;
            EyouSoft.IBLL.SystemStructure.ISysCity bll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysCity> list = bll.GetSysCityList(ProvinceID,null);
            if (list != null && list.Count > 0)
            {                
                foreach (EyouSoft.Model.SystemStructure.SysCity model in list)
                {
                    if (RequestCityID == 0)
                    {
                        tmp = string.Format("<li id=\"cityID_0\"><a class=\"dijieshechengon\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyListUrl(model.ProvinceId, CityId) + "\"><nobr>{1}：</nobr></a></li>", model.ProvinceId, model.ProvinceName);
                        //tmp = string.Format("<li id=\"cityID_0\"><a class=\"dijieshechengon\" href=\"" + Utils.GeneratePublicCenterUrl("/RouteManage/LocalAgencyList.aspx?ProvinceID={0}", CityId) + "\"><nobr>{1}：</nobr></a></li>", model.ProvinceId, model.ProvinceName);
                    }
                    else {
                        tmp = string.Format("<li id=\"cityID_0\"><a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyListUrl(model.ProvinceId, CityId) + "\"><nobr>{1}：</nobr></a></li>", model.ProvinceId, model.ProvinceName);
                        //tmp = string.Format("<li id=\"cityID_0\"><a href=\"" + Utils.GeneratePublicCenterUrl("/RouteManage/LocalAgencyList.aspx?ProvinceID={0}", CityId) + "\"><nobr>{1}：</nobr></a></li>", model.ProvinceId, model.ProvinceName);
                    }

                    if (model.CityId == RequestCityID)
                    {
                        str.AppendFormat("<li id=\"cityID_{0}\"><a class=\"dijieshechengon\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyListUrl(model.ProvinceId, model.CityId, CityId) + "\"><nobr>{2}</nobr></a></li>", model.CityId, model.ProvinceId, model.CityName);                        
                        //str.AppendFormat("<li id=\"cityID_{0}\"><a class=\"dijieshechengon\" href=\"" + Utils.GeneratePublicCenterUrl("/RouteManage/LocalAgencyList.aspx?ProvinceID={1}&RequestCityID={0}", CityId) + "\"><nobr>{2}</nobr></a></li>", model.CityId, model.ProvinceId, model.CityName);                        
                    }
                    else
                    {
                        str.AppendFormat("<li id=\"cityID_{0}\"><a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetLocalAgencyListUrl(model.ProvinceId, model.CityId, CityId) + "\"><nobr>{2}</nobr></a></li>", model.CityId, model.ProvinceId, model.CityName);
                        //str.AppendFormat("<li id=\"cityID_{0}\"><a href=\"" + Utils.GeneratePublicCenterUrl("/RouteManage/LocalAgencyList.aspx?ProvinceID={1}&RequestCityID={0}", CityId) + "\"><nobr>{2}</nobr></a></li>", model.CityId, model.ProvinceId, model.CityName);
                    }
                }                
            }
            list = null;
            bll = null;
            if (str.ToString() != string.Empty)
            {
                strCityList = tmp + str.ToString();
            }
        }
        #endregion 

        #region 获取地接社列表
        private void InitLocalAgencyList()
        {
            int intRecordCount = 0;
            string CompanyName = Server.UrlDecode(Utils.GetQueryStringValue("CompanyName"));
            string ContactName = Server.UrlDecode(Utils.GetQueryStringValue("ContactName"));
            CurrencyPage = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo bll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.QueryParamsCompany query = new EyouSoft.Model.CompanyStructure.QueryParamsCompany();
            query.CityId = RequestCityID;
            query.CompanyName = CompanyName;
            query.ContactName = ContactName;
            query.PorvinceId = ProvinceID;
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> list = bll.GetListLocalAgency(query,intPageSize,CurrencyPage,ref intRecordCount);
            if (intRecordCount > 0)
            {

                string[] CompanyIDList = new string[list.Count];
                CompanyIDList = (from c in list where true select c.ID).ToArray();
                AttachList = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetList(CompanyIDList);

                this.rptLocalAgencyList.DataSource = list;
                this.rptLocalAgencyList.DataBind();

                if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
                {
                    this.ExporPageInfoSelect1.Placeholder = "#PageIndex#";
                    this.ExporPageInfoSelect1.IsUrlRewrite = true;
                    string strTemp = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request);
                    if (strTemp.Split('_').Count() == 3)
                    {
                        this.ExporPageInfoSelect1.PageLinkURL = strTemp.Split('_')[0] + "_" + strTemp.Split('_')[1] + "_0" + "_" + CityId + "_#PageIndex#";
                    }
                    else
                    {
                        this.ExporPageInfoSelect1.PageLinkURL = strTemp + "_#PageIndex#";
                    }
                    //this.ExporPageInfoSelect1.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
                }
                else
                {
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                }

                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                //this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?CityID=" + CityId + "&";
                //this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            }
            else {
                this.ExporPageInfoSelect1.Visible = false;
                this.pnlNoData.Visible = true;
            }

            this.txtCompanyName.Value = CompanyName;
            this.txtContactName.Value = ContactName;

            list = null;
            bll = null;
        }
        #endregion

        #region Repeater_ItemDataBound
        protected void rptLocalAgencyList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EyouSoft.Model.CompanyStructure.CompanyInfo model = (EyouSoft.Model.CompanyStructure.CompanyInfo)e.Item.DataItem;
                System.Web.UI.HtmlControls.HtmlImage imgLogo = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgLogo");
               
                if (AttachList != null && AttachList.Count > 0)
                {
                    foreach (EyouSoft.Model.CompanyStructure.CompanyAttachInfo AttachModel in AttachList)
                    {
                        if (AttachModel.CompanyId == model.ID)
                        {
                            imgLogo.Src = Utils.GetNewImgUrl(AttachModel.CompanyLogo.ImagePath, 3);
                            break;
                        }
                    }
                }
                System.Web.UI.WebControls.Label lblCityName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblCityName");
                EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(model.CityId);
                if (cityModel != null)
                {
                    lblCityName.Text = cityModel.CityName;
                }
                cityModel = null;

                System.Web.UI.WebControls.Literal ltrContactInfo = (System.Web.UI.WebControls.Literal)e.Item.FindControl("ltrContactInfo");
                StringBuilder str = new StringBuilder();
                if (model.ContactInfo != null)
                {
                    str.AppendFormat("<td>联系人：<strong>{0}</strong></td>", model.ContactInfo.ContactName);
                    str.AppendFormat("<td><strong>电&nbsp;&nbsp;话：</strong><span class=\"hong\">{0}</span></td>", model.ContactInfo.Tel);
                    str.AppendFormat("<td>传&nbsp;&nbsp;真：<span class=\"hong\">{0}</span></td>", model.ContactInfo.Fax);
                }
                ltrContactInfo.Text = str.ToString();
            }
        }
        #endregion
    }
}
