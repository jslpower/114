using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-01
    /// 描述：单位采购目录业务层接口
    /// </summary>
    public interface ICompanyFavor
    {
        #region ICompanyFavor成员
        /// <summary>
        /// 获取公司采购目录被收藏公司的区域编号集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.AreaBase> GetAllFavorArea(string CompanyId);
        /// <summary>
        /// 获取公司采购目录总数
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        int GetAllFavorCount(string CompanyId);
        /// <summary>
        /// 获取指定公司指定线路区域下的采购目录总数
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AreaId">线路区域编号 =0时返回全部</param>
        /// <returns></returns>
        int GetAllFavorCount(string CompanyId, int AreaId);
        /// <summary>
        /// 获取指定公司指定线路区域下的被收藏公司的编号集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<string> GetListByCompanyId(string CompanyId);
        /// <summary>
        /// 获取当前登录人所在公司收藏的公司信息
        /// </summary>
        /// <param name="currentCompanyId">公司编号</param>
        /// <returns></returns>
        /// 开发人:张志瑜   开发时间:2010-7-12
        IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListCompany(string currentCompanyId);

        /// <summary>
        /// 保存采购目录
        /// </summary>
        /// <param name="model">采购目录列表集合</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_CompanyFavor_SetFavor_Title,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_CompanyFavor_SetFavor,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""FavorCompanyId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_CompanyFavor_SetFavor_CODE)]
        bool SaveCompanyFavor(EyouSoft.Model.CompanyStructure.CompanyFavor model);

        /// <summary>
        /// 删除指定公司的指定区域的采购目录
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="FavorCompanyId">被收藏公司编号</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_CompanyFavor_CancelFavor_Title,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_CompanyFavor_CancelFavor,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":1,""Attribute"":""FavorCompanyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_CompanyFavor_CancelFavor_CODE)]
        bool Delete(string CompanyID, string FavorCompanyId);
        #endregion
    }
}
