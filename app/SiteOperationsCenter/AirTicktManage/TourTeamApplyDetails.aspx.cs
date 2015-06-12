using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.AirTicktManage
{
    /// <summary>
    /// 页面功能：运营后台--机票管理---团队申请票详细信息页
    /// CreateDate:2011-05-24
    /// </summary>
    /// Author:liuym
    public partial class TourTeamApplyDetails : EyouSoft.Common.Control.YunYingPage
    {
        protected string TourID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.机票首页管理_团队票申请管理))
            {
                Utils.ResponseNoPermit(YuYingPermission.机票首页管理_团队票申请管理, true);
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["TourId"]))
            {
                TourID = Request.QueryString["TourId"];
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(TourID))
                {
                    EyouSoft.Model.TicketStructure.GroupTickets Model = EyouSoft.BLL.TicketStructure.GroupTickets.CreateInstance().GetGroupTicket(int.Parse(TourID));
                    if(Model!=null)
                    {
                        // 行程类型
                        ltrJourneyType.Text = Model.GroupType.ToString();
                        //开始城市
                        ltrStartCity.Text = Model.StartCity;
                        //结束城市
                        ltrEndCity.Text = Model.EndCity;
                        //乘机人数
                        ltrPersonCount.Text = Model.PelopeCount.ToString();
                        //联系电话
                        ltrTelephone.Text = Model.Phone;
                        //航空公司
                        ltrPlaneCompany.Text = string.IsNullOrEmpty(Model.AirlinesType.ToString()) || Model.AirlinesType.ToString() == "0" ? "无" : Model.AirlinesType.ToString();
                        //去程航班号
                        ltrToPlaneNo.Text = Model.FlightNumber;
                        //出发时间
                        ltrStarDate.Text = Model.StartTime;
                        //时间范围
                        ltrRangeTime.Text = Model.TimeRange;
                        //备注
                        ltrNotes.Text = Model.Notes;
                        //联系人
                        ltrContactName.Text = Model.Contact;
                        //联系qq
                        ltrContactQQ.Text = Model.ContactQQ;
                        //希望价格
                        ltrWishPirce.Text =Utils.FilterEndOfTheZeroDecimal(Model.HopesPrice);
                        //Email地址
                        ltrEmail.Text = Model.Email;
                        //乘客信息集合
                        IList<EyouSoft.Model.TicketStructure.PassengerInformation> list = Model.PassengerInformationList;
                        if(list!=null&&list.Count>0)
                        {
                            this.crp_PassongerList.DataSource = list;
                            this.crp_PassongerList.DataBind();
                        }
                    }
                }
            }
        }
    }
}
