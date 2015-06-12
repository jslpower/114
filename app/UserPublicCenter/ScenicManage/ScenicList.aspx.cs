using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.AdvStructure;
using EyouSoft.Model;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace UserPublicCenter.ScenicManage
{
    public partial class ScenicList : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 分页设置
        /// </summary>
        protected int pageSize = 8;
        protected int pageIndex = 1;
        protected int recordCount;

        protected string Province = string.Empty;
        protected string ThemeList = string.Empty;
        protected string CityList = string.Empty;
        protected StringBuilder PageTitle = new StringBuilder();
        protected string ThemeName = string.Empty;
        protected string CityName = string.Empty;
        protected string ProvinceName = string.Empty;
        protected string SceniceNameList = string.Empty;
        /// <summary>
        /// 景区搜索结果列表页
        /// 功能：显示查询以后获得景区的列表
        /// 创建人：戴银柱
        /// 创建时间： 2010-07-23   
        /// </summary>

        /// <summary>
        /// 景区搜索结果列表页
        /// 功能：显示查询以后获得景区的列表
        /// 修改人：方琪
        /// 修改时间： 2011-11-21  
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            //获取查询的关键字的值
            string searchVal = Server.UrlDecode(EyouSoft.Common.Utils.GetString(Request.QueryString["searchVal"], ""));
            if (searchVal == "")
            {
                this.textfield.Value = "输入景点关键字";
            }
            else
            {
                this.textfield.Value = searchVal;
            }
            //获取查询的城市ID
            int cid = EyouSoft.Common.Utils.GetInt(Request.QueryString["cid"], 0);
            this.hidCityList.Value = cid.ToString();
            //获得查询的省份ID
            int pid = EyouSoft.Common.Utils.GetInt(Request.QueryString["pid"], 0);
            this.hidProvinceList.Value = pid.ToString();
            //获得查询的主题ID
            int tid = EyouSoft.Common.Utils.GetInt(Request.QueryString["tid"], 0);
            this.hidThemeList.Value = tid.ToString();
            if (!IsPostBack)
            {
                ScenicAdvBannerSecond();
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 5;
                //获取当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                //设置景区右侧用户控件的值
                this.ViewRightControl1.Cid = this.CityId;
                //本周最热景点
                this.NewAttrControl1.TopNum = 9;
                this.NewAttrControl1.ProvinceId = pid < 1 ? null : (int?)pid;
                //特价景区门票

                this.TicketsControl1.TopNum = 10;
                this.TicketsControl1.ProvinceId = pid < 1 ? null : (int?)pid;
                //绑定主题
                if (pid != 0)
                {
                    BindProvince(pid);
                    BindCity(pid, cid);
                }
                if (cid != 0 && pid == 0)
                {

                    EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cid);
                    pid = cityModel.ProvinceId;
                    BindProvince(pid);
                    BindCity(pid, cid);
                }
                if (cid == 0 && pid == 0)
                {
                    BindProvince(pid);
                }
                BindTheme(tid);

                DataInitBySearch(searchVal, pid, cid, tid);

                this.Title = GetPageTietle();
                if (!string.IsNullOrEmpty(SceniceNameList) && SceniceNameList.Length > 0)
                {
                    AddMetaTag("description", "同业114景区频道,包含" +ProvinceName+CityName+"旅游景点信息,旅游景点联系方式.包括热门旅游目的地,热门旅游景点,旅游风景区推荐, 景点门票" + SceniceNameList.Substring(0, SceniceNameList.Length - 1));
                }
                AddMetaTag("keywords", "景点大全,景点门票，旅游景点，景点门票价格，旅游风景区,特价门票，旅游景点门票预定，门票预定");
            }
        }

        #region 接受参数初始化
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="searchVal"></param>
        /// <param name="ProvinceRequired"></param>
        /// <param name="CityRequired"></param>
        protected void DataInitBySearch(string searchVal, int ProvinceRequired, int CityRequired, int ThemeId)
        {
            EyouSoft.Model.ScenicStructure.MSearchSceniceArea SearchModel = new EyouSoft.Model.ScenicStructure.MSearchSceniceArea();
            if (ThemeId != 0)
            {
                SearchModel.ThemeId = ThemeId;
            }
            else
            {
                SearchModel.ThemeId = null;
            }
            if (ProvinceRequired != 0)
            {
                SearchModel.ProvinceId = ProvinceRequired;
            }
            else
            {
                SearchModel.ProvinceId = null;

            }
            if (CityRequired != 0)
            {
                SearchModel.CityId = CityRequired;
            }
            else
            {
                SearchModel.CityId = null;
            }

            SearchModel.ScenicName = searchVal;


            IList<EyouSoft.Model.ScenicStructure.MScenicArea> SceniceList = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetPublicList(pageSize, pageIndex, ref recordCount, SearchModel);
            if (SceniceList != null)
            {
                foreach (var item in SceniceList)
                {
                    //if (item.B2B == EyouSoft.Model.ScenicStructure.ScenicB2BDisplay.隐藏)
                    //{
                    //    SceniceList.Remove(item);
                    //}
                    SceniceNameList += item.ScenicName + ",";
                }
            }

            this.SceniceList.DataSource = null;
            this.SceniceList.DataSource = SceniceList;
            this.SceniceList.DataBind();
            BindPage();
            SceniceList = null;
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;

        }
        #endregion


        #region 绑定省份
        /// <summary>
        /// 绑定省份
        /// </summary>
        protected void BindProvince(int ProvinceId)
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            StringBuilder sbProvince = new StringBuilder();

            sbProvince.Append(string.Format("<a class=\"Province\" {2} href=\"{0}\">{1}</a>", 0, "全部",
                                            ProvinceId == 0 ? "style=\"color:Red\"" : string.Empty));
            ;
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysProvince item in ProvinceList)
                {
                    if (item.ProvinceId == ProvinceId)
                    {
                        sbProvince.Append(string.Format("<a class=\"Province\" style=\"color:Red\" href=\"{0}\">{1}</a>", item.ProvinceId, item.ProvinceName));
                        ProvinceName = item.ProvinceName;

                    }
                    else
                    {
                        sbProvince.Append(string.Format("<a class=\"Province\" href=\"{0}\">{1}</a>", item.ProvinceId, item.ProvinceName));
                    }

                }
                Province = sbProvince.ToString();
            }
        }
        #endregion

        #region 绑定城市
        /// <summary>
        /// 绑定城市
        /// </summary>
        /// <param name="ProvinceId"></param>
        protected void BindCity(int ProvinceId, int CityId)
        {
            if (ProvinceId == 0)
            {
                ProvinceId = this.CityModel.ProvinceId;
            }
            IList<EyouSoft.Model.SystemStructure.SysCity> CityModelList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(ProvinceId, 0, null, null, null);
            StringBuilder sbCity = new StringBuilder();
            if (CityModelList != null && CityModelList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysCity item in CityModelList)
                {
                    if (item.CityId == CityId)
                    {
                        sbCity.Append(string.Format("<a class=\"City\" href=\"{0}\" style=\"color:Red\">{1}</a>", item.CityId, item.CityName));
                        CityName = item.CityName;
                    }
                    else
                    {
                        sbCity.Append(string.Format("<a class=\"City\" href=\"{0}\">{1}</a>", item.CityId, item.CityName));
                    }

                }
                CityList = sbCity.ToString();
            }

        }
        #endregion

        #region 绑定主题
        /// <summary>
        /// 绑定主题
        /// </summary>
        protected void BindTheme(int ThemeId)
        {
            IList<EyouSoft.Model.ScenicStructure.MScenicTheme> themeList = EyouSoft.BLL.ScenicStructure.BScenicTheme.CreateInstance().GetList();
            StringBuilder sbTheme = new StringBuilder();
            sbTheme.Append(string.Format("<a class=\"Theme\" {2} href=\"{0}\" >{1}</a>", 0, "全部",
                                         ThemeId == 0 ? "style=\"color:Red\"" : string.Empty));
            if (themeList != null && themeList.Count > 0)
            {
                foreach (EyouSoft.Model.ScenicStructure.MScenicTheme item in themeList)
                {
                    if (item.ThemeId == ThemeId)
                    {
                        sbTheme.Append(string.Format("<a class=\"Theme\" href=\"{0}\" style=\"color:Red\" >{1}</a>", item.ThemeId, item.ThemeName));
                        ThemeName = item.ThemeName;
                    }
                    else
                    {
                        sbTheme.Append(string.Format("<a class=\"Theme\" href=\"{0}\" >{1}</a>", item.ThemeId, item.ThemeName));
                    }
                }
                ThemeList = sbTheme.ToString();
            }
        }
        #endregion

        #region 绑定图片
        /// <summary>
        /// 绑定图片
        /// </summary>
        /// <param name="obj">景区图片集合</param>
        /// <returns></returns>
        protected string GetSceniceImage(object obj)
        {
            IList<MScenicImg> list = (IList<MScenicImg>)obj;
            string imgAddress = string.Empty;
            if (list != null && list.Count > 0)
            {
                //每个景区，形象照片只有一张
                foreach (MScenicImg i in list)
                {
                    imgAddress = i.ThumbAddress;
                    break;
                }
            }

            return imgAddress;
        }
        #endregion

        #region 景区广告或者对象方法
        /// <summary>
        /// 景区广告或者对象方法
        /// </summary>
        protected EyouSoft.Model.AdvStructure.AdvInfo GetAdvsModel(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = null;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advInfoList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (advInfoList != null && advInfoList.Count > 0)
            {
                model = advInfoList[0];
                if (model != null)
                {
                    return model;
                }
            }
            model = null;
            return null;
        }
        #endregion

        #region 景区频道通栏广告图片2
        /// <summary>
        /// 景区频道通栏广告图片
        /// </summary>
        protected void ScenicAdvBannerSecond()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.景区频道通栏banner2);
            if (model != null)
            {

                this.litImgBoxBannerSecond.Text = Utils.GetImgOrFalash(model.ImgPath, model.RedirectURL);
            }
            model = null;
        }
        #endregion

        #region 拼接页面标题
        /// <summary>
        /// 拼接页面标题
        /// </summary>
        /// <returns>页面标题</returns>
        protected string GetPageTietle()
        {
            PageTitle.Append("景区_");
            if (!string.IsNullOrEmpty(ProvinceName))
            {
                PageTitle.Append(ProvinceName);
            }
            if (!string.IsNullOrEmpty(CityName))
            {
                PageTitle.Append(CityName);
            }
            if (!string.IsNullOrEmpty(ThemeName))
            {
                PageTitle.Append(ThemeName);
            }
            if (string.IsNullOrEmpty(ThemeName))
            {
                PageTitle.Append("景区");
            }
            PageTitle.Append("_同业114");
            return PageTitle.ToString();
        }
        #endregion
    }
}
