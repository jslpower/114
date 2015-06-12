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
    /// 创建人：鲁功源  2010-07-14
    /// 描述：行业资讯文章数据层
    /// </summary>
    public class InfoArticle : DALBase, IDAL.CommunityStructure.IInfoArticle
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public InfoArticle()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        private const string SQL_InfoArticle_ADD = "INSERT INTO [tbl_CommunityInfoArticle]([ID],[ArticleTitle],[ImgPath],[ImgThumb],[ArticleText],[ArticleTag],[Editor],[Source],[TopicClassId],[AreaId],[IsImage],[IsTop],[IsFrontPage],[IssueTime],[OperatorId],[Click],[TitleColor]) VALUES(@ID,@ArticleTitle,@ImgPath,@ImgThumb,@ArticleText,@ArticleTag,@Editor,@Source,@TopicClassId,@AreaId,@IsImage,@IsTop,@IsFrontPage,@IssueTime,@OperatorId,@Click,@TitleColor);";
        private const string SQL_InfoArticle_UPDATE = "UPDATE [tbl_CommunityInfoArticle] SET [ArticleTitle] = @ArticleTitle,[ImgPath] = @ImgPath,[ImgThumb] = @ImgThumb,[ArticleText] = @ArticleText,[ArticleTag] = @ArticleTag,[Editor] = @Editor,[Source] = @Source,[TopicClassId] = @TopicClassId,[AreaId] = @AreaId,[IsImage] = @IsImage,[IsTop] = @IsTop,[IsFrontPage] = @IsFrontPage, [OperatorId] = @OperatorId,[TitleColor]=@TitleColor WHERE [ID] = @ID;";
        private const string SQL_InfoArticle_DELETE = "delete tbl_CommunityInfoArticle where id in({0}) ;";
        private const string SQL_InfoArticle_GETMODEL = "declare @IssueTime datetime set @IssueTime=getdate() select @IssueTime=[IssueTime] from [tbl_CommunityInfoArticle] where ID=@ID SELECT [ID],[ArticleTitle],[ImgPath],[ImgThumb],[ArticleText],[ArticleTag],[Editor],[Source],[TopicClassId],[AreaId],[IssueTime],[IsImage],[IsTop],[IsFrontPage],[OperatorId],[Click],[TitleColor] FROM [tbl_CommunityInfoArticle] where ID=@ID; select id,ArticleTitle,TopicClassId,AreaId from [tbl_CommunityInfoArticle] where issueTime=(select max(IssueTime) from [tbl_CommunityInfoArticle] where IssueTime<@IssueTime); select id,ArticleTitle,TopicClassId,AreaId from [tbl_CommunityInfoArticle] where issueTime=(select min(IssueTime) from [tbl_CommunityInfoArticle] where IssueTime>@IssueTime)";
        private const string SQL_InfoArticle_SetClicks = "Update tbl_CommunityInfoArticle set click=click+1 where ID=@ID";
        private const string SQL_InfoArticle_ADDTAGS = "INSERT INTO tbl_CommunityArticleTag(ArticleId,AreaId,ArticleTag) VALUES('{0}',{1},'{2}');";
        private const string SQL_InfoArticle_DELTAGS = "DELETE tbl_CommunityArticleTag where ArticleId=@ID;";
        private const string SQL_InfoArticle_SETTOP = "UPDATE tbl_CommunityInfoArticle set IsTop=@IsTop where ID=@ID";
        private const string SQL_InfoArticle_GetHeadInfo = "select top 1 id,ArticleTitle,ArticleText,TitleColor from tbl_CommunityInfoArticle where IsTop='1' ";
        private const string SQL_InfoArticle_GetTopListByReadCount = "select {0} id,ArticleTitle,TopicClassId,AreaId from tbl_CommunityInfoArticle";
        private const string SQL_InfoArticle_SetFrontPage = "Update tbl_CommunityInfoArticle set IsFrontPage=@IsFrontPage where ID=@ID";
        private const string SQL_InfoArticle_GetTopNumListByTopicList = "SELECT {0} ID,ArticleTitle,TopicClassId,AreaId,ImgThumb,ImgPath from tbl_CommunityInfoArticle ";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_CommunityInfoArticle] WHERE [ID]=@ID AND [ImgPath]=@ImgPath)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_CommunityInfoArticle] WHERE [ID]=@ID AND ImgPath<>'';IF (SELECT COUNT(*) FROM [tbl_CommunityInfoArticle] WHERE [ID]=@ID AND [ImgThumb]=@ImgThumb)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgThumb] FROM [tbl_CommunityInfoArticle] WHERE [ID]=@ID AND ImgThumb<>'';";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_CommunityInfoArticle] WHERE [ID] in({0}) AND ImgPath<>'';INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgThumb] FROM [tbl_CommunityInfoArticle] WHERE [ID] in({0}) AND ImgThumb<>'';";
        #endregion

        #region IInfoArticle成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">行业资讯文章实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.CommunityStructure.InfoArticle model)
        {
            StringBuilder strSql = new StringBuilder(SQL_InfoArticle_ADD);
            string ArticleID = Guid.NewGuid().ToString();
            model.ID = ArticleID;
            if (!string.IsNullOrEmpty(model.ArticleTag))
            {
                string[] Tags = model.ArticleTag.Split(',');
                for (int i = 0; i < Tags.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Tags[i]))
                    {
                        strSql.AppendFormat(SQL_InfoArticle_ADDTAGS, ArticleID, (int)model.AreaId, Tags[i].ToString());
                    }
                }
            }
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ArticleID);
            this._database.AddInParameter(dc, "ArticleTitle", DbType.String, model.ArticleTitle);
            this._database.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            this._database.AddInParameter(dc, "ImgThumb", DbType.String, model.ImgThumb);
            this._database.AddInParameter(dc, "ArticleText", DbType.String, model.ArticleText);
            this._database.AddInParameter(dc, "ArticleTag", DbType.String, model.ArticleTag);
            this._database.AddInParameter(dc, "Editor", DbType.String, model.Editor);
            this._database.AddInParameter(dc, "Source", DbType.String, model.Source);
            this._database.AddInParameter(dc, "TopicClassId", DbType.Byte, (int)model.TopicClassId);
            this._database.AddInParameter(dc, "AreaId", DbType.Byte, (int)model.AreaId);
            this._database.AddInParameter(dc, "IsImage", DbType.AnsiStringFixedLength, model.IsImage ? "1" : "0");
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, model.IsTop ? "1" : "0");
            this._database.AddInParameter(dc, "IsFrontPage", DbType.AnsiStringFixedLength, model.IsFrontPage ? "1" : "0");
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            this._database.AddInParameter(dc, "TitleColor", DbType.String, model.TitleColor);
            this._database.AddInParameter(dc, "Click", DbType.String, model.Click);
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">行业资讯文章实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.CommunityStructure.InfoArticle model)
        {
            StringBuilder strSql = new StringBuilder(SQL_InfoArticle_DELTAGS);
            if (!string.IsNullOrEmpty(model.ArticleTag))
            {
                string[] Tags = model.ArticleTag.Split(',');
                for (int i = 0; i < Tags.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Tags[i]))
                    {
                        strSql.AppendFormat(SQL_InfoArticle_ADDTAGS, model.ID, (int)model.AreaId, Tags[i].ToString());
                    }
                }
            }
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE + SQL_InfoArticle_UPDATE + strSql.ToString());
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "ArticleTitle", DbType.String, model.ArticleTitle);
            this._database.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            this._database.AddInParameter(dc, "ImgThumb", DbType.String, model.ImgThumb);
            this._database.AddInParameter(dc, "ArticleText", DbType.String, model.ArticleText);
            this._database.AddInParameter(dc, "ArticleTag", DbType.String, model.ArticleTag);
            this._database.AddInParameter(dc, "Editor", DbType.String, model.Editor);
            this._database.AddInParameter(dc, "Source", DbType.String, model.Source);
            this._database.AddInParameter(dc, "TopicClassId", DbType.Byte, (int)model.TopicClassId);
            this._database.AddInParameter(dc, "AreaId", DbType.Byte, (int)model.AreaId);
            this._database.AddInParameter(dc, "IsImage", DbType.AnsiStringFixedLength, model.IsImage ? "1" : "0");
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, model.IsTop?"1":"0");
            this._database.AddInParameter(dc, "IsFrontPage", DbType.AnsiStringFixedLength, model.IsFrontPage ? "1" : "0");
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            this._database.AddInParameter(dc, "TitleColor", DbType.String, model.TitleColor);
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="IDs">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(params string[] IDs)
        {
            StringBuilder StrParam = new StringBuilder();
            for (int i = 0; i < IDs.Length; i++)
            {
                StrParam.AppendFormat("{0}{1}{2}", i > 0 ? "," : "", "@Param", i);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_DELETEDFILE_DELETEMOVE, StrParam.ToString());
            strSql.AppendFormat(SQL_InfoArticle_DELETE, StrParam.ToString());
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            for (int i = 0; i < IDs.Length; i++)
            {
                this._database.AddInParameter(dc, "Param" + i.ToString(), DbType.AnsiStringFixedLength, IDs[i]);
            }
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">置顶状态</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetTop(string ID, bool IsTop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_InfoArticle_SETTOP);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置首页显示
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsFrontPage">是否首页显示</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetFrontPage(string ID, bool IsFrontPage)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_InfoArticle_SetFrontPage);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsFrontPage", DbType.AnsiStringFixedLength, IsFrontPage ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取头条行业资讯
        /// </summary>
        /// <param name="TopicClass">资讯大类别 =null返回全部</param>
        /// <param name="TopicArea">资讯小类别 =null返回全部</param>
        /// <returns>行业资讯实体(ID,标题,内容)</returns>
        public virtual EyouSoft.Model.CommunityStructure.InfoArticle GetHeadInfo(EyouSoft.Model.CommunityStructure.TopicClass? TopicClass, EyouSoft.Model.CommunityStructure.TopicAreas? TopicArea)
        {
            StringBuilder strSql = new StringBuilder(SQL_InfoArticle_GetHeadInfo);
            if (TopicClass.HasValue)
            {
                strSql.AppendFormat(" AND TopicClassId={0} ", (int)TopicClass.Value);
            }
            if (TopicArea.HasValue)
            {
                strSql.AppendFormat(" AND AreaId={0} ", (int)TopicArea.Value);
            }
            strSql.Append("order by IssueTime desc");
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            EyouSoft.Model.CommunityStructure.InfoArticle model = null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr.GetString(0);
                    model.ArticleTitle = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ArticleText = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.TitleColor = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                }
            }
            return model;
        }
        /// <summary>
        /// 获取行业资讯实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>行业资讯实体</returns>
        public virtual EyouSoft.Model.CommunityStructure.InfoArticle GetModel(string ID)
        {
            EyouSoft.Model.CommunityStructure.InfoArticle model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_InfoArticle_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr.GetString(0);
                    model.ArticleTitle = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ImgPath = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.ImgThumb = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.ArticleText = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.ArticleTag = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.Editor = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.Source = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.TopicClassId = (EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr.GetByte(8).ToString());
                    model.AreaId = (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(dr.GetByte(9).ToString());
                    model.IssueTime = dr.GetDateTime(10);
                    model.IsImage = dr.IsDBNull(11) ? false : (dr.GetString(11) == "1" ? true : false);
                    model.IsTop = dr.IsDBNull(12) ? false : (dr.GetString(12) == "1" ? true : false);
                    model.IsFrontPage = dr.IsDBNull(13) ? false : (dr.GetString(13) == "1" ? true : false);
                    model.OperatorId = dr.GetInt32(14);
                    model.Click = dr.GetInt32(15);
                    model.TitleColor = dr.IsDBNull(16) ? string.Empty : dr.GetString(16);
                }
                if (dr.NextResult() && model!=null)
                {
                    if (dr.Read())
                    {
                        model.PrevInfo = new EyouSoft.Model.CommunityStructure.InfoArticleBase(dr.GetString(0), dr.GetString(1),(EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr.GetByte(2).ToString()));
                    }
                }
                if (dr.NextResult() && model != null)
                {
                    if (dr.Read())
                    {
                        model.NextInfo = new EyouSoft.Model.CommunityStructure.InfoArticleBase(dr.GetString(0), dr.GetString(1), (EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr.GetByte(2).ToString()));
                    }
                }
            }
            return model;
        }
        /// <summary>
        ///获取指定条数的行业资讯列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="IsCurrWeekNew">是否本周最新资讯</param>
        /// <param name="IsFrontPage">是否首页显示 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="CurrInfoID">是否当前资讯的相关文章 =""返回全部</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param> 
        /// <returns>行业资讯列表</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew,
            bool IsCurrWeekNew, bool? IsFrontPage, string KeyWord, string CurrInfoID, bool? IsFocusPic, bool? IsTopPic)
        {
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
            DbCommand dc = this._database.GetStoredProcCommand("proc_InfoArticle_GetTopNumList");
            this._database.AddInParameter(dc, "topNumber", DbType.Int32, topNumber);
            this._database.AddInParameter(dc, "topClass", DbType.Int32, topClass.HasValue ? (int)topClass.Value : -1);
            this._database.AddInParameter(dc, "areaId", DbType.Int32, areaId.HasValue ? (int)areaId.Value : -1);
            this._database.AddInParameter(dc, "IsPicNew", DbType.AnsiStringFixedLength, IsPicNew.HasValue ? (IsPicNew.Value ? 1 : 0) : -1);
            this._database.AddInParameter(dc, "IsFrontPage", DbType.AnsiStringFixedLength, IsFrontPage.HasValue ? (IsFrontPage.Value ? 1 : 0) : -1);
            this._database.AddInParameter(dc, "KeyWord", DbType.String, KeyWord);
            this._database.AddInParameter(dc, "IsCurrWeekNew", DbType.String, IsCurrWeekNew ? "1" : "0");
            this._database.AddInParameter(dc, "CurrInfoID", DbType.String, CurrInfoID);
            this._database.AddInParameter(dc, "IsFocusPic", DbType.Int32, IsFocusPic.HasValue ? (IsFocusPic.Value ? 1 : 0) : -1);
            this._database.AddInParameter(dc, "IsTopPic", DbType.Int32, IsTopPic.HasValue ? (IsTopPic.Value ? 1 : 0) : -1);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr.IsDBNull(0) ? string.Empty : dr.GetString(0);
                    model.ArticleTitle = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ImgThumb = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.ArticleText = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.TitleColor = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.IssueTime = dr.IsDBNull(5) ? DateTime.MinValue : dr.GetDateTime(5);
                    model.ImgPath = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.TopicClassId=(EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr.GetByte(7).ToString());
                    model.AreaId = (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(dr.GetByte(8).ToString());
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 根据大类别集合获取指定条数的行业资讯列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数</param>
        /// <param name="TopicList">大类别集合 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param> 
        /// <returns>行业资讯列表</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumListByTopicList(int topNumber, List<EyouSoft.Model.CommunityStructure.TopicClass> TopicList,
            bool? IsPicNew, bool? IsFocusPic, bool? IsTopPic)
        {
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_InfoArticle_GetTopNumListByTopicList, topNumber > 0 ? string.Format(" top {0} ", topNumber) : "");
            #region 生成查询条件
            strSql.Append(" where 1=1 ");
            if (TopicList != null && TopicList.Count > 0)
            {
                strSql.Append(" and TopicClassId in(");
                int index = 0;
                foreach (EyouSoft.Model.CommunityStructure.TopicClass type in TopicList)
                {
                    strSql.AppendFormat("{0}{1}", index > 0 ? "," : "", (int)type);
                    index++;
                }
                strSql.Append(")");
            }
            if (IsPicNew.HasValue)
            {
                strSql.AppendFormat(" and IsImage='{0}' ", IsPicNew.Value ? "1" : "0");
            }
            if (IsFocusPic.HasValue)
            {
                strSql.Append(IsFocusPic.Value ? " AND len(ImgThumb)>0 " : " AND len(ImgThumb)=0 ");
            }
            if (IsTopPic.HasValue)
            {
                strSql.Append(IsTopPic.Value ? " AND len(ImgPath)>0 " : " AND len(ImgPath)=0 ");
            }
            strSql.Append(" order by IsTop desc,IssueTime desc");
            #endregion
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr.GetString(0);
                    model.ArticleTitle = dr.GetString(1);
                    model.TopicClassId = (EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr.GetByte(2).ToString());
                    model.AreaId = (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(dr.GetByte(3).ToString());
                    model.ImgThumb = dr.GetString(4);
                    model.ImgPath = dr.GetString(5);
                    list.Add(model);
                    model = null;

                }
            }
            return list;
        }
        /// <summary>
        /// 分业获取行业资讯列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="InfoTag">需要匹配的标签</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param> 
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetPageList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord, string InfoTag
            ,bool? IsFocusPic,bool? IsTopPic,params EyouSoft.Model.CommunityStructure.TopicClass?[] topClass)
        {
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
            string tableName = "tbl_CommunityInfoArticle";
            string fields = "ID,ArticleTitle,ImgThumb,ArticleText,ArticleTag,Editor,Source,IssueTime,Click,TitleColor,IsTop,TopicClassId,AreaId,IsFrontPage,ImgPath";
            string primaryKey = "ID";
            string orderByString = "IssueTime desc";
            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (topClass != null)
            {
                string strIds = string.Empty;
                foreach (EyouSoft.Model.CommunityStructure.TopicClass? Item in topClass)
                {
                    if (Item != null)
                        strIds += ((int)Item).ToString() + ",";
                }
                strIds = strIds.TrimEnd(',');
                strWhere.AppendFormat(" and TopicClassId in ({0}) ", strIds);
            }
            if (areaId.HasValue)
            {
                    strWhere.AppendFormat(" AND AreaId={0} ", (int)areaId.Value);
            }
            if (IsPicNew.HasValue)
            {
                    strWhere.AppendFormat(" AND IsImage='{0}' ", IsPicNew.Value ? "1" : "0");
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                    strWhere.AppendFormat(" AND ArticleTitle like '%{0}%' ", KeyWord);
            }
            if (!string.IsNullOrEmpty(InfoTag))
            {
                    strWhere.AppendFormat(" AND charindex('{0}',ArticleTag,0)>0 ", InfoTag);
            }
            if (IsFocusPic.HasValue)
            {
                strWhere.Append(IsFocusPic.Value ? " AND len(ImgThumb)>0 " : " AND len(ImgThumb)=0 ");
            }
            if (IsTopPic.HasValue)
            {
                strWhere.Append(IsTopPic.Value ? " AND len(ImgPath)>0 " : " AND len(ImgPath)=0 ");
            }
            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr.GetString(0);
                    model.ArticleTitle = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ImgThumb = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.ArticleText = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.ArticleTag = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.Editor = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.Source = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.IssueTime = dr.IsDBNull(7) ? DateTime.Now : dr.GetDateTime(7);
                    model.Click = dr.IsDBNull(8) ? 0 : dr.GetInt32(8);
                    model.TitleColor = dr.IsDBNull(9) ? string.Empty : dr.GetString(9);
                    model.IsTop = dr.IsDBNull(10) ? false : (dr.GetString(10) == "1" ? true : false);
                    if (!dr.IsDBNull(dr.GetOrdinal("TopicClassId")))
                        model.TopicClassId = (EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr["TopicClassId"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaId")))
                        model.AreaId = (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(dr["AreaId"].ToString());
                    model.IsFrontPage = dr.IsDBNull(13) ? false : (dr.GetString(13) == "1" ? true : false);
                    model.ImgPath = dr.IsDBNull(14) ? string.Empty : dr.GetString(14);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }


        /// <summary>
        /// 分业获取行业资讯列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="InfoTag">需要匹配的标签</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param> 
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetPageList(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord, string InfoTag
             , bool? IsFocusPic, bool? IsTopPic)
        {
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
            string tableName = "tbl_CommunityInfoArticle";
            string fields = "ID,ArticleTitle,ImgThumb,ArticleText,ArticleTag,Editor,Source,IssueTime,Click,TitleColor,IsTop,TopicClassId,AreaId,IsFrontPage,ImgPath";
            string primaryKey = "ID";
            string orderByString = "IssueTime desc";
            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (topClass.HasValue)
            {
                strWhere.AppendFormat(" AND TopicClassId={0} ", (int)topClass.Value);
            }
            if (areaId.HasValue)
            {
                    strWhere.AppendFormat(" AND AreaId={0} ", (int)areaId.Value);
            }
            if (IsPicNew.HasValue)
            {
                    strWhere.AppendFormat(" AND IsImage='{0}' ", IsPicNew.Value ? "1" : "0");
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                    strWhere.AppendFormat(" AND ArticleTitle like '%{0}%' ", KeyWord);
            }
            if (!string.IsNullOrEmpty(InfoTag))
            {
                    strWhere.AppendFormat(" AND charindex('{0}',ArticleTag,0)>0 ", InfoTag);
            }
            if (IsFocusPic.HasValue)
            {
                strWhere.Append(IsFocusPic.Value ? " AND len(ImgThumb)>0 " : " AND len(ImgThumb)=0 ");
            }
            if (IsTopPic.HasValue)
            {
                strWhere.Append(IsTopPic.Value ? " AND len(ImgPath)>0 " : " AND len(ImgPath)=0 ");
            }
            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr.GetString(0);
                    model.ArticleTitle = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ImgThumb = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    model.ArticleText = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.ArticleTag = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.Editor = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.Source = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.IssueTime = dr.IsDBNull(7) ? DateTime.Now : dr.GetDateTime(7);
                    model.Click = dr.IsDBNull(8) ? 0 : dr.GetInt32(8);
                    model.TitleColor = dr.IsDBNull(9) ? string.Empty : dr.GetString(9);
                    model.IsTop = dr.IsDBNull(10) ? false : (dr.GetString(10) == "1" ? true : false);
                    if (!dr.IsDBNull(dr.GetOrdinal("TopicClassId")))
                        model.TopicClassId = (EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr["TopicClassId"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaId")))
                        model.AreaId = (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(dr["AreaId"].ToString());
                    model.IsFrontPage = dr.IsDBNull(13) ? false : (dr.GetString(13) == "1" ? true : false);
                    model.ImgPath = dr.IsDBNull(14) ? string.Empty : dr.GetString(14);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetClicks(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_InfoArticle_SetClicks);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取指定类型的资讯列表（以浏览次数倒序排列）
        /// </summary>
        /// <param name="topNumber">要获取的记录行数</param>
        /// <param name="TypeId">资讯大类别 =null返回全部</param>
        /// <param name="areaId">资讯小类别 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopListByReadCount(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? TypeId, EyouSoft.Model.CommunityStructure.TopicAreas? areaId)
        {
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_InfoArticle_GetTopListByReadCount, topNumber > 0 ? string.Format(" top {0} ", topNumber) : string.Empty);
            strSql.Append(" where 1=1 ");
            if (TypeId.HasValue)
            {
                strSql.AppendFormat(" and TopicClassId={0} ", (int)TypeId.Value);
            }
            if (areaId.HasValue)
            {
                strSql.AppendFormat(" and AreaId={0} ", (int)areaId.Value);
            }
            else
            {
                strSql.Append(" and AreaId<>9 "); //不取标签为“同业之星”的数据
            }
            strSql.Append(" order by Click desc ");
            DbCommand dc=this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                    model.ID = dr[0].ToString();
                    model.ArticleTitle = dr.IsDBNull(1) ? string.Empty : dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.TopicClassId=(EyouSoft.Model.CommunityStructure.TopicClass)int.Parse(dr.GetByte(2).ToString());
                    if (!dr.IsDBNull(3))
                        model.AreaId = (EyouSoft.Model.CommunityStructure.TopicAreas)int.Parse(dr.GetByte(3).ToString());
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
