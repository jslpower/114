using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间：2010-05-11
    /// 描述：基础数据-公用价格等级实体类
    /// </summary>
    public class CommonPriceStand
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonPriceStand() { }

        #region 属性
        /// <summary>
        /// 公用价格等级编号(默认为newid())
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 等级类型(默认为0)
        /// </summary>
        public CommPriceTypeID TypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string PriceStandName
        {
            get;
            set;
        }
        #endregion

    }
    #region 公用价格类型枚举
    /// <summary>
    /// 公用价格类型枚举
    /// </summary>
    public enum CommPriceTypeID
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 1,
        /// <summary>
        /// 火车
        /// </summary>
        火车,
        /// <summary>
        /// 飞机
        /// </summary>
        飞机,
        /// <summary>
        /// 轮船
        /// </summary>
        轮船,
        /// <summary>
        /// 其他
        /// </summary>
        其他
    }
    #endregion
}
