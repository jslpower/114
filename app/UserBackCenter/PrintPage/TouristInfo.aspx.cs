using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.TicketStructure;
using System.Text;
using EyouSoft.Model.NewTourStructure;
namespace UserBackCenter.PrintPage
{
    /// <summary>
    ///  团队人员名单打印页面
    ///  创建时间： 2011-12-23   方琪
    /// </summary>
    public partial class TouristInfo : EyouSoft.Common.Control.BasePage
    {
        #region 页面变量
        /// <summary>
        /// 序号
        /// </summary>
        protected int count = 0;
        /// <summary>
        /// 团队人数不为0
        /// </summary>
        protected bool isTrue = false;
        #endregion
        
        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "团队人员名单";
            string tourId = Utils.GetQueryStringValue("TeamId");
            string orderStatus = Utils.GetQueryStringValue("OrderStatus");
            InitPage(tourId, orderStatus);
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void InitPage(string tourID, string orderStatus)
        {
            EyouSoft.Model.NewTourStructure.MPowderList PowderListModel =
                EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(tourID);
            if (PowderListModel != null)
            {
                //线路名称
                this.RouteName.Text = PowderListModel.RouteName;
                this.LeaveDate.Text = PowderListModel.LeaveDate.ToString("yyyy-MM-dd");
                //出团时间
                this.GroupNo.Text = PowderListModel.TourNo;
                //线路名称
                this.RouteName1.Text = PowderListModel.RouteName;
                //出团时间
                this.LeaveTime.Text = PowderListModel.LeaveDate.ToString("yyyy-MM-dd");

                //备注
                this.Remark.Value = Utils.TextToHtml(PowderListModel.TourNotes);
            }
            IList<EyouSoft.Model.NewTourStructure.MTourOrderCustomer> list = null;
            PowderOrderStatus?[] arryOrderStatus = null;
            //游客信息绑定
            if (orderStatus == "lishi")
            {
                //历史团队名单
                arryOrderStatus = new PowderOrderStatus?[2];
                arryOrderStatus[0] = PowderOrderStatus.结单;
                arryOrderStatus[1] = PowderOrderStatus.专线商已确定;
                list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderCustomerByTourId(tourID, this.SiteUserInfo.CompanyID, CompanyType.专线, arryOrderStatus);

            }
            else
            {
                //散拼计划人员名单
                arryOrderStatus = new PowderOrderStatus?[1];
                arryOrderStatus[0] = PowderOrderStatus.专线商已确定;
                list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderCustomerByTourId(tourID, this.SiteUserInfo.CompanyID, CompanyType.专线, arryOrderStatus);
            }

            if (list != null && list.Count > 0)
            {
                //实际人数
                this.FactNo.Text = list.Count.ToString();
                isTrue = true;
                this.TouristInfomation.DataSource = list;
                this.TouristInfomation.DataBind();
            }

        }
        #endregion

        #region 获取证件信息
        /// <summary>
        /// 获取证件信息
        /// </summary>
        /// <param name="IdentityCard">身份证</param>
        /// <param name="Passport">护照</param>
        /// <param name="OtherCard">其他证件</param>
        /// <returns>证件信息</returns>
        protected string GetCard(Object IdentityCard, Object Passport, Object OtherCard)
        {
            StringBuilder sb = new StringBuilder();
            if (IdentityCard != null && IdentityCard.ToString().Trim() != "")
            {
                sb.Append("身份证：<br/>" + IdentityCard.ToString() + "<br/>");
            }
            if (Passport != null && Passport.ToString().Trim() != "")
            {
                sb.Append("护照：<br/>" + Passport.ToString() + "<br/>");
            }
            if (OtherCard != null && OtherCard.ToString().Trim() != "")
            {
                sb.Append("其他证件：<br/>" + OtherCard.ToString() + "<br/>");
            }
            return sb.ToString();
        }
        #endregion

        #region 序号
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns>序号</returns>
        protected int GetCount()
        {
            return ++count;
        }
        #endregion

    }
}
