using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    /// 机票折扣申请
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public interface ITicketApply
    {
        /// <summary>
        /// 获得机票折扣申请实体
        /// </summary>
        /// <param name="id">申请ID</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketApply GetModel(string id);
        /// <summary>
        /// 添加机票折扣申请
        /// </summary>
        /// <param name="model">机票折扣信息实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.TicketStructure.TicketApply model);
        /// <summary>
        /// 删除机票折扣申请
        /// </summary>
        /// <param name="idList">申请的ID串</param>
        /// <returns></returns>
        bool Delete(params string[] idList);
        /// <summary>
        /// 修改机票折扣申请
        /// </summary>
        /// <param name="model">机票折扣信息实体</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.TicketStructure.TicketApply model);
        /// <summary>
        /// 获得申请的列表
        /// </summary>
        /// <param name="query">查询实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketApply> GetList(EyouSoft.Model.TicketStructure.QueryTicketApply query , int pageSize, int pageIndex, ref int recordCount);
        void Test();
    }
}
