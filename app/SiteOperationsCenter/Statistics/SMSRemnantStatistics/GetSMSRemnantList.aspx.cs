using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics.SMSRemnantStatistics
{
    /// <summary>
    /// 功能：获的短信条数小于100的单位列表
    /// 开发人:刘玉灵   时间：2010-9-17
    /// </summary>
    public partial class GetSMSRemnantList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        private int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_短信余额统计))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_短信余额统计, false);
            }
            if (Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                this.BindSmsRemnantList();
            }
        }
        /// <summary>
        /// 绑定短信少于100条的公司列表
        /// </summary>
        private void BindSmsRemnantList()
        {
            int recordCount=0;
            int ? ProvinceId=null;
            int ?CityId=null;
            string CompanyName=Server.UrlDecode(Request.QueryString["CompanyName"]);
            if(Utils.GetInt(Request.QueryString["ProvinceId"])>0)
            {
                ProvinceId=Utils.GetInt(Request.QueryString["ProvinceId"]);
            }
            if(Utils.GetInt(Request.QueryString["CityId"])>0)
            {
                CityId=Utils.GetInt(Request.QueryString["CityId"]);
            }
            IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> RemnantList = EyouSoft.BLL.SMSStructure.Account.CreateInstance().RemnantStats(PageSize, PageIndex, ref recordCount, ProvinceId, CityId, CompanyName);
            if (recordCount > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "SMSRemnantStatistics.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "SMSRemnantStatistics.LoadData(this);", 0);
                this.rep_SmsRemnatList.DataSource = RemnantList;
                this.rep_SmsRemnatList.DataBind();
            }
            else
            {
                this.rep_SmsRemnatList.EmptyText = "<table><tr><td height='100px' align='center'>暂无记录</td></tr></table>";
                this.tbExporPage.Visible = false;
            }
            RemnantList = null;
        }

        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }

        /// <summary>
        /// 获的省份-城市名称
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        protected string GetCityName(int ProvinceId, int CityId)
        {
            string strVal = "";

            IList<EyouSoft.Model.SystemStructure.SysCity> CityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(ProvinceId, 0, null, null, null);
            if (CityList != null)
            {
                CityList = (from c in CityList where c.CityId == CityId select c).ToList();
                if (CityList != null && CityList.Count > 0)
                {
                    strVal = CityList[0].ProvinceName + " " + CityList[0].CityName;
                }
            }
            CityList = null;

            return strVal;
        }

    }
}
