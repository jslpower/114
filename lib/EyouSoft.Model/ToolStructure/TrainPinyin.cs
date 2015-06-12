using System;
namespace EyouSoft.Model.ToolStructure
{
    /// <summary>
    /// 站台名称拼音-省市
    /// </summary>
    public class TrainPinyin
    {
        public TrainPinyin()
        { }
        #region Model
        private string _station;
        private string _shortcode;
        private string _fullcode;
        private string _province;
        private string _propinyin;
        private int _call;
        /// <summary>
        /// 车站
        /// </summary>
        public string Station
        {
            set { _station = value; }
            get { return _station; }
        }
        /// <summary>
        /// 拼音缩写
        /// </summary>
        public string Shortcode
        {
            set { _shortcode = value; }
            get { return _shortcode; }
        }
        /// <summary>
        /// 完整拼音
        /// </summary>
        public string FullCode
        {
            set { _fullcode = value; }
            get { return _fullcode; }
        }
        /// <summary>
        /// 省份/直辖市
        /// </summary>
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 省份/直辖市 拼音
        /// </summary>
        public string ProPinyin
        {
            set { _propinyin = value; }
            get { return _propinyin; }
        }
        /// <summary>
        /// 备用，可用于计数
        /// </summary>
        public int Call
        {
            set { _call = value; }
            get { return _call; }
        }
        #endregion Model

    }
}

