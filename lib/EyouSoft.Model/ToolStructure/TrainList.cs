using System;
namespace EyouSoft.Model.ToolStructure
{
    /// <summary>
    /// 列车车次信息
    /// </summary>
    public class TrainList
    {
        public TrainList()
        { }
        #region Model
        private string _id;
        private string _type;
        private string _startstation;
        private string _endstation;
        private string _r_date;
        private int _distance;
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
        /// 始发站
        /// </summary>
        public string StartStation
        {
            set { _startstation = value; }
            get { return _startstation; }
        }
        /// <summary>
        /// 终点站
        /// </summary>
        public string EndStation
        {
            set { _endstation = value; }
            get { return _endstation; }
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
        #endregion Model

    }
}

