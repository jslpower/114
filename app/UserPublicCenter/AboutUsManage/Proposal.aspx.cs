using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 信息反馈页
    /// 功能：保存反馈信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04   
    public partial class Proposal : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //type 为 1的时候 类型为商铺
                //type 为 2的时候 类型为MQ
                //type 为 3的时候 类型为平台
                //type 为 4的时候 类型为个人中心
                string type = Utils.GetQueryStringValue("type");
                this.hidetype.Value = type;
                //初始化内容
                DataInit();
                //设置头部图片
                this.AboutUsHeadControl1.SetImgType = "6";
                this.AboutUsLeftControl1.SetIndex = "6";
                
                //设置按钮图片
                this.ImgBtn.ImageUrl = ImageServerPath + "/images/UserPublicCenter/tijiaojianyian.gif";
            }

        }
        #region 初始化内容
        protected void DataInit()
        {
            if (IsLogin)
            {
                this.txtCompany.Value = SiteUserInfo.CompanyName;
                this.txtComtact.Value = SiteUserInfo.ContactInfo.ContactName;
                this.txtTelPhone.Value = SiteUserInfo.ContactInfo.Tel;
                this.hideMobile.Value = SiteUserInfo.ContactInfo.Mobile;
                this.hideMQ.Value = SiteUserInfo.ContactInfo.MQ;
                this.hideQQ.Value = SiteUserInfo.ContactInfo.QQ;
            }
        }
        #endregion

        #region 保存不同的反馈信息
        protected void AddNewMessage(string type)
        {
            EyouSoft.IBLL.SystemStructure.IProductSuggestion IBll = EyouSoft.BLL.SystemStructure.ProductSuggestion.CreateInstance();
            EyouSoft.Model.SystemStructure.ProductSuggestionType pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.商铺;
            switch (type)
            {
                case "1": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.商铺; break;
                case "2": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.MQ; break;
                case "3": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.平台; break;
                case "4": pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.个人中心; break;
                default: pst = EyouSoft.Model.SystemStructure.ProductSuggestionType.平台; break;
            }

            EyouSoft.Model.SystemStructure.ProductSuggestionInfo model = new EyouSoft.Model.SystemStructure.ProductSuggestionInfo();

            model.CompanyName = Utils.InputText(this.txtCompany.Value);
            model.ContactName = Utils.InputText(this.txtComtact.Value);
            model.ContactTel = Utils.InputText(this.txtTelPhone.Value);
            model.ContentText = Utils.InputText(this.txtContentText.Value); 
            model.IssueTime = DateTime.Now;
            model.ContactMobile = this.hideMobile.Value;
            model.MQ = this.hideMQ.Value;
            model.QQ = this.hideQQ.Value;   
            model.SuggestionType = pst;
            //新增
            IBll.InsertSuggestionInfo(model); 
        }
        #endregion

        #region 按钮提交表单事件
        protected void ImgBtn_Click(object sender, ImageClickEventArgs e)
        {
            AddNewMessage(this.hidetype.Value);
            Utils.ShowAndRedirect("建议提交成功!", "Proposal.aspx?type=" + this.hidetype.Value + "&CityId=" + this.CityId);
        }
        #endregion

    }
}
