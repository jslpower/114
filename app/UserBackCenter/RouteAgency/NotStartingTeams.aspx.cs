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

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 未出发团队
    /// 创建者：luofx 时间：2010-7-8
    /// </summary>
    public partial class NotStartingTeams : EyouSoft.Common.Control.BackPage
    {
        private string CompanyID = string.Empty;
        private int[] AreaId;
        protected int CurrentAreaId = 0;
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected bool IsGrantUpdate = false;
        protected bool IsGrantDelete = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路管理))
            {
                Response.Clear();
                Response.Write("对不起，你正在使用的帐号没有操作改页面的权限！");
                Response.End();
            }
            IsGrantUpdate = true;// this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路管理);
            IsGrantDelete = true;//this.CheckGrant(EyouSoft.Common.TravelPermission.专线_未出团计划_删除);
            AreaId = this.SiteUserInfo.AreaId;
            CompanyID = this.SiteUserInfo.CompanyID;
            if (Request.QueryString["AreaId"] != null)
            {
                CurrentAreaId = Utils.GetInt(Request.QueryString["AreaId"]);
            }
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "setTourMarkerNote")
            {
                SetTourMarkerNote();
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void InitPage()
        {
            List<EyouSoft.Model.SystemStructure.SysArea> AreaList = (List<EyouSoft.Model.SystemStructure.SysArea>)EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(AreaId);
            rptSysArea.DataSource = AreaList;
            rptSysArea.DataBind();
            if (AreaList != null && AreaList.Count > 0 && CurrentAreaId == 0)
            {
                CurrentAreaId = AreaList[0].AreaId;
            }
            AreaList = null;
        }
        /// <summary>
        /// 设置团队类型,即推广状态
        /// </summary>
        private void SetTourMarkerNote()
        {
            if (!IsGrantUpdate)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'对不起，你当前登录的帐号没有权限执行该操作！'}]");
                Response.End();
            }
            EyouSoft.Model.TourStructure.TourSpreadState SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.None;
            switch (Utils.GetQueryStringValue("TourMarkerNote"))
            {
                case "1"://推荐精品 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.推荐精品;
                    break;
                case "2"://促销 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.促销;
                    break;
                case "3"://最新 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.最新;
                    break;
                case "4"://品质 
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.品质;
                    break;
                case "5"://纯玩  
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.纯玩;
                    break;
                default://none
                    SpreadState = EyouSoft.Model.TourStructure.TourSpreadState.None;
                    break;
            }
            string TemplateTourID = Utils.GetQueryStringValue("TemplateTourID");
            //推广说明
            string Description = Utils.GetQueryStringValue("Description");
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            bool isTrue = Ibll.SetTemplateTourSpreadState(SpreadState, Description, TemplateTourID);
            Ibll = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("1");
                Response.End();
            }
        }
    }
}
