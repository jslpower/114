using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// 创建人：郑知远 2011-05-26
    /// 描述：MQ同业中心公告数据层
    /// </summary>
    public class IMSuperClusterNews:DALBase,IDAL.MQStructure.IIMSuperClusterNews
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMSuperClusterNews()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region 后台运用成员方法

        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Add(EyouSoft.Model.MQStructure.IMSuperClusterNews model)
        {
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" DECLARE @VALUE NVARCHAR(500)");
            strSql.Append(" SET @VALUE = STUFF((SELECT ',' + A.items FROM (SELECT i.items FROM dbo.fn_Split(@Centres ,',') AS i WHERE i.items IN (SELECT id FROM tbl_SuperCluster)) AS A FOR XML PATH('')) , 1 , 1 , '')");
            strSql.Append(" INSERT INTO tbl_SuperClusterNews(");
            strSql.Append("     Category");                                                             // 公告类型
            strSql.Append("     ,Num");                                                                 // 序号
            strSql.Append("     ,Title");                                                               // 标题
            strSql.Append("     ,Centres");                                                             // 同业中心ID
            strSql.Append("	    ,NewsContent");                                                         // 正文
            strSql.Append("	    ,Operater");                                                           // 发布人
            strSql.Append("	    ,OperateTime");                                                         // 发布时间
            strSql.Append("     ,IssueTime)");
            strSql.Append(" VALUES(");
            strSql.Append("     @Category");                                                            // 公告类型
            strSql.Append("     ,@Num");                                                                // 序号
            strSql.Append("     ,@Title");                                                              // 标题
            strSql.Append("     ,@VALUE");                                                            // 同业中心ID
            strSql.Append("	    ,@NewsContent");                                                        // 正文
            strSql.Append("	    ,@Operater");                                                          // 发布人
            strSql.Append("	    ,@OperateTime");                                                        // 发布时间
            strSql.Append("     ,GETDATE())");

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "Category", DbType.Byte, (int)model.Category);
            this._database.AddInParameter(dc, "Num", DbType.Int32, model.Num);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "Centres", DbType.String, model.Centres);
            this._database.AddInParameter(dc, "NewsContent", DbType.String, model.NewsContent);
            this._database.AddInParameter(dc, "Operater", DbType.String, model.Operater);
            this._database.AddInParameter(dc, "OperateTime", DbType.DateTime, model.OperateTime);
            
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Upd(EyouSoft.Model.MQStructure.IMSuperClusterNews model)
        {
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" UPDATE");
            strSql.Append("     tbl_SuperClusterNews");
            strSql.Append(" SET");
            strSql.Append("     Category = @Category");                                                 // 公告类型
            strSql.Append("     ,Num = @Num");                                                          // 序号
            strSql.Append("     ,Title = @Title");                                                      // 标题
            strSql.Append("     ,Centres = STUFF((SELECT ',' + A.items FROM (SELECT i.items FROM dbo.fn_Split(@Centres ,',') AS i WHERE i.items IN (SELECT id FROM tbl_SuperCluster)) AS A FOR XML PATH('')) , 1 , 1 , '')");                                                  // 同业中心ID
            strSql.Append("	    ,NewsContent = @NewsContent");                                          // 正文
            strSql.Append("	    ,Operater = @Operater");                                              // 发布人
            strSql.Append("	    ,OperateTime = @OperateTime");                                          // 发布时间
            strSql.Append("     ,IssueTime = GETDATE()");
            strSql.Append(" WHERE");
            strSql.Append("     ID = @ID");                                                             // 公告ID

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "ID", DbType.Int32, model.Id);
            this._database.AddInParameter(dc, "Category", DbType.Byte, (int)model.Category);
            this._database.AddInParameter(dc, "Num", DbType.Int32, model.Num);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "Centres", DbType.String, model.Centres);
            this._database.AddInParameter(dc, "NewsContent", DbType.String, model.NewsContent);
            this._database.AddInParameter(dc, "Operater", DbType.String, model.Operater);
            this._database.AddInParameter(dc, "OperateTime", DbType.DateTime, model.OperateTime);

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 根据公告ID删除公告信息
        /// </summary>
        /// <param name="id">公告ID</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Del(int[] id)
        {
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" DELETE FROM");
            strSql.Append("     tbl_SuperClusterNews");
            strSql.Append(" WHERE");
            strSql.Append("     ID IN (");                                                              // 公告ID
            foreach (var i in id)
            {
                strSql.Append(i + ",");
            }
            strSql.Append("            0)");                                                            // 公告ID

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 设置序号
        /// </summary>
        /// <param name="lst">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetNums(IList<Model.MQStructure.IMSuperClusterNews> lst)
        {
            var xml = new XmlDocument();                                                                // XML对象
            var sbxml = new StringBuilder();                                                            // XML编辑器
            DbCommand dc = null;

            dc = this._database.GetStoredProcCommand("proc_SuperClusterNews_SetNums");                  // 执行存储过程命令

            sbxml.Append("<Values>");

            foreach (var item in lst)
            {
                sbxml.Append("<Value NewsId=\"" + item.Id + "\" NewsNum=\"" + item.Num + "\"/>");
            }

            sbxml.Append("</Values>");

            xml.LoadXml(sbxml.ToString());
            this._database.AddInParameter(dc, "@Values", DbType.Xml, xml.InnerXml);

            try
            {
                return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 根据公告ID获取公告信息
        /// </summary>
        /// <param name="id">公告ID</param>
        /// <returns>公告信息实体</returns>
        public EyouSoft.Model.MQStructure.IMSuperClusterNews GetModel(int id)
        {
            var model = new EyouSoft.Model.MQStructure.IMSuperClusterNews();                            // 公告信息实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" SELECT");
            strSql.Append("     Category");                                                             // 公告类型
            strSql.Append("     ,Num");                                                                 // 序号
            strSql.Append("     ,Title");                                                               // 标题
            strSql.Append("     ,Centres");                                                             // 同业中心ID
            strSql.Append("	    ,NewsContent");                                                         // 正文
            strSql.Append("	    ,Operater");                                                           // 发布人
            strSql.Append("	    ,OperateTime");                                                         // 发布时间
            strSql.Append(" FROM");
            strSql.Append("     tbl_SuperClusterNews");
            strSql.Append(" WHERE");
            strSql.Append("     ID = @ID");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行
            this._database.AddInParameter(cmd, "@ID", DbType.Int32, id);                                // 公告ID

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 公告类型
                    model.Category = (Model.MQStructure.Type)dr.GetByte(dr.GetOrdinal("Category"));

                    // 序号
                    model.Num = dr.GetInt32(dr.GetOrdinal("Num"));

                    // 标题
                    model.Title = dr.GetString(dr.GetOrdinal("Title"));

                    // 【，】隔开同业中心ID
                    model.Centres = dr.GetString(dr.GetOrdinal("Centres"));

                    // 正文
                    model.NewsContent = dr.GetString(dr.GetOrdinal("NewsContent"));

                    // 发布人
                    model.Operater = dr.GetString(dr.GetOrdinal("Operater"));

                    // 发布时间
                    model.OperateTime = dr.GetDateTime(dr.GetOrdinal("OperateTime"));
                }
            }

            // 返回公告信息实体
            return model;
        }

        /// <summary>
        /// 根据同业中心ID获取公告信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>   
        /// <param name="centerId">同业中心ID</param>
        /// <param name="type">公告类型</param>
        /// <returns>公告信息列表</returns>
        public IList<Model.MQStructure.IMSuperClusterNews> GetList(int pageSize, int pageIndex, ref int recordCount, int centerId,EyouSoft.Model.MQStructure.Type type)
        {
            // 公告信息实体列表
            IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> lst = new List<EyouSoft.Model.MQStructure.IMSuperClusterNews>();

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount
                ,"tbl_SuperClusterNews", "ID", "ID,Category,Num,Title,Centres,NewsContent,Operater,OperateTime"
                , centerId != 0 ? string.Format("(SELECT COUNT(i.items) FROM dbo.fn_Split(Centres ,',') AS i WHERE i.items = '{0}') > 0 AND Category = {1}", centerId, (int)type) : string.Format("Category = {0}",(int)type)
                , "Num ASC,OperateTime DESC"))
            {
                while (dr.Read())
                {
                    // 公告信息实体
                    var model = new EyouSoft.Model.MQStructure.IMSuperClusterNews
                                    {
                                        // 公告ID
                                        Id = dr.GetInt32(dr.GetOrdinal("ID")),

                                        // 公告类型
                                        Category = (Model.MQStructure.Type)dr.GetByte(dr.GetOrdinal("Category")),

                                        // 序号
                                        Num = dr.GetInt32(dr.GetOrdinal("Num")),

                                        // 标题
                                        Title = dr.GetString(dr.GetOrdinal("Title")),

                                        // 【，】隔开同业中心ID
                                        Centres = dr.IsDBNull(dr.GetOrdinal("Centres")) ? string.Empty : dr.GetString(dr.GetOrdinal("Centres")),

                                        // 正文
                                        NewsContent = dr.GetString(dr.GetOrdinal("NewsContent")),

                                        // 发布人
                                        Operater = dr.GetString(dr.GetOrdinal("Operater")),

                                        // 发布时间
                                        OperateTime = dr.GetDateTime(dr.GetOrdinal("OperateTime"))
                                    };

                    // 追加到信息列表实体
                    lst.Add(model);
                }
            }

            // 返回公告信息列表实体
            return lst;
        }

        #endregion

        #region 同业中心前台公告成员方法

        /// <summary>
        /// 根据公告类型、同业中心ID获取指定条数公告
        /// </summary>
        /// <param name="top">指定条数</param>
        /// <param name="typ">公告类型</param>
        /// <param name="clusterId">同业中心ID</param>
        /// <returns>公告信息列表实体</returns>
        public IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> GetSuperClusterNews(int top,EyouSoft.Model.MQStructure.Type typ,int clusterId)
        {
            // 公告信息实体列表
            IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> lst = new List<EyouSoft.Model.MQStructure.IMSuperClusterNews>();

            // SQL编辑器
            var strSql = new StringBuilder();

            strSql.Append(" SELECT TOP(@TOP)");
            strSql.Append("     ID");                                                                   // 公告ID
            strSql.Append("     ,Title");                                                               // 标题
            strSql.Append(" FROM");
            strSql.Append("     tbl_SuperClusterNews");
            strSql.Append(" WHERE");
            strSql.AppendFormat("     (SELECT i.items FROM dbo.fn_Split(Centres ,',') AS i WHERE i.items = '{0}') > 0 AND Category = {1}", clusterId, (int)typ);
            strSql.Append(" ORDER BY");
            strSql.Append("     Num ASC,OperateTime DESC");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行
            this._database.AddInParameter(cmd, "@TOP", DbType.Int32, top);                              // 公告ID

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    var model = new EyouSoft.Model.MQStructure.IMSuperClusterNews
                                    {
                                        // 公告ID
                                        Id = dr.GetInt32(dr.GetOrdinal("ID")),

                                        // 标题
                                        Title = dr.GetString(dr.GetOrdinal("Title")),
                                    };
                    
                    // 公告信息实体
                    lst.Add(model);
                }
            }

            // 返回公告信息实体
            return lst;

        }

        #endregion
    }
}
