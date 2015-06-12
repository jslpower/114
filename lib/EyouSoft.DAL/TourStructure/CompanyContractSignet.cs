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

namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 盖章数据访问类
    /// </summary>
    /// Author:汪奇志 2010-07-08
    public class CompanyContractSignet : DALBase, EyouSoft.IDAL.TourStructure.ICompanyContractSignet
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CompanyContractSignet() { }

        #region static constants
        //static constants
        private const string SQL_INSERT_ToStamp = "INSERT INTO tbl_CompanyContractSignet(ContractId,ContractType,SignetPath,SignetType) VALUES(@ContractId,@ContractType,@SignetPath,@SignetType)";
        private const string SQL_DELETE_CancelStamp = "DELETE FROM tbl_CompanyContractSignet WHERE ContractId=@ContractId AND ContractType=@ContractType AND SignetType=@SignetType";
        private const string SQL_SELECT_GetGroupAdviceSignet = "SELECT SignetPath FROM tbl_CompanyContractSignet WHERE ContractId=@ContractId AND ContractType=1";
        private const string SQL_SELECT_GetConfirmationSignet = "SELECT SignetPath,SignetType FROM tbl_CompanyContractSignet WHERE ContractId=@ContractId AND ContractType=2";
        #endregion

        #region 成员方法
        /// <summary>
        /// 盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="signetPath">图章路径</param>
        /// <param name="contractType">1:出团通知书 2:确认单</param>
        /// <param name="signetType">1:买方 2:卖方</param>
        /// <returns></returns>
        public virtual bool ToStamp(string orderId, string signetPath, int contractType, int signetType)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_INSERT_ToStamp);

            base.TourStore.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, orderId);
            base.TourStore.AddInParameter(cmd, "ContractType", DbType.Byte, contractType);
            base.TourStore.AddInParameter(cmd, "SignetPath", DbType.String, signetPath);
            base.TourStore.AddInParameter(cmd, "SignetType", DbType.Byte, signetType);

            return DbHelper.ExecuteSql(cmd, base.TourStore) == 1 ? true : false;
        }

        /// <summary>
        /// 取消盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="contractType">1:出团通知书 2:确认单</param>
        /// <param name="signetType">1:买方 2:卖方</param>
        /// <returns></returns>
        public virtual bool CancelStamp(string orderId, int contractType, int signetType)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_DELETE_CancelStamp);

            base.TourStore.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, orderId);
            base.TourStore.AddInParameter(cmd, "ContractType", DbType.Byte, contractType);
            base.TourStore.AddInParameter(cmd, "SignetType", DbType.Byte, signetType);

            return DbHelper.ExecuteSql(cmd, base.TourStore) == 1 ? true : false;
        }

        /// <summary>
        /// 获取出团通知书盖章信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual string GetGroupAdviceSignet(string orderId)
        {
            string signetPath = "";
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetGroupAdviceSignet);
            base.TourStore.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    signetPath = rdr[0].ToString();
                }
            }

            return signetPath;
        }

        /// <summary>
        /// 获取确认单盖章信息 string[2] [0]:买方 [1]:卖方
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>string[2] [0]:买方 [1]:卖方</returns>
        public virtual string[] GetConfirmationSignet(string orderId)
        {
            string[] signetPath = new string[2];

            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetConfirmationSignet);
            base.TourStore.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    if (rdr.GetByte(1) == 1)
                    {
                        signetPath[0] = rdr[0].ToString();
                    }
                    else
                    {
                        signetPath[1] = rdr[0].ToString();
                    }
                }
            }

            return signetPath;
        }
        #endregion
    }
}
