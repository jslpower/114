using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.NewsStructure;
using EyouSoft.BLL.NewsStructure;

namespace UserBackCenter.usercontrol
{
    /// <summary>
    /// 我的专线
    /// </summary>
    public partial class ILine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_setareatype == null)
                {
                    ICompanyUser companyUserBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
                    EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = companyUserBLL.GetModel(UserId);
                    if (companyUserModel != null && companyUserModel.Area != null && companyUserModel.Area.Count > 0)
                    {
                        rpt_line1.DataSource = companyUserModel.Area;
                    }
                }
                else
                {
                    switch (_setareatype)
                    {
                        case AreaType.国际线:
                            rpt_line1.DataSource = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
                            break;
                        case AreaType.国内长线:
                            rpt_line1.DataSource = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetLongAreaSiteControl(ProvinceID);
                            break;
                        case AreaType.地接线路:
                        case AreaType.国内短线:
                            {
                                rpt_line1.DataSource = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(SiteUserInfo_CityId);
                            }
                            break;
                    }
                }
                rpt_line1.DataBind();
                rpt_line2.DataSource = rpt_line1.DataSource;
                rpt_line2.DataBind();
                int recordCount = 0;
                //绑定同业资讯
                if (IsTongYe)
                {
                    MQueryPeerNews queryModel = new MQueryPeerNews();
                    switch (_setareatype)
                    {
                        case AreaType.国际线:
                            queryModel.AreaType = AreaType.国际线;
                            break;
                        case AreaType.国内长线:
                            queryModel.AreaType = AreaType.国内长线;
                            break;
                        case AreaType.地接线路:
                        case AreaType.国内短线:
                            queryModel.AreaType = AreaType.国内短线;
                            break;
                    }
                    IList<MPeerNews> lst = BPeerNews.CreateInstance().GetGetPeerNewsList(10, queryModel);
                    if (null != lst && lst.Count > 0)
                    {
                        this.RepList.DataSource = lst;
                        this.RepList.DataBind();
                    }
                    else
                    {
                        IsTongYe = false;
                    }
                }
            }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        private string _title = "我的专线";
        /// <summary>
        /// 获取或设置标题,默认：我的专线
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _selectfunctionname = "GetList";
        /// <summary>
        /// 获取或设置查询方法名称(js)
        /// </summary>
        /// 设置时建议去掉方法后面带着的括号,js参数在html中提供接口
        public string SelectFunctionName
        {
            get
            {
                //过滤方法后面的括号
                if (_selectfunctionname.IndexOf('(') > 0)
                {
                    return _selectfunctionname.Substring(0, _selectfunctionname.IndexOf('(')) + "()";
                }
                return _selectfunctionname + "()";
            }
            set { _selectfunctionname = value; }
        }
        private string _key = string.Empty;
        /// <summary>
        /// 用户控件唯一标识
        /// </summary>
        public string Key
        {
            get
            {
                if (_key.Length > 0)
                {
                    return _key;
                }
                throw new ArgumentException("用户控件ILine,未指定Key"); //抛出异常

            }

            set
            {
                _key = value;
            }
        }
        private string _userid = string.Empty;
        /// <summary>
        /// 获取或设置用户Id
        /// </summary>
        public string UserId
        {
            get
            {
                if (_userid.Length > 0)
                {
                    return _userid;
                }
                throw new ArgumentException("用户控件ILine,未指定UserId"); //抛出异常
            }
            set { _userid = value; }
        }
        /// <summary>
        /// 获取或设置选择项ID
        /// </summary>
        public string CheckedId
        {
            get;
            set;
        }
        private AreaType? _setareatype = null;
        /// <summary>
        /// 设置线路区域,默认为全部
        /// </summary>
        public AreaType SetAreaType
        {
            set { _setareatype = value; }
        }
        /// <summary>
        /// 获取或设置用户所在城市
        /// </summary>
        public int SiteUserInfo_CityId
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置用户所在省份
        /// </summary>
        public int ProvinceID
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示同业资讯默认值
        /// </summary>
        private bool _istongye = false;
        /// <summary>
        /// 获取或设置是否显示同业资讯
        /// </summary>
        public bool IsTongYe
        {

            set { _istongye = value; }
            get { return _istongye; }
        }
    }
}