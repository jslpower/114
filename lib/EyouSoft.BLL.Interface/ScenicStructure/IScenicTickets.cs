using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.IBLL.ScenicStructure
{
    /// <summary>
    /// 景区门票
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public interface IScenicTickets
    {
        /// <summary>
        /// 添加门票(用户后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Add(MScenicTickets item);

        /// <summary>
        /// 添加门票(运营后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool OperateAdd(MScenicTickets item);

        /// <summary>
        /// 修改门票(价格一修改，则状态改为待审核)(用户后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Update(MScenicTickets item);

        /// <summary>
        /// 修改门票(运营后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool OperateUpdate(MScenicTickets item);
        /// <summary>
        /// 删除门票(待审核状态才可以删除)
        /// </summary>
        /// <param name="ticketsId">门票编号</param>
        /// <param name="operatorId">审核用户</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool Delete(string ticketsId, string companyId);
        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="tickets">门票编号</param>
        /// <param name="operatorId">审核用户编号</param>
        /// <param name="es">审核状态</param>
        /// <returns></returns>
        bool UpdateExamineStatus(string tickets, int operatorId, ExamineStatus es);
        /// <summary>
        /// 获取门票实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="ticketsId">门票编号</param>
        /// <returns>门票实体</returns>
        MScenicTickets GetModel(string ticketsId, string companyId);

        /// <summary>
        /// 获取特价门票
        /// </summary>
        /// <param name="topNum">指定获取数量</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MScenicTicketsSale> GetList(int topNum, MSearchScenicTicketsSale search);
        /// <summary>
        /// 门票列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>门票列表</returns>
        IList<MScenicTickets> GetList(int pageSize, int pageIndex, ref int recordCount, MSearchScenicTickets search);
        /// <summary>
        /// 到期时间还有1周
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetExpireTickets(string companyId);
    }
}
