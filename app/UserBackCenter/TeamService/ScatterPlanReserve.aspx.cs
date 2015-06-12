using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 用户后台 组团社 国内散拼计划 预定
    /// 李晓欢 2011-12-22
    /// </summary>
    public partial class ScatterPlanReserve : EyouSoft.Common.Control.BackPage
    {
        protected string StrMQUrl = string.Empty;
        protected string StrMQImg = string.Empty;
        protected string StrQQUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsLogin)
                {
                    EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
                    return;
                }

                string orderID = Utils.GetQueryStringValue("orderID");

               // EyouSoft.Model.TourStructure.TourOrder

                InitRouteInfo();

                InitTourInfo(orderID);
       
            }
        }

        #region 初始化团队信息
        protected void InitTourInfo(string orderID)
        { 
            //订单号
            //this.litOrderCode.Text = "";
            //线路名称
            //this.litRouteName.Text = "";
            //线路团队信息
            Utils.GetFormValue(this.ddlRouteList.UniqueID);
            //当前剩余空位
            this.litSYKW.Text = "";
            //状态
            this.litTourState.Text = "";
            //发布单位
            this.litCompanyName.Text = "";
            //mq
            StrMQUrl = Utils.GetShopUrl(SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.CompanyType.专线, -1);
            StrMQImg = Utils.GetMQ("");
            //qq
            StrQQUrl = "";
            //出发城市 
            this.litStartPlace.Text = "";
            //线路状态
            this.litRouteState.Text = "";
            //交通
            this.litStartTraffic.Text = "";
            this.litStartFlight.Text = "";
            //出发时间 航班
            this.litStartDate.Text = "";
            this.litEndFlight.Text = "";
            //返回时间 航班
            this.litEndDate.Text = "";
            this.litEndFlight.Text = "";
            //集合说明
            this.litSetexplain.Text = "";
            //领队全陪说明
            this.litManagexplain.Text = "";
            //游客联系人
            this.txtTourisContectName.Value = "";
            //联系电话
            this.txtTourisContectPhone.Value = "";
            //商家负责人
            this.txtMerchants.Value = "";
            //联系电话
            this.txtMerchantsPhone.Value = "";
            //价格组成 成人 儿童 单房差
            this.txtAdultPrice.Value = "";
            this.txtChildrenPrice.Value = "";
            this.txtRoomSent.Value = "";
        }
        #endregion

        #region 初始化线路信息
        protected void InitRouteInfo()
        {
            this.ddlRouteList.Items.Clear();
            this.ddlRouteList.DataSource = "";
            this.ddlRouteList.DataBind();
        }
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
