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
    /// 供求信息管理
    /// </summary>
    /// 周文超 2010-07-26
    public partial class SupplierInfo : EyouSoft.Common.Control.YunYingPage
    {
        #region Attributes

        protected int intPageSize = 15, CurrencyPage = 1;

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.供求信息_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        #endregion

        #region 页面数据初始化

        /// <summary>
        /// 页面数据初始化
        /// </summary>
        private void InitPageData()
        {
            InitQueryDropDownList();
            InitSupplierInfoList();
        }

        #region 初始化查询下拉框

        /// <summary>
        /// 初始化查询下拉框
        /// </summary>
        private void InitQueryDropDownList()
        {
            #region 初始化区域下拉框

            ddlCity.Items.Clear();
            ddlCity.DataSource = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList();
            ddlCity.DataTextField = "ProvinceName";
            ddlCity.DataValueField = "ProvinceId";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("请选择", "0"));


            #endregion

            #region 初始化类别下拉框

            ddlSupplierType.Items.Clear();
            ddlSupplierType.DataSource = EyouSoft.Common.Function.EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.ExchangeType));
            ddlSupplierType.DataTextField = "text";
            ddlSupplierType.DataValueField = "value";
            ddlSupplierType.DataBind();
            ddlSupplierType.Items.Insert(0, new ListItem("请选择", "0"));

            #endregion
        }

        #endregion

        #region 初始化供求信息列表

        /// <summary>
        /// 初始化供求信息列表
        /// </summary>
        private void InitSupplierInfoList()
        {
            int intRecordCount = 0;
            DateTime? StartTime = Utils.GetDateTimeNullable(Utils.InputText(Request.QueryString["StartTime"]));
            DateTime? EndTime = Utils.GetDateTimeNullable(Utils.InputText(Request.QueryString["EndTime"]));
            int Type = Utils.GetInt(Utils.InputText(Request.QueryString["Type"]));
            int ProvinceId = Utils.GetInt(Utils.InputText(Request.QueryString["CityId"]));
            string strKeyWord = Utils.InputText(Request.QueryString["KeyWord"]);
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            txtStartTime.Text = StartTime.HasValue ? StartTime.Value.ToString("yyyy-MM-dd") : string.Empty;
            txtEndTime.Text = EndTime.HasValue ? EndTime.Value.ToString("yyyy-MM-dd") : string.Empty;
            ddlCity.SelectedValue = ProvinceId.ToString();
            ddlSupplierType.SelectedValue = Type.ToString();
            txtKeyWord.Value = strKeyWord;
            EyouSoft.Model.CommunityStructure.ExchangeType? enumType = new EyouSoft.Model.CommunityStructure.ExchangeType?();
            if (Type > 0)
                enumType = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), Type.ToString());
            else
                enumType = null;
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> List = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetSerachList(intPageSize, CurrencyPage, ref intRecordCount, enumType, ProvinceId, 0, strKeyWord, StartTime, EndTime, null);

            if (!List.Equals(null) && List.Count > 0)
            {
                rptSupplierList.DataSource = List;
                rptSupplierList.DataBind();
                //绑定分页控件
                this.ExportPageInfo.intPageSize = intPageSize;
                this.ExportPageInfo.intRecordCount = intRecordCount;
                this.ExportPageInfo.CurrencyPage = CurrencyPage;
                this.ExportPageInfo.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo.UrlParams = Request.QueryString;
                this.ExportPageInfo.PageLinkURL = "/SupplierManage/SupplierInfo.aspx?";
                this.ExportPageInfo.LinkType = 3;
            }
            else
            {
                trNoData.Visible = true;
            }
            if (List != null) List.Clear();
            List = null;
        }

        #endregion

        #region 前台函数

        /// <summary>
        /// 根据城市ID获取名称
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns></returns>
        protected string GetCityNameById(string ProvinceId)
        {
            if (string.IsNullOrEmpty(ProvinceId) || EyouSoft.Common.Function.StringValidate.IsInteger(ProvinceId) == false)
                return string.Empty;

            EyouSoft.Model.SystemStructure.SysProvince model = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(int.Parse(ProvinceId));
            if (model.Equals(null))
                return string.Empty;
            else
                return model.ProvinceName;
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btn = (Button)e.Item.FindControl("btnDel");
                ImageButton ibtnSetTop = (ImageButton)e.Item.FindControl("ibtnSetTop");
                Button btnCheck = (Button)e.Item.FindControl("btnCheck");
                if (btn != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_删除))
                    {
                        btn.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                    else
                    {
                        btn.Attributes.Add("onclick", "return confirm('您确定要删除此供求信息吗？');");
                    }
                }
                if (ibtnSetTop != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_修改))
                    {
                        ibtnSetTop.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                }
                if (btnCheck != null)
                {
                    if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_修改))
                    {
                        btnCheck.Attributes.Add("onclick", "alert('对不起，您还没有该权限！');return false;");
                    }
                }
            }
        }

        /// <summary>
        /// 命令行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CommandName) || string.IsNullOrEmpty(e.CommandArgument.ToString()))
                return;
            switch (e.CommandName.ToLower())
            {
                case "del":
                    if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_删除))
                    {
                         MessageBox.Show(this.Page, "对不起，你没有该权限");
                        return;
                    }
                    if (EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().ManageDel(e.CommandArgument.ToString()))
                        MessageBox.ShowAndRedirect(this, "删除成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, "删除失败！", Request.RawUrl);
                    break;
                case "top":
                    if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_修改))
                    {
                         MessageBox.Show(this.Page, "对不起，你没有该权限");
                        return;
                    }
                    string[] strTmp = e.CommandArgument.ToString().Trim().Split(',');
                    bool IsTop = false;
                    if (strTmp == null || strTmp.Length != 2 || string.IsNullOrEmpty(strTmp[0]))
                        break;
                    if (strTmp[1].ToLower() == "true" || strTmp[1].ToLower() == "1")
                        IsTop = true;
                    if (EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().SetTop(strTmp[0], IsTop ? false : true))
                        MessageBox.ShowAndRedirect(this, (IsTop ? "取消置顶" : "设置置顶") + "成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, (IsTop ? "取消置顶" : "设置置顶") + "失败！", Request.RawUrl);
                    break;
                case "check":
                    if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_修改))
                    {
                         MessageBox.Show(this.Page, "对不起，你没有该权限");
                        return;
                    }
                    string[] Tmp = e.CommandArgument.ToString().Trim().Split(',');
                    bool IsCheck = false;
                    if (Tmp == null || Tmp.Length != 2 || string.IsNullOrEmpty(Tmp[0]))
                        break;
                    if (Tmp[1].ToLower() == "true" || Tmp[1].ToLower() == "1")
                        IsCheck = true;
                    if (EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().SetCheck(Tmp[0], IsCheck ? false : true))
                        MessageBox.ShowAndRedirect(this, (IsCheck ? "取消审核" : "审核") + "成功！", Request.RawUrl);
                    else
                        MessageBox.ShowAndRedirect(this, (IsCheck ? "取消审核" : "审核") + "失败！", Request.RawUrl);
                    break;
            }
        }

        #endregion

        protected void btnCheckAll_Click(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_修改))
            {
                 MessageBox.Show(this.Page, "对不起，你没有该权限");
                return;
            }
            string[] ExchangeIds = Utils.GetFormValues("ckExchangID");
            if (ExchangeIds == null || ExchangeIds.Length == 0)
            {
                MessageBox.Show(this.Page, "请选择您要审核的项！");
                return;
            }
            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().BatchCheck(ExchangeIds);
            if (Result)
            {
                MessageBox.ShowAndRedirect(this.Page, "审核成功！", Request.RawUrl);
            }
            {
                MessageBox.ShowAndRedirect(this.Page, "审核失败！", Request.RawUrl);
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            bool Result = true;
            if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目, YuYingPermission.供求信息_修改))
            {
                MessageBox.Show(this.Page, "对不起，你没有该权限");
                return;
            }
            string[] ExchangeIds = Utils.GetFormValues("ckExchangID");
            if (ExchangeIds == null || ExchangeIds.Length == 0)
            {
                MessageBox.Show(this.Page, "请选择您要删除的项！");
                return;
            }
            foreach (string eid in ExchangeIds)
            {
                EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().Delete(eid);
            }
            if (Result)
            {
                MessageBox.ShowAndRedirect(this.Page, "删除成功！", Request.RawUrl);
            }
            {
                MessageBox.ShowAndRedirect(this.Page, "删除失败！", Request.RawUrl);
            }
        }

        #endregion
    }
}
