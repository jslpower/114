using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 查看旅行社证书
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class CompanyCreditList : EyouSoft.Common.Control.YunYingPage
    {
        protected string AllCompanyCertif = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.GetCreditList(Request.QueryString["CompanyId"]);
            }
        }
        /// <summary>
        /// 获的公司证书信息
        /// </summary>
        protected void GetCreditList(string CompanyId)
        {

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            bool isSetCertif = false;
            if (Model != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyAttachInfo AttchInfo = Model.AttachInfo;
                if (AttchInfo != null)
                {
                    EyouSoft.Model.CompanyStructure.BusinessCertif CertifModel = AttchInfo.BusinessCertif;
                    if (CertifModel != null)
                    {
                        if (CertifModel.BusinessCertImg != "")
                        {
                            isSetCertif = true;
                            AllCompanyCertif += string.Format("经营许可证：<a href='{0}' target='_blank'><img src='{0}' width='60px' height='30px'/></a><br/>", EyouSoft.Common.Domain.FileSystem + CertifModel.BusinessCertImg);
                        }
                        if (CertifModel.LicenceImg != "")
                        {
                            isSetCertif = true;
                            AllCompanyCertif += string.Format("营业执照：<a href='{0}' target='_blank'><img src='{0}' width='60px' height='30px'/></a><br/>", EyouSoft.Common.Domain.FileSystem + CertifModel.LicenceImg);
                        }
                        if (CertifModel.TaxRegImg != "")
                        {
                            isSetCertif = true;
                            AllCompanyCertif += string.Format("税务登记证：<a href='{0}' target='_blank'><img src='{0}' width='60px' height='30px'/></a><br/>", EyouSoft.Common.Domain.FileSystem + CertifModel.TaxRegImg);
                        }
                    }

                    CertifModel = null;
                }

                AttchInfo = null;
            }
            if(!isSetCertif)
            {
                AllCompanyCertif = "该公司暂未上传证书";
            }
            Model=null;

        }
    }
}
