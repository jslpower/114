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
namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 团队行程信息打印单
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class TeamInfoPrint : System.Web.UI.Page
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected string CompanyName = string.Empty;
        protected string TourContact = string.Empty;//联系人
        protected string TourContactTel = string.Empty;//联系人电话
        protected string License = string.Empty;//许可证号
        protected string CompanyAddress = string.Empty;//许可证号
        protected string RouteName = string.Empty;//线路名称
        protected string QuickPlanContent = string.Empty;
        private string TourID = string.Empty;

        protected string ResideContent = string.Empty;//住宿
        protected string SpeciallyNotice = string.Empty;//温馨提醒?友情提示
        protected string GuideContent=string.Empty;//导游
        protected string DinnerContent=string.Empty;//用餐
        protected string TrafficContent = string.Empty;//往返交通?大交通 
        protected string CarContent=string.Empty;//景交?用车

        protected string DoorPrice = string.Empty;//门票 
        protected string SiglePeplePrice = string.Empty;//报价
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["TourID"] != null && !string.IsNullOrEmpty(Request.QueryString["TourID"]))
            {
                TourID = Utils.InputText(Request.QueryString["TourID"]);
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        private void InitPage()
        {
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                #region 获取所属公司信息
                EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel= ICompanyInfobll.GetModel(model.CompanyID);
                CompanyName = companyInfoModel.CompanyName;
                CompanyAddress = companyInfoModel.CompanyAddress;
                License = companyInfoModel.License;
                ICompanyInfobll = null;
                companyInfoModel = null;
                #endregion
                List<EyouSoft.Model.TourStructure.TourPriceDetail> priceList = new List<EyouSoft.Model.TourStructure.TourPriceDetail>();
                priceList = (List<EyouSoft.Model.TourStructure.TourPriceDetail>)model.TourPriceDetail;
                #region 团队报价等级处理
                //用于数据绑定
                List<CPriceDetail> BindPriceList = new List<CPriceDetail>();
                CPriceDetail cModel = null;
                //该团队的报价等级明细                
                priceList.ForEach(delegate(EyouSoft.Model.TourStructure.TourPriceDetail item)
                {
                    cModel = new CPriceDetail();
                    cModel.PriceStandName = item.PriceStandName;
                    ((List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>)item.PriceDetail).ForEach(delegate(EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail itemPriceLeave)
                    {
                        switch (itemPriceLeave.CustomerLevelType) {
                            case EyouSoft.Model.CompanyStructure.CustomerLevelType.同行:
                                cModel.AdultPrice = itemPriceLeave.AdultPrice;
                                cModel.ChildrenPrice = itemPriceLeave.AdultPrice;
                                break;
                            case EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差:
                                cModel.SingleRoom = itemPriceLeave.ChildrenPrice;
                                break;
                        };
                    });
                    BindPriceList.Add(cModel);
                });
                BindPriceList = null;
                cModel = null;
                this.rptTourPriceDetail.DataSource = BindPriceList;
                this.rptTourPriceDetail.DataBind();
                #endregion
                if (((int)model.ReleaseType) == 0)
                {

                    rptStandardPlan.DataSource = model.StandardPlan;
                    rptStandardPlan.DataBind();
                }
                else
                {
                    QuickPlanContent = model.QuickPlan;
                }
            }
            Ibll = null;
            model = null;
        }
        /// <summary>
        /// 绑定报价等级:成人，儿童等；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptTourPriceDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
            }
        }
    }
    ///// <summary>
    ///// 转换价格等级数据，方便处理
    ///// </summary>
    //public class CPriceDetail
    //{
    //    /// <summary>
    //    /// 价格等级名称
    //    /// </summary>
    //    public string PriceStandName { get; set; }
    //    /// <summary>
    //    /// 成人价
    //    /// </summary>
    //    public decimal AdultPrice { get; set; }
    //    /// <summary>
    //    /// 儿童价
    //    /// </summary>
    //    public decimal ChildrenPrice { get; set; }
    //    /// <summary>
    //    /// 单房差
    //    /// </summary>
    //    public decimal SingleRoom { get; set; }
    //    /// <summary>
    //    /// 0：同行，1：门市
    //    /// </summary>
    //    public int CustomerLevelType { get; set; }
    //}
}
