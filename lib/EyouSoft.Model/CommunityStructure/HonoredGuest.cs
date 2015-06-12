using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-14
    /// 描述：嘉宾访谈实体类
    /// </summary>
    [Serializable]
    public class HonoredGuest
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HonoredGuest() { }

        #region 属性
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
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImgPath
        {
            get;
            set;
        }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImgThumb
        {
            get;
            set;
        }
        /// <summary>
        /// 访谈内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// 同业观点1
        /// </summary>
        public string Opinion1
        {
            get;
            set;
        }
        /// <summary>
        /// 同业观点2
        /// </summary>
        public string Opinion2
        {
            get;
            set;
        }
        /// <summary>
        /// 同业观点3
        /// </summary>
        public string Opinion3
        {
            get;
            set;
        }
        /// <summary>
        /// 小编总结
        /// </summary>
        public string Summary
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
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
        /// 回复总数
        /// </summary>
        public int CommentCount
        {
            set;
            get;
        }

        #endregion
    }
}
