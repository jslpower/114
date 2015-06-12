using System;
using System.Collections;
using System.Collections.Generic;
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

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 高级网店到期审核列表 
    /// 创建时间：2010-9-19   袁惠
    /// </summary>
    public partial class AjaxEshopList : EyouSoft.Common.Control.YunYingPage
    {
        protected int intCurrentPage = 1;   //当前页数
        protected int intPageSize = 20;     //每页显示条数

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_高级网店到期统计))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_高级网店到期统计, true);
                return;
            } 
            int pageindex= Utils.GetInt(Request.QueryString["Page"]);
            if (pageindex> 1)
            {
                intCurrentPage =pageindex;
            }
            if (!IsPostBack)
            {
                BindShopList();//绑定列表
            }
        }

        //绑定控件
        private void BindShopList()
        {
            int recordCount = 0; 
            DateTime? startDate = null;
            DateTime? EndDate = null;
            int? provinceId = Utils.GetInt(Request.QueryString["ProvinceId"],0);
            provinceId = provinceId == 0 ? null : provinceId;
            int? cityId = Utils.GetInt(Request.QueryString["CityId"],0);
            cityId = cityId == 0 ? null : cityId;
            string companyName = Utils.InputText(Request.QueryString["CompanyName"]);
            string date = Request.QueryString["StartDate"];

            if (StringValidate.IsDateTime(date))
                startDate = Convert.ToDateTime(date);

            date = Request.QueryString["EndDate"];
            if (StringValidate.IsDateTime(date))
                EndDate = Convert.ToDateTime(date);

            IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> list =
EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetComingExpireApplys(intPageSize, intCurrentPage, ref recordCount, EyouSoft.Model.CompanyStructure.SysService.HighShop, provinceId, cityId, companyName, startDate, EndDate);
            if (list.Count > 0 && list != null)
            {
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = intCurrentPage;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "EshopDueDate.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "EshopDueDate.LoadData(this);", 0);
                crptEshopList.DataSource = list;
                crptEshopList.DataBind();
                list = null;
            }
            else
            {
                crptEshopList.EmptyText = "<tr><td colspan=\"11\"><div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无数据！</span></div></td></tr>";
                ExporPageInfoSelect1.Visible = false;               
            }

        }
    }
}
