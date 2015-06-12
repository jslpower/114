using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-15
    /// 描述：供求收藏夹实体类
    /// </summary>
    public class ExchangeFavor : ExchangeListBase
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public new int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 供求信息编号
        /// </summary>
        public string ExchangeId
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 供求浏览、收藏基类
    /// </summary>
    public class ExchangeBase
    {
        #region 属性
        /// <summary>
        /// 供求信息编号
        /// </summary>
        public string ExchangeId
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        #endregion
    }
}
