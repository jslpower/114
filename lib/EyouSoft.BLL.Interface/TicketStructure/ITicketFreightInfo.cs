using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    /// 运价信息数据层接口
    /// </summary>
    /// Author:罗丽娥  2010-10-29
    public interface ITicketFreightInfo
    {
        /// <summary>
        /// 添加运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(Model.TicketStructure.TicketFreightInfo model);

        /// <summary>
        /// 批量添加运价信息
        /// </summary>
        /// <param name="list">运价信息实体集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(IList<Model.TicketStructure.TicketFreightInfo> list);

        /// <summary>
        /// 修改运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(Model.TicketStructure.TicketFreightInfo model);

        /// <summary>
        /// 复制运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价ID</param>
        /// <returns>true:成功 false:失败</returns>
        bool Copy(string FreightInfoId);

        /// <summary>
        /// 获取运价实体
        /// </summary>
        /// <param name="FreightInfoId">运价ID</param>
        /// <returns>运价信息实体</returns>
        Model.TicketStructure.TicketFreightInfo GetModel(string FreightInfoId);

        /// <summary>
        /// 运价维护列表
        /// </summary>
        /// <param name="pageSize">当前页数</param>
        /// <param name="pageIndex">每页条数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="orderIndex">排序索引</param>
        /// <param name="queryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        IList<Model.TicketStructure.TicketFreightInfo> GetList(int pageSize, int pageIndex, ref int recordCount, int orderIndex, Model.TicketStructure.QueryTicketFreightInfo queryTicketFreightInfo);

        /// <summary>
        /// 设置运价启用状态
        /// </summary>
        /// <param name="FreightInfoId">运价ID</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetIsEnabled(string FreightInfoId, bool IsEnabled);

        /// <summary>
        /// 获取采购商运价信息集合
        /// </summary>
        /// <param name="queryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        IList<Model.TicketStructure.TicketFreightInfoList> GetFreightInfoList(Model.TicketStructure.QueryTicketFreightInfo queryTicketFreightInfo);
    }
}
