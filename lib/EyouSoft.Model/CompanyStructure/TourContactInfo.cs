using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：团队联系人
    /// </summary>
    [Serializable]
    public class TourContactInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourContactInfo() { }

        #region 属性
        /// <summary>
        /// 团队联系人编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 联系人QQ
        /// </summary>
        public string ContactQQ { get; set; }
        /// <summary>
        /// 联系人MQ编号
        /// </summary>
        public string ContactMQId { get; set; }
        /// <summary>
        /// 联系人用户名
        /// </summary>
        public string UserName { get; set; }
        #endregion
    }
}
