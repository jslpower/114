using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model.NewsStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.NewsStructure
{
    /// <summary>
    /// 同业资讯数据访问
    /// </summary>
    public class DPeerNews : DALBase, IDAL.NewsStructure.IPeerNews
    {
        #region private 成员

        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 同业资讯插入sql
        /// </summary>
        private const string SqlPeerNewsAdd = @" INSERT INTO [tbl_PeerNews]
           ([NewId]
           ,[Title]
           ,[TypeId]
           ,[Content]
           ,[CompanyId]
           ,[CompanyName]
           ,[OperatorId]
           ,[OperatorName]
           ,[B2BDisplay]
           ,[SortId]
           ,[ClickNum]
           ,[IP]
           ,[IssueTime]
           ,[LastUpdateTime]
           ,[AreaId]
           ,[AreaName]
           ,[AreaType]
           ,[ScenicId]
            )
     VALUES
           (@NewId
           ,@Title
           ,@TypeId
           ,@Content
           ,@CompanyId
           ,@CompanyName
           ,@OperatorId
           ,@OperatorName
           ,@B2BDisplay
           ,@SortId
           ,@ClickNum
           ,@IP
           ,@IssueTime
           ,@LastUpdateTime
           ,@AreaId
           ,@AreaName
           ,@AreaType
           ,@ScenicId
            ); ";

        /// <summary>
        /// 用户后台更新同业资讯sql
        /// </summary>
        private const string SqlPeerNewsUpdateByCustomer = @" UPDATE [tbl_PeerNews]
   SET [Title] = @Title
      ,[TypeId] = @TypeId
      ,[Content] = @Content
      ,[LastUpdateTime] = @LastUpdateTime
      ,[AreaId] = @AreaId
      ,[AreaName] = @AreaName
      ,[AreaType] = @AreaType
      ,[ScenicId] = @ScenicId
 WHERE [NewId] = @NewId ;";

        /// <summary>
        /// 运营后台修改同业资讯sql
        /// </summary>
        private const string SqlPeerNewsUpdateByManage = @"UPDATE [tbl_PeerNews]
   SET [Title] = @Title
      ,[TypeId] = @TypeId
      ,[Content] = @Content
      ,[B2BDisplay] = @B2BDisplay
      ,[SortId] = @SortId
      ,[LastUpdateTime] = @LastUpdateTime
      ,[AreaId] = @AreaId
      ,[AreaName] = @AreaName
      ,[AreaType] = @AreaType
      ,[ScenicId] = @ScenicId
 WHERE [NewId] = @NewId ;";

        /// <summary>
        /// 同业资讯查询sql（不带where）
        /// </summary>
        private const string SqlPeerNewsSelect = @" SELECT [NewId]
      ,[Title]
      ,[TypeId]
      ,[Content]
      ,[CompanyId]
      ,[CompanyName]
      ,[OperatorId]
      ,[OperatorName]
      ,[B2BDisplay]
      ,[SortId]
      ,[ClickNum]
      ,[IP]
      ,[IssueTime]
      ,[LastUpdateTime]
      ,[AreaId]
      ,[AreaName]
      ,[AreaType]
      ,[ScenicId]
  FROM [tbl_PeerNews] ";

        /// <summary>
        /// 同业资讯附件插叙sql（不带where）
        /// </summary>
        private const string SqlPeerNewsAttachInfoSelect = @" SELECT [Id]
      ,[NewId]
      ,[Type]
      ,[Path]
      ,[FileName]
      ,[Remark]
  FROM [tbl_PeerNewsAttachInfo] ";

        /// <summary>
        /// 同业资讯附件插入sql
        /// </summary>
        private const string SqlPeerNewsAttachInfoAdd = @" INSERT INTO [tbl_PeerNewsAttachInfo]
           ([Id]
           ,[NewId]
           ,[Type]
           ,[Path]
           ,[FileName]
           ,[Remark])
     VALUES
           ('{0}'
           ,'{1}'
           ,{2}
           ,'{3}'
           ,'{4}'
           ,'{5}'); ";

        /// <summary>
        /// 根据资讯编号删除资讯附件sql
        /// </summary>
        private const string SqlPeerNewsAttachInfoDelByNewId = @" DELETE FROM [tbl_PeerNewsAttachInfo] WHERE [NewId] = @NewId ;";

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPeerNews()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 添加同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddPeerNews(MPeerNews model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Title))
                return 0;

            model.NewId = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;
            model.LastUpdateTime = model.IssueTime;
            var strSql = new StringBuilder();
            strSql.Append(SqlPeerNewsAdd);
            if (model.AttachInfo != null && model.AttachInfo.Count > 0)
            {
                foreach (var t in model.AttachInfo)
                {
                    if (t == null)
                        continue;

                    t.Id = Guid.NewGuid().ToString();
                    strSql.AppendFormat(SqlPeerNewsAttachInfoAdd, t.Id, model.NewId, (int)t.Type, t.Path, t.FileName,
                                        t.Remark);
                }
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "NewId", DbType.AnsiStringFixedLength, model.NewId);
            _db.AddInParameter(dc, "Title", DbType.String, model.Title);
            _db.AddInParameter(dc, "TypeId", DbType.Byte, (int)model.TypeId);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            _db.AddInParameter(dc, "B2BDisplay", DbType.Byte, (int)model.B2BDisplay);
            _db.AddInParameter(dc, "SortId", DbType.Int32, model.SortId);
            _db.AddInParameter(dc, "ClickNum", DbType.Int32, model.ClickNum);
            _db.AddInParameter(dc, "IP", DbType.String, model.Ip);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            _db.AddInParameter(dc, "LastUpdateTime", DbType.DateTime, model.LastUpdateTime);
            _db.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(dc, "AreaName", DbType.String, model.AreaName);
            _db.AddInParameter(dc, "AreaType", DbType.Byte, (int)model.AreaType);
            _db.AddInParameter(dc, "ScenicId", DbType.String, model.ScenicId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : -1;
        }

        /// <summary>
        /// 用户后台修改同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int CustomerUpdatePeerNews(MPeerNews model)
        {
            if (model == null || string.IsNullOrEmpty(model.NewId))
                return 0;

            model.LastUpdateTime = DateTime.Now;
            var strSql = new StringBuilder();
            strSql.Append(SqlPeerNewsUpdateByCustomer);
            if (model.AttachInfo != null && model.AttachInfo.Count > 0)
            {
                strSql.Append(SqlPeerNewsAttachInfoDelByNewId);
                foreach (var t in model.AttachInfo)
                {
                    if (t == null)
                        continue;

                    t.Id = Guid.NewGuid().ToString();
                    strSql.AppendFormat(SqlPeerNewsAttachInfoAdd, t.Id, model.NewId, (int)t.Type, t.Path, t.FileName,
                                        t.Remark);
                }
            }
            else if (model.AttachInfo == null || model.AttachInfo.Count <= 0)
            {
                strSql.AppendFormat(SqlPeerNewsAttachInfoDelByNewId);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "NewId", DbType.AnsiStringFixedLength, model.NewId);
            _db.AddInParameter(dc, "Title", DbType.String, model.Title);
            _db.AddInParameter(dc, "TypeId", DbType.Byte, (int)model.TypeId);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "LastUpdateTime", DbType.DateTime, model.LastUpdateTime);
            _db.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(dc, "AreaName", DbType.String, model.AreaName);
            _db.AddInParameter(dc, "AreaType", DbType.Byte, (int)model.AreaType);
            _db.AddInParameter(dc, "ScenicId", DbType.String, model.ScenicId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : -1;
        }

        /// <summary>
        /// 运营后台修改同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int ManageUpdatePeerNews(MPeerNews model)
        {
            if (model == null || string.IsNullOrEmpty(model.NewId))
                return 0;

            model.LastUpdateTime = DateTime.Now;
            var strSql = new StringBuilder();
            strSql.Append(SqlPeerNewsUpdateByManage);
            if (model.AttachInfo != null && model.AttachInfo.Count > 0)
            {
                strSql.Append(SqlPeerNewsAttachInfoDelByNewId);
                foreach (var t in model.AttachInfo)
                {
                    if (t == null)
                        continue;

                    t.Id = Guid.NewGuid().ToString();
                    strSql.AppendFormat(SqlPeerNewsAttachInfoAdd, t.Id, model.NewId, (int)t.Type, t.Path, t.FileName,
                                        t.Remark);
                }
            }
            else if (model.AttachInfo == null || model.AttachInfo.Count <= 0)
            {
                strSql.AppendFormat(SqlPeerNewsAttachInfoDelByNewId);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "NewId", DbType.AnsiStringFixedLength, model.NewId);
            _db.AddInParameter(dc, "Title", DbType.String, model.Title);
            _db.AddInParameter(dc, "TypeId", DbType.Byte, (int)model.TypeId);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "LastUpdateTime", DbType.DateTime, model.LastUpdateTime);
            _db.AddInParameter(dc, "B2BDisplay", DbType.Byte, (int)model.B2BDisplay);
            _db.AddInParameter(dc, "SortId", DbType.Int32, model.SortId);
            _db.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(dc, "AreaName", DbType.String, model.AreaName);
            _db.AddInParameter(dc, "AreaType", DbType.Byte, (int)model.AreaType);
            _db.AddInParameter(dc, "ScenicId", DbType.String, model.ScenicId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : -1;
        }

        /// <summary>
        /// 删除同业资讯信息
        /// </summary>
        /// <param name="newId">同业资讯编号</param>
        /// <returns>返回1成功，其他失败</returns>
        public int DelPeerNews(params string[] newId)
        {
            if (newId == null || newId.Length < 1)
                return 0;

            var strSql = new StringBuilder();

            if (newId.Length == 1)
            {
                strSql.Append(" DELETE FROM [tbl_PeerNewsAttachInfo] WHERE [NewId] ");
                strSql.AppendFormat(" = '{0}' ; ", newId[0]);
                strSql.Append(" DELETE FROM [tbl_PeerNews] WHERE [NewId] ");
                strSql.AppendFormat(" = '{0}' ; ", newId[0]);
            }
            else
            {
                string strTmp = string.Empty;
                foreach (var s in newId)
                {
                    if (string.IsNullOrEmpty(s))
                        continue;

                    strTmp += ("'" + s + "'" + ",");
                }
                strTmp = strTmp.TrimEnd(',');

                strSql.Append(" DELETE FROM [tbl_PeerNewsAttachInfo] WHERE [NewId] ");
                strSql.AppendFormat(" in ({0}) ; ", strTmp);
                strSql.Append(" DELETE FROM [tbl_PeerNews] WHERE [NewId] ");
                strSql.AppendFormat(" in ({0}) ; ", strTmp);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : -1;
        }

        /// <summary>
        /// 获取同业资讯信息
        /// </summary>
        /// <param name="newId">资讯编号</param>
        /// <returns>返回同业资讯信息实体</returns>
        public MPeerNews GetPeerNews(string newId)
        {
            MPeerNews model = null;

            if (string.IsNullOrEmpty(newId))
                return model;

            var strSql = new StringBuilder();
            strSql.Append(SqlPeerNewsSelect);
            strSql.Append(" where [NewId] = @NewId order by LastUpdateTime desc ; ");
            strSql.Append(SqlPeerNewsAttachInfoSelect);
            strSql.Append(" where [NewId] = @NewId order by Type desc ; ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "NewId", DbType.AnsiStringFixedLength, newId);

            model = new MPeerNews();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("NewId")))
                        model.NewId = dr.GetString(dr.GetOrdinal("NewId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                        model.Title = dr.GetString(dr.GetOrdinal("Title"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeId")))
                        model.TypeId = (PeerNewType)dr.GetByte(dr.GetOrdinal("TypeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyName")))
                        model.CompanyName = dr.GetString(dr.GetOrdinal("CompanyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorName")))
                        model.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay = (NewsB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SortId")))
                        model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        model.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IP")))
                        model.Ip = dr.GetString(dr.GetOrdinal("IP"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")))
                        model.LastUpdateTime = dr.GetDateTime(dr.GetOrdinal("LastUpdateTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaId")))
                        model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaName")))
                        model.AreaName = dr.GetString(dr.GetOrdinal("AreaName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaType")))
                        model.AreaType = (Model.SystemStructure.AreaType)dr.GetByte(dr.GetOrdinal("AreaType"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ScenicId")))
                        model.ScenicId = dr.GetString(dr.GetOrdinal("ScenicId"));
                }

                dr.NextResult();

                model.AttachInfo = new List<MPeerNewsAttachInfo>();
                MPeerNewsAttachInfo modelInfo;
                while (dr.Read())
                {
                    modelInfo = new MPeerNewsAttachInfo();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        modelInfo.Id = dr.GetString(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Type")))
                        modelInfo.Type = (AttachInfoType)dr.GetByte(dr.GetOrdinal("Type"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Path")))
                        modelInfo.Path = dr.GetString(dr.GetOrdinal("Path"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FileName")))
                        modelInfo.FileName = dr.GetString(dr.GetOrdinal("FileName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Remark")))
                        modelInfo.Remark = dr.GetString(dr.GetOrdinal("Remark"));

                    model.AttachInfo.Add(modelInfo);
                }
            }

            return model;
        }

        /// <summary>
        /// 获取同业资讯列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回同业资讯信息集合</returns>
        public IList<MPeerNews> GetGetPeerNewsList(int pageSize, int pageIndex, ref int recordCount
            , MQueryPeerNews queryModel)
        {
            string fileds = @" [NewId]
      ,[Title],[TypeId],[Content],[CompanyId],[CompanyName],[OperatorId],[OperatorName],[B2BDisplay],[SortId]
      ,[ClickNum],[IP],[IssueTime],[LastUpdateTime],[MQ],[AreaId],[AreaName],[AreaType],[ScenicId] ";
            string tableName = "view_PeerNewsUserInfo";
            string orderby = " LastUpdateTime desc ";
            var strWhere = new StringBuilder(" 1 = 1 ");
            if (queryModel != null)
            {
                if (!string.IsNullOrEmpty(queryModel.CompanyId))
                    strWhere.AppendFormat(" and [CompanyId] = '{0}' ", queryModel.CompanyId);
                if (!string.IsNullOrEmpty(queryModel.Title))
                    strWhere.AppendFormat(" and [Title] like '%{0}%' ", queryModel.Title);
                if (!string.IsNullOrEmpty(queryModel.KeyWord))
                    strWhere.AppendFormat(" and isnull([Title],'') + isnull([CompanyName],'') like '%{0}%' ", queryModel.KeyWord);
                if (queryModel.TypeId.HasValue)
                    strWhere.AppendFormat(" and [TypeId] = {0} ", (int)queryModel.TypeId.Value);
                if (!queryModel.IsShowHideNew)
                    strWhere.AppendFormat(" and [B2BDisplay] <> {0} ", (int)NewsB2BDisplay.隐藏);

                switch (queryModel.OrderIndex)
                {
                    case 0:
                        orderby = " LastUpdateTime desc ";
                        break;
                    case 1:
                        orderby = " LastUpdateTime asc ";
                        break;
                    case 2:
                        orderby = " IssueTime desc ";
                        break;
                    case 3:
                        orderby = " IssueTime asc ";
                        break;
                    case 4:
                        orderby = " B2BDisplay desc,SortId asc,IssueTime desc ";
                        break;
                    case 5:
                        orderby = " B2BDisplay asc,SortId desc,IssueTime asc ";
                        break;
                    default:
                        orderby = " LastUpdateTime desc ";
                        break;
                }
            }

            IList<MPeerNews> list;
            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount
                , tableName, "NewId", fileds, strWhere.ToString(), orderby))
            {
                list = new List<MPeerNews>();
                while (dr.Read())
                {
                    var model = new MPeerNews();
                    if (!dr.IsDBNull(dr.GetOrdinal("NewId")))
                        model.NewId = dr.GetString(dr.GetOrdinal("NewId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                        model.Title = dr.GetString(dr.GetOrdinal("Title"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeId")))
                        model.TypeId = (PeerNewType)dr.GetByte(dr.GetOrdinal("TypeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyName")))
                        model.CompanyName = dr.GetString(dr.GetOrdinal("CompanyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorName")))
                        model.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay = (NewsB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SortId")))
                        model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        model.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IP")))
                        model.Ip = dr.GetString(dr.GetOrdinal("IP"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")))
                        model.LastUpdateTime = dr.GetDateTime(dr.GetOrdinal("LastUpdateTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MQ")))
                        model.OperatorMQ = dr.GetString(dr.GetOrdinal("MQ"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaId")))
                        model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaName")))
                        model.AreaName = dr.GetString(dr.GetOrdinal("AreaName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaType")))
                        model.AreaType = (Model.SystemStructure.AreaType)dr.GetByte(dr.GetOrdinal("AreaType"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ScenicId")))
                        model.ScenicId = dr.GetString(dr.GetOrdinal("ScenicId"));

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取同业资讯列表
        /// </summary>
        /// <param name="topNum">top数量</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回同业资讯信息集合</returns>
        public IList<MPeerNews> GetGetPeerNewsList(int topNum, MQueryPeerNews queryModel)
        {
            var strSql = new StringBuilder();
            strSql.Append(" select ");
            if (topNum > 0)
                strSql.AppendFormat(" top {0} ", topNum);
            strSql.Append(
                @" [NewId],[Title],[TypeId],[Content],[CompanyId],[CompanyName],[OperatorId],[OperatorName]
                            ,[B2BDisplay],[SortId],[ClickNum],[IP],[IssueTime],[LastUpdateTime],[MQ],[AreaId],[AreaName],[AreaType],[ScenicId] ");
            strSql.Append(" from view_PeerNewsUserInfo ");
            strSql.Append(" where 1 = 1 ");
            if (queryModel != null)
            {
                if (!string.IsNullOrEmpty(queryModel.CompanyId))
                    strSql.AppendFormat(" and [CompanyId] = '{0}' ", queryModel.CompanyId);
                if (!string.IsNullOrEmpty(queryModel.Title))
                    strSql.AppendFormat(" and [Title] like '%{0}%' ", queryModel.Title);
                if (!string.IsNullOrEmpty(queryModel.KeyWord))
                    strSql.AppendFormat(" and isnull([Title],'') + isnull([CompanyName],'') like '%{0}%' ", queryModel.KeyWord);
                if (queryModel.TypeId.HasValue)
                    strSql.AppendFormat(" and [TypeId] = {0} ", (int)queryModel.TypeId.Value);
                if (!queryModel.IsShowHideNew)
                    strSql.AppendFormat(" and [B2BDisplay] <> {0} ", (int)NewsB2BDisplay.隐藏);

                strSql.Append(" order by ");
                switch (queryModel.OrderIndex)
                {
                    case 0:
                        strSql.Append(" LastUpdateTime desc ");
                        break;
                    case 1:
                        strSql.Append(" LastUpdateTime asc ");
                        break;
                    case 2:
                        strSql.Append(" IssueTime desc ");
                        break;
                    case 3:
                        strSql.Append(" IssueTime asc ");
                        break;
                    case 4:
                        strSql.Append(" B2BDisplay desc,SortId asc,IssueTime desc ");
                        break;
                    case 5:
                        strSql.Append(" B2BDisplay asc,SortId desc,IssueTime asc ");
                        break;
                    default:
                        strSql.Append(" LastUpdateTime desc ");
                        break;
                }
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            IList<MPeerNews> list;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<MPeerNews>();
                while (dr.Read())
                {
                    var model = new MPeerNews();
                    if (!dr.IsDBNull(dr.GetOrdinal("NewId")))
                        model.NewId = dr.GetString(dr.GetOrdinal("NewId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Title")))
                        model.Title = dr.GetString(dr.GetOrdinal("Title"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeId")))
                        model.TypeId = (PeerNewType)dr.GetByte(dr.GetOrdinal("TypeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyName")))
                        model.CompanyName = dr.GetString(dr.GetOrdinal("CompanyName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorName")))
                        model.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("B2BDisplay")))
                        model.B2BDisplay = (NewsB2BDisplay)dr.GetByte(dr.GetOrdinal("B2BDisplay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SortId")))
                        model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClickNum")))
                        model.ClickNum = dr.GetInt32(dr.GetOrdinal("ClickNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IP")))
                        model.Ip = dr.GetString(dr.GetOrdinal("IP"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")))
                        model.LastUpdateTime = dr.GetDateTime(dr.GetOrdinal("LastUpdateTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MQ")))
                        model.OperatorMQ = dr.GetString(dr.GetOrdinal("MQ"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaId")))
                        model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaName")))
                        model.AreaName = dr.GetString(dr.GetOrdinal("AreaName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaType")))
                        model.AreaType = (Model.SystemStructure.AreaType)dr.GetByte(dr.GetOrdinal("AreaType"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ScenicId")))
                        model.ScenicId = dr.GetString(dr.GetOrdinal("ScenicId"));

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 更新同业资讯点击量
        /// </summary>
        /// <param name="clickNum">点击量</param>
        /// <param name="newIds">同业资讯编号集合</param>
        /// <returns>返回是否成功</returns>
        public bool UpdateClickNum(int clickNum, params string[] newIds)
        {
            if (newIds == null || newIds.Length <= 0)
                return false;

            var strSql = new StringBuilder();
            strSql.Append(" update [tbl_PeerNews] set [ClickNum] = isnull(ClickNum,0) + @ClickNum where ");
            if (newIds.Length == 1)
            {
                strSql.AppendFormat(" [NewId] = '{0}' ", newIds[0]);
            }
            else
            {
                string strTmp = string.Empty;
                foreach (var t in newIds)
                {
                    if (string.IsNullOrEmpty(t))
                        continue;

                    strTmp += "'" + t + "'" + ",";
                }
                strTmp = strTmp.TrimEnd(',');
                strSql.AppendFormat(" [NewId] in ({0}) ", strTmp);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "ClickNum", DbType.Int32, clickNum);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? true : false;
        }
    }
}
