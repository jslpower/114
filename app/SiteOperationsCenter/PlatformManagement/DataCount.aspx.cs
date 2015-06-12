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
using System.Text;
using EyouSoft.Common.Function;
using System.Collections.Generic;
using EyouSoft.Common;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——统计数据维护
    /// 开发人：杜桂云      开发时间：2010-08-09
    /// </summary>
    public partial class DataCount : EyouSoft.Common.Control.YunYingPage
    {
        protected bool isManageGrant = true;
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            ////权限判断
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_统计数据维护 };
            if (!CheckMasterGrant(parms))
            {
                //isManageGrant = false;
                //this.btn_Save.Visible = false;
                Utils.ResponseNoPermit(YuYingPermission.平台管理_统计数据维护, true);
                return;
            }
            if (!IsPostBack)
            {
                //初始化相关绑定
                this.BindInfo();
            }
        }
        #endregion

        #region 绑定数据
       /// <summary>
       /// 初始化数据绑定
       /// </summary>
        protected void BindInfo()
        {
            EyouSoft.Model.SystemStructure.SummaryCount model = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            if (model != null)
            {
                this.lblCarCount.Text = model.Car.ToString();
                this.lblHotelCount.Text = model.Hotel.ToString();
                this.lblJourneyCount.Text = model.TravelAgency.ToString();
                this.lblRouteCount.Text = model.Route.ToString();
                this.lblShopCount.Text = model.Shop.ToString();
                this.lblSightCount.Text = model.Sight.ToString();
                this.lblUserCount.Text = model.User.ToString();
                this.lblIntemediCount.Text = model.Intermediary.ToString();
                this.lblMQ_Count.Text = model.MQUser.ToString();
                this.txtIntemediCount.Text = model.IntermediaryVirtual.ToString();
                this.txtUserCount.Text = model.UserVirtual.ToString();
                this.txtCityCount.Text = model.CityRouteVirtual.ToString();
                this.txtWorldCount.Text = model.RouteVirtual.ToString();
                this.txtFactCarCount.Text = model.CarVirtual.ToString();
                this.txtFactHotelCount.Text = model.HotelVirtual.ToString();
                this.txtFactJourCount.Text = model.TravelAgencyVirtual.ToString();
                this.txtFactShopCount.Text = model.ShopVirtual.ToString();
                this.txtFactSightCount.Text = model.SightVirtual.ToString();
                this.txtMQCount.Text = model.MQUserVirtual.ToString();
                this.lblFeightCount.Text = model.TicketFreight.ToString();
                this.txtFeightCount.Text = model.TicketFreightVirtual.ToString();
                this.lblBuyers.Text = model.Buyer.ToString();
                this.txtBuyersVirtual.Text = model.BuyerVirtual.ToString();
                this.lblSuppliers.Text = model.Suppliers.ToString();
                this.txtSuppliersVirtual.Text = model.SuppliersVirtual.ToString();
                this.lblSupplyInfos.Text = model.SupplyInfos.ToString();
                this.txtSupplyInfosVirtual.Text = model.SupplyInfosVirtual.ToString();
                this.lblScenic.Text = model.Scenic.ToString();
            }
            model = null;
        }
        #endregion

        #region 提交操作
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (isManageGrant)
            {
                EyouSoft.Model.SystemStructure.SummaryCount model = new EyouSoft.Model.SystemStructure.SummaryCount();
                //获取表单数据
                int CityCount = Utils.GetInt(Utils.GetFormValue(this.txtCityCount.UniqueID), 0);
                int WorldCount = Utils.GetInt(Utils.GetFormValue(this.txtWorldCount.UniqueID), 0);
                int CarCount = Utils.GetInt(Utils.GetFormValue(this.txtFactCarCount.UniqueID), 0);
                int HotelCount = Utils.GetInt(Utils.GetFormValue(this.txtFactHotelCount.UniqueID), 0);
                int JourCount = Utils.GetInt(Utils.GetFormValue(this.txtFactJourCount.UniqueID), 0);
                int ShopCount = Utils.GetInt(Utils.GetFormValue(this.txtFactShopCount.UniqueID), 0);
                int SightCount = Utils.GetInt(Utils.GetFormValue(this.txtFactSightCount.UniqueID), 0);
                int UserCount = Utils.GetInt(Utils.GetFormValue(this.txtUserCount.UniqueID), 0);
                int InteCount = Utils.GetInt(Utils.GetFormValue(this.txtIntemediCount.UniqueID), 0);
                int MQCount = Utils.GetInt(Utils.GetFormValue(this.txtMQCount.UniqueID), 0);
                int FeightCount = Utils.GetInt(Utils.GetFormValue(this.txtFeightCount.UniqueID), 0);
                int BuyersCount = Utils.GetInt(Utils.GetFormValue(this.txtBuyersVirtual.UniqueID), 0);
                int SuppliersCount = Utils.GetInt(Utils.GetFormValue(this.txtSuppliersVirtual.UniqueID), 0);
                int SupplyInfosCount = Utils.GetInt(Utils.GetFormValue(this.txtSupplyInfosVirtual.UniqueID), 0);
                int ScenicCount = Utils.GetInt(Utils.GetFormValue(this.txtScenicVirtual.UniqueID), 0);

                //保存到实体类
                model.CityRouteVirtual = CityCount;
                model.RouteVirtual = WorldCount;
                model.CarVirtual = CarCount;
                model.SightVirtual = SightCount;
                model.HotelVirtual = HotelCount;
                model.TravelAgencyVirtual = JourCount;
                model.ShopVirtual = ShopCount;
                model.UserVirtual = UserCount;
                model.IntermediaryVirtual = InteCount;
                model.MQUserVirtual = MQCount;
                model.TicketFreightVirtual = FeightCount;
                model.BuyerVirtual = BuyersCount;
                model.SuppliersVirtual = SuppliersCount;
                model.SupplyInfosVirtual = SupplyInfosCount;
                model.ScenicVirtual = ScenicCount;

                //修改虚拟数量
                bool isFlag = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().SetSummaryCount(model);
                if (isFlag)
                {
                    MessageBox.ShowAndRedirect(this, "操作成功", "DataCount.aspx");
                }
                model = null;
            }
        }
        #endregion
    }
}
