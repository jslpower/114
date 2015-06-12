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
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：供应商列表数据绑定
    /// 开发人：杜桂云   开发时间：2010-08-24
    /// </summary>
    public partial class AjaxByRouteAgencyList : System.Web.UI.Page
    {
        #region 成员变量初始化
        private int CurrencyPage = 1;
        private int intPageSize = 40;
        protected int AreaId = 0;
        protected int RecordCount = 0;
        protected int cityID = 0;
        protected int selectCompanyCount = 0;
        protected IList<string> SiteCompanyId = null;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Utils.InputText(Request.QueryString["RouteAreaId"])) && !StringValidate.IsInteger(Utils.InputText(Request.QueryString["RouteAreaId"])))
            {
                MessageBox.ShowAndClose(this, "没有找到此线路区域信息！");
            }
            else
            {
                AreaId = Utils.GetInt(Request.QueryString["RouteAreaId"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CityID"]) && StringValidate.IsInteger(Request.QueryString["CityID"]))
            {
                cityID = Utils.GetInt(Request.QueryString["CityID"]);
            }
            //根据城市编号和线路区域编号获取已选择过的批发商列表
            SiteCompanyId = EyouSoft.BLL.SystemStructure.CompanyCityAd.CreateInstance().GetCompanyIdsByCityAndArea(cityID, AreaId);
            selectCompanyCount = SiteCompanyId.Count;
            if (!IsPostBack)
            {
                BindList();
            }
        }
        #endregion

        #region 显示公司名称
        protected string ShowCompanyName(int index, string Id, string Name)
        {
            string str = string.Format("<td width=\"25%\" align=\"left\"><input id=\"ck{0}\" name=\"CompanyId\"  type=\"checkbox\" IsDefault=\"false\" Text=\"{1}\" value=\"{0}\" />{1}</td>", Id, Name);
            if (SiteCompanyId != null)
            {
                foreach (string Cid in SiteCompanyId)
                {
                    if (Cid == Id)
                    {
                        str = string.Format("<td align=\"left\"><input id=\"ck{0}\" type=\"checkbox\" name=\"CompanyId\" checked='true'  IsDefault=\"true\" Text=\"{1}\"  value=\"{0}\" />{1}</td>", Id, Name);
                    }
                }
            }

            if (index != 1 && index % 3 == 0)
            {
                str += ("</tr><tr>");
            }
            return str;
        }

        #endregion


        #region 绑定数据源
        /// <summary>
        /// 绑定该线路区域下面的批发商
        /// </summary>
        protected void BindList()
        {
            //根据公司名称查询数据
            string CompanyName = Server.UrlDecode(Utils.InputText(Request.QueryString["kw"]));

            EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query = new EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase();
            query.AreaId = AreaId;
            query.CityId = 0;
            query.CompanyName = CompanyName;
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            //获取数据集
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> comList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListRouteAgency(query, intPageSize, CurrencyPage, ref RecordCount);
            if (comList != null && comList.Count > 0)
            {
                //绑定数据源
                this.CompanyList.DataSource = comList;
                this.CompanyList.DataBind();
                comList = null;
            }
            //释放资源
            comList = null;
        }
        #endregion
    }
}
