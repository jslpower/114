using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Security.Membership;

namespace UserBackCenter.SupplyManage
{
    public partial class FreeShop : EyouSoft.Common.Control.BackPage
    {
        protected string CompanyName = string.Empty;
        protected string ShortRemark = string.Empty;
        protected string Remark = string.Empty;
        protected string CompanyImgThumb = string.Empty;
        private string CompanyID = string.Empty;
        protected string ShortRmarkIntroduce = string.Empty;
        protected bool isShowShortRemark = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;
            EyouSoft.Model.CompanyStructure.SupplierInfo model = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(CompanyID);
            if (model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                Response.Clear();
                Response.Write("对不起，你没有权限操作供应商网店后台，如有需要请申请开通！");
                Response.End();
            }
            model = null;
            //if (!this.IsSupplyUser)
            //{
            //    Utils.ShowError("对不起，你的公司身份不是属于供应商，没有权限进入供应商网店管理！", "SupplyError");
            //    return;
            //}
            getUserCompanyType();
            CompanyName = this.SiteUserInfo.CompanyName;

            if (Utils.GetQueryStringValue("action") == "SupplerManage")
            {
                Save();
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo model = Ibll.GetModel(CompanyID);
            if (model != null)
            {
                CompanyName = model.CompanyName;
                CompanyImgThumb = model.CompanyImgThumb;
                Remark =model.Remark;
                ShortRemark = model.ShortRemark;
            }
            Ibll = null;
            model = null;
        }
        private void getUserCompanyType()
        {
            if (this.SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                switch (this.SiteUserInfo.CompanyRole.RoleItems[0])
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        isShowShortRemark = false;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                        isShowShortRemark = false;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                        ShortRmarkIntroduce = "主营产品";
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        ShortRmarkIntroduce = "主营产品";
                        break;
                    default:
                        ShortRmarkIntroduce = "主要优势";
                        break;
                }
            }
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        private void Save()
        {
            if (this.IsLogin)
            {
                if (!IsCompanyCheck)
                {                    
                    Response.Clear();
                    Response.Write("[{isSuccess:false,Message:'对不起，新注册用户还未审核，没有权限进入供应商网店管理！'}]");
                    Response.End();
                }

                string ComRemark = Server.UrlDecode(Request.Form["Remark"]);
                ComRemark = Utils.EditInputText(Server.UrlDecode(ComRemark.Substring(0, Remark.Length > 4000 ? 4000 : ComRemark.Length)));
                string ComShortRemark = Utils.GetFormValue("ShortRemark", 100);
                if (string.IsNullOrEmpty(ComShortRemark) && isShowShortRemark)
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:false,Message:'主要优势不能为空！'}]");
                    Response.End();
                }
                EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
                string ImgPath = Utils.GetFormValue("hidCompanyImg");
                if (!string.IsNullOrEmpty(Request.Form["ctl00$ContentPlaceHolder1$SingleFileUpload1$hidFileName"]))
                {
                    ImgPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$SingleFileUpload1$hidFileName");
                }
                bool isTrue = Ibll.Update(CompanyID, CompanyName, ComRemark, ComShortRemark, ImgPath);
                Ibll = null;
                if (isTrue)
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:true,Message:'保存成功！'}]");
                    Response.End();
                }

            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,Message:'你还未登录或登录过期请重新登录！'}]");
                Response.End();
            }

        }
    }
}
