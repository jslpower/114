using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 首页友情链接业务接口
    /// </summary>
    /// 周文超 2010-05-26
    public interface ISysFriendLink
    {
        /// <summary>
        /// 新增友情链接
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Add_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Add,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""LinkType"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Add_CODE)]
        int AddSysFriendLink(EyouSoft.Model.SystemStructure.SysFriendLink model);

        /// <summary>
        /// 修改友情链接
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Edit_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Edit,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""LinkType"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Edit_CODE)]
        int UpdateSysFriendLink(EyouSoft.Model.SystemStructure.SysFriendLink model);

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="FriendLinkId">友情链接ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Del_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Del,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""FriendLinkId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Del_CODE)]
        bool DeleteSysFriendLink(int FriendLinkId);

        /// <summary>
        /// 根据ID获取友情链接信息
        /// </summary>
        /// <param name="FriendLinkId">友情链接ID</param>
        /// <returns>友情链接实体</returns>
        EyouSoft.Model.SystemStructure.SysFriendLink GetSysFriendLinkModel(int FriendLinkId);

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="LinkType">链接类型</param>
        /// <returns>返回友情链接实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType LinkType);

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引:0/1添加时间升/降序</param>
        /// <param name="LinkType">类型</param>
        /// <returns>返回友情链接实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetSysFriendLinkList(int PageSize, int PageIndex
            , ref int RecordCount, int OrderIndex, EyouSoft.Model.SystemStructure.FriendLinkType? LinkType);

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="FriendLinkIds">友情链接ID集合</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Del_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Del,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""FriendLinkIds"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysFriendLink_Del_CODE)]
        bool DeleteSysFriendLink(int[] FriendLinkIds);
    }
}
