using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{   
    /// <summary>
    /// 订单记录
    /// </summary>
    public class TicketOrderLog
    {
        string state;
        /// <summary>
        /// 状态
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        DateTime time;
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        string userName;
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
