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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Xml;
using System.Collections.Generic;

namespace UserPublicCenter.ScenicManage
{

    /// <summary>
    /// 景区详细信息页
    /// 功能：详细信息显示
    /// 创建人：蔡永辉
    /// 创建时间： 2011-11-21   
    /// </summary>
    public partial class NewScenicDetails : EyouSoft.Common.Control.FrontPage
    {
        protected double _Longitude = 116.389503; //经度
        protected double _Latitude = 39.918953;  //纬度

        protected MScenicArea ScenicModel = null;
        protected string result = "";
        protected string defaultname = "该景区未指定";
        protected string GoogleMapKey = string.Empty;
        string scenicId = Utils.GetQueryStringValue("ScenicId");

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
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.ddl_ProvinceList.ClientID + "');");
            BindProvince();
            GoogleMapKey = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
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
            }
            //添加点击量
            EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().UpdateClickNum(scenicId);
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
            ScenicModel = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(scenicId);//"99f93a65-4c82-4448-bc0b-c4039bf5ca23" 测试Guid
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
                        result = item.ThumbAddress+"$"+item.Address+"$"+item.Description;
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
            if (ScenicModel.Description != string.Empty)
                AddMetaTag("description", string.Format("{0}", Utils.GetText2(Utils.TextToHtml(ScenicModel.Description), 100, false)));
            else
                AddMetaTag("description", " 暂无描述");
            AddMetaTag("keywords", string.Format("{0}，{1}，{2}，{3}，{4}", ScenicModel.ScenicName, ScenicModel.ScenicName + "门票", ScenicModel.ScenicName + "门票价格", ScenicModel.ScenicName + "特价门票", ScenicModel.ScenicName + "门票预定", ScenicModel.ScenicName + "介绍"));

            #endregion

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
            if (Utils.IsOpenHighShop(companyid))
            {
                result += string.Format("<a href=\"{0}\" title=\"{1}\">",
                        Utils.GetShopUrl(companyid, EyouSoft.Model.CompanyStructure.CompanyType.景区, -1),
                        companyname);
            }
            else
            {
                result += string.Format("<a href=\"{0}\" title=\"{1}\">",
                        "javascript:void(0);return false;",
                        companyname);
            }

            if (companyid == tongYe114SightId)
            {
                result += string.Format("{0}</a>", string.Empty);
            }
            else
            {
                result += string.Format("{0}</a>", subcompanname);
            }

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
                        result += item.ThemeName+" ";
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


    }

}
