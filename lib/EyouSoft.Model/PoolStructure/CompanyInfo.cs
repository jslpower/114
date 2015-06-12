//Author:汪奇志 2010-11-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.PoolStructure
{
    #region 易诺用户池公司信息基类
    /// <summary>
    /// 易诺用户池公司信息基类
    /// </summary>
    public class CompanyBaseInfo
    {
        public CompanyBaseInfo() { }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
    #endregion

    #region 易诺用户池公司信息业务实体
    /// <summary>
    /// 易诺用户池公司信息业务实体
    /// </summary>
    public class CompanyInfo:CompanyBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CompanyInfo() { }

        /// <summary>
        /// 企业星级
        /// </summary>
        public string Star { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 公司备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 县市编号
        /// </summary>
        public int CountyId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<ContacterInfo> Contacters { get; set; }
        /// <summary>
        /// 适用产品信息集合
        /// </summary>
        public IList<SuitProductInfo> SuitProductConfig { get; set; }
        /// <summary>
        /// 类型信息集合
        /// </summary>
        public IList<CustomerTypeInfo> CustomerTypeConfig { get; set; }
    }
    #endregion

    #region 易诺用户池公司联系人信息业务实体
    /// <summary>
    /// 易诺用户池公司联系人信息业务实体
    /// </summary>
    public class ContacterInfo : CompanyBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ContacterInfo() { }

        /// <summary>
        /// 联系人编号
        /// </summary>
        public int ContacterId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Fullname { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 纪念日
        /// </summary>
        public DateTime? RememberDay { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 性格
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Interest { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
    #endregion

    #region 易诺用户池适用产品信息业务实体
    /// <summary>
    /// 易诺用户池适用产品信息业务实体
    /// </summary>
    public class SuitProductInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SuitProductInfo() { }

        /// <summary>
        /// 产品编号
        /// </summary>
        public int ProuctId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
    }
    #endregion

    #region 易诺用户池客户类型信息业务实体
    /// <summary>
    /// 易诺用户池客户类型信息业务实体
    /// </summary>
    public class CustomerTypeInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CustomerTypeInfo() {  }

        /// <summary>
        /// 类型编号
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
    #endregion

    #region 易诺用户池公司查询提交信息业务实体
    /// <summary>
    /// 易诺用户池公司查询提交信息业务实体
    /// </summary>
    public class CompanySearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CompanySearchInfo() { }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县市编号
        /// </summary>
        public int? CountyId { get; set; }
        /// <summary>
        /// 客户类型编号
        /// </summary>
        public int? CustomerTypeId { get; set; }
        /// <summary>
        /// 适用产品编号
        /// </summary>
        public int? SuitProductId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContacterFullname { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string ContacterMobile { get; set; }
        /// <summary>
        /// 用户分管的城市集合
        /// </summary>
        public int[] UserCitys{get;set;}
        /// <summary>
        /// 用户分管的客户类型
        /// </summary>
        public IList<int> UserCustomerTypes  {get;set;}
    }
    #endregion
}
