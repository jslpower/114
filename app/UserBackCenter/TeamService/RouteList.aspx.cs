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
using EyouSoft.Common.Control;
using EyouSoft.Common;
namespace UserBackCenter.TeamService
{   
    /// <summary>
    /// 页面功能：设置目录下批发商产品产看
    /// 开发人：xuty 开发时间：2010-07-01
    /// </summary>
    public partial class RouteList : EyouSoft.Common.Control.BasePage
    {   
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;
        protected string companyName;//公司名
        protected string cer;//许可证号
        protected string admin;//负责人


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)//是否登录
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx");
                return;
            }
         

            //绑定产品
            BindTour();
        }

        #region 绑定产品信息
        protected void BindTour()
        {
            if (!CheckGrant(TravelPermission.组团_线路散客订单管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);//获取当前页
            string companyId = Utils.GetQueryStringValue("companyid");//专线商ID
            EyouSoft.Model.CompanyStructure.CompanyInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyId);
            //获取公司名,许可证,负责人
            if(companyModel!=null)
            {
                companyName=companyModel.CompanyName;
                cer=companyModel.License;
                admin = companyModel.ContactInfo.ContactName;
            }
            //获取产品列表
            EyouSoft.IBLL.TourStructure.ITour tourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tourInfoList = tourBll.GetNotStartingTours(pageSize, pageIndex, ref recordCount, companyId, null);
            if (tourInfoList!=null&&tourInfoList.Count > 0)
            {
                rl_rpt_TourList.DataSource = tourInfoList;
                rl_rpt_TourList.DataBind();
                BindPage();//设置分页
            }
            else
            {
                rl_rpt_TourList.EmptyText = "暂无团队信息";
                this.ExportPageInfo1.Visible = false;
            }
            tourInfoList = null;
        }
        #endregion

       
        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("routearea", Utils.GetQueryStringValue("routearea"));
            this.ExportPageInfo1.UrlParams.Add("areatype", Utils.GetQueryStringValue("areatype"));
            this.ExportPageInfo1.UrlParams.Add("companyid", Utils.GetQueryStringValue("companyid"));
            this.ExportPageInfo1.UrlParams.Add("iframeId", Utils.GetQueryStringValue("iframeId"));
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion
    }
}
