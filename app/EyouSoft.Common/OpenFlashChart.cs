using System;
using System.Collections.Generic;
using System.Text;

 namespace EyouSoft.Common
{
    /// <summary>
    /// OpenFlashChart数据实体类
    /// </summary>
    public class OpenFlashDataSource
    {
        private string _text = "";
        private string _colorvalue = "";
        private string _onclickjsfun = "";
        private IList<OpenFlashOneLineData> _onelinedata = null;

        public OpenFlashDataSource() { }

        /// <summary>
        /// 线条数据源
        /// </summary>
        public IList<OpenFlashOneLineData> OneLineData
        {
            get { return _onelinedata; }
            set { _onelinedata = value; }
        }

        /// <summary>
        /// onclick的js函数名称
        /// </summary>
        public string OnClickJsFun
        {
            get { return _onclickjsfun; }
            set { _onclickjsfun = value; }
        }

        /// <summary>
        /// 文本说明
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// 文本以及线条的样式
        /// </summary>
        public string ColorValue
        {
            get { return _colorvalue; }
            set { _colorvalue = value; }
        }
    }

    /// <summary>
    /// OpenFlashChart  单个线条的数据
    /// </summary>
    public class OpenFlashOneLineData
    {
        private string _id = null;
        private double _value = 0.0;
        private DateTime _time = new DateTime();

        public OpenFlashOneLineData() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="time">当前数值对应的日期</param>
        public OpenFlashOneLineData(double value, DateTime time)
        {
            this._value = value;
            this._time = time;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ID">当前曲线所对应的唯一ID值</param>
        /// <param name="value">数值</param>
        /// <param name="time">当前数值对应的日期</param>
        public OpenFlashOneLineData(string ID, double value, DateTime time)
        {
            this._id = ID;
            this._value = value;
            this._time = time;
        }

        /// <summary>
        /// 当前曲线所对应的唯一ID值
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 数值
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 当前数值对应的日期
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }

    /// <summary>
    /// OpenFlashChart  节点的提示格式(x坐标值＋x坐标单位＋y坐标值＋y坐标单位＋其它提示)
    /// </summary>
    public class OpenFlashDotTipFormat
    {
        private string _x_unit = "";
        private string _y_unit = "";
        private string _tipother = "";

        public OpenFlashDotTipFormat() { }

        public OpenFlashDotTipFormat(string x_unit, string y_unit, string tipother)
        {
            this._x_unit = x_unit;
            this._y_unit = y_unit;
            this._tipother = tipother;
        }

        /// <summary>
        /// X坐标的数值单位
        /// </summary>
        public string X_Unit
        {
            get { return _x_unit; }
            set { _x_unit = value; }
        }

        /// <summary>
        /// Y坐标的数值单位
        /// </summary>
        public string Y_Unit
        {
            get { return _y_unit; }
            set { _y_unit = value; }
        }

        /// <summary>
        /// 其它提示
        /// </summary>
        public string TipOther
        {
            get { return _tipother; }
            set { _tipother = value; }
        }
    }
}
