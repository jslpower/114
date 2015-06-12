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
using EyouSoft.ControlCommon.Control;

namespace TourUnion.WEB.IM
{
    /// <summary>
    /// 添加合作批发商
    /// 章已泉 2009-10-15
    /// </summary>
    public partial class CommendCompany : MQPage
    {
        #region 声明变量
        private int CompanyId;
        private int OperatorId;
        private string CompanyName;
        private string OperatorName;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //TourUnion.Account.Model.UserManage usermanage = TourUnion.Account.Factory.UserFactory.GetUserManage(TourUnion.Account.Enum.ChildSystemLocation.TourAgency, TourUnion.Account.Enum.SystemMedia.MQ);
            //if (usermanage.IsLoginAndGoTo())
            //{
            //    TourUnion.Account.Model.Account operatorModel = usermanage.AccountUser;
            //    CompanyId = operatorModel.CompanyId;
            //    CompanyName = operatorModel.CompanyName;
            //    OperatorId = operatorModel.Id;
            //    OperatorName = operatorModel.ContactName;
            //    operatorModel = null;
            //}
            //usermanage = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region 获取数据
            string DescCompanyName = txtCompanyName.Text.Trim();
            string DescContactName = txtContactName.Text.Trim();
            string DescContactTel = txtContactTel.Text.Trim();
            string DescContactMobile = txtContactMobil.Text.Trim();
            string TourAreaContent = txtAreaContent.Text.Trim();
            #endregion

            #region 数据有效性验证
            //string strError = "";
            //if (DescCompanyName == "")
            //{
            //    strError += "请输入批发商名称。\n";
            //}
            //if (DescContactName == "")
            //{
            //    strError += "请输入联系人。\n";
            //}
            //if (DescContactTel == "")
            //{
            //    strError += "请输入联系电话。\n";
            //}
            //if (TourAreaContent == "")
            //{
            //    strError += "请输入经营专线。\n";
            //}

            //if (strError != "")
            //{
            //    MessageBox.Show(strError);
            //    return;
            //}
            #endregion

            //TourUnion.Model.TourUnion_CommendComapny model = new TourUnion.Model.TourUnion_CommendComapny();
            //model.CompanyId = CompanyId;
            //model.CompanyName = CompanyName;
            //model.OperatorId = OperatorId;
            //model.OperatorName = OperatorName;
            //model.DescCompanyName = StringValidate.SafeRequest(DescCompanyName);
            //model.DescContactName = StringValidate.SafeRequest(DescContactName);
            //model.DescContactTel = StringValidate.SafeRequest(DescContactTel);
            //model.DescContactMobile = StringValidate.SafeRequest(DescContactMobile);
            //model.TourAreaContent = StringValidate.SafeRequest(TourAreaContent); 
            //model.IssueTime = DateTime.Now;

            //TourUnion.BLL.TourUnion_CommendComapny bll = new TourUnion.BLL.TourUnion_CommendComapny();
            //try
            //{
            //    bll.Add(model);
            //    MessageBox.ShowAndRedirect("推荐供应商成功！", Request.Url.ToString());
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("推荐供应商失败！");
            //}
        }
    }
}
