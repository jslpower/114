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
    /// IM中用户自定义组
    /// </summary>
    /// 周文超 2010-05-11
    public class IMGroupList : DALBase, IDAL.MQStructure.IIMGroupList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMGroupList()
        { }

        private const string SQL_SELECT_GetFriendGroups = " SELECT A.groupname AS GroupName,(SELECT COUNT(*) FROM im_friendlist AS B INNER JOIN im_member AS C ON B.friendid=C.im_uid WHERE B.uid=A.uid AND B.groupname=A.groupname AND C.im_status>11) AS OnlineFriendCount,friendcount AS FriendCount,issuetime FROM im_grouplist AS A WHERE A.uid=@MQID ";

        #region 函数成员

        /// <summary>
        /// 获取好友分组信息集合
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        public virtual IList<Model.MQStructure.IMGroupList> GetFriendGroups(int mqId)
        {
            IList<Model.MQStructure.IMGroupList> friendGroups = new List<Model.MQStructure.IMGroupList>();

            DbCommand dc = base.MQStore.GetSqlStringCommand(SQL_SELECT_GetFriendGroups);
            base.MQStore.AddInParameter(dc, "MQID", DbType.AnsiStringFixedLength, mqId.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                Model.MQStructure.IMGroupList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMGroupList();
                    model.MQId = mqId.ToString();
                    model.GroupName = dr[0].ToString();
                    if(!dr.IsDBNull(1))
                        model.OnlineFriendCount = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.FriendCount = dr.GetInt32(2);
                    if (!dr.IsDBNull(3))
                        model.IssueTime = dr.GetDateTime(3);

                    friendGroups.Add(model);
                }
                model = null;
            }

            return friendGroups;
        }

        #endregion

    }
}
