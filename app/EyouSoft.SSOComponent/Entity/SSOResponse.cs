using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.SSOComponent.Entity
{
    /// <summary>
    /// SSO返回信息
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    [Serializable]   
    public class SSOResponse
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get;
            set;
        }
        /// <summary>
        /// SSO脚本
        /// </summary>
        public string SSOScript
        {
            get;
            set;
        }
    }
}
