using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.TicketsCenter.FreightManage
{
    /// <summary>
    /// 用于显示公司的当前购买运价数的列表控件
    /// </summary>
    public partial class FreightTop : System.Web.UI.UserControl
    {
        private string _companyId;

        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        protected void DataInit()
        {

            if (CompanyId != "")
            {
                //初始化当月信息
                DateTime dt = DateTime.Now;
                EyouSoft.Model.TicketStructure.PackBuyLogStatistics model = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().GetPackBuyLog(dt, CompanyId, EyouSoft.Model.TicketStructure.RateType.团队散拼);
                if (model != null)
                {
                    //常规可用数 - 已用数
                    this.frei_lblThisGeneral.Text = model.GeneralAvailableCount.ToString();
                    this.frei_lblThisGeneralUsed.Text = model.GeneralUsedCount.ToString();
                    //套餐可用数 - 已用数
                    this.frei_lblThisPackage.Text = model.PackageAvailableCount.ToString();
                    this.frei_lblThisPackageUsed.Text = model.PackageUsedCount.ToString();
                    //促销可用数 - 已用数
                    this.frei_lblThisPromotions.Text = model.SaleAvailableCount.ToString();
                    this.frei_lblThisPromotionsUsed.Text = model.SaleUsedCount.ToString();
                    //总数
                    this.frei_lblThisAll.Text = (model.GeneralAvailableCount + model.PackageAvailableCount + model.SaleAvailableCount + model.GeneralUsedCount + model.PackageUsedCount + model.SaleUsedCount).ToString();
                }

                //初始化次月信息
                int year = dt.Year;
                int month = dt.Month;
                if (month == 12)
                {
                    year = year + 1;
                    month = 1;
                }
                DateTime dtNext = new DateTime(year, month, 1);
                EyouSoft.Model.TicketStructure.PackBuyLogStatistics modelNext = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().GetPackBuyLog(dtNext, CompanyId, EyouSoft.Model.TicketStructure.RateType.团队散拼);
                //常规可用数 - 已用数
                this.frei_lblNextGeneral.Text = modelNext.GeneralAvailableCount.ToString();
                this.frei_lblNextGeneralUsed.Text = modelNext.GeneralUsedCount.ToString();
                //套餐可用数 - 已用数
                this.frei_lblNextPackage.Text = modelNext.PackageAvailableCount.ToString();
                this.frei_lblNextPackageUsed.Text = modelNext.PackageUsedCount.ToString();
                //促销可用数 - 已用数
                this.frei_lblNextPromotions.Text = modelNext.SaleAvailableCount.ToString();
                this.frei_lblNextPromotionsUsed.Text = modelNext.SaleUsedCount.ToString();
                //总数
                this.frei_lblNextAll.Text = (modelNext.GeneralAvailableCount + modelNext.PackageAvailableCount + modelNext.SaleAvailableCount + modelNext.GeneralUsedCount + modelNext.PackageUsedCount + modelNext.SaleUsedCount).ToString();
            }



        }
    }

}