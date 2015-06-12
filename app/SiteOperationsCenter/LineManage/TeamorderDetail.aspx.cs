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
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.CompanyStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 团队订单统计修改
    /// 蔡永辉 2011-12-23
    /// </summary>
    public partial class TeamorderDetail : EyouSoft.Common.Control.YunYingPage
    {
        protected string TourId = "";//团号
        protected string OrderId = "";//订单号
        protected MTourList modelMTourList = null;
        protected CompanyDetailInfo modelCompany = null;
        protected StringBuilder strStatus = new StringBuilder("");
        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("TourId");
            GetModelByOrderNoId();
        }


        #region 获取订单状态
        protected void GetStatues(TourOrderStatus? Paymentstu)
        {
            List<EnumObj> listEnumObj = EnumObj.GetList(typeof(TourOrderStatus));
            foreach (EnumObj item in listEnumObj)
            {
                if (Paymentstu == (TourOrderStatus)Enum.Parse(typeof(TourOrderStatus), item.Value))
                    strStatus.AppendFormat("<input type=\"radio\" checked=\"checked\" name=\"radbtn\" value={0} />{1}", item.Value, item.Text);
                else
                    strStatus.AppendFormat("<input type=\"radio\" name=\"radbtn\" value={0} />{1}", item.Value, item.Text);
            }
        }

        #endregion

        #region 获取订单实体
        protected void GetModelByOrderNoId()
        {
            if (!string.IsNullOrEmpty(TourId))
            {
                modelMTourList = EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance().GetModel(TourId);
                if (modelMTourList == null)
                {
                    modelMTourList = new MTourList();
                }
                else
                {
                    GetStatues(modelMTourList.OrderStatus);
                    #region 获取专线商联系人
                    modelCompany = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(modelMTourList.Business);
                    if (modelCompany == null)
                        modelCompany = new CompanyDetailInfo();
                    else
                    {
                        ContactInfo.Text = modelCompany.ContactInfo.ContactName + "    电话：" + modelCompany.ContactInfo.Tel;
                    }
                    #endregion

                    MRoute modelMroute = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(modelMTourList.RouteId);
                    litPriceTeam.Text = modelMroute.IndependentGroupPrice.ToString("0.00");

                    #region 获取游客联系人

                    litVisitorContact.Text = modelMTourList.VisitorContact + "    电话：" + modelMTourList.VisitorTel;

                    #endregion
                }
            }

        }
        #endregion

        #region  保存数据
        protected void btnsanve_Click(object sender, EventArgs e)
        {
            if (Utils.GetFormValues("radbtn").Length > 0)
            {
                TourOrderStatus stu = (TourOrderStatus)Enum.Parse(typeof(TourOrderStatus), Utils.GetFormValues("radbtn")[0]);
                if (EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance().TourOrderStatusChange(stu, TourId))
                {
                    MessageBox.ShowAndRedirect(Page, "修改成功", "/LineManage/TeamorderManage.aspx?TourId=" + TourId);
                }
                else
                    MessageBox.ShowAndRedirect(Page, "修改失败", "/LineManage/TeamorderDetail.aspx?TourId=" + TourId);

            }
            else
            {
                MessageBox.ShowAndRedirect(Page, "没有修改状态", "/LineManage/TeamorderDetail.aspx?TourId=" + TourId);
            }
        }
        #endregion
    }
}
