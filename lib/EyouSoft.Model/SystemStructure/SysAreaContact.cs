using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：区域联系人信息
    /// </summary>
    public class SysAreaContact
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysAreaContact() { }

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
        /// 负责区域
        /// </summary>
        public string SaleArea
        {
            get;
            set;
        }
        /// <summary>
        /// 类别 0： 销售 1： 客服
        /// </summary>
        public int SaleType
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string ContactTel
        {
            get;
            set;
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string ContactMobile
        {
            get;
            set;
        }
        /// <summary>
        ///  QQ
        /// </summary>
        public string QQ
        {
            get;
            set;
        }
        /// <summary>
        ///  MQ
        /// </summary>
        public string MQ
        {
            get;
            set;
        }
        #endregion
    }
}
