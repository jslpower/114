using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.AdvStructure
{
    #region 广告栏目 (enum) AdvCatalog
    /// <summary>
    /// 广告栏目
    /// </summary>
    /// Author:汪奇志 2010-07-13
    public enum AdvCatalog
    {
        /// <summary>
        /// 首页广告
        /// </summary>
        首页广告=1,
        /// <summary>
        /// 线路频道
        /// </summary>
        线路频道,
        /// <summary>
        /// 机票频道
        /// </summary>
        机票频道,
        /// <summary>
        /// 景区频道
        /// </summary>
        景区频道,
        /// <summary>
        /// 酒店频道
        /// </summary>
        酒店频道,
        /// <summary>
        /// 车队频道
        /// </summary>
        车队频道,
        /// <summary>
        /// 旅游用品频道
        /// </summary>
        旅游用品频道,
        /// <summary>
        /// 购物点频道
        /// </summary>
        购物点频道,
        /// <summary>
        /// 供求信息频道
        /// </summary>
        供求信息频道,
        /// <summary>
        /// MQ广告
        /// </summary>
        MQ广告
    }
    #endregion

    #region 广告投放范围 (enum) AdvRange
    /// <summary>
    /// 广告投放范围
    /// </summary>
    /// Author:汪奇志 2010-07-13
    public enum AdvRange
    {
        /// <summary>
        /// 单位类型
        /// </summary>
        单位类型=0,
        /// <summary>
        /// 全国
        /// </summary>
        全国=1,
        /// <summary>
        /// 全省
        /// </summary>
        全省,
        /// <summary>
        /// 城市
        /// </summary>
        城市
    }
    #endregion

    #region 广告类别 (enum) AdvCategory
    /// <summary>
    /// 广告类别
    /// </summary>
    /// Author:汪奇志 2010-07-13
    public enum AdvCategory
    {
        /// <summary>
        /// 客户广告
        /// </summary>
        客户广告=1,
        /// <summary>
        /// 赠送广告
        /// </summary>
        赠送广告,
        /// <summary>
        /// 同业114广告
        /// </summary>
        同业114广告
    }
    #endregion

    #region 广告表现形式 (enum) AdvDisplayType
    /// <summary>
    /// 广告表现形式
    /// </summary>
    /// Author:汪奇志 2010-07-13
    public enum AdvDisplayType
    {
        /// <summary>
        /// 文字广告
        /// </summary>
        文字广告 = 1,
        /// <summary>
        /// 图片广告
        /// </summary>
        图片广告,
        /// <summary>
        /// 图文广告
        /// </summary>
        图文广告,
        /// <summary>
        /// MQ广告
        /// </summary>
        MQ广告,
        /// <summary>
        /// 单位文字广告
        /// </summary>
        单位文字广告,
        /// <summary>
        /// 单位图片广告
        /// </summary>
        单位图片广告,
        /// <summary>
        /// 单位图文广告
        /// </summary>
        单位图文广告,
        /// <summary>
        /// 供求图文广告
        /// </summary>
        供求图文广告
        
    }
    #endregion

    #region 广告投放类型 (enum) AdvType
    /// <summary>
    /// 广告投放类型
    /// </summary>
    /// Author:汪奇志 2010-07-13
    public enum AdvType
    {
        /// <summary>
        /// 城市
        /// </summary>
        城市=1,
        /// <summary>
        /// MQ
        /// </summary>
        MQ
    }
    #endregion

    #region 广告位置 (enum) AdvPosition
    /// <summary>
    /// 广告位置
    /// </summary>
    /// Author:汪奇志 2010-07-13
    /// 2011-5-6 华磊添加首页广告
    public enum AdvPosition
    {
        焦点图片 = 1,
        首页广告公告,
        首页广告爆料区,
        首页资讯banner广告通栏,
        首页广告首页通栏banner2,
        首页广告首页通栏banner3,
        首页广告供求信息下部广告,
        首页广告精品推荐图文,
        首页广告精品推荐文字,
        首页广告最活跃的企业排名,
        首页广告优秀企业展示,
        首页广告旅游动态文字,
        首页广告旅游动态图文,
        首页广告成功故事文字,
        首页广告成功故事图文,
        线路频道通栏banner1,
        线路频道精品推荐,
        线路频道热门景点展示,
        线路频道热闹酒店展示,
        机票频道通栏banner1,
        机票频道通栏banner2,
        景区频道通栏banner1,
        景区频道通栏banner2,
        景区频道本周最热景点图文,
        景区频道特价门票展示图文,
        景区频道旗帜广告1,
        景区频道旗帜广告2,
        景区频道最新加入,
        酒店频道通栏banner1,
        酒店频道通栏banner2,
        酒店频道本周最热酒店,
        酒店频道特价房展示,
        酒店频道旗帜广告1,
        酒店频道旗帜广告2,
        酒店频道最新加入,
        车队频道精品推荐图文,
        车队频道旗帜广告1,
        车队频道旗帜广告2,
        车队频道最新加入,
        旅游用品频道精品推荐图文,
        旅游用品频道新货上架,
        旅游用品频道旗帜广告1,
        旅游用品频道旗帜广告2,
        旅游用品频道最新加入,
        购物点频道精品推荐图文,
        购物点频道新货上架,
        购物点频道旗帜广告1,
        购物点频道旗帜广告2,
        购物点频道最新加入,
        供求信息频道通栏banner,
        供求信息频道供求信息文章页图片广告1,
        供求信息频道同业之星访谈,
        供求信息频道促销广告,
        供求信息频道推荐广告,
        供求信息频道旗帜广告,
        供求信息频道同业学堂旗帜广告1,
        供求信息频道同业学堂旗帜广告2,
        供求信息频道行业资讯旗帜广告,
        MQ主窗体广告,
        MQ聊天窗口右上广告,
        MQ聊天窗口右侧广告1,
        MQ聊天窗口右侧广告2,
        MQ群窗口右上广告,
        首页广告最新加入企业,
        景区频道景区主题广告,
        供求信息频道文章及列表右侧1,
        供求信息频道文章及列表右侧2,
        供求信息频道最具人气企业推荐,
        机票频道焦点广告翻屏,
        国内长线5张logo图片,
        国际长线5张logo图片,
        周边长线5张logo图片,
        首页金牌企业展示广告,
        首页推荐产品广告,
        首页资讯通栏广告,
        首页资讯非通栏广告,
        首页推荐企业,
        散拼中心广告普通版
    }
    #endregion

    #region MQ广告投放范围 (enum) AdvCompanyRange
    /// <summary>
    /// MQ广告投放范围
    /// </summary>
    /// Author:汪奇志 2010-07-15
    public enum AdvCompanyRange
    {
        /// <summary>
        /// 专线商(批发商)
        /// </summary>
        专线 = 1,
        /// <summary>
        /// 组团社(零售商)
        /// </summary>
        组团,
        /// <summary>
        /// 地接社
        /// </summary>
        地接
    }
    #endregion

    #region 广告位置信息业务实体 (class) AdvPositionInfo
    /// <summary>
    /// 广告位置信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-07-13
    [Serializable]
    public class AdvPositionInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AdvPositionInfo()
        {
            this.DefaultImgPath = "虚位以待";
            this.DefaultRedirectURL = "http://www.tongye114.com";
            this.DefaultTitle = "虚位以待";
        }

        /// <summary>
        /// 位置
        /// </summary>
        public AdvPosition Position { get; set; }
        /// <summary>
        /// 广告栏目
        /// </summary>
        public AdvCatalog Catalog { get; set; }
        /// <summary>
        /// 广告数量
        /// </summary>
        public int AdvCount { get; set; }
        /// <summary>
        /// 广告表现形式
        /// </summary>
        public AdvDisplayType DisplayType { get; set; }
        /// <summary>
        /// 广告默认图片
        /// </summary>
        public string DefaultImgPath { get; set; }
        /// <summary>
        /// 广告默认标题
        /// </summary>
        public string DefaultTitle { get; set; }
        /// <summary>
        /// 广告默认链接
        /// </summary>
        public string DefaultRedirectURL { get; set; }
        /// <summary>
        /// 广告投放类型
        /// </summary>
        public EyouSoft.Model.AdvStructure.AdvType AdvType
        {
            get
            {
                if (this.DisplayType == AdvDisplayType.MQ广告)
                {
                    return AdvType.MQ;
                }
                else
                {
                    return AdvType.城市;
                }
            }
        }
    }
    #endregion

    #region 广告基本信息业务实体 (class) AdvBasicInfo
    /// <summary>
    /// 广告基本信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-07-13
    /// 华磊 2011-05-13 添加缩略图字段
    [Serializable]
    public class AdvBasicInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AdvBasicInfo() { }

        /// <summary>
        /// 广告编号
        /// </summary>
        public int AdvId { get; set; }        
        /// <summary>
        /// 广告标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 跳转到的URL
        /// </summary>
        public string RedirectURL { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string AdvThumb { get; set; }
    }
    #endregion

    #region 广告信息业务实体 (class) AdvInfo
    /// <summary>
    /// 广告信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-07-13
    [Serializable]
    public class AdvInfo:AdvBasicInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AdvInfo()
        {
            this.Range = AdvRange.城市;
        }

        /// <summary>
        /// 广告位置
        /// </summary>
        public AdvPosition Position { get; set; }
        /// <summary>
        /// 广告类别
        /// </summary>
        public AdvCategory Category { get; set; }
        /// <summary>
        /// 广告内容介绍
        /// </summary>
        public string Remark { get; set; }        
        /// <summary>
        /// 购买单位编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 购买单位名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactInfo { get; set; }
        /// <summary>
        /// 联系MQ
        /// </summary>
        public string ContactMQ { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 投放范围集合 投放范围为全国时无需设置,全省时关联省份编号,城市时关联城市编号,单位类型时关联单位类型
        /// </summary>
        public IList<int> Relation { get; set; }
        /// <summary>
        /// 广告投放类型
        /// </summary>
        public AdvType AdvType { get; set; }
        /// <summary>
        /// 广告投放范围
        /// </summary>
        public AdvRange Range { get; set; }        
        /// <summary>
        /// 排序编号
        /// </summary>
        public int SortId { get; set; }
    }
    #endregion
}
