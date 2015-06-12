using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：我的客户接口
    /// </summary>
    public interface IMyCustomer
    {
        /// <summary>
        /// 获取我的客户列表
        /// </summary>
        /// <param name="currentCompanyId">当前登录人公司ID</param>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.MyCustomer> GetList(string currentCompanyId, EyouSoft.Model.CompanyStructure.QueryParamsCompany query, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 批量设置我的客户
        /// </summary>
        /// <param name="currUserId">当前操作的用户ID</param>
        /// <param name="customerCompanyId">要设置的客户ID</param>
        /// <returns></returns>
        bool SetMyCustomer(string currUserId, params string[] customerCompanyId);
        /// <summary>
        /// 批量取消我的客户
        /// </summary>
        /// <param name="listId">我的客户列表的编号</param>
        /// <returns></returns>
        bool CancelMyCustomer(params string[] listId);
    }
}
