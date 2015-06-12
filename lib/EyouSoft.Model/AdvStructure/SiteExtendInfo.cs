using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.AdvStructure
{
    /// <summary>
    /// 描述：分站旅行社推广实体类
    /// 修改记录:
    /// 1. 2011-05-09 AM 曹胡生 创建
    /// </summary>
    public class SiteExtendInfo
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int SortID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司产品数（发布的线路数）
        /// </summary>
        public int ProductNum { get; set; }
        /// <summary>
        /// 公司登录次数
        /// </summary>
        public int LoginCount { get; set; }
        /// <summary>
        /// 公司名称前台显示时，是否加粗
        /// </summary>
        public bool IsBold { get; set; }
        /// <summary>
        /// 公司名称前台显示时使用的颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 公司名称是否显示在前台页面
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 投放城市名称
        /// </summary>
        public string ShowCity { get; set; }
        /// <summary>
        /// 投放城市编号
        /// </summary>
        public int ShowCityID { get; set; }
         /// <summary>
        /// 投放省份编号
        /// </summary>
        public int ShowProID { get; set; }
    }
    /// <summary>
    /// 分站实体类
    /// </summary>
    public class SiteExtend
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
    }

    /// <summary>
    /// 已审核的公司实体类
    /// </summary>
    public class CompanySelectInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司添加时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 公司所在的城市编号
        /// </summary>
        public int CityID { get; set; }
        /// <summary>
        /// 公司所在的城市名
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 该公司是否是付费用户（是否开退了高级网店）
        /// </summary>
        public bool IsPay { get; set; }
        /// <summary>
        /// 该公司是否已推荐
        /// </summary>
        //public bool IsSelected { get; set; }
    }
}
