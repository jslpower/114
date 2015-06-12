/************************************************************
 * 模块名称：同业资讯相关实体
 * 功能说明：
 * 创建人：周文超  2011-12-19 14:27:38
 * *********************************************************/

using System;
using System.Collections.Generic;

namespace EyouSoft.Model.NewsStructure
{
    #region 同业资讯类型 enum PeerNewType

    /// <summary>
    /// 同业资讯类型
    /// </summary>
    public enum PeerNewType
    {
        /// <summary>
        /// 活动资讯
        /// </summary>
        活动资讯 = 0,
        /// <summary>
        /// 特价促销
        /// </summary>
        特价促销 = 1,
        /// <summary>
        /// 同业资料
        /// </summary>
        同业资料 = 2,
        /// <summary>
        /// 旅行动态
        /// </summary>
        旅行动态 = 3,
    }

    #endregion

    #region 同业资讯推荐状态 enum NewsB2BDisplay

    /// <summary>
    /// 同业资讯推荐状态
    /// </summary>
    public enum NewsB2BDisplay
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

    #endregion

    #region 同业资讯附件类型枚举 enum AttachInfoType

    /// <summary>
    /// 同业资讯附件类型枚举
    /// </summary>
    public enum AttachInfoType
    {
        /// <summary>
        /// 图片
        /// </summary>
        图片 = 0,

        /// <summary>
        /// 文件
        /// </summary>
        文件 = 1
    }

    #endregion

    #region 同业资讯实体 class MPeerNews

    /// <summary>
    /// 同业资讯实体
    /// </summary>
    [Serializable]
    public class MPeerNews
    {
        #region Model

        private DateTime _issuetime = DateTime.Now;
        private DateTime _lastUpdateTime = DateTime.Now;

        /// <summary>
        /// 资讯编号
        /// </summary>
        public string NewId { get; set; }

        /// <summary>
        /// 资讯标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 资讯类别   活动资讯、特价促销、同业资料、旅行动态
        /// </summary>
        public PeerNewType TypeId { get; set; }

        /// <summary>
        /// 资讯内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 发布公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 发布用户编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 发布用户名称
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 推荐状态
        /// </summary>
        public NewsB2BDisplay B2BDisplay { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int ClickNum { get; set; }
        /// <summary>
        /// 发布IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime
        {
            set { _lastUpdateTime = value; }
            get { return _lastUpdateTime; }
        }

        /// <summary>
        /// 线路区域编号（专线/地接类型公司发布同业资讯时赋值）
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 线路区域名称（专线/地接类型公司发布同业资讯时赋值）
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 线路区域类别（专线/地接类型公司发布同业资讯时赋值）
        /// </summary>
        public SystemStructure.AreaType AreaType { get; set; }

        /// <summary>
        /// 景区编号（景区类型公司发布同业资讯时赋值）
        /// </summary>
        public string ScenicId { get; set; }

        #endregion Model

        #region 扩展属性

        /// <summary>
        /// 发布人MQ（列表显示用，新增、修改不需赋值）
        /// </summary>
        public string OperatorMQ { get; set; }

        /// <summary>
        /// 资讯附件信息
        /// </summary>
        public IList<MPeerNewsAttachInfo> AttachInfo { get; set; }

        #endregion
    }

    #endregion

    #region 同业资讯附件实体 class MPeerNewsAttachInfo

    /// <summary>
    /// 同业资讯附件实体
    /// </summary>
    [Serializable]
    public class MPeerNewsAttachInfo
    {
        #region Model

        /// <summary>
        /// 附件编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 附件类型     图片、文件
        /// </summary>
        public AttachInfoType Type { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 附件说明
        /// </summary>
        public string Remark { get; set; }

        #endregion Model
    }

    #endregion


    #region 同业资讯查询实体 class MQueryPeerNews

    /// <summary>
    /// 同业资讯查询实体
    /// </summary>
    public class MQueryPeerNews
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 同业资讯标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 关键字（查询 标题、发布单位）
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 资讯类别
        /// </summary>
        public PeerNewType? TypeId { get; set; }

        /// <summary>
        /// 线路区域
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 线路区域类型
        /// </summary>
        public SystemStructure.AreaType AreaType { get; set; }

        /// <summary>
        /// 是否显示b2b隐藏的同业资讯
        /// </summary>
        public bool IsShowHideNew { get; set; }

        /// <summary>
        /// 排序编号  0/1 最后更新时间降/升序 2/3发布时间降/升序  4/5b2b推荐状态降序、b2b排序升序、发布时间降序（IsShowHideNew必须传值false）
        /// </summary>
        public int OrderIndex { get; set; }
    }

    #endregion
}
