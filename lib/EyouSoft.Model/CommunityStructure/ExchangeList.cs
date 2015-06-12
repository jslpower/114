using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-16
    /// 描述：供求信息实体类
    /// </summary>
    /// 修改人：zhengfj 2011-05-10
    /// 修改内容：ExchangeType枚举 ExchangeTag枚举
    /// 新增内容：SearchDateTimeTyoe枚举 ExchangeCategory枚举 
    public class ExchangeList:ExchangeListBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeList() { }

        #region 属性
        /// <summary>
        /// 供求内容
        /// </summary>
        public string ExchangeText
        {
            get;
            set;
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人用户ID
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人姓名
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        } 
        /// <summary>
        /// 发布人城市编号
        /// </summary>
        public int CityId
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
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            get;
            set;
        }
        /// <summary>
        /// 是否置顶(1:置顶  0:非置顶 ,默认为0)
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
        /// <summary>
        /// 置顶时间(默认为null)
        /// </summary>
        public DateTime? TopTime
        {
            get;
            set;
        }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string AttatchPath
        {
            get;
            set;
        }
        /// <summary>
        /// 是否审核(1:审核  0:未审核 ,默认为0)
        /// </summary>
        public bool IsCheck
        {
            get;
            set;
        }
        /// <summary>
        /// 供求类别
        /// </summary>
        public ExchangeCategory ExchangeCategory { get; set; }

        #endregion

        #region 附加属性
        /// <summary>
        /// 供求信息与城市关系集合
        /// </summary>
        public IList<ExchangeCityContact> CityContactList
        {
            get;
            set;
        }
        /// <summary>
        /// 供求信息图片集合
        /// </summary>
        public IList<ExchangePhoto> ExchangePhotoList
        {
            get;
            set;
        } 
        #endregion 
        /// <summary>
        /// QGroup供求发布信息的QQ号码
        /// </summary>
        public string QGroupSendU { get; set; }
    }
    #region 供求信息基类
    /// <summary>
    /// 供求信息基类
    /// </summary>
    public class ExchangeListBase
    {
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
        /// 信息类别编号
        /// </summary>
        public ExchangeType TopicClassID
        {
            get;
            set;
        }
        /// <summary>
        /// 供求标签
        /// </summary>
        public ExchangeTag ExchangeTag
        {
            get;
            set;
        }
        /// <summary>
        /// 供求标题（根据供求内容截取）
        /// </summary>
        public string ExchangeTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人公司ID
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人公司名称
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人MQ号码
        /// </summary>
        public string OperatorMQ
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人省份编号
        /// </summary>
        public int ProvinceId
        {
            get;
            set;
        }
        /// <summary>
        /// 供求标签的HTML
        /// </summary>
        public Tag ExchangeTagHtml
        {
            get
            {
                return new Tag((int)this.ExchangeTag);
            }
        }
        /// <summary>
        /// 回复次数(为所有的回复次数)
        /// </summary>
        public int WriteBackCount
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 供求信息类别枚举
    /// <summary>
    /// 供求信息类别枚举
    /// </summary>
    public enum ExchangeType
    {
        /// <summary>
        /// 团队询价
        /// </summary>
        团队询价 = 1,
        /// <summary>
        /// 地接报价
        /// </summary>
        地接报价 = 2,
        /// <summary>
        /// 直通车
        /// </summary>
        直通车 = 3,
        /// <summary>
        /// 车辆
        /// </summary>
        车辆 = 4,
        /// <summary>
        /// 酒店
        /// </summary>
        酒店 = 5,
        /// <summary>
        /// 导游/招聘
        /// </summary>
        招聘 = 6,
        /// <summary>
        /// 票务
        /// </summary>
        票务 = 7,
        /// <summary>
        /// 找地接
        /// </summary>
        找地接 = 9,
        /// <summary>
        /// 机票
        /// </summary>
        机票 = 10,
        /// <summary>
        /// 签证
        /// </summary>
        签证 = 11,
        /// <summary>
        /// 同业MQ
        /// </summary>
        同业MQ = 12,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 8

        ///// <summary>
        ///// 团队询价
        ///// </summary>
        //团队询价=1,
        ///// <summary>
        ///// 地接报价
        ///// </summary>
        //地接报价,
        ///// <summary>
        ///// 直通车
        ///// </summary>
        //直通车,
        ///// <summary>
        ///// 车辆
        ///// </summary>
        //车辆,
        ///// <summary>
        ///// 酒店
        ///// </summary>
        //酒店,
        ///// <summary>
        ///// 导游/招聘
        ///// </summary>
        //导游招聘,
        ///// <summary>
        ///// 票务-签证
        ///// </summary>
        //票务签证,
        ///// <summary>
        ///// 其他
        ///// </summary>
        //其他

    }
    #endregion

    #region 供求信息标签枚举
    /// <summary>
    /// 供求信息标签枚举
    /// </summary>
    public enum ExchangeTag
    {
        /// <summary>
        /// 无
        /// </summary>
        无 = 1,
        /// <summary>
        /// 品质
        /// </summary>
        品质 = 2,
        /// <summary>
        /// 特价
        /// </summary>
        特价 = 3,
        /// <summary>
        /// 急急急
        /// </summary>
        急急急 = 4,
        /// <summary>
        /// 最新报价
        /// </summary>
        最新报价 = 5,
        /// <summary>
        /// 热
        /// </summary>
        热 = 6

        ///// <summary>
        ///// 供应信息
        ///// </summary>
        //供应信息=1,
        ///// <summary>
        ///// 需求信息
        ///// </summary>
        //需求信息,
        ///// <summary>
        ///// 敬请收客
        ///// </summary>
        //敬请收客,
        ///// <summary>
        ///// 特价抢购
        ///// </summary>
        //特价抢购,
        ///// <summary>
        ///// 出团计划
        ///// </summary>
        //出团计划,
        ///// <summary>
        ///// 急寻报价
        ///// </summary>
        //急寻报价
    }
    #endregion

    #region 供求信息城市关系实体
    /// <summary>
    /// 供求信息城市关系实体
    /// </summary>
    public class ExchangeCityContact
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeCityContact()
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exchangeid">供求编号</param>
        /// <param name="provinceid">省份编号</param>
        /// <param name="cityid">城市编号</param>
        public ExchangeCityContact(string exchangeid, int provinceid, int cityid)
        {
            this.ExchangeId = exchangeid;
            this.ProvinceId = provinceid;
            this.CityId = cityid;
        }
        /// <summary>
        /// 供求信息编号
        /// </summary>
        public string ExchangeId
        {
            get;
            set;
        }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId
        {
            get;
            set;
        }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId
        {
            get;
            set;
        }
    }
    #endregion

    #region 供求信息相关图片实体
    /// <summary>
    /// 供求信息相关图片实体
    /// </summary>
    public class ExchangePhoto
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ExchangePhoto() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="imgid">主键编号</param>
        /// <param name="exchangeid">供求信息编号</param>
        /// <param name="imgpath">图片路径</param>
        /// <param name="issuetime">添加时间</param>
        public ExchangePhoto(string imgid, string exchangeid, string imgpath, DateTime issuetime)
        {
            this.ExchangeId = exchangeid;
            this.ImgId = imgid;
            this.ImgPath = imgpath;
            this.IssueTime = issuetime;
        }
        /// <summary>
        /// 图片编号
        /// </summary>
        public string ImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 供求编号
        /// </summary>
        public string ExchangeId
        {
            get;
            set;
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
    }
    #endregion

    #region 供求信息标签实体
    /// <summary>
    /// 供求信息标签实体
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        public Tag(int id)
        {
            this.ID = id;
        }
        /// <summary>
        /// 供求标签编号
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 供求标签HTML
        /// </summary>
        public string TagHTML
        {
            get 
            { 
                return string.Format("<div class=\"gqbiaoqian{0}\">{1}</div>",this.ID,((ExchangeTag)this.ID).ToString());
            }
        }
    }
    #endregion

    #region 日期相关查询枚举
    /// <summary>
    /// 日期相关查询枚举
    /// </summary>
    public enum SearchDateType
    { 
        /// <summary>
        /// 全部
        /// </summary>
        全部=0,
        /// <summary>
        /// 今天
        /// </summary>
        今天,
        /// <summary>
        /// 近三天
        /// </summary>
        近三天,
        /// <summary>
        /// 本周
        /// </summary>
        本周,
        /// <summary>
        /// 本月
        /// </summary>
        本月
    }
    #endregion

    #region 供求搜索条件实体

    /// <summary>
    /// 供求搜索实体
    /// </summary>
    public class SearchInfo
    {
        public SearchInfo() { }

        /// <summary>
        /// 发布人用户ID
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 供/求 为空时查询供求类别为[供、求，不含QGroup]
        /// </summary>
        public ExchangeCategory? ExchangeCategory { get; set; }

        /// <summary>
        /// 供求信息类别
        /// </summary>
        public ExchangeType? ExchangeType { get; set; }

        /// <summary>
        /// 供求信息标签
        /// </summary>
        public ExchangeTag? ExchangeTag { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public int? Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public int? City { get; set; }

        /// <summary>
        /// 标题关键字
        /// </summary>
        public string ExchangeTitle { get; set; }

        /// <summary>
        /// 日期枚举 
        /// </summary>
        public SearchDateTimeTyoe? SearchDateTimeType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 前台读取还是后台读取(默认false为前台)
        /// </summary>
        public bool BeforeOrAfter { get; set; }
        /// <summary>
        /// 供求编号
        /// </summary>
        public string OfferId { get; set; }
    }
     

    #endregion

    #region 日期相关查询枚举

    /// <summary>
    /// 日期搜索枚举
    /// </summary>
    public enum SearchDateTimeTyoe
    {
        /// <summary>
        /// 今天
        /// </summary>
        今天 = 1,
        /// <summary>
        /// 昨天
        /// </summary>
        昨天 = 2,
        /// <summary>
        /// 前天
        /// </summary>
        前天 = 3,
        /// <summary>
        /// 更早
        /// </summary>
        更早 = 4,
        /// <summary>
        /// 时间段
        /// </summary>
        时间段 = 5
    }

    #endregion

    #region 供求类别
    /// <summary>
    /// 供求类别
    /// </summary>
    public enum ExchangeCategory
    {
        /// <summary>
        /// 供
        /// </summary>
        供 = 1,
        /// <summary>
        /// 求
        /// </summary>
        求 = 2,
        /// <summary>
        /// QQ群供求
        /// </summary>
        QGroup
    }

    #endregion
}
