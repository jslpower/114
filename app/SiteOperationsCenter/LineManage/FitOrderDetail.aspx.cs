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
using EyouSoft.Common;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散客订单管理 团队订单查看以及修改
    /// 蔡永辉 2011-12-22
    /// </summary>
    public partial class FitOption : EyouSoft.Common.Control.YunYingPage
    {
        protected string orderId = "";//订单id
        protected MTourOrder modelMTourOrder = null;
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        protected int currentPage = 0;
        protected StringBuilder strOrderStatus = null;
        protected StringBuilder strPaymentStatus = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            orderId = Utils.GetQueryStringValue("OrderId");
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            if (!IsPostBack)
            {
                Getmodelorder();
            }

        }

        #region 获取出发城市
        protected string GetLineModel(string lineid)
        {
            string result = "";
            MRoute modelroute = new MRoute();
            if (!string.IsNullOrEmpty(lineid))
            {
                modelroute = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(lineid);
                if (modelroute != null)
                {
                    result = modelroute.StartCityName;
                }
            }
            return result;
        }
        #endregion

        #region 获取联系方式
        protected string GetContact(string companyid, string typename)
        {
            StringBuilder result = new StringBuilder("");
            CompanyInfo modelcompanyinfo = new CompanyInfo();
            if (!string.IsNullOrEmpty(companyid))
            {
                modelcompanyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyid);
                if (modelcompanyinfo != null)
                {
                    result.AppendFormat("{3}名称：<a href=\"/CompanyManage/AddBusinessMemeber.aspx?EditId={4}\">{0}</a>;{3}联系人：{1};手机：{2};</br>", modelcompanyinfo.CompanyName, modelcompanyinfo.ContactInfo.ContactName, modelcompanyinfo.ContactInfo.Mobile, typename, companyid);
                    result.AppendFormat("<strong>MQ：</strong>{0}<strong>QQ：</strong>{1}", Utils.GetMQ(modelcompanyinfo.ContactInfo.MQ), Utils.GetQQ(modelcompanyinfo.ContactInfo.QQ));
                }
            }
            return result.ToString();
        }
        #endregion

        #region
        /// <summary>
        /// 绑定订单状态
        /// </summary>
        protected void GetBindPowder(PowderOrderStatus? OrderStatus)
        {
            strOrderStatus = new StringBuilder("");
            List<EnumObj> listEnumObj = EnumObj.GetList(typeof(PowderOrderStatus));
            foreach (EnumObj modelEnumObj in listEnumObj)
            {
                if (OrderStatus == (PowderOrderStatus)Enum.Parse(typeof(PowderOrderStatus), modelEnumObj.Value))
                    strOrderStatus.AppendFormat("<input type=\"radio\" checked=\"checked\" name=\"radbtnPowder\" value={0} />{1}", modelEnumObj.Value, modelEnumObj.Text);
                else
                    strOrderStatus.AppendFormat("<input type=\"radio\" name=\"radbtnPowder\" value={0} />{1}", modelEnumObj.Value, modelEnumObj.Text);
            }
        }
        /// <summary>
        /// 绑定支付状态
        /// </summary>
        protected void GetBindPayment(PaymentStatus? Paymentstu)
        {
            strPaymentStatus = new StringBuilder("");
            List<EnumObj> listEnumObj = null;
            listEnumObj = EnumObj.GetList(typeof(PaymentStatus));
            foreach (EnumObj item in listEnumObj)
            {
                if (Paymentstu == (PaymentStatus)Enum.Parse(typeof(PaymentStatus), item.Value))
                    strPaymentStatus.AppendFormat("<input type=\"radio\" checked=\"checked\" name=\"radbtn\" value={0} />{1}", item.Value, item.Text);
                else
                    strPaymentStatus.AppendFormat("<input type=\"radio\" name=\"radbtn\" value={0} />{1}", item.Value, item.Text);
            }
        }

        #endregion

        #region 获取订单详细
        protected void Getmodelorder()
        {
            modelMTourOrder = new MTourOrder();
            if (!string.IsNullOrEmpty(orderId))
            {
                modelMTourOrder = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetModel(orderId);
                if (modelMTourOrder != null)
                {
                    GetBindPowder(modelMTourOrder.OrderStatus);
                    GetBindPayment(modelMTourOrder.PaymentStatus);
                    GetCustomList(modelMTourOrder.Customers);
                }
            }
        }

        #endregion

        #region 获取游客列表
        protected void GetCustomList(IList<MTourOrderCustomer> customerslist)
        {
            if (customerslist != null)
            {
                int recordCount = customerslist.Count;
                if (customerslist.Count > 0)
                {
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = recordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "FitCustomesManage.LoadData(this);", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "FitCustomesManage.LoadData(this);", 0);
                    this.repList.DataSource = customerslist;
                    this.repList.DataBind();
                }
                else
                {
                    StringBuilder strEmptyText = new StringBuilder();
                    strEmptyText.Append("<table width=\"100%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                    strEmptyText.Append("<tr>");
                    strEmptyText.Append("<th>序号</th>");
                    strEmptyText.Append("<th>姓名</th>");
                    strEmptyText.Append("<th>联系电话</th><th>身份证</th>");
                    strEmptyText.Append("<th>护照</th><th>其他证件</th>");
                    strEmptyText.Append("<th>性别</th><th>类型</th>");
                    strEmptyText.Append("<th>座号</th><th>备注</th>");
                    strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无游客</td></tr>");
                    strEmptyText.Append("</tr>");
                    strEmptyText.Append("</table>");
                    this.repList.EmptyText = strEmptyText.ToString();
                }
            }
        }

        #endregion

        #region 保存数据
        protected void btnsave_Click(object sender, EventArgs e)
        {
            bool boolPowder = false;
            bool boolPayment = false;
            EyouSoft.IBLL.NewTourStructure.ITourOrder orderBll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();
            if (Utils.GetFormValues("radbtnPowder").Length > 0)
                boolPowder = orderBll.SetOrderStatus(orderId, (PowderOrderStatus)Enum.Parse(typeof(PowderOrderStatus), Utils.GetFormValues("radbtnPowder")[0]), DateTime.Now);
            if (Utils.GetFormValues("radbtn").Length > 0)
                boolPayment = orderBll.SetPaymentStatus(orderId, (PaymentStatus)Enum.Parse(typeof(PaymentStatus), Utils.GetFormValues("radbtn")[0]));
            if (boolPowder && boolPayment)
            {
                Getmodelorder();
                MessageBox.ShowAndRedirect(Page, "修改成功", "FitList.aspx");
            }
            else
            {
                MessageBox.Show(Page, "修改失败");//, "FitOrderDetail.aspx?OrderId=" + orderId
                Getmodelorder();
            }
        }
        #endregion

        #region 删除数据
        protected void btndelete_Click(object sender, EventArgs e)
        {
            Getmodelorder();
            if (EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().DelTourOrder(modelMTourOrder.OrderId))
            {
                MessageBox.ShowAndRedirect(Page, "删除成功", "FitList.aspx");
            }
            else
            {
                MessageBox.Show(Page, "删除失败");
                Getmodelorder();
            }
        }
        #endregion
    }
}
