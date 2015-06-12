using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    #region 高级服务申请审核状态 (enum) ApplyServiceState
    /// <summary>
    /// 高级服务申请审核状态
    /// </summary>
    public enum ApplyServiceState
    {
        /// <summary>
        /// 未审核
        /// </summary>
        未审核=0,
        /// <summary>
        /// 审核通过
        /// </summary>
        审核通过,
        /// <summary>
        /// 审核未通过
        /// </summary>
        未通过
    }
    #endregion

    #region 高级服务申请信息业务实体
    /// <summary>
    /// 高级服务申请信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-07-22
    public class SysApplyServiceInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SysApplyServiceInfo() { }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// 申请服务类型
        /// </summary>
        public EyouSoft.Model.CompanyStructure.SysService ApplyServiceType { get; set; }
        /// <summary>
        /// 申请人公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 申请人公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 申请人省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 申请人省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 申请人城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 申请人城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 联系MQ
        /// </summary>
        public string ContactMQ { get; set; }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string ContactQQ { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ContactAddress{get;set;}
        /// <summary>
        /// 申请信息
        /// </summary>
        public string ApplyText{get;set;}
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime{get;set;}
        /// <summary>
        /// 审核人编号
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ApplyServiceState ApplyState { get; set; }
        /// <summary>
        /// 审核信息
        /// </summary>
        public string CheckText { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 服务启用时间
        /// </summary>
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 服务到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
    #endregion

    #region 高级服务申请审核信息业务实体 (class) ServiceCheckedInfo
    /// <summary>
    /// 高级服务申请审核信息业务实体
    /// </summary>
    public class ServiceCheckedInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ServiceCheckedInfo() { }
        
        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyId { get; set; }
        /// <summary>
        /// 服务启用时间
        /// </summary>
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 服务到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public EyouSoft.Model.SystemStructure.ApplyServiceState ApplyState { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public int OperatorId { get; set; }
    }
    #endregion

    #region 高级网店申请审核信息业务实体 (class) EshopCheckInfo
    /// <summary>
    /// 高级网店申请审核信息业务实体
    /// </summary>
    public class EshopCheckInfo : ServiceCheckedInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public EshopCheckInfo() { }

        /// <summary>
        /// 审核通过的域名
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 模板路径
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// 谷歌地图APIKey
        /// </summary>
        public string GoogleMapKey { get; set; }
    }
    #endregion

    #region 收费MQ申请审核信息业务实体 (class) MQCheckInfo
    /// <summary>
    /// 收费MQ申请审核信息业务实体
    /// </summary>
    public class MQCheckInfo : ServiceCheckedInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MQCheckInfo() { }

        /// <summary>
        /// 子账号数量
        /// </summary>
        public int SubAccountNumber { get; set; }
    }
    #endregion

    #region 高级服务续费信息业务实体 (class) ServiceRenewInfo
    /// <summary>
    /// 高级服务续费信息业务实体
    /// </summary>
    public class ServiceRenewInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ServiceRenewInfo() { }

        /// <summary>
        /// 续费编号
        /// </summary>
        public string RenewId { get; set; }
        /// <summary>
        /// 续费公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 高级服务申请编号
        /// </summary>
        public string ApplyServiceId { get; set; }
        /// <summary>
        /// 服务起始时间
        /// </summary>
        public DateTime EnableTime { get; set; }
        /// <summary>
        /// 服务到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 续费时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 续费人
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 子账户数量(仅收费MQ服务时有效)
        /// </summary>
        public int SubAccountNumber { get; set; }
    }
    #endregion
}
