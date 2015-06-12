using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间：2010-05-11
    /// 描述：基础数据-推广类型实体类
    /// </summary>
    public class TourStateBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourStateBase() { }

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int TypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 推广文字
        /// </summary>
        public string PromoText
        {
            get;
            set;
        }
        /// <summary>
        /// 推广文字CSS
        /// </summary>
        public string PromoTextHTML { get; set; }
        #endregion
    }
}
