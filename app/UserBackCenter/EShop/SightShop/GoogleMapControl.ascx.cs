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

namespace UserBackCenter.EShop.SightShop
{
    public partial class GoogleMapControl1 : System.Web.UI.UserControl
    {
        private double _Longitude = 121.423532; //经度
        private double _Latitude = 31.186651;  //纬度
        private int _width = 236;
        private int _height = 236;
        private bool _isborder = true;
        private string _title = "谷歌地图";
        private string _loadintText = "loading...";
        private int _ShowTitleDivWidth = 300;
        private int _ShowTitleDivHeight = 20;
        private bool _IsShowTitle = false;
        private bool _IsShowGLargeMap = false;
        private bool _IsShowGOverviewMap = false;

        /// <summary>
        /// 谷歌地图APIKey
        /// </summary>
        public string GoogleMapKey { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        /// <summary>
        /// 要显示地图的宽度 默认236
        /// </summary>
        public int ShowMapWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 要显示地图的高度 默认236
        /// </summary>
        public int ShowMapHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 载入地图是显示的提示文字   默认 loading...
        /// </summary>
        public string LoadintText
        {
            get { return _loadintText; }
            set { _loadintText = value; }
        }

        /// <summary>
        /// 地图是否显示边框
        /// </summary>
        public bool IsBorder
        {
            get { return _isborder; }
            set { _isborder = value; }
        }

        /// <summary>
        /// 是否显示提示层 如果为false 则 ShowTitleDivWidth、ShowTitleDivHeight属性无效
        /// </summary>
        public bool IsShowTitle
        {
            get { return _IsShowTitle; }
            set { _IsShowTitle = value; }
        }

        /// <summary>
        /// 提示层显示的文字(Html)  默认 谷歌地图
        /// </summary>
        public string ShowTitleText
        {
            get { return _title; }
            set { _title = value; }
        }


        /// <summary>
        /// 提示层的宽度 默认300px
        /// </summary>
        public int ShowTitleDivWidth
        {
            get { return _ShowTitleDivWidth; }
            set { _ShowTitleDivWidth = value; }
        }

        /// <summary>
        /// 提示层的高度 默认20px
        /// </summary>
        public int ShowTitleDivHeight
        {
            get { return _ShowTitleDivHeight; }
            set { _ShowTitleDivHeight = value; }
        }

        /// <summary>
        /// 是否显示平移及缩放控件（左上角）
        /// </summary>
        public bool IsShowGLargeMap
        {
            get { return _IsShowGLargeMap; }
            set { _IsShowGLargeMap = value; }
        }

        /// <summary>
        /// 是否显示缩略图控件（右下角）
        /// </summary>
        public bool IsShowGOverviewMap
        {
            get { return _IsShowGOverviewMap; }
            set { _IsShowGOverviewMap = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}