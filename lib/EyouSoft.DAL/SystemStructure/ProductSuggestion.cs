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

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 意见反馈数据访问类
    /// </summary>
    /// Author:汪奇志 2010-07-19
    public class ProductSuggestion:DALBase,EyouSoft.IDAL.SystemStructure.IProductSuggestion
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_InsertSuggestionInfo = "INSERT INTO [tbl_ProductSuggestion]([ID],[TypeID],[CompanyID],[CompanyName],[ContactName],[ContactTel],[ContactMobile],[QQ],[MQ],[ContentText]) VALUES(@ID,@TypeID,@CompanyID,@CompanyName,@ContactName,@ContactTel,@ContactMobile,@QQ,@MQ,@ContentText)";
        private const string SQL_SELECT_GetSuggestionInfo = "SELECT * FROM [tbl_ProductSuggestion] WHERE Id=@SuggestionId";
        private const string SQL_DELETE_DeleteSuggestionInfo = "DELETE FROM [tbl_ProductSuggestion] WHERE Id=@SuggestionId";
        #endregion

        #region 成员方法
        /// <summary>
        /// 写入意见反馈信息
        /// </summary>
        /// <param name="info">意见反馈信息业务实体</param>
        /// <returns></returns>
        public virtual bool InsertSuggestionInfo(EyouSoft.Model.SystemStructure.ProductSuggestionInfo info)
        {
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_INSERT_InsertSuggestionInfo);

            base.SystemStore.AddInParameter(cmd, "ID", DbType.String, info.SuggestionId);
            base.SystemStore.AddInParameter(cmd, "TypeID", DbType.Byte, (int)info.SuggestionType);
            base.SystemStore.AddInParameter(cmd, "CompanyID", DbType.String, info.CompanyId);
            base.SystemStore.AddInParameter(cmd, "CompanyName", DbType.String, info.CompanyName);
            base.SystemStore.AddInParameter(cmd, "ContactName", DbType.String, info.ContactName);
            base.SystemStore.AddInParameter(cmd, "ContactTel", DbType.String, info.ContactTel);
            base.SystemStore.AddInParameter(cmd, "ContactMobile", DbType.String, info.ContactMobile);
            base.SystemStore.AddInParameter(cmd, "QQ", DbType.String, info.QQ);
            base.SystemStore.AddInParameter(cmd, "MQ", DbType.String, info.MQ);
            base.SystemStore.AddInParameter(cmd, "ContentText", DbType.String, info.ContentText);

            return DbHelper.ExecuteSql(cmd, base.SystemStore) == 1 ? true : false;
        }

        /// <summary>
        /// 获取意见反馈信息
        /// </summary>
        /// <param name="suggestionId">意见编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.ProductSuggestionInfo GetSuggestionInfo(string suggestionId)
        {
            EyouSoft.Model.SystemStructure.ProductSuggestionInfo info = null;
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_SELECT_GetSuggestionInfo);

            base.SystemStore.AddInParameter(cmd, "SuggestionId", DbType.AnsiStringFixedLength, suggestionId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.SystemStructure.ProductSuggestionInfo();

                    info.SuggestionId = rdr.GetString(rdr.GetOrdinal("Id"));
                    info.SuggestionType = (EyouSoft.Model.SystemStructure.ProductSuggestionType)rdr.GetByte(rdr.GetOrdinal("TypeId"));
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.CompanyName = rdr["CompanyName"].ToString();
                    info.ContactName = rdr["ContactName"].ToString();
                    info.ContactTel = rdr["ContactTel"].ToString();
                    info.ContactMobile = rdr["ContactMobile"].ToString();
                    info.QQ = rdr["QQ"].ToString();
                    info.MQ = rdr["MQ"].ToString();
                    info.ContentText = rdr["ContentText"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                }
            }

            return info;
        }

        /// <summary>
        /// 删除意见反馈信息
        /// </summary>
        /// <param name="suggestionId">意见编号</param>
        /// <returns></returns>
        public virtual bool DeleteSuggestionInfo(string suggestionId)
        {
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_DELETE_DeleteSuggestionInfo);
            base.SystemStore.AddInParameter(cmd, "SuggestionId", DbType.AnsiStringFixedLength, suggestionId);

            return DbHelper.ExecuteSql(cmd, base.SystemStore) == 1 ? true : false;
        }

        /// <summary>
        /// 获取意见反馈信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="contactName">联系人 为null时不做为查询条件</param>
        /// <param name="content">内容 为null时不做为查询条件</param>
        /// <param name="suggestionType">意见反馈类别 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.ProductSuggestionInfo> GetSuggestions(int pageSize, int pageIndex, ref int recordCount
            , string companyName, string contactName, string content, params EyouSoft.Model.SystemStructure.ProductSuggestionType[] suggestionType)
        {
            IList<EyouSoft.Model.SystemStructure.ProductSuggestionInfo> suggestions = new List<EyouSoft.Model.SystemStructure.ProductSuggestionInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_ProductSuggestion";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.Append(" 1=1 ");

            if (suggestionType!=null)
            {
                if (suggestionType.Length == 1)
                {
                    cmdQuery.AppendFormat(" AND TypeId={0} ", (int)suggestionType[0]);
                }
                else if(suggestionType.Length>1)
                {
                    cmdQuery.Append(" AND TypeId IN (");
                    cmdQuery.Append((int)suggestionType[0]);
                    for(int i=1; i<suggestionType.Length; i++)
                    {
                        cmdQuery.Append(" ," + (int)suggestionType[i]);
                    }
                    cmdQuery.Append(" ) ");
                }
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND  CompanyName LIKE '%{0}%' ", companyName);
            }

            if (!string.IsNullOrEmpty(contactName))
            {
                cmdQuery.AppendFormat(" AND  ContactName LIKE '%{0}%' ", contactName);
            }

             if (!string.IsNullOrEmpty(content))
            {
                cmdQuery.AppendFormat(" AND  ContentText LIKE '%{0}%' ", content);
            }
            #endregion

             using (IDataReader rdr = DbHelper.ExecuteReader(base.SystemStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
             {
                 while (rdr.Read())
                 {
                     EyouSoft.Model.SystemStructure.ProductSuggestionInfo info = new EyouSoft.Model.SystemStructure.ProductSuggestionInfo();

                     info.SuggestionId = rdr.GetString(rdr.GetOrdinal("Id"));
                     info.SuggestionType = (EyouSoft.Model.SystemStructure.ProductSuggestionType)rdr.GetByte(rdr.GetOrdinal("TypeId"));
                     info.CompanyId = rdr["CompanyId"].ToString();
                     info.CompanyName = rdr["CompanyName"].ToString();
                     info.ContactName = rdr["ContactName"].ToString();
                     info.ContactTel = rdr["ContactTel"].ToString();
                     info.ContactMobile = rdr["ContactMobile"].ToString();
                     info.QQ = rdr["QQ"].ToString();
                     info.MQ = rdr["MQ"].ToString();
                     info.ContentText = rdr["ContentText"].ToString();
                     info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));

                     suggestions.Add(info);
                 }
             }

             return suggestions;
        }
        #endregion
    }
}
