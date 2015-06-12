using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

namespace IMFrame.WebControls
{
    public partial class SubAccount : System.Web.UI.UserControl
    {
        /// <summary>
        /// 是否显示控件 默认为显示
        /// </summary>
        public bool IsShow
        {
            get { return _isShow; }
            set { _isShow = value; }
        }
        
        /// <summary>
        /// 调用的JS函数以及参数
        /// </summary>
        public string FunStringOnChange
        {
            get { return _funStringOnChange; }
            set { _funStringOnChange = value; }
        }

        /// <summary>
        /// 当前公司ID
        /// </summary>
        public string CompanyId
        {
            get { return _companyid; }
            set { _companyid = value; }
        }

        /// <summary>
        /// 当前用户ID
        /// </summary>
        public string OperatorId
        {
            get { return _operatorid; }
            set { _operatorid = value; }
        }

        private string _companyid = "";
        private string _operatorid = string.Empty;
        private bool _isShow = true;  
        private string _funStringOnChange;
        protected int SubAccountCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Visible = IsShow;
                //获取该公司下的子账户

                EyouSoft.Common.Utils.GetCompanyChildAccount(ddlOperator, CompanyId);

                if (ddlOperator != null)
                {
                    this.SubAccountCount = ddlOperator.Items.Count - 1;
                }
                if (!IsShow)
                {
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(FunStringOnChange))
                    {
                        ddlOperator.Attributes.Add("onchange", FunStringOnChange);
                    }
                }

            }
        }
    }
}