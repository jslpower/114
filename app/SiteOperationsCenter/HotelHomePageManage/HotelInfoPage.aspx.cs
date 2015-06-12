using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.HotelHomePageManage
{
    /// <summary>
    /// 页面功能：运营后台---酒店首页管理----特价酒店管理
    /// BuildDate：2011-05-16
    /// </summary>
    /// Author:liuym
    public partial class HotelInfoPage : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        /// <summary>
        /// 修改编号
        /// </summary>
        protected string EditId = string.Empty;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["EditId"]))
            {
                EditId = Request.QueryString["EditId"];
            }
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店首页管理_特价酒店))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店首页管理_特价酒店, true);
                    return;
                }
                //初始化星级
                InitHotelRankList();               
                // 初始化表单
                InitForm();
            }
        }
        #endregion

        #region 初始化表单
        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitForm()
        {
            EyouSoft.Model.HotelStructure.MNewHotelInfo model = null;
     
            if (!string.IsNullOrEmpty(EditId))
                model = EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance().GetModel(int.Parse(EditId));

            if (model != null)
            {
                ddlAreaList.SelectedValue = model.CityName.ToString();
                txt_HotelName.Value = model.HotelName;
                int hotelRank = Convert.ToInt32(model.HotelStar);
                ddlHotelRank.SelectedValue = hotelRank.ToString();
                txt_SalesRoomPrice.Value = Utils.FilterEndOfTheZeroDecimal(model.MenShiPrice).ToString();
                txt_TourTeamPrice.Value = Utils.FilterEndOfTheZeroDecimal(model.TeamPrice).ToString();
                txt_OperateDate.Text = model.OperateTime.ToString("yyyy-MM-dd");
                txtQQ.Value = model.QQ;
           
                if(model.CityAreaType == EyouSoft.Model.HotelStructure.CityAreaType.港澳)
                    hCityAreaId.Value = ((int)EyouSoft.Model.HotelStructure.CityAreaType.港澳).ToString();
                else if (model.CityAreaType == EyouSoft.Model.HotelStructure.CityAreaType.华东五市)
                    hCityAreaId.Value = ((int)EyouSoft.Model.HotelStructure.CityAreaType.华东五市).ToString();               
                model = null;
            }
            else
            {
                txt_OperateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

        }
        #endregion

        #region 保存事件       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //执行是否成功
            bool Result = false;
            //城市名称
            string cityName = this.ddlAreaList.SelectedValue;
            //地区类型
            int areaId = Utils.GetInt(hCityAreaId.Value, 0);
            //酒店名称
            string hotelName = Utils.GetText(txt_HotelName.Value.Trim(), 100);
            //星级
            string hotelRank = ddlHotelRank.SelectedValue;
            //门市价
            decimal salseRoomPrice =Utils.GetDecimal(txt_SalesRoomPrice.Value.ToString(),0);
            //团队价
            decimal tourPrice = Utils.GetDecimal(this.txt_TourTeamPrice.Value.ToString(),0);
            //qq
            string qq = Utils.InputText(txtQQ.Value.ToString(),50);
            //发布时间
            DateTime CreateTime = DateTime.Parse(txt_OperateDate.Text);

            #region 实体赋值
            EyouSoft.Model.HotelStructure.MNewHotelInfo model = new EyouSoft.Model.HotelStructure.MNewHotelInfo();
            model.CityAreaType = (EyouSoft.Model.HotelStructure.CityAreaType)areaId;
            model.CityName = cityName;
            model.HotelName = Utils.InputText(hotelName);
            model.HotelStar = (EyouSoft.Model.HotelStructure.HotelStarType)int.Parse(hotelRank);
            model.HotelName = Utils.InputText(hotelName);
            model.MenShiPrice = salseRoomPrice;
            model.OperateId = MasterUserInfo.ID;
            model.OperateTime =DateTime.Parse(this.txt_OperateDate.Text);
            model.TeamPrice = tourPrice;
            model.QQ = qq;

            #endregion

            #region 验证执行添加或者修改
            if (!string.IsNullOrEmpty(EditId))
            {
                model.Id = int.Parse(EditId);
                Result = EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance().Update(model);
            }
            else
                Result = EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance().Add(model);
            #endregion

            #region 输出提醒信息
            if (Result)
                MessageBox.ShowAndRedirect(this, model.Id > 0 ? "修改成功！" : "添加成功！",
                    "/HotelHomePageManage/HotelManageList.aspx");
            else
                MessageBox.ShowAndRedirect(this, model.Id > 0 ? "修改失败！" : "添加失败！",
                    "/HotelHomePageManage/HotelManageList.aspx");
            #endregion
        }
        #endregion

        #region 绑定星级列表
        private void InitHotelRankList()
        {
            this.ddlHotelRank.Items.Add(new ListItem("请选择", ""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.HotelStructure.HotelStarType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlHotelRank.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.HotelStarType), str)).ToString()));
                }
            }
        }
        #endregion
    }
}
