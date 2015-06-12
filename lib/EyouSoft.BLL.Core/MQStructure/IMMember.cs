using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ用户信息业务逻辑
    /// </summary>
    /// 周文超 2010-06-12
    public class IMMember : IBLL.MQStructure.IIMMember
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMember() { }

        private readonly IDAL.MQStructure.IIMMember dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMMember>();

        /// <summary>
        /// MQ用户信息业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMMember CreateInstance()
        {
            IBLL.MQStructure.IIMMember op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMMember>();
            }
            return op;
        }

        #region private members
        /// <summary>
        /// 根据群号获取MQ编号及群序号
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupIndex">群序号</param>
        private void GetMqIdAndGroupIndexByGroupId(int groupId,out int mqId,out int groupIndex)
        {
            mqId = 0;
            groupIndex = 0;

            if (groupId >= 100)
            {
                string s = groupId.ToString();

                //mq编号：群号去掉后两位
                mqId = int.Parse(s.Substring(0, s.Length - 2));
                //群序号：群号后两位，若后两位数的十位为0刚省略该0
                groupIndex = int.Parse(s.Substring(s.Length - 2));
            }
        }
        #endregion

        #region IIMMember 成员

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>返回MQ用户信息实体</returns>
        public EyouSoft.Model.MQStructure.IMMember GetModel(int im_uid)
        {
            if (im_uid <= 0)
                return null;

            return dal.GetModel(im_uid);
        }

        /// <summary>
        /// 通过MQ帐号获得MQId
        /// </summary>
        /// <param name="Im_UserName">MQ帐号</param>
        /// <returns>返回MQID</returns>
        public int GetImId(string Im_UserName)
        {
            if (string.IsNullOrEmpty(Im_UserName))
                return 0;

            return dal.GetImId(Im_UserName);
        }

        /// <summary>
        /// 获的在线人数
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>在线人数</returns>
        public int GetOnLineNumber(int im_uid)
        {
            return dal.GetOnLineNumber(im_uid);
        }

        /// <summary>
        /// 获取在线MQID
        /// </summary>
        public string[] GetOnLineMQ()
        {
            return GetOnLineMQ(0, 0);
        }

        /// <summary>
        /// 获取在线MQID
        /// </summary>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        public string[] GetOnLineMQ(int ProvinceId, int CityId)
        {
            return dal.GetOnLineMQ(ProvinceId, CityId);
        }

        /// <summary>
        /// 获取MQ用户数量
        /// </summary>
        /// <returns>MQ用户数量</returns>
        public int GetMQUserCount()
        {
            return dal.GetMQUserCount();
        }

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
        public IList<Model.MQStructure.IMMemberAndUser> GetSearchFriend(int PageSize, int PageIndex, ref int RecordCount
            , string CompanyName, string ContactName, string UserName, int ProvinceId, int CityId, string CityName, int MQID
            , EyouSoft.Model.MQStructure.MQFriendSearchType? FriendSearchType, bool IsOnline
            , params EyouSoft.Model.CompanyStructure.CompanyType[] CurrCompanyType)
        {
            return dal.GetSearchFriend(PageSize, PageIndex, ref RecordCount, CompanyName, ContactName, UserName, ProvinceId, CityId, CityName
                , MQID, FriendSearchType, IsOnline ,CurrCompanyType);
        }

        /// <summary>
        /// 设置MQ群人数，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="maxMember">人数上限</param>
        /// <returns></returns>
        public int SetGroupMember(int groupId, int maxMember)
        {
            if (groupId < 100) return 0;
            if (maxMember < 100) return -1;
            if (maxMember > 900) return -2;

            int mqId;
            int groupIndex;

            this.GetMqIdAndGroupIndexByGroupId(groupId, out mqId, out groupIndex);

            return dal.SetGroupMember(maxMember, mqId, groupIndex) ? 1 : -3;
        }

        /// <summary>
        /// 获取MQ群人数
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <returns></returns>
        public int GetGroupMember(int groupId)
        {
            if (groupId < 100) return 0;

            int mqId;
            int groupIndex;
            this.GetMqIdAndGroupIndexByGroupId(groupId, out mqId, out groupIndex);

            return dal.GetGroupMember(mqId, groupIndex);
        }
        /// <summary>
        /// /获取指定城市在线用户数量
        /// </summary>
        /// <param name="porvinceId">省份</param>
        /// <param name="cityId">城市</param>
        /// <returns></returns>
        public int GetCityOnlineNumber(int porvinceId, int cityId)
        {
            return dal.GetCityOnlineNumber(porvinceId, cityId);
        }
        #endregion
    }
}
