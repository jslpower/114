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
using EyouSoft.Common;
using System.Text;

namespace UserBackCenter.RouteAgency
{
    public partial class AjaxGetContactInfo : System.Web.UI.Page
    {
        protected string CompanyID = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = Utils.InputText(Request.QueryString["CompanyID"]);
            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("select", StringComparison.OrdinalIgnoreCase))
                {
                    SelectAllContactInfo();
                }
                else if (flag.Equals("MQ", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(SelectMQID());
                    Response.End();
                }
            }
        }

        #region 查询团队联系人
        private void SelectAllContactInfo()
        {
            Response.Clear();
            EyouSoft.IBLL.CompanyStructure.ICompanyUser bll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();

            string strContactName = string.Empty;
            if (!String.IsNullOrEmpty(Utils.GetFormValue("ContactName")))   // 输入筛选
            {
                strContactName = Utils.GetFormValue("ContactName");
            }
            StringBuilder str = new StringBuilder();
            EyouSoft.Model.CompanyStructure.QueryParamsUser param = new EyouSoft.Model.CompanyStructure.QueryParamsUser();
            param.UserName = strContactName;
            param.IsShowAdmin = true;
            IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> list = bll.GetList(CompanyID, param);
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyUserBase model in list)
                {
                    str.Append(model.ContactInfo.ContactName + "~&&~" + model.ContactInfo.Tel + "~&&~" + model.ContactInfo.MQ + "~&&~" + model.UserName + "\n");
                }
            }
            list = null;
            bll = null;
            Response.Write(str.ToString());
            Response.End();
        }
        #endregion

        #region 查询MQID
        private int SelectMQID()
        {
            string MQUserName = Server.UrlDecode(Request.QueryString["MQUserName"]);
            EyouSoft.IBLL.MQStructure.IIMMember bll = EyouSoft.BLL.MQStructure.IMMember.CreateInstance();
            return bll.GetImId(MQUserName);
        }
        #endregion
    }
}
