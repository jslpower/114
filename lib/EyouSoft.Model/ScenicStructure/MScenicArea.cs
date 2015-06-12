using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ScenicStructure
{
    /// <summary>
    /// 景区
    /// 创建者：郑付杰
    /// 创建时间：2011/10/27
    /// </summary>
    [Serializable]
    public class MScenicArea:MScenicBase
    {
        /// <summary>
        /// 
        /// </summary>
        public MScenicArea() { }

        /// <summary>
        /// 自增编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 成立年份
        /// </summary>
        public int SetYear { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Y { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int CountyId { get; set; }
        /// <summary>
        /// 县区名称
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// 地标
        /// </summary>
        public IList<MScenicRelationLandMark> LankId { get; set; }
        /// <summary>
        /// 中文地址
        /// </summary>
        public string CnAddress { get; set; }
        /// <summary>
        /// 英文地址
        /// </summary>
        public string EnAddress { get; set; }
        /// <summary>
        /// 主题编号
        /// </summary>
        public IList<MScenicTheme> ThemeId { get; set; }
        /// <summary>
        /// 景区等级
        /// </summary>
        public ScenicLevel ScenicLevel { get; set; }
        /// <summary>
        /// 日常开放时间
        /// </summary>
        public string OpenTime { get; set; }
        /// <summary>
        /// 景区详细介绍
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 交通说明
        /// </summary>
        public string Traffic { get; set; }
        /// <summary>
        /// 周边设施
        /// </summary>
        public string Facilities { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ExamineStatus Status { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 发布公司
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyInfo Company { get; set; }
        /// <summary>
        /// 景区联系人
        /// </summary>
        public string ContactOperator { get; set; }
        /// <summary>
        /// 景区联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 景区联系人电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 景区联系人手机
        /// </summary>
        public string ContactMobile { get; set; }
       
        /// <summary>
        /// 点击量
        /// </summary>
        public int ClickNum { get; set; }
        /// <summary>
        /// 景区图片
        /// </summary>
        public IList<MScenicImg> Img { get; set; }
        /// <summary>
        /// 景区门票
        /// </summary>
        public IList<MScenicTickets> TicketsList { get; set; }
    }

    /// <summary>
    /// 景区门票共有属性
    /// </summary>
    public abstract class MScenicBase
    {
        public MScenicBase() { }
        /// <summary>
        /// 景区编号
        /// </summary>
        public string ScenicId { get; set; }
        /// <summary>
        /// 景区名称
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// B2B显示控制
        /// </summary>
        public ScenicB2BDisplay B2B { get; set; }
        /// <summary>
        /// B2B排序值
        /// </summary>
        public int B2BOrder { get; set; }
        /// <summary>
        /// B2C显示控制
        /// </summary>
        public ScenicB2CDisplay B2C { get; set; }
        /// <summary>
        /// B2C排序值
        /// </summary>
        public int B2COrder { get; set; }
        /// <summary>
        /// 发布用户编号
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 审核用户编号
        /// </summary>
        public int ExamineOperator { get; set; }
    }

    /// <summary>
    /// 景区地标关联
    /// </summary>
    [Serializable]
    public class MScenicRelationLandMark
    {
        public MScenicRelationLandMark() { }
        /// <summary>
        /// 景区编号
        /// </summary>
        public string ScenicId { get; set; }
        /// <summary>
        /// 地标编号
        /// </summary>
        public int LandMarkId { get; set; }
    }

    /// <summary>
    /// 景区搜索实体
    /// </summary>
    [Serializable]
    public class MSearchSceniceArea
    {
        /// <summary>
        /// 省份
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int? CountyId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ExamineStatus? Status { get; set; }
        /// <summary>
        /// 景区名称
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 主题编号
        /// </summary>
        public int? ThemeId { get; set; }
        /// <summary>
        /// B2B显示
        /// </summary>
        public ScenicB2BDisplay? B2B { get; set; }
        /// <summary>
        /// B2C显示
        /// </summary>
        public ScenicB2CDisplay? B2C { get; set; }
        /// <summary>
        /// 景区等级
        /// </summary>
        public ScenicLevel? Level { get; set; }
        /// <summary>
        /// 前后台 (true:后台 false：前台)
        /// </summary>
        public bool? IsQH { get; set; }
        /// <summary>
        /// 排序方式
        /// 1:b2b显示控制 2:景区点击量 3:景区等级 (其他整型数字无效)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 易诺公司编号
        /// </summary>
        public string YiNuo { get; set; }

        /// <summary>
        /// 景区b2b状态查询集合
        /// </summary>
        public ScenicB2BDisplay?[] B2Bs { get; set; }
    }

}
