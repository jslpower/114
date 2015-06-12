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
    /// MQ客服
    /// </summary>
    /// 周文超 2010-05-11
    public class IMServiceMQ : DALBase, IDAL.MQStructure.IIMServiceMQ
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMServiceMQ()
        { }

        /// <summary>
        /// 插入mq客服号码为好友
        /// </summary>
        /// <param name="mqId">用户的MQID</param>
        /// <param name="provinceId">用户所在的省份</param>
        /// <returns></returns>
        public virtual bool InsertFriendServiceMQ(int mqId, int provinceId)
        {
            string serviceMQIds = "";//默认客服信息
            string groupName = "同业MQ客服";

            DbCommand dcServiceMQ = base.MQStore.GetStoredProcCommand("proc_im_ServiceMQ_SELECT");
            base.MQStore.AddInParameter(dcServiceMQ, "ProvinceId", DbType.Int32, provinceId);

            using (IDataReader rdr = DbHelper.RunReaderProcedure(dcServiceMQ, base.MQStore))
            {
                if (rdr.Read())
                {
                    serviceMQIds = rdr.GetString(0);
                }
            }

            if (string.IsNullOrEmpty(serviceMQIds))
                return false;

            StringBuilder strMySQlMQ = new StringBuilder();
            StringBuilder strSQlMQ = new StringBuilder();
            string[] serviceMQIdArr = serviceMQIds.Split(',');
            string kfStr = System.Text.Encoding.GetEncoding("latin1").GetString(System.Text.Encoding.GetEncoding("gbk").GetBytes(groupName));
            strMySQlMQ.AppendFormat("INSERT INTO group_list (uid,group_name) values({0}, '{1}');", mqId, kfStr);  //mysql中的组
            strSQlMQ.AppendFormat("INSERT INTO im_grouplist (uid,groupname,friendcount,issuetime) values({0}, '{1}', {2}, getdate());", mqId, groupName, serviceMQIdArr.Length);  //sql中的组

            foreach (string s in serviceMQIdArr)
            {
                //mysql中的好友
                strMySQlMQ.AppendFormat("INSERT INTO friend_list (uid,friend_id,group_name) values({0}, {1}, '{2}');", mqId, s, kfStr);
                //sql中的好友
                strSQlMQ.AppendFormat(" INSERT INTO [im_friendlist]([uid],[friendid],[groupname],[issuetime]) VALUES ('{0}','{1}','{2}',getdate()); ", mqId, s, groupName);
            }
            DbCommand dcSql = base.MySQLStore.GetSqlStringCommand(strMySQlMQ.ToString());
            bool Result = false;
            try
            {
                Result = DbHelper.ExecuteSql(dcSql, base.MySQLStore) > 0 ? true : false;
                if (Result)
                {
                    dcSql = base.MQStore.GetSqlStringCommand(strSQlMQ.ToString());
                    DbHelper.ExecuteSql(dcSql, base.MQStore);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return Result;
        }
    }
}
