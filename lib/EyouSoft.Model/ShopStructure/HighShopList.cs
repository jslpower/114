using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：高级网店基本信息
    /// </summary>
    public class HighShopList
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopList() { }
        #endregion

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 客户所在省份编号
        /// </summary>
        public int ProvinceId
        {
            get;
            set;
        }
        /// <summary>
        /// 客户单位所在城市编号
        /// </summary>
        public int CityId
        {
            get;
            set;
        }
        /// <summary>
        /// 客户单位所在省份名称
        /// </summary>
        public string ProvinceName
        {
            get;
            set;
        }
        /// <summary>
        /// 客户单位所在城市名称
        /// </summary>
        public string CityName
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public string ApplyUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApplyContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplyContactTel
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人手机
        /// </summary>
        public string ApplyContactMobile
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人MQ
        /// </summary>
        public string ApplyContactMQ
        {
            get;
            set;
        }
        /// <summary>
        /// 申请人QQ
        /// </summary>
        public string ApplyContactQQ
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string OperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 申请的域名
        /// </summary>
        public string ApplyDomainName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsCheck
        {
            get;
            set;
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核通过时间
        /// </summary>
        public DateTime CheckTime
        {
            get;
            set;
        }
        /// <summary>
        /// 网店到期时间
        /// </summary>
        public DateTime ExpireTime
        {
            get;
            set;
        }
        #endregion

        #region 附件属性
        private HighShopCompanyInfo _shopcompanyinfo = null;
        /// <summary>
        /// 网店详细信息
        /// </summary>
        public HighShopCompanyInfo ShopCompanyInfo
        {
            get { return _shopcompanyinfo; }
            set { _shopcompanyinfo = value; }
        }
        #endregion
    }
}
