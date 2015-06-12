using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ消息业务逻辑
    /// </summary>
    /// 周文超 2010-06-12
    public class IMMessage : IBLL.MQStructure.IIMMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMessage() { }

        private readonly IDAL.MQStructure.IIMMessage dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMMessage>();

        #region MQ消息域名配置

        /// <summary>
        /// MQ消息中转页面地址（登录页）
        /// </summary>
        private static readonly string MQMessageTransitUrl = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("MQMessage", "MQMessageTransitUrl");
        /// <summary>
        /// 订单专线后台管理页面地址
        /// </summary>
        private static readonly string MQBackCenterWholesalersOrderPurposeUrl = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("MQMessage", "MQBackCenterWholesalersOrderPurposeUrl");
        /// <summary>
        /// 组团专线后台管理页面地址
        /// </summary>
        private static readonly string MQBackCenterRetailersOrderPurposeUrl = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("MQMessage", "MQBackCenterRetailersOrderPurposeUrl");
        /// <summary>
        /// 供求信息回复查看页
        /// </summary>
        private static readonly string MQExchangeComment = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("MQMessage", "MQExchangeComment");

        #endregion

        /// <summary>
        /// 构造MQ消息业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMMessage CreateInstance()
        {
            IBLL.MQStructure.IIMMessage op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMMessage>();
            }
            return op;
        }


        #region IIMMessage 成员

        /*/// <summary>
        /// 发送一对一消息
        /// </summary>
        /// <param name="info">MQ消息一对一消息业务实体</param>
        /// <returns></returns>
        public bool SendOneToOneMessage(EyouSoft.Model.MQStructure.OneToOneMessageInfo info)
        {
            if (info == null)
                return false;

            return dal.SendOneToOneMessage(info);
        }

        /// <summary>
        /// 发送一对多消息(消息中可以包含链接)
        /// </summary>
        /// <param name="info">MQ消息一对多(指定了接收人的MQ编号)消息业务实体</param>
        /// <returns></returns>
        public bool SendOneToManyMessage(EyouSoft.Model.MQStructure.OneToManySpecifiedMQMessageInfo info)
        {
            if (info == null)
                return false;

            return dal.SendOneToManyMessage(info);
        }*/

        /// <summary>
        /// 发送一对多消息
        /// </summary>
        /// <param name="info">MQ消息一对多(未指定接收人的MQ编号)消息业务实体</param>
        /// <returns>System.Int32 发送的消息数 小于0时为相应的错误号</returns>
        public int SendOneToManyMessage(EyouSoft.Model.MQStructure.OneToManyUnspecifiedMQMessageInfo info)
        {
            if (info.SendType== EyouSoft.Model.MQStructure.SendType.聊天窗口 && info.SendMQId == 0)
            {
                return -1;//以聊天窗口方式发送消息时未填写发送人MQ编号
            }

            if (string.IsNullOrEmpty(info.Message))
            {
                return -2;//未填写消息内容
            }

            if (info.SendType == EyouSoft.Model.MQStructure.SendType.系统消息 && Encoding.Default.GetBytes(info.Message).Length > 399)
            {
                return -3;//以系统消息方式发送时消息内容不能超过200个字符
            }

            if (info.SendType == EyouSoft.Model.MQStructure.SendType.聊天窗口 && Encoding.Default.GetBytes(info.Message).Length > 399)
            {
                return -4;//以聊天窗口方式发送时消息内容不能超过200个字符
            }

            if (!info.IsAllCompanyType && (info.AcceptCompanyType == null || info.AcceptCompanyType.Count == 0) && !info.IsContainsGuest)
            {
                return -5;//接收对象为指定类型时未指定任何类型
            }

            return dal.SendOneToManyMessage(info);
        }

        /// <summary>
        /// 新增订单后向批发商发送MQ消息
        /// </summary>
        /// <param name="NewOrderId">订单ID</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        public bool AddMessageToWholesalersByNewOrder(string NewOrderId,int orderType)
        {
            if (string.IsNullOrEmpty(NewOrderId) || string.IsNullOrEmpty(MQMessageTransitUrl) || string.IsNullOrEmpty(MQBackCenterWholesalersOrderPurposeUrl))
                return false;

            return dal.AddMessageToWholesalersByNewOrder(NewOrderId, MQMessageTransitUrl, MQBackCenterWholesalersOrderPurposeUrl, orderType);
        }

        /// <summary>
        /// 订单状态改变后发送消息给预订人(三种状态，已成交，不受理，已留位)
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        public bool AddMessageToRetailersByOrder(string OrderId, int orderType)
        {
            if (string.IsNullOrEmpty(OrderId) || string.IsNullOrEmpty(MQMessageTransitUrl) || string.IsNullOrEmpty(MQBackCenterRetailersOrderPurposeUrl))
                return false;

            return dal.AddMessageToRetailersByOrder(OrderId, MQMessageTransitUrl, MQBackCenterRetailersOrderPurposeUrl,orderType);
        }

        /// <summary>
        /// 供求信息被评论后发送MQ消息给发布人
        /// </summary>
        /// <param name="CommentId">供求评论ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool AddMessageToExchangeComment(string CommentId)
        {
            if (string.IsNullOrEmpty(CommentId) || string.IsNullOrEmpty(MQMessageTransitUrl) || string.IsNullOrEmpty(MQExchangeComment))
                return false;

            return dal.AddMessageToExchangeComment(CommentId, MQMessageTransitUrl, MQExchangeComment);
        }

        #endregion
    }
}
