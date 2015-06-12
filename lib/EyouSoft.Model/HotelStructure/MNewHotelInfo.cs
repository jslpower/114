using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.HotelBI;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 二次整改酒店管理实体类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class MNewHotelInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName
        {
            get;
            set;
        }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            get;
            set;
        }

        /// <summary>
        /// 城市区域类型
        /// </summary>
        public CityAreaType CityAreaType
        {
            get;
            set;
        }

        /// <summary>
        /// 酒店星级
        /// </summary>
        public HotelStarType HotelStar
        {
            get;
            set;
        }

        /// <summary>
        /// 门市价格
        /// </summary>
        public decimal MenShiPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 团队价格
        /// </summary>
        public decimal TeamPrice
        {
            get;
            set;
        }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get;
            set;
        }


        /// <summary>
        /// 发布人
        /// </summary>
        public int OperateId
        {
            get;
            set;
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime OperateTime
        {
            get;
            set;
        }

    }
    /// <summary>
    /// 酒店星级枚举
    /// </summary>
    public enum HotelStarType
    {
        /// <summary>
        /// 一星
        /// </summary>
        一星 = 1,
        /// <summary>
        /// 二星
        /// </summary>
        二星 = 2,
        /// <summary>
        /// 三星
        /// </summary>
        三星 = 3,
        /// <summary>
        /// 四星
        /// </summary>
        四星 = 4,
        /// <summary>
        /// 五星
        /// </summary>
        五星 = 5
    }
    /// <summary>
    /// 城市
    /// </summary>
    public enum CityAreaType
    {
        华东五市 = 0,
        港澳 = 1
    }

}
