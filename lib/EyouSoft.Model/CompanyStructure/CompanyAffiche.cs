using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-25
    /// 描述：公司资讯实体类
    /// </summary>
    [Serializable]
    public class CompanyAffiche
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyAffiche() { }
        #endregion

        #region 属性
        /// <summary>
        /// 资讯编号
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyType CompanyType
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName
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
        /// 资讯类型
        /// </summary>
        public CompanyAfficheType AfficheClass
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯标题
        /// </summary>
        public string AfficheTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯类容
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
        /// 图片地址
        /// </summary>
        public string PicPath
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
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
        #endregion
    }

    #region 公司资讯类别 Enum
    /// <summary>
    /// 公司资讯类别
    /// </summary>
    public enum CompanyAfficheType
    { 
        /// <summary>
        /// 景区新闻
        /// </summary>
        景区新闻 = 1,
        /// <summary>
        /// 酒店新闻
        /// </summary>
        酒店新闻,
        /// <summary>
        /// 车队新闻
        /// </summary>
        车队新闻,
        /// <summary>
        /// 购物点新闻
        /// </summary>
        购物点新闻,
        /// <summary>
        /// 旅游用品新闻
        /// </summary>
        旅游用品新闻
    }
    #endregion
}
