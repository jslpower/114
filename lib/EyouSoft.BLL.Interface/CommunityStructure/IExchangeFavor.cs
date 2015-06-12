using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;
namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-16
    /// 描述：供求收藏业务层接口
    /// </summary>
    public interface IExchangeFavor
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供求收藏实体</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
        LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_INSERT_TITLE,
        LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_INSERT,
        LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
        EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_INSERT_CODE)]
        bool Add(EyouSoft.Model.CommunityStructure.ExchangeBase model);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号字符（，分割）</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
        LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_DELETE_TITLE,
        LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_DELETE,
        LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        LogAttribute = @"[{""Index"":0,""Attribute"":""Ids"",""AttributeType"":""array""}]",
        EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_DELETE_CODE)]
        bool Delete(string[] Ids);
        /// <summary>
        /// 分页获取所有供求收藏信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetList(int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 分页获取指定公司的所有供求收藏信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetListbyCompanyID(int pageSize, int pageIndex, ref int recordCount, string CompanyId);
        /// <summary>
        /// 分页获取指定用户的所有供求信息收藏列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="UserId">用户编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetListByUserID(int pageSize, int pageIndex, ref int recordCount, string UserId);
    }
}
