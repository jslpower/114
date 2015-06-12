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
using System.ComponentModel;

namespace UserBackCenter.usercontrol.SupplyInformation
{
    /// <summary>
    /// 供求信息Tab控件
    /// luofx 2010-8-3
    /// </summary>
    public partial class SupplyInfoTab : System.Web.UI.UserControl
    {
        protected string TabIndexOneClass = "zttooltitleun";
        protected string TabIndexTwoClass = "zttooltitleun";
        protected string TabIndexThreeClass = "zttooltitleun";
        [Bindable(true)]
        public int TabIndex
        {
            set
            {
                tabIndex = value;
                switch (tabIndex)
                {
                    case 0:
                        TabIndexOneClass = "zttooltitle";
                        break;
                    case 1:
                        TabIndexTwoClass = "zttooltitle";
                        break;
                    case 3:
                        TabIndexThreeClass = "zttooltitle";
                        break;
                }
            }
            get
            {
                return tabIndex;
            }
        }
        private int tabIndex;
        public int CityId = 0;
        protected string MoreSupplyInfoUrl =EyouSoft.Common.Domain.UserPublicCenter+ "/SupplierInfo/SupplierInfo.aspx?CityId=";
        protected void Page_Load(object sender, EventArgs e)
        {
                 
            MoreSupplyInfoUrl = MoreSupplyInfoUrl + CityId;         
        }
    }
}