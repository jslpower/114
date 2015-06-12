using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HotelStructure
{
    /// <summary>
    /// 酒店订单数据层接口
    /// </summary>
    /// 鲁功源  2010-12-01
    public interface IHotelOrder
    {
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="model">酒店订单实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.HotelStructure.OrderInfo model);
        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="ResOrderId">订单号</param>
        /// <param name="OrderState">订单状态[枚举]</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetOrderState(string ResOrderId,  EyouSoft.HotelBI.HBEResStatus OrderState);
        /// <summary>
        /// 设置订单审核状态
        /// </summary>
        /// <param name="ResOrderId">订单号</param>
        /// <param name="CheckState">审核状态[枚举]</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetCheckState(string ResOrderId, EyouSoft.Model.HotelStructure.CheckStateList CheckState);
        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="ResOrderId">订单号</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.OrderInfo GetOrderInfo(string ResOrderId);
        /// <summary>
        /// 分页获取酒店订单
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">查询实体</param>
        /// <returns>酒店订单列表</returns>
        IList<EyouSoft.Model.HotelStructure.OrderInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo);
    }
}
