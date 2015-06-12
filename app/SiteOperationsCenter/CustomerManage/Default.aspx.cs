using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.CustomerManage
{
    /// <summary>
    /// 客户资料信息管理
    /// 开发人：孙川 时间：2010-12-2
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.YunYingPage
    {
        protected int PageSize = 20;   //每页显示条数
        protected int PageIndex = 1;   //当前页
        protected bool EditFlag = false;
        protected bool DeleteFlag = false;
        protected bool InsertFlag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["page"], 1);

            if (!this.IsPostBack)
            {
                bool isManage=CheckMasterGrant(EyouSoft.Common.YuYingPermission.客户资料_管理该栏目);
                if(isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.客户资料_新增))
                {
                    InsertFlag = true;
                }
                if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.客户资料_修改))
                {
                    EditFlag = true;
                }
                if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.客户资料_删除))
                {
                    DeleteFlag = true;
                    img_Delete.Visible = true;
                }
                if (!isManage)
                {
                    Utils.ResponseNoPermit();
                    return;
                }
                else
                {

                    this.img_Delete.ImageUrl = ImageServerUrl + "/images/yunying/shanchu.gif";
                    this.img_Delete.Attributes.Add("onclick", "return CustomerManage.DeleteCompany();");

                    InitQueryDropDownList();
                    BindCustomerList();
                }
            }
        }

        /// <summary>
        /// 初始话查询下来框
        /// </summary>
        private void InitQueryDropDownList()
        {
            #region  客户类型

            ddlCustomerType.Items.Clear();
            ddlCustomerType.DataSource = EyouSoft.BLL.PoolStructure.CustomerType.CreateInstance().GetCustomerTypeList();
            ddlCustomerType.DataTextField = "TypeName";
            ddlCustomerType.DataValueField = "TypeId";
            ddlCustomerType.DataBind();
            ddlCustomerType.Items.Insert(0, new ListItem("请选择", "0"));

            #endregion

            #region 适用产品

            ddlSuitProduct.Items.Clear();
            ddlSuitProduct.DataSource = EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().GetSuitProductList();
            ddlSuitProduct.DataTextField = "ProductName";
            ddlSuitProduct.DataValueField = "ProuctId";
            ddlSuitProduct.DataBind();
            ddlSuitProduct.Items.Insert(0, new ListItem("请选择", "0"));

            #endregion
        }

        /// <summary>
        /// 初始话客户信息列表
        /// </summary>
        protected void BindCustomerList()
        {
            int recordCount = 0;
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["ProvinceId"]);
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);

            int CustomerTypeaId = Utils.GetInt(Request.QueryString["CustomerTypeId"]);
            int SuitProductId = Utils.GetInt(Request.QueryString["SuitProductId"]);

            string CustomerName = Utils.InputText(Request.QueryString["CustomerName"]);
            string ContacterFullname = Utils.InputText(Request.QueryString["ContacterFullname"]);
            string ContacterMobile = Utils.InputText(Request.QueryString["ContacterMobile"]);

            EyouSoft.Model.PoolStructure.CompanySearchInfo SearchModel = new EyouSoft.Model.PoolStructure.CompanySearchInfo();

            if (ProvinceId != 0)
            {
                SearchModel.ProvinceId = ProvinceId;
                SearchModel.CityId = CityId;
            }
            SearchModel.CompanyName = CustomerName;
            SearchModel.ContacterFullname = ContacterFullname;
            SearchModel.ContacterMobile = ContacterMobile;
            if (CustomerTypeaId != 0)
            {
                SearchModel.CustomerTypeId = CustomerTypeaId;
            }
            if (SuitProductId != 0)
            {
                SearchModel.SuitProductId = SuitProductId;
            }

            SearchModel.UserCitys = MasterUserInfo.AreaId;                   //用户分管的城市类型
            SearchModel.UserCustomerTypes = MasterUserInfo.CustomerTypeIds;  //用户分管的客户类型

            IList<EyouSoft.Model.PoolStructure.CompanyInfo> CustomerList = EyouSoft.BLL.PoolStructure.Company.CreateInstance().GetCompanys(PageSize, PageIndex, ref recordCount, SearchModel);                                      //查询
            if (CustomerList != null && CustomerList.Count > 0)
            {
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.repCustomerList.DataSource = CustomerList;
                this.repCustomerList.DataBind();
            }
            else
            {
                System.Text.StringBuilder strEmptyText = new System.Text.StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" class=\"kuang\">");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>生日</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>职务</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>查看</strong></td>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='7' height='100px'>暂无客户信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr background=\"" + ImageServerUrl + "/images/yunying/hangbg.gif\" class=\"white\" height=\"23\">");
                strEmptyText.AppendFormat("<td width=\"5%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>序号</strong> </td> <td width=\"7%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>单位名称</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"18%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>联系人</strong></td><td width=\"10%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"> <strong>生日</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"12%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>职务</strong></td><td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>手机</strong></td>", ImageServerUrl);
                strEmptyText.AppendFormat("<td width=\"13%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\">  <strong>查看</strong></td>", ImageServerUrl);
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repCustomerList.EmptyText = strEmptyText.ToString();
            }

            if(SearchModel.ProvinceId.HasValue)
                this.ProvinceAndCityList1.SetProvinceId = SearchModel.ProvinceId.Value;
            if(SearchModel.CityId.HasValue)
                this.ProvinceAndCityList1.SetCityId = SearchModel.CityId.Value;
            if(SearchModel.CustomerTypeId.HasValue)
                this.ddlCustomerType.SelectedIndex = SearchModel.CustomerTypeId.Value;
            if(SearchModel.SuitProductId.HasValue)
                this.ddlSuitProduct.SelectedIndex = SearchModel.SuitProductId.Value;

            this.txtCompanyName.Value = SearchModel.CompanyName;
            this.txtContacterFullname.Value = SearchModel.ContacterFullname;
            this.txtContacterMobile.Value = SearchModel.ContacterMobile;

            SearchModel = null;
            CustomerList = null;
        }

        /// <summary>
        /// 添加序号
        /// </summary>
        protected void repCustomerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                int itemNum = e.Item.ItemIndex + 1;
                Label lblItemID = (Label)e.Item.FindControl("lblCustomerId");
                CheckBox cbListId = (CheckBox)e.Item.FindControl("cbCustomerId");
                cbListId.Text = Convert.ToString((PageIndex - 1) * PageSize + itemNum);
                cbListId.Attributes.Add("InnerValue", lblItemID.Text);
            }
        }

        /// <summary>
        /// 删除客户资料
        /// </summary>
        protected void img_Delete_Click(object sender, ImageClickEventArgs e)
        {
            bool Result = false;
            foreach (RepeaterItem item in this.repCustomerList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("cbCustomerId");
                if (cb.Checked)
                {
                    //获取要删除的列表项的主键编号
                    Label lblItemID = (Label)item.FindControl("lblCustomerId");
                    string CustomerID = lblItemID.Text;
                    Result = EyouSoft.BLL.PoolStructure.Company.CreateInstance().Delete(CustomerID);
                }
            }
            if (Result)
            {
                MessageBox.ShowAndRedirect(this, "删除成功!", "Default.aspx");
            }
            else
            {
                MessageBox.Show(this, "删除失败!");
            }
        }
    }
}
