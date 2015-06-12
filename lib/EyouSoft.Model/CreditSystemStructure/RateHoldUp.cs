using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-公司推荐业务实体
    /// </summary>
    public class RateHoldUp
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public RateHoldUp() { }

        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="id">推荐编号</param>
        /// <param name="fromCompanyId">推荐人所在公司编号</param>
        /// <param name="fromCompanyName">推荐人所在公司名称</param>
        /// <param name="fromUserId">推荐人编号</param>
        /// <param name="fromUserContactName">推荐人名称</param>
        /// <param name="toCompanyId">推荐的公司编号</param>
        /// <param name="holdUpScore">分值</param>
        /// <param name="issueTime">推荐时间</param>
        public RateHoldUp(string id, string fromCompanyId, string fromCompanyName, string fromUserId, string fromUserContactName, string toCompanyId, double holdUpScore, DateTime issueTime) 
        {
            this.Id = id;
            this.FromCompanyId = fromCompanyId;
            this.FromCompanyName = fromCompanyName;
            this.FromUserId = fromUserId;
            this.FromUserContactName = fromUserContactName;
            this.ToCompanyId = toCompanyId;
            this.HoldUpScore = holdUpScore;
            this.IssueTime = issueTime;
        }

        /// <summary>
        /// 推荐编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 推荐人所在公司编号
        /// </summary>
        public string FromCompanyId { get; set; }
        /// <summary>
        /// 推荐人所在公司名称
        /// </summary>
        public string FromCompanyName { get; set; }
        /// <summary>
        /// 推荐人编号
        /// </summary>
        public string FromUserId { get; set; }
        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string FromUserContactName { get; set; }
        /// <summary>
        /// 推荐的公司编号
        /// </summary>
        public string ToCompanyId { get; set; }
        /// <summary>
        /// 推荐所获分值
        /// </summary>
        public double HoldUpScore { get; set; }
        /// <summary>
        /// 推荐时间
        /// </summary>
        public DateTime IssueTime { get; set; }        
    }
}
