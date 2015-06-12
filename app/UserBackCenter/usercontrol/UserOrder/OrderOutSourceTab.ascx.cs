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
namespace UserBackCenter.usercontrol.UserOrder
{
    /// <summary>
    /// 外发订单用户控件 
    /// </summary>
    public partial class OrderOutSourceTab : System.Web.UI.UserControl
    {
        protected string imagePath1 = string.Empty;
        protected string imagePath2 = string.Empty;
        public int TabIndex { 
            get; set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ImageServerPath = ImageManage.GetImagerServerUrl(1);
                if (TabIndex == 0)
                {
                    imagePath1 = ImageServerPath + "/images/barcj.gif";
                    imagePath2 = ImageServerPath + "/images/barcjun.gif";
                }
                else
                {
                    imagePath1 = ImageServerPath + "/images/barcjun.gif";
                    imagePath2 = ImageServerPath + "/images/barcj.gif";
                }
            }
        }
    }
}