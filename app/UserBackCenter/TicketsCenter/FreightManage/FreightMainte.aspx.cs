using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.TicketsCenter.FreightManage
{
    /// <summary>
    /// 运价维护 页面
    /// 创建人：戴银柱
    /// 创建时间： 2010-10-19  
    /// </summary>
    public partial class FreightMainte : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AirCompanyInit();

                StartAddressInit();

                FromAddressInit();

                this.FreightTop1.CompanyId = SiteUserInfo.CompanyID;
            }
        }


        /// <summary>
        /// 初始化所有航空公司
        /// </summary>
        /// <param name="selectVal">设置选中项，为空则不设置</param>
        protected void AirCompanyInit()
        {

            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> list = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompanyList(null);
            this.mai_AirCompany.Items.Clear();
            this.mai_AirCompany.Items.Add(new ListItem("--所有--", "0"));
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].Id.ToString();
                    item.Text = list[i].AirportCode + "-" + list[i].AirportName;
                    this.mai_AirCompany.Items.Add(item);
                }
            }
            list = null;
        }

        /// <summary>
        /// 初始化起始地
        /// </summary>
        /// <param name="selectVal"></param>
        protected void StartAddressInit()
        {

            IList<EyouSoft.Model.TicketStructure.TicketSeattle> list = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattle(null);
            this.mai_DdlBegin.Items.Clear();
            this.mai_DdlBegin.Items.Add(new ListItem("--所有--", "0"));
            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(p => p.ShortName).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].SeattleId.ToString();
                    item.Text = list[i].ShortName.Substring(0, 1).ToUpper() + "-" + list[i].Seattle;
                    this.mai_DdlBegin.Items.Add(item);
                }
            }
            list = null;
        }

        /// <summary>
        /// 初始化目的地
        /// </summary>
        protected void FromAddressInit()
        {
            IList<EyouSoft.Model.TicketStructure.TicketSeattle> list = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattle(null);
            this.mai_DdlEnd.Items.Clear();
            this.mai_DdlEnd.Items.Add(new ListItem("--所有--", "0"));
            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(p => p.ShortName).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].SeattleId.ToString();
                    item.Text = list[i].ShortName.Substring(0, 1).ToUpper() + "-" + list[i].Seattle;
                    this.mai_DdlEnd.Items.Add(item);
                }
            }
            list = null;

        }
    }

}
