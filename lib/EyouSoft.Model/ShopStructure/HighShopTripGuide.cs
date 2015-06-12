using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：高级网店出游指南
    /// </summary>
    public class HighShopTripGuide
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopTripGuide() { }
        #endregion

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 类型 1.风土人情介绍  2.温馨提醒  3.综合介绍
        /// </summary>
        public TripGuideType? TypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string ContentText
        {
            get;
            set;
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImagePath
        {
            get;
            set;
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
        /// <summary>
        /// 置顶时间
        /// </summary>
        public DateTime TopTime
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion

        #region 出游指南类型 枚举
        /// <summary>
        /// 出游指南类型
        /// </summary>
        public enum TripGuideType
        {
            /// <summary>
            /// 风土人情
            /// </summary>
            风土人情 = 1,
            /// <summary>
            /// 温馨提醒
            /// </summary>
            温馨提醒 = 2,
            /// <summary>
            /// 综合介绍
            /// </summary>
            综合介绍 = 3,
            /// <summary>
            /// 旅游资源推荐
            /// </summary>
            旅游资源推荐 = 4,
            /// <summary>
            /// 景区动态
            /// </summary>
            景区动态 = 5,
            /// <summary>
            /// 景区美图
            /// </summary>
            景区美图 = 6,
            /// <summary>
            /// 景区导游
            /// </summary>
            景区导游 = 7,
            /// <summary>
            /// 景区攻略
            /// </summary>
            景区攻略 = 8,
            /// <summary>
            /// 景区美食
            /// </summary>
            景区美食 = 9,
            /// <summary>
            /// 景区住宿
            /// </summary>
            景区住宿 = 10,
            /// <summary>
            /// 景区交通
            /// </summary>
            景区交通 = 11,
            /// <summary>
            /// 景区购物
            /// </summary>
            景区购物 = 12,
            /// <summary>
            /// 景区线路
            /// </summary>
            景区线路 = 13,
            /// <summary>
            /// 门票政策
            /// </summary>
            门票政策 = 14

        }
        #endregion
    }
}
