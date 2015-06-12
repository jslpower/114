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
using EyouSoft.Common.Control;
using EyouSoft.Common;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：从文件中导入手机号码
    /// 开发人：杜桂云  开发时间：2010-08-03
    /// </summary>
    public partial class ImportNumFromFile : BasePage
    {
        #region 成员变量
    
        protected int pageTypeId = 0;  //0:从发送短信页面过来 ； 1：从客户列表页面过来
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(Context.Request.QueryString["typeID"]))
            {
                pageTypeId = int.Parse(Context.Request.QueryString["typeID"]);
            }
            if (pageTypeId == 1)
            {
                this.btn_ImportInfo.Visible = true;
                this.importTelPhone.Visible = false;
                this.btn_ImportInfo.Attributes.Add("onclick", "return inff.checkCusPhone();");
            }
            else
            {
                this.btn_ImportInfo.Visible = false;
                this.importTelPhone.Visible = true;
            }
        }
        #endregion

        #region 从文件中导入客户列表
        protected void btn_ImportInfo_Click(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>alert('对不起,你尚未审核通过!');</script>");
                return;
            }
            EyouSoft.IBLL.SMSStructure.ICustomer cus_Bll = EyouSoft.BLL.SMSStructure.Customer.CreateInstance();
            EyouSoft.Model.SMSStructure.CustomerInfo cus_Model = null;
            
            int CusTypeID = 0;
            string TelPhone = "";
            string CompanyName = "";
            string CustomerName = "";
            string Remark = "";
            string[] TelPhoneList = null;
            string[] CompanyNameList = null;
            string[] CustomerNameList = null;
         
                TelPhone = Utils.GetFormValue("hidTel");
                TelPhoneList = Utils.Split(TelPhone, "{~@}");
         
          
                CompanyName = Utils.GetFormValue("hidCompany");
                CompanyNameList = Utils.Split(CompanyName, "{~@}");
           
        
                CustomerName = Utils.GetFormValue("hidName");
                CustomerNameList = Utils.Split(CustomerName, "{~@}");
         
             for (int i = 0; i < TelPhoneList.Length; i++)
            {
                cus_Model = new  EyouSoft.Model.SMSStructure.CustomerInfo();
                cus_Model.CategoryId = CusTypeID;
                cus_Model.CompanyId = SiteUserInfo.CompanyID;
                if (CompanyNameList != null)
                {
                    cus_Model.CustomerCompanyName = CompanyNameList[i].ToString();
                }
                else
                {
                    cus_Model.CustomerCompanyName = "";
                }
                if (CustomerNameList != null)
                {
                    cus_Model.CustomerContactName = CustomerNameList[i].ToString();
                }
                else
                {
                    cus_Model.CustomerContactName = "";
                }
                if (TelPhoneList != null)
                {
                    cus_Model.Mobile = TelPhoneList[i].ToString();
                }
                else
                {
                    cus_Model.Mobile = "";
                }
                cus_Model.Remark = Remark;
                cus_Model.UserId = SiteUserInfo.ID;
                cus_Bll.InsertCustomer(cus_Model);

            }
            cus_Model = null;
            cus_Bll = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>alert('导入成功，若存在重复手机号码，将不会被导入！');window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();for(var i in window.parent.searchParam){window.parent.searchParam[i]=''};window.parent.CustomerList.search();</script>");

        }
        #endregion
    }
}
