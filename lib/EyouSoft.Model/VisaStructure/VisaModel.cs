using System;
using System.Collections.Generic;

namespace EyouSoft.Model.VisaStructure
{
    #region 国家实体基类
    /// <summary>
    /// 国家实体基类
    /// </summary>
    /// 郑知远 2011/05/06
    [Serializable]
    public class Country
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Country(){}

        /// <summary>
        /// 国家编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 洲名
        /// </summary>
        public Areas Areas { get; set; }

        /// <summary>
        /// 国家中文名
        /// </summary>
        public string CountryCn { get; set; }

        /// <summary>
        /// 国家英文名
        /// </summary>
        public string CountryEn { get; set; }

        /// <summary>
        /// 国家简拼
        /// </summary>
        public string CountryJp { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        public string CountryCd { get; set; }

        /// <summary>
        /// 国家国旗
        /// </summary>
        public string FlagPath { get; set; }
    }
    #endregion

    #region 旅游签证实体基类
    /// <summary>
    /// 旅游签证实体
    /// </summary>
    [Serializable]
    public class Visa
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Visa() { }

        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 使馆信息
        /// </summary>
        public string EmbassyInfo { get; set; }

        /// <summary>
        /// 特别提示
        /// </summary>
        public string HintInfo { get; set; }

        /// <summary>
        /// 申请表下载
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 所需签证资料
        /// </summary>
        public IList<File> FileInfos { get; set; }

        /// <summary>
        /// 站点信息
        /// </summary>
        public IList<Link> Links { get; set; }
    }
    #endregion

    #region 所需签证资料实体
    /// <summary>
    /// 所需签证资料实体
    /// </summary>
    [Serializable]
    public class File
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public File(){}

        /// <summary>
        /// 资料类型
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// 资料信息
        /// </summary>
        public string FileInfo { get; set; }
    }
    #endregion

    #region 站点信息实体
    /// <summary>
    /// 站点信息实体
    /// </summary>
    [Serializable]
    public class Link
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Link() { }

        /// <summary>
        /// 站点标题
        /// </summary>
        public string LinkTile { get; set; }

        /// <summary>
        /// 站点链接
        /// </summary>
        public string LinkUrl { get; set; }
    }
    #endregion

    #region 洲名枚举
    /// <summary>
    /// 洲名枚举
    /// </summary>
    public enum Areas
    {
        /// <summary>
        /// 欧洲
        /// </summary>
        欧洲 = 1,

        /// <summary>
        /// 亚洲
        /// </summary>
        亚洲 = 2,

        /// <summary>
        /// 美洲
        /// </summary>
        美洲 = 3,

        /// <summary>
        /// 非洲
        /// </summary>
        非洲 = 4,
        /// <summary>
        /// 大洋洲
        /// </summary>
        大洋洲=5
    }
    #endregion

    #region 签证资料枚举
    /// <summary>
    /// 签证资料枚举
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 个人身份证明
        /// </summary>
        个人身份证明 = 1,

        /// <summary>
        /// 资产证明
        /// </summary>
        资产证明 = 2,

        /// <summary>
        /// 工作证明
        /// </summary>
        工作证明 = 3,

        /// <summary>
        /// 学生及儿童
        /// </summary>
        学生及儿童 = 4,

        /// <summary>
        /// 老人
        /// </summary>
        老人 = 5,

        /// <summary>
        /// 其他
        /// </summary>
        其他 = 6
    }
    #endregion
}
