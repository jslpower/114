using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.ToolStructure
{
    /// <summary>
    /// 消息提醒记录
    /// </summary>
    /// Author:张志瑜  2010-10-25
    public class MsgTipRecord : EyouSoft.IBLL.ToolStructure.IMsgTipRecord
    {
        private readonly EyouSoft.IDAL.ToolStructure.IMsgTipRecord idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ToolStructure.IMsgTipRecord>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.ToolStructure.IMsgTipRecord CreateInstance()
        {
            EyouSoft.IBLL.ToolStructure.IMsgTipRecord op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.ToolStructure.IMsgTipRecord>();
            }
            return op;
        }

        /// <summary>
        /// 是否发送消息提醒
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="sendWay">消息发送途径</param>
        /// <param name="mqID">接收消息用户MQID</param>
        /// <param name="cityId">当前用户所在城市</param>
        /// <returns></returns>
        public bool IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType msgType, EyouSoft.Model.ToolStructure.MsgSendWay sendWay, string mqID, int cityId)
        {
            bool issend = false;
            IList<int> cityidlist = new List<int>();
            DateTime? endtime = null;

            string MsgTipConfig = System.Configuration.ConfigurationManager.AppSettings["MsgTipConfig"];
            if (!string.IsNullOrEmpty(MsgTipConfig))
            {
                string[] valueArr = MsgTipConfig.Split("|".ToCharArray());
                if (valueArr != null && valueArr.Length > 0)
                {
                    foreach (string item in valueArr)
                    {
                        string[] val = item.Split(":".ToCharArray());
                        if (val != null && val.Length > 0)
                        {
                            switch (val[0])
                            {
                                case "city":
                                    string[] citys = val[1].Split(",".ToCharArray());
                                    foreach (string city in citys)
                                        cityidlist.Add(Convert.ToInt32(city));
                                    break;
                                case "endtime":
                                    endtime = Convert.ToDateTime(val[1]);
                                    break;
                            }
                        }
                    }
                }
                
                //判断时间有无到期,以及是否在要发送的城市内
                if (endtime.HasValue && endtime.Value.CompareTo(DateTime.Now) > 0)
                {
                    int smsSendCount = 0;   //短信发送次数
                    int smsSendCountMax = 1;   //短信当日可以发送的次数
                    if (sendWay == EyouSoft.Model.ToolStructure.MsgSendWay.SMS)
                    {
                        if (idal.GetSmsRemain() < 1) //当期发完
                            return false;
                        if (idal.GetTodayCount() > 800)//每天800
                            return false;
                    }
                    switch (msgType)
                    { 
                        case EyouSoft.Model.ToolStructure.MsgType.RegPass:
                            switch (sendWay)
                            { 
                                case EyouSoft.Model.ToolStructure.MsgSendWay.Email:
                                    issend = true;
                                    break;
                                case EyouSoft.Model.ToolStructure.MsgSendWay.SMS:
                                    if (cityidlist.Contains(cityId))
                                        issend = true;
                                    break;
                            }
                            break;
                        case EyouSoft.Model.ToolStructure.MsgType.AddFriend:
                        case EyouSoft.Model.ToolStructure.MsgType.NewOrder:
                            if (sendWay == EyouSoft.Model.ToolStructure.MsgSendWay.SMS && !cityidlist.Contains(cityId))
                                break;

                            //判断是否在线
                            Model.MQStructure.IMMember model_member = EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetModel(Convert.ToInt32(mqID));
                            bool isonline = false;
                            if (model_member.im_status > 11 && !model_member.IsAutoLogin)
                                isonline = true;
                            model_member = null;

                            //判断可发送的短信数量
                            //if (msgType == EyouSoft.Model.ToolStructure.MsgType.NewOrder)
                            //    smsSendCountMax = 2;
                            //else
                            //    smsSendCountMax = 1;

                            //判断最终结果
                            if (!isonline)
                            {
                                switch (sendWay)
                                {
                                    case EyouSoft.Model.ToolStructure.MsgSendWay.Email:
                                        issend = true;
                                        break;
                                    case EyouSoft.Model.ToolStructure.MsgSendWay.SMS:
                                        if (!idal.IsValid(mqID)) //MQ是否被连续发送冻结
                                        {
                                            if (idal.IsValid(mqID, 3))
                                            {
                                                //连续3天发送冻结
                                                idal.Add(mqID, DateTime.Now.AddDays(3));
                                                return false;
                                            }
                                            smsSendCount = idal.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.MQNoReadMsg, sendWay, mqID) +
                                                idal.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.AddFriend, sendWay, mqID) +
                                                idal.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.NewOrder, sendWay, mqID);
                                            if (smsSendCount < smsSendCountMax)
                                                issend = true;
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
            }

            return issend;
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">消息提醒记录实体</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.ToolStructure.MsgTipRecord model)
        {
            return idal.Add(model);
        }
    }
}
