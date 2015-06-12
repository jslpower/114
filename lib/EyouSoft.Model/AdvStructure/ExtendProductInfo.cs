using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.AdvStructure
{
    /// <summary>
    /// 描述:首页推荐产品实体类
    /// 修改记录:
    /// 1 2011-05-10 曹胡生 创建
    /// </summary>
    public class ExtendProductInfo
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系MQ
        /// </summary>
        public string ContactMQ { get; set; }
        /// <summary>
        /// 投放城市名称
        /// </summary>
        public string ShowCity { get; set; }
        /// <summary>
        /// 投放城市编号
        /// </summary>
        public int ShowCityId { get; set; }
        /// <summary>
        /// 投放省份编号
        /// </summary>
        public int ShowProId { get; set; }
    }
}
