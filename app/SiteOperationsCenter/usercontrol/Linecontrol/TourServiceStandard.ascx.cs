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
using EyouSoft.Common;

namespace UserBackCenter.usercontrol.Linecontrol
{
    /// <summary>
    /// 标准发布服务标准
    /// 罗丽娥   2010-06-25
    /// </summary>
    public partial class TourServiceStandard : System.Web.UI.UserControl
    {
        #region 自定义变量
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected string tmpResideContent = string.Empty, tmpDinnerContent = string.Empty, tmpSightContent = string.Empty, tmpCarContent = string.Empty, tmpGuideContent = string.Empty, tmpTrafficContent = string.Empty, tmpIncludeOtherContent = string.Empty;
        /// <summary>
        /// 页面最外层的tableid
        /// </summary>
        private string _containerid;
        public string ContainerID
        {
            set { _containerid = value; }
            get { return _containerid; }
        }
        /// <summary>
        /// 公司id
        /// </summary>
        private string _companyid;
        public string Companyid
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 联系人ID
        /// </summary>
        private string _operatorid;
        public string OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 判断是团队和线路的发布类型
        /// </summary>
        private string _releasetype;
        public string ReleaseType
        {
            set { _releasetype = value; }
            get { return _releasetype; }
        }

        private EyouSoft.Model.TourStructure.TourServiceStandard _tourSservicestandard = null;
        /// <summary>
        /// 团队服务标准
        /// </summary>
        public EyouSoft.Model.TourStructure.TourServiceStandard TourServiceStandardInfo
        {
            set { _tourSservicestandard = value; }
            get { return _tourSservicestandard; }
        }

        private EyouSoft.Model.NewTourStructure.MServiceStandard _routeSservicestandard = null;
        /// <summary>
        /// 线路服务标准
        /// </summary>
        public EyouSoft.Model.NewTourStructure.MServiceStandard RouteServiceStandardInfo
        {
            set { _routeSservicestandard = value; }
            get { return _routeSservicestandard; }
        }
        private string _moduletype;
        /// <summary>
        /// 判断是线路还是团队
        /// </summary>
        public string ModuleType
        {
            set { _moduletype = value; }
            get { return _moduletype; }
        }
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ModuleType == "tour")
            {
                InitTourStandardService();
            }
            else {
                InitRouteStandardService();
            }
        }

        #region 初始化团队服务标准
        /// <summary>
        /// 初始化团队服务标准
        /// </summary>
        /// <param name="ID"></param>
        private void InitTourStandardService()
        {
            if (TourServiceStandardInfo != null)
            {
                tmpResideContent = TourServiceStandardInfo.ResideContent;
                tmpDinnerContent = TourServiceStandardInfo.DinnerContent;
                tmpSightContent = TourServiceStandardInfo.SightContent;
                tmpCarContent = TourServiceStandardInfo.CarContent;
                tmpGuideContent = TourServiceStandardInfo.GuideContent;
                tmpTrafficContent = TourServiceStandardInfo.TrafficContent;
                tmpIncludeOtherContent = TourServiceStandardInfo.IncludeOtherContent;
            }
            TourServiceStandardInfo = null;
        }
        #endregion

        #region 初始化线路服务标准
        /// <summary>
        /// 初始化线路服务标准
        /// </summary>
        /// <param name="ID"></param>
        private void InitRouteStandardService()
        {
            if (RouteServiceStandardInfo != null)
            {
                tmpResideContent = RouteServiceStandardInfo.ResideContent;
                tmpDinnerContent = RouteServiceStandardInfo.DinnerContent;
                tmpSightContent = RouteServiceStandardInfo.SightContent;
                tmpCarContent = RouteServiceStandardInfo.CarContent;
                tmpGuideContent = RouteServiceStandardInfo.GuideContent;
                tmpTrafficContent = RouteServiceStandardInfo.TrafficContent;
                tmpIncludeOtherContent = RouteServiceStandardInfo.IncludeOtherContent;
            }
            RouteServiceStandardInfo = null;
        }
        #endregion
    }
}