using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.FeedbackManage
{
    /// <summary>
    ///页面功能：运营后台---客户反馈--- 网络营销反馈列表
    ///CreateDate:2011-05-26
    /// </summary>
    /// Author:liuym
    public partial class NetWorkMarketingList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        /// <summary>
        /// 省份编号
        /// </summary>
        protected string ProvinceId = string.Empty;
        /// <summary>
        /// 城市编号
        /// </summary>
        protected string CityId = string.Empty;
        /// <summary>
        /// 单位名称
        /// </summary>
        protected string CompanyName = string.Empty;
        /// <summary>
        /// 申请开始时间
        /// </summary>
        protected DateTime? StartTime;
        /// <summary>
        /// 申请结束时间
        /// </summary>
        protected DateTime? EndTime;
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int RecordCount = 0;
        /// <summary>
        /// 申请类型
        /// </summary>
        protected string TypeId = string.Empty;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.网络营销反馈_管理栏目))
                {
                    Utils.ResponseNoPermit(YuYingPermission.网络营销反馈_管理栏目, true);
                    return;
                }
                //初始化类型
                BindType();
                //初始化列表
                InitNetWorkMarketList();
            }
        }
        #endregion

        #region 初始化网络营销统计列表
        private void InitNetWorkMarketList()
        {
            #region 获取查询参数
            //设置当前页
            PageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            ProvinceId = Request.QueryString["ProvinceId"];
            CityId = Request.QueryString["CityId"];           
            StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("StartTime")) == null ? null : Utils.GetDateTimeNullable(Utils.GetQueryStringValue(("StartTime")));
            EndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndTime")) == null ? null : Utils.GetDateTimeNullable(Utils.GetQueryStringValue(("EndTime")));
            CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);
            TypeId = Request.QueryString["TypeId"];
            #endregion

            #region 初始化查询条件
            txtStartTime.Value = StartTime.HasValue ? StartTime.Value.ToString("yyyy-MM-dd") : "";
            txtEndTime.Value = EndTime.HasValue ? EndTime.Value.ToString("yyyy-MM-dd") : "";
            txtCompanyName.Value = CompanyName;
            if (!string.IsNullOrEmpty(ProvinceId))
            {
                this.ProvinceAndCityList1.SetProvinceId = int.Parse(ProvinceId);
            }
            if (!string.IsNullOrEmpty(CityId))
            {
                this.ProvinceAndCityList1.SetCityId = int.Parse(CityId);
            }
            int typeInt = 0;
            if (!string.IsNullOrEmpty(TypeId))
            {
                typeInt = int.Parse(TypeId);
                ddlType.SelectedValue = typeInt.ToString();
            }
            #endregion

            IList<EyouSoft.Model.CommunityStructure.MarketingCompany> list =
                EyouSoft.BLL.CommunityStructure.MarketingCompany.CreateInstance().GetMarketingCompany(PageSize, PageIndex, ref RecordCount, string.IsNullOrEmpty(ProvinceId) || ProvinceId == "0" ? new Nullable<int>() : int.Parse(ProvinceId), string.IsNullOrEmpty(CityId) || CityId == "0" ? new Nullable<int>() : int.Parse(CityId), string.IsNullOrEmpty(TypeId) ? new Nullable<EyouSoft.Model.CommunityStructure.MarketingCompanyType>() : (EyouSoft.Model.CommunityStructure.MarketingCompanyType)int.Parse(TypeId), CompanyName, StartTime, EndTime);

            if (list != null && list.Count > 0)
            {
                this.crp_NetWorkMarketList.DataSource = list;
                this.crp_NetWorkMarketList.DataBind();
                //绑定分页
                BindPage();
            }
            else
                this.crp_NetWorkMarketList.EmptyText = "<tr align='center'><td colspan='8' height='50px'>暂时没有数据！</td></tr>";
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = PageSize;
            this.ExportPageInfo1.CurrencyPage = PageIndex;
            this.ExportPageInfo1.intRecordCount = RecordCount;
        }
        #endregion

        #region 绑定申请类型
        private void BindType()
        {
            this.ddlType.Items.Add(new ListItem("请选择", ""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.MarketingCompanyType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                    this.ddlType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.MarketingCompanyType), str)).ToString()));
            }
            typeList = null;
        }
        #endregion
    }
}
