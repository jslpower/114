using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;
namespace EyouSoft.IBLL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-03
    /// 描述：友情链接业务逻辑接口
    /// </summary>
    public interface IHighShopFriendLink
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_INSERT_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_INSERT,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_INSERT_CODE)]
        bool Add(EyouSoft.Model.ShopStructure.HighShopFriendLink model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_UPDATE_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_UPDATE,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_UPDATE_CODE)]
        bool Update(EyouSoft.Model.ShopStructure.HighShopFriendLink model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
       LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_DELETE_TITLE,
       LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_DELETE,
       LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
       LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""}]",
       EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_HighShopFriendLink_DELETE_CODE)]
        bool Delete(string ID);
        /// <summary>
        /// 获取指定公司的友情链接列表
        /// </summary>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的记录</param>
        /// <returns>友情链接列表</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> GetList(string CompanyID);
    }
}
