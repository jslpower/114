using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using EyouSoft.ControlCommon.Control;
namespace IMFram.TourAgency.TourManger
{
    /// <summary>
    /// 页面功能:零售商端 团队,线路区域列表
    /// 修改时间:2010-08-14 修改人:xuty
    /// </summary>
    public partial class TourAreaList : MQPage
    {
     
        protected string cityId = "0";//销售城市ID
        protected int companyId = 0;//公司ID
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {
                Response.Clear();
                Response.Write("对不起，你未开通组团服务！");
                Response.End();
                return;
            }
            //if (!CheckGrant(EyouSoft.Common.TravelPermission.组团_管理栏目))
            //{
            //    Response.Clear();
            //    Response.Write("对不起，你没有权限！");
            //    Response.End();
            //    return;
            //}
            if (SiteUserInfo!= null)//当前登录用户是否为空
            {
                //if (CheckGrant(EyouSoft.Common.TravelPermission.组团_管理栏目, EyouSoft.Common.TravelPermission.组团_线路采购预定))//如果有权限则显示‘修改我的批发商’链接
                if (CheckGrant(EyouSoft.Common.TravelPermission.组团_线路采购预定))
                {
                    this.a_SettionCompany.Visible = true;
                }
                else
                {
                    this.a_SettionCompany.Visible = false;
                }
                cityId = SiteUserInfo.CityId.ToString();//默认初始化时取用户公司所在城市
                SiteSelect1.CityId = SiteUserInfo.CityId;//为当前销售城市初始化用户公司所在城市
                IMTop1.MQLoginId = MQLoginId;
                IMTop1.Password = Password;
            }
          
          
        }
    }
}
