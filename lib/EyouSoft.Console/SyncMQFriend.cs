using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model.MQStructure;

namespace EyouSoft.ConsoleApp
{
    /// <summary>
    /// MQ好友同步
    /// </summary>
    public class SyncMQFriend
    {
        public static void Run()
        {
            MQServices dal = new MQServices();
            foreach (System.Data.DataRow dr in dal.GetMQMemberList(true))
            {
                if (dr.Equals(null) || string.IsNullOrEmpty(dr["im_uid"].ToString()))
                    continue;

                dal.InStepFriendGroups(int.Parse(dr["im_uid"].ToString()));
                dal.InStepFriends(int.Parse(dr["im_uid"].ToString()));
            }
        }
    }

    public class MQServices : EyouSoft.Common.DAL.DALBase
    {

        private const string MYSQL_SELECT_GetFriendGroups = "SELECT A.group_name AS GroupName,(SELECT COUNT(*) FROM friend_list AS B WHERE B.uid=A.uid AND B.group_name=A.group_name) AS FriendCount FROM group_list AS A WHERE A.uid={0}; ";
        private const string MYSQL_SELECT_GetFriends = "SELECT friend_id,group_name FROM friend_list WHERE uid={0};";

        #region IMQServices 成员

        /// <summary>
        /// 获取MQ用户集合
        /// </summary>
        /// <param name="executeOnAll"></param>
        /// <returns></returns>
        public IEnumerable<System.Data.DataRow> GetMQMemberList(bool executeOnAll)
        {
            IEnumerable<System.Data.DataRow> MQMemberList = Enumerable.Empty<System.Data.DataRow>();
            DbCommand dc = base.MQStore.GetSqlStringCommand(" SELECT [im_uid],[im_password],[im_status],[im_latest_time],[im_displayname],[im_username],[bs_uid] FROM [dbo].[im_member] ");
            using (DataTable dt = DbHelper.DataTableQuery(dc, this.MQStore))
            {
                MQMemberList = dt.Select();
            }
            return MQMemberList;
        }

        /// <summary>
        /// 同步好友信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        public bool InStepFriends(int mqId)
        {
            if (mqId <= 0)
                return false;

            IList<IMGroupList> GroupList = GetFriendGroups(mqId);
            if (GroupList == null || GroupList.Count <= 0)
                return false;

            string strDelGroup = " DELETE FROM [im_grouplist] WHERE [uid] = '{0}'; ";
            string strInToGroup = " INSERT INTO [im_grouplist]([uid],[groupname],[friendcount],[issuetime]) VALUES ('{0}','{1}',{2},'{3}'); ";
            StringBuilder strSqlGroup = new StringBuilder();
            strSqlGroup.AppendFormat(strDelGroup, mqId);
            foreach (IMGroupList model in GroupList)
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
        /// 同步好友分组信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        public bool InStepFriendGroups(int mqId)
        {
            if (mqId <= 0)
                return false;

            IList<IMGroupList> GroupList = GetFriendGroups(mqId);
            if (GroupList == null || GroupList.Count <= 0)
                return false;

            string strDelGroup = " DELETE FROM [im_grouplist] WHERE [uid] = '{0}'; ";
            string strInToGroup = " INSERT INTO [im_grouplist]([uid],[groupname],[friendcount],[issuetime]) VALUES ('{0}','{1}',{2},'{3}'); ";
            StringBuilder strSqlGroup = new StringBuilder();
            strSqlGroup.AppendFormat(strDelGroup, mqId);
            foreach (IMGroupList model in GroupList)
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

        #endregion

        #region 私有

        /// <summary>
        /// 获取IM好友信息(从mysql库获取数据)
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        private IList<IMFriendList> GetFriends(int mqId)
        {
            IList<IMFriendList> List = new List<IMFriendList>();

            DbCommand dc = base.MySQLStore.GetSqlStringCommand(string.Format(MYSQL_SELECT_GetFriends, mqId));

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                IMFriendList model = null;
                while (dr.Read())
                {
                    model = new IMFriendList();
                    model.MQId = mqId.ToString();
                    model.FriendMQId = dr[0].ToString();
                    if(dr[1].ToString()!="")
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
        /// 获取IM好友分组信息(从mysql库获取数据)
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        private IList<IMGroupList> GetFriendGroups(int mqId)
        {
            IList<IMGroupList> List = new List<IMGroupList>();

            DbCommand dc = base.MySQLStore.GetSqlStringCommand(string.Format(MYSQL_SELECT_GetFriendGroups, mqId));

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                IMGroupList model = null;
                while (dr.Read())
                {
                    model = new IMGroupList();
                    model.MQId = mqId.ToString();
                    if(dr[0].ToString()!="")
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

        #endregion
    }
}
