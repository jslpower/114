using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.OnLineServer
{
    /// <summary>
    /// 在线客服业务逻辑
    /// </summary>
    public class OLServer : EyouSoft.IBLL.OnLineServer.IOLServer
    {
        #region

        private readonly IDAL.OnLineServer.IOLServer dal = ComponentFactory.CreateDAL<IDAL.OnLineServer.IOLServer>();

        /// <summary>
        /// 构造在线客服业务逻辑接口
        /// </summary>
        /// <returns>在线客服业务逻辑接口</returns>
        public static IBLL.OnLineServer.IOLServer CreateInstance()
        {
            IBLL.OnLineServer.IOLServer op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.OnLineServer.IOLServer>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public OLServer()
        {

        }

        #endregion

        #region IOLServer 成员

        /// <summary>
        /// 未登录游客进入在线客服
        /// </summary>
        /// <param name="olCookieInfo">进入在线客服的cookie信息</param>
        /// <param name="CompanyId">公司Id</param>
        /// <returns></returns>
        public EyouSoft.Model.OnLineServer.OlServerUserInfo GuestLogin(EyouSoft.Model.OnLineServer.OlServerUserInfo olCookieInfo, string CompanyId)
        {
            //默认客户信息
            Model.OnLineServer.OlServerUserInfo guestOlInfo = new Model.OnLineServer.OlServerUserInfo()
            {
                OId = string.Empty,
                OlName = string.Empty,
                UserId = string.Empty,
                UserName = string.Empty,
                AcceptId = string.Empty,
                AcceptName = string.Empty,
                CompanyId = CompanyId,
                IsOnline = true,
                IsService = false,
                LoginTime = DateTime.Now,
                LastSendMessageTime = DateTime.Now
            };

            if (olCookieInfo == null) //cookie信息等于null
            {
                //写入在线客户信息
                guestOlInfo = InsertGuestInfo(null, CompanyId);
            }
            else //cookie信息不等于null
            {
                //取客户信息
                guestOlInfo = dal.GetUserInfoByOlId(olCookieInfo.OId);

                if (guestOlInfo != null && guestOlInfo.IsOnline) //在线
                {
                    guestOlInfo.CompanyId = CompanyId;
                    //更新在线客户信息
                    dal.UpdateUserInfo(guestOlInfo);
                }
                else
                {
                    //写入在线客户信息
                    guestOlInfo = InsertGuestInfo(null, CompanyId);
                }
            }

            return guestOlInfo;
        }

        /// <summary>
        /// 登录游客/客服进入在线客服
        /// </summary>
        /// <param name="isService">是否客服</param>
        /// <param name="CompanyId">高级网店公司Id</param>
        /// <returns></returns>
        public EyouSoft.Model.OnLineServer.OlServerUserInfo ServiceLogin(EyouSoft.SSOComponent.Entity.UserInfo userInfo, bool isService, string CompanyId)
        {
            Model.OnLineServer.OlServerUserInfo serviceOlInfo = null;

            if (userInfo == null)
            {
                serviceOlInfo = GuestLogin(null, null);
            }
            else
            {
                serviceOlInfo = dal.GetUserInfoByUserId(userInfo.ID, isService);
                if (serviceOlInfo != null)//已进入过在线客服
                {
                    if (!serviceOlInfo.IsOnline)
                    {
                        //设置登录时间
                        serviceOlInfo.LoginTime = DateTime.Now;
                    }
                    //设置在线名称
                    serviceOlInfo.OlName = userInfo.ContactInfo.ContactName;
                    //设置在线状态
                    serviceOlInfo.IsOnline = true;
                    if (!isService) //不是客服
                    {
                        Model.OnLineServer.OlServerUserInfo defaultOlServiceInfo =
                            dal.GetDefaultServiceInfo(CompanyId);
                        if (defaultOlServiceInfo != null)
                        {
                            serviceOlInfo.AcceptId = defaultOlServiceInfo.OId;
                            serviceOlInfo.AcceptName = defaultOlServiceInfo.OlName;
                        }
                    }
                    serviceOlInfo.CompanyId = CompanyId;

                    dal.UpdateUserInfo(serviceOlInfo);
                }
                else //未进入过在线客服
                {
                    serviceOlInfo = new Model.OnLineServer.OlServerUserInfo()
                    {
                        OId = string.Empty,
                        OlName = userInfo.ContactInfo.ContactName,
                        UserId = userInfo.ID,
                        UserName = userInfo.UserName,
                        AcceptId = string.Empty,
                        AcceptName = string.Empty,
                        CompanyId = CompanyId,
                        IsOnline = true,
                        IsService = isService,
                        LoginTime = DateTime.Now
                    };
                    if (!isService)//不是客服
                    {
                        Model.OnLineServer.OlServerUserInfo defaultOlServiceInfo =
                            dal.GetDefaultServiceInfo(CompanyId);
                        if (defaultOlServiceInfo != null)
                        {
                            serviceOlInfo.AcceptId = defaultOlServiceInfo.OId;
                            serviceOlInfo.AcceptName = defaultOlServiceInfo.OlName;
                            serviceOlInfo.LastSendMessageTime = defaultOlServiceInfo.LastSendMessageTime;
                        }
                    }
                    dal.InserUserInfo(serviceOlInfo);
                }
            }

            return serviceOlInfo;
        }

        /// <summary>
        /// 获取在线用户,是客服将获取正在服务的客户,非客服将获取所有客服及自己的信息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> GetOlUsers(string olId, bool isService)
        {
            if (string.IsNullOrEmpty(olId))
                return null;

            IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> users = null;

            if (isService)
            {
                users = dal.GetOnlineUsersInfo(olId);
            }
            else
            {
                users = dal.GetServicesInfo(olId);
                //users.Add(dal.GetUserInfoByOlId(olId));
            }

            return users;
        }

        /// <summary>
        /// 设置客服
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="serviceId">客服在线编号</param>
        /// <param name="serviceName">客服在线名称</param>
        public bool SetService(string olId, string serviceId, string serviceName)
        {
            if (string.IsNullOrEmpty(olId) || string.IsNullOrEmpty(serviceId))
                return false;

            return dal.SetService(olId, serviceId, serviceName);
        }

        /// <summary>
        /// 写入消息,返回写入的消息编号
        /// </summary>
        /// <param name="messageInfo">Model.OnLineServer.OlServerMessageInfo</param>
        /// <returns>string</returns>
        public string InsertMessage(EyouSoft.Model.OnLineServer.OlServerMessageInfo messageInfo)
        {
            if (messageInfo == null)
                return string.Empty;

            //设置最后发送消息时间
            if (dal.InsertMessage(messageInfo))
            {
                dal.SetLastSendMessageTime(messageInfo.SendId, messageInfo.SendTime);
            }

            return messageInfo.MessageId;
        }

        /// <summary>
        /// 根据最后发送消息时间获取最后发送消息时间以后发给自己的所有消息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="LastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> GetMessages(string olId, DateTime? LastSendMessageTime)
        {
            if (string.IsNullOrEmpty(olId))
                return null;

            IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> messages =
                new List<EyouSoft.Model.OnLineServer.OlServerMessageInfo>();

            if (LastSendMessageTime.HasValue)
            {
                messages = dal.GetMessages(olId, LastSendMessageTime);
            }
            else
            {
                messages = dal.GetMessages(olId);
            }

            return messages;
        }

        /// <summary>
        /// 退出在线客服系统
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        public bool Exit(string olId, bool isService)
        {
            if (string.IsNullOrEmpty(olId))
                return false;

            //设置用户的在线状态为false
            bool exitResult = dal.Exit(olId);
            //设置成功且是客服
            if (exitResult && isService)
            {
                //设置与之会话的客户的默认服务客服信息为无
                dal.ExitSetDefaultService(olId);
            }

            return exitResult;
        }

        /// <summary>
        /// 清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
        /// </summary>
        /// <param name="stayTime">停留时间 单位：分钟</param>
        public bool ClearUserOut(int stayTime)
        {
            if (stayTime <= 0)
                return false;

            return dal.ClearUserOut(stayTime);
        }

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
        public IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> GetMessages(DateTime? StartTime, DateTime? EndTime, string LikeInfo, int pageSize, int pageIndex, ref int recordSum)
        {
            return dal.GetMessages(StartTime, EndTime, LikeInfo, pageSize, pageIndex, ref recordSum);
        }

        /// <summary>
        /// 根据id删除聊天记录
        /// </summary>
        /// <param name="MessagesId">聊天记录ID</param>
        /// <returns></returns>
        public bool DeleteMessages(string MessagesId)
        {
            if (string.IsNullOrEmpty(MessagesId))
                return false;

            return dal.DeleteMessages(MessagesId);
        }

        /// <summary>
        /// 根据UserId删除在线客服用户信息
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="IsService">是否为客服</param>
        /// <returns></returns>
        public bool DeleteUserInfoByUserId(string UserId, bool IsService)
        {
            if (string.IsNullOrEmpty(UserId))
                return false;

            return dal.DeleteUserInfoByUserId(UserId, IsService);
        }

        /// <summary>
        /// 根据在线编号获取此在线用户最后发送消息的时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        public DateTime? GetLastSendMessageTime(string olId)
        {
            if (string.IsNullOrEmpty(olId))
                return null;

            return dal.GetLastSendMessageTime(olId);
        }

        /// <summary>
        /// 根据用户Id获取该用户所有未读消息的记录数
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public int GetMessageCountByUserId(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
                return 0;

            return dal.GetMessageCountByUserId(UserId);
        }

        /// <summary>
        /// 根据指定在线编号设置用户最后发送消息时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="lastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        public bool SetLastSendMessageTime(string olId, DateTime lastSendMessageTime)
        {
            if (string.IsNullOrEmpty(olId))
                return false;

            return dal.SetLastSendMessageTime(olId, lastSendMessageTime);
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 写入在线客户信息
        /// </summary>
        /// <param name="UserInfo">登录网站的用户信息</param>
        /// <param name="CompanyId">公司Id</param>
        /// <returns></returns>
        private EyouSoft.Model.OnLineServer.OlServerUserInfo InsertGuestInfo(EyouSoft.SSOComponent.Entity.UserInfo UserInfo
            , string CompanyId)
        {
            //默认客户信息
            Model.OnLineServer.OlServerUserInfo guestOlInfo = new Model.OnLineServer.OlServerUserInfo()
            {
                OId = string.Empty,
                OlName = string.Empty,
                UserId = string.Empty,
                UserName = string.Empty,
                AcceptId = string.Empty,
                AcceptName = string.Empty,
                CompanyId = CompanyId,
                IsOnline = true,
                IsService = false,
                LoginTime = DateTime.Now,
                LastSendMessageTime = DateTime.Now
            };

            //取默认的客服
            Model.OnLineServer.OlServerUserInfo defaultOlServiceInfo = defaultOlServiceInfo = dal.GetDefaultServiceInfo(CompanyId);

            if (defaultOlServiceInfo != null) //设置默认的客服
            {
                guestOlInfo.AcceptId = defaultOlServiceInfo.OId;
                guestOlInfo.AcceptName = defaultOlServiceInfo.OlName;
            }

            if (UserInfo != null) //已登录用户
            {
                //设置登录用户信息
                guestOlInfo.UserId = UserInfo.ID;
                guestOlInfo.UserName = UserInfo.UserName;
                //在线名称设置成用户名
                guestOlInfo.OlName = UserInfo.UserName;
                guestOlInfo.CompanyId = CompanyId;
                //写入在线客户信息
                dal.InserUserInfo(guestOlInfo);
            }
            else
            {
                //写入在线客户信息
                int i = dal.InserUserInfo(guestOlInfo);
                //设置在线名称
                guestOlInfo.OlName = string.Format("客户{0:0000}号", i + 1);
                //更新在线客户信息
                dal.UpdateUserInfo(guestOlInfo);
            }

            return guestOlInfo;
        }

        #endregion
    }
}
