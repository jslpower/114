using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
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
using EyouSoft.Common.Function;
using EyouSoft.Common;
using EyouSoft.BLL.CompanyStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.TeamService
{   
    /// <summary>
    /// 页面功能：ajax获取设置目录下的公司集合
    /// 开发人：xuty 开发时间：2010-06-24
    /// </summary>
    public partial class AjaxLineCompanyList : EyouSoft.Common.Control.BasePage
    {
        protected int recordCount;
        protected int pageSize=10;
        protected int pageIndex = 1;
        EyouSoft.IBLL.CompanyStructure.ICompanyFavor favorBll;
        IList<string> companyCheckedList;//被收藏批发商Id集合
        EyouSoft.IBLL.TourStructure.ITour tourBll;
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsLogin)//是否登录
           {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Request.UrlReferrer.ToString());
           }
           //if (!CheckGrant(TravelPermission.组团_管理栏目))
           //{
           //    Utils.ResponseNoPermit();
           //    return;
           //}
           favorBll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();
           //获取当前页
           pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
           companyCheckedList = favorBll.GetListByCompanyId(SiteUserInfo.CompanyID);//获取收藏的批发商Id
           tourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
           BindCompany();//绑定所有批发商
        }

        #region 绑定批发商
        protected void BindCompany()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo companyBll=EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            int areaId = Utils.GetInt(Request.QueryString["areaid"],0);
            if (areaId!=0)
            {   
                //获取查询条件
                QueryParamsCompanyBase query = new QueryParamsCompanyBase();
                query.AreaId=areaId;
                query.CityId = SiteUserInfo.CityId;
                //获取批发商数据
                IList<EyouSoft.Model.CompanyStructure.CompanyInfo> companyList = companyBll.GetListRouteAgency(query, pageSize, pageIndex, ref recordCount);
                if (companyList.Count > 0)
                {
                    alcl_rpt_companyList1.DataSource = companyList;
                    alcl_rpt_companyList1.DataBind();
                    BindPage(areaId);//设置分页
                }
                else
                {
                    alcl_rpt_companyList1.EmptyText = "暂无批发商信息";
                    this.ExportPageInfo1.Visible = false;
                }
                companyList = null;
            }
        }
        #endregion

        protected string GetTourCount(string companyId)
        {
           return tourBll.GetTourNumber(companyId, EyouSoft.Model.TourStructure.TourDisplayType.线路产品).ToString();
          
        }


        #region 产生分页控件
        protected void BindPage(int areaId)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
            this.ExportPageInfo1.UrlParams.Add("areaid", areaId.ToString());
        }
        #endregion

        #region 判断是否被收藏批发商
        protected string GetChecked(string companyId)
        {   
            string isChecked="";
            if (companyCheckedList != null)
            {
                if (companyCheckedList.Contains(companyId))
                    isChecked = "checked='checked'";
            }
            return isChecked;
        }
        #endregion
    }
    
    
    
}
