using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// 线路枚举
/// 创建人：郑付杰
/// 创建时间：2011-12-16
namespace EyouSoft.Model.NewTourStructure
{
    /// <summary>
    /// B2B显示控制
    /// </summary>
    public enum RouteB2BDisplay
    {
        /// <summary>
        /// 常规
        /// </summary>
        常规 = 0,
        /// <summary>
        /// 侧边
        /// </summary>
        侧边 = 1,
        /// <summary>
        /// 列表
        /// </summary>
        列表 = 2,
        /// <summary>
        /// 首页
        /// </summary>
        首页 = 3,
        /// <summary>
        /// 隐藏
        /// </summary>
        隐藏 = 99
    }

    /// <summary>
    /// B2C显示控制
    /// </summary>
    public enum RouteB2CDisplay
    {
        /// <summary>
        /// 常规
        /// </summary>
        常规 = 0,
        /// <summary>
        /// 侧边
        /// </summary>
        侧边 = 1,
        /// <summary>
        /// 列表
        /// </summary>
        列表 = 2,
        /// <summary>
        /// 首页
        /// </summary>
        首页 = 3,
        /// <summary>
        /// 隐藏
        /// </summary>
        隐藏 = 99
    }

    /// <summary>
    /// 推荐类型
    /// </summary>
    public enum RecommendType
    {
        /// <summary>
        /// 无
        /// </summary>
        无 = 1,
        /// <summary>
        /// 推荐
        /// </summary>
        推荐 = 2,
        /// <summary>
        /// 特价
        /// </summary>
        特价 = 3,
        /// <summary>
        /// 豪华
        /// </summary>
        豪华 = 4,
        /// <summary>
        /// 热门
        /// </summary>
        热门 = 5,
        /// <summary>
        /// 新品
        /// </summary>
        新品 = 6,
        /// <summary>
        /// 纯玩
        /// </summary>
        纯玩 = 7,
        /// <summary>
        /// 经典
        /// </summary>
        经典 = 8
    }
    /// <summary>
    /// 线路上下架状态
    /// </summary>
    public enum RouteStatus
    {
        /// <summary>
        /// 上架
        /// </summary>
        上架 = 1,
        /// <summary>
        /// 下架
        /// </summary>
        下架 = 2
    }
    /// <summary>
    /// 大交通
    /// </summary>
    public enum TrafficType
    {
        /// <summary>
        /// 飞机
        /// </summary>
        飞机 = 1,
        /// <summary>
        /// 大巴
        /// </summary>
        大巴 = 2,
        /// <summary>
        /// 火车
        /// </summary>
        火车 = 3,
        /// <summary>
        /// 轮船
        /// </summary>
        轮船 = 4,
        /// <summary>
        /// 不包含
        /// </summary>
        不包含 = 5,
        /// <summary>
        /// 其它
        /// </summary>
        其它 = 6
    }

    /// <summary>
    /// 线路来源
    /// </summary>
    public enum RouteSource
    {
        /// <summary>
        /// 专线商添加
        /// </summary>
        专线商添加 = 1,
        /// <summary>
        /// 地接社添加
        /// </summary>
        地接社添加
    }

    #region 线路搜索枚举

    /// <summary>
    /// 搜索线路天数
    /// </summary>
    public enum PublicCenterRouteDay
    {
        /// <summary>
        /// 全部
        /// </summary>
        全部 = 0,
        /// <summary>
        /// 1日
        /// </summary>
        _1日,
        /// <summary>
        /// 2日
        /// </summary>
        _2日,
        /// <summary>
        /// 3日
        /// </summary>
        _3日,
        /// <summary>
        /// 4日
        /// </summary>
        _4日,
        /// <summary>
        /// 5日
        /// </summary>
        _5日,
        /// <summary>
        /// 6日
        /// </summary>
        _6日,
        /// <summary>
        /// 7日
        /// </summary>
        _7日,
        /// <summary>
        /// 7日以上
        /// </summary>
        _7日以上
    }

    /// <summary>
    /// 搜索线路市场价
    /// </summary>
    public enum PublicCenterPrice
    {
        /// <summary>
        /// 全部
        /// </summary>
        全部 = 0,
        /// <summary>
        /// 100元以下
        /// </summary>
        _100元以下,
        /// <summary>
        /// 100-300元
        /// </summary>
        _100到300元,
        /// <summary>
        /// 300-1000元
        /// </summary>
        _300到1000元,
        /// <summary>
        /// 1000-3000元
        /// </summary>
        _1000到3000元,
        /// <summary>
        /// 3000-10000元
        /// </summary>
        _3000到10000元,
        /// <summary>
        /// 10000以上
        /// </summary>
        _10000以上
    }

    #endregion
}
