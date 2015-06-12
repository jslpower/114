using System;

namespace EyouSoft.OpenRelation.Model
{
    /// <summary>
    /// http request post info
    /// </summary>
    public class MRequestInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRequestInfo() { }

        /// <summary>
        /// app key
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 发起请求系统类型
        /// </summary>
        public SystemType RequestSystemType { get; set; }
        /// <summary>
        /// 指令类型
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// 指令代码，相应实体的JSON格式数据，实体类型根据指令类型来确定
        /// </summary>
        public string InstructionCode { get; set; }
        /// <summary>
        /// 处理程序资源URI
        /// </summary>
        public string RequestUriString { get; set; }
    }
}
