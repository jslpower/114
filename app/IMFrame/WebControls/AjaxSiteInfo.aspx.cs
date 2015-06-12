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
using System.Collections.Generic;

namespace TourUnion.WEB.IM.WebControls
{
    /// <summary>
    /// 功能:使用ajax调用某个省份下的分站信息
    /// </summary>
    public partial class AjaxSiteInfo : System.Web.UI.Page
    {
        private int ProvinceId = 0;
        private int SiteId = 0;
        private int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProvinceId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["ProvinceId"]);
                SiteId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["SiteId"]);
                CityId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["CityId"]);

                string SiteName = GetSiteName(SiteId);
                string SiteInfo = GetSiteInfo(ProvinceId);
                Response.Write(SiteName + "~~@@~~" + SiteInfo);
                Response.End();
            }
        }

        private string GetSiteInfo(int ProvinceId)
        {
            string strSiteName = "";
            TourUnion.BLL.TourUnion_UnionSites Sitebll = new TourUnion.BLL.TourUnion_UnionSites();
            TourUnion.Account.Model.Account operatorModel = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.TourAgency, TourUnion.Account.Enum.SystemMedia.MQ).AccountUser;
            int UnionId = 0;
            if (operatorModel != null)
            {
                UnionId = operatorModel.UnionId;
            }
            DataSet ds = Sitebll.GetSiteCityProvinceAll(UnionId, false, 0);
            operatorModel = null;
            //获得当前省份下的分站信息
            DataRow[] rows = ds.Tables[0].Select(string.Format("ProvinceId={0}", ProvinceId));
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    strSiteName += string.Format("<a id='{0}' href='javascript:void(0)' onclick='ChangeSiteName(this,{0},{1})' {3}>{2}</a>&nbsp;|&nbsp;", row["SiteId"].ToString(), row["CityId"].ToString(), row["SiteName"].ToString(), row["SiteId"].ToString() == SiteId.ToString() ? "style='color:Red'" : "");
                }

                strSiteName = strSiteName.Length > 0 ? strSiteName.Substring(0, strSiteName.LastIndexOf("|")) : "";
            }
            else   //cookie中所保存的分站信息在数据库中已经不存在了
            {
                WEB.ProceFlow.SiteManage.DealCookieSiteNotData();
            }
            return strSiteName;
        }

        private string GetSiteName(int SiteId)
        {
            TourUnion.BLL.TourUnion_UnionSites bll = new TourUnion.BLL.TourUnion_UnionSites();
            TourUnion.Model.TourUnion_UnionSites model = bll.GetModel(SiteId);
            string SiteName = model.SiteName;
            model = null;
            bll = null;
            return SiteName;
        }
    }
}
