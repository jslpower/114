using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-01
    /// 描述：单位采购目录实体类
    /// </summary>
    public class CompanyFavor
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyFavor() { }
        #endregion

        #region 属性
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 被收藏的公司
        /// </summary>
        public string FavorCompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId
        {
            get;
            set;
        }
        #endregion
    }
}
