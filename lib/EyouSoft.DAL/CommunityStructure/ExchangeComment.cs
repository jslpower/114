using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-21
    /// 描述：互动交流评论数据层
    /// </summary>
    public class ExchangeComment : DALBase, IDAL.CommunityStructure.IExchangeComment
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeComment() { }

        private const string Sql_ExchangeComment_Add = " INSERT INTO [tbl_ExchangeComment]([ID],[TopicId],[TopicClassId],[CommentId],[CompanyId],[CompanyName],[OperatorId],[OperatorName],[OperatorMQ],[CommentText],[IssueTime],[IsHasNextLevel],[IsDeleted],[IsAnonymous],[CommentIP],[IsCheck]) VALUES (@ID,@TopicId,@TopicClassId,@CommentId,@CompanyId,@CompanyName,@OperatorId,@OperatorName,@OperatorMQ,@CommentText,@IssueTime,@IsHasNextLevel,@IsDeleted,@IsAnonymous,@CommentIP,@IsCheck) ";
        private const string Sql_ExchangeComment_Delete = " DELETE FROM [tbl_ExchangeComment] ";
        private const string Sql_ExchangeComment_Select = " SELECT {0} [ID],[TopicId],[TopicClassId],[CommentId],[CompanyId],[CompanyName],[OperatorId],[OperatorName],[OperatorMQ],[CommentText],[IssueTime],[IsHasNextLevel],[IsDeleted],[IsAnonymous],[CommentIP],[IsCheck] FROM [tbl_ExchangeComment] where 1 = 1 ";
        private const string Sql_ExchangeComment_UpdateIsCheck = " UPDATE [tbl_ExchangeComment] SET [IsCheck] = @IsCheck WHERE [ID] in (";
        private const string Sql_ExchangeComment_UpdateIsDelete = " UPDATE [tbl_ExchangeComment] SET [IsDeleted] = @IsDeleted WHERE [ID] in (";
        private const string Sql_ExchangeComment_UpdateIsHasNextLevel = " UPDATE [tbl_ExchangeComment] SET [IsHasNextLevel] = @IsHasNextLevel WHERE [ID] = @ID ";
        private const string Sql_ExchangeComment_Count = " select count(ID) from tbl_ExchangeComment where TopicId = @TopicId and TopicClassId = @TopicClassId ";


        #region IExchangeComment 成员

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="model">交流评论实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddExchangeComment(EyouSoft.Model.CommunityStructure.ExchangeComment model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_ExchangeComment_Add);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            base.SystemStore.AddInParameter(dc, "TopicId", DbType.String, model.TopicId);
            base.SystemStore.AddInParameter(dc, "TopicClassId", DbType.Byte, (int)model.TopicType);
            base.SystemStore.AddInParameter(dc, "CommentId", DbType.String, model.CommentId);
            base.SystemStore.AddInParameter(dc, "CompanyId", DbType.String, model.CompanyId);
            base.SystemStore.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            base.SystemStore.AddInParameter(dc, "OperatorId", DbType.String, model.OperatorId);
            base.SystemStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            base.SystemStore.AddInParameter(dc, "OperatorMQ", DbType.String, model.OperatorMQ);
            base.SystemStore.AddInParameter(dc, "CommentText", DbType.String, model.CommentText);
            base.SystemStore.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            base.SystemStore.AddInParameter(dc, "IsHasNextLevel", DbType.String, model.IsHasNextLevel ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "IsDeleted", DbType.String, "0");
            base.SystemStore.AddInParameter(dc, "IsAnonymous", DbType.String, model.IsAnonymous ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "CommentIP", DbType.String, model.CommentIP);
            base.SystemStore.AddInParameter(dc, "IsCheck", DbType.String, "0");

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="IsCheck">是否审核</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsCheck(bool IsCheck, params string[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            StringBuilder srtSql = new StringBuilder(Sql_ExchangeComment_UpdateIsCheck);
            for (int i = 0; i < CommentIds.Length; i++)
            {
                srtSql.AppendFormat("{0}{1}{2}", i == 0 ? "" : ",", "@PARM", i);
            }

            srtSql.Append(") ");
            DbCommand dc = base.SystemStore.GetSqlStringCommand(srtSql.ToString());
            base.SystemStore.AddInParameter(dc, "IsCheck", DbType.AnsiStringFixedLength, IsCheck ? "1" : "0");
            for (int i = 0; i < CommentIds.Length; i++)
            {
                base.SystemStore.AddInParameter(dc, "PARM" + i, DbType.AnsiStringFixedLength, CommentIds[i]);
            }

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 更新是否有子级评论
        /// </summary>
        /// <param name="CommentId">评论ID</param>
        /// <param name="IsHasNextLevel">否有子级评论</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsHasNextLevel(string CommentId, bool IsHasNextLevel)
        {
            if (string.IsNullOrEmpty(CommentId))
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_ExchangeComment_UpdateIsHasNextLevel);
            base.SystemStore.AddInParameter(dc, "IsHasNextLevel", DbType.AnsiStringFixedLength, IsHasNextLevel ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, CommentId);

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 虚拟删除评论
        /// </summary>
        /// <param name="IsDelete">是否虚拟删除</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsDelete(bool IsDelete, params string[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            StringBuilder srtSql = new StringBuilder(Sql_ExchangeComment_UpdateIsDelete);
            for (int i = 0; i < CommentIds.Length; i++)
            {
                srtSql.AppendFormat("{0}{1}{2}", i == 0 ? "" : ",", "@PARM", i);
            }

            srtSql.Append(") ");
            DbCommand dc = base.SystemStore.GetSqlStringCommand(srtSql.ToString());
            base.SystemStore.AddInParameter(dc, "IsDeleted", DbType.AnsiStringFixedLength, IsDelete ? "1" : "0");
            for (int i = 0; i < CommentIds.Length; i++)
            {
                base.SystemStore.AddInParameter(dc, "PARM" + i, DbType.AnsiStringFixedLength, CommentIds[i]);
            }

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="CommentIds">评论ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(params string[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            StringBuilder srtSql = new StringBuilder(Sql_ExchangeComment_Delete + " where ID in (");
            for (int i = 0; i < CommentIds.Length; i++)
            {
                srtSql.AppendFormat("{0}{1}{2}", i == 0 ? "" : ",", "@PARM", i);
            }

            srtSql.Append(") ");
            DbCommand dc = base.SystemStore.GetSqlStringCommand(srtSql.ToString());
            for (int i = 0; i < CommentIds.Length; i++)
            {
                base.SystemStore.AddInParameter(dc, "PARM" + i, DbType.AnsiStringFixedLength, CommentIds[i]);
            }

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 根据互动交流删除其下所有评论
        /// </summary>
        /// <param name="TopicId">互动交流ID</param>
        /// <param name="TopicType">评论主题类型</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool DeleteByExchange(string TopicId,EyouSoft.Model.CommunityStructure.TopicType TopicType)
        {
            if (string.IsNullOrEmpty(TopicId))
                return false;

            string strWhere = Sql_ExchangeComment_Delete + " where TopicId = @TopicId and TopicClassId=@TopicClassId";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "TopicId", DbType.AnsiStringFixedLength, TopicId);
            base.SystemStore.AddInParameter(dc, "TopicClassId", DbType.Byte, (int)TopicType);
            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取互动交流下的评论
        /// </summary>
        /// <param name="TopNum">top条数(小于等于0取所有)</param>
        /// <param name="TopicId">互动交流ID(必须传值)</param>
        /// <param name="TopicType">评论类型(为null不作条件)</param>
        /// <param name="CommentId">所回复的评论编号(小于等于0不作条件)</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetExchangeCommentList(int TopNum, string TopicId, EyouSoft.Model.CommunityStructure.TopicType? TopicType, string CommentId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeComment> List = new List<EyouSoft.Model.CommunityStructure.ExchangeComment>();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(Sql_ExchangeComment_Select, TopNum > 0 ? string.Format(" top {0} ", TopNum) : string.Empty);
            if (!string.IsNullOrEmpty(TopicId))
                strSql.AppendFormat(" and TopicId = '{0}' ", TopicId);
            if (TopicType.HasValue)
                strSql.AppendFormat(" and TopicClassId = {0} ", (int)TopicType.Value);
            if (!string.IsNullOrEmpty(CommentId))
            {
                strSql.AppendFormat(" and CommentId = '{0}' ", CommentId);
            }
            else
            {
                strSql.Append(" and CommentId = '' ");
            }
            strSql.Append(" order by IssueTime asc ");
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.CommunityStructure.ExchangeComment model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.ExchangeComment();
                    model.ID = dr["ID"].ToString();
                    model.TopicId = dr["TopicId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TopicClassId")))
                        model.TopicType = (EyouSoft.Model.CommunityStructure.TopicType)int.Parse(dr["TopicClassId"].ToString());
                    model.CommentId = dr["CommentId"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.OperatorMQ = dr["OperatorMQ"].ToString();
                    model.CommentText = dr["CommentText"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.CommentIP = dr["CommentIP"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsHasNextLevel")) && dr["IsHasNextLevel"].ToString().Equals("1"))
                        model.IsHasNextLevel = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")) && dr["IsDeleted"].ToString().Equals("1"))
                        model.IsDeleted = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsAnonymous")) && dr["IsAnonymous"].ToString().Equals("1"))
                        model.IsAnonymous = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsCheck")) && dr["IsCheck"].ToString().Equals("1"))
                        model.IsCheck = true;

                    List.Add(model);
                }
            }
            return List;
        }

        /// <summary>
        /// 分页获取互动交流下的评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">互动交流ID(必须传值)</param>
        /// <param name="TopicType">评论类型(为null不作条件)</param>
        /// <param name="CommentId">所回复的评论编号(小于等于0不作条件)</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetExchangeCommentList(int PageSize, int PageIndex, ref int RecordCount, string TopicId, EyouSoft.Model.CommunityStructure.TopicType? TopicType, string CommentId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeComment> List = new List<EyouSoft.Model.CommunityStructure.ExchangeComment>();
            StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
            string strFiles = " [ID],[TopicId],[TopicClassId],[CommentId],[CompanyId],[CompanyName],[OperatorId],[OperatorName],[OperatorMQ],[CommentText],[IssueTime],[IsHasNextLevel],[IsDeleted],[IsAnonymous],[CommentIP],[IsCheck] ";
            string strOrder = " IssueTime asc ";

            if (!string.IsNullOrEmpty(TopicId))
                strWhere.AppendFormat(" and TopicId = '{0}' ", TopicId);
            if (TopicType.HasValue)
                strWhere.AppendFormat(" and TopicClassId = {0} ", (int)TopicType.Value);
            if (!string.IsNullOrEmpty(CommentId))
            {
                strWhere.AppendFormat(" and CommentId = '{0}' ", CommentId);
            }
            else
            {
                strWhere.Append(" and CommentId = '' ");
            }
            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_ExchangeComment", "[ID]", strFiles, strWhere.ToString(), strOrder))
            {
                EyouSoft.Model.CommunityStructure.ExchangeComment model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.ExchangeComment();
                    model.ID = dr["ID"].ToString();
                    model.TopicId = dr["TopicId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TopicClassId")))
                        model.TopicType = (EyouSoft.Model.CommunityStructure.TopicType)int.Parse(dr["TopicClassId"].ToString());
                    model.CommentId = dr["CommentId"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.OperatorMQ = dr["OperatorMQ"].ToString();
                    model.CommentText = dr["CommentText"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.CommentIP = dr["CommentIP"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsHasNextLevel")) && dr["IsHasNextLevel"].ToString().Equals("1"))
                        model.IsHasNextLevel = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")) && dr["IsDeleted"].ToString().Equals("1"))
                        model.IsDeleted = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsAnonymous")) && dr["IsAnonymous"].ToString().Equals("1"))
                        model.IsAnonymous = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsCheck")) && dr["IsCheck"].ToString().Equals("1"))
                        model.IsCheck = true;

                    List.Add(model);
                }
            }

            return List;
        }

        /// <summary>
        /// 分页获取互动交流下的评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">互动交流ID(必须传值)</param>
        /// <param name="TopicType">评论类型(为null不作条件)</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetExchangeCommentList(int PageSize, int PageIndex, ref int RecordCount, string TopicId, EyouSoft.Model.CommunityStructure.TopicType? TopicType)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeComment> List = new List<EyouSoft.Model.CommunityStructure.ExchangeComment>();
            StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
            string strFiles = " [ID],[TopicId],[TopicClassId],[CommentId],[CompanyId],[CompanyName],[OperatorId],[OperatorName],[OperatorMQ],[CommentText],[IssueTime],[IsHasNextLevel],[IsDeleted],[IsAnonymous],[CommentIP],[IsCheck] ";
            string strOrder = " IssueTime asc ";

            if (!string.IsNullOrEmpty(TopicId))
                strWhere.AppendFormat(" and TopicId = '{0}' ", TopicId);
            if (TopicType.HasValue)
                strWhere.AppendFormat(" and TopicClassId = {0} ", (int)TopicType.Value);
            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_ExchangeComment", "[ID]", strFiles, strWhere.ToString(), strOrder))
            {
                EyouSoft.Model.CommunityStructure.ExchangeComment model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.ExchangeComment();
                    model.ID = dr["ID"].ToString();
                    model.TopicId = dr["TopicId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TopicClassId")))
                        model.TopicType = (EyouSoft.Model.CommunityStructure.TopicType)int.Parse(dr["TopicClassId"].ToString());
                    model.CommentId = dr["CommentId"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.OperatorMQ = dr["OperatorMQ"].ToString();
                    model.CommentText = dr["CommentText"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.CommentIP = dr["CommentIP"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsHasNextLevel")) && dr["IsHasNextLevel"].ToString().Equals("1"))
                        model.IsHasNextLevel = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")) && dr["IsDeleted"].ToString().Equals("1"))
                        model.IsDeleted = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsAnonymous")) && dr["IsAnonymous"].ToString().Equals("1"))
                        model.IsAnonymous = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsCheck")) && dr["IsCheck"].ToString().Equals("1"))
                        model.IsCheck = true;

                    List.Add(model);
                }
            }

            return List;
        }
        /// <summary>
        /// 获取回复总数
        /// </summary>
        /// <param name="TopicId">信息ID</param>
        /// <param name="TopicType">信息类型</param>
        /// <returns>回复总数</returns>
        public virtual int GetCommentCountByIdAndType(string TopicId, EyouSoft.Model.CommunityStructure.TopicType TopicType)
        {
            if (string.IsNullOrEmpty(TopicId))
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_ExchangeComment_Count);
            base.SystemStore.AddInParameter(dc, "TopicId", DbType.AnsiStringFixedLength, TopicId);
            base.SystemStore.AddInParameter(dc, "TopicClassId", DbType.Byte, (int)TopicType);

            object obj = DbHelper.GetSingle(dc, base.SystemStore);
            if (obj.Equals(null))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        #endregion
    }
}
