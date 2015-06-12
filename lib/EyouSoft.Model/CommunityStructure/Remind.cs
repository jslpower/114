using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：华磊 2011-5-6
    /// 描述：首页登录下面的提醒
    /// </summary>
    [Serializable]
    public class Remind
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Remind() { }

        #region model

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public TitleTypes TilteType
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime EventTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay
        {
            get;
            set;
        }
        /// <summary>
        /// 类型值
        /// </summary>
        public int Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string TypeLink
        {
            get;
            set;
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID
        {
            get;
            set;
        }
        #endregion
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum TitleTypes
    { 
        /// <summary>
        /// 注册成功
        /// </summary>
        注册成功 = 1,
        /// <summary>
        /// 发布团队
        /// </summary>
        发布团队,
        /// <summary>
        /// 促销
        /// </summary>
        促销,
        /// <summary>
        /// 客满
        /// </summary>
        客满,
        /// <summary>
        /// 短信充值
        /// </summary>
        短信充值,
        /// <summary>
        /// 发布供求
        /// </summary>
        发布供求,
        /// <summary>
        /// 预订机票
        /// </summary>
        预订机票,
        /// <summary>
        /// 预订酒店
        /// </summary>
        预订酒店,
        /// <summary>
        /// 预订旅游线路
        /// </summary>
        预订旅游线路,
        /// <summary>
        /// 加MQ好友
        /// </summary>
        加MQ好友,
        /// <summary>
        /// 留言询价
        /// </summary>
        留言询价
    }
}
