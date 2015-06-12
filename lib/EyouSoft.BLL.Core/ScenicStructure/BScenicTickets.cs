using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.BLL.ScenicStructure
{
    /// <summary>
    /// 景区门票
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public class BScenicTickets:EyouSoft.IBLL.ScenicStructure.IScenicTickets
    {
        private readonly EyouSoft.IDAL.ScenicStructure.IScenicTickets dal =
            EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ScenicStructure.IScenicTickets>();

        public static IBLL.ScenicStructure.IScenicTickets CreateInstance()
        {
            IBLL.ScenicStructure.IScenicTickets op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<IBLL.ScenicStructure.IScenicTickets>();
            }
            return op;
        }

        #region IScenicTickets 成员
        /// <summary>
        /// 添加门票(用户后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(MScenicTickets item)
        {
            bool result = false;
            if (item != null)
            {
                item.TicketsId = Guid.NewGuid().ToString();
                //默认待审核
                item.ExamineStatus = ExamineStatus.待审核;
                //B2B,B2C排序默认50
                item.B2BOrder = 50;
                item.B2COrder = 50;
                result = dal.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 添加门票(运营后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool OperateAdd(MScenicTickets item)
        {
            bool result = false;
            if (item != null)
            {
                item.TicketsId = Guid.NewGuid().ToString();
                result = dal.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 修改门票(价格一修改，则状态改为待审核)(用户后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(MScenicTickets item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item, false);
            }
            return result;
        }
        /// <summary>
        /// 修改门票(运营后台)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool OperateUpdate(MScenicTickets item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item, true);
            }
            return result;
        }
        /// <summary>
        /// 删除门票(待审核状态才可以删除)
        /// </summary>
        /// <param name="ticketsId">门票编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool Delete(string ticketsId, string companyId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(ticketsId) && !string.IsNullOrEmpty(companyId))
            {
                result = dal.Delete(ticketsId, companyId);
            }
            return result;
        }
        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="tickets">门票编号</param>
        /// <param name="operatorId">审核用户编号</param>
        /// <param name="es">审核状态</param>
        /// <returns></returns>
        public bool UpdateExamineStatus(string tickets, int operatorId, ExamineStatus es)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(tickets) && operatorId > 0)
            {
                result = dal.UpdateExamineStatus(tickets, operatorId, es);
            }
            return result;
        }
        /// <summary>
        /// 获取门票实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="ticketsId">门票编号</param>
        /// <returns>门票实体</returns>
        public MScenicTickets GetModel(string ticketsId, string companyId)
        {
            MScenicTickets item = null;

            if (!string.IsNullOrEmpty(ticketsId) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(ticketsId, companyId);
            }

            return item;
        }

        /// <summary>
        /// 获取特价门票
        /// </summary>
        /// <param name="topNum">指定获取数量</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MScenicTicketsSale> GetList(int topNum,MSearchScenicTicketsSale search)
        {
            topNum = topNum < 1 ? 10 : topNum;
            return dal.GetList(topNum,search);
        }
        /// <summary>
        /// 门票列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>门票列表</returns>
        public IList<MScenicTickets> GetList(int pageSize, int pageIndex, ref int recordCount, MSearchScenicTickets search)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;

            return dal.GetList(pageSize, pageIndex, ref recordCount, search);
        }

        /// <summary>
        /// 到期时间还有1周
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetExpireTickets(string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetExpireTickets(companyId);
            }
            return 0;
        }

        #endregion
    }
}
