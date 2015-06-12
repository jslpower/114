using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 单位信息抽象类
    /// </summary>
    [Serializable]
    public class Company
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }

    /// <summary>
    /// 单位图文信息
    /// </summary>
    [Serializable]
    public class CompanyPicTxt : Company
    {
        /// <summary>
        /// 公司企业logo
        /// </summary>
        private CompanyLogo _companylogo = new CompanyLogo();

        /// <summary>
        /// 公司企业logo
        /// </summary>
        public CompanyLogo CompanyLogo { get { return this._companylogo; } set { this._companylogo = value; } }
    }

    /// <summary>
    /// 通用单位信息基类
    /// </summary>
    /// 创建人：张志瑜 2010-05-28
    public class CompanyInfo : Company
    {
        #region 私有变量
        /// <summary>
        /// 公司身份
        /// </summary>
        private CompanyRole _companyrole = new CompanyRole();
        /// <summary>
        /// 公司联系人信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.ContactPersonInfo _contactinfo = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
        #endregion

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int CountyId { get; set; }
        /// <summary>
        /// 公司身份
        /// </summary>
        public CompanyRole CompanyRole { get { return this._companyrole; } set { this._companyrole = value; } }
        /// <summary>
        /// 获取企业性质
        /// </summary>
        public BusinessProperties BusinessProperties
        {
            get
            {


                if (this._companyrole.Length == 0)
                {
                    return BusinessProperties.随便逛逛;
                }

                if (this._companyrole.RoleItems.Contains(CompanyType.专线) || this._companyrole.RoleItems.Contains(CompanyType.组团) || this._companyrole.RoleItems.Contains(CompanyType.地接))
                {
                    return BusinessProperties.旅游社;
                }

                BusinessProperties _BusinessProperties = BusinessProperties.随便逛逛;

                switch (this._companyrole[0])
                {
                    case CompanyType.车队: _BusinessProperties = BusinessProperties.车队; break;
                    case CompanyType.购物店: _BusinessProperties = BusinessProperties.购物店; break;
                    case CompanyType.机票供应商: _BusinessProperties = BusinessProperties.机票供应商; break;
                    case CompanyType.景区: _BusinessProperties = BusinessProperties.景区; break;
                    case CompanyType.酒店: _BusinessProperties = BusinessProperties.酒店; break;
                    case CompanyType.旅游用品店: _BusinessProperties = BusinessProperties.旅游用品店; break;
                    case CompanyType.其他采购商: _BusinessProperties = BusinessProperties.其他采购商; break;
                }

                return _BusinessProperties;
            }
        }
        /// <summary>
        /// 获取公司贸易类型
        /// </summary>
        public TradeType TradeType
        {
            get
            {
                if (this._companyrole.Length == 0)
                    return TradeType.随便逛逛;
                else if (this._companyrole.Length == 1 && (this._companyrole[0] == CompanyType.组团 || this._companyrole[0] == CompanyType.其他采购商))
                    return TradeType.采购商;
                else
                    return TradeType.供应商;
            }
        }
        /// <summary>
        /// 公司品牌
        /// </summary>
        public string CompanyBrand { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 许可证
        /// </summary>
        public string License { get; set; }
        /// <summary>
        /// 公司介绍
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 引荐人
        /// </summary>
        public string CommendPeople { get; set; }
        /// <summary>
        /// 公司简介(业务优势)
        /// </summary>
        public string ShortRemark { get; set; }
        /// <summary>
        /// 公司网址
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// 联系人信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactInfo { get { return this._contactinfo; } set { this._contactinfo = value; } }

        #region 2011-12-20 线路改版增加的公司信息

        /// <summary>
        /// 公司自增编号
        /// </summary>
        public int OpCompanyId { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 公司规模
        /// </summary>
        public CompanyScale Scale { get; set; }

        /// <summary>
        /// 公司资质
        /// </summary>
        public IList<CompanyQualification> Qualification { get; set; }

        /// <summary>
        /// 公司所在地经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 公司所在地纬度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 同业联系方式
        /// </summary>
        public string PeerContact { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string AlipayAccount { get; set; }

        /// <summary>
        /// B2B显示控制
        /// </summary>
        public CompanyB2BDisplay B2BDisplay { get; set; }

        /// <summary>
        /// B2B显示排序
        /// </summary>
        public int B2BSort { get; set; }

        /// <summary>
        /// B2C显示控制
        /// </summary>
        public CompanyB2CDisplay B2CDisplay { get; set; }

        /// <summary>
        /// B2C显示排序
        /// </summary>
        public int B2CSort { get; set; }

        /// <summary>
        /// 公司网店点击量
        /// </summary>
        public int ClickNum { get; set; }

        /// <summary>
        /// 公司等级
        /// </summary>
        public CompanyLev CompanyLev { get; set; }

        /// <summary>
        /// 公司资料完整度（以小数存储，显示时需要乘以100 + %）
        /// </summary>
        public decimal InfoFull { get; set; }

        /// <summary>
        /// 签约开始时间
        /// </summary>
        public DateTime? ContractStart { get; set; }

        /// <summary>
        /// 签约结束时间
        /// </summary>
        public DateTime? ContractEnd { get; set; }

        #endregion

    }
    /// <summary>
    /// 单位基本信息实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-28
    [Serializable]
    public class CompanyBasicInfo : CompanyInfo
    {
        #region 私有变量
        /// <summary>
        /// 公司总管理员帐号信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.UserAccount _adminaccount = new EyouSoft.Model.CompanyStructure.UserAccount();
        #endregion 私有变量
        #region 公共属性
        /// <summary>
        /// 管理员帐号信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.UserAccount AdminAccount { get { return this._adminaccount; } set { this._adminaccount = value; } }


        #endregion 公共属性
    }

    /// <summary>
    /// 公司档案信息实体
    /// </summary>
    /// 创建人：张志瑜 2010-06-24
    public class CompanyArchiveInfo : CompanyBasicInfo
    {
        #region 私有变量
        /// <summary>
        /// 公司附件信息
        /// </summary>
        private CompanyAttachInfo _attachinfo = new CompanyAttachInfo();
        private List<BankAccount> _bankaccounts = new List<BankAccount>();
        #endregion 私有变量

        #region 公共属性
        /// <summary>
        /// 单位附件信息
        /// </summary>
        public CompanyAttachInfo AttachInfo { get { return this._attachinfo; } set { this._attachinfo = value; } }
        /// <summary>
        /// 获取或设置单位银行账户信息列表,若无,则为null
        /// </summary>
        public List<BankAccount> BankAccounts { get { return this._bankaccounts; } set { this._bankaccounts = value; } }
        /// <summary>
        /// 获取或设置单位销售城市
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.CityBase> SaleCity { get; set; }
        /// <summary>
        /// 获取或设置单位线路区域
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.AreaBase> Area { get; set; }
        /// <summary>
        /// 机票供应商扩展信息
        /// </summary>
        public EyouSoft.Model.TicketStructure.TicketWholesalersInfo TicketSupplierInfo { get; set; }

        #endregion 公共属性
    }
    /// <summary>
    /// 公司基本属性
    /// </summary>
    [Serializable]
    public class CompanyState
    {
        private CompanyService _companyservice = new CompanyService();
        #region 公共属性
        /// <summary>
        /// 公司开通的高级服务项目
        /// </summary>
        public CompanyService CompanyService { get { return this._companyservice; } set { this._companyservice = value; } }
        /// <summary>
        /// 公司是否审核通过(ture:审核通过  false:未通过)
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 是否启用(true:启用  false:停用)
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 加入时间(未审核通过的公司为注册时间,审核通过的公司为审核时间)
        /// </summary>
        public DateTime JoinTime { get; set; }
        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginCount { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        #endregion 公共属性
    }
    /// <summary>
    /// 旅行社公司基本属性
    /// </summary>
    [Serializable]
    public class CompanyStateMore : CompanyState
    {
        #region 公共属性
        /// <summary>
        /// 证书(营业执照,经营许可证,税务登记证)是否审核通过
        /// </summary>
        public bool IsCertificateCheck { get; set; }
        /// <summary>
        /// 承诺书是否审核通过
        /// </summary>
        public bool IsCreditCheck { get; set; }
        /// <summary>
        /// 公司推荐数
        /// </summary>
        public int CommendCount { get; set; }
        #endregion 公共属性
    }
    /// <summary>
    /// 高级服务项目
    /// </summary>
    public enum SysService
    {
        /// <summary>
        /// MQ收费功能
        /// </summary>
        MQ = 1,

        /// <summary>
        /// 高级网店
        /// </summary>
        HighShop = 2,

        /// <summary>
        /// 短信服务
        /// </summary>
        SMS = 3
    }
    /// <summary>
    /// 公司高级服务项目
    /// </summary>
    [Serializable]
    public class CompanyServiceItem
    {
        /// <summary>
        /// 高级服务类型
        /// </summary>
        public SysService Service
        {
            get;
            set;
        }
        /// <summary>
        /// 服务状态(true:表示可用,false:表示不可用)
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyServiceItem() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service">服务项目</param>
        /// <param name="isenabled">服务状态(true:表示可用,false:表示不可用)</param>
        public CompanyServiceItem(SysService service, bool isenabled)
        {
            this.Service = service;
            this.IsEnabled = isenabled;
        }
    }

    /// <summary>
    /// 公司开通的高级服务
    /// </summary>
    [Serializable]
    public class CompanyService
    {
        private List<CompanyServiceItem> _typeitems = new List<CompanyServiceItem>();
        /// <summary>
        /// 获得所有高级服务项目的元素
        /// </summary>
        public CompanyServiceItem[] ServiceItems
        {
            get { return this._typeitems.ToArray(); }
        }
        /// <summary>
        /// 获得所有高级服务项目的长度
        /// </summary>
        public int Length
        {
            get { return this._typeitems.Count; }
        }
        /// <summary>
        /// 获得某个索引值的高级服务项目元素
        /// </summary>
        /// <param name="index">要获得的元素从0开始的索引</param>
        /// <returns></returns>
        public CompanyServiceItem this[int index]
        {
            get { return this._typeitems[index]; }
        }
        /// <summary>
        /// 获得某个索引值的高级服务项目元素,若不存在,则返回null
        /// </summary>
        /// <param name="service">高级服务类型</param>
        /// <returns></returns>
        public CompanyServiceItem this[SysService service]
        {
            get
            {
                foreach (CompanyServiceItem item in this._typeitems)
                    if (item.Service == service)
                        return item;
                return null;
            }
        }
        /// <summary>
        /// 设置高级服务项目
        /// </summary>
        /// <param name="typeItem">身份类型</param>
        public void SetServiceItem(CompanyServiceItem typeItem)
        {
            this._typeitems.Add(typeItem);
        }
        /// <summary>
        /// 高级服务项目是否可用
        /// </summary>
        /// <param name="service">服务类型</param>
        /// <returns></returns>
        public bool IsServiceAvailable(SysService service)
        {
            return this._typeitems.Where(
                i => i.Service == service && i.IsEnabled == true
                ).Count() > 0;
        }
        /// <summary>
        /// 高级服务项目是否登记
        /// </summary>
        /// <param name="service">服务类型</param>
        /// <returns></returns>
        public bool IsServiceReg(SysService service)
        {
            bool isReg = false;
            foreach (CompanyServiceItem item in _typeitems)
            {
                if (item.Service == service)
                {
                    isReg = true; break;
                }
            }
            return isReg;
        }
    }
    /// <summary>
    /// 单位明细信息实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-28
    [Serializable]
    public class CompanyDetailInfo : CompanyArchiveInfo
    {
        #region 私有变量
        private CompanyStateMore _statemore = new CompanyStateMore();
        #endregion
        /// <summary>
        /// 单位更多属性
        /// </summary>
        public CompanyStateMore StateMore
        {
            get
            {
                return _statemore;
            }
            set
            {
                _statemore = value;
            }
        }
    }

    /// <summary>
    /// 同行信息
    /// </summary>
    public class CompanyTongHang : CompanyDetailInfo
    {
        ///// <summary>
        ///// 同行类型
        ///// </summary>
        //public CompanyType TongHangType { get; set; }

        /// <summary>
        /// 项目类型 1：MQ 2：高级网店 3：短信
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 县区名称
        /// </summary>
        public string CountyName { get; set; }
    }

    /// <summary>
    /// 公司标签类(景点主题、酒店周边环境)
    /// </summary>
    public class CompanyTag : EyouSoft.Model.SystemStructure.SysFieldBase
    {
    }
    /// <summary>
    /// 酒店星级
    /// </summary>
    public enum HotelLevel
    {
        三星以下 = 0,
        准三星,
        三星或同级,
        准四星,
        四星或同级,
        五星或同级
    }
    /// <summary>
    /// 供应商信息实体类
    /// </summary>
    public class SupplierInfo : CompanyInfo
    {
        #region 私有变量
        private IList<ProductInfo> _productinfo = new List<ProductInfo>();
        private CompanyState _state = new CompanyState();
        private IList<CompanyTag> _companytag = new List<CompanyTag>();
        #endregion 私有变量
        /// <summary>
        /// 公司宣传图片
        /// </summary>
        public string CompanyImg { get; set; }
        /// <summary>
        /// 公司LOGO
        /// </summary>
        public string CompanyImgThumb { get; set; }
        /// <summary>
        /// 公司产品图片
        /// </summary>
        public IList<ProductInfo> ProductInfo
        {
            get
            {
                return _productinfo;
            }
            set
            {
                _productinfo = value;
            }
        }
        /// <summary>
        /// 酒店星级
        /// </summary>
        public int CompanyLevel { get; set; }
        /// <summary>
        /// 公司标签(景点主题、酒店周边环境)
        /// </summary>
        public IList<CompanyTag> CompanyTag { get { return _companytag; } set { _companytag = value; } }
        /// <summary>
        /// 公司属性
        /// </summary>
        public CompanyState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
    }

    /// <summary>
    /// 公司身份类
    /// </summary>
    [Serializable]
    public class CompanyRole
    {
        private List<CompanyType> _typeitems = new List<CompanyType>();
        /// <summary>
        /// 获得所有单位类型的元素
        /// </summary>
        public CompanyType[] RoleItems
        {
            get { return this._typeitems.ToArray(); }
        }

        /// <summary>
        /// 获得公司身份列表的长度
        /// </summary>
        public int Length
        {
            get { return this._typeitems.Count; }
        }

        /// <summary>
        /// 获得某个索引值的单位类型元素
        /// </summary>
        /// <param name="index">要获得的元素从0开始的索引</param>
        /// <returns></returns>
        public CompanyType this[int index]
        {
            get { return this._typeitems[index]; }
        }
        /// <summary>
        /// 是否拥有身份
        /// </summary>
        /// <param name="typeItem">身份类型</param>
        /// <returns></returns>
        public bool HasRole(CompanyType typeItem)
        {
            return this._typeitems.Contains(typeItem);
        }
        /// <summary>
        /// 设置公司身份
        /// </summary>
        /// <param name="typeItem">身份类型</param>
        public void SetRole(CompanyType typeItem)
        {
            this._typeitems.Add(typeItem);
        }
    }

    /// <summary>
    /// 公司所有查询条件参数实体
    /// </summary>
    /// 创建人：张志瑜 2010-06-25
    public class QueryParamsAllCompany : QueryParamsCompany
    {
        private CompanyType _companytype = CompanyType.全部;
        private BusinessProperties _businessproperties = BusinessProperties.全部;
        private bool _isAuthentication = true;

        /// <summary>
        /// 引荐人,默认为空[[若=空,则不作为查询条件]
        /// </summary>
        public string CommendName { get; set; }
        /// <summary>
        /// 公司类型,默认为全部[若=全部,则不作为查询条件]
        /// </summary>
        public CompanyType CompanyType { get { return this._companytype; } set { this._companytype = value; } }
        /// <summary>
        /// 企业性质,默认为全部[若=全部,则不作为查询条件]
        /// </summary>
        public BusinessProperties BusinessProperties { get { return this._businessproperties; } set { this._businessproperties = value; } }
        /// <summary>
        /// 证书有无审核,默认为null[若=null,则不作为查询条件]
        /// </summary>
        public bool? IsCertificateCheck { get; set; }
        /// <summary>
        /// 是否查询在线公司[若=null,则不作为查询条件]
        /// </summary>
        public bool? IsQueryOnlineUser { get; set; }
        /// <summary>
        /// 查询公司用户登录开始时间[若=null,则不作为查询条件]
        /// </summary>
        public DateTime? LoginStartDate { get; set; }
        /// <summary>
        /// 查询公司用户登录结束时间[若=null,则不作为查询条件]
        /// </summary>
        public DateTime? LoginEndDate { get; set; }
        /// <summary>
        /// 是否验证后台权限
        /// </summary>
        public bool IsAuthentication { get { return _isAuthentication; } set { _isAuthentication = value; } }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
    }

    /// <summary>
    /// 公司基本查询条件参数实体
    /// </summary>
    public class QueryParamsCompany : QueryParamsCompanyBase
    {
        /// <summary>
        /// 联系人,默认为空[若=空,则不作为查询条件]
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 公司名牌名称,默认为空[若=空,则不作为查询条件]
        /// </summary>
        public string CompanyBrand { get; set; }
    }

    /// <summary>
    /// 公司查询条件参数基类
    /// </summary>
    /// 创建人：张志瑜 2010-06-25
    public class QueryParamsCompanyBase
    {
        /// <summary>
        /// 省份ID,默认为0[若=0,则不作为查询条件]
        /// </summary>
        public int PorvinceId { get; set; }
        /// <summary>
        /// 城市ID,默认为0[若=0,则不作为查询条件]
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 线路区域ID,默认为0[若=0,则不作为查询条件]
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 公司名称,默认为空[若=空,则不作为查询条件]
        /// </summary>
        public string CompanyName { get; set; }
    }

    #region 枚举类型
    /// <summary>
    /// 查询城市的方式类型
    /// </summary>
    /// 创建人：张志瑜 2010-06-25
    public enum QueryCityType
    {
        /// <summary>
        /// 按公司的注册所在地查询
        /// </summary>
        注册城市,

        /// <summary>
        /// 按公司所拥有的销售城市查询
        /// </summary>
        销售城市,

        /// <summary>
        /// 按公司所拥有的出港城市查询
        /// </summary>
        出港城市
    }

    /// <summary>
    /// 单位类型
    /// </summary>
    /// 创建人：张志瑜 2010-05-28
    [Serializable]
    public enum CompanyType
    {
        /// <summary>
        /// 全部
        /// </summary>
        全部 = 0,
        /// <summary>
        /// 专线商(批发商)
        /// </summary>
        专线 = 1,
        /// <summary>
        /// 组团社(零售商)
        /// </summary>
        组团 = 2,
        /// <summary>
        /// 地接社
        /// </summary>
        地接 = 3,
        /// <summary>
        /// 景区
        /// </summary>
        景区 = 4,
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 5,
        /// <summary>
        /// 车队
        /// </summary>
        车队 = 6,
        /// <summary>
        /// 旅游用品店
        /// </summary>
        旅游用品店 = 7,
        /// <summary>
        /// 购物店
        /// </summary>
        购物店 = 8,
        /// <summary>
        /// 机票供应商
        /// </summary>
        机票供应商 = 9,
        /// <summary>
        /// 其他采购商 = 10
        /// </summary>
        其他采购商 = 10,
        /// <summary>
        /// 随便逛逛 = 11
        /// </summary>
        随便逛逛 = 11
    }

    /// <summary>
    /// 企业性质
    /// </summary>
    /// 创建人：张志瑜 2010-06-22
    public enum BusinessProperties
    {
        /// <summary>
        /// 全部
        /// </summary>
        全部 = 0,
        /// <summary>
        /// 旅游社
        /// </summary>
        旅游社 = 1,
        /// <summary>
        /// 景区
        /// </summary>
        景区 = 2,
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 3,
        /// <summary>
        /// 车队
        /// </summary>
        车队 = 4,
        /// <summary>
        /// 旅游用品店
        /// </summary>
        旅游用品店 = 5,
        /// <summary>
        /// 购物店
        /// </summary>
        购物店 = 6,
        /// <summary>
        /// 机票供应商
        /// </summary>
        机票供应商 = 7,
        /// <summary>
        /// 其他采购商 = 8
        /// </summary>
        其他采购商 = 8,
        /// <summary>
        /// 随便逛逛
        /// </summary>
        随便逛逛 = 9
    }

    /// <summary>
    /// 公司贸易类型
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// 全部 = 0
        /// </summary>
        全部 = 0,
        /// <summary>
        /// 采购商 = 1
        /// </summary>
        采购商 = 1,
        /// <summary>
        /// 供应商 = 2
        /// </summary>
        供应商 = 2,
        /// <summary>
        /// 随便逛逛 = 3
        /// </summary>
        随便逛逛 = 3
    }

    /// <summary>
    /// 公司规模
    /// </summary>
    /// 2011-12-20线路改版时添加的枚举
    public enum CompanyScale
    {
        /// <summary>
        /// 无
        /// </summary>
        无 = 0,
        /// <summary>
        /// 10人以下
        /// </summary>
        十人以下 = 1,
        /// <summary>
        /// 10~20人
        /// </summary>
        十人至二十人 = 2,
        /// <summary>
        /// 20~50人
        /// </summary>
        二十至五十人 = 3,
        /// <summary>
        /// 50~100人
        /// </summary>
        五十至一百人 = 4,
        /// <summary>
        /// 100~200人
        /// </summary>
        一百人至二百人 = 5,
        /// <summary>
        /// 200人以上
        /// </summary>
        二百人以上 = 6
    }

    /// <summary>
    /// 公司资质
    /// </summary>
    public enum CompanyQualification
    {
        /// <summary>
        /// 出境旅游
        /// </summary>
        出境旅游 = 1,
        /// <summary>
        /// 入境旅游
        /// </summary>
        入境旅游 = 2,
        /// <summary>
        /// 台湾旅游
        /// </summary>
        台湾旅游 = 3
    }

    /// <summary>
    /// 公司b2b显示控制
    /// </summary>
    public enum CompanyB2BDisplay
    {
        /// <summary>
        /// 常规
        /// </summary>
        常规 = 0,
        /// <summary>
        ///  侧边推荐,
        /// </summary>
        侧边推荐 = 1,
        /// <summary>
        /// 列表置顶,
        /// </summary>
        列表置顶 = 2,
        /// <summary>
        /// 首页推荐,
        /// </summary>
        首页推荐 = 3,
        /// <summary>
        /// 隐藏,
        /// </summary>
        隐藏 = 99
    }

    /// <summary>
    /// 公司b2c显示控制
    /// </summary>
    public enum CompanyB2CDisplay
    {
        /// <summary>
        /// 常规
        /// </summary>
        常规 = 0,
        /// <summary>
        ///  侧边推荐,
        /// </summary>
        侧边推荐 = 1,
        /// <summary>
        /// 列表置顶,
        /// </summary>
        列表置顶 = 2,
        /// <summary>
        /// 首页推荐,
        /// </summary>
        首页推荐 = 3,
        /// <summary>
        /// 隐藏,
        /// </summary>
        隐藏 = 99
    }

    /// <summary>
    /// 公司等级
    /// </summary>
    public enum CompanyLev
    {
        /// <summary>
        /// 注册商户（待审核）
        /// </summary>
        注册商户 = 0,
        /// <summary>
        /// 审核商户（已审核）
        /// </summary>
        审核商户 = 1,
        /// <summary>
        /// 免费认证
        /// </summary>
        认证商户 = 2,
        /// <summary>
        /// 收费推荐
        /// </summary>
        推荐商户 = 3,
        /// <summary>
        /// 签约入驻
        /// </summary>
        签约商户 = 4,
    }

    #endregion 枚举类型

    #region 最新加入企业信息业务实体
    /// <summary>
    /// 最新加入企业信息业务实体
    /// </summary>
    public class MLatestRegCompanyInfo : Company
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLatestRegCompanyInfo() { }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
    }
    #endregion

    #region 2011-12-20线路改版新公司查询实体

    /// <summary>
    /// 2011-12-20线路改版新公司查询实体
    /// </summary>
    public class QueryNewCompany : QueryParamsCompanyBase
    {
        /// <summary>
        /// 关键字(公司名、地址、简称、品牌)
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 县区编号
        /// </summary>
        public int CountyId { get; set; }

        /// <summary>
        /// 单位类型
        /// </summary>
        public CompanyType?[] CompanyTypes { get; set; }

        /// <summary>
        /// 公司等级
        /// </summary>
        public CompanyLev? CompanyLev { get; set; }

        /// <summary>
        /// b2b显示控制
        /// </summary>
        public CompanyB2BDisplay? B2BDisplay { get; set; }

        /// <summary>
        /// b2c显示控制
        /// </summary>
        public CompanyB2CDisplay? B2CDisplay { get; set; }

        /// <summary>
        /// 是否显示未审核的
        /// </summary>
        public bool IsShowNoCheck { get; set; }

        /// <summary>
        /// 排序方式（0/1审核时间降/升序;2/3 B2B排序降/升序）
        /// </summary>
        public int OrderIndex { get; set; }

    }

    #endregion
}
