using System;

namespace EyouSoft.OpenRelation.Model
{
    #region 基本信息业务实体
    /// <summary>
    /// 基本信息业务实体
    /// </summary>
    public class MBaseInfo
    {
        private PlatformCompanyType _PlatformCompanyType = PlatformCompanyType.ZT;
        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemType SystemType { get; set; }
        /// <summary>
        /// 平台公司编号
        /// </summary>
        public string PlatformCompanyId { get; set; }
        /// <summary>
        /// 系统公司类型，用来确定各个系统专线、组团、供应商公司是否存在于一个表内，若存在于一个表内用ZX
        /// </summary>
        public SystemCompanyType SystemCompanyType { get; set; }
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public int SystemCompanyId { get; set; }
        /// <summary>
        /// 平台公司类型，默认为ZT，由此来决定在大平台创建的公司类型
        /// </summary>
        public PlatformCompanyType PlatformCompanyType
        {
            get { return this._PlatformCompanyType; }
            set { this._PlatformCompanyType = value; }
        }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
    }
    #endregion

    #region 公司信息业务实体
    /// <summary>
    /// 公司信息业务实体
    /// </summary>
    /// Author:汪奇志 DateTime:2011-04-01
    public class MCompanyInfo : MBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCompanyInfo() { }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTelephone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 联系传真
        /// </summary>
        public string ContactFax { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// 联系人MSN
        /// </summary>
        public string ContactMSN { get; set; }
        /// <summary>
        /// 联系人QQ
        /// </summary>
        public string ContactQQ { get; set; }
        /// <summary>
        /// 联系人性别
        /// </summary>
        public Gender ContactGender { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }        
    }
    #endregion

    #region 公司及用户信息业务实体
    /// <summary>
    /// 公司及用户信息业务实体
    /// </summary>
    /// Author:汪奇志 DateTime:2011-04-02
    public class MCUInfo
    {
        /// <summary>
        /// 公司信息
        /// </summary>
        public MCompanyInfo CompanyInfo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public MUserInfo UserInfo { get; set; }
    }
    #endregion
}
