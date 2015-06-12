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
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.usercontrol
{
    /// <summary>
    /// 页面功能：起始时间菜单（设置和获取起始时间）
    /// 开发人：袁惠      开发时间：2010-09-17
    /// --用到的页面中需要引入js--

    public partial class StartAndEndDate : System.Web.UI.UserControl
    {
        public string SetTitle { get; set; }      //设置显示控件类型文字
        public string SetStartDate {get;set; }   //设置开始日期    
        public string SetEndDate { get; set; }      //设置结束日期  
      
        public DateTime? GetStartDate    //获取开始日期
        { 
            get
            {
                if (StringValidate.IsDateTime(dpkStart.Value))
                    return Convert.ToDateTime(dpkStart.Value);
                else
                    return null;
            }
        } 
        public DateTime? GetEndDate    //获取结束日期
        {
            get
            {
                if (StringValidate.IsDateTime(dpkEnd.Value))
                    return Convert.ToDateTime(dpkEnd.Value);
                else
                    return null;
            }
        } 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitle.Text = string.IsNullOrEmpty(SetTitle) == true ? "时间段" : SetTitle;
                dpkStart.Value = StringValidate.IsDateTime(SetStartDate) == false ? "" : SetStartDate;
                dpkEnd.Value = StringValidate.IsDateTime(SetEndDate) == false ? "" : SetEndDate;        
            }
        }     
    }
}