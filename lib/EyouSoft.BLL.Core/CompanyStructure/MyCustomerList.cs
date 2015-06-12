using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 我的客户业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public class MyCustomer : EyouSoft.IBLL.CompanyStructure.IMyCustomer
    {
        private readonly EyouSoft.IDAL.CompanyStructure.IMyCustomer idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.IMyCustomer>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.IMyCustomer CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.IMyCustomer op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.IMyCustomer>();
            }
            return op;
        }

        /// <summary>
        /// 获取我的客户列表
        /// </summary>
        /// <param name="currentCompanyId">当前登录人公司ID</param>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.MyCustomer> GetList(string currentCompanyId, EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount)
        {
            return idal.GetList(currentCompanyId, query, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 批量设置我的客户
        /// </summary>
        /// <param name="currUserId">当前操作的用户ID</param>
        /// <param name="customerCompanyId">要设置的客户ID</param>
        /// <returns></returns>
        public bool SetMyCustomer(string currUserId, params string[] customerCompanyId)
        {
            return idal.SetMyCustomer(currUserId, customerCompanyId);
        }
        /// <summary>
        /// 批量取消我的客户
        /// </summary>
        /// <param name="listId">我的客户列表的编号</param>
        /// <returns></returns>
        public bool CancelMyCustomer(params string[] listId)
        {
            return idal.CancelMyCustomer(listId);
        }
    }
}
