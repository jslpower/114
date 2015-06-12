using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店高级搜索页面
    /// 功能：提供酒店的高级搜索
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class AdvanceSearch : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置title
                this.Page.Title =EyouSoft.Common.PageTitle.Hotel_Title;
                //设置关键字
                AddMetaTag("description", EyouSoft.Common.PageTitle.Hotel_Des);
                AddMetaTag("keywords", EyouSoft.Common.PageTitle.Hotel_Keywords);
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 4;

                //城市数据初始化
                CityDataInit();
                //行政区域数据初始化
                RegionDataInit();
                //地理位置数据初始化
                ProvinceDataInit(null);
                //显示酒店星级
                HotelLevelInit(null);

                //设置用户控件CityId
                this.CommonUserControl1.CityId = this.CityId;
                this.ImgFristControl1.ImageWidth = "225px";
                this.ImgSecondControl1.ImageWidth = "225px";
            }
        }

        #region 城市数据初始化
        /// <summary>
        /// 城市数据初始化
        /// </summary>
        protected void CityDataInit()
        {
            IList<EyouSoft.Model.HotelStructure.HotelCity> list = EyouSoft.BLL.HotelStructure.HotelCity.CreateInstance().GetList();  //获取所有城市的集合
            this.ddl_City.Items.Clear();
            this.ddl_City.Items.Add(new ListItem("--请选择--", "0")); //添加默认
            //清空城市隐藏域值
            this.hideCity.Value = "";
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //城市简拼:城市三字码:城市名称
                    this.hideCity.Value += list[i].Spelling + ":" + list[i].CityCode + ":" + list[i].CityName + "|";
                }
            }
            //去掉最后一个 | 
            this.hideCity.Value = this.hideCity.Value.TrimEnd('|');
            list = null;
        }
        #endregion

        #region 数据初始化
        /// <summary>
        /// 行政区域数据初始化
        /// </summary>
        protected void RegionDataInit()
        {
            IList<EyouSoft.Model.HotelStructure.HotelCityAreas> list = EyouSoft.BLL.HotelStructure.HotelCityAreas.CreateInstance().GetList(null);
            if (list != null && list.Count > 0)
            {
                this.ddlRegion.Items.Clear();
                this.ddlRegion.Items.Add(new ListItem("--请选择--", ""));
                this.hideRegion.Value = "";
                for (int i = 0; i < list.Count; i++)
                {
                    //行政区域ID ： 城市区域编号 ： 城市区域名
                    this.hideRegion.Value += list[i].Id + ":" + list[i].CityCode + ":" + list[i].AreaName + "|";
                }
                this.hideRegion.Value = this.hideRegion.Value.TrimEnd('|');
            }
            list = null;
        }
        #endregion

        #region 地理位置初始化
        /// <summary>
        /// 地理位置初始化
        /// </summary>
        protected void ProvinceDataInit(string cityCode)
        {
            this.ddlGeography.Items.Clear();
            this.ddlGeography.Items.Add(new ListItem("--请选择--", ""));
            this.hideGeography.Value = "";
            IList<EyouSoft.Model.HotelStructure.HotelLandMarks> list = EyouSoft.BLL.HotelStructure.HotelLandMarks.CreateInstance().GetList(cityCode);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //ListItem item = new ListItem();
                    //item.Value = list[i].CityCode;
                    //item.Text = list[i].Por;
                    // this.ddlGeography.Items.Add(item);
                    this.hideGeography.Value += list[i].Id + ":" + list[i].CityCode + ":" + list[i].Por + "|";
                }
            }
            this.hideGeography.Value = this.hideGeography.Value.TrimEnd('|');
            list = null;
        }
        #endregion

        #region 根据酒店星级显示信息
        /// <summary>
        /// 酒店星级初始化
        /// </summary>
        /// <param name="hotelLevel"></param>
        protected void HotelLevelInit(string hotelLevel)
        {
            IList<ListItem> list = EyouSoft.Common.Function.EnumHandle.GetListEnumValue(typeof(EyouSoft.HotelBI.HotelRankEnum));
            this.ddlHotelStar.Items.Clear();
            string str = "一二三四五";
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].Value;
                    //添加默认
                    if (i == 0)
                    {
                        item.Text = "--不限--";
                    }
                    //小于6的为星级的
                    if (i > 0 && i < 6)
                    {
                        item.Text = str.Substring(i - 1, 1) + "星级";
                    }
                    //大于6的为准星级的
                    if (i >= 6)
                    {
                        item.Text = "准" + str.Substring(i - 6, 1) + "星级";
                    }
                    this.ddlHotelStar.Items.Add(item);
                }
            }
            if (hotelLevel != null && hotelLevel.Trim() != "")
            {
                //如果不为空 那么选中
                this.ddlHotelStar.SelectedValue = hotelLevel;
            }
            list = null;
        }
        #endregion
    }
}
