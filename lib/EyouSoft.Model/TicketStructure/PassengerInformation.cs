using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 乘客信息实体类
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class PassengerInformation
    {
        public PassengerInformation() { }
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 团队票编号
        /// </summary>
        public int GroupTicketsID { get; set; } 
        /// <summary>
        /// 姓名
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// 乘客类型
        /// </summary>
        public PassengerType? PassengerType { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public DocumentType? DocumentType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string DocumentNo { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

    }

    #region 乘客枚举类型
    /// <summary>
    /// 乘客枚举类型
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public enum PassengerType
    {
        成人 = 1,
        儿童 = 2
    }
    #endregion
     
    #region 证件枚举类型
    /// <summary>
    /// 证件枚举类型
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public enum DocumentType
    {
        身份证 = 1,
        学生证 = 2,
        军人证 = 3,
        残疾证 = 4
    }
    #endregion
}
