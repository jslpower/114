using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-Interface for the CreditLetter BLL
    /// </summary>
    public interface ICreditLetter
    {
        /// <summary>
        /// 新增服务承诺书信息
        /// </summary>
        /// <param name="info">服务承诺书业务实体</param>
        /// <returns>int 0:操作失败 1:操作成功 2:已存在该公司服务承诺书</returns>
        int Insert(EyouSoft.Model.CreditSystemStructure.CreditLetter info);

        /// <summary>
        /// 根据指定的公司编号更新服务承诺书信息
        /// </summary>
        /// <param name="info">服务承诺书业务实体</param>
        /// <returns></returns>
        bool UpdateByCompanyId(EyouSoft.Model.CreditSystemStructure.CreditLetter info);

        /// <summary>
        /// 根据指定的承诺书编号更新服务承诺书信息
        /// </summary>
        /// <param name="info">服务承诺书业务实体</param>
        /// <returns>int 0:操作失败 1:操作成功 2:已存在该公司服务承诺书</returns>
        int UpdateByCreditId(EyouSoft.Model.CreditSystemStructure.CreditLetter info);

        /// <summary>
        /// 根据指定的公司编号获取服务承诺书状态,0:未审核 1:通过审核 2:未通过审核 3:失效 -1:没有任务服务承诺服务书信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>int 0:未审核 1:通过审核 2:未通过审核 3:失效 -1:没有任务服务承诺服务书信息</returns>
        int GetCreditState(int companyId);

        /// <summary>
        /// 根据指定的公司编号获取服务承诺书信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.CreditSystemStructure.CreditLetter GetCreditInfoByCompanyId(int companyId);

        /// <summary>
        /// 根据指定的服务承诺书编号获取服务承诺书信息
        /// </summary>
        /// <param name="creditId">服务承诺书编号</param>
        /// <returns></returns>
        EyouSoft.Model.CreditSystemStructure.CreditLetter GetCreditInfoByCreditId(string creditId);

        /// <summary>
        /// 根据指定条件分页获取服务承诺书信息
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="unionId">联盟编号</param>
        /// <param name="provinceId">省份编号 设置成0时不做为查询条件</param>
        /// <param name="siteId">分站编号 设置成0时不做为查询条件</param>
        /// <param name="state">服务承诺书状态 0:未审核 1:通过审核 2:未通过审核 3:失效 设置成区间[0,3]以外的值时不做为查询条件</param>
        /// <param name="companyName">公司名称 设置成string.Empty或null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CreditSystemStructure.CreditLetter> GetCredits(int pageSize, int pageIndex, ref int recordCount, int unionId, int provinceId, int siteId, int state, string companyName);

        /// <summary>
        /// 根据指定的承诺书编号、承诺书状态更新承诺书状态
        /// </summary>
        /// <param name="state">状态 0:未审核 1:通过审核 2:未通过审核 3:失效</param>
        /// <param name="creditId">服务承诺书编号</param>        
        /// <returns></returns>
        bool SetCreditState(int state, string creditId);

        /// <summary>
        /// 根据指定的公司编号、承诺书状态更新承诺书状态
        /// </summary>
        /// <param name="state">状态 0:未审核 1:通过审核 2:未通过审核 3:失效</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool SetCreditState(int state, int companyId);

        /// <summary>
        /// 根据指定的承诺书编号删除承诺书信息
        /// </summary>
        /// <param name="creditId">承诺书编号</param>
        /// <returns></returns>
        bool Delete(string creditId);
    }
}
