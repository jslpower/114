using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.HotelStructure
{
    /// <summary>
    /// 酒店订单业务层接口
    /// </summary>
    /// 鲁功源  2010-12-01
    public interface IHotelOrder
    {  
        /// <summary>
        /// 分页获取酒店订单
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">查询实体</param>
        /// <returns>酒店订单列表</returns>
        IList<EyouSoft.Model.HotelStructure.OrderInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo);
        /// <summary>
        /// 添加订单，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="model">酒店订单实体</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        int Add(EyouSoft.Model.HotelStructure.OrderInfo model, out string errorDesc);
        /// <summary>
        /// 取消订单，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="cancelReason">取消原因</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns>正值：成功  负值或0失败</returns>
        int Cancel(string resOrderId, string cancelReason,out string errorDesc);
        /// <summary>
        /// 获取订单信息业务实体
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="errorCode">错误代码 正值：成功 负值或0：失败</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.OrderInfo GetInfo(string resOrderId, out int errorCode, out string errorDesc);
        /// <summary>
        /// 执行酒店订单异步通知指令，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="hint">指令内容</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        int ExecOrderHint(string hint);
        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        bool SetOrderState(string resOrderId, EyouSoft.HotelBI.HBEResStatus orderState);
        /// <summary>
        /// 设置订单审核状态
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="checkState">订单审核状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetCheckState(string resOrderId, EyouSoft.Model.HotelStructure.CheckStateList checkState);
    }
}
