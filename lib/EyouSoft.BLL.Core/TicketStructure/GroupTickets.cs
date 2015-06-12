using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 团队票业务处理类
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class GroupTickets:EyouSoft.IBLL.TicketStructure.IGroupTickets
    {
        #region 私有变量
        private readonly EyouSoft.DAL.TicketStructure.GroupTickets dal = new EyouSoft.DAL.TicketStructure.GroupTickets();
        #endregion

        #region 构造函数
        public GroupTickets() { }

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.IGroupTickets CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.IGroupTickets op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.IGroupTickets>();
            }
            return op;
        }
        #endregion

        #region IGroupTickets 成员

        /// <summary>
        /// 申请团队票
        /// </summary>
        /// <param name="item">团队票实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddGroupTickets(EyouSoft.Model.TicketStructure.GroupTickets item)
        {
            if (item == null)
                return false;
            if (item.PelopeCount < 10)  //乘机人数必须大于等于10
                return false;
            return dal.AddGroupTickets(item);
        }
        /// <summary>
        /// 单个/批量删除团队票信息
        /// </summary>
        /// <param name="ids">团队票编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteGroupTickets(params int[] ids)
        {
            StringBuilder idsText = new StringBuilder();
            if (ids == null || ids.Length < 1)
                return false;
            for (int i = 0; i < ids.Length; i++)
            {
                idsText.Append(ids[i]);
                if (i + 1 < ids.Length)
                    idsText.Append(",");
            }

            return dal.DeleteGroupTickets(idsText.ToString());
        }
        /// <summary>
        /// 单个/批量审核团队票信息
        /// </summary>
        /// <param name="ids">团队票编号</param>
        /// <param name="isChecked">审核状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ModifyIsChecked(bool isChecked,params int[] ids)
        {
            StringBuilder idsText = new StringBuilder();
            if (ids == null || ids.Length < 1)
                return false;
            for (int i = 0; i < ids.Length; i++)
            {
                idsText.Append(ids[i]);
                if (i + 1 < ids.Length)
                    idsText.Append(",");
            }

            return dal.ModifyIsChecked(idsText.ToString(), isChecked);
        }
        /// <summary>
        /// 根据编号获取团队票信息
        /// </summary>
        /// <param name="id">团队票编号</param>
        /// <returns>团队票实体</returns>
        public EyouSoft.Model.TicketStructure.GroupTickets GetGroupTicket(int id)
        {
            return dal.GetGroupTicket(id);
        }
        /// <summary>
        /// 分页获取团队票信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>团队票集合</returns>
        public IList<EyouSoft.Model.TicketStructure.GroupTickets> GetGroupTickets(int pageSize, int pageIndex, ref int recordCount)
        {
            if (pageSize < 1 || pageIndex < 1)
                return null;
            return dal.GetGroupTickets(pageSize, pageIndex, ref recordCount);
        }

        #endregion
    }
}
