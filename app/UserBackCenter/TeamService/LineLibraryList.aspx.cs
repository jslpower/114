using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using System.Text;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.BLL.CompanyStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团-旅游线路库
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-14
    public partial class LineLibraryList : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 显示国内，显示国际，显示周边
        /// </summary>
        protected bool showGN = false, showGJ = false, showZB = false;
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 线路库线路区域类型,线路库线路区域Id
        /// </summary>
        protected string type = string.Empty, lineId = string.Empty, page = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (!CheckGrant(TravelPermission.组团_线路团队订单管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            Key = "LineLibraryList" + Guid.NewGuid().ToString();
            type = Utils.GetQueryStringValue("type");
            lineId = Utils.GetQueryStringValue("lineId");
            page = Utils.GetQueryStringValue("page");
            if (!IsPostBack)
            {
                InitPage();
                BindLineArea();
                BindCity();
                BindLeaveMonth();

            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            txt_keyWord.Value = Utils.GetQueryStringValue("keyWord");
            txt_goCity.Value = Utils.GetQueryStringValue("goCity");


        }
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindLineArea()
        {
            //用户Id
            string UserID = string.Empty; ;
            if (SiteUserInfo != null)
            {
                EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = SiteUserInfo;

                UserID = UserInfoModel.ID ?? "0";
            }
            IList<SysArea> ls = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList();
            if (ls != null && ls.Count > 0)
            {


                //国内
                List<SysArea> gn = ls.Where(ty => ty.RouteType == AreaType.国内长线).ToList();
                //国际
                List<SysArea> gj = ls.Where(ty => ty.RouteType == AreaType.国际线).ToList();
                //周边(根据城市编号取周边的线路区域)
                IList<SysAreaSiteControl> zb = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(SiteUserInfo.CityId);
                if (gn != null && gn.Count > 0)
                {
                    showGN = true;
                    rpt_gn.DataSource = gn;
                    rpt_gn.DataBind();
                }
                if (gj != null && gj.Count > 0)
                {
                    showGJ = true;
                    rpt_gj.DataSource = gj;
                    rpt_gj.DataBind();
                }
                if (zb != null && zb.Count > 0)
                {
                    showZB = true;
                    rpt_zb.DataSource = zb;
                    rpt_zb.DataBind();
                }
            }
        }

        /// <summary>
        /// 绑定出发返回城市
        /// </summary>
        private void BindCity()
        {
            IList<EyouSoft.Model.SystemStructure.CityBase> list = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanyPortCity(SiteUserInfo.CompanyID);
            if (list != null && list.Count > 0)
            {
                //出发城市
                rpt_goCity.DataSource = list;
                rpt_goCity.DataBind();

            }
        }
        /// <summary>
        /// 绑定出团月份
        /// </summary>
        private void BindLeaveMonth()
        {
            IList<string> lsDateMonth = new List<string>();
            DateTime dateTiem = DateTime.Now;
            for (int i = 0; i < 6; i++)
            {
                lsDateMonth.Add(dateTiem.AddMonths(i).ToString("yyyy-MM"));
            }
            sel_goTime.AppendDataBoundItems = true;
            sel_goTime.DataSource = lsDateMonth;
            sel_goTime.DataBind();
        }

    }
}
