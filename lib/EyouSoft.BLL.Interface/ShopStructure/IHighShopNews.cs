using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler; 
namespace EyouSoft.IBLL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-03
    /// 描述：最新旅游动态业务逻辑接口
    /// </summary>
    public interface IHighShopNews
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">旅游动态实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_INSERT_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_INSERT,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_INSERT_CODE)]
        bool Add(EyouSoft.Model.ShopStructure.HighShopNews model);
        /// <summary>
        /// 修改旅游动态
        /// </summary>
        /// <param name="model">旅游动态实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_UPDATE_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_UPDATE,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_UPDATE_CODE)]
        bool Update(EyouSoft.Model.ShopStructure.HighShopNews model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_DELETE_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_DELETE,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_DELETE_CODE)]
        bool Delete(string ID);
        /// <summary>
        /// 获取旅游动态实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>存在返回实体类 不存在返回NUll</returns>
        EyouSoft.Model.ShopStructure.HighShopNews GetModel(string ID);
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">置顶状态 TRUE或者FALSE</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_SETTOP_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_SETTOP,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsTop"",""AttributeType"":""val""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopNews_SETTOP_CODE)]
        bool SetTop(string ID, bool IsTop);
        /// <summary>
        /// 获取前台页面旅游动态列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="KeyWord">关键字</param>
        /// <returns>旅游动态列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopNews> GetWebList(int pageSize,int pageIndex,ref int recordCount, string CompanyID,string KeyWord);
        /// <summary>
        /// 获取指定公司指定条数的记录
        /// </summary>
        /// <param name="TopNumber">需要返回的记录条数 =0返回全部 >0返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>旅游动态列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopNews> GetTopNumberList(int TopNumber, string CompanyID);
    }
}
