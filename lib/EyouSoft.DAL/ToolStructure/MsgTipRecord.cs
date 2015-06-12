using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.ToolStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ToolStructure
{
    /// <summary>
    ///  消息提醒记录
    /// </summary>
    /// Author:张志瑜  2010-10-21
    public class MsgTipRecord : DALBase, IMsgTipRecord
    {
        #region SQL变量定义

        #endregion SQL变量定义
        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public MsgTipRecord()
        {
            this._database = base.CompanyStore;
        }


        #region 成员方法

        /// <summary>
        /// 获取短信剩余条数
        /// </summary>
        /// <returns></returns>
        public virtual int GetSmsRemain()
        {
            int count = 0;
            string SQL = "SELECT count FROM tbl_MsgTip_SmsRemain";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                    count = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
            }
            return count;
        }

        /// <summary>
        /// 获取当天短信总条数
        /// </summary>
        /// <returns></returns>
        public virtual int GetTodayCount()
        {
            int count = 0;
            string SQL = "SELECT COUNT(*) AS Num FROM tbl_MsgTipList WHERE SendWay=@SendWay AND DATEDIFF(dd,SendTime,GETDATE())=0";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "SendWay", DbType.Byte, EyouSoft.Model.ToolStructure.MsgSendWay.SMS);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                    count = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
            }
            return count;
        }


        /// <summary>
        /// 是否连续发送
        /// </summary>
        /// <param name="mqID">MQ号</param>
        /// <param name="validNum">连续数</param>
        /// <returns></returns>
        public virtual bool IsValid(string mqID, int validNum)
        {
            int count = 0;
            string SQL = "SELECT COUNT(*) AS Num FROM tbl_MsgTipList WHERE Mobile in (select ContactMobile from tbl_CompanyUser where mq=@ToMQID) and SendWay=@SendWay and msgtype IN (2,3,4) and issuetime between dateadd(dd, -3 ,getdate()) and getdate()";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "ToMQID", DbType.AnsiString, mqID);
            this._database.AddInParameter(dc, "SendWay", DbType.Byte, EyouSoft.Model.ToolStructure.MsgSendWay.SMS);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                    count = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
            }
            return count > validNum;
        }

        /// <summary>
        /// 是否被禁用
        /// </summary>
        /// <param name="mqID">MQ号</param>
        /// <returns></returns>
        public virtual bool IsValid(string mqID)
        {
            int count = 0;
            string SQL = "SELECT COUNT(*) AS Num FROM tbl_MsgTip_DisabledList WHERE MQId=@ToMQID";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "ToMQID", DbType.AnsiString, mqID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                    count = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
            }
            return count > 0;
        }

        /// <summary>
        /// 获取当天提醒次数
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="sendWay">消息发送途径</param>
        /// <param name="mqID">接收消息用户MQID</param>
        /// <returns></returns>
        public virtual int GetTodayCount(EyouSoft.Model.ToolStructure.MsgType msgType, EyouSoft.Model.ToolStructure.MsgSendWay sendWay, string mqID)
        {
            int count = 0;
            string SQL = "SELECT COUNT(*) AS Num FROM tbl_MsgTipList WHERE Mobile in (select ContactMobile from tbl_CompanyUser where mq=@ToMQID) AND MsgType=@MsgType AND SendWay=@SendWay AND DATEDIFF(dd,SendTime,GETDATE())=0";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "ToMQID", DbType.AnsiString, mqID);
            this._database.AddInParameter(dc, "MsgType", DbType.Byte, msgType);
            this._database.AddInParameter(dc, "SendWay", DbType.Byte, sendWay);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                    count = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
            }
            return count;
        }

        /// <summary>
        /// 新增禁用记录
        /// </summary>
        /// <param name="mqID">MQ</param>
        /// <param name="EnableTime">启用时间</param>
        /// <returns></returns>
        public virtual bool Add(string mqID, DateTime EnableTime)
        {
            string SQL = "insert into tbl_MsgTip_DisabledList(MQId,EnableTime)values(@ToMQID,@EnableTime);";
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "ToMQID", DbType.AnsiString, mqID);
            this._database.AddInParameter(dc, "EnableTime", DbType.DateTime, EnableTime);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">消息提醒记录实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.ToolStructure.MsgTipRecord model)
        {
            string SQL = "INSERT INTO tbl_MsgTipList (ID,ToMQID,Mobile,Email,MsgType,SendWay,FromMQID,SendTime,SendState,IssueTime) VALUES (@ID,@ToMQID,@Mobile,@Email,@MsgType,@SendWay,@FromMQID,@SendTime,@SendState,@IssueTime);";
            if (model.SendWay == EyouSoft.Model.ToolStructure.MsgSendWay.SMS)
            {
                SQL += "UPDATE tbl_MsgTip_SmsRemain SET count=count-1;";
            }
            DbCommand dc = this._database.GetSqlStringCommand(SQL);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "ToMQID", DbType.AnsiString, model.ToMQID);
            this._database.AddInParameter(dc, "Mobile", DbType.String, model.Mobile);
            this._database.AddInParameter(dc, "Email", DbType.String, model.Email);            
            this._database.AddInParameter(dc, "MsgType", DbType.Byte, Convert.ToByte(model.MsgType));
            this._database.AddInParameter(dc, "SendWay", DbType.Byte, Convert.ToByte(model.SendWay));
            this._database.AddInParameter(dc, "FromMQID", DbType.AnsiString, model.FromMQID);
            this._database.AddInParameter(dc, "SendTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "SendState", DbType.Byte, 0);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        #endregion 成员方法
    }
}
