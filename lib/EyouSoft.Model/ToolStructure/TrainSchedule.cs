using System;
namespace EyouSoft.Model.ToolStructure
{
    /// <summary>
    /// 列车时刻详细信息
    /// </summary>
    public class TrainSchedule
    {
        public TrainSchedule()
        { }
        #region Model
        private string _id;
        private string _type;
        private string _station;
        private int _s_no;
        private int _day;
        private string _a_time;
        private string _d_time;
        private string _r_date;
        private int _distance;
        private string _p1;
        private string _p2;
        private string _p3;
        private string _p4;
        /// <summary>
        /// 车次
        /// </summary>
        public string TrainID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 列车类型
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 车站
        /// </summary>
        public string Station
        {
            set { _station = value; }
            get { return _station; }
        }
        /// <summary>
        /// 站序
        /// </summary>
        public int SiteNo
        {
            set { _s_no = value; }
            get { return _s_no; }
        }
        /// <summary>
        /// 天数
        /// </summary>
        public int Day
        {
            set { _day = value; }
            get { return _day; }
        }
        /// <summary>
        /// 到达时间
        /// </summary>
        public string ArriveTime
        {
            set { _a_time = value; }
            get { return _a_time; }
        }
        /// <summary>
        /// 开车时间
        /// </summary>
        public string StartTime
        {
            set { _d_time = value; }
            get { return _d_time; }
        }
        /// <summary>
        /// 运行时间
        /// </summary>
        public string RunDate
        {
            set { _r_date = value; }
            get { return _r_date; }
        }
        /// <summary>
        /// 里程
        /// </summary>
        public int Distance
        {
            set { _distance = value; }
            get { return _distance; }
        }
        /// <summary>
        /// 硬座
        /// </summary>
        public string P1
        {
            set { _p1 = value; }
            get { return _p1; }
        }
        /// <summary>
        /// 软座
        /// </summary>
        public string P2
        {
            set { _p2 = value; }
            get { return _p2; }
        }
        /// <summary>
        /// 硬卧上/中/下
        /// </summary>
        public string P3
        {
            set { _p3 = value; }
            get { return _p3; }
        }
        /// <summary>
        /// 软卧上/下
        /// </summary>
        public string P4
        {
            set { _p4 = value; }
            get { return _p4; }
        }
        #endregion Model

    }
}

