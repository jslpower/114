using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    #region 枚举类型
    /// <summary>
    /// 航程
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public enum VoyageType
    {
        /// <summary>
        /// 所有=0
        /// </summary>
        所有 = 0,
        /// <summary>
        /// 单程票=1
        /// </summary>
        单程 = 1,
        /// <summary>
        /// 往返票=2
        /// </summary>
        往返程 = 2,
        /// <summary>
        /// 中转联程票=3
        /// </summary>
        缺口程 = 3
    }

    /// <summary>
    /// 乘客类型
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public enum PeopleCountryType
    { 
        /// <summary>
        /// 内宾/国内乘客=1
        /// </summary>
        Home = 1,

        /// <summary>
        /// 外宾/国外乘客=2
        /// </summary>
        Foreign = 2
    }
    #endregion

    /// <summary>
    /// 机票航班信息实体类
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public class TicketFlight
    {
        #region 类成员
        #region 私有变量
        private DateTime? _TakeOffDate = null;
        private DateTime? _ReturnDate = null;
        private VoyageType _VoyageSet = VoyageType.单程;
        private PeopleCountryType _PeopleCountryType = PeopleCountryType.Home;
        #endregion

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime? TakeOffDate
        {
            set { this._TakeOffDate = value; }
            get { return this._TakeOffDate; }
        }
        /// <summary>
        /// 返程时间
        /// </summary>
        public DateTime? ReturnDate
        {
            set { this._ReturnDate = value; }
            get { return this._ReturnDate; }
        }

        /// <summary>
        /// 航程,默认为单程
        /// </summary>
        public VoyageType VoyageSet
        {
            set { this._VoyageSet = value; }
            get { return this._VoyageSet; }
        }

        /// <summary>
        /// 乘机人数
        /// </summary>
        public int PeopleNumber { get; set; }

        /// <summary>
        /// 乘客类型,默认为内宾
        /// </summary>
        public PeopleCountryType PeopleCountryType { get { return this._PeopleCountryType; } set { this._PeopleCountryType = value; } }
        #endregion 类成员
    }
}
