using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店搜索用户控件
    /// 功能：提供酒店的快捷搜索
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class HotelSearchControl : System.Web.UI.UserControl
    {
        #region 控件属性
        //当前城市ID
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        //城市三字码
        private string _cityCode;

        public string CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }
        //入住时间
        private string _inTime;

        public string InTime
        {
            get { return _inTime; }
            set { _inTime = value; }
        }
        //离开时间
        private string _leaveTime;

        public string LeaveTime
        {
            get { return _leaveTime; }
            set { _leaveTime = value; }
        }
        //最小金额
        private decimal _priceBegin;

        public decimal PriceBegin
        {
            get { return _priceBegin; }
            set { _priceBegin = value; }
        }
        //最大金额
        private decimal _priceEnd;

        public decimal PriceEnd
        {
            get { return _priceEnd; }
            set { _priceEnd = value; }
        }
        //酒店等级
        private string _hotelLevel;

        public string HotelLevel
        {
            get { return _hotelLevel; }
            set { _hotelLevel = value; }
        }
        //关键字
        private string _hotelName;

        public string HotelName
        {
            get { return _hotelName; }
            set { _hotelName = value; }
        }
        //即时确认
        private int _instant;

        public int Instant
        {
            get { return _instant; }
            set { _instant = value; }
        }
        //接机服务
        private int _service;

        public int Service
        {
            get { return _service; }
            set { _service = value; }
        }
        //上网设施
        private int _internet;

        public int Internet
        {
            get { return _internet; }
            set { _internet = value; }
        }
        //装修时间
        private string _decoration;

        public string Decoration
        {
            get { return _decoration; }
            set { _decoration = value; }
        }
        //特殊房型
        private string _special;

        public string Special
        {
            get { return _special; }
            set { _special = value; }
        }
        //床型
        private string _bed;

        public string Bed
        {
            get { return _bed; }
            set { _bed = value; }
        }
        #endregion

        //图片URL
        private string _imageServerPath;

        public string ImageServerPath
        {
            get { return _imageServerPath; }
            set { _imageServerPath = value; }
        }
        //地理位置
        private string _landMark;

        public string LandMark
        {
            get { return _landMark; }
            set { _landMark = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 用户控件 页面数据初始化
                this.txtInTime.Value = this.InTime;
                this.txtLeaveTime.Value = this.LeaveTime;
                this.txtPriceBegin.Value = this.PriceBegin.ToString();
                if (this.txtPriceBegin.Value == "0") { this.txtPriceBegin.Value = ""; }
                this.txtPriceEnd.Value = this.PriceEnd.ToString();
                if (this.txtPriceEnd.Value == "0") { this.txtPriceEnd.Value = ""; }
                HotelLevelInit(this.HotelLevel);
                this.txtHotelName.Value = this.HotelName;
                //即时确认
                if (this.Instant == 1)
                {
                    this.cbx_Instant.Checked = true;
                }
                else
                {
                    this.cbx_Instant.Checked = false;
                }

                //接机服务
                if (this.Service == 1)
                {
                    this.cbx_Service.Checked = true;
                }
                else
                {
                    this.cbx_Service.Checked = false;
                }

                //上网设施
                if (this.Internet == 1)
                {
                    this.cbx_Internet.Checked = true;
                }
                else
                {
                    this.cbx_Internet.Checked = false;
                }
                //装修时间
                this.ddl_Decoration.SelectedValue = this.Decoration;
                //特殊房型
                this.ddl_Special.SelectedValue = this.Special;
                //床型
                this.ddl_Bed.SelectedValue = this.Bed;
                //城市三字码
                this.hideCityCode.Value = this.CityCode;
                //地理位置
                this.hideGeography.Value = this.LandMark;
                #endregion 
            }
        }

        /// <summary>
        /// 酒店星级初始化
        /// </summary>
        /// <param name="hotelLevel"></param>
        protected void HotelLevelInit(string hotelLevel)
        {
            IList<ListItem> list = EyouSoft.Common.Function.EnumHandle.GetListEnumValue(typeof(EyouSoft.HotelBI.HotelRankEnum));
            this.ddl_HotelLevel.Items.Clear();
            string str = "一二三四五";
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].Value;
                    if (i == 0)
                    {
                        item.Text = "--不限--";
                    }
                    if (i > 0 && i < 6)
                    {
                        item.Text = str.Substring(i-1, 1) + "星级";
                    }
                    if (i >= 6)
                    {
                        item.Text = "准" + str.Substring(i - 6, 1) + "星级";
                    }
                    this.ddl_HotelLevel.Items.Add(item);
                }
            }
            if (hotelLevel != null && hotelLevel.Trim() != "")
            {
                this.ddl_HotelLevel.SelectedValue = hotelLevel;
            }
            list = null;
        }
    }
}