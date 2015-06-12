using System;

namespace EyouSoft.Model.TicketStructure
{
    #region 机票接口访问日志信息业务实体
    /// <summary>
    /// 机票接口访问日志信息业务实体
    /// </summary>
    /// Author：汪奇志 2011-05-10
    public class MLogTicketInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLogTicketInfo() { }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 出发城市
        /// </summary>
        public string LCity { get; set; }
        /// <summary>
        /// 目的城市
        /// </summary>
        public string RCity { get; set; }
        /// <summary>
        /// 出发日期
        /// </summary>
        public DateTime? LDate { get; set; }
        /// <summary>
        /// 回程日期
        /// </summary>
        public DateTime? RDate { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CDate { get; set; }

        
    }
    #endregion

    #region 机票接口访问日志列表信息业务实体
    /// <summary>
    /// 机票接口访问日志列表信息业务实体
    /// </summary>
    /// Author:汪奇志 2011-05-10
    public class MLBLogInfo:MLogTicketInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBLogInfo() { }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTelephone { get; set; }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string ContactQQ { get; set; }
        /// <summary>
        /// 总登录次数
        /// </summary>
        public int LoginNumber { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 总访问次数
        /// </summary>
        public int TotalTimes { get; set; }
        /// <summary>
        /// 一周内访问次数
        /// </summary>
        public int WeekTimes { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LatestDate { get; set; }
        /// <summary>
        /// 一周内登录次数
        /// </summary>
        public int WeekLoginNumber { get; set; }
    }
    #endregion

    #region 机票接口访问日志查询信息业务实体
    /// <summary>
    /// 机票接口访问日志查询信息业务实体
    /// </summary>
    public class MLogTicketSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLogTicketSearchInfo() { }
    }
    #endregion
}
