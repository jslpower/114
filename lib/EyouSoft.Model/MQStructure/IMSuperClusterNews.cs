using System;

namespace EyouSoft.Model.MQStructure
{
    #region 公告信息实体
    /// <summary>
    /// 公告信息实体
    /// </summary>
    /// 郑知远 2011/05/26
    [Serializable]
    public class IMSuperClusterNews
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMSuperClusterNews(){}

        /// <summary>
        /// 公告ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公告类型
        /// </summary>
        public Type Category { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 公告对象
        /// </summary>
        public string Centres { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string Operater { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime OperateTime { get; set; }
    }
    #endregion

    #region 公告类型枚举
    /// <summary>
    /// 公告类型枚举
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// 公告
        /// </summary>
        公告 = 0,

        /// <summary>
        /// 广播
        /// </summary>
        广播 = 1
    }
    #endregion
}
