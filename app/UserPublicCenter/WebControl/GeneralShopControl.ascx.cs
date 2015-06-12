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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text.RegularExpressions;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 页面功能：供应商普通网店通用控件
    /// 开发人：杜桂云      开发时间：2010-07-24
    /// </summary>
    public partial class GeneralShopControl : System.Web.UI.UserControl
    {
        #region 成员变量
        protected string ImageServerPath = "";
        private string _setAgencyId = "0";
        /// <summary>
        ///设置供应商编号
        /// </summary>
        public string SetAgencyId
        {
            get { return _setAgencyId; }
            set { _setAgencyId = value; }
        }

        public string CompanyName = "";  //公司名称
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                this.BindListInfo();
            }
        }
        #endregion

        #region 绑定列表数据
        private void BindListInfo()
        {
            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            //实例化类的对象
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo ComModel = Ibll.GetModel(SetAgencyId);
            if (ComModel != null) 
            {
                if (ComModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区) || ComModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店))
                {
                    this.isShowImprotent.Visible = false;
                }
                else 
                {
                    this.lbl_Importent.Text = ComModel.ShortRemark;
                }
                this.lbl_Title.Text = ComModel.CompanyName;
                //文章、景区、车队、旅游用品等抓取来的内容页面,不能出现外链,同业114内部链接可以出现
                if (ComModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区) 
                    || ComModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队)
                    || ComModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
                {
                    this.lit_Content.Text = Utils.RemoveHref(ComModel.Remark);
                }
                else
                {
                    this.lit_Content.Text = ComModel.Remark;
                }

                //this.lit_Content.Text = ComModel.Remark;
                //this.Page.Title = ComModel.CompanyName;
                CompanyName = ComModel.CompanyName;
            }
            //释放资源
            ComModel = null;
            Ibll = null;
        }
        #endregion
    }
}