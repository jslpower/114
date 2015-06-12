using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.HotelStructure
{
    /// <summary>
    /// 酒店-团队定制业务层接口
    /// </summary>
    /// 鲁功源  2010-12-02
    public interface IHotelTourCustoms
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">团队定制实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.HotelStructure.HotelTourCustoms model);
        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="model">团队定制回复实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddAsk(EyouSoft.Model.HotelStructure.HotelTourCustomsAsk model);
        /// <summary>
        /// 获取团队定制实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>团队预定实体</returns>
        EyouSoft.Model.HotelStructure.HotelTourCustoms GetModel(int Id);
        /// <summary>
        /// 获取团队定制列表
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">团队定制查询实体</param>
        /// <returns>团队定制列表</returns>
        IList<EyouSoft.Model.HotelStructure.HotelTourCustoms> GetList(int PageSize,int PageIndex,ref int RecordCount,EyouSoft.Model.HotelStructure.SearchTourCustomsInfo SearchInfo);
        /// <summary>
        /// 获取团队定制回复列表
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TourOrderId">团队定制订单ID</param>
        /// <returns>团队定制回复列表</returns>
        IList<EyouSoft.Model.HotelStructure.HotelTourCustomsAsk> GetListAsk(int PageSize, int PageIndex, ref int RecordCount, int TourOrderId);
        /// <summary>
        /// 设置处理状态
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="TreatState">处理状态</param>
        /// <param name="OperatorId">操作人编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetTreatState(int Id, EyouSoft.Model.HotelStructure.OrderStateList TreatState, int OperatorId);
    }
}
