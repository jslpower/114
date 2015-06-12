using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// 酒店订单查询
    /// 开发人：孙川  时间：2010-12-08
    /// </summary>
    public partial class OrderSearch : YunYingPage
    {
        string ReturnUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Utils.GetQueryStringValue("method");
            string BuyerCId = Utils.GetQueryStringValue("BuyerCId");
            string HotelOrderID=Utils.GetQueryStringValue("HotelOrderID");
            string State=Utils.GetQueryStringValue("State");
            string CheckState = Utils.GetQueryStringValue("CheckState");

            bool isManager = CheckMasterGrant(EyouSoft.Common.YuYingPermission.酒店后台管理_订单管理);
            if (isManager)
            {
                if (!IsPostBack)
                {
                    ReturnUrl = this.Request.Url.ToString();
                    MessageBox.ResponseScript(this.Page, "OrderSearch.OnSearch();");
                    if (method == "GetComapnyDetailInfo" && BuyerCId != "")
                    {
                        Response.Clear();
                        Response.Write(GetBuyerCompanyInfo(BuyerCId));
                        Response.End();
                    }
                    if (method == "SetHotelOrderState" && HotelOrderID != "" && State != "")
                    {
                        Response.Clear();
                        Response.Write(SetHotleOrderState(HotelOrderID, State));
                        Response.End();
                    }
                    if (method == "SetHotelCheckState" && HotelOrderID != "" && CheckState != "")
                    {
                        Response.Clear();
                        Response.Write(SetHotelCheckState(HotelOrderID, CheckState));
                        Response.End();
                    }
                }
                
            }
            else
                Utils.ResponseNoPermit();
        }

        /// <summary>
        /// 获取采购用户的详细信息
        /// </summary>
        protected string GetBuyerCompanyInfo(string BuyerCId)
        {
            StringBuilder strCompanyDetail = new StringBuilder();
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo ModelCDetailInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(BuyerCId);
            if (ModelCDetailInfo != null)
            {
                strCompanyDetail.Append(string.Format("用户名:{0}<br/>", ModelCDetailInfo.ContactInfo.ContactName));
                strCompanyDetail.Append(string.Format("固定联系电话:{0}<br/>", ModelCDetailInfo.ContactInfo.Tel));
                strCompanyDetail.Append(string.Format("手机:{0}<br/>", ModelCDetailInfo.ContactInfo.Mobile));
                strCompanyDetail.Append(string.Format("公司名称:{0}<br/>", ModelCDetailInfo.CompanyName));
                strCompanyDetail.Append(string.Format("地址:{0}<br/>", ModelCDetailInfo.CompanyAddress));
            }
            else
                strCompanyDetail.Append("");

            return strCompanyDetail.ToString();
        }

        /// <summary>
        /// 订单状态修改
        /// </summary>
        public bool SetHotleOrderState(string strHotleOrderID, string state)
        {
            EyouSoft.BLL.HotelStructure.HotelOrder BllHotelOrder = new EyouSoft.BLL.HotelStructure.HotelOrder();
            bool result = false;
            switch (state)
            {
                case "RES":
                    result = BllHotelOrder.SetOrderState(strHotleOrderID, EyouSoft.HotelBI.HBEResStatus.RES);
                    break;
                case "CON":
                    result = BllHotelOrder.SetOrderState(strHotleOrderID, EyouSoft.HotelBI.HBEResStatus.CON);
                    break;
                case "CAN":
                    result = BllHotelOrder.SetOrderState(strHotleOrderID, EyouSoft.HotelBI.HBEResStatus.CAN);
                    break;
                default: break;
            }

            return result;
        }

        /// <summary>
        /// 审核状态修改
        /// </summary>
        public bool SetHotelCheckState(string strHotelOrderID, string CheckState)
        {
            int nCheckState = Utils.GetInt(CheckState, -1);
            if (nCheckState != -1)
            {
                EyouSoft.BLL.HotelStructure.HotelOrder BllHotelOrder = new EyouSoft.BLL.HotelStructure.HotelOrder();
                EyouSoft.Model.HotelStructure.CheckStateList status = (EyouSoft.Model.HotelStructure.CheckStateList)nCheckState;
                return BllHotelOrder.SetCheckState(strHotelOrderID, status);
            }
            else
                return false;
        }
    }
}
