using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.NewsStructure
{
    /// <summary>
    /// 资讯焦点图片业务实体
    /// </summary>
    /// 鲁功源 2011-03-31
    public class NewsPicInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsPicInfo() { }
        #endregion

        #region 属性
        /// <summary>
        /// 首页焦点图片
        /// </summary>
        public IList<BasicNewsPic> FocusPic
        {
            get;
            set;
        }
        /// <summary>
        /// 首页右侧图片
        /// </summary>
        public BasicNewsPic FocusRightPic
        {
            get;
            set;
        }
        /// <summary>
        /// 旅游资讯焦点
        /// </summary>
        public BasicNewsPic TravelFocusPic
        {
            get;
            set;
        }
        /// <summary>
        /// 同业学堂焦点
        /// </summary>
        public BasicNewsPic SchoolFocusPic
        {
            get;
            set;
        }
        /// <summary>
        /// 同业学堂右侧
        /// </summary>
        public BasicNewsPic SchoolRightPic
        {
            get;
            set;
        }
        /// <summary>
        /// 同行社区
        /// </summary>
        public BasicNewsPic CommunityPic
        {
            get;
            set;
        }
        /// <summary>
        /// 同行社区中间
        /// </summary>
        public IList<BasicNewsPic> CommunityMiddlePic
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
        /// 操作时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }

    #region 资讯公用图片实体
    /// <summary>
    /// 资讯公用图片实体
    /// </summary>
    public class BasicNewsPic
    { 
         #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BasicNewsPic() { }
        #endregion

         #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 类别
        /// </summary>
        public PicCategory Category
        {
            get;
            set;
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath
        {
            get;
            set;
        }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string PicTitle
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 图片位置枚举
    /// <summary>
    /// 图片位置枚举
    /// </summary>
    public enum PicCategory
    {
        /// <summary>
        /// 首页焦点图片
        /// </summary>
        首页焦点图片=1,
        /// <summary>
        /// 首页焦点新闻右侧公告
        /// </summary>
        首页焦点新闻右侧公告=2,
        /// <summary>
        /// 旅游资讯焦点
        /// </summary>
        旅游资讯焦点=3,
        /// <summary>
        /// 同业学堂焦点
        /// </summary>
        同业学堂焦点=4,
        /// <summary>
        /// 同业学堂右侧
        /// </summary>
        同业学堂右侧=5,
        /// <summary>
        /// 同行社区
        /// </summary>
        同行社区左侧=6,
        /// <summary>
        /// 同行社区中间
        /// </summary>
        同行社区中间=7
    }
    #endregion

}
