using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Generic;
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
    /// 页面功能：全部客户
    /// 开发人：xuty 开发时间：2010-07-07
    /// 修改人：徐从栎  2011-12-30
    /// 修改说明：单击公司名链接到外部网店，免费认证、签约入驻商户、收费商户可看到手机号（信息来源：楼）
    /// </summary>
    public partial class AllCustomers : BackPage
    {
        protected int pageSize = 15;
        protected int pageIndex = 1;
        protected int recordCount;
        EyouSoft.IBLL.CompanyStructure.IMyCustomer myCustomerBll;
        EyouSoft.IBLL.SystemStructure.ISysCity cityBll;
        EyouSoft.IBLL.SystemStructure.ISysProvince provinceBll;
        EyouSoft.IBLL.CompanyStructure.ICompanyArea companyAreaBll;
        protected bool haveUpdate = true;
        private CompanyLev SiteUserComLev = CompanyLev.注册商户;
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Utils.GetQueryStringValue("method");
            if (!CheckGrant(TravelPermission.营销工具_同业名录))
            {
                Utils.ResponseNoPermit();
                return;
            }
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (companyModel != null)
            {
                SiteUserComLev = companyModel.CompanyLev;
            }

            //是否开通收费MQ
            //if (!companyModel.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ))
            //{
            //    Server.Transfer("/SystemSet/ApplyMQ.aspx?iscustomer=yes&" + StringValidate.BuildUrlString(Request.QueryString, new string[] { }), false);
            //    return;
            //}
            if (method == "setMyCustomer")//设置为我的客户
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起,你没有改权限!");
                    return;
                }
                myCustomerBll = EyouSoft.BLL.CompanyStructure.MyCustomer.CreateInstance();
                SetMyCustomer();
                myCustomerBll = null;
                return;
            }

            //获取查询条件
            int province = Utils.GetInt(Utils.InputText(Server.UrlDecode(Request.QueryString["province"] ?? "")));//省份ID
            int city = Utils.GetInt(Utils.InputText(Server.UrlDecode(Request.QueryString["city"] ?? "")));//城市ID
            string companyName = Utils.InputText(Server.UrlDecode(Request.QueryString["companyname"] ?? ""));//公司名
            string admin = Utils.InputText(Server.UrlDecode(Request.QueryString["admin"] ?? ""));//负责人
            string brand = Utils.InputText(Server.UrlDecode(Request.QueryString["brand"] ?? ""));//品牌
            EyouSoft.Model.CompanyStructure.QueryParamsCompany query = new EyouSoft.Model.CompanyStructure.QueryParamsCompany();
            query.CityId = city;
            query.PorvinceId = province;
            query.CompanyBrand = brand;
            query.CompanyName = companyName;
            query.ContactName = admin;
            //当前页码
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //获取我的客户
            cityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            provinceBll = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance();
            companyAreaBll = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> allCustomerList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListTravelAgency(query, pageSize, pageIndex, ref recordCount);
            if (allCustomerList != null && allCustomerList.Count > 0)
            {
                alc_rpt_customers.DataSource = allCustomerList;
                alc_rpt_customers.DataBind();
                BindPage(province, city, companyName, admin, brand);//设置分页
            }
            else
            {
                alc_rpt_customers.EmptyText = "<tr><td style='text-align:center'>暂无客户信息</td></tr>";
                this.ExportPageInfo1.Visible = false;
            }
            //清理对象
            allCustomerList = null;
            cityBll = null;
            provinceBll = null;

            //恢复查询条件
            ac_pc.SetProvinceId = province;
            ac_pc.SetCityId = city;
            ac_txtAdmin.Value = admin;
            ac_txtBrand.Value = brand;
            ac_txtCompanyName.Value = companyName;

          
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

        #region 设置为我的客户
        protected void SetMyCustomer()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string ids = Utils.GetQueryStringValue("ids");
            string[] idArray = ids.Split(',');

            if (myCustomerBll.SetMyCustomer(SiteUserInfo.ID, idArray))
            {
                Utils.ResponseMeg(true, "设置成功!");
            }
            else
            {
                Utils.ResponseMeg(false, "设置失败!");
            }
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

        #region 获取线路区域
        protected string GetComapnyAreaList(string companyId)
        {
            StringBuilder areaBuilder = new StringBuilder();
            IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = companyAreaBll.GetCompanyArea(companyId);
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
                EyouSoft.Model.CompanyStructure.CompanyInfo companyModel = e.Item.DataItem as EyouSoft.Model.CompanyStructure.CompanyInfo;
                if (companyModel != null)
                {
                    Literal ltr = e.Item.FindControl("ltrAreaName") as Literal;//绑定线路区域
                    string areaNameList = "";
                    if (ltr != null)
                    {
                        areaNameList = GetComapnyAreaList(companyModel.ID);
                        ltr.Text = string.Format("<span title='{1}' style='cursor:pointer'>{0}</span>", Utils.GetText(areaNameList, 10, true), areaNameList);
                    }
                    Literal ltr1 = e.Item.FindControl("ltrLookTour") as Literal;//绑定产看产品(如果有线路区域则查看产品)
                    if (areaNameList != "暂无" && areaNameList != "")
                    {
                        ltr1.Text = GetLookTour(companyModel.ID);
                    }


                }
            }
        }
        #endregion

        protected string GetMoible(object moibleobj)
        {
            string str = Convert.ToString(moibleobj).Trim();


            if (this.SiteUserComLev == CompanyLev.注册商户 || this.SiteUserComLev == CompanyLev.审核商户)
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
