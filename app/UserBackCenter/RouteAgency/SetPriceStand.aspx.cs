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
using EyouSoft.Common.Function;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 设置价格等级
    /// 罗丽娥   2010-06-28
    /// </summary>
    public partial class SetPriceStand : EyouSoft.Common.Control.BasePage
    {
        protected string CompanyId = "0";
        protected string MyPriceStand = string.Empty;
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected string ContainerID = string.Empty;
        private int i = 0, j = 0;
        protected int OtherPriceCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/Default.aspx");
            }
            else
            {
                if (SiteUserInfo != null)
                {
                    UserInfoModel = SiteUserInfo;
                    CompanyId = UserInfoModel.CompanyID;
                }
            }
            ContainerID = Utils.InputText(Request.QueryString["ContainerID"]);
            if (!Page.IsPostBack)
            {
                InitOtherPriceStand();
                //初始化公司己设置的价格等级

                BindPriceList();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("save", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(btnSave());
                    Response.End();
                }
                else if (flag.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(InsertPrice());
                    Response.End();
                }
                else if (flag.Equals("click", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(IsUsingPriceStand());
                    Response.End();
                }
            }
        }

        #region 初始化其他价格等级
        private void InitOtherPriceStand()
        {
            IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> list = EyouSoft.BLL.CompanyStructure.CommonPriceStand.CreateInstance().GetList();
            if (list != null && list.Count > 0)
            {
                OtherPriceCount = list.Count;
                this.rptPriceStand.DataSource = list;
                this.rptPriceStand.DataBind();
            }
            list = null;
        }
        #endregion

        #region 获的公司已经设置过的价格等级
        /// <summary>
        /// 获的公司已经设置过的价格等级
        /// </summary>
        private void BindPriceList()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> list = bll.GetList(CompanyId);
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand model in list)
                {
                    MyPriceStand += model.CommonPriceStandID + ",";
                }
                MyPriceStand = MyPriceStand.Substring(0, MyPriceStand.Length - 1);
            }
            list = null;
            bll = null;
        }
        #endregion

        #region 添加新的价格等级
        /// <summary>
        /// 添加新的价格等级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected bool InsertPrice()
        {
            string PriceName = Request.QueryString["PriceName"];
            if (string.IsNullOrEmpty(PriceName))
            {
                MessageBox.Show(this,"请输入价格等级名称！!");
                return false;
            }
            else
            {
                EyouSoft.IBLL.SystemStructure.IProductSuggestion bll = EyouSoft.BLL.SystemStructure.ProductSuggestion.CreateInstance();
                EyouSoft.Model.SystemStructure.ProductSuggestionInfo model = new EyouSoft.Model.SystemStructure.ProductSuggestionInfo();
                model.CompanyId = CompanyId;
                model.CompanyName = UserInfoModel.CompanyName;
                if(UserInfoModel.ContactInfo != null)
                {
                    model.ContactMobile = UserInfoModel.ContactInfo.Mobile;
                    model.ContactName = UserInfoModel.ContactInfo.ContactName;
                    model.ContactTel = UserInfoModel.ContactInfo.Tel;
                    model.MQ = UserInfoModel.ContactInfo.MQ;
                    model.QQ = UserInfoModel.ContactInfo.QQ;
                }
                model.ContentText = PriceName;
                model.SuggestionId = Guid.NewGuid().ToString();
                model.SuggestionType = EyouSoft.Model.SystemStructure.ProductSuggestionType.个人中心报价标准;
                bool Result = bll.InsertSuggestionInfo(model);
                model = null;
                bll = null;
                return Result;
            }
        }
        #endregion

        #region 设置价格等级
        /// <summary>
        /// 设置价格等级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected string btnSave()
        {
            string strSelect = string.Empty;
            string PriceId = Request.QueryString["IdList"];
            string PriceName = Request.QueryString["priceNameList"];
            if (PriceId.Length == 0)
            {
                MessageBox.Show(this, "请选择价格等级!");
            }
            else
            {
                IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> commonPriceStands = new List<EyouSoft.Model.CompanyStructure.CommonPriceStand>();
                for (int i = 0; i < PriceId.Split(',').Length; i++)
                {
                    EyouSoft.Model.CompanyStructure.CommonPriceStand model = new EyouSoft.Model.CompanyStructure.CommonPriceStand();
                    model.ID = PriceId.Split(',')[i];
                    model.PriceStandName = PriceName.Split(',')[i];
                    model.TypeID = EyouSoft.Model.CompanyStructure.CommPriceTypeID.None;
                    commonPriceStands.Add(model);
                    model = null;
                }
                EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
                bool Result = bll.SetCompanyPriceStand(CompanyId, commonPriceStands);
                if (Result)
                {
                    strSelect = "<select  name=\"drpPriceRank\" onchange=\"TourPriceStand.isSamePrice(this);\">";
                    string strOption = "";
                    IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> list = bll.GetList(CompanyId);
                    if (list != null)
                    {
                        foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand Pricemodel in list)
                        {
                            strOption += string.Format("<option value=\"{0}\">{1}</option>", Pricemodel.ID, Pricemodel.PriceStandName);
                        }
                    }
                    strSelect += strOption + "</select>";

                    list = null;
                }                
                bll = null;
            }
            return strSelect;
        }
        #endregion

        private bool IsUsingPriceStand()
        {
            string commonPriceStandID = Utils.InputText(Request.QueryString["pricestandID"]);
            EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand bll = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance();
            return bll.IsUsing(UserInfoModel.CompanyID, commonPriceStandID);
        }

        protected void rptPriceStand_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (i%3 == 0)
                {
                    Literal ltrTR = (Literal)e.Item.FindControl("ltrTR");
                    ltrTR.Visible = true;
                }
                if (j>0 && (j+1)%3 == 0)
                {
                    Literal ltrBTR = (Literal)e.Item.FindControl("ltrBTR");
                    ltrBTR.Visible = true;
                }
                j++;
                i++;
            }
        }
    }
}

