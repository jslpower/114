using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 团队地接社信息业务实体
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourLocalityInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourLocalityInfo(){ }

        #region Model
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 地接社编号
        /// </summary>
        public string LocalComoanyID { get; set; }
        /// <summary>
        /// 地接社名称
        /// </summary>
        public string LocalCompanyName { get; set; }
        /// <summary>
        /// 许可证号
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        #endregion Model
    }
}
