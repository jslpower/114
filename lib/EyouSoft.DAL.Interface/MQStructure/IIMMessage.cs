using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.MQStructure
{
    /// <summary>
    /// MQ消息数据访问接口
    /// </summary>
    /// 周文超 2010-05-11
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
        /// <returns>System.Int32 发送的消息数</returns>
        int SendOneToManyMessage(Model.MQStructure.OneToManyUnspecifiedMQMessageInfo info);

        /// <summary>
        /// 新增订单后向批发商发送MQ消息
        /// </summary>
        /// <param name="NewOrderId">订单ID</param>
        /// <param name="MQMessageTransitUrl">MQ消息中转页面地址（登录页）</param>
        /// <param name="MQPurposeUrl">MQ消息目的地址</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        bool AddMessageToWholesalersByNewOrder(string NewOrderId, string MQMessageTransitUrl, string MQPurposeUrl, int orderType);

        /// <summary>
        /// 订单状态改变后发送消息给预订人
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="MQMessageTransitUrl">MQ消息中转页面地址（登录页）</param>
        /// <param name="MQPurposeUrl">MQ消息目的地址</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        bool AddMessageToRetailersByOrder(string OrderId, string MQMessageTransitUrl, string MQPurposeUrl,int orderType);

        /// <summary>
        /// 供求信息被评论后发送MQ消息给发布人
        /// </summary>
        /// <param name="CommentId">供求评论ID</param>
        /// <param name="MQMessageTransitUrl">MQ消息中转页面地址（登录页）</param>
        /// <param name="MQPurposeUrl">MQ消息目的地址</param>
        /// <returns>返回操作是否成功</returns>
        bool AddMessageToExchangeComment(string CommentId, string MQMessageTransitUrl, string MQPurposeUrl);
        /// <summary>
        /// 获得当天最近的MQ消息
        /// </summary>
        /// <param name="count">要获取的MQ消息条数,若为0,则表示查询所有</param>
        /// <returns></returns>
        IList<EyouSoft.Model.MQStructure.IMMessage> GetTodayLastMessage(int count);
    }
}
