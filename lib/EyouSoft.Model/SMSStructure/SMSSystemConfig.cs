using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    /// <summary>
    /// 短信通道实体
    /// </summary>
    /// Author:张志瑜 2010-09-20
    public class SMSChannel
    {
        /// <summary>
        /// 该通道短信通道索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 该通道短信通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 该通道发送短信的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 该通道发送短信的密码
        /// </summary>
        public string Pw { get; set; }
        /// <summary>
        /// 该通道发送1条短信的价格(单位:分/条)
        /// </summary>
        public decimal PriceOne { get; set; }
        /// <summary>
        /// 是否为长短信通道,true则为210个字的长短信
        /// </summary>
        public bool IsLong { get; set; }
    }

    /// <summary>
    /// 短信通道实体集合
    /// </summary>
    /// Author:张志瑜 2010-09-20
    public class SMSChannelList
    {
        private Dictionary<int, EyouSoft.Model.SMSStructure.SMSChannel> _items = new Dictionary<int, SMSChannel>();

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public SMSChannelList()
        {
            string SMS_SMSChannel = System.Configuration.ConfigurationManager.AppSettings["SMS_SMSChannel"];
            if (!string.IsNullOrEmpty(SMS_SMSChannel))
            {
                string[] channelArr = SMS_SMSChannel.Split("|".ToCharArray());
                if (channelArr != null && channelArr.Length > 0)
                {
                    foreach (string channel in channelArr)
                    {
                        string[] valArr = channel.Split(",".ToCharArray());
                        if (valArr != null && valArr.Length > 0)
                        {
                            EyouSoft.Model.SMSStructure.SMSChannel item = new EyouSoft.Model.SMSStructure.SMSChannel();
                            //index:1,name:通道1,user:tongye1,pw:000000,price:7.8,size:0
                            item.Index = Convert.ToInt32(valArr[0].Split(":".ToCharArray())[1]);
                            item.ChannelName = valArr[1].Split(":".ToCharArray())[1];
                            item.UserName = valArr[2].Split(":".ToCharArray())[1];
                            item.Pw = valArr[3].Split(":".ToCharArray())[1];
                            item.PriceOne = Convert.ToDecimal(valArr[4].Split(":".ToCharArray())[1]);
                            switch (valArr[5].Split(":".ToCharArray())[1])
                            { 
                                case "0":
                                    item.IsLong = false;
                                    break;
                                case "1":
                                    item.IsLong = true;
                                    break;
                            }                            
                            this._items.Add(item.Index, item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得某个通道索引值下的通道实体,若不存在,则抛出异常
        /// </summary>
        /// <param name="index">通道索引值,从0开始</param>
        /// <returns></returns>
        public SMSChannel this[int index]
        {
            get {
                if (this._items.ContainsKey(index))
                    return this._items[index];
                else
                {
                    EyouSoft.Model.SMSStructure.SMSChannel c = new SMSChannel();
                    c.Index = -1;
                    c.ChannelName = "未找到该通道";
                    c.IsLong = false;
                    return c;
                }
                   // throw new Exception("未找到该通道值!");
            }
        }

        ///// <summary>
        ///// 获得某个通道类型的通道实体,若不存在,则抛出异常
        ///// </summary>
        ///// <param name="channelType">通道类型</param>
        ///// <returns></returns>
        //public SMSChannel this[SMSChannelType channelType]
        //{
        //    get {
        //        return this[Convert.ToInt32(channelType)];
        //    }
        //}

        /// <summary>
        /// 获得总的通道数量
        /// </summary>
        public int Count { get { return this._items.Count; } }
    }
}
