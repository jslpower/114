using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// 团队枚举
/// 创建人:郑付杰
/// 创建时间：2011-12-19
namespace EyouSoft.Model.NewTourStructure
{
    /// <summary>
    /// 团队状态
    /// </summary>
    public enum TourStatus
    {
        /// <summary>
        /// 客满
        /// </summary>
        客满 = 1,
        /// <summary>
        /// 停收
        /// </summary>
        停收 = 2,
        /// <summary>
        /// 收客
        /// </summary>
        收客 = 3
    }

    /// <summary>
    /// 散拼团队状态
    /// </summary>
    public enum PowderTourStatus
    {
        /// <summary>
        /// 客满
        /// </summary>
        客满 = 1,
        /// <summary>
        /// 停收
        /// </summary>
        停收 = 2,
        /// <summary>
        /// 收客
        /// </summary>
        收客 = 3,
        /// <summary>
        /// 成团
        /// </summary>
        成团 = 4
    }

    /// <summary>
    /// 散拼订单状态
    /// </summary>
    public enum PowderOrderStatus
    {
        /// <summary>
        /// 组团社待处理
        /// </summary>
        组团社待处理 = 0,
        /// <summary>
        /// 组团社已阅
        /// </summary>
        组团社已阅 = 1,
        /// <summary>
        /// 专线商待处理
        /// </summary>
        专线商待处理=2,
        /// <summary>
        /// 专线商预留
        /// </summary>
        专线商预留 = 3,
        /// <summary>
        /// 专线商已阅
        /// </summary>
        专线商已阅 = 4,
        /// <summary>
        /// 预留过期
        /// </summary>
        预留过期 = 5,
        /// <summary>
        /// 专线商已确定
        /// </summary>
        专线商已确定 = 6,
        /// <summary>
        /// 结单
        /// </summary>
        结单 = 7,
        /// <summary>
        /// 取消
        /// </summary>
        取消 = 8
    }

    /// <summary>
    /// 团队订单状态
    /// </summary>
    public enum TourOrderStatus
    {
        /// <summary>
        /// 未确认
        /// </summary>
        未确认 = 0,
        /// <summary>
        /// 已确认
        /// </summary>
        已确认 = 1,
        /// <summary>
        /// 结单
        /// </summary>
        结单 = 2,
        /// <summary>
        /// 取消
        /// </summary>
        取消 = 3
    }

    /// <summary>
    /// 支付状态
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// 游客未支付
        /// </summary>
        游客未支付 = 1,
        /// <summary>
        /// 游客待支付
        /// </summary>
        游客待支付 = 2,
        /// <summary>
        /// 游客已支付定金
        /// </summary>
        游客已支付定金 = 3,
        /// <summary>
        /// 游客已支付全款
        /// </summary>
        游客已支付全款 = 4,
        /// <summary>
        /// 组团社已付定金
        /// </summary>
        组团社已付定金 = 5,
        /// <summary>
        /// 组团社已付全款
        /// </summary>
        组团社已付全款 = 6,
        /// <summary>
        /// 结账
        /// </summary>
        结账 = 7,
        /// <summary>
        /// 申请退款
        /// </summary>
        申请退款 = 8,
        /// <summary>
        /// 已退款
        /// </summary>
        已退款 = 9
    }

    /// <summary>
    /// 游客身份类型
    /// </summary>
    public enum CradType
    {
        /// <summary>
        /// 成人
        /// </summary>
        成人 = 1,
        /// <summary>
        /// 儿童
        /// </summary>
        儿童 = 2
    }

    /// <summary>
    /// 订单来源
    /// </summary>
    public enum OrderSource
    {
        /// <summary>
        /// 线路散拼订单
        /// </summary>
        线路散拼订单 = 1,
        /// <summary>
        /// 线路团队订单
        /// </summary>
        线路团队订单 = 2,
        /// <summary>
        /// 景区门票订单
        /// </summary>
        景区门票订单 = 3,
        /// <summary>
        /// 酒店订单
        /// </summary>
        酒店订单 = 4,
        /// <summary>
        /// 机票订单
        /// </summary>
        机票订单 = 5
    }

    /// <summary>
    /// 出游天数
    /// </summary>
    public enum PowderDay
    {
        /// <summary>
        /// 一日游
        /// </summary>
        一日游 = 1,
        /// <summary>
        /// 两日游
        /// </summary>
        两日游 = 2,
        /// <summary>
        /// 三日游
        /// </summary>
        三日游 = 3,
        /// <summary>
        /// 四日游
        /// </summary>
        四日游 = 4,
        /// <summary>
        /// 五日游
        /// </summary>
        五日游 = 5,
        /// <summary>
        /// 六日游
        /// </summary>
        六日游 = 6,
        /// <summary>
        /// 七日游
        /// </summary>
        七日游 = 7,
        /// <summary>
        /// 八日游
        /// </summary>
        八日游 = 8,
        /// <summary>
        /// 九日游
        /// </summary>
        九日游 = 9,
        /// <summary>
        /// 十日游
        /// </summary>
        十日游 = 10,
        /// <summary>
        /// 四日游及以上
        /// </summary>
        四日游及以上 = 11,
        /// <summary>
        /// 五日游及以下
        /// </summary>
        五日游及以下 = 12,
        /// <summary>
        /// 七日游及以上
        /// </summary>
        七日游及以上 = 13,
        /// <summary>
        /// 八至十日游
        /// </summary>
        八至十日游 = 14,
        /// <summary>
        /// 十日游及以上
        /// </summary>
        十日游及以上 = 15,
        /// <summary>
        /// 三日游及以上
        /// </summary>
        三日游及以上 = 16,
    }

}
