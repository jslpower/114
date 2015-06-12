using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common;
using System.Configuration;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Common.URLREWRITE;

namespace UserPublicCenter.ScenicManage
{
    public partial class ScenicDetails : EyouSoft.Common.Control.FrontPage
    {
        #region  新页面代码
        protected double _Longitude = 116.389503; //经度
        protected double _Latitude = 39.918953;  //纬度

        protected MScenicArea ScenicModel = null;
        protected EyouSoft.Model.CompanyStructure.CompanyUser modelcompanyuser = null;
        protected string Mq = string.Empty;
        protected string QQ = string.Empty;
        protected bool IsorNoLogin = false;
        protected string result = "";
        protected string defaultname = "该景区未指定";
        protected string GoogleMapKey = string.Empty;
        private long Id = 0;  //景区自增编号
        protected StringBuilder strb = null;
        private string scenicId = Utils.GetQueryStringValue("ScenicId");

        #region 加载省份
        private int _setProvinceId = 0;

        /// <summary>
        /// 默认选中省份ID
        /// </summary>
        public int SetProvinceId
        {
            get { return _setProvinceId; }
            set { _setProvinceId = value; }
        }
        private int _setCityId = 0;
        /// <summary>
        /// 默认选中城市ID
        /// </summary>
        public int SetCityId
        {
            get { return _setCityId; }
            set { _setCityId = value; }
        }
        private bool _isShowRequired = false;
        /// <summary>
        /// 是否显示必填（*）信息
        /// </summary>
        public bool IsShowRequired
        {
            get { return _isShowRequired; }
            set { _isShowRequired = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Utils.GetQueryStringValue("Id"), out Id);

            IsorNoLogin = IsLogin;
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.ddl_ProvinceList.ClientID + "');");
            BindProvince();
            GoogleMapKey = Utils.GetGoogleMapKeyByXml();
            if (!IsPostBack)
            {
                ScenicAdvBannerSecond();
                GetScenicModel();
                this.ViewRightControl1.Cid = ScenicModel.CityId;
                this.CityAndMenu1.HeadMenuIndex = 5;

                /* 默认省份选中项 */
                if (SetProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince(" + SetProvinceId + ");ChangeList('" + this.ddl_CityList.ClientID + "'," + SetProvinceId + ");", true);
                    if (SetCityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity(" + SetCityId + ");", true);
                    }
                }
                if (IsShowRequired)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "$('#ProvinceRequired').show();$('#CityRequired').show()", true);
                }
                strb = new StringBuilder("");
                #region 获取城市景区
                if (ScenicModel != null)
                {
                    var listcity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList().Where(r => r.ProvinceId == ScenicModel.ProvinceId);
                    foreach (var item in listcity)
                    {
                        if (item.CityId == ScenicModel.CityId)
                            strb.Append("<li class=\"dangqian\">" + item.CityName + "旅游景点</a></li>");
                        else
                            strb.Append("<li><a href=\"/ScenicManage/ScenicList.aspx?cid=" + item.CityId + "\">" + item.CityName + "旅游景点</a></li>");
                    }

                }

                
                #endregion


            }
        }

        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {

            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "', this.options[this.selectedIndex].value)"));
            this.ddl_CityList.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion

        #region   获取景区实体
        protected void GetScenicModel()
        {
            if (Id > 0)
                ScenicModel = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(Id);
            else
                ScenicModel = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(scenicId);

            if (ScenicModel == null)
            {
                ScenicModel = new MScenicArea();
            }

            if (ScenicModel.Img != null)
            {
                foreach (var item in ScenicModel.Img)
                {
                    if (item.ImgType == ScenicImgType.景区形象)
                    {
                        result = item.ThumbAddress + "$" + item.Address + "$" + item.Description;
                    }
                }
            }
            if (ScenicModel.X != "" || ScenicModel.Y != "")
            {

                _Longitude = Convert.ToDouble(ScenicModel.X);
                _Latitude = Convert.ToDouble(ScenicModel.Y);
                defaultname = ScenicModel.ScenicName;

            }


            #region 设置标题

            //设置Title.....
            this.Title = string.Format("{0}", ScenicModel.ScenicName);
            if (Utils.LoseHtml(ScenicModel.Description) != string.Empty)
                AddMetaTag("description", string.Format("{0}", Utils.GetText2(Utils.LoseHtml(ScenicModel.Description), 100, false)));
            else
                AddMetaTag("description", " 暂无描述");
            AddMetaTag("keywords", string.Format("{0}，{1}，{2}，{3}，{4}", ScenicModel.ScenicName, ScenicModel.ScenicName + "门票", ScenicModel.ScenicName + "门票价格", ScenicModel.ScenicName + "特价门票", ScenicModel.ScenicName + "门票预定", ScenicModel.ScenicName + "介绍"));

            #endregion

            #region 景区介绍下面的景区联系人
            if (IsLogin)
            {
                if (!string.IsNullOrEmpty(ScenicModel.ContactOperator))
                {
                    modelcompanyuser = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(ScenicModel.ContactOperator);
                    if (modelcompanyuser != null && modelcompanyuser.ContactInfo != null)
                    {
                        txt_ContactName.Text = modelcompanyuser.ContactInfo.ContactName;
                        txt_Fax.Text = modelcompanyuser.ContactInfo.Fax;
                        txt_Mobile.Text = modelcompanyuser.ContactInfo.Mobile;
                        txt_QQ.Text = modelcompanyuser.ContactInfo.QQ;
                        txt_Tel.Text = modelcompanyuser.ContactInfo.Tel;
                        Mq = Utils.GetMQ(modelcompanyuser.ContactInfo.MQ);
                        QQ = Utils.GetQQ(modelcompanyuser.ContactInfo.QQ);
                    }
                }
            }
            #endregion

            //添加点击量
            if (Id > 0)
                EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().UpdateClickNum(Id);
            else
                EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().UpdateClickNum(scenicId);

        }
        #endregion

        #region   获取注册认领
        protected string GetRe(string companyid)
        {
            result = "";
            if (string.IsNullOrEmpty(companyid))
                return result;

            var model = GetCompanyName(companyid);
            if (model == null)
                return string.Empty;
            string companyname = model.CompanyName;
            string subcompanname = Utils.GetText2(model.CompanyName, 20, true);
            string mq = Utils.GetMQ(model.ContactInfo.MQ);
            string tongYe114SightId = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("TongYe114SightId");

            //公司名称链接(有高级网店链接到高级网店，没有链接到普通网店)
            result += string.Format("<a target=\"_blank\" href=\"{0}\" title=\"{1}\">",
                                    Utils.GetShopUrl(companyid, EyouSoft.Model.CompanyStructure.CompanyType.景区, -1),
                                    companyname);

            if (companyid == tongYe114SightId)
            {
                result += string.Format("{0}", string.Empty);
            }
            else
            {
                result += string.Format("{0}", subcompanname);
            }

            result += "</a>";
            result += mq;

            if (IsLogin)
            {
                if (SiteUserInfo.CompanyID == tongYe114SightId)
                {
                    result += string.Empty;
                }
                else
                {
                    if (companyid == tongYe114SightId)
                        result += "<a href=\"/Register/CompanyUserRegister.aspx?cType=gy\"><img src=\"" + Domain.ServerComponents + "/images/UserPublicCenter/zcrl_03.jpg\""
                                + "width=\"58\" height=\"21\" alt=\"注册认领\" target=\"_blank\" /></a>";
                    else
                        result += string.Empty;
                }
            }
            else
            {
                if (companyid == tongYe114SightId)
                    result += "<a href=\"/Register/CompanyUserRegister.aspx?cType=gy\"><img src=\"" + Domain.ServerComponents + "/images/UserPublicCenter/zcrl_03.jpg\""
                                + "width=\"58\" height=\"21\" alt=\"注册认领\" target=\"_blank\"/></a>";
                else
                    result += string.Empty;
            }

            return result;
        }
        #endregion

        #region   获取景区名称
        public string GetContactName(string id)
        {
            string result = "";
            if (!string.IsNullOrEmpty(id))
            {
                var model = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(id);
                if (model != null && model.ContactInfo != null)
                    result = model.ContactInfo.ContactName;
            }
            return result;


        }
        #endregion

        #region   获取景区主题
        public string GetThemeName(IList<MScenicTheme> idlist)
        {
            string result = "";
            if (idlist != null)
            {
                foreach (var item in idlist)
                {
                    if (item != null)
                        result += item.ThemeName + " ";
                }
            }
            return result;
        }
        #endregion

        #region   获取公司名称
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetCompanyName(string id)
        {
            return EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(id);
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

        #region 取得有效时间
        /// <summary>
        /// 取得有效时间
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        protected string GetGoodTime(DateTime? sTime, DateTime? eTime)
        {

            if (sTime == null || Convert.ToDateTime(sTime).ToString("yyyy-MM-dd").Equals("0001-01-01"))
            {
                if (eTime == null || Convert.ToDateTime(eTime).ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    return "长期有效";

                return Utils.GetDateTime(eTime.ToString()).ToShortDateString() + "前有效";
            }

            if (eTime == null || Convert.ToDateTime(eTime).ToString("yyyy-MM-dd").Equals("0001-01-01"))
                return "长期有效";

            return Utils.GetDateTime(sTime.ToString()).ToShortDateString() + "至" +
                   Utils.GetDateTime(eTime.ToString()).ToShortDateString();
        }

        #endregion

        #endregion


        #region 以前页面代码
        ///// <summary>
        ///// 景区详细信息页
        ///// 功能：详细信息显示
        ///// 创建人：戴银柱
        ///// 创建时间： 2010-07-23   
        ///// </summary>
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {

        //        //景区本周最热景点列表初始化
        //        this.NewAttrControl1.CityId = this.CityId;
        //        //景区特价票列表初始化
        //        this.TicketsControl1.CityId = this.CityId;
        //        this.CityAndMenu1.HeadMenuIndex = 5;
        //        //接收ID进行景区详细信息初始化
        //        string agencyId = Request.QueryString["Cid"];

        //        if(agencyId!=null)
        //        {
        //            this.GeneralShopControl1.SetAgencyId = agencyId.ToString();
        //            this.ViewRightControl1.Cid = this.CityId;
        //        }

        //    }
        //}
        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    //设置Title.....
        //    this.Title = string.Format(EyouSoft.Common.PageTitle.ScenicDetail_Title, CityModel.CityName, GeneralShopControl1.CompanyName);
        //    AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.ScenicDetail_Des, CityModel.CityName, GeneralShopControl1.CompanyName));
        //    AddMetaTag("keywords",string.Format(EyouSoft.Common.PageTitle.ScenicDetail_Keywords,GeneralShopControl1.CompanyName));
        //}
        #endregion
    }
}
