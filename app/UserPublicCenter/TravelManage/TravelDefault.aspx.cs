using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.TravelManage
{
    /// <summary>
    /// 旅游用品首页
    /// 功能：显示旅游用品信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    /// </summary>
    public partial class TravelDefault : EyouSoft.Common.Control.FrontPage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置title
                this.Page.Title =string.Format(EyouSoft.Common.PageTitle.Travel_Title,CityModel.CityName);
                //设置关键字
                AddMetaTag("description",string.Format(EyouSoft.Common.PageTitle.Travel_Des,CityModel.CityName));
                AddMetaTag("keywords", EyouSoft.Common.PageTitle.Travel_Keywords);

                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 7;
                //设置右侧用户控件的赋值
                this.TravelRightControl1.Cid = this.CityId;
                //获取当前列表的页数
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                //旅游用品头部广告的集合
                HeadListInit();
                //旅游用品新货上架
                NewTravelInit();
                //获得查询的关键字
                string searchVal =Server.UrlDecode(EyouSoft.Common.Utils.GetString(Request.QueryString["searchVal"],""));
                //获得查询的城市ID
                int cid = EyouSoft.Common.Utils.GetInt(Request.QueryString["cid"], 0);
                //获得查询的省份ID
                int pid = EyouSoft.Common.Utils.GetInt(Request.QueryString["pid"], 0);
                //判断用户是否查询，没有就获取当前所在城市的数据集合
                if (cid == 0 && pid == 0)
                {
                    DataListInit(Utils.InputText(searchVal), this.CityModel.ProvinceId, 0);
                }
                else
                {
                    DataListInit(Utils.InputText(searchVal.Trim()), pid, cid);
                    this.ProvinceAndCityList1.SetCityId = cid;
                    this.ProvinceAndCityList1.SetProvinceId = pid;
                }
                this.txtSearchVal.Value = searchVal;
            }
        }



        #region 旅游用品网店列表
        protected void DataListInit(string keyWord, int provinceId, int cityId)
        {
            IList<EyouSoft.Model.CompanyStructure.SupplierInfo> list = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetList(pageSize, Utils.GetInt(Request.QueryString["Page"], 1), ref recordCount, keyWord, provinceId, cityId, EyouSoft.Model.CompanyStructure.BusinessProperties.旅游用品店);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();

                BindPage();
            }
            list = null;
        }
        #endregion

        #region 列表行绑定事件
        protected void rptList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.CompanyStructure.SupplierInfo model = e.Item.DataItem as EyouSoft.Model.CompanyStructure.SupplierInfo;
                if (model != null)
                {
                    //电话
                    Panel pnlTelPhone = e.Item.FindControl("pnlTelPhone") as Panel;
                    //传真
                    Panel pnlFax = e.Item.FindControl("pnlFax") as Panel;
                    //联系人名
                    Panel pnlContact = e.Item.FindControl("pnlContact") as Panel;

                    //添加城市
                    Label lblCity = e.Item.FindControl("lblCity") as Label;
                    //添加省份
                    Label lblProvince = e.Item.FindControl("lblProvince") as Label;

                    EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(model.CityId);
                    EyouSoft.Model.SystemStructure.SysProvince provModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(model.ProvinceId);

                    if (cityModel != null)
                    {
                        lblCity.Text = cityModel.CityName;
                    }
                    if (provModel != null)
                    {
                        lblProvince.Text = provModel.ProvinceName;
                    }

                    if (model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
                    {
                        //添加联系信息
                        Label lblTelPhone = new Label();
                        lblTelPhone.Text = "电&nbsp;&nbsp;话：<span class=\"hong\">" + model.ContactInfo.Tel + "</span>";
                        pnlTelPhone.Controls.Add(lblTelPhone);
                        //添加传真
                        Label lblFax = new Label();
                        lblFax.Text = "传&nbsp;&nbsp;真：<span class=\"hong\">" + model.ContactInfo.Fax + "</span>";
                        pnlFax.Controls.Add(lblFax);

                        //添加联系人名
                        Label lblName = new Label();
                        lblName.Text = "联系人：" + model.ContactInfo.ContactName + "";
                        pnlContact.Controls.Add(lblName);
                    }
                    else
                    {
                        pnlTelPhone.Controls.Clear();
                        pnlFax.Controls.Clear();
                        pnlContact.Controls.Clear();
                    }
                }
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

        #region 旅游用品头部广告
        protected void HeadListInit()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> list = GetList(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道精品推荐图文);
            if (list != null)
            {
                this.rptHeadList.DataSource = list;
                this.rptHeadList.DataBind();
                list = null;
            }
        }
        #endregion

        #region 旅游集合方法
        /// <summary>
        /// 旅游集合方法
        /// </summary>
        protected IList<EyouSoft.Model.AdvStructure.AdvInfo> GetList(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> List = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (List != null && List.Count > 0)
            {
                return List;
            }
            List = null;
            return null;
        }
        #endregion

        #region 旅游新货上架
        protected void NewTravelInit()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> list = GetList(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道新货上架);
            if (list != null)
            {
                this.rptNewTravel.DataSource = list;
                this.rptNewTravel.DataBind();
                list = null;

            }
        }
        #endregion
    }
}
