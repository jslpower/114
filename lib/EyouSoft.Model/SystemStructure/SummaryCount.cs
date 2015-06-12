using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统数据统计实体类
    /// </summary>
    /// 创建人:蒋胜蓝  2010-6-23
    [Serializable]
    public class SummaryCount
    {
        /// <summary>
        /// 采购商
        /// </summary>
        public int Buyer
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商
        /// </summary>
        public int Suppliers
        {
            get;
            set;
        }

        /// <summary>
        /// 近30天供求数量
        /// </summary>
        public int SupplyInfos
        {
            get;
            set;
        }

        /// <summary>
        /// 旅行社
        /// </summary>
        public int TravelAgency
        {
            get;
            set;
        }
        /// <summary>
        /// 酒店
        /// </summary>
        public int Hotel
        {
            get;
            set;
        }
        /// <summary>
        /// 景区
        /// </summary>
        public int Sight
        {
            get;
            set;
        }
        /// <summary>
        /// 车队
        /// </summary>
        public int Car
        {
            get;
            set;
        }
        /// <summary>
        /// 购物点
        /// </summary>
        public int Shop
        {
            get;
            set;
        }
        /// <summary>
        /// 旅游用品
        /// </summary>
        public int Goods
        {
            get;
            set;
        }
        /// <summary>
        /// 用户
        /// </summary>
        public int User
        {
            get;
            set;
        }
        /// <summary>
        /// 供需信息
        /// </summary>
        public int Intermediary
        {
            get;
            set;
        }
        /// <summary>
        /// 有效线路
        /// </summary>
        public int Route
        {
            get;
            set;
        }
        /// <summary>
        /// MQ用户数
        /// </summary>
        public int MQUser
        {
            get;
            set;
        }
        /// <summary>
        /// 机票供应商
        /// </summary>
        public int TicketCompany
        {
            get;
            set;
        }
        /// <summary>
        /// 运价数
        /// </summary>
        public int TicketFreight
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟采购商
        /// </summary>
        public int BuyerVirtual
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟供应商
        /// </summary>
        public int SuppliersVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟供求数量
        /// </summary>
        public int SupplyInfosVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟旅行社
        /// </summary>
        public int TravelAgencyVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟酒店
        /// </summary>
        public int HotelVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟景区
        /// </summary>
        public int SightVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟车队
        /// </summary>
        public int CarVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟购物点
        /// </summary>
        public int ShopVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟旅游用品
        /// </summary>
        public int GoodsVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟用户
        /// </summary>
        public int UserVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟供需信息
        /// </summary>
        public int IntermediaryVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟有效线路
        /// </summary>
        public int RouteVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 城市线路虚拟基数
        /// </summary>
        public int CityRouteVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// MQ用户虚拟基数数
        /// </summary>
        public int MQUserVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟机票供应商
        /// </summary>
        public int TicketCompanyVirtual
        {
            get;
            set;
        }
        /// <summary>
        /// 虚拟运价数
        /// </summary>
        public int TicketFreightVirtual
        {
            get;
            set;
        }

        /// <summary>
        /// 实际景区数量
        /// </summary>
        public int Scenic
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟景区数量
        /// </summary>
        public int ScenicVirtual
        {
            get;
            set;
        }


    }
}
