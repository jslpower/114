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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.ScenicManage
{
    /// <summary>
    /// 景区管理：景区列表
    /// 功能：查询，删除，排序，新增，修改
    /// 创建人：蔡永辉
    /// 创建时间： 2011-10-31  
    /// </summary>
    public partial class ScenicList : EyouSoft.Common.Control.YunYingPage
    {
        protected int currentPage = 0;
        protected string PagePath = "";
        protected int pageSize = 15;
        protected bool IsUpdateGant = false;
        protected bool IsDeleteGant = true;
        protected bool IsAddGant = false;
        protected int recordcount = 1;
        public int ManageUserName = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            #region 权限判断
            //查询
            //判断是否有【景区_景区管理栏目管理】权限，如果没有改权限，则不能查看改页面
            //if (!this.CheckMasterGrant(YuYingPermission.景区_景区管理栏目管理))
            //{
            //    Utils.ResponseNoPermit(YuYingPermission.景区_景区管理栏目管理, true);
            //    return;
            //}
            //IsUpdateGant = CheckMasterGrant(YuYingPermission.景区_景区管理栏目修改);
            //IsDeleteGant = CheckMasterGrant(YuYingPermission.景区_景区管理栏目删除);
            //IsAddGant = CheckMasterGrant(YuYingPermission.景区_景区管理栏目新增);
            #endregion
            ManageUserName=MasterUserInfo.ID;
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            if (!IsPostBack)
            {
                BindStatue();
                int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
                if (ProvinceId > 0)
                {
                    this.ProvinceAndCityAndCounty1.SetProvinceId = ProvinceId;
                    int CityId = Utils.GetInt(Request.QueryString["CityId"]);
                    if (CityId > 0)
                    {
                        this.ProvinceAndCityAndCounty1.SetCityId = CityId;
                        int CountyId = Utils.GetInt(Request.QueryString["CountyId"]);
                        if (CountyId > 0)
                        {
                            this.ProvinceAndCityAndCounty1.SetCountyId = CountyId;
                        }
                    }
                }
                string ScenicName = Utils.GetQueryStringValue("ScenicName");
                if (!string.IsNullOrEmpty(ScenicName))
                    txtScenicName.Value = ScenicName;
                int DpStatus = Utils.GetInt(Request.QueryString["Status"]);
                if (DpStatus > 0)
                    DdlStatus.SelectedValue = DpStatus.ToString();
                if (ProvinceId > 0 || ScenicName != "" || DpStatus > 0)
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "ScenicManage.OnSearch();");
            } InitData();

        }

        /// <summary>
        /// 景区列表数据初始化
        /// </summary>
        private void InitData()
        {
            //EyouSoft.Model.ScenicStructure.MSearchSceniceArea modelSceniceArea = new EyouSoft.Model.ScenicStructure.MSearchSceniceArea();
            //modelSceniceArea.ScenicName = Utils.GetQueryStringValue("ScenicName");
            //txtScenicName.Value = Utils.GetQueryStringValue("ScenicName");
            //if (Utils.GetInt(Utils.GetQueryStringValue("ProId")) > 0)
            //{
            //    modelSceniceArea.ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("ProId"));
            //}
            //if (Utils.GetInt(Utils.GetQueryStringValue("CityId")) > 0)
            //{
            //    modelSceniceArea.CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            //}
            //if (Utils.GetInt(Utils.GetQueryStringValue("CountyId")) > 0)
            //{
            //    modelSceniceArea.CountyId = Utils.GetInt(Utils.GetQueryStringValue("CountyId"));
            //}
            //if (Utils.GetInt(Utils.GetQueryStringValue("DdlSta")) > 0)
            //{
            //    modelSceniceArea.Status = (ExamineStatus)Utils.GetInt(Utils.GetQueryStringValue("DdlSta"));
            //    DdlStatus.SelectedValue = Utils.GetQueryStringValue("DdlSta");
            //}
            //System.Collections.Generic.IList<EyouSoft.Model.ScenicStructure.MScenicArea> listScenicArea = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(pageSize, currentPage, ref recordcount, modelSceniceArea);
            //if (listScenicArea.Count > 0)
            //{
            //    this.repList.DataSource = listScenicArea;
            //    this.repList.DataBind();
            //    BindFenYe();
            //}

            //if (modelSceniceArea.ProvinceId.HasValue)
            //    this.ProvinceAndCityAndCounty1.SetProvinceId = Convert.ToInt32(modelSceniceArea.ProvinceId);
            //if (modelSceniceArea.CityId.HasValue)
            //{
            //    if (modelSceniceArea.ProvinceId.HasValue)
            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('ProvinceAndCityAndCounty1_ddl_CityList','" + modelSceniceArea.ProvinceId + "', '" + modelSceniceArea.ProvinceId + "');", true);
            //}
            //if (modelSceniceArea.CountyId.HasValue)
            //    this.ProvinceAndCityAndCounty1.SetCountyId = Convert.ToInt32(modelSceniceArea.CountyId);
            //modelSceniceArea = null;

        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        private void BindFenYe()
        {
            //绑定分页控件
            //this.ExportPageInfo1.intPageSize = pageSize;//每页显示记录数
            //this.ExportPageInfo1.intRecordCount = recordcount;
            //this.ExportPageInfo1.CurrencyPage = currentPage;
            //this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
            //this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            //this.ExportPageInfo1.UrlParams = Request.QueryString;
            //this.ExportPageInfo1.LinkType = 3;
        }

        /// <summary>
        /// 查询状态绑定
        /// </summary>
        private void BindStatue()
        {
            this.DdlStatus.DataSource = EnumObj.GetList(typeof(ExamineStatus));
            this.DdlStatus.DataTextField = "Text";
            this.DdlStatus.DataValueField = "Value";
            this.DdlStatus.DataBind();
            this.DdlStatus.Items.Insert(0, new ListItem("请选择", "0"));

            //B2B显示绑定
            this.DdlB2B.DataSource = EnumObj.GetList(typeof(ScenicB2BDisplay));
            this.DdlB2B.DataTextField = "Text";
            this.DdlB2B.DataValueField = "Value";
            this.DdlB2B.DataBind();
            this.DdlB2B.Items.Insert(0, new ListItem("请选择", "请选择"));

            //B2C显示绑定
            this.DdlB2C.DataSource = EnumObj.GetList(typeof(ScenicB2CDisplay));
            this.DdlB2C.DataTextField = "Text";
            this.DdlB2C.DataValueField = "Value";
            this.DdlB2C.DataBind();
            this.DdlB2C.Items.Insert(0, new ListItem("请选择", "请选择"));

        }
        

        /// <summary>
        /// 获取公司的详细信息
        /// </summary>
        /// <returns></returns>
        public string GetCompanyInfo(object companyinfo, string type)
        {
            EyouSoft.Model.CompanyStructure.CompanyInfo modelcompany = (EyouSoft.Model.CompanyStructure.CompanyInfo)companyinfo;
            string result = "";
            if (type == "ContactInfo")
            {
                result = "<a href=" + "javascript:void(0)" + " onmouseout=" + "wsug(event,0)" + " onmouseover=" + "wsug(event,'联系人:" + modelcompany.ContactInfo.ContactName + "&lt;br/&gt;手机:" + modelcompany.ContactInfo.Mobile + "&lt;br/&gt;电话:" + modelcompany.ContactInfo.Tel + "&lt;br/&gt;传真:" + modelcompany.ContactInfo.Fax + "&lt;br/&gt;QQ:" + modelcompany.ContactInfo.QQ + "')" + ">"
                    + "联系人：" + modelcompany.ContactInfo.ContactName + "<br />"
                    + "联系电话：" + modelcompany.ContactInfo.Tel + "</a>";

            }
            else if (type == "CompanyName")
            {
                if (modelcompany.CompanyName != "")
                    result = modelcompany.CompanyName;
            }
            else if (type == "CompanyId")
            {
                if (modelcompany.ID != "")
                    result = modelcompany.ID;
            }
            return result;
        }


        /// <summary>
        /// 获取公司地址根据相应id
        /// </summary>
        /// <param name="provinceid">省id</param>
        /// <param name="cityid">市id</param>
        /// <param name="countryid">县id</param>
        /// <returns></returns>
        public string GetCompanyAddress(string provinceid, string cityid, string countryid)
        {
            string result = string.Empty;
            int proid = Convert.ToInt32(provinceid);
            int ctid = Convert.ToInt32(cityid);
            int cyid = Convert.ToInt32(countryid);
            EyouSoft.Model.SystemStructure.SysProvince proviceModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(proid);
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(ctid);
            EyouSoft.Model.SystemStructure.SysDistrictCounty countyModel = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetModel(cyid);
            if (proviceModel != null)
                result = proviceModel.ProvinceName + " ";
            if (cityModel != null)
                result += cityModel.CityName + " ";
            if (countyModel != null)
                result += countyModel.DistrictName;

            return result;
        }
    }
}
