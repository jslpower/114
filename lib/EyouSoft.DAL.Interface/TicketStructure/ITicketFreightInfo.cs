using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 运价信息数据层接口
    /// </summary>
    /// 周文超 2010-10-25
    public interface ITicketFreightInfo
    {
        /// <summary>
        /// 添加运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>返回1：添加成功</returns>
        int AddFreightInfo(Model.TicketStructure.TicketFreightInfo model);

        /// <summary>
        /// 批量添加运价信息
        /// </summary>
        /// <param name="FreightInfoList">运价信息实体集合</param>
        /// <returns>返回1：添加成功</returns>
        int AddFreightInfo(IList<Model.TicketStructure.TicketFreightInfo> FreightInfoList);

        /// <summary>
        /// 修改运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>返回1：添加成功</returns>
        int UpdateFreightInfo(Model.TicketStructure.TicketFreightInfo model);

        /// <summary>
        /// 设置是否启用运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回true表示成功</returns>
        bool SetIsEnabled(string FreightInfoId, bool IsEnabled);

        /// <summary>
        /// 设置运价信息是否过期
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <param name="IsExpired">是否过期</param>
        /// <returns>返回true表示成功</returns>
        bool SetIsExpired(string FreightInfoId, bool IsExpired);

        /// <summary>
        /// 获取运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <returns>返回运价信息实体</returns>
        Model.TicketStructure.TicketFreightInfo GetModel(string FreightInfoId);

        /// <summary>
        /// 获取供应商的运价信息集合
        /// </summary>
        /// <param name="PageSize">当前页数</param>
        /// <param name="PageIndex">每页条数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引：0/1运价添加时间升/降序；2/3最后修改时间升/降序；4/5套餐类型升/降序；6/7启用状态升/降序；8/9航空公司编号升/降序；</param>
        /// <param name="QueryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        IList<Model.TicketStructure.TicketFreightInfo> GetList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, Model.TicketStructure.QueryTicketFreightInfo QueryTicketFreightInfo);

        /// <summary>
        /// 获取采购商查看运价信息集合
        /// </summary>
        /// <param name="QueryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        IList<Model.TicketStructure.TicketFreightInfoList> GetFreightInfoList(Model.TicketStructure.QueryTicketFreightInfo QueryTicketFreightInfo);
    }
}
