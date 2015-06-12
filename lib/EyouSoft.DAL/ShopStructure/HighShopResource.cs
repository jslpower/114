using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.ShopStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace EyouSoft.DAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-02
    /// 描述：旅游资源推荐数据层
    /// </summary>
    public class HighShopResource:DALBase,IHighShopResource
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopResource() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopResource_ADD = "INSERT INTO tbl_HighShopTripGuide(ID,CompanyID,OperatorID,TypeID,Title,ContentText,ImagePath) VALUES(@ID,@CompanyID,@OperatorID,@TypeID,@Title,@ContentText,@ImagePath)";
        const string SQL_HighShopResource_UPDATE = "UPDATE tbl_HighShopTripGuide set Title=@Title,ContentText=@ContentText,ImagePath=@ImagePath,UpdateTime=getdate() where ID=@ID";
        const string SQL_HighShopResource_DELETE = "DELETE tbl_HighShopTripGuide where ID=@ID";
        const string SQL_HighShopResource_GETMODEL = "SELECT ID,CompanyID,OperatorID,Title,ContentText,ImagePath,UpdateTime,IsTop,TopTime from tbl_HighShopTripGuide WHERE ID=@ID";
        const string SQL_HighShopResource_GETTOPLIST = "SELECT {0} ID,Title,ImagePath,IsTop,TopTime from tbl_HighShopTripGuide ";
        const string SQL_HighShopResource_SETTOP = "UPDATE tbl_HighShopTripGuide SET IsTop=@IsTop,TopTime=(case when @IsTop='1' then getdate() else null end) where ID=@ID";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_HighShopTripGuide] WHERE [ID]=@ID AND [ImagePath]=@ImagePath)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImagePath] FROM [tbl_HighShopTripGuide] WHERE [ID]=@ID AND ImagePath<>'';";
        const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImagePath] FROM [tbl_HighShopTripGuide] WHERE [ID]=@ID AND ImagePath<>'';";
        #endregion

        #region IHighShopResource成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">旅游资源推荐实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.ShopStructure.HighShopResource model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopResource_ADD);
            model.ID = Guid.NewGuid().ToString();
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc, "TypeID", DbType.Byte, 4);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ContentText", DbType.String, model.ContentText);
            this._database.AddInParameter(dc, "ImagePath", DbType.String, model.ImagePath);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">旅游资源推荐实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.ShopStructure.HighShopResource model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE + SQL_HighShopResource_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ContentText", DbType.String, model.ContentText);
            this._database.AddInParameter(dc, "ImagePath", DbType.String, model.ImagePath);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_DELETEMOVE + SQL_HighShopResource_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">置顶状态</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetTop(string ID, bool IsTop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopResource_SETTOP);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取旅游资源推荐实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ShopStructure.HighShopResource GetModel(string ID)
        {
            EyouSoft.Model.ShopStructure.HighShopResource model=null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopResource_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                { 
                    model=new EyouSoft.Model.ShopStructure.HighShopResource();
                    model.ID=dr.GetString(0);
                    model.CompanyID=dr.GetString(1);
                    model.OperatorID=dr.GetString(2);
                    model.Title=dr.GetString(3);
                    model.ContentText=dr.IsDBNull(4) ? "" : dr.GetString(4);
                    model.ImagePath=dr.IsDBNull(5) ? "": dr.GetString(5);
                    model.IssueTime = dr.IsDBNull(6) ? DateTime.Now : dr.GetDateTime(6);
                    model.IsTop = dr.IsDBNull(7) ? false : dr.GetString(7) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(8) ? DateTime.MinValue : dr.GetDateTime(8);
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取旅游资源推荐列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的数据</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>旅游资源推荐列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopResource> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopResource> list = new List<EyouSoft.Model.ShopStructure.HighShopResource>();
            string tableName = "tbl_HighShopTripGuide";
            string fields = "ID,Title,ImagePath,IsTop,TopTime,UpdateTime";
            string primaryKey = "ID";
            string orderByString = "IsTop desc,TopTime desc,UpdateTime desc";

            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" TypeId=4 ");
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strWhere.AppendFormat(" AND CompanyID='{0}' ", CompanyID);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere.AppendFormat(" AND title like'%{0}%'", KeyWord);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopResource model = new EyouSoft.Model.ShopStructure.HighShopResource();
                    model.ID = dr.GetString(0);
                    model.Title = dr.IsDBNull(1) ? "" : dr.GetString(1);
                    model.ImagePath = dr.IsDBNull(2) ? "" : dr.GetString(2);
                    model.IsTop = dr.IsDBNull(3) ? false : dr.GetString(3) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(4) ? DateTime.Now : dr.GetDateTime(4);
                    model.IssueTime = dr.IsDBNull(5) ? DateTime.Now : dr.GetDateTime(5);
                    list.Add(model);
                    model = null;
                }
            }
            return list;        
        }
        /// <summary>
        /// 获取首页旅游资源推荐列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数 =0返回全部 >0返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>旅游资源推荐列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopResource> GetIndexList(int topNumber, string CompanyID)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopResource> list = new List<EyouSoft.Model.ShopStructure.HighShopResource>();
            StringBuilder strSql = new StringBuilder();
            if (topNumber > 0)
            {
                strSql.AppendFormat(SQL_HighShopResource_GETTOPLIST, "Top " + topNumber.ToString());
            }
            else
            {
                strSql.AppendFormat(SQL_HighShopResource_GETTOPLIST, string.Empty);
            }
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strSql.AppendFormat(" where companyID='{0}' AND TypeId=4 ", CompanyID);
            }
            strSql.Append(" order by IsTop desc,TopTime desc,UpdateTime desc");

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopResource model = new EyouSoft.Model.ShopStructure.HighShopResource();
                    model.ID = dr.GetString(0);
                    model.Title = dr.IsDBNull(1) ? "": dr.GetString(1);
                    model.ImagePath = dr.IsDBNull(2) ? "": dr.GetString(2);
                    model.IsTop = dr.IsDBNull(3) ? false : dr.GetString(3) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(4) ? DateTime.Now : dr.GetDateTime(4);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
