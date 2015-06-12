using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：华磊 2011-5-10
    /// 描述：首页登录下面的数据来源提醒
    /// </summary>
    public class RemindSource
    {
         /// <summary>
        /// 构造
        /// </summary>
        public RemindSource() { }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string EventTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime EventTime
        {
            get;
            set;
        }
    }
}
