using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
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
using EyouSoft.Common.Function;
using EyouSoft.Model.CompanyStructure;
namespace UserBackCenter.CustomerManage
{
    /// <summary>
    /// 页面功能：我的客户
    /// 开发人：xuty 开发时间：2010-07-07
    /// 修改人：徐从栎  2011-12-29
    /// 说明：添加"查看公司详情"
    /// </summary>
    public partial class MyCustomers : BackPage
    {
        protected int pageSize = 15;
        protected int pageIndex = 1;
        protected int recordCount;
        EyouSoft.IBLL.CompanyStructure.IMyCustomer myCustomerBll;
        EyouSoft.IBLL.TourStructure.ITourOrder tourOrderBLL;
        EyouSoft.IBLL.SystemStructure.ISysCity cityBll;
        EyouSoft.IBLL.SystemStructure.ISysProvince provinceBll;
        protected bool haveUpdate = true;
        protected bool IsCheck = true;
        private CompanyLev SiteUserComLev = CompanyLev.注册商户;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_同业名录))
            {
                Utils.ResponseNoPermit();
                return;
            }
            string method = Utils.GetQueryStringValue("method");

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (companyModel != null)
            {
                SiteUserComLev = companyModel.CompanyLev;
            }
            myCustomerBll = EyouSoft.BLL.CompanyStructure.MyCustomer.CreateInstance();
            tourOrderBLL = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            cityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            provinceBll = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance();
            if (method == "clearMyCustomers")
            {
                if (!haveUpdate)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>alert('对不起，你没有该权限!');</script>");
                }
                else
                {
                    ClearMyCustomers();//解除合作关系
                }
            }
            //获取当前页
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //获取查询条件
            int province = Utils.GetInt(Utils.InputText(Server.UrlDecode(Request.QueryString["province"] ?? "")));//省份ID
            int city = Utils.GetInt(Utils.InputText(Server.UrlDecode(Request.QueryString["city"] ?? "")));//城市ID
            string companyName = Utils.InputText(Server.UrlDecode(Request.QueryString["companyname"] ?? ""));//公司名
            string admin = Utils.InputText(Server.UrlDecode(Request.QueryString["admin"] ?? ""));//负责人
            string brand = Utils.InputText(Server.UrlDecode(Request.QueryString["brand"] ?? ""));//品牌
            string myCompanyId = SiteUserInfo.CompanyID;
            EyouSoft.Model.CompanyStructure.QueryParamsCompany query = new EyouSoft.Model.CompanyStructure.QueryParamsCompany();
            query.CityId = city;
            query.PorvinceId = province;
            query.CompanyBrand = brand;
            query.CompanyName = companyName;
            query.ContactName = admin;
            //绑定我的客户
            IList<EyouSoft.Model.CompanyStructure.MyCustomer> myCustomerList = myCustomerBll.GetList(myCompanyId, query, pageSize, pageIndex, ref recordCount);
            if (myCustomerList != null && myCustomerList.Count > 0)
            {
                alc_rpt_customers.DataSource = myCustomerList;
                alc_rpt_customers.DataBind();
                BindPage(province, city, companyName, admin, brand);
            }
            else
            {
                alc_rpt_customers.EmptyText = "<tr><td style='text-align:center'>暂无客户信息</td></tr>";
                this.ExportPageInfo1.Visible = false;
            }

            //清除对象
            myCustomerList = null;
            myCustomerBll = null;
            cityBll = null;
            provinceBll = null;
            tourOrderBLL = null;
            //恢复查询条件
            mc_pc.SetProvinceId = province;
            mc_pc.SetCityId = city;
            mc_txtAdmin.Value = admin;
            mc_txtBrand.Value = brand;
            mc_txtCompanyName.Value = companyName;
        }

        #region 设置分页
        protected void BindPage(int province, int city, string companyName, string admin, string brand)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("province", province.ToString());
            this.ExportPageInfo1.UrlParams.Add("city", city.ToString());
            this.ExportPageInfo1.UrlParams.Add("companyname", companyName);
            this.ExportPageInfo1.UrlParams.Add("admin", admin);
            this.ExportPageInfo1.UrlParams.Add("brand", brand);
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion

        #region 解除合作关系
        protected void ClearMyCustomers()
        {
            if (!IsCompanyCheck)
            {
                MessageBox.Show(this, "对不起,你尚未审核通过!");
                return;
            }
            string ids = Utils.GetQueryStringValue("ids");
            string[] idArray = ids.Split(',');
            if (myCustomerBll.CancelMyCustomer(idArray))
            {
                MessageBox.Show(this, "解除成功!");
            }
            else
            {
                MessageBox.Show(this, "操作失败!");
            }
        }
        #endregion

        #region 获取交易记录次数
        protected int GetRecordNum(string Id)
        {
            if (SiteUserInfo.CompanyRole.Length == 1)
                return tourOrderBLL.GetRetailersOrderNumByState(SiteUserInfo.CompanyID, Id, SiteUserInfo.CompanyRole.RoleItems[0], EyouSoft.Model.TourStructure.OrderState.已成交);
            else
                return tourOrderBLL.GetRetailersOrderNumByState(SiteUserInfo.CompanyID, Id, null, EyouSoft.Model.TourStructure.OrderState.已成交);
        }
        #endregion

        #region 获取省份城市
        protected string GetProAndCity(int pId, int cId)
        {
            string strCity = "";
            string strPro = "";
            EyouSoft.Model.SystemStructure.SysCity city = cityBll.GetSysCityModel(cId);
            EyouSoft.Model.SystemStructure.SysProvince province = provinceBll.GetProvinceModel(pId);
            if (city != null)
                strCity = city.CityName;
            if (province != null)
                strPro = province.ProvinceName;
            return strPro + "-" + strCity;

        }
        #endregion

        #region 获取公司线路区域
        protected string GetComapnyAreaList(string companyId)
        {
            StringBuilder areaBuilder = new StringBuilder();
            IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(companyId);
            if (areaList != null && areaList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.AreaBase area in areaList)
                {
                    areaBuilder.Append(area.AreaName + "，");
                }
            }
            else
            {
                areaBuilder.Append("暂无");
            }
            areaList = null;
            return areaBuilder.ToString().TrimEnd('，');
        }
        #endregion

        #region 查看产品
        protected string GetLookTour(string companyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<a href=\"javascript:void(0)\" onclick=\"MyCustomers.OpenDialog('" + companyId + "');return false;\">查看产品</a>");
            return builder.ToString();
        }
        #endregion

        #region 创建客户Item
        protected void rpt_customers_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.CompanyStructure.MyCustomer customModel = e.Item.DataItem as EyouSoft.Model.CompanyStructure.MyCustomer;
                if (customModel != null)
                {
                    Literal ltr = e.Item.FindControl("ltrAreaName") as Literal;//绑定线路区域
                    string areaNameList = "";
                    if (ltr != null)
                    {
                        areaNameList = GetComapnyAreaList(customModel.CustomerCompanyID);
                        ltr.Text = string.Format("<span title='{1}' style='cursor:pointer'>{0}</span>", Utils.GetText(areaNameList, 10, true), areaNameList);
                    }
                    Literal ltr1 = e.Item.FindControl("ltrLookTour") as Literal;//绑定产看产品(如果有线路区域则查看产品)
                    if (areaNameList != "暂无" && areaNameList != "")
                    {
                        ltr1.Text = GetLookTour(customModel.CustomerCompanyID);
                    }
                }
            }
        }
        #endregion

        protected string GetMoible(object moibleobj)
        {
            string str = Convert.ToString(moibleobj).Trim();

            if (SiteUserComLev == CompanyLev.注册商户 || SiteUserComLev == CompanyLev.审核商户)
            {
                return Utils.GetEncryptMobile(str, true);
            }
            return str;
        }
        /// <summary>
        /// 返回公司网店URL链接
        /// </summary>
        /// <param name="cmpName">公司名</param>
        /// <param name="cmpID">公司ID</param>
        /// <returns></returns>
        protected string getShopUrl(object cmpName, object cmpID)
        {
            string strUrl = string.Empty;
            string strCmpName = Convert.ToString(cmpName);
            string strCmpID = Convert.ToString(cmpID);
            if (!string.IsNullOrEmpty(strCmpName) && !string.IsNullOrEmpty(strCmpID))
            {
                //判断是否开通了高级网店
                if (Utils.IsOpenHighShop(strCmpID))//开通了高级网店
                {
                    strUrl = Domain.SeniorOnlineShop + "/seniorshop/default.aspx?cid=" + strCmpID;
                }
                else//没有开通
                {
                    strUrl = Domain.UserPublicCenter + "/shop_" + strCmpID;
                }
            }
            return string.Format("<a href=\"{0}\" target='_blank'>{1}</a>", strUrl, strCmpName);
        }

    }
}
