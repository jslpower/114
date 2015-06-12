using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 运价套餐购买记录接口
    /// </summary>
    /// 鲁功源 2010-10-25
    public interface IFreightBuyLog
    {
        /// <summary>
        /// 添加套餐购买记录
        /// </summary>
        /// <param name="model">运价套餐购买记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.TicketStructure.TicketFreightBuyLog model);
        /// <summary>
        /// 分页获取套餐购买记录列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">购买公司编号 =""返回全部</param>
        /// <param name="PackageType">套餐类型 =null返回全部</param>
        /// <param name="RateType">运价类型 =null返回全部</param>
        /// <param name="FlightId">航空公司编号 =0返回全部</param>
        /// <param name="HomeCityId">起始地编号 =0返回全部</param>
        /// <param name="DestCityId">目的地编号 =0返回全部</param>
        /// <param name="StartMonth">起始日期 =null返回全部</param>
        /// <param name="EndMonth">结束日期 =null返回全部</param>
        /// <param name="PayState">支付状态 =null返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketFreightBuyLog> GetList(int pageSize, int pageIndex, ref int recordCount,string CompanyId,
            EyouSoft.Model.TicketStructure.PackageTypes? PackageType, EyouSoft.Model.TicketStructure.RateType? RateType,
            int FlightId, int HomeCityId, int DestCityId, DateTime? StartMonth, DateTime? EndMonth, bool? PayState);
        /// <summary>
        /// 获取套餐购买记录实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>套餐购买记录实体</returns>
        EyouSoft.Model.TicketStructure.TicketFreightBuyLog GetModel(string Id);
        /// <summary>
        /// 获取套餐购买记录实体
        /// </summary>
        /// <param name="Id">订单编号</param>
        /// <returns>套餐购买记录实体</returns>
        EyouSoft.Model.TicketStructure.TicketFreightBuyLog GetModelByOrderNo(string OrderNo);
        /// <summary>
        /// 设置支付信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="PayState">支付状态</param>
        /// <param name="PayTime">支付时间</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetPayInfo(string Id, bool PayState, DateTime PayTime);
        /// <summary>
        /// 获取指定购买公司的可用套餐集合
        /// </summary>
        /// <param name="StartDate">启用时间</param>
        /// <param name="CompanyId">购买公司编号</param>
        /// <param name="HomeCityId">起始地编号</param>
        /// <param name="RateType">业务类型</param>
        IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> GetAvailableInfo(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType,int HomeCityId);
        /// <summary>
        /// 获取指定购买公司的可用套餐总数
        /// </summary>
        /// <param name="StartDate">启用时间</param>
        /// <param name="CompanyId">购买公司编号</param>
        /// <param name="RateType">业务类型</param>
        int GetAvailableNum(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType);
        /// <summary>
        /// 设置运价套餐可用条数[运价添加完成后执行]
        /// </summary>
        /// <param name="list">运价套餐使用记录列表</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetAvailableNum(IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list);
        /// <summary>
        /// 获取供应商的套餐购买记录统计信息
        /// </summary>
        /// <param name="StartDate">启用时间</param>
        /// <param name="CompanyId">购买公司编号</param>
        /// <param name="RateType">业务类型</param>
        /// <returns>套餐购买记录统计信息实体</returns>
        EyouSoft.Model.TicketStructure.PackBuyLogStatistics GetPackBuyLog(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType);
        /// <summary>
        /// 根据运价启用状态与主键编号，设置购买记录可用数
        /// </summary>
        /// <param name="Id">主键编号[对应运价表中的，套餐购买编号]</param>
        /// <param name="FreightEnabled">运价启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetAvailableByFreightState(string Id, bool FreightEnabled);
    }
}
