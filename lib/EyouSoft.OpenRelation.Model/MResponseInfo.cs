using System;

namespace EyouSoft.OpenRelation.Model
{
    #region http response write info
    /// <summary>
    /// http response write info
    /// </summary>
    public class MResponseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MResponseInfo() {  }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }        
        /// <summary>
        /// 指令类型（请求时的指令类型）
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// 相应JSON格式数据，类型根据指令类型来确定
        /// </summary>
        public string InstructionCode { get; set; }
    }
    #endregion

    #region 创建公司或用户返回信息业务实体
    /// <summary>
    /// 创建公司或用户返回信息业务实体
    /// </summary>
    public class MRCreateUserInfo
    {
        /// <summary>
        /// 平台公司编号，创建公司、用户时返回
        /// </summary>
        public string PlatformCompanyId { get; set; }
        /// <summary>
        /// 平台用户编号，创建公司、用户时返回
        /// </summary>
        public string PlatformUserId { get; set; }
    }
    #endregion
}
