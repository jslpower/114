using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 团队票接口
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public interface IGroupTickets
    {
        /// <summary>
        /// 申请团队票
        /// </summary>
        /// <param name="item">团队票实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddGroupTickets(EyouSoft.Model.TicketStructure.GroupTickets item);

        /// <summary>
        /// 单个/批量删除团队票信息
        /// </summary>
        /// <param name="ids">团队票编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool DeleteGroupTickets(string ids);

        /// <summary>
        /// 单个/批量审核团队票信息
        /// </summary>
        /// <param name="ids">团队票编号</param>
        /// <param name="isChecked">审核状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool ModifyIsChecked(string ids,bool isChecked);

        /// <summary>
        /// 根据编号获取团队票信息
        /// </summary>
        /// <param name="id">团队票编号</param>
        /// <returns>团队票实体</returns>
        EyouSoft.Model.TicketStructure.GroupTickets GetGroupTicket(int id);

        /// <summary>
        /// 分页获取团队票信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>团队票集合</returns>
        IList<EyouSoft.Model.TicketStructure.GroupTickets> GetGroupTickets(int pageSize, int pageIndex, ref int recordCount);

    }


}
