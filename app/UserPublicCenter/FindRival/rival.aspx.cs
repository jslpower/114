using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.URLREWRITE;
using EyouSoft.Common.Function;
using EyouSoft.Model.CompanyStructure;
namespace UserPublicCenter.FindRival
{
    /// <summary>
    /// 陈蝉鸣 
    /// 2011-5-12
    /// 寻找同同行列表
    /// 
    /// 修改时间：2011-12-27
    /// 修改人：刘飞 
    /// </summary>

    public partial class rival : EyouSoft.Common.Control.FrontPage
    {
        protected int recordCount;//信息总条数
        protected int pageIndex = 1;//当前页码
        protected int pageSize = 20; //分页数量
        protected string provinceName;//所显示的省份
        protected string companyName;//搜索的公司名字
        protected int provinceID;//省份ID
        protected int cityID, areaID;
        protected string companyType = string.Empty;//公司类型
        protected int companytypeid;
        protected bool IsState = false;//是否显示公司状态
        protected bool IslevImg = false;//是否显示公司等级图标

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.ddl_ProvinceList.ClientID + "')");
            //绑定省份
            BindProvince();
            //绑定单位类型
            GetCompanyType();
            if (!IsPostBack)
            {
                (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 1;//设置导航条
                Initialize();
                this.Title = provinceName + "旅行社大全_" + provinceName + "地接社_" + provinceName + "组团社_" + provinceName + "专线_旅游企业大全_同业114同行频道";
            }
        }

        //初始化页面
        public void Initialize()
        {
            provinceID = Utils.GetInt(Utils.GetQueryStringValue("provinceID"), 0);
            cityID = Utils.GetInt(Utils.GetQueryStringValue("cId"), 0);
            areaID = Utils.GetInt(Utils.GetQueryStringValue("areaID"), 0);
            companytypeid = Utils.GetInt(Utils.GetQueryStringValue("companytype"));
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            companyName = Utils.GetQueryStringValue("CompanyName").Trim();
            companyType = Utils.GetQueryStringValue("companytype");
            if (provinceID <= 0 && cityID > 0)
            {
                EyouSoft.IBLL.SystemStructure.ISysCity Bcity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
                EyouSoft.Model.SystemStructure.SysCity Mcity = new EyouSoft.Model.SystemStructure.SysCity();
                Mcity = Bcity.GetSysCityModel(cityID);
                provinceID = Mcity.ProvinceId;
            }
            //获取省份列表
            EyouSoft.IBLL.SystemStructure.ISysProvince Bpro = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProList = Bpro.GetProvinceList();
            this.province.DataSource = ProList;
            this.province.DataBind();
            //获取同行列表
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo Bcompany = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.CompanyTongHang> ComList = null;
            ComList = Bcompany.GetTongHangList(pageSize, pageIndex, ref recordCount,
                                               new EyouSoft.Model.CompanyStructure.QueryNewCompany()
                                                   {
                                                       PorvinceId = provinceID,
                                                       KeyWord = companyName,
                                                       CityId = cityID,
                                                       CountyId = areaID,
                                                       CompanyTypes =
                                                           (companytypeid == 0)
                                                               ? null
                                                               : new CompanyType?[]
                                                                     {
                                                                         (EyouSoft.Model.CompanyStructure.CompanyType)
                                                                         companytypeid
                                                                     }
                                                   });

            //获取省份名字
            if (provinceID > 0)
            {
                provinceName = Bpro.GetProvinceModel(provinceID).ProvinceName;
            }
            else
            {
                provinceName = "全部";
            }
            Bcompany = null;
            if (ComList != null && ComList.Count > 0)
            {
                this.rivalList.DataSource = ComList;
                this.rivalList.DataBind();
                BindPage();
            }
            else
            {
                this.rivalList.EmptyText = "搜索结果为空，请尝试修改搜索条件重试！";
                this.pageInfor.Visible = false;
            }
        }

        /// <summary>
        /// 绑定省份
        /// </summary>
        private void BindProvince()
        {
            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "','" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value)"));
            this.ddl_CityList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value,'" + this.ddl_ProvinceList.ClientID + "')"));
            this.ddl_CountyList.Items.Insert(0, new ListItem("请选择", "0"));
        }


        //绑定分页控件
        private void BindPage()
        {
            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.pageInfor.IsUrlRewrite = true;
                this.pageInfor.Placeholder = "#PageIndex#";
                this.pageInfor.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_p#PageIndex#";
            }
            else
            {
                this.pageInfor.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.pageInfor.UrlParams = Request.QueryString;
            }
            this.pageInfor.intPageSize = pageSize;
            this.pageInfor.CurrencyPage = pageIndex;
            this.pageInfor.intRecordCount = recordCount;
        }

        /// <summary>
        /// 判断QQ,MQ
        /// </summary>
        /// <param name="Mcom">传入的联系人信息对象</param>
        /// <param name="type">判断是QQ还是MQ</param>
        /// <returns></returns>
        protected string Judge(EyouSoft.Model.CompanyStructure.ContactPersonInfo Mcom, string type)
        {
            string str = "";
            if (Mcom != null)
            {
                switch (type)
                {
                    case "QQ":

                        if (Mcom.QQ != null && Mcom.QQ.Length > 0)
                        {
                            string[] arrqq = Mcom.QQ.Split(',');
                            string strqq = Utils.GetNewQQ(arrqq[0]);
                            return strqq;
                        }
                        break;
                    case "MQ":

                        if (Mcom.MQ != null && Mcom.MQ.Length > 0)
                        {
                            return Utils.GetMQ(Mcom.MQ);
                        }

                        break;
                }

            }
            return str;
        }

        /// <summary>
        /// 获取公司类型
        /// </summary>
        private void GetCompanyType()
        {
            List<EnumObj> lst = EnumObj.GetList(typeof(EyouSoft.Model.CompanyStructure.CompanyType));
            if (lst != null && lst.Any())
            {
                ddl_CompanyTypeList.DataSource =
                    lst.Where(
                        item =>
                        (item.Text == "专线" || item.Text == "组团" || item.Text == "地接" || item.Text == "景区" ||
                         item.Text == "酒店")).ToList();
                ddl_CompanyTypeList.DataTextField = "Text";
                ddl_CompanyTypeList.DataValueField = "Value";
                ddl_CompanyTypeList.DataBind();
            }

            ddl_CompanyTypeList.Items.Insert(0, new ListItem("公司类型", "0"));
        }

        /// <summary>
        /// 获取公司Logo路径
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        protected string GetCompanyLogoSrc(object obj)
        {
            CompanyAttachInfo CompanyAinfo = (CompanyAttachInfo)obj;
            if (CompanyAinfo != null && !string.IsNullOrEmpty(CompanyAinfo.CompanyLogo.ImagePath))
            {
                return Utils.GetNewImgUrl(CompanyAinfo.CompanyLogo.ImagePath, 2);
            }
            else
            {
                return Utils.GetNewImgUrl("", 2);
            }
        }

        /// <summary>
        /// 获取是否显示侧边推荐
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        protected string GetIsState(string CompanyId)
        {
            string StateImgSrc = string.Empty;
            CompanyInfo info = new CompanyInfo();
            info = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            if (info.B2BDisplay == CompanyB2BDisplay.侧边推荐)
            {
                StateImgSrc = "<img src=\"" + Domain.ServerComponents + "/images/new2011/tuijian.gif\" title=\"推荐\"/>";
            }
            return StateImgSrc;
        }
        /// <summary>
        /// 获取公司类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetCompanyType(object o)
        {
            CompanyRole role = (CompanyRole)o;
            string companytype = string.Empty;
            for (int i = 0; i < role.RoleItems.Length; i++)
            {
                companytype += role.RoleItems[i].ToString() + "、";
            }
            return companytype.Remove(companytype.Length - 1);
        }
    }
}
