using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.OpenStructure
{
    /// <summary>
    /// 处理异常信息业务实体
    /// </summary>
    /// Author:汪奇志 2011-04-01
    public class MExceptionInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MExceptionInfo() { }

        /// <summary>
        /// 异常编号
        /// </summary>
        public string ExecptionId { get; set; }
        /// <summary>
        /// 请求系统类型
        /// </summary>
        public int SystemType { get; set; }
        /// <summary>
        /// 请求代码 http request post info json data
        /// </summary>
        public string InstructionCode { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ExceptionCode { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ExceptionDesc { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime ExceptionTime { get; set; }

    }
}
