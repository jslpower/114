using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ScenicStructure
{
    /// <summary>
    /// 景区等级
    /// </summary>
    public enum ScenicLevel
    {
        /// <summary>
        /// A = 1
        /// </summary>
        A = 1,
        /// <summary>
        /// AA
        /// </summary>
        AA,
        /// <summary>
        /// AAA
        /// </summary>
        AAA,
        /// <summary>
        /// AAAA
        /// </summary>
        AAAA,
        /// <summary>
        /// AAAAA
        /// </summary>
        AAAAA
    }

    /// <summary>
    /// B2B显示控制
    /// </summary>
    public enum ScenicB2BDisplay
    {
        /// <summary>
        /// 首页推荐,
        /// </summary>
        首页推荐 = 0,
        /// <summary>
        /// 列表置顶,
        /// </summary>
        列表置顶,
        /// <summary>
        ///  侧边推荐,
        /// </summary>
        侧边推荐,
        /// <summary>
        /// 常规
        /// </summary>
        常规,
        /// <summary>
        /// 隐藏,
        /// </summary>
        隐藏 = 99
    }
    /// <summary>
    /// B2C显示控制
    /// </summary>
    public enum ScenicB2CDisplay
    {
        /// <summary>
        /// 首页推荐,
        /// </summary>
        首页推荐 = 0,
        /// <summary>
        /// 列表置顶,
        /// </summary>
        列表置顶,
        /// <summary>
        ///  侧边推荐,
        /// </summary>
        侧边推荐,
        /// <summary>
        /// 常规
        /// </summary>
        常规,
        /// <summary>
        /// 隐藏,
        /// </summary>
        隐藏 = 99
    }

    /// <summary>
    /// 景区状态
    /// </summary>
    public enum ExamineStatus
    {
        /// <summary>
        /// 待审核 = 1,
        /// </summary>
        待审核 = 1,
        /// <summary>
        /// 待审核
        /// </summary>
        已审核
    }

    /// <summary>
    /// 景区门票状态
    /// </summary>
    public enum ScenicTicketsStatus
    {
        /// <summary>
        /// 上架,
        /// </summary>
        上架 = 1,
        /// <summary>
        /// 下架,
        /// </summary>
        下架,
        /// <summary>
        /// 申请删除
        /// </summary>
        申请删除,
    }

    /// <summary>
    /// 景区图片类型
    /// </summary>
    public enum ScenicImgType
    {
        /// <summary>
        /// 景区形象 = 1,
        /// </summary>
        景区形象 = 1,
        /// <summary>
        /// 景区导游地图,
        /// </summary>
        景区导游地图,
        /// <summary>
        /// 其他
        /// </summary>
        其他
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum ScenicPayment
    {
        /// <summary>
        /// 景区支付 = 1,
        /// </summary>
        景区支付 = 1,
        /// <summary>
        /// 先行支付,
        /// </summary>
        先行支付,
        /// <summary>
        ///  仅限旅行社预定
        /// </summary>
        仅限旅行社预定
    }
}
