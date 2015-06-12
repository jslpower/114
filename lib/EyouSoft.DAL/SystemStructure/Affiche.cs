using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-20
    /// 描述：公告信息数据层
    /// </summary>
    public class Affiche : DALBase, IAffiche
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public Affiche()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_Affiche_INSERT = "INSERT INTO tbl_Affiche(OperatorID,OperatorName,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime,Clicks) values(@OperatorID,@OperatorName,@AfficheClass,@AfficheTitle,@AfficheInfo,@IsHot,@IsPicNews,@PicPath,@IssueTime,@Clicks);SELECT @@IDENTITY;";
        const string SQL_Affiche_UPDATE = "UPDATE tbl_Affiche set OperatorID=@OperatorID,OperatorName=@OperatorName,AfficheClass=@AfficheClass,AfficheTitle=@AfficheTitle,AfficheInfo=@AfficheInfo,IsHot=@IsHot,IsPicNews=@IsPicNews,PicPath=@PicPath WHERE ID=@ID";
        const string SQL_Affiche_DELETE = "DELETE tbl_Affiche WHERE ID=@ID";
        const string SQL_Affiche_SETTOP = "UPDATE tbl_Affiche set IsHot=@IsTop where ID=@ID";
        const string SQL_Affiche_SETREADCOUNT = "UPDATE tbl_Affiche set Clicks=Clicks+1 where ID=@ID";
        const string SQL_Affiche_SELECT = "SELECT ID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime from tbl_Affiche order by IsHot DESC,IssueTime DESC";
        const string SQL_Affiche_GETTOPLIST = "SELECT {0} ID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime,Clicks from tbl_Affiche {1} order by IsHot DESC,IssueTime DESC";
        const string SQL_Affiche_GETMODEL = "SELECT ID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime,OperatorID,OperatorName,Clicks from tbl_Affiche where ID=@ID";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_Affiche] WHERE [ID]=@ID AND [PicPath]=@PicPath)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [PicPath] FROM [tbl_Affiche] WHERE [ID]=@ID AND PicPath<>'';";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [PicPath] FROM [tbl_Affiche] WHERE [ID]=@ID AND PicPath<>'';";
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.SystemStructure.Affiche model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Affiche_INSERT);
            this._database.AddInParameter(dc, "OperatorID", DbType.Int32, model.OperatorID);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "AfficheClass", DbType.Byte, (int)model.AfficheClass);
            this._database.AddInParameter(dc, "AfficheTitle", DbType.String, model.AfficheTitle);
            this._database.AddInParameter(dc, "AfficheInfo", DbType.String, model.AfficheInfo);
            this._database.AddInParameter(dc, "Clicks", DbType.Int32, 0);
            this._database.AddInParameter(dc, "IsHot", DbType.AnsiStringFixedLength, model.IsHot ? "1" : "0");
            this._database.AddInParameter(dc, "IsPicNews", DbType.AnsiStringFixedLength, model.IsPicNews ? "1" : "0");
            this._database.AddInParameter(dc, "PicPath", DbType.String, model.PicPath);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            object obj = DbHelper.GetSingle(dc, base.SystemStore);
            if (obj == null)
            {
                return false;
            }
            else
            {
                model.ID = Convert.ToInt32(obj);
                return Convert.ToInt32(obj) > 0 ? true : false;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.SystemStructure.Affiche model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE + SQL_Affiche_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, model.ID);
            this._database.AddInParameter(dc, "OperatorID", DbType.Int32, model.OperatorID);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "AfficheClass", DbType.Int16, model.AfficheClass);
            this._database.AddInParameter(dc, "AfficheTitle", DbType.String, model.AfficheTitle);
            this._database.AddInParameter(dc, "AfficheInfo", DbType.String, model.AfficheInfo);
            this._database.AddInParameter(dc, "IsHot", DbType.AnsiStringFixedLength, model.IsHot ? "1" : "0");
            this._database.AddInParameter(dc, "IsPicNews", DbType.AnsiStringFixedLength, model.IsPicNews ? "1" : "0");
            this._database.AddInParameter(dc, "PicPath", DbType.String, model.PicPath);
            //this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(int id)
        {           
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_DELETEMOVE + SQL_Affiche_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取指定行数的新闻信息
        /// </summary>
        /// <param name="topNumber">需要返回的行数 =0返回全部</param>
        /// <param name="affichType">新闻类别 =null返回全部</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.Affiche> GetTopList(int topNumber, EyouSoft.Model.SystemStructure.AfficheType? affichType)
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> list = new List<EyouSoft.Model.SystemStructure.Affiche>();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_Affiche_GETTOPLIST, topNumber > 0 ? string.Format(" top {0} ", topNumber) : string.Empty,
                affichType.HasValue ? string.Format(" where AfficheClass={0} ", (int)affichType.Value) : string.Empty);
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.Affiche model = new EyouSoft.Model.SystemStructure.Affiche();
                    model.ID =dr.IsDBNull(dr.GetOrdinal("ID"))?0:dr.GetInt32(dr.GetOrdinal("ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AfficheClass")))
                        model.AfficheClass = (EyouSoft.Model.SystemStructure.AfficheType)int.Parse(dr.GetByte(dr.GetOrdinal("AfficheClass")).ToString());
                    model.AfficheTitle =dr.IsDBNull(dr.GetOrdinal("AfficheTitle"))?"":dr.GetString(dr.GetOrdinal("AfficheTitle"));
                    model.AfficheInfo = dr.IsDBNull(dr.GetOrdinal("AfficheInfo")) ? "" : dr.GetString(dr.GetOrdinal("AfficheInfo"));
                    model.IsHot = dr.IsDBNull(dr.GetOrdinal("IsHot")) ? false : (dr.GetString(dr.GetOrdinal("IsHot")) == "1" ? true : false);
                    model.IsPicNews = dr.IsDBNull(dr.GetOrdinal("IsPicNews")) ? false : (dr.GetString(dr.GetOrdinal("IsPicNews")) == "1" ? true : false);
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath")) ? "" : dr.GetString(dr.GetOrdinal("PicPath"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.Clicks = dr.IsDBNull(dr.GetOrdinal("Clicks")) ? 0 : dr.GetInt32(dr.GetOrdinal("Clicks"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.Affiche GetModel(int ID)
        {
            EyouSoft.Model.SystemStructure.Affiche model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Affiche_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.Affiche();
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AfficheClass")))
                        model.AfficheClass = (EyouSoft.Model.SystemStructure.AfficheType)int.Parse(dr.GetByte(dr.GetOrdinal("AfficheClass")).ToString());
                    model.AfficheTitle = dr.IsDBNull(dr.GetOrdinal("AfficheTitle")) ? "" : dr.GetString(dr.GetOrdinal("AfficheTitle"));
                    model.AfficheInfo = dr.IsDBNull(dr.GetOrdinal("AfficheInfo")) ? "" : dr.GetString(dr.GetOrdinal("AfficheInfo"));
                    model.IsHot = dr.IsDBNull(dr.GetOrdinal("IsHot")) ? false : (dr.GetString(dr.GetOrdinal("IsHot")) == "1" ? true : false);
                    model.IsPicNews = dr.IsDBNull(dr.GetOrdinal("IsPicNews")) ? false : (dr.GetString(dr.GetOrdinal("IsPicNews")) == "1" ? true : false);
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath")) ? "" : dr.GetString(dr.GetOrdinal("PicPath"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorID = dr.IsDBNull(dr.GetOrdinal("OperatorID")) ? 0 : dr.GetInt32(dr.GetOrdinal("OperatorID"));
                    model.OperatorName =dr.IsDBNull(dr.GetOrdinal("OperatorName"))?string.Empty:dr.GetString(dr.GetOrdinal("OperatorName"));
                    model.Clicks = dr.IsDBNull(dr.GetOrdinal("Clicks")) ? 0 : dr.GetInt32(dr.GetOrdinal("Clicks"));
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="affichType">新闻类别 =null返回全部</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.Affiche> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SystemStructure.AfficheType? affichType)
        {
            IList<EyouSoft.Model.SystemStructure.Affiche> list = new List<EyouSoft.Model.SystemStructure.Affiche>();
            string tableName = "tbl_Affiche";
            string fields = "ID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime,OperatorID,Clicks";
            string primaryKey = "ID";
            string orderByString = " IsHot DESC,IssueTime DESC ";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (affichType.HasValue)
            {
                strWhere.AppendFormat(" and  AfficheClass={0} ", (int)affichType.Value);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.Affiche model = new EyouSoft.Model.SystemStructure.Affiche();
                    model.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AfficheClass")))
                        model.AfficheClass = (EyouSoft.Model.SystemStructure.AfficheType)int.Parse(dr.GetByte(dr.GetOrdinal("AfficheClass")).ToString());
                    model.AfficheTitle = dr.IsDBNull(dr.GetOrdinal("AfficheTitle")) ? "" : dr.GetString(dr.GetOrdinal("AfficheTitle"));
                    model.AfficheInfo = dr.IsDBNull(dr.GetOrdinal("AfficheInfo")) ? "" : dr.GetString(dr.GetOrdinal("AfficheInfo"));
                    model.IsHot = dr.IsDBNull(dr.GetOrdinal("IsHot")) ? false : (dr.GetString(dr.GetOrdinal("IsHot")) == "1" ? true : false);
                    model.IsPicNews = dr.IsDBNull(dr.GetOrdinal("IsPicNews")) ? false : (dr.GetString(dr.GetOrdinal("IsPicNews")) == "1" ? true : false);
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath")) ? "" : dr.GetString(dr.GetOrdinal("PicPath"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.Clicks = dr.IsDBNull(dr.GetOrdinal("Clicks")) ? 0 : dr.GetInt32(dr.GetOrdinal("Clicks"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetTop(int id, bool istop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Affiche_SETTOP);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, istop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool UpdateReadCount(int id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Affiche_SETREADCOUNT);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        #endregion
    }
}
