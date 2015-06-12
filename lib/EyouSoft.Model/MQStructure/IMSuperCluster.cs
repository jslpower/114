using System;

namespace EyouSoft.Model.MQStructure
{
    using System.Collections.Generic;

    /// <summary>
    /// 同业中心信息实体
    /// </summary>
    /// 郑知远 2011/05/27
    [Serializable]
    public class IMSuperCluster
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMSuperCluster(){}

        /// <summary>
        /// 同业中心（超级群）ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 同业中心名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 总管理员
        /// </summary>
        public int Master { get; set; }

        /// <summary>
        /// 普通管理员
        /// </summary>
        public string Waiter { get; set; }

        /// <summary>
        /// 是否已用
        /// </summary>
        public bool Used { get; set; }

        /// <summary>
        /// 成员构成导入类型
        /// </summary>
        public SelectType SelectType { get; set; }

        /// <summary>
        /// 用【，】隔开导入省份或者会员ID数据
        /// </summary>
        public string SelectValue { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 同业中心密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Opertor { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 成员构成统计
        /// </summary>
        public string CountValue { get; set; }
    }

    /// <summary>
    /// 聊天记录实体
    /// </summary>
    [Serializable]
    public class IMSuperClusterMsg
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMSuperClusterMsg() { }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DisplayDate { get; set; }

        /// <summary>
        /// 消息发起者
        /// </summary>
        public string MessageSender { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
    }

    /// <summary>
    /// 同业中心会员名片信息实体
    /// </summary>
    [Serializable]
    public class IMClusterUserCard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMClusterUserCard(){}

        /// <summary>
        /// 同业中心ID
        /// </summary>
        public int ClusterId { get; set; }

        /// <summary>
        /// 用户姓名（性别）
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 主营业务
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 同业MQ号
        /// </summary>
        public int MQ { get; set; }
        
        /// <summary>
        /// 公司LOGO地址
        /// </summary>
        public string CompanyLogo { get; set; }

        /// <summary>
        /// 公司网店地址
        /// </summary>
        public string EshopUrl { get; set; }

        /// <summary>
        /// MQ登录次数
        /// </summary>
        public int Frequency { get; set; }
    }

    /// <summary>
    /// 成员构成导入枚举
    /// </summary>
    public enum SelectType
    {
        /// <summary>
        /// 成员构成导入：选择省市
        /// </summary>
        选择省市 = 1,

        /// <summary>
        /// 成员构成导入：选择会员ID
        /// </summary>
        选择会员ID = 2
    }
}
