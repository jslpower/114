using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.OnLineServer
{
    /// <summary>
    /// 在线客服数据访问接口
    /// </summary>
    public interface IOLServer
    {
        /// <summary>
        /// 获取默认的客服信息(服务人数最少的客服为默认的客服) 返回null时没有在线客服
        /// </summary>
        /// <param name="CompanyId">公司Id</param>
        /// <returns></returns>
        Model.OnLineServer.OlServerUserInfo GetDefaultServiceInfo(string CompanyId);

        /// <summary>
        /// 根据指定的在线编号获取在线用户信息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        Model.OnLineServer.OlServerUserInfo GetUserInfoByOlId(string olId);

        /// <summary>
        /// 根据指定的用户编号获取在线用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        Model.OnLineServer.OlServerUserInfo GetUserInfoByUserId(string userId, bool isService);

        /// <summary>
        /// 获取所有客服信息（不区分是否在线）
        /// </summary>
        /// <param name="OlId">在线Id</param>
        /// <returns></returns>
        IList<Model.OnLineServer.OlServerUserInfo> GetServicesInfo(string OlId);

        /// <summary>
        /// 根据指定的在线客服编号获取所有在线用户信息
        /// </summary>
        /// <param name="olServiceId">在线客服编号</param>
        /// <returns></returns>
        IList<Model.OnLineServer.OlServerUserInfo> GetOnlineUsersInfo(string olServiceId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns></returns>
        bool UpdateUserInfo(Model.OnLineServer.OlServerUserInfo info);

        /// <summary>
        /// 插入用户信息，返回当前公司游客总数
        /// </summary>
        /// <param name="info">用户信息</param>
        int InserUserInfo(Model.OnLineServer.OlServerUserInfo info);

        /// <summary>
        /// 设置客服
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="serviceId">客服在线编号</param>
        /// <param name="serviceName">客服在线名称</param>
        /// <returns></returns>
        bool SetService(string olId, string serviceId, string serviceName);

        /// <summary>
        /// 写入消息,返回写入的消息编号
        /// </summary>
        /// <param name="messageInfo">Model.OnLineServer.OlServerMessageInfo</param>
        /// <returns></returns>
        bool InsertMessage(Model.OnLineServer.OlServerMessageInfo messageInfo);

        /// <summary>
        /// 根据最后发送消息时间获取最后发送消息时间以后发给自己的所有消息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="LastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        IList<Model.OnLineServer.OlServerMessageInfo> GetMessages(string olId, DateTime? LastSendMessageTime);

        /// <summary>
        /// 获取登录后所有消息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        IList<Model.OnLineServer.OlServerMessageInfo> GetMessages(string olId);

        /// <summary>
        /// 根据指定的在线编号设置在线状态为false[用户退出]
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        bool Exit(string olId);

        /// <summary>
        /// 客服退出时设置与之会话的客户的默认服务客服信息为无
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        bool ExitSetDefaultService(string olId);

        /// <summary>
        /// 根据指定在线编号设置用户最后发送消息时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="lastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        bool SetLastSendMessageTime(string olId, DateTime lastSendMessageTime);

        /// <summary>
        /// 清理过了指定停留时间的用户在线状态为不在线（不含客服人员）
        /// </summary>
        /// <param name="stayTime">停留时间 单位：分钟</param>
        /// <returns></returns>
        bool ClearUserOut(int stayTime);

        /// <summary>
        /// 按条件分页获得聊天记录
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="LikeInfo">消息内容包含的文字</param>
        /// <param name="pageSize">单页显示的数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordSum">总记录数量</param>
        /// <returns></returns>
        IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> GetMessages(DateTime? StartTime, DateTime? EndTime, string LikeInfo, int pageSize, int pageIndex, ref int recordSum);

        /// <summary>
        /// 根据id删除聊天记录
        /// </summary>
        /// <param name="MessagesId">聊天记录ID</param>
        /// <returns></returns>
        bool DeleteMessages(string MessagesId);

        /// <summary>
        /// 根据UserId删除在线客服用户信息
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="IsService">是否为客服</param>
        /// <returns></returns>
        bool DeleteUserInfoByUserId(string UserId, bool IsService);

        /// <summary>
        /// 根据在线编号获取此在线用户最后发送消息的时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        DateTime? GetLastSendMessageTime(string olId);

        /// <summary>
        /// 根据用户Id获取该用户所有未读消息的记录数
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        int GetMessageCountByUserId(string UserId);
    }
}
