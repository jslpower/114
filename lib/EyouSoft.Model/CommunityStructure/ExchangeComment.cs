using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：互动平台信息—互动交流评论表
    /// </summary>
    public class ExchangeComment
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeComment() { }

        #region Model

        private string _id;
        private string _topicid;
        private TopicType _topicclassid;
        private string _commentid;
        private string _companyid;
        private string _companyname;
        private string _operatorid;
        private string _operatorname;
        private string _operatormq;
        private string _commenttext;
        private DateTime _issuetime;
        private bool _ishasnextlevel;
        private bool _isdeleted;
        private bool _isanonymous;
        private string _commentip;
        private bool _ischeck;

        /// <summary>
        /// 评论编号
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 评论主题编号
        /// </summary>
        public string TopicId
        {
            set { _topicid = value; }
            get { return _topicid; }
        }
        /// <summary>
        /// 评论类型 1：供求 2：嘉宾
        /// </summary>
        public TopicType TopicType
        {
            set { _topicclassid = value; }
            get { return _topicclassid; }
        }
        /// <summary>
        /// 所回复的评论编号(对应对某个互动交流话题的评论)  默认为0  单索引
        /// </summary>
        public string CommentId
        {
            set { _commentid = value; }
            get { return _commentid; }
        }
        /// <summary>
        /// 发表评论的公司编号
        /// </summary>
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 发表评论的公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 发表评论的用户编号
        /// </summary>
        public string OperatorId
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 发表评论的用户名称
        /// </summary>
        public string OperatorName
        {
            set { _operatorname = value; }
            get { return _operatorname; }
        }
        /// <summary>
        /// 发表评论的用户MQID
        /// </summary>
        public string OperatorMQ
        {
            set { _operatormq = value; }
            get { return _operatormq; }
        }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentText
        {
            set { _commenttext = value; }
            get { return _commenttext; }
        }
        /// <summary>
        /// 评论时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }
        /// <summary>
        /// 是否存在下级回复(1:存在  0:不存在  默认为0)
        /// </summary>
        public bool IsHasNextLevel
        {
            set { _ishasnextlevel = value; }
            get { return _ishasnextlevel; }
        }
        /// <summary>
        /// 是否删除(1:是  0:否  默认为0)
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsAnonymous
        {
            set { _isanonymous = value; }
            get { return _isanonymous; }
        }
        /// <summary>
        /// 回复IP
        /// </summary>
        public string CommentIP
        {
            set { _commentip = value; }
            get { return _commentip; }
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsCheck
        {
            set { _ischeck = value; }
            get { return _ischeck; }
        }

        #endregion Model
    }

    /// <summary>
    /// 评论主题类型
    /// </summary>
    public enum TopicType
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 供求
        /// </summary>
        供求,
        /// <summary>
        /// 嘉宾
        /// </summary>
        嘉宾
    }
}
