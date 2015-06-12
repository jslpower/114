using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;
namespace EyouSoft.IBLL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-03
    /// 描述：高级网店基本信息业务逻辑接口
    /// </summary>
    public interface IHighShopList
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">高级网店基本信息实体</param>
        /// <returns>false:失败 true:成功</returns>
        //[CommonLogHandler(
        //LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShop_INSERT_TITLE,
        //LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShop_INSERT,
        //LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        //LogAttribute = @"[{""Index"":0,""Attribute"":""Id"",""AttributeType"":""class""}]",
        //EventCode = 0)]
        bool Add(EyouSoft.Model.ShopStructure.HighShopList model);
        /// <summary>
        /// 后台获取高级网店列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当期页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>高级网店列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopList> GetList(int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取高级网店实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>高级网店实体</returns>
        EyouSoft.Model.ShopStructure.HighShopList GetModel(string ID);
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="OperatorID">操作员ID</param>
        /// <param name="ExpireTime">网店到期时间</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
        LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_HighShop_CHECK_TITLE,
        LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_HighShop_CHECK,
        LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
        LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsCheck"",""AttributeType"":""val""}]",
        EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_HighShop_CHECK_CODE)]
        bool CheckInfo(string ID, bool IsCheck, string OperatorID, DateTime ExpireTime);
        /// <summary>
        /// 修改高级网店基本信息
        /// </summary>
        /// <param name="model">高级网店实体</param>
        /// <returns>false:失败 true:成功</returns>
        //[CommonLogHandler(
        //LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShop_INSERT_TITLE,
        //LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShop_INSERT,
        //LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        //LogAttribute = @"[{""Index"":0,""Attribute"":""Id"",""AttributeType"":""class""}]",
        //EventCode = 0)]
        bool UpdateBasicInfo(EyouSoft.Model.ShopStructure.HighShopList model);
        /// <summary>
        /// 获取快到期的高级网店
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ProvinceId">所在省份ID >0返回指定省份 =0返回全部</param>
        /// <param name="CityId">所在城市ID >0返回指定城市 =0返回全部</param>
        /// <param name="CompanyName">公司名称 模糊匹配</param>
        /// <param name="StartExpireDate">到期日期起始值 不需要该参数时传入null</param>
        /// <param name="EndExpireDate">到期时间结束值 不需要该参数时传入null</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ShopStructure.HighShopList> GetExpireList(int pageSize, int pageIndex, ref int recordCount, int ProvinceId, int CityId, string CompanyName, DateTime? StartExpireDate, DateTime? EndExpireDate);

    }
}
