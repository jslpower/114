using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店首页
    /// 功能：显示酒店信息，提供城市搜索
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    /// </summary>
    public partial class HotelDefault : EyouSoft.Common.Control.FrontPage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //设置title
                this.Page.Title = EyouSoft.Common.PageTitle.Hotel_Title;
                // 设置关键字
                AddMetaTag("description", EyouSoft.Common.PageTitle.Hotel_Des);
                AddMetaTag("keywords", EyouSoft.Common.PageTitle.Hotel_Keywords);
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 4;
                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                //酒店的通栏广告图片一
                ScenicAdvBannerFirst();
                //酒店的通栏广告图片二
                ScenicAdvBannerSecond();
                //酒店内最热酒店列表
                this.NewAttrHotelControl1.CityId = this.CityId;
                //特价酒店列表
                this.DiscountControl2.CityId = this.CityId;
                //获得酒店的数量
                GetHotelCount();
                //设置酒店右边用户控件的值
                this.HotelRightControl1.Cid = this.CityId;
                //获得查询的酒店星级
                string level =Utils.GetQueryStringValue("level");
                //获得酒店查询的城市名称
                string cityName = Utils.GetQueryStringValue("cityName");
                //获得酒店查询的关键字
                string searchVal = Utils.GetQueryStringValue("searchVal");
                
                if (level != "")
                {
                    GetHotelByLevel(Utils.GetInt(Utils.InputText(level), 0));
                    return;
                }

                if (cityName != "" || searchVal != "")
                {
                    searchVal = Utils.InputText(Server.UrlDecode(EyouSoft.Common.Utils.GetString(Request.QueryString["searchVal"],"")));
                    cityName = Utils.InputText(Server.UrlDecode(EyouSoft.Common.Utils.GetString(cityName,"")));
                    this.txtFromCity.Value = cityName;
                    this.txtSearchVal.Value = searchVal;
                    this.txtSearchVal.Style.Add("color", "black");
                    DataListInit(0, cityName, searchVal.Trim(), 0);
                    return;
                }
                DataListInit(0, null, null, this.CityModel.ProvinceId);

            }
        }


        #region 酒店频道通栏广告图片1
        /// <summary>
        /// 酒店频道通栏广告图片
        /// </summary>
        protected void ScenicAdvBannerFirst()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.酒店频道通栏banner1);
            if (model != null)
            {
                this.ltlImgBoxBanner.Text = Utils.GetImgOrFalash(model.ImgPath, model.RedirectURL);
            }
            model = null;
        }
        #endregion

        #region 酒店频道通栏广告图片2
        /// <summary>
        /// 酒店频道通栏广告图片
        /// </summary>
        protected void ScenicAdvBannerSecond()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.酒店频道通栏banner2);
            if (model != null)
            {
                this.litImgBoxBannerSecond.Text = Utils.GetImgOrFalash(model.ImgPath, model.RedirectURL);
            }
            model = null;
        }
        #endregion

        #region 酒店广告或者对象方法
        /// <summary>
        /// 酒店广告或者对象方法
        /// </summary>
        protected EyouSoft.Model.AdvStructure.AdvInfo GetAdvsModel(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = null;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advInfoList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (advInfoList!=null&& advInfoList.Count > 0)
            {
                model = advInfoList[0];
                if (model != null)
                {
                    return model;
                }
                model = null;
            }
            return null;
        }
        #endregion

        #region 酒店集合方法
        /// <summary>
        /// 景区集合方法
        /// </summary>
        protected IList<EyouSoft.Model.AdvStructure.AdvInfo> GetList(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> List = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (List.Count > 0)
            {
                return List;
            }
            List = null;
            return null;
        }
        #endregion

        #region 酒店列表
        protected void DataListInit(int cityId,string cityName, string keyWord ,int provinceId)
        {
            IList<EyouSoft.Model.CompanyStructure.SupplierInfo> list = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetList(pageSize, Utils.GetInt(Request.QueryString["Page"], 1), ref recordCount, keyWord,provinceId ,cityId,  -1,0, cityName);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                BindPage();
            }
            list = null;

        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo.UrlParams = Request.QueryString;
            this.ExportPageInfo.intPageSize = pageSize;
            this.ExportPageInfo.CurrencyPage = pageIndex;
            this.ExportPageInfo.intRecordCount = recordCount;
        }
        #endregion

        #region 显示高级网店的星星
        public string HotelLevel(int level)
        {
            EyouSoft.Model.CompanyStructure.HotelLevel HotelLevel = new EyouSoft.Model.CompanyStructure.HotelLevel();

            HotelLevel = (EyouSoft.Model.CompanyStructure.HotelLevel)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.HotelLevel), Enum.GetName(typeof(EyouSoft.Model.CompanyStructure.HotelLevel), level));

            int count=0;
            switch (HotelLevel)
            {
                case EyouSoft.Model.CompanyStructure.HotelLevel.三星以下: break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.准三星: count = 3; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.三星或同级: count = 3; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.准四星: count = 4; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.四星或同级: count = 4; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.五星或同级: count = 5; break;
                default: break;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append("<img src=\"" + this.ImageServerPath + "/images/UserPublicCenter/xing.gif\" width=\"16\" height=\"16\" style=\"margin-bottom: -3px;\" />");
            }
            if (count == 0)
            {
                sb.Append("三星以下");
            }
            if (sb.ToString() != "")
            {
                return sb.ToString();
            }
            return "";
        }
        #endregion

        #region 列表行绑定事件
        //列表 行内容判断
        protected void rptList_ItemCreated(object sender,RepeaterItemEventArgs e){
            if(e.Item.ItemIndex!=-1){
                EyouSoft.Model.CompanyStructure.SupplierInfo model = e.Item.DataItem as EyouSoft.Model.CompanyStructure.SupplierInfo;
                if(model!=null){
                    //星级
                    Panel pnlLevel = e.Item.FindControl("pnlLevel") as Panel;
                    //品牌
                    Panel pnlCompanyBrand = e.Item.FindControl("pnlCompanyBrand") as Panel;
                    //周边环境
                    Panel pnlCompanyTag = e.Item.FindControl("pnlCompanyTag") as Panel;
                    //联系信息
                    Panel pnlContact = e.Item.FindControl("pnlContact") as Panel;

                    if(model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
                    {
                        //添加周边环境
                        Label lblTag = e.Item.FindControl("lblTag") as Label;
                        if (lblTag != null)
                        {
                            lblTag.Text = "";
                            if (model.CompanyTag.Count > 0)
                            {
                                for (int i = 0; i < model.CompanyTag.Count; i++)
                                {
                                    lblTag.Text += model.CompanyTag[i].FieldName + " &nbsp";
                                }
                            }
                            else
                            {
                                lblTag.Text = "";
                            }  
                        }
                        //添加联系信息
                        Label lblContact = new Label();
                        lblContact.Text = "联系人：" + model.ContactInfo.ContactName + " &nbsp;电&nbsp;&nbsp;话：<span class=\"hong\">" + model.ContactInfo.Tel + " </span> 传&nbsp;&nbsp;真：<span class=\"hong\">" + model.ContactInfo.Fax + "</span><span class=\"hui12\"> 地&nbsp;&nbsp;址：" + model.CompanyAddress + "&nbsp;</span>";
                        pnlContact.Controls.Add(lblContact);
                    }else
                    {
                        pnlLevel.Controls.Clear();
                        pnlCompanyTag.Controls.Clear();
                        pnlContact.Controls.Clear();
                        pnlCompanyBrand.Controls.Clear();
                    }
                }
            }
        }
        #endregion

        #region 获得各个星级的酒店
        protected void GetHotelByLevel(int level)
        {
            //记录当前星级
            this.hideStar.Value = level.ToString();
            IList<EyouSoft.Model.CompanyStructure.SupplierInfo> list = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, "", -1, this.CityId, level,-1,null);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                BindPage();
            }
            list = null;
        }
        #endregion

        #region 获得酒店的数量
        protected void GetHotelCount()
        {
            EyouSoft.IBLL.SystemStructure.ISummaryCount SummaryBll = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance();
            EyouSoft.Model.SystemStructure.SummaryCount SummaryModel = SummaryBll.GetSummary();
            if (SummaryModel != null)
            {
                this.lblHotelCount.Text = (SummaryModel.Hotel + SummaryModel.HotelVirtual).ToString();
            }
            SummaryModel = null;
            SummaryBll = null;

        }
        #endregion

     
    }
}
