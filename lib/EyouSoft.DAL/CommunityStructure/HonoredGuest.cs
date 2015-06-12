using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CommunityStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-14
    /// 描述：嘉宾访谈数据层
    /// </summary>
    public class HonoredGuest : DALBase, IDAL.CommunityStructure.IHonoredGuest
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database=null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public HonoredGuest() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        private const string SQL_CommunityHonoredGuest_ADD = "INSERT INTO [tbl_CommunityHonoredGuest]([ID],[Title],[ImgPath] ,[ImgThumb],[Content],[Opinion1],[Opinion2],[Opinion3],[Summary],[IssueTime],[OperatorId]) VALUES(@ID,@Title,@ImgPath,@ImgThumb,@Content ,@Opinion1,@Opinion2 ,@Opinion3,@Summary,@IssueTime,@OperatorId)";
        private const string SQL_CommunityHonoredGuest_UPDATE = "UPDATE [tbl_CommunityHonoredGuest]  SET [Title] = @Title, [ImgPath] = @ImgPath,[ImgThumb] = @ImgThumb,[Content] = @Content,[Opinion1] = @Opinion1,[Opinion2] = @Opinion2,[Opinion3] = @Opinion3,[Summary] = @Summary,[OperatorId] = @OperatorId WHERE  ID=@ID";
        private const string SQL_CommunityHonoredGuest_DELETE = "delete [tbl_CommunityHonoredGuest] where ID in({0})";
        private const string SQL_CommunityHonoredGuest_GetNewInfo = "SELECT Top 1 [ID],[Title],[ImgPath],[ImgThumb],[Content],[Opinion1],[Opinion2],[Opinion3],[Summary],[IssueTime],[OperatorId] FROM [tbl_CommunityHonoredGuest] order by IssueTime desc ";
        private const string SQL_CommunityHonoredGuest_GetTopNumList = "Select {0} ID,Title,ImgThumb,ImgPath,[Content] from tbl_CommunityHonoredGuest order by IssueTime desc ";
        private const string SQL_CommunityHonoredGuest_GetModel = "Select [ID],[Title],[ImgPath],[ImgThumb],[Content],[Opinion1],[Opinion2],[Opinion3],[Summary],[IssueTime],[OperatorId] from tbl_CommunityHonoredGuest where ID=@ID";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_CommunityHonoredGuest] WHERE [ID]=@ID AND [ImgPath]=@ImgPath)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_CommunityHonoredGuest] WHERE [ID]=@ID AND ImgPath<>'';IF (SELECT COUNT(*) FROM [tbl_CommunityHonoredGuest] WHERE [ID]=@ID AND [ImgThumb]=@ImgThumb)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgThumb] FROM [tbl_CommunityHonoredGuest] WHERE [ID]=@ID AND ImgThumb<>'';";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_CommunityHonoredGuest] WHERE ID in({0}) AND ImgPath<>'';INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgThumb] FROM [tbl_CommunityHonoredGuest] WHERE ID in({0}) AND ImgThumb<>'';";
        
        #endregion

        #region ICommunityHonoreGuest成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">嘉宾访谈实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.CommunityStructure.HonoredGuest model)
        {
            model.ID = Guid.NewGuid().ToString();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommunityHonoredGuest_ADD);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            this._database.AddInParameter(dc, "ImgThumb", DbType.String, model.ImgThumb);
            this._database.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._database.AddInParameter(dc, "Opinion1", DbType.String, model.Opinion1);
            this._database.AddInParameter(dc, "Opinion2", DbType.String, model.Opinion2);
            this._database.AddInParameter(dc, "Opinion3", DbType.String, model.Opinion3);
            this._database.AddInParameter(dc, "Summary", DbType.String, model.Summary);
            this._database.AddInParameter(dc, "IssueTime", DbType.String, DateTime.Now);
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">嘉宾访谈实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.CommunityStructure.HonoredGuest model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE + SQL_CommunityHonoredGuest_UPDATE);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            this._database.AddInParameter(dc, "ImgThumb", DbType.String, model.ImgThumb);
            this._database.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._database.AddInParameter(dc, "Opinion1", DbType.String, model.Opinion1);
            this._database.AddInParameter(dc, "Opinion2", DbType.String, model.Opinion2);
            this._database.AddInParameter(dc, "Opinion3", DbType.String, model.Opinion3);
            this._database.AddInParameter(dc, "Summary", DbType.String, model.Summary);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">主键编号字符串（，分割）</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(params string[] Ids)
        {
            if (Ids == null)
                return false;

            StringBuilder StrParam = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                StrParam.AppendFormat("{0}{1}{2}", i > 0 ? "," : "", "@Param", i);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_DELETEDFILE_DELETEMOVE, StrParam.ToString());
            strSql.AppendFormat(SQL_CommunityHonoredGuest_DELETE, StrParam.ToString());
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            for (int i = 0; i < Ids.Length; i++)
            {
                this._database.AddInParameter(dc, "Param" + i.ToString(), DbType.AnsiStringFixedLength, Ids[i]);
            }
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取最新一期的嘉宾访谈
        /// </summary>
        /// <returns>嘉宾访谈实体</returns>
        public  virtual EyouSoft.Model.CommunityStructure.HonoredGuest GetNewInfo()
        {
            EyouSoft.Model.CommunityStructure.HonoredGuest model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommunityHonoredGuest_GetNewInfo);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.HonoredGuest();
                    model.ID = dr.GetString(0);
                    model.Title=dr.IsDBNull(1)?string.Empty:dr.GetString(1);
                    model.ImgPath=dr.IsDBNull(2)?string.Empty:dr.GetString(2);
                    model.ImgThumb=dr.IsDBNull(3)?string.Empty:dr.GetString(3);
                    model.Content=dr.IsDBNull(4)?string.Empty:dr.GetString(4);
                    model.Opinion1=dr.IsDBNull(5)?string.Empty:dr.GetString(5);
                    model.Opinion2=dr.IsDBNull(6)?string.Empty:dr.GetString(6);
                    model.Opinion3=dr.IsDBNull(7)?string.Empty:dr.GetString(7);
                    model.Summary=dr.IsDBNull(8)?string.Empty:dr.GetString(8);
                    model.IssueTime=dr.IsDBNull(9)?DateTime.Now:dr.GetDateTime(9);
                    model.OperatorId=dr.IsDBNull(10)?0:dr.GetInt32(10);
                }
            }
            return model;
        }
        /// <summary>
        /// 获取嘉宾访谈实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CommunityStructure.HonoredGuest GetModel(string Id)
        {
            EyouSoft.Model.CommunityStructure.HonoredGuest model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommunityHonoredGuest_GetModel);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.HonoredGuest();
                    model.ID = dr.GetString(0);
                    model.Title = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ImgPath = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.ImgThumb = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.Content = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.Opinion1 = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.Opinion2 = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.Opinion3 = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.Summary = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.IssueTime = dr.IsDBNull(9) ? DateTime.Now : dr.GetDateTime(9);
                    model.OperatorId = dr.IsDBNull(10) ? 0 : dr.GetInt32(10);
                }
            }
            return model;
        }
        /// <summary>
        /// 获取指定条数的嘉宾访谈列表集合
        /// </summary>
        /// <param name="topNumber">需要返回的记录数 =0返回全部</param>
        /// <returns>嘉宾访谈列表集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GetTopNumList(int topNumber)
        {
            string strSql = string.Format(SQL_CommunityHonoredGuest_GetTopNumList, topNumber > 0 ? string.Format(" top {0} ", topNumber) : string.Empty);
            IList<EyouSoft.Model.CommunityStructure.HonoredGuest> list = new List<EyouSoft.Model.CommunityStructure.HonoredGuest>();
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.HonoredGuest model = new EyouSoft.Model.CommunityStructure.HonoredGuest();
                    model.ID = dr.GetString(0);
                    model.Title = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ImgThumb = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.ImgPath = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.Content = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 分页获取嘉宾访谈列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>嘉宾访谈列表集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GetPageList(int pageSize, int pageIndex, ref int recordCount,string KeyWord)
        {
            IList<EyouSoft.Model.CommunityStructure.HonoredGuest> list = new List<EyouSoft.Model.CommunityStructure.HonoredGuest>();
            string tableName = "tbl_CommunityHonoredGuest";
            string fields = "ID,Title,Content,IssueTime,Summary";
            string primaryKey = "ID";
            string orderByString = "IssueTime desc";
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere = string.Format(" Title like'%{0}%' ", KeyWord);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere, orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.HonoredGuest model = new EyouSoft.Model.CommunityStructure.HonoredGuest();
                    model.ID = dr.GetString(0);
                    model.Title = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.Content = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.IssueTime = dr.IsDBNull(3) ? DateTime.Now : dr.GetDateTime(3);
                    model.Summary = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
