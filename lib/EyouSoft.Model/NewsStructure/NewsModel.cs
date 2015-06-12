using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.NewsStructure
{
    #region 新闻实体基类
    /// <summary>
    /// 新闻实体基类
    /// </summary>
    /// 鲁功源  2011-04-02
    [Serializable]
    public class BasicNews
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BasicNews(){}

        #region 属性
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 咨询类别
        /// </summary>
        public NewsCategory AfficheCate { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string AfficheTitle { get; set; }
        /// <summary>
        /// 新闻类别编号
        /// </summary>
        public int AfficheClass { get; set; }
        /// <summary>
        /// 新闻类别名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 新闻标题颜色代码
        /// </summary>
        public string TitleColor { get; set; }
        /// <summary>
        /// 新闻描述
        /// </summary>
        public string AfficheDesc { get; set; }
        /// <summary>
        /// 新闻来源
        /// </summary>
        public string AfficheSource { get; set; }
        /// <summary>
        /// 新闻图片路径
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        public string AfficheAuthor { get; set; }
        /// <summary>
        /// 当推荐位置为URL跳转，跳转到的URL
        /// </summary>
        public string GotoUrl { get; set; }
        /// <summary>
        /// 新闻所属省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 新闻所属城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 新闻所属省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 新闻所属城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 新闻推荐位置类别
        /// </summary>
        public IList<RecPosition> RecPositionId { get; set; }
        /// <summary>
        /// 新闻标签列表
        /// </summary>
        public IList<NewsSubItem> NewsTagItem { get; set; }
        /// <summary>
        /// 新闻关键字列表
        /// </summary>
        public IList<NewsSubItem> NewsKeyWordItem { get; set; }
        /// <summary>
        /// 添加新闻时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 新闻的总页数
        /// </summary>      
        public int AffichePageNum { get; set; }
        /// <summary>
        /// 点击率
        /// </summary>
        public int Clicks { get; set; }
        /// <summary>
        /// 点击率
        /// </summary>
        public IList<OtherItems> OtherItem { get; set; }
        #endregion
    }
    #endregion

    #region 新闻实体类[运营后台]
    /// <summary>
    /// 描述：新闻实体类[运营后台]
    /// 修改记录：
    /// 1. 2011-03-31 PM 曹胡生 创建
    /// </summary>
    [Serializable]
    public class NewsModel:BasicNews
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsModel() { }

        #region 属性
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string AfficheContent { get; set; }
        /// <summary>
        /// 新闻的排序方式
        /// </summary>
        public AfficheSource AfficheSort { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int OperatorID { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 新闻内容实体
        /// </summary>
        public IList<NewsContent> NewsContent { get; set; }
        /// <summary>
        /// 修改新闻时间[即置顶时间]
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 新闻修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        #endregion

    }

    #endregion 

    #region 新闻实体类[网站前台]
    /// <summary>
    /// 新闻实体类[网站前台]
    /// </summary>
    /// 鲁功源 2011-04-02
    [Serializable]
    public class WebSiteNews:BasicNews
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WebSiteNews(){ }

        #region 属性
        /// <summary>
        /// 当前信息的上一条新闻
        /// </summary>
        public BasicNews PrevNewsInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 当前信息的下一条新闻
        /// </summary>
        public BasicNews NextNewsInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 内容信息
        /// </summary>
        public NewsContent ContentInfo
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 新闻内容实体
    /// <summary>
    /// 新闻内容实体
    /// </summary>
    [Serializable]
    public class NewsContent
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int NewId { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }
    }
    #endregion

    #region 新闻关键字，Tag关联实体
    /// <summary>
    /// 新闻关键字，Tag关联实体
    /// </summary>
    [Serializable]
    public class NewsSubItem
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int NewId { get; set; }
        /// <summary>
        /// 关键字或Tag编号
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// 关键字或Tag名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 关键字或Tag
        /// </summary>
        public ItemCategory ItemType { get; set; }
        /// <summary>
        /// 项目链接
        /// </summary>
        public string ItemUrl { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 推荐位置枚举
    /// <summary>
    /// 推荐位置枚举
    /// </summary>
    public enum RecPosition
    {
        /// <summary>
        /// 推荐至首页
        /// </summary>
        推荐至首页 = 1,
        /// <summary>
        /// 资讯头条
        /// </summary>
        资讯头条 = 2,
        /// <summary>
        /// 普通推荐资讯
        /// </summary>
        普通推荐资讯 = 3,
        /// <summary>
        /// URL跳转
        /// </summary>
        URL跳转 = 4,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 9
    }
    #endregion

    #region 文章排序枚举
    /// <summary>
    /// 文章排序枚举
    /// </summary>
    public enum AfficheSource
    {
        /// <summary>
        /// 默认排序
        /// </summary>
        默认排序 = 1,
        /// <summary>
        /// 置顶一周
        /// </summary>
        置顶一周 = 2,
        /// <summary>
        /// 置顶一个月
        /// </summary>
        置顶一个月 = 3,
        /// <summary>
        /// 置顶三个月
        /// </summary>
        置顶三个月 = 4,
        /// <summary>
        /// 置顶半年
        /// </summary>
        置顶半年 =5,
        /// <summary>
        /// 置顶一年
        /// </summary>
        置顶一年 = 6
    }
    #endregion

    #region 附加项枚举
    /// <summary>
    /// 附加项枚举
    /// </summary>
    public enum OtherItems
    {
        /// <summary>
        /// 图片是否加水印
        /// </summary>
        图片是否加水印 = 1,
        /// <summary>
        /// 删除非站内链接
        /// </summary>
        删除非站内链接 = 2,
        /// <summary>
        /// 下载远程图片和资源
        /// </summary>
        下载远程图片和资源 = 3
    }
    #endregion

    #region 新闻资讯查询实体
    /// <summary>
    /// 新闻资讯查询实体
    /// updateTime:2011-4-15 author:zhengfj
    /// </summary>
    [Serializable]
    public class SearchOrderInfo
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public int NewsID { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 省份ID
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// Top条数
        /// </summary>
        public int TopCount { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 新闻大类别
        /// </summary>
        public NewsCategory? Category { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int City { get; set; }
        /// <summary>
        /// 文章排序
        /// </summary>
        public AfficheSource? Source { get; set; }
        /// <summary>
        /// 是否图片
        /// </summary>
        public bool? IsPic { get; set; }
        /// <summary>
        /// tag标签
        /// </summary>
        public int Tag { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool? IsRecPositionId { get; set; }

        public IList<NewsSubItem> Keywords { get; set; }
    }
    #endregion

    #region 网站前台新闻检索类型枚举
    /// <summary>
    /// 网站前台新闻检索类型枚举
    /// </summary>
    public enum NewsQueryType
    { 
        /// <summary>
        /// 按首页推荐
        /// </summary>
        按首页推荐=1,
        /// <summary>
        /// 按资讯头条
        /// </summary>
        按资讯头条=2,
        /// <summary>
        /// 按普通推荐
        /// </summary>
        按普通推荐=3,
        /// <summary>
        /// 按点击次数
        /// </summary>
        按点击次数=4
    }
    #endregion
}
