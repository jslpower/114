using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.OnLineServer
{
    /// <summary>
    /// 在线客服业务逻辑接口
    /// </summary>
    public interface IOLServer
    {
        /// <summary>
        /// 未登录游客进入在线客服
        /// </summary>
        /// <param name="olCookieInfo">进入在线客服的cookie信息</param>
        /// <param name="CompanyId">高级网店公司Id</param>
        /// <returns></returns>
        EyouSoft.Model.OnLineServer.OlServerUserInfo GuestLogin(EyouSoft.Model.OnLineServer.OlServerUserInfo olCookieInfo
            , string CompanyId);

        /// <summary>
        /// 登录游客/客服进入在线客服
        /// </summary>
        /// <param name="userInfo">登录网站的用户信息</param>
        /// <param name="isService">是否客服</param>
        /// <param name="CompanyId">高级网店公司Id</param>
        /// <returns></returns>
        EyouSoft.Model.OnLineServer.OlServerUserInfo ServiceLogin(EyouSoft.SSOComponent.Entity.UserInfo userInfo, bool isService, string CompanyId);

        /// <summary>
        /// 获取在线用户,是客服将获取正在服务的客户,非客服将获取所有客服及自己的信息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        IList<Model.OnLineServer.OlServerUserInfo> GetOlUsers(string olId, bool isService);

        /// <summary>
        /// 设置客服
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="serviceId">客服在线编号</param>
        /// <param name="serviceName">客服在线名称</param>
        bool SetService(string olId, string serviceId, string serviceName);

        /// <summary>
        /// 写入消息,返回写入的消息编号
        /// </summary>
        /// <param name="messageInfo">Model.OnLineServer.OlServerMessageInfo</param>
        /// <returns>string</returns>
        string InsertMessage(Model.OnLineServer.OlServerMessageInfo messageInfo);

        /// <summary>
        /// 根据最后发送消息时间获取最后发送消息时间以后发给自己的所有消息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="LastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        IList<Model.OnLineServer.OlServerMessageInfo> GetMessages(string olId, DateTime? LastSendMessageTime);

        /// <summary>
        /// 退出在线客服系统
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        bool Exit(string olId, bool isService);

        /// <summary>
        /// 清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
        /// </summary>
        /// <param name="stayTime">停留时间 单位：分钟</param>
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

        /// <summary>
        /// 根据指定在线编号设置用户最后发送消息时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="lastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        bool SetLastSendMessageTime(string olId, DateTime lastSendMessageTime);
    }
}
