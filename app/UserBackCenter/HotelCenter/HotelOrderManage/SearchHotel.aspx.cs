using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 说明：用户后台—旅游资源—预订酒店
    /// 创建人：徐从栎
    /// 创建时间：2011-12-14
    /// </summary>
    public partial class SearchHotel : BackPage
    {
        protected string tblID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //使用GUID作为页面Table容器的id.
                this.tblID = Guid.NewGuid().ToString();
                this.initData();
            }
        }
        protected void initData()
        { 
            //城市
            this.InitHotelCity();
            //行政区域
            this.RegionDataInit();
            //地理位置
            this.ddlRegion.DataSource = null;
            this.ddlRegion.DataBind();
            //酒店星级
            this.HotelLevelInit(null);
        }
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
                //this.hideRegion.Value = "";
                for (int i = 0; i < list.Count; i++)
                {
                    //行政区域ID ： 城市区域编号 ： 城市区域名
                    this.hideRegion.Value += list[i].Id + ":" + list[i].CityCode + ":" + list[i].AreaName + "|";
                }
                this.hideRegion.Value = this.hideRegion.Value.TrimEnd('|');
            }
            list = null;
        }
        /// <summary>
        /// 绑定酒店所在目的地
        /// </summary>
        protected void InitHotelCity()
        {
            var s = "SearchHotel.CityList={0}";
            IList<EyouSoft.Model.HotelStructure.HotelCity> Model_HotelCity = EyouSoft.BLL.HotelStructure.HotelCity.CreateInstance().GetList(); //获取所有的城市信息列表
            this.ddlCity.Items.Clear();  //清空下拉框项
            if (Model_HotelCity != null && Model_HotelCity.Count > 0) //城市列表不为空
            {
                this.ddlCity.Items.Add(new ListItem("--请选择城市--", "0"));
                for (int i = 0; i < Model_HotelCity.Count; i++)
                {
                    ListItem list = new ListItem();
                    list.Value = Model_HotelCity[i].CityCode;
                    list.Text = Model_HotelCity[i].Spelling + Model_HotelCity[i].CityName + Model_HotelCity[i].CityCode;
                    this.ddlCity.Items.Add(list);
                }
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format(s, Newtonsoft.Json.JsonConvert.SerializeObject(Model_HotelCity)), true);
        }
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
    }
}
