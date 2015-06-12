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
    /// 页面功能：销售区域维护——专线上列表
    /// 开发人：杜桂云      开发时间：2010-06-28
    /// </summary>
    public partial class RouteAgencyList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int cityID = 0;
        protected int selectCompanyCount = 0;
        protected int AreaId = 0;
        protected IList<string> SiteCompanyId = null;
        protected bool EditFlag = false; //修改权限
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_城市管理 };
            EditFlag = CheckMasterGrant(parms);
            if (!EditFlag)
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_城市管理, true);
                return;
                //this.btnSave.Visible = false;
            }
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
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["DeletID"])))
            {
                EyouSoft.BLL.SystemStructure.CompanyCityAd.CreateInstance().DeleteCompanyCityAd(cityID, AreaId, Utils.InputText(Request.QueryString["DeletID"]));
                //return;
            }
            //根据城市编号和线路区域编号获取已选择过的批发商列表
            SiteCompanyId = EyouSoft.BLL.SystemStructure.CompanyCityAd.CreateInstance().GetCompanyIdsByCityAndArea(cityID, AreaId);
            selectCompanyCount = SiteCompanyId.Count;
            if (!IsPostBack)
            {
                BindSelectList();
            }
        }
        #endregion

        #region 绑定已经选择的批发商列表
        protected void BindSelectList()
        {
            //设置数据的查询条件
            EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase query = new EyouSoft.Model.CompanyStructure.QueryParamsCompanyBase();
            query.AreaId = AreaId;
            query.CityId = cityID;
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListCityAreaAdvRouteAgencyName(query);
            if (CompanyList != null && CompanyList.Count > 0 && CompanyList.Count < 7)
            {
                this.dalSelectList.DataSource = CompanyList;
                this.dalSelectList.DataBind();
            }
            //释放资源
            CompanyList = null;
        }
        #endregion

        #region 处理项操作事件
        /// <summary>
        ///修改和删除操作
        /// </summary>
        /// <returns></returns>
        public string CreateOperation(string ID)
        {
            if (EditFlag)
            {
                return string.Format("<a href='javascript:void(0)' onclick='DeleteAgency(\"{0}\")'>删除</a>", ID);
            }
            else
            {
                return "";
            }
        }
        #endregion


        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (EditFlag)
            {
                //获取用户选中的批发商
                string[] AllCompany = Utils.GetFormValues("hNewVal");

                if (AllCompany != null && AllCompany.Length > 0)
                {
                    IList<string> adList = new List<string>();
                    //EyouSoft.Model.SystemStructure.CompanyCityAd adModel = null;
                    foreach (string CompanyID in AllCompany)
                    {
                        adList.Add(CompanyID);
                    }
                    int reInt = EyouSoft.BLL.SystemStructure.CompanyCityAd.CreateInstance().AddCompanyCityAd(cityID, AreaId, adList);
                    if (reInt > 0)
                    {
                        MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
                    }
                    //释放资源
                    adList = null;
                }
                else
                {
                    MessageBox.ResponseScript(this.Page, "alert('请先选择批发商！');");
                }
            }
            else
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_城市管理, true);
                return;
            }
        }
        #endregion

        #region 搜索
        //protected void btn_Save_Click(object sender, EventArgs e)
        //{
        //    string CompanyName = this.txt_CompanyName.Text.Trim();
        //    Response.Redirect("RouteAgencyList.aspx?CompanyName=" + Server.UrlEncode(CompanyName) + "&RouteAreaId=" + AreaId.ToString() + "&CityID=" + cityID.ToString() + "&iframeId=" + Request.QueryString["iframeId"]);
        //}
        #endregion
    }
}
