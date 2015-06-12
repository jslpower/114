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
    /// 服务标准选择
    /// 罗丽娥   2010-06-28
    /// </summary>
    public partial class ServiceStandardList : EyouSoft.Common.Control.BasePage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        private int intPageSize = 10, CurrencyPage = 1;
        private string CompanyID = "0";
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected string ControlID = string.Empty;
        protected string ServiceType = string.Empty;
        private string ReleaseType = string.Empty;
        protected string ContainerID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/Default.aspx");
            }
            else {
                if (SiteUserInfo != null)
                {
                    UserInfoModel = SiteUserInfo;
                    CompanyID = UserInfoModel.CompanyID;
                }
            }
            if (!Page.IsPostBack)
            {
                ReleaseType = Utils.InputText(Request.QueryString["ReleaseType"]);
                ServiceType = Utils.InputText(Request.QueryString["Type"]);
                ContainerID = Request.QueryString["ContainerID"];
                InitServiceStandard(ServiceType);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(AddData());
                    Response.End();
                }
            }
        }

        #region 初始化
        private void InitServiceStandard(string ServiceType)
        {
            if (!String.IsNullOrEmpty(ServiceType))
            {
                ControlID = Utils.GetQueryStringValue("ControlID");
                switch (ServiceType)
                { 
                    case "1":    // 住宿
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.住宿);
                        this.lbltype.Text = "住宿安排";
                        
                        break;
                    case "2":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.用餐);
                        this.lbltype.Text = "用餐安排";

                        break;
                    case "3":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.景点);
                        this.lbltype.Text = "景点安排";

                        break;
                    case "4":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.用车);
                        this.lbltype.Text = "用车安排";

                        break;
                    case "5":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.导服);
                        this.lbltype.Text = "导游安排";

                        break;
                    case "6":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.往返大交通);
                        this.lbltype.Text = "往返交通安排";

                        break;
                    case "7":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.其他);
                        this.lbltype.Text = "其他安排";

                        break;
                    case "8":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.集合方式);
                        this.lbltype.Text = "集合方式";

                        break;
                    case "9":
                        this.InitData(EyouSoft.Model.CompanyStructure.ServiceTypes.接团方式);
                        this.lbltype.Text = "接团方式";
                        break;
                }
            }
        }
        #endregion

        #region 初始化服务标准信息
        /// <summary>
        /// 初始化服务标准信息
        /// </summary>
        private void InitData(EyouSoft.Model.CompanyStructure.ServiceTypes type)
        {
            int intRecordCount = 0;
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"],1);
            EyouSoft.IBLL.CompanyStructure.IServiceStandard bll = EyouSoft.BLL.CompanyStructure.ServiceStandard.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.ServiceStandard> list = bll.GetList(CompanyID, type, intPageSize, CurrencyPage, ref intRecordCount);
            if (list != null && list.Count > 0)
            {
                this.RepeaterList.DataSource = list;
                this.RepeaterList.DataBind();
            }
            else {
                this.pnlNoData.Visible = true;
            }

            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = CurrencyPage;
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";

            list = null;
            bll = null;
        }
        #endregion

        protected void RepeaterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Label lblNum = (Label)e.Item.FindControl("lblNum");
                lblNum.Text = Convert.ToString((CurrencyPage - 1) * intPageSize + e.Item.ItemIndex + 1);
            }
        }

        #region 添加数据
        protected bool AddData()
        {
            EyouSoft.Model.CompanyStructure.ServiceStandard model = new EyouSoft.Model.CompanyStructure.ServiceStandard();
            string Content = Utils.InputText(Request.QueryString["AddValue"]);
            model.CompanyID = CompanyID;
            model.Content = Content;
            model.OperatorID = UserInfoModel.ID;
            if (!String.IsNullOrEmpty(ServiceType))
            {
                switch (ServiceType)
                { 
                    case "1":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.住宿;
                        break;
                    case "2":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.用餐;
                        break;
                    case "3":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.景点;
                        break;
                    case "4":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.用车;
                        break;
                    case "5":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.导服;
                        break;
                    case "6":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.往返大交通;
                        break;
                    case "7":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.其他;
                        break;
                    case "8":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.集合方式;
                        break;
                    case "9":
                        model.TypeID = EyouSoft.Model.CompanyStructure.ServiceTypes.接团方式;
                        break;
                }
            }
            EyouSoft.IBLL.CompanyStructure.IServiceStandard bll = EyouSoft.BLL.CompanyStructure.ServiceStandard.CreateInstance();
            return bll.Add(model);
        }
        #endregion
    }
}
