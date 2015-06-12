using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.MQStructure
{
    /// <summary>
    /// MQ用户信息数据访问接口
    /// </summary>
    /// 周文超 2010-05-11
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
        /// 获取在线MID
        /// </summary>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        string[] GetOnLineMQ(int ProvinceId, int CityId);

        /// <summary>
        /// 获的在线人数
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>在线人数</returns>
        int GetOnLineNumber(int im_uid);

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
        /// <param name="IsOnline">是否在线</param>
        /// <param name="CurrCompanyType">当前查找好友的公司类型数组</param>
        /// <returns>返回MQ查找好友实体集合</returns>
        IList<Model.MQStructure.IMMemberAndUser> GetSearchFriend(int PageSize, int PageIndex, ref int RecordCount, string CompanyName
            , string ContactName, string UserName, int ProvinceId, int CityId, string CityName, int MQID
            , EyouSoft.Model.MQStructure.MQFriendSearchType? FriendSearchType, bool IsOnline
            , params EyouSoft.Model.CompanyStructure.CompanyType[] CurrCompanyType);

        /// <summary>
        /// 获得长期未登录mq的用户信息
        /// </summary>
        /// <param name="days">未登录的天数</param>
        /// <param name="cityList">要查找的城市ID列表,若为null,则表示查找全国城市</param>
        /// <returns></returns>
        IList<Model.CompanyStructure.CompanyUserBase> GetLongOffLineList(int days, params int[] cityList);
        /// <summary>
        /// 设置MQ群人数
        /// </summary>
        /// <param name="maxMember">群人数上限</param>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupIndex">群序号</param>
        /// <returns></returns>
        bool SetGroupMember(int maxMember, int mqId, int groupIndex);
        /// <summary>
        /// 获取MQ群人数
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupIndex">群序号</param>
        /// <returns></returns>
        int GetGroupMember(int mqId, int groupIndex);


        /// <summary>
        /// /获取指定城市在线用户数量
        /// </summary>
        /// <param name="porvinceId">省份</param>
        /// <param name="cityId">城市</param>
        /// <returns></returns>
        int GetCityOnlineNumber(int porvinceId, int cityId);
    }
}
