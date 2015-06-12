using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ScenicStructure
{
    /// <summary>
    /// 景区图片
    /// 创建者：郑付杰
    /// 创建时间：2011/10/27
    /// </summary>
    [Serializable]
    public class MScenicImg
    {
        public MScenicImg() { }

        /// <summary>
        /// 图片编号
        /// </summary>
        public string ImgId { get; set; }
        /// <summary>
        /// 景区自增编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 景区编号
        /// </summary>
        public string ScenicId { get; set; }
        /// <summary>
        /// 景区名称
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// 图片类型
        /// </summary>
        public ScenicImgType ImgType { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbAddress { get; set; }
        /// <summary>
        /// 图片说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
    }

    /// <summary>
    /// 图片搜索实体
    /// </summary>
    [Serializable]
    public class MScenicImgSearch
    {
        public MScenicImgSearch() { }

        /// <summary>
        /// 图片类型
        /// </summary>
        public ScenicImgType?[] ImgType { get; set; }
    }
}
