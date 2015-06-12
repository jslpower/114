using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.OpenStructure;
using EyouSoft.Common.Function;
using EyouSoft.Model.OpenStructure;

namespace IMFrame.EyouSoft1
{
    /// <summary>
    /// 易游通MQ整合易游通导航页面
    /// 创建者：曹胡生 2011/05/20
    /// </summary>
    public partial class WebForm1 : EyouSoft.ControlCommon.Control.MQPage
    {
        /// <summary>
        /// 公司基本信息业务实体
        /// </summary>
        private static IList<MCompanyInfo> MCompanyInfos = new List<MCompanyInfo>();

        /// <summary>
        /// 用户易游通权限列表
        /// </summary>
        private static int[] UserPermissions = null;

        /// <summary>
        /// 系统公司编号
        /// </summary>
        private static int SystemCompanyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 获取用户易游通权限列表
            if (Request["SystemUserId"] != null)
            {
                UserPermissions = BUser.CreateInstance().GetUserPermission(1, Convert.ToInt32(Request["SystemUserId"].ToString()));
            }

            // 获取公司基本信息业务实体
            MCompanyInfos = BCompany.CreateInstance().GetCompanyList(0, (int)EyouSoft.OpenRelation.Model.SystemType.Platform, SiteUserInfo.CompanyID);

            // 系统公司编号
            if (MCompanyInfos != null && MCompanyInfos.Count() > 0)
            {
                foreach (MCompanyInfo info in MCompanyInfos)
                {
                    // 属于易游通
                    if (((int)EyouSoft.OpenRelation.Model.SystemType.YYT).Equals(info.SystemCompanyType))
                    {
                        SystemCompanyId = info.SystemCompanyId;
                    }
                }
            }

            // 取得域名
            GetDomainName();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="o">权限</param>
        /// <returns>是否有权限</returns>
        public bool IsHavePower(string o)
        {
            string[] permissions = o.Split(',');

            if (UserPermissions == null)
            {
                return false;
            }

            foreach (var i in UserPermissions)
            {
                if (StringValidate.IsStringExists(i.ToString(), permissions))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 易游通域名
        /// </summary>
        public string DomainName
        {
            get;
            set;
        }

        /// <summary>
        /// 根据系统公司编号获得域名
        /// </summary>
        private void GetDomainName()
        {
            // 获取易游通处理程序资源URI
            var strUri = EyouSoft.OpenRelation.Utils.GetURI(EyouSoft.OpenRelation.Model.SystemType.YYT);

            // 判断易游通处理程序资源URI的合法性
            if (Uri.IsWellFormedUriString(strUri, UriKind.Absolute))
            {
                var uri = new Uri(strUri);

                this.DomainName = uri.AbsoluteUri.Replace(uri.AbsolutePath, string.Empty) + "/MqGoLogin.aspx";
            }
            else
            {
                // 默认易游通处理程序资源URI
                this.DomainName = "http://www.gocn.cn/MqGoLogin.aspx";
            }
        }

        /// <summary>
        /// 获得完整路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetFullPath(string path)
        {            
            return DomainName + string.Format("?ReturnUrl={3}&u={0}&p={1}&SystemCompanyId={2}", HttpUtility.UrlPathEncode(SiteUserInfo.UserName), SiteUserInfo.PassWordInfo.SHAPassword, SystemCompanyId, path);
        }
    }

    /// <summary>
    /// 模块权限编号
    /// </summary>
    public struct PowerFunc
    {
        public const string 发布计划 = "29,270,37,424,58,281,79,867,151,330,725,483,33,41,51,72,86,65,439";
        public const string 我要报价 = "322,325,449,314,515";
        public const string 预订报名 = "524,410,607";
        public const string 统计分析 = "175,649,438,647,648,660,661,921,922,923,924,925,926,927,928,929,930,659";
        public const string 销售收款 = "165";
        public const string 组团收入 = "100";
        public const string 地接收入 = "148";
        public const string 入境收入 = "172";
        public const string 我要付款 = "99,146,382";
        public const string 导游中心 = "854";
        public const string 行政中心 = "747";
        public const string 单项服务 = "319";
        public const string 计调安排 = "8,102,216,106,305,110,114,118,308,122,219,126,130,134,138,446";
    }

    /// <summary>
    /// 后缀路径
    /// </summary>
    public struct Path
    {
        public const string 个人中心 = "TravelAgency/RemindIndex/Index.aspx";
        public const string 发布计划 = "MqGoPages/PublishPlan.aspx?tab=0";
        public const string 我要报价 = "MqGoPages/PublisQuote.aspx?tab=2";
        public const string 预订报名 = "MqGoPages/TourApply.aspx?tab=1";
        public const string 统计分析 = "MqGoPages/StatisticsAnalysis.aspx?tab=6";
        public const string 销售收款 = "TravelAgency/SalesOrder/Default.aspx?tab=4";
        public const string 组团收入 = "TravelAgency/FinancialAffairs";
        public const string 地接收入 = "TravelAgency/FinancialAffairs/?FClass=2";
        public const string 入境收入 = "TravelAgency/FinancialAffairs/?FClass=3";
        public const string 我要付款 = "MqGoPages/Pay.aspx?tab=5";
        public const string 导游中心 = "TravelAgency/CiceroniCenter/ReserveCiceroniYieldInfo.aspx?tab=7";
        public const string 行政中心 = "TravelAgency/ExecutiveCenter/WorkInfoList.aspx?tab=8";
        public const string 单项服务 = "TravelAgency/SingleOperation/Default.aspx?tab=3";
        public const string 计调安排 = "TravelAgency/TourPlan/AllTourGroup.aspx";
    }
}
