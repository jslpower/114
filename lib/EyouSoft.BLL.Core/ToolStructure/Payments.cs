using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
namespace EyouSoft.BLL.ToolStructure
{
    /// <summary>
    /// 应付账款信息业务逻辑
    /// </summary>
    /// 鲁功源 2010-11-09
    public class Payments:EyouSoft.IBLL.ToolStructure.IPayments
    {
        private readonly IDAL.ToolStructure.IPayments dal = ComponentFactory.CreateDAL<IDAL.ToolStructure.IPayments>();

        /// <summary>
        /// 构造应付账款信息业务逻辑接口
        /// </summary>
        /// <returns>应付账款信息业务逻辑接口</returns>
        public static IBLL.ToolStructure.IPayments CreateInstance()
        {
            IBLL.ToolStructure.IPayments op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.ToolStructure.IPayments>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Payments()
        {

        }


        #region IPayments 成员
        /// <summary>
        /// 添加应付款信息
        /// </summary>
        /// <param name="model">应付款信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddPayments(EyouSoft.Model.ToolStructure.Payments model)
        {
            if (model == null)
                return false;
            return dal.AddPayments(model);
        }
        /// <summary>
        /// 获取应付款信息实体
        /// </summary>
        /// <param name="PaymentId">应付款信息编号</param>
        /// <returns>应付款信息实体</returns>
        public EyouSoft.Model.ToolStructure.Payments GetModel(string PaymentId)
        {
            if (string.IsNullOrEmpty(PaymentId))
                return null;
            return dal.GetModel(PaymentId);
        }
        /// <summary>
        /// 获取应付账款信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="TourNo">団号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="SupperName">供应商名称</param>
        /// <param name="SupperType">供应商类型</param>
        /// <param name="StartLeaveDate">开始出团时间</param>
        /// <param name="EndLeaveDate">结束出团时间</param>
        /// <param name="IsCheck">是否只查询已付</param>
        /// <param name="CompanyId">所属供应商编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.Payments> GetList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string TourNo, string RouteName, string SupperName, string SupperType, DateTime? StartLeaveDate, DateTime? EndLeaveDate, bool IsCheck,string CompanyId)
        {
            return dal.GetList(PageSize, PageIndex, ref RecordCount, OrderIndex, TourNo, RouteName, SupperName, SupperType, StartLeaveDate, EndLeaveDate, IsCheck,CompanyId);
        }

        #endregion
    }
}
