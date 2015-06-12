using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 供求填写对话框
    /// author dj
    /// date 2011-05-16
    /// </summary>
    public partial class SupplierDialog : EyouSoft.Common.Control.FrontPage
    {
        protected IList<EyouSoft.Model.SystemStructure.SysProvince> provlist = null;//省份
        protected int htype = 1;
        protected int ssel = 1;
        public int publishCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string strLoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Domain.UserPublicCenter + Request.RawUrl, string.Empty);
            //if (!IsLogin)
            //{
            //    Response.Redirect(strLoginUrl);
            //}
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectMinLoginPage("/SupplierInfo/SupplierDialog.aspx", "如果要发布供求，请先登录");
            }
            string type = Utils.GetQueryStringValue("type");
            string titleInfo = Utils.GetQueryStringValue("titleinfo");
            bool sameTitle = false;
            if (type == "sametitle" && titleInfo != "")
            {
                sameTitle = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().ExistsSameTitle(SiteUserInfo.ID, titleInfo,"");
                if (sameTitle)
                {
                    Response.Clear();
                    Response.Write("true");
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("false");
                    Response.End();
                }
            }
            publishCount = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetExchangeListCount(SiteUserInfo.ID);

            if (!IsPostBack)
            {
                Bind();
            }
            else
            {
                if (publishCount < 15 && !sameTitle)
                {
                    btnSave();
                }
                else
                {
                    Utils.ShowAndRedirect("发布条数已满！", Request.RawUrl);
                }
            }
        }

        /// <summary>
        /// 保存求购
        /// </summary>
        private void btnSave()
        {
            #region 验证数据
            //int Type = Utils.GetInt(Request.Form["type"], 1);     //类型
            // = Utils.GetFormValue("seltype");    //标签
            //string Title = Utils.GetFormValue("txtTitle", 50);      //标题
            //string TxtInfo = Utils.GetFormValue("txtInfo", 500);     //内容
            //string TxtName = Utils.InputText(txtName.Value, 100);    //姓名
            //string TxtMQ = Utils.InputText(txtMQ.Value, 20);        //MQ
            //string TxtTel = Utils.InputText(txtTel.Value, 50);      //联系电话
            //string AttatchPath = Utils.GetFormValue("ctl00$ctl00$c1$SupplierBody$SingleFileUpload1$hidFileName", 250);   //附件地址
            //string[] strProvinceIds = Utils.GetFormValues("ckbProvince");               //发布省份
            //if (Title.Trim().Length == 0)
            //{
            //    ErrStr += "请输入求购信息标题\\n";
            //}
            //if (TxtInfo.Trim().Length == 0)
            //{
            //    ErrStr += "请输入求购信息内容\\n";
            //}
            //if (TxtMQ.Trim().Length == 0)
            //{
            //    ErrStr += "请输入MQ号码";
            //}
            //if (!string.IsNullOrEmpty(ErrStr))
            //{
            //    MessageBox.Show(this.Page, ErrStr);
            //    return;
            //}
            #endregion
            EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            model.AttatchPath = Utils.GetFormValue("SingleFileUpload1$hidFileName", 250);
            model.CityId = SiteUserInfo.CityId;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.ContactName = Utils.GetFormValue("txtname",100);
            model.ContactTel = Utils.GetFormValue("txttel",50);
            model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Utils.GetInt(Request.Form["type"]);
            model.ExchangeText = Utils.GetFormValue("content");
            model.ExchangeTitle = Utils.GetFormValue("title");
            model.ID = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = Utils.GetFormValue("txtmq", 20);
            model.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            model.ProvinceId = SiteUserInfo.ProvinceId;
            int htype = Utils.GetInt(Request.Form["htype"]);
            if (htype == 1)
            {
                model.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.求;
            }
            else
            {
                model.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.供;
            }
            if (model.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
            {
                model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Utils.GetInt(Request.Form["seltype2"]);
            }
            else
            {
                model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Utils.GetInt(Request.Form["seltype1"]);
            }
            model.IsCheck = IsCompanyCheck;//供求审核状态默认等于当前用户所在公司的审核状态
            

            string[] strProvinceIds  = Utils.GetFormValues("selprov");
            IList<int> ProvinceIds = null;
            if (strProvinceIds != null && strProvinceIds.Length > 0)
            {
                ProvinceIds = new List<int>();
                for (int i = 0; i < strProvinceIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strProvinceIds[i]) && StringValidate.IsInteger(strProvinceIds[i]))
                        ProvinceIds.Add(int.Parse(strProvinceIds[i]));
                }
            }
            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().AddExchangeList(model, ProvinceIds == null ? null : ProvinceIds.ToArray());
            if (Result)
            {

                Utils.ShowAndClose("发布成功！", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(model.ID, CityId));
            }
            else
            {
                Utils.ShowAndRedirect("发布失败！", Request.RawUrl);
            }
        }

        /// <summary>
        /// 初使化绑定
        /// </summary>
        private void Bind()
        {
            provlist = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            htype = Utils.GetInt(Request.QueryString["htype"],1);
            ssel = Utils.GetInt(Request.QueryString["ssel"], 1);
            if (htype > 2)
            { htype = 1; }
            if (htype < 1)
            { htype = 2; }
        }
    }
}
