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
    /// MQ消息提醒中转地址
    /// </summary>
    /// 周文超 2010-05-11
    public class IMMessageUrl : DALBase, IDAL.MQStructure.IIMMessageUrl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMessageUrl()
        { }

        private const string SQL_IMMessageUrl_SELECT = "SELECT url.uid,url.RedirectUrl,im.im_password FROM im_message_url AS url,im_member AS im WHERE ID=@ID AND url.uid=im.im_uid;";

        #region  成员方法

        /// <summary>
        /// 在im_message_url表中查询中转地址实体
        /// </summary>
        /// <param name="Id">im_message_url表ID</param>
        /// <returns></returns>
        public virtual Model.MQStructure.IMMessageUrl GetIm_Message_Url(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return null;

            Model.MQStructure.IMMessageUrl model = new EyouSoft.Model.MQStructure.IMMessageUrl();
            DbCommand dc = base.MQStore.GetSqlStringCommand(SQL_IMMessageUrl_SELECT);
            base.MQStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                if (dr.Read())
                {
                    model.RedirectUrl = dr.IsDBNull(dr.GetOrdinal("RedirectUrl")) == true ? "" : dr.GetString(dr.GetOrdinal("RedirectUrl"));
                    model.MQID = dr.IsDBNull(dr.GetOrdinal("uid")) == true ? 0 : Convert.ToInt32(dr.GetString(dr.GetOrdinal("uid")));
                    model.MD5Pw = dr.IsDBNull(dr.GetOrdinal("im_password")) == true ? "" : dr.GetString(dr.GetOrdinal("im_password"));
                }
            }
            return model;
        }

        #endregion  成员方法
    }
}
