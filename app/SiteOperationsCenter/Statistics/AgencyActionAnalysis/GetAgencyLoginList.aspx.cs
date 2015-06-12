using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.Statistics.AgencyActionAnalysis
{
    /// <summary>
    /// 功能:获的零售商登录列表
    /// 开发人:刘玉灵   时间：2010-9-17
    /// </summary>
    public partial class GetAgencyLoginList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int PageSize = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        private int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int Count = 0;
        /// <summary>
        /// 是否为查看所有
        /// 默认查看top10
        /// </summary>
        private bool isLookAll = false;

        protected int? visitcount_OrderIndex = null;
        protected int? logincount_OrderIndex = null;
        protected int currentOrderIndex =1;


        /// <summary>
        /// 类型名称
        /// </summary>
        protected string strOrderTypeName = "所有";
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限控制
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_组团社行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, false);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["LookAll"]))
            {
                isLookAll = true;
                PageSize = 20;
                if (Utils.GetInt(Request.QueryString["Page"]) > 1)
                {
                    PageIndex = Utils.GetInt(Request.QueryString["Page"]);
                }
            }
            if (!Page.IsPostBack)
            {
                this.BindLoginList();
            }
        }

        /// <summary>
        /// 获的批发商预定列表
        /// </summary>
        private void BindLoginList()
        {
            InitOrderIndex();

            int recordCount = 0;
            string CompanyName = Server.UrlDecode(Request.QueryString["CompanyName"]);
            DateTime? StartDate = Utils.GetDateTimeNullable(Request.QueryString["StartDate"]);
            DateTime? EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);

            IList<EyouSoft.Model.TourStructure.RetailLoginStatistics> LoginList = new List<EyouSoft.Model.TourStructure.RetailLoginStatistics>();
            if (!isLookAll)
            {
                LoginList = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetRetailLoginStatistics(PageSize, CompanyName, MasterUserInfo.AreaId, StartDate, EndDate, 1);
                if (LoginList != null && LoginList.Count > 0)
                {
                    this.rep_CompanyLoginList.DataSource = LoginList;
                    this.rep_CompanyLoginList.DataBind();
                }
                else
                {
                    this.rep_CompanyLoginList.EmptyText = "<tr><td height='100px' align='center' colspan='4'>暂无登录记录</td></tr>";
                }
            }
            else
            {
                LoginList = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetRetailLoginStatistics(PageSize, PageIndex, ref recordCount, currentOrderIndex, CompanyName, MasterUserInfo.AreaId, StartDate, EndDate);
                if (recordCount > 0)
                {
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = recordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "AllLoginList.LoadData(this);", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "AllLoginList.LoadData(this);", 0);
                    this.rep_CompanyLoginList.DataSource = LoginList;
                    this.rep_CompanyLoginList.DataBind();

                    this.tbExporPage.Visible = true;
                }
                else
                {
                    this.rep_CompanyLoginList.EmptyText = "<tr><td height='100px' align='center'  colspan='4'>暂无登录记录</td></tr>";
                    this.tbExporPage.Visible = false;
                }
            }
            LoginList = null;
        }
        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            if (isLookAll)
            {
                return ++Count + (PageIndex - 1) * PageSize;
            }
            else
            {
                return ++Count;
            }
        }

        /// <summary>
        /// 初始化排序参数
        /// </summary>
        protected void InitOrderIndex()
        {
            visitcount_OrderIndex = Utils.GetIntNull(Request.QueryString["visitcountorder"]);
            logincount_OrderIndex = Utils.GetIntNull(Request.QueryString["logincountorder"]);
            if (visitcount_OrderIndex.HasValue)
            {
                if (visitcount_OrderIndex.Value != 2 && visitcount_OrderIndex.Value != 3)
                {
                    visitcount_OrderIndex = null;
                }
            }
            if (logincount_OrderIndex.HasValue)
            {
                if (logincount_OrderIndex.Value != 0 && logincount_OrderIndex.Value != 1)
                {
                    logincount_OrderIndex = null;
                }
            }

            if (logincount_OrderIndex.HasValue)
            {
                currentOrderIndex = logincount_OrderIndex.Value;
            }
            else if (visitcount_OrderIndex.HasValue)
            {
                currentOrderIndex = visitcount_OrderIndex.Value;
            }
            else
            {
                currentOrderIndex = 1;
            }
        }

        protected string GetTableHead(int index)
        {
            string head = string.Empty;
            switch (index)
            {
                case 1:
                    if (!isLookAll)
                    {
                        head = "<strong>查看对象</strong>";
                    }
                    else
                    {
                        if (visitcount_OrderIndex.HasValue && visitcount_OrderIndex.Value == currentOrderIndex)
                        {
                            if (currentOrderIndex == 2)
                            {
                                head = "<a class='DataListUpGreen' onclick='AllLoginList.UpdateOrderIndex(\"visitcountorder\",\"3\");return false;' title='点击按照访问数量从高到低排序' href='#'>查看对象</a>";
                            }
                            else
                            {
                                head = "<a class='DataListDownGreen' onclick='AllLoginList.UpdateOrderIndex(\"visitcountorder\",\"2\");return false;' title='点击按照访问数量从低到高排序' href='#'>查看对象</a>";
                            }
                        }
                        else
                        {
                            head = "<a class='DataListDownGray' onclick='AllLoginList.UpdateOrderIndex(\"visitcountorder\",\"3\");return false;'  title='点击按照访问数量从高到低排序' href='#'>查看对象</a>";
                        }
                    }
                    break;
                case 2:
                    if (!isLookAll)
                    {
                        head = "<strong>登录次数</strong>";
                    }
                    else
                    {
                        if (logincount_OrderIndex.HasValue && logincount_OrderIndex.Value == currentOrderIndex)
                        {
                            if (currentOrderIndex == 0)
                            {
                                head = "<a class='DataListUpGreen' onclick='AllLoginList.UpdateOrderIndex(\"logincountorder\",\"1\");return false;' title='点击按照登录次数从高到低排序' href='#'>登录次数</a>";
                            }
                            else
                            {
                                head = "<a class='DataListDownGreen' onclick='AllLoginList.UpdateOrderIndex(\"logincountorder\",\"0\");return false;'  title='点击按照登录次数从低到高排序' href='#'>登录次数</a>";
                            }
                        }
                        else
                        {
                            head = "<a class='DataListDownGray' onclick='AllLoginList.UpdateOrderIndex(\"logincountorder\",\"1\");return false;' title='点击按照登录次数从高到低排序' href='#'>登录次数</a>";
                        }
                    }
                    break;
            }
            return head;
        }
    }
}
