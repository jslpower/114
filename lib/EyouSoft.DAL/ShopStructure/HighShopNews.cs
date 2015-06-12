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
    /// 描述：最新旅游动态数据层
    /// </summary>
    public class HighShopNews:DALBase,IHighShopNews
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopNews() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopNews_ADD = "INSERT INTO tbl_HighShopNews(ID,CompanyID,OperatorID,TypeID,Title,ContentText,IssueTime) values(@ID,@CompanyID,@OperatorID,1,@Title,@ContentText,@IssueTime)";
        const string SQL_HighShopNews_UPDATE = "UPDATE tbl_HighShopNews set Title=@Title,ContentText=@ContentText where ID=@ID";
        const string SQL_HighShopNews_DELETE = "DELETE tbl_HighShopNews where ID=@ID";
        const string SQL_HighShopNews_GETMODEL = "SELECT ID,Title,ContentText,IsTop,TopTime,IssueTime from tbl_HighShopNews where ID=@ID ORDER BY IsTop desc,TopTime desc,IssueTime desc";
        const string SQL_HighShopNews_SETTOP = "UPDATE tbl_HighShopNews set IsTop=@IsTop,TopTime=(CASE WHEN @IsTop='1' THEN GETDATE() ELSE NULL END) where ID=@ID";
        const string SQL_HighShopNews_GETTOPLIST = "SELECT {0} ID,Title,IssueTime,IsTop,TopTime from tbl_HighShopNews ";
        #endregion

        #region IHighShopNews成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">旅游动态实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.ShopStructure.HighShopNews model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopNews_ADD);
            model.ID = Guid.NewGuid().ToString();
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ContentText", DbType.String, model.ContentText);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改旅游动态
        /// </summary>
        /// <param name="model">旅游动态实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.ShopStructure.HighShopNews model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopNews_UPDATE);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ContentText", DbType.String, model.ContentText);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopNews_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取旅游动态实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>存在返回实体类 不存在返回NUll</returns>
        public virtual EyouSoft.Model.ShopStructure.HighShopNews GetModel(string ID)
        { 
            EyouSoft.Model.ShopStructure.HighShopNews model=null;
            DbCommand dc=this._database.GetSqlStringCommand(SQL_HighShopNews_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using(IDataReader dr=DbHelper.ExecuteReader(dc,this._database))
            {
                if(dr.Read())
                {
                    model=new EyouSoft.Model.ShopStructure.HighShopNews();
                    model.ID=dr.GetString(0);
                    model.Title=dr.GetString(1);
                    model.ContentText=dr.IsDBNull(2) ? "" : dr.GetString(2);
                    model.IsTop= dr.IsDBNull(3) ? false : dr.GetString(3)=="1" ? true:false;
                    model.TopTime=dr.IsDBNull(4) ? DateTime.Now : dr.GetDateTime(4);
                    model.IssueTime = dr.IsDBNull(5) ? DateTime.Now : dr.GetDateTime(5);
                }
            }
            return model;
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">置顶状态 TRUE或者FALSE</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetTop(string ID, bool IsTop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopNews_SETTOP);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取前台页面旅游动态列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="KeyWord">关键字</param>
        /// <returns>旅游动态列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopNews> GetWebList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopNews> list = new List<EyouSoft.Model.ShopStructure.HighShopNews>();
            string tableName = "tbl_HighShopNews";
            string fields = "ID,Title,IssueTime,IsTop,TopTime";
            string primaryKey = "ID";
            string orderByString = "IsTop desc,TopTime desc,IssueTime desc";

            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strWhere.AppendFormat(" CompanyID='{0}' ", CompanyID);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" and title like'%{0}%'", KeyWord);
                else
                    strWhere.AppendFormat(" title like'%{0}%'", KeyWord);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopNews model = new EyouSoft.Model.ShopStructure.HighShopNews();
                    model.ID = dr.GetString(0);
                    model.Title = dr.GetString(1);
                    model.IssueTime =dr.IsDBNull(2)?DateTime.Now:dr.GetDateTime(2);
                    model.IsTop = dr.IsDBNull(3) ? false : dr.GetString(3) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(4) ? DateTime.Now : dr.GetDateTime(4);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取指定公司指定条数的记录
        /// </summary>
        /// <param name="TopNumber">需要返回的记录条数 =0返回全部 >0返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>旅游动态列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopNews> GetTopNumberList(int TopNumber, string CompanyID)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopNews> list = new List<EyouSoft.Model.ShopStructure.HighShopNews>();
            StringBuilder strSql = new StringBuilder();
            if (TopNumber > 0)
            {
                strSql.AppendFormat(SQL_HighShopNews_GETTOPLIST, "Top " + TopNumber.ToString());
            }
            else
            {
                strSql.AppendFormat(SQL_HighShopNews_GETTOPLIST, string.Empty);
            }
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strSql.AppendFormat(" where companyID='{0}' ", CompanyID);
            }
            strSql.Append(" order by IsTop desc,TopTime desc,IssueTime desc");

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopNews model = new EyouSoft.Model.ShopStructure.HighShopNews();
                    model.ID = dr.GetString(0);
                    model.Title = dr.GetString(1);
                    model.IssueTime = dr.GetDateTime(2);
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
