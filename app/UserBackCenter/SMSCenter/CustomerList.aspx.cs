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
using System.Collections.Generic;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：SMS客户列表
    /// 开发人：xuty 开发时间：2010-08-03
    /// </summary>
    public partial class CustomerList : BackPage
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCustomerCate();//初始化客户类型
        }

        #region 绑定客户类型
        protected void BindCustomerCate()
        {
            //2010-12-16修改
            //客户类型来源 枚举型
            Type CusType = typeof(EyouSoft.Model.CompanyStructure.CompanyType);
            foreach (string s in Enum.GetNames(CusType))
            {
                ListItem item = new ListItem(s, Enum.Format(CusType, Enum.Parse(CusType, s), "d").Trim());
                cl_selUserClass.Items.Add(item);
            
            }
            //2010-12-16被替换部分   
            //cl_selUserClass.DataTextField = "CategoryName";
            //cl_selUserClass.DataValueField = "CategoryId";
            //cl_selUserClass.DataSource = EyouSoft.BLL.SMSStructure.Customer.CreateInstance().GetCategorys(SiteUserInfo.CompanyID);
            //cl_selUserClass.DataBind();
            cl_selUserClass.Items.Insert(0, new ListItem("选择类型", "-1"));
        }
        #endregion
    }
}
