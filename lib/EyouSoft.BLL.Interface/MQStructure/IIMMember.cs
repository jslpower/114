using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ用户信息业务逻辑接口
    /// </summary>
    /// 周文超 2010-06-12
    public interface IIMMember
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>返回MQ用户信息实体</returns>
        Model.MQStructure.IMMember GetModel(int im_uid);

        /// <summary>
        /// 通过MQ帐号获得MQId
        /// </summary>
        /// <param name="Im_UserName">MQ帐号</param>
        /// <returns>返回MQID</returns>
        int GetImId(string Im_UserName);

        /// <summary>
        /// 获的在线人数
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>在线人数</returns>
        int GetOnLineNumber(int im_uid);

         /// <summary>
        /// 获取在线MQID
        /// </summary>
        string[] GetOnLineMQ();

        /// <summary>
        /// 获取在线MQID
        /// </summary>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        string[] GetOnLineMQ(int ProvinceId, int CityId);

        /// <summary>
        /// 获取MQ用户数量
        /// </summary>
        /// <returns>MQ用户数量</returns>
        int GetMQUserCount();


        /// <summary>
        /// MQ查找好友
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="ContactName">联系人名称</param>
        /// <param name="UserName">用户名</param>
        /// <param name="ProvinceId">公司所在省份</param>
        /// <param name="CityId">公司所在城市</param>
        /// <param name="CityName">城市名称</param>
        /// <param name="MQID">MQID</param>
        /// <param name="FriendSearchType">查找方式</param>
        /// <param name="IsOnline">是否在线(否:查询所有状态)</param>
        /// <param name="CurrCompanyType">当前查找好友的公司类型数组</param>
        /// <returns>返回MQ查找好友实体集合</returns>
        IList<Model.MQStructure.IMMemberAndUser> GetSearchFriend(int PageSize, int PageIndex, ref int RecordCount, string CompanyName
            , string ContactName, string UserName, int ProvinceId, int CityId, string CityName, int MQID
            , EyouSoft.Model.MQStructure.MQFriendSearchType? FriendSearchType, bool IsOnline
            , params EyouSoft.Model.CompanyStructure.CompanyType[] CurrCompanyType);

        /// <summary>
        /// 设置MQ群人数，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="maxMember">人数上限</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_MQ_SetGroupMember_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_MQ_SetGroupMember,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""groupId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""maxMember"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_MQ_SetGroupMember_CODE)]
        int SetGroupMember(int groupId, int maxMember);
        /// <summary>
        /// 获取MQ群人数
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <returns></returns>
        int GetGroupMember(int groupId);

        /// <summary>
        /// /获取指定城市在线用户数量
        /// </summary>
        /// <param name="porvinceId">省份</param>
        /// <param name="cityId">城市</param>
        /// <returns></returns>
        int GetCityOnlineNumber(int porvinceId, int cityId);
    }
}
