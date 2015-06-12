using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-28
    /// 描述：单位设置实体
    /// </summary>
    [Serializable]
    public class CompanySetting
    {
        /// <summary>
        /// 优先展示栏目
        /// </summary>
        private MenuSection _firstmenu = MenuSection.组团服务;
        /// <summary>
        /// 订单刷新时间(单位:分钟)
        /// </summary>
        private int _orderrefresh = 30;
        /// <summary>
        /// 可开通的子帐号数量
        /// </summary>
        private int _operatorlimit = 0;
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 自动停收时间(单位:天)
        /// </summary>
        public int TourStopTime { get; set; }
        /// <summary>
        /// 优先展示栏目,默认为"组团"
        /// </summary>
        public MenuSection FirstMenu { get { return this._firstmenu; } set { this._firstmenu = value; } }
        /// <summary>
        /// 订单刷新时间(单位:分钟),默认为30分钟
        /// </summary>
        public int OrderRefresh { get { return this._orderrefresh; } set { this._orderrefresh = value; } }
        /// <summary>
        /// 可开通的子帐号数量,默认为0
        /// </summary>
        public int OperatorLimit { get { return this._operatorlimit; } set { this._operatorlimit = value; } }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public EyouSoft.Model.ShopStructure.PositionInfo PositionInfo { get; set; }
    }

    /// <summary>
    /// 描述：菜单栏目
    /// </summary>
    /// 创建人：张志瑜 2010-06-23
    [Serializable]
    public enum MenuSection
    {
        /// <summary>
        /// 专线服务栏目
        /// </summary>
        专线服务 = 0,

        /// <summary>
        /// 组团服务栏目
        /// </summary>
        组团服务 = 1,

        /// <summary>
        /// 地接服务栏目
        /// </summary>
        地接服务 = 2,
        /// <summary>
        /// 景区服务栏目
        /// </summary>
        景区服务 = 3,
        /// <summary>
        /// 酒店服务栏目
        /// </summary>
        酒店服务 = 4
    }
}
