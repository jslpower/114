using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 乘客信息数据操作
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class PassengerInformation:DALBase,EyouSoft.IDAL.TicketStructure.IPassengerInformation
    {

        #region 构造函数

        Database _database = null;

        public PassengerInformation()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region sql

        const string SQL_PassengerInformation_GetPassengerInformation = "SELECT ID,GroupTicketsID,UName,PassengerType,DocumentType,DocumentNo,Mobile FROM tbl_PassengerInformation WHERE GroupTicketsID = @GroupTicketsID";

        #endregion

        #region IPassengerInformation 成员

        /// <summary>
        /// 根据团队票编号获取乘客信息
        /// </summary>
        /// <param name="groupTicketID">团队票编号</param>
        /// <returns>乘客信息集合</returns>
        public IList<EyouSoft.Model.TicketStructure.PassengerInformation> GetPassengerInformation(int groupTicketID)
        {
            IList<EyouSoft.Model.TicketStructure.PassengerInformation> list = new List<EyouSoft.Model.TicketStructure.PassengerInformation>();

            EyouSoft.Model.TicketStructure.PassengerInformation item = null;

            DbCommand comm = this._database.GetSqlStringCommand(SQL_PassengerInformation_GetPassengerInformation);
            this._database.AddInParameter(comm, "@GroupTicketsID", DbType.Int32, groupTicketID);
            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._database))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.TicketStructure.PassengerInformation()
                    {
                        ID = (int)reader["ID"],
                        GroupTicketsID = (int)reader["GroupTicketsID"],
                        UName = reader.IsDBNull(reader.GetOrdinal("UName")) ? "" : reader["UName"].ToString(),
                        DocumentNo = reader.IsDBNull(reader.GetOrdinal("DocumentNo")) ? "" : reader["DocumentNo"].ToString(),
                        Mobile = reader.IsDBNull(reader.GetOrdinal("Mobile")) ? "" : reader["Mobile"].ToString()
                    };
                    if (!reader.IsDBNull(reader.GetOrdinal("PassengerType")))
                        item.PassengerType = (EyouSoft.Model.TicketStructure.PassengerType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.PassengerType), reader["PassengerType"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("DocumentType")))
                        item.DocumentType = (EyouSoft.Model.TicketStructure.DocumentType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.DocumentType), reader["DocumentType"].ToString());

                    list.Add(item);
                }
            }

            return list;
        }

        #endregion
    }
}
