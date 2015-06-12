using System;
using System.Collections;
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
using EyouSoft.Common.Function;
using System.Text;
using System.Collections.Generic;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——专线商查看列表页
    /// 开发人：杜桂云      开发时间：2010-07-1
    /// </summary>
    public partial class ShowCompany : EyouSoft.Common.Control.YunYingPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //初始化数据绑定
                BindCompanyList();
            }
        }
        #endregion

        #region 绑定列表数据源
        private void BindCompanyList()
        {
            //销售城市编号
            int SaleCityID = Utils.GetInt(Request.QueryString["EditSiteId"]);
            int AreaId = Utils.GetInt(Request.QueryString["AreaId"]);
            EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query = new EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase();
            query.AreaId =AreaId;
            query.CityId =SaleCityID;
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListCityAreaAdvRouteAgencyName(query); 
            if (CompanyList != null && CompanyList.Count > 0)
            {
                //绑定数据源
                this.CompanyList.DataSource = CompanyList;
                this.CompanyList.DataBind();
            }
            else
            {
                this.NoData.Visible = true;
            }
            //释放资源
            CompanyList = null;
        }
        #endregion

        #region 绑定公司信息
        protected string ShowCompanyName(int index, string CompanyName)
        {
            StringBuilder ReturnVal = new StringBuilder();
            ReturnVal.Append(string.Format("<td width=\"20%\">·{0}</td>", CompanyName));
            if (index != 1 && index % 5 == 0)
                ReturnVal.Append("</tr><tr class=\"lr_hangbg\">");
            return ReturnVal.ToString();
        }
        #endregion
    }
}
