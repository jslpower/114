using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.ToolStructure
{
    /// <summary>
    /// 应收账款信息业务逻辑
    /// </summary>
    /// 周文超 2010-11-09
    public class Receivables : EyouSoft.IBLL.ToolStructure.IReceivables
    {

        private readonly IDAL.ToolStructure.IReceivables dal = ComponentFactory.CreateDAL<IDAL.ToolStructure.IReceivables>();

        /// <summary>
        /// 构造订单信息业务逻辑接口
        /// </summary>
        /// <returns>订单信息业务逻辑接口</returns>
        public static IBLL.ToolStructure.IReceivables CreateInstance()
        {
            IBLL.ToolStructure.IReceivables op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.ToolStructure.IReceivables>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Receivables()
        {

        }

        #region IReceivables 成员

        /// <summary>
        /// 添加应收账款信息
        /// </summary>
        /// <param name="model">应收账款信息实体</param>
        /// <returns></returns>
        public bool AddReceivables(EyouSoft.Model.ToolStructure.Receivables model)
        {
            if (model == null)
                return false;

            return dal.AddReceivables(model);
        }

        /// <summary>
        /// 根据ID获取对应的应收账款信息
        /// </summary>
        /// <param name="ReceivablesId">应收账款信息ID</param>
        /// <returns></returns>
        public EyouSoft.Model.ToolStructure.Receivables GetModel(string ReceivablesId)
        {
            if (string.IsNullOrEmpty(ReceivablesId))
                return null;

            return dal.GetModel(ReceivablesId);
        }

        /// <summary>
        /// 获取应收（已收）账款信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="TourNo">団号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="OperatorName">下单人</param>
        /// <param name="RetailersName">组团社名称</param>
        /// <param name="StartLeaveDate">开始出团时间</param>
        /// <param name="EndLeaveDate">结束出团时间</param>
        /// <param name="IsCheck">是否只查询已收</param>
        /// <param name="CompanyId">所属供应商编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.Receivables> GetList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string TourNo, string RouteName, string OperatorName, string RetailersName, DateTime? StartLeaveDate, DateTime? EndLeaveDate, bool IsCheck,string CompanyId)
        {
            return dal.GetList(PageSize, PageIndex, ref RecordCount, OrderIndex, TourNo, RouteName, OperatorName, RetailersName, StartLeaveDate, EndLeaveDate, IsCheck,CompanyId);
        }

        #endregion
    }
}
