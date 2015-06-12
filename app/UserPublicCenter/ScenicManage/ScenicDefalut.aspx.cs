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
using System.Text;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.BLL.ScenicStructure;

namespace UserPublicCenter.ScenicManage
{
    /// <summary>
    /// 景区列表页
    /// 功能：景区搜索
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-23   
    /// </summary>
    public partial class ScenicDefalut : EyouSoft.Common.Control.FrontPage
    {
        public int ProvinceId { get { return Utils.GetInt(Utils.GetQueryStringValue("province")); } }
        public int ThemeId { get { return Utils.GetInt(Utils.GetQueryStringValue("theme")); } }

        /// <summary>
        /// 分页设置
        /// </summary> 
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("province"));
                //省份
                GetProvince();
                //ThemeId = Utils.GetInt(Utils.GetQueryStringValue("theme"));
                //主题
                GetTheme();
                //获得当前页数
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                //景区
                GetScenic();
                //景区图片-4张
                GetScenicImg();
                //设置title
                this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Scenic_Title, this.CityModel.CityName);
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 5;
                //景区频道通栏广告一图片初始化
                ScenicAdvBannerFirst();
                //景区频道通栏广告二图片初始化
                ScenicAdvBannerSecond();
                //景区主题广告图片初始化
                ScenicAdvTitle();
                //景区数量
                GetScenicCount();
                //本周最热景点
                this.NewAttrControl1.TopNum = 9;
                this.NewAttrControl1.ProvinceId = ProvinceId < 1 ? null : (int?)ProvinceId;
                //特价景区门票
                this.NewTicketsControl.TopNum = 10;
                this.NewTicketsControl.ProvinceId = ProvinceId < 1 ? null : (int?)ProvinceId;
                //最新加入-上门2张图片
                this.ViewRightControl1.Cid = this.CityModel.CityId;
                AddMetaTag("description", "同业114景区频道,为旅游景区推广提供平台,包含最新最全的杭州旅游景点信息,旅游景点联系方式.包括门旅游目的地,最热旅游景点,旅游风景区推荐,为景区和旅行社搭建合作桥梁.");
                AddMetaTag("keywords", "旅游景点大全,旅游景点信息,旅游景点图片,热门景点,热门目的地,旅游风景区 景区列表,景点名单,景点门票,景区活动,景区优惠, 景点返佣,景点旅行社合作,景区推广,景点宣传,景区网店.");
            }
        }

        #region 景区
        protected void GetScenic()
        {
            MSearchSceniceArea search = new MSearchSceniceArea()
            {
                ProvinceId = ProvinceId < 1 ? null : (int?)ProvinceId,
                ThemeId = ThemeId < 1 ? null : (int?)ThemeId,
                Status = ExamineStatus.已审核
               
            };
            IList<MScenicArea> list = BScenicArea.CreateInstance().GetPublicList(pageSize, pageIndex, ref recordCount, search);
            if (list != null && list.Count > 0)
            {
                rpt_ScenicList.DataSource = list;
                rpt_ScenicList.DataBind();
                BindPage();
            }
        }
        #endregion

        #region 景区图片

        protected void GetScenicImg()
        {
            IList<MScenicImg> list = BScenicImg.CreateInstance().GetList(4);
            if (list != null && list.Count > 0)
            {
                rpt_ScenicImg.DataSource = list;
                rpt_ScenicImg.DataBind();
            }
        }

        #endregion

        #region 景区列表图片

        protected string GetScenicImg(object obj)
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

        #region 景区频道通栏广告图片1
        /// <summary>
        /// 景区频道通栏广告图片
        /// </summary>
        protected void ScenicAdvBannerFirst()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.景区频道通栏banner1);
            if (model != null)
            {
                this.ltlImgBoxBanner.Text = Utils.GetImgOrFalash(model.ImgPath, model.RedirectURL);
            }
            model = null;
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

        #region 景区主题广告
        /// <summary>
        /// 景区主题广告
        /// </summary>
        protected void ScenicAdvTitle()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.景区频道景区主题广告);
            if (model != null)
            {
                if (model.RedirectURL == Utils.EmptyLinkCode)
                {
                    this.ltlImgTitleAdv.Text = "<a href=\"" + model.RedirectURL + "\" target=_self><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"225px\" height=\"130px\"/></a>";
                }
                else
                {
                    this.ltlImgTitleAdv.Text = "<a href=\"" + model.RedirectURL + "\" target=_blank><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"225px\" height=\"130px\"/></a>";
                }
            }
            model = null;
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

        #region 获得景区的数量
        protected void GetScenicCount()
        {
            EyouSoft.IBLL.SystemStructure.ISummaryCount SummaryBll = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance();
            EyouSoft.Model.SystemStructure.SummaryCount SummaryModel = SummaryBll.GetSummary();
            if (SummaryModel != null)
            {
                this.lblCount.Text = (SummaryModel.Scenic + SummaryModel.ScenicVirtual).ToString();
            }
            SummaryModel = null;
            SummaryBll = null;

        }
        #endregion

        #region Url参数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="type">p:省份 t:主题</param>
        /// <returns></returns>
        protected string GetUrlParams(int num, string type)
        {
            StringBuilder url = new StringBuilder();
            
            if (num > 0 && type.Equals("p"))
            {
                url.AppendFormat("pid={0}&", num);
            }
            else //if (num > 0 && type.Equals("t"))
            {
                url.AppendFormat("&tid={0}&", num);
            }
            return url.ToString();
        }

        #endregion

        #region 省份

        protected void GetProvince()
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (EyouSoft.Model.SystemStructure.SysProvince t in list)
                {
                    if (t.ProvinceId == ProvinceId)
                    {
                        sb.AppendFormat("<a href=\"/jingqumap_{0}_{1}\" class=\"dianjihou\">{2}</a>", CityId,
                                        t.ProvinceId, t.ProvinceName);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"/jingqumap_{0}_{1}\">{2}</a>", CityId,
                                        t.ProvinceId, t.ProvinceName);
                    }
                }
                ltlProvince.Text = sb.ToString();
            }
        }

        #endregion

        #region 主题
        protected void GetTheme()
        {
            IList<MScenicTheme> list = BScenicTheme.CreateInstance().GetList();
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MScenicTheme t in list)
                {
                    if (t.ThemeId == ThemeId)
                    {
                        sb.AppendFormat("<a href=\"/ScenicManage/ScenicList.aspx?{0}\"  class=\"dianjihou\">{1}</a>", GetUrlParams(t.ThemeId, "t"), t.ThemeName);
                    }
                    else
                    {
                        sb.AppendFormat("<a href=\"/ScenicManage/ScenicList.aspx?{0}\">{1}</a>", GetUrlParams(t.ThemeId, "t"), t.ThemeName);
                    }
                }
                ltlTheme.Text = sb.ToString();
            }
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfo.IsUrlRewrite = true;
                this.ExportPageInfo.Placeholder = "#PageIndex#";
                this.ExportPageInfo.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfo.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo.UrlParams = Request.QueryString;
            }
            this.ExportPageInfo.intPageSize = pageSize;
            this.ExportPageInfo.CurrencyPage = pageIndex;
            this.ExportPageInfo.intRecordCount = recordCount;
        }
        #endregion
       
    }
}




