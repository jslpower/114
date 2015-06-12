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
using System.Collections.Generic;
using System.Text;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.NewsStructure;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 姓名：刘飞
    /// 时间2011-12-16
    /// </summary>
    public partial class UCRightList : System.Web.UI.UserControl
    {
        private bool _isPinpai = true;
        /// <summary>
        /// 是否显示推荐品牌
        /// </summary>
        public bool IsPinpai
        {
            get { return _isPinpai; }
            set { _isPinpai = value; }
        }

        private bool _isToolbar = false;
        /// <summary>
        /// 是否显示工具箱
        /// </summary>
        public bool IsToolbar
        {
            get { return _isToolbar; }
            set { _isToolbar = value; }
        }
        /// <summary>
        /// 是否显示搜索
        /// </summary>
        private bool _isSearch = false;
        public bool IsSearch
        {
            get { return _isSearch; }
            set { _isSearch = value; }
        }
        public string TourAreaId = string.Empty;
        public int CityID;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRemCommendList();
            GetTraveNewList();
            GetJoinTravelList();
            if (string.IsNullOrEmpty(TourAreaId) && Utils.GetInt(TourAreaId) > 0)
            {
                GetPinPai(TourAreaId);
            }
            else
            {
                GetPinPai();
            }
        }
        /// <summary>
        /// 获取旅游资讯
        /// </summary>
        private void GetTraveNewList()
        {
            IList<WebSiteNews> NewsList = EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance().GetListByCategory(8, EyouSoft.Model.NewsStructure.NewsCategory.行业资讯);
            if (NewsList != null && NewsList.Count > 0)
            {
                this.rptTraveNewList.DataSource = NewsList;
                this.rptTraveNewList.DataBind();
            }
            else
            {
                this.lbtravenew.Visible = true;
                this.lbtravenew.Text = "暂无资讯！";
            }
        }

        /// <summary>
        /// 获取加盟旅行社列表
        /// </summary>
        private void GetJoinTravelList()
        {
            EyouSoft.Model.CompanyStructure.QueryNewCompany modelsearch = new EyouSoft.Model.CompanyStructure.QueryNewCompany();
            modelsearch.CompanyTypes = new CompanyType?[] { CompanyType.组团 };
            modelsearch.B2BDisplay = CompanyB2BDisplay.侧边推荐;
            modelsearch.OrderIndex = 3;
            IList<CompanyDetailInfo> list = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(10, modelsearch);
            if (list != null && list.Count > 0)
            {
                this.rptJoinTraveList.DataSource = list;
                this.rptJoinTraveList.DataBind();
            }
            else
            {
                this.lbjoinTrave.Visible = true;
                this.lbjoinTrave.Text = "暂无加盟旅行社!";
            }
        }

        /// <summary>
        /// 获取推荐线路
        /// </summary>
        private void GetRemCommendList()
        {
            IList<EyouSoft.Model.NewTourStructure.MRoute> RemCommendList = new List<EyouSoft.Model.NewTourStructure.MRoute>();
            var search = new EyouSoft.Model.NewTourStructure.MTuiJianRouteSearch
                             {
                                 Type = null,
                                 B2BDisplay = EyouSoft.Model.NewTourStructure.RouteB2BDisplay.侧边
                             };
            if (CityID > 0)
            {
                var cityIds = new List<int> { CityID };
                search.SellCityIds = cityIds.ToArray();
            }
            RemCommendList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetRecommendList(10, search);
            if (RemCommendList != null && RemCommendList.Count > 0)
            {
                this.rptRecommendList.DataSource = RemCommendList;
                this.rptRecommendList.DataBind();
            }
            else
            {
                this.lbRecommend.Visible = true;
                this.lbRecommend.Text = "暂无线路";
            }
        }
        /// <summary>
        /// 获取推荐品牌列表(无线路区域)
        /// </summary>
        private void GetPinPai()
        {
            QueryNewCompany CompanySearch = new QueryNewCompany();
            CompanySearch.CompanyTypes = new CompanyType?[] { CompanyType.专线 };
            CompanySearch.B2BDisplay = CompanyB2BDisplay.侧边推荐;
            CompanySearch.OrderIndex = 3;
            IList<CompanyDetailInfo> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(6, CompanySearch);
            if (CompanyList != null && CompanyList.Count > 0)
            {
                this.rptPinPaiList.DataSource = CompanyList;
                this.rptPinPaiList.DataBind();
            }
            else
            {
                this.lbPinPai.Visible = true;
                this.lbPinPai.Text = "暂无品牌";
            }
        }
        /// <summary>
        /// 获取推荐品牌列表(有线路区域)
        /// </summary>
        private void GetPinPai(string AreaID)
        {
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany CompanySearch = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            CompanySearch.CityId = CityID;
            CompanySearch.AreaId = Utils.GetInt(AreaID);
            IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListCityAreaAdvRouteAgency(CompanySearch);
            if (CompanyList != null && CompanyList.Count > 0)
            {
                this.rptPinPai.DataSource = CompanyList;
                this.rptPinPai.DataBind();
            }
            else
            {
                this.lbPinPai.Visible = true;
                this.lbPinPai.Text = "暂无品牌";
            }

        }
        /// <summary>
        /// 获取公司网店地址
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        protected string GetCompanyShopUrl(string CompanyId)
        {
            EyouSoft.Model.CompanyStructure.CompanyState CompanyStateModel = new EyouSoft.Model.CompanyStructure.CompanyState();
            string GoToUrl = EyouSoft.Common.Utils.GetCompanyDomain(CompanyId, EyouSoft.Model.CompanyStructure.CompanyType.专线, CityID);
            return GoToUrl;
        }

        /// <summary>
        /// 获取公司品牌名称
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        protected string GetCompanyBrand(string CompanyId)
        {
            CompanyDetailInfo Companyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            if (Companyinfo != null)
            {
                if (!string.IsNullOrEmpty(Companyinfo.CompanyBrand))
                {
                    return Utils.GetText2(Companyinfo.CompanyBrand, 6, false);
                }
                else if (!string.IsNullOrEmpty(Companyinfo.Introduction))
                {
                    return Utils.GetText2(Companyinfo.Introduction, 6, false);
                }
                else
                {
                    return Utils.GetText2(Companyinfo.CompanyName, 6, false);
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取公司品牌名称
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        protected string GetCompanyBrandall(string CompanyId)
        {
            CompanyDetailInfo Companyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            if (Companyinfo != null)
            {
                if (!string.IsNullOrEmpty(Companyinfo.CompanyBrand))
                {
                    return Companyinfo.CompanyBrand;
                }
                else if (!string.IsNullOrEmpty(Companyinfo.Introduction))
                {
                    return Companyinfo.Introduction;
                }
                else
                {
                    return Companyinfo.CompanyName;
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取推荐品牌公司logo路径
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetCompanyLogoSrc(object obj)
        {
            EyouSoft.Model.CompanyStructure.CompanyAttachInfo attachinfo = (EyouSoft.Model.CompanyStructure.CompanyAttachInfo)obj;
            if (attachinfo != null && attachinfo.CompanyLogo != null && !string.IsNullOrEmpty(attachinfo.CompanyLogo.ImagePath))
                return Utils.GetNewImgUrl(attachinfo.CompanyLogo.ImagePath, 2);
            else
                return Utils.GetNewImgUrl("", 2);
        }

        /// <summary>
        /// 获取推荐品牌公司logo路径(有线路区域)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetCompanyLogo(object obj)
        {
            CompanyLogo companylogo = (CompanyLogo)obj;
            if (companylogo != null)
            {
                return Utils.GetNewImgUrl(companylogo.ImagePath, 2);
            }
            else
            {
                return Utils.GetNewImgUrl("", 2);
            }
        }
    }
}