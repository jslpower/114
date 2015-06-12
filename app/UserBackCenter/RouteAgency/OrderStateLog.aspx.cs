using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace UserBackCenter.RouteAgency
{
    public partial class OrderStateLog : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        private void DataInit()
        {
            EyouSoft.IBLL.NewTourStructure.ITourOrder bll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();
            EyouSoft.Model.NewTourStructure.MOrderHandleLogSearch searchModel = new EyouSoft.Model.NewTourStructure.MOrderHandleLogSearch();
            //searchModel.CompanyId = SiteUserInfo.CompanyID;
            searchModel.OrderId = Utils.GetQueryStringValue("orderID");
            IList<EyouSoft.Model.NewTourStructure.MOrderHandleLog> list = bll.GetOrderHandleLogLst(searchModel);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
    }
}
