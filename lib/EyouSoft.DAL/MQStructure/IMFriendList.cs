using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// IM中用户好友
    /// </summary>
    /// 周文超 2010-05-11
    public class IMFriendList : DALBase, IDAL.MQStructure.IIMFriendList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMFriendList()
        { }

        #region SQL

        private const string SQL_SELECT_GetFriends = " SELECT A.friendid AS FriendId,B.im_displayname AS FriendName,B.im_status AS OnlineStatus,IsOnline = case when B.im_status > 11 then 1 else 0 end FROM im_friendlist AS A INNER JOIN im_member AS B ON A.friendid = cast(B.im_uid as varchar(20)) WHERE A.uid=@MQID AND A.groupname=@GROUPNAME  order by IsOnline desc ";
        private const string Sql_Select_GetFriendsById = " SELECT [uid],[friendid],[groupname],[issuetime],[im_member].im_status AS OnlineStatus,IsOnline = case when [im_member].im_status > 11 then 1 else 0 end,[im_member].im_displayname as im_displayname FROM [im_friendlist] INNER JOIN [im_member] ON [im_friendlist].[uid] = cast([im_member].[im_uid] AS varchar(20)) WHERE [im_friendlist].[uid] = @MQID order by IsOnline desc ";
        private const string MYSQL_SELECT_GetFriendGroups = "Set Names utf8; SELECT A.group_name AS GroupName,(SELECT COUNT(*) FROM friend_list AS B WHERE B.uid=A.uid AND B.group_name=A.group_name) AS FriendCount FROM group_list AS A WHERE A.uid={0}; ";
        private const string MYSQL_SELECT_GetFriends = "Set Names utf8;SELECT friend_id,group_name FROM friend_list WHERE uid={0};";
        private const string SQL_SELECT_GetTodayFriendCount = "SELECT TodayFriendCount FROM im_member WHERE im_uid=@MQID";
        private const string SQL_UPDATE_TodayFriendCount = "UPDATE im_member SET TodayFriendCount=TodayFriendCount+1 WHERE im_uid=@MQID";

        #endregion

        #region 函数成员

        /// <summary>
        /// 根据MQ用户ID获取该用户的所有的好友信息
        /// </summary>
        /// <param name="MQId">MQ用户ID</param>
        /// <returns>IM中用户好友实体集合</returns>
        public virtual IList<EyouSoft.Model.MQStructure.IMFriendList> GetFriendList(int MQId)
        {
            IList<Model.MQStructure.IMFriendList> List = new List<Model.MQStructure.IMFriendList>();
            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Select_GetFriendsById);
            base.MQStore.AddInParameter(dc, "MQID", DbType.AnsiStringFixedLength, MQId.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                Model.MQStructure.IMFriendList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMFriendList();
                    model.MQId = MQId.ToString();
                    model.FriendMQId = dr["friendid"].ToString();
                    model.GroupName = dr["groupname"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("issuetime")))
                        model.IssueTime = DateTime.Parse(dr["issuetime"].ToString());
                    model.IsOnline = dr.GetInt16(dr.GetOrdinal("OnlineStatus")) > 11 ? true : false;
                    model.FriendName = dr["im_displayname"].ToString();

                    List.Add(model);
                }
                model = null;
            }
            return List;
        }

        /// <summary>
        /// 获取好友信息集合
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupName">分组名称 为空时返回不在任何分组里的好友</param>
        /// <returns></returns>
        public virtual IList<Model.MQStructure.IMFriendList> GetFriends(int mqId, string groupName)
        {
            if (mqId <= 0)
                return null;

            IList<Model.MQStructure.IMFriendList> List = new List<Model.MQStructure.IMFriendList>();
            DbCommand dc = base.MQStore.GetSqlStringCommand(SQL_SELECT_GetFriends);
            base.MQStore.AddInParameter(dc, "MQID", DbType.AnsiStringFixedLength, mqId.ToString());
            base.MQStore.AddInParameter(dc, "GROUPNAME", DbType.String, groupName);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                Model.MQStructure.IMFriendList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMFriendList();
                    model.MQId = mqId.ToString();
                    model.FriendMQId = dr["FriendId"].ToString();
                    model.GroupName = groupName;
                    model.IsOnline = dr.GetInt16(dr.GetOrdinal("OnlineStatus")) > 11 ? true : false;
                    model.FriendName = dr["FriendName"].ToString();

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }

        /// <summary>
        /// 同步好友信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        public virtual bool InStepFriends(int mqId)
        {
            if (mqId <= 0)
                return false;

            IList<Model.MQStructure.IMFriendList> FriendList = GetFriends(mqId);
            if (FriendList == null || FriendList.Count <= 0)
                return false;

            string strInToFriend = " INSERT INTO [im_friendlist]([uid],[friendid],[groupname],[issuetime]) VALUES ('{0}','{1}','{2}','{3}'); ";
            string strDelFriend = " DELETE FROM [im_friendlist] WHERE [uid] = '{0}'; ";
            StringBuilder strSqlFriend = new StringBuilder();
            strSqlFriend.AppendFormat(strDelFriend, mqId);
            foreach (Model.MQStructure.IMFriendList model in FriendList)
            {
                if (model != null)
                {
                    strSqlFriend.AppendFormat(strInToFriend, mqId, model.FriendMQId, model.GroupName, DateTime.Now);
                }
            }
            DbCommand dc = base.MQStore.GetSqlStringCommand(strSqlFriend.ToString());

            int Result = DbHelper.ExecuteSql(dc, base.MQStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 同步好友分组信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        public virtual bool InStepFriendGroups(int mqId)
        {
            if (mqId <= 0)
                return false;

            IList<Model.MQStructure.IMGroupList> GroupList = GetFriendGroups(mqId);
            if (GroupList == null || GroupList.Count <= 0)
                return false;

            string strDelGroup = " DELETE FROM [im_grouplist] WHERE [uid] = '{0}'; ";
            string strInToGroup = " INSERT INTO [im_grouplist]([uid],[groupname],[friendcount],[issuetime]) VALUES ('{0}','{1}',{2},'{3}'); ";
            StringBuilder strSqlGroup = new StringBuilder();
            strSqlGroup.AppendFormat(strDelGroup, mqId);
            foreach (Model.MQStructure.IMGroupList model in GroupList)
            {
                if (model != null)
                {
                    strSqlGroup.AppendFormat(strInToGroup, mqId, model.GroupName, model.FriendCount, DateTime.Now);
                }
            }
            DbCommand dc = base.MQStore.GetSqlStringCommand(strSqlGroup.ToString());
            int Result = DbHelper.ExecuteSql(dc, base.MQStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取某个MQ用户的好友MQID集合
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>好友MQID集合</returns>
        public virtual List<int> GetMyFriendIdList(int im_uid)
        {
            if (im_uid <= 0)
                return null;

            string strWhere = " Select friend_id from friend_list where uid=@uid ";
            List<int> FriendIdList = new List<int>();
            DbCommand dc = base.MySQLStore.GetSqlStringCommand(strWhere);
            base.MySQLStore.AddInParameter(dc, "uid", DbType.String, im_uid.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        FriendIdList.Add(int.Parse(dr["friend_id"].ToString().Trim()));
                }
            }

            return FriendIdList;
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 获取IM好友分组信息(从mysql库获取数据)
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        private IList<Model.MQStructure.IMGroupList> GetFriendGroups(int mqId)
        {
            IList<Model.MQStructure.IMGroupList> List = new List<Model.MQStructure.IMGroupList>();

            DbCommand dc = base.MySQLStore.GetSqlStringCommand(string.Format(MYSQL_SELECT_GetFriendGroups,mqId));
            //base.MySQLStore.AddInParameter(dc, "uid", DbType.Int32, mqId);          

            //MySql.Data.MySqlClient.MySqlParameter parm = new MySql.Data.MySqlClient.MySqlParameter("@MQID", MySql.Data.MySqlClient.MySqlDbType.Int32, 4);
            //parm.Value = mqId;

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                Model.MQStructure.IMGroupList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMGroupList();
                    model.MQId = mqId.ToString();
                    if (dr[0].ToString() != "")
                        model.GroupName = System.Text.UTF8Encoding.GetEncoding("gbk").GetString(System.Text.Encoding.GetEncoding("latin1").GetBytes(dr[0].ToString()));
                    else
                        model.GroupName = "";
                    if (!dr.IsDBNull(1))
                        model.FriendCount = dr.GetInt32(1);

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }

        /// <summary>
        /// 获取IM好友信息(从mysql库获取数据)
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        private IList<Model.MQStructure.IMFriendList> GetFriends(int mqId)
        {
            IList<Model.MQStructure.IMFriendList> List = new List<Model.MQStructure.IMFriendList>();

            DbCommand dc = base.MySQLStore.GetSqlStringCommand(string.Format(MYSQL_SELECT_GetFriends, mqId));

            //MySql.Data.MySqlClient.MySqlParameter parm = new MySql.Data.MySqlClient.MySqlParameter("@MQID", MySql.Data.MySqlClient.MySqlDbType.Int32, 4);
            //parm.Value = mqId;

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                Model.MQStructure.IMFriendList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMFriendList();
                    model.MQId = mqId.ToString();
                    model.FriendMQId = dr[0].ToString();
                    if (dr[1].ToString() != "")
                        model.GroupName = System.Text.UTF8Encoding.GetEncoding("gbk").GetString(System.Text.Encoding.GetEncoding("latin1").GetBytes(dr[1].ToString()));
                    else
                        model.GroupName = "";

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }

        /// <summary>
        /// 获得已添加的MQ好友数量
        /// </summary>
        /// <param name="mqId">MQID</param>
        /// <param name="findType">查找类型: 0全部好友  1:当天所添加的好友</param>
        /// <returns></returns>
        public virtual int GetFriendCount(int mqId, int findType)
        {
            int count = 0;
            DbCommand dc = this.MQStore.GetSqlStringCommand(SQL_SELECT_GetTodayFriendCount);
            this.MQStore.AddInParameter(dc, "MQID", DbType.Int32, mqId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this.MQStore))
            {
                if (dr.Read())
                {
                    count = dr.GetInt32(0);
                }
            }
            return count;
        }

        /// <summary>
        /// 累加好友数量
        /// </summary>
        /// <param name="mqId">MQID</param>
        public virtual void AddFriendCount(int mqId)
        {
            DbCommand dc = this.MQStore.GetSqlStringCommand(SQL_UPDATE_TodayFriendCount);
            this.MQStore.AddInParameter(dc, "MQID", DbType.Int32, mqId);
            DbHelper.ExecuteSql(dc, this.MQStore);
        }
        #endregion
    }
}
