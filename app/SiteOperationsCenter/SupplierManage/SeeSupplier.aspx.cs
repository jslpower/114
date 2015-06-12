using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.SupplierManage
{
    /// <summary>
    /// 供求信息查看
    /// </summary>
    /// 周文超 2010-07-30
    public partial class SeeSupplier : EyouSoft.Common.Control.YunYingPage
    {
        private string ExchangeId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ExchangeId = Utils.InputText(Request.QueryString["ID"]);
            if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.供求信息_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                EyouSoft.Model.CommunityStructure.ExchangeList model = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetModel(ExchangeId);
                if (model == null)
                {
                    MessageBox.ShowAndClose(this, "未找到对应的供求信息！");
                    return;
                }

                ltrTitle.Text = model.ExchangeTitle;
                ltrTime.Text = model.IssueTime.ToShortDateString();
                ltrCompanyName.Text = model.CompanyName;
                ltrInfo.Text = model.ExchangeText;

                System.Text.StringBuilder strImgPath = new System.Text.StringBuilder();
                if (model.ExchangePhotoList != null && model.ExchangePhotoList.Count > 0)
                {
                    foreach (EyouSoft.Model.CommunityStructure.ExchangePhoto item in model.ExchangePhotoList)
                    {
                        strImgPath.AppendFormat("<img src=\"{0}\" />", Domain.FileSystem + item.ImgPath);
                    }
                }
                ltrImg.Text = strImgPath.ToString();
                if (!string.IsNullOrEmpty(model.AttatchPath))
                {
                    string strFileName = model.AttatchPath.Substring(model.AttatchPath.LastIndexOf("/") + 1, (model.AttatchPath.Length - model.AttatchPath.LastIndexOf("/") - 1));
                    ltrDownLoad.Text = string.Format("<span class=\"lan14\"><strong>附 件:</strong></span><a target=\"_blank\" href=\"{0}{1}\">{3}<img src=\"{2}/images/UserPublicCenter/baocun.gif\" width=\"14\" height=\"13\" /><img src=\"{2}/images/UserPublicCenter/dakai.gif\" width=\"16\" height=\"14\" /></a>", Domain.FileSystem, model.AttatchPath, Domain.ServerComponents, strFileName);
                }

                model = null;
            }
        }
    }
}
