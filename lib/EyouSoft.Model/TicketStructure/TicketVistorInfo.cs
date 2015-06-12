using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CompanyStructure;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 常旅客实体类
    /// </summary>
    /// 创建人：luofx 2010-10-21
    [Serializable]
    public class TicketVistorInfo
    {
        /// <summary>
        /// 国籍信息
        /// </summary>
        private TicketNationInfo _NationInfo = new TicketNationInfo();
        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketVistorInfo() { }
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 平台公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public TicketCardType CardType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 旅客中文姓名
        /// </summary>
        public string ChinaName { get; set; }
        /// <summary>
        /// 旅客英文姓名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 旅客性别
        /// </summary>
        public Sex ContactSex { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 国籍信息
        /// </summary>
        public EyouSoft.Model.TicketStructure.TicketNationInfo NationInfo
        {
            get { return _NationInfo; }
            set { _NationInfo = value; }
        }
        /// <summary>
        /// 旅客类别
        /// </summary>
        public TicketVistorType VistorType { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 常旅客数据来源类型
        /// </summary>
        private TicketDataType _DataType = TicketDataType.机票常旅客;
        /// <summary>
        /// 常旅客数据来源类型
        /// </summary>
        public TicketDataType DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮寄地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮政编号
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCardCode { get; set; }
        /// <summary>
        /// 护照号码
        /// </summary>
        public string PassportCode { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
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
        public int DistrictId { get; set; }
    }

    /// <summary>
    /// 机票证件类型枚举
    /// 创建人：luofx 2010-10-21
    /// </summary>
    public enum TicketCardType
    {
        /// <summary>
        /// None
        /// </summary>
        None = -1,
        /// <summary>
        /// 身份证
        /// </summary>
        身份证 = 0,
        /// <summary>
        /// 护照
        /// </summary>
        护照 = 1,
        /// <summary>
        /// 军官证
        /// </summary>
        军官证 = 2,
        /// <summary>
        /// 台胞证
        /// </summary>
        台胞证 = 3,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        港澳通行证 = 4
    }

    /// <summary>
    /// 旅客类别枚举
    /// 创建人：luofx 2010-10-21
    /// </summary>
    public enum TicketVistorType
    {
        /// <summary>
        /// 成人
        /// </summary>
        成人 = 0,
        /// <summary>
        /// 儿童
        /// </summary>
        儿童 = 1,
        /// <summary>
        /// 婴儿
        /// </summary>
        婴儿 = 2
    }

    /// <summary>
    /// 常旅客数据来源类型
    /// </summary>
    public enum TicketDataType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 机票常旅客
        /// </summary>
        机票常旅客 = 1,
        /// <summary>
        /// 酒店常旅客
        /// </summary>
        酒店常旅客 = 2,
        /// <summary>
        /// 线路常旅客
        /// </summary>
        线路常旅客
    }

    /// <summary>
    /// 常旅客查询信息业务实体
    /// </summary>
    public class MVisitorSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MVisitorSearchInfo() { }

        /// <summary>
        /// 常旅客类型(机票、酒店、线路等)
        /// </summary>
        public TicketDataType? Type { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 旅客类别(按年龄分：成人、儿童、婴儿等)
        /// </summary>
        public TicketVistorType? VType {get;set;}

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}
