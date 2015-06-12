using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：公告信息
    /// </summary>
    public class Affiche
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Affiche() { }

        #region 属性
        /// <summary>
        /// 公告ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }
        /// <summary>
        /// 公告类别 0公告,1机票,2新闻,3:.net模板(ThirdTheme)首页公告
        /// </summary>
        public AfficheType AfficheClass
        {
            get;
            set;
        }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string AfficheTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string AfficheInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 阅读数
        /// </summary>
        public int Clicks
        {
            get;
            set;
        }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot
        {
            get;
            set;
        }
        /// <summary>
        /// 是否图片新闻
        /// </summary>
        public bool IsPicNews
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
        /// 发布公告时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }

    /// <summary>
    /// 创建人：鲁功源  2010-06-29
    /// 描述：新闻中心-新闻类别
    /// </summary>
    public enum AfficheType
    {
        /// <summary>
        /// 运价参考
        /// </summary>
        运价参考,
        /// <summary>
        /// 合作供应商
        /// </summary>
        合作供应商,
        /// <summary>
        /// 帮助信息
        /// </summary>
        帮助信息,
        /// <summary>
        /// 旅行社后台广告
        /// </summary>
        旅行社后台广告,
        /// <summary>
        /// 最新活动
        /// </summary>
        最新活动
    }
    //public class AfficheType
    //{
    //    #region 构造函数
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    public AfficheType() { }
    //    #endregion

    //    #region AfficheType属性
    //    /// <summary>
    //    /// 编号
    //    /// </summary>
    //    public int ID
    //    {
    //        get;
    //        set;
    //    }
    //    /// <summary>
    //    /// 类别名称
    //    /// </summary>
    //    public string ClassName
    //    {
    //        get;
    //        set;
    //    }
    //    /// <summary>
    //    /// 操作员ID
    //    /// </summary>
    //    public int OperatorID
    //    {
    //        get;
    //        set;
    //    }
    //    /// <summary>
    //    /// 添加时间
    //    /// </summary>
    //    public DateTime IssueTime
    //    {
    //        get;
    //        set;
    //    }
    //    #endregion
    //}
}
