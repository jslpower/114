using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-14
    /// 描述：行业资讯文章实体
    /// </summary>
   [Serializable]
    public class InfoArticle : InfoArticleBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InfoArticle() { }

        #region 属性
        /// <summary>
        /// 推荐图片
        /// </summary>
        public string ImgPath
        {
            get;
            set;
        }
       /// <summary>
       /// 标题颜色
       /// </summary>
        public string TitleColor
        {
            get;
            set;
        }
        /// <summary>
        /// 焦点图片
        /// </summary>
        public string ImgThumb
        {
            get;
            set;
        }
        /// <summary>
        /// 正文
        /// </summary>
        public string ArticleText
        {
            get;
            set;
        }
        /// <summary>
        /// 关键词标签(,分隔)
        /// </summary>
        public string ArticleTag
        {
            get;
            set;
        }
        /// <summary>
        /// 责任编辑
        /// </summary>
        public string Editor
        {
            get;
            set;
        }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            get;
            set;
        }
        /// <summary>
        /// 是否图片资讯
        /// </summary>
        public bool IsImage
        {
            get;
            set;
        }
        /// <summary>
        /// 是否头条资讯
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
        /// <summary>
        /// 是否首页显示
        /// </summary>
        public bool IsFrontPage
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int Click
        {
            get;
            set;
        }
       /// <summary>
       /// 上一篇
       /// </summary>
        public InfoArticleBase PrevInfo
        {
            get;
            set;
        }
       /// <summary>
       /// 下一篇
       /// </summary>
        public InfoArticleBase NextInfo
        {
            get;
            set;
        }
        #endregion
    }

    #region 行业资讯基类
   /// <summary>
   /// 行业资讯基类
    /// </summary>
   public class InfoArticleBase
   {
       /// <summary>
       /// 构造函数
       /// </summary>
       public InfoArticleBase() { }
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="id">主键编号</param>
       /// <param name="articletitle">标题</param>
       /// <param name="TopicClassId">资讯大类别</param>
       public InfoArticleBase(string id, string articletitle, TopicClass TopicClassId)
       {
           this.ID = id;
           this.ArticleTitle = articletitle;
           this.TopicClassId = TopicClassId;
       }
       /// <summary>
       /// 编号
       /// </summary>
       public string ID
       {
           get;
           set;
       }
       /// <summary>
       /// 标题
       /// </summary>
       public string ArticleTitle
       {
           get;
           set;
       }
       /// <summary>
       /// 大类
       /// </summary>
       public TopicClass TopicClassId
       {
           get;
           set;
       }
       /// <summary>
       /// 子类
       /// </summary>
       public TopicAreas AreaId
       {
           get;
           set;
       }
   }
   #endregion

    #region 行业资讯大类别枚举
    /// <summary>
   /// 行业资讯大类别
    /// </summary>
   public enum TopicClass
   { 
        /// <summary>
        /// 行业资讯
        /// </summary>
        行业资讯=1,
        /// <summary>
        /// 计调指南
        /// </summary>
        计调指南,
       /// <summary>
        /// 导游之家
       /// </summary>
        导游之家,
       /// <summary>
        /// 案例分析
       /// </summary>
        案例分析,
       /// <summary>
        /// 经验交流
       /// </summary>
        经验交流
   }
    #endregion

    #region 行业资讯小类别枚举
    /// <summary>
   /// 行业资讯小类别
    /// </summary>
   public enum TopicAreas
   {
       /// <summary>
       /// 未知
       /// </summary>
       未知=0,
       /// <summary>
       /// 新闻资讯
       /// </summary>
       新闻资讯=1,
       /// <summary>
       /// 行业动态
       /// </summary>
       行业动态,
       /// <summary>
       /// 景区资讯
       /// </summary>
       景区新闻,
       /// <summary>
       /// 旅行社资讯
       /// </summary>
       旅行社,
       /// <summary>
       /// 酒店资讯
       /// </summary>
       酒店新闻,
       /// <summary>
       /// 成功故事
       /// </summary>
       成功故事,
       /// <summary>
       /// 政策解读
       /// </summary>
       政策解读,
       /// <summary>
       /// 业务进阶
       /// </summary>
       业务进阶,
       /// <summary>
       /// 同业之星
       /// </summary>
       同业之星,
       /// <summary>
       /// 同业集结号
       /// </summary>
       同业集结号,
       /// <summary>
       /// 导游导购词
       /// </summary>
       导游导购词,
       /// <summary>
       /// 导游考试相关
       /// </summary>
       导游考试相关,
       /// <summary>
       /// 带团经验分享
       /// </summary>
       带团经验分享,
       /// <summary>
       /// 景区案例分析
       /// </summary>
       景区案例分析,
       /// <summary>
       /// 旅行社案例分析
       /// </summary>
       旅行社案例分析,
       /// <summary>
       /// 酒店案例分析
       /// </summary>
       酒店案例分析,
       /// <summary>
       /// 新手计调
       /// </summary>
       新手计调

   }
    #endregion
}
