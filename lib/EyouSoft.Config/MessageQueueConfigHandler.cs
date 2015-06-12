using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace EyouSoft.Config
{
    /// <summary>
    /// RabbitMQ自定义配置节处理程序
    /// </summary>
    public class MessageQueueConfigHandler:IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员

        object IConfigurationSectionHandler.Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlNodeList queueNodes;
            String aName, aServer;
            List<MessageQueue> list = new List<MessageQueue>();
            aServer = section.Attributes.GetNamedItem("Server").Value;
            queueNodes = section.SelectNodes("Queue");

            foreach (XmlNode node in queueNodes)
            {
                aName = node.Attributes.GetNamedItem("name").Value;

                list.Add(new MessageQueue()
                {
                    QueueName = aName,
                    Server = aServer
                });
            }
            return list;
        }

        #endregion
    }

    public class MessageQueue
    {
        /// <summary>
        /// 队列名
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 队列地址
        /// </summary>
        public string Server { get; set; }
    }
}
