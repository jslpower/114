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
namespace UserBackCenter.CustomerManage
{   
    /// <summary>
    /// 页面功能：客户管理中的查看产品列表
    /// 开发人：xuty 开发时间：2010-07-12
    /// </summary>
    public partial class TourList : BasePage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;
        protected string companyName;//公司名
        protected string cer;//许可证号
        protected string admin;//负责人
        protected string companyId;
        protected EyouSoft.IBLL.SystemStructure.ISysArea areaBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)//是否登录
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx");
                return;
            }
            //if (!CheckGrant(TravelPermission.客户管理_管理栏目))
            //{
            //    Utils.ResponseNoPermit();
            //    return;
            //}
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel1 = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            //是否开通收费MQ
            //if (!companyModel1.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ))
            //{
            //    Server.Transfer("/SystemSet/ApplyMQ.aspx?iscustomer=yes&urltype=tab", false);
            //    return;
            //}
            companyId = Utils.GetQueryStringValue("companyid");//专线商
            EyouSoft.Model.CompanyStructure.CompanyInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyId);
            companyName = companyModel.CompanyName;
            cer = companyModel.License;
            admin = companyModel.ContactInfo.ContactName;
            //获取当前页
            pageIndex = Utils.GetInt(Request.QueryString["Page"],1);
            EyouSoft.IBLL.TourStructure.ITour tourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            areaBll=EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            //绑定产品列表
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tourInfoList = tourBll.GetNotStartingTours(pageSize, pageIndex, ref recordCount, companyId, null);
            if (tourInfoList!=null&&tourInfoList.Count > 0)
            {   
                tl_rpt_TourList.DataSource = tourInfoList;
                tl_rpt_TourList.DataBind();
                BindPage();
            }
            else
            {
                tl_rpt_TourList.EmptyText = "暂无团队信息";
                this.ExportPageInfo1.Visible = false;
            }
            tourInfoList = null;
        }

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("companyid", companyId);
            this.ExportPageInfo1.UrlParams.Add("iframeId", Utils.GetQueryStringValue("iframeId"));
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion

        #region 获取区域名
        protected string GetAreaName(int areaId)
        {
            EyouSoft.Model.SystemStructure.SysArea areaModel=areaBll.GetSysAreaModel(areaId);
            if(areaModel!=null)
               return areaModel.AreaName;
            else
                return "";
        }
        #endregion
    }
}
