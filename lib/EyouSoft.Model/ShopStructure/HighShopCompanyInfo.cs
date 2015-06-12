using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：高级网店详细信息
    /// </summary>
    public class HighShopCompanyInfo
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopCompanyInfo() { }
        #endregion

        #region 属性
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司介绍
        /// </summary>
        public string CompanyInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 公司网店版权
        /// </summary>
        public string ShopCopyRight
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义模板编号
        /// </summary>
        public int TemplateId
        {
            get;
            set;
        }

        /// <summary>
        /// 地理位置
        /// </summary>
        public PositionInfo PositionInfo { get; set; }

        /// <summary>
        /// 谷歌地图APIKey
        /// </summary>
        public string GoogleMapKey { get; set; }

        #endregion
    }

    #region 地理位置

    /// <summary>
    /// 地理位置
    /// </summary>
    [Serializable]
    public class PositionInfo
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 地图缩放级别
        /// </summary>
        public int ZoomLevel { get; set; }
    }

    #endregion
}
