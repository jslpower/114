using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 特价机票接口
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public interface ISpecialFares
    {
        /// <summary>
        /// 添加特价机票信息
        /// </summary>
        /// <param name="item">特价机票实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddSpecialFares(EyouSoft.Model.TicketStructure.SpecialFares item);

        /// <summary>
        /// 修改特价机票信息
        /// </summary>
        /// <param name="item">特价机票实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        bool ModifySpecialFares(EyouSoft.Model.TicketStructure.SpecialFares item);

        /// <summary>
        /// 单个/批量删除特价机票信息
        /// </summary>
        /// <param name="ids">特价机票编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool DeleteSpecialFares(string ids);

        /// <summary>
        /// 根据编号获取特价机票信息
        /// </summary>
        /// <param name="id">特价机票编号</param>
        /// <returns>特价机票实体对象</returns>
        EyouSoft.Model.TicketStructure.SpecialFares GetSpecialFare(int id);

        /// <summary>
        /// 指定条数获取特价机票信息
        /// </summary>
        /// <param name="topNum">获取数量</param>
        /// <returns>特价机票集合</returns>
        IList<EyouSoft.Model.TicketStructure.SpecialFares> GetTopSpecialFares(int topNum);

        /// <summary>
        /// 分页获取特价机票信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>特价机票集合</returns>
        IList<EyouSoft.Model.TicketStructure.SpecialFares> GetSpecialFares(int pageSize, int pageIndex, ref int recordCount);
    }
}
