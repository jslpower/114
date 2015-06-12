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
using EyouSoft.Common.Control;
using EyouSoft.Common;
using Newtonsoft.Json;
namespace UserBackCenter.SMSCenter
{
    public partial class ImportNumFromSystem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string method=Utils.GetFormValue("method");
            if(method=="add")
            {
                AddCustomer();
            }
        }
        protected void AddCustomer()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            EyouSoft.IBLL.SMSStructure.ICustomer cus_Bll = EyouSoft.BLL.SMSStructure.Customer.CreateInstance();
            string custs=Utils.GetFormValue("custs");
            IList<EyouSoft.Model.SMSStructure.CustomerInfo> custList = JsonConvert.DeserializeObject<IList<EyouSoft.Model.SMSStructure.CustomerInfo>>(custs);
            foreach (EyouSoft.Model.SMSStructure.CustomerInfo cust in custList)
            {
                cust.CompanyId = SiteUserInfo.CompanyID;
                cust.UserId = SiteUserInfo.ID;
                cus_Bll.InsertCustomer(cust);
              
            }
            Utils.ResponseMeg(true, "");
        }
    }
}
