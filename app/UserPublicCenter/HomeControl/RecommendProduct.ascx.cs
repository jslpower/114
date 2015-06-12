using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HomeControl
{
    public partial class RecommendProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //绑定最新线路
            int recordCount = 1;
            IList<EyouSoft.Model.NewTourStructure.MRoute> TourList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetNewRouteList(6);
            if (TourList != null && TourList.Count > 0)
            {
                recordCount = TourList.Count;
                rptProduct.DataSource = TourList;
                rptProduct.DataBind();
            }
            else
            {
                this.emptydata.Visible = true;
            }
        }
        public int CityId;
        public bool IsShow;
       
    }
}