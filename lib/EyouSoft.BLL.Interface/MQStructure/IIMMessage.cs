using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ消息业务逻辑接口
    /// </summary>
    /// 周文超 2010-06-12
    public interface IIMMessage
    {
        /*/// <summary>
        /// 发送一对一消息
        /// </summary>
        /// <param name="info">MQ消息一对一消息业务实体</param>
        /// <returns></returns>
        bool SendOneToOneMessage(Model.MQStructure.OneToOneMessageInfo info);        

        /// <summary>
        /// 发送一对多消息(消息中可以包含链接)
        /// </summary>
        /// <param name="info">MQ消息一对多(指定了接收人的MQ编号)消息业务实体</param>
        /// <returns></returns>
        bool SendOneToManyMessage(Model.MQStructure.OneToManySpecifiedMQMessageInfo info);*/

        /// <summary>
        /// 发送一对多消息
        /// </summary>
        /// <param name="info">MQ消息一对多(未指定接收人的MQ编号)消息业务实体</param>
        /// <returns>System.Int32 发送的消息数 小于0时为相应的错误号</returns>
        int SendOneToManyMessage(Model.MQStructure.OneToManyUnspecifiedMQMessageInfo info);

        /// <summary>
        /// 新增订单后向批发商发送MQ消息
        /// </summary>
        /// <param name="NewOrderId">订单ID</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        bool AddMessageToWholesalersByNewOrder(string NewOrderId, int orderType);

        /// <summary>
        /// 订单状态改变后向预定者发送MQ消息
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)只处理散拼订单</param>
        /// <returns>返回操作是否成功</returns>
        bool AddMessageToRetailersByOrder(string OrderId, int orderType);

        /// <summary>
        /// 供求信息被评论后发送MQ消息给发布人
        /// </summary>
        /// <param name="CommentId">供求评论ID</param>
        /// <returns>返回操作是否成功</returns>
        bool AddMessageToExchangeComment(string CommentId);
    }
}
