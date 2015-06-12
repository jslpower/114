//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Data.Common;
//using EyouSoft.Common.DAL;
//using EyouSoft.IDAL.SystemStructure;
//using Microsoft.Practices.EnterpriseLibrary.Data;

//namespace EyouSoft.DAL.SystemStructure
//{
//    /// <summary>
//    /// 创建人：鲁功源  2010-06-30
//    /// 描述：新闻类别数据层
//    /// </summary>
//    public class AfficheType:DALBase,IAfficheType
//    {
//        /// <summary>
//        /// 数据库变量
//        /// </summary>
//        protected Database _database;

//        #region 构造函数
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public AfficheType() 
//        {
//            this._database = base.SystemStore;
//        }
//        #endregion

//        #region SQL变量定义
//        private const string SQL_AfficheType_EXISTS = "SELECT COUNT(1) from tbl_AfficheType where ID<>@ID AND ClassName=@ClassName ";
//        private const string SQL_AfficheType_INSERT = "INSERT INTO tbl_AfficheType(ClassName,OperatorID) VALUES(@ClassName,@OperatorID)";
//        private const string SQL_AfficheType_UPDATE = "UPDATE tbl_AfficheType Set ClassName=@ClassName Where ID=@ID";
//        private const string SQL_AfficheType_SELECT = "SELECT ID,ClassName from tbl_AfficheType";
//        #endregion

//        /// <summary>
//        /// 验证是否存在同名称的记录
//        /// </summary>
//        /// <param name="ClassName">类别名称</param>
//        /// <param name="ID">主键编号</param>
//        /// <returns>true：存在 false:不存在</returns>
//        public virtual bool Exists(string ClassName, int ID)
//        {
//            DbCommand dc = this._database.GetSqlStringCommand(SQL_AfficheType_EXISTS);
//            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
//            this._database.AddInParameter(dc, "ClassName", DbType.String, ClassName);
//            return DbHelper.Exists(dc, this._database);
//        }
//        /// <summary>
//        ///新增类别
//        /// </summary>
//        /// <param name="model">新闻类别实体</param>
//        /// <returns>true:成功 false:失败</returns>
//        public virtual bool Add(EyouSoft.Model.SystemStructure.AfficheType model)
//        {
//            DbCommand dc = this._database.GetSqlStringCommand(SQL_AfficheType_INSERT);
//            this._database.AddInParameter(dc, "ClassName", DbType.String, model.ClassName);
//            this._database.AddInParameter(dc, "OperatorID", DbType.Int32, model.OperatorID);
//            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
//        }
//        /// <summary>
//        /// 修改类别
//        /// </summary>
//        /// <param name="model">新闻类别实体</param>
//        /// <returns>true:成功 false:失败</returns>
//        public virtual bool Update(EyouSoft.Model.SystemStructure.AfficheType model)
//        {
//            DbCommand dc = this._database.GetSqlStringCommand(SQL_AfficheType_UPDATE);
//            this._database.AddInParameter(dc, "ClassName", DbType.String, model.ClassName);
//            this._database.AddInParameter(dc, "ID", DbType.Int32, model.ID);
//            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
//        }
//        /// <summary>
//        /// 分页获取所有类别列表
//        /// </summary>
//        /// <param name="pageSize">每页显示条数</param>
//        /// <param name="pageIndex">当前页码</param>
//        /// <param name="recordCount">总记录数</param>
//        /// <returns>新闻类别列表</returns>
//        public virtual IList<EyouSoft.Model.SystemStructure.AfficheType> GetList(int pageSize, int pageIndex, ref int recordCount)
//        {
//            IList<EyouSoft.Model.SystemStructure.AfficheType> list = new List<EyouSoft.Model.SystemStructure.AfficheType>();
//            string tableName = "tbl_AfficheType";
//            string fields = "ID,ClassName,IssueTime";
//            string primaryKey = "ID";
//            string orderByString = "IssueTime DESC";
//            using(IDataReader dr=DbHelper.ExecuteReader(this._database,pageSize,pageIndex,ref recordCount,tableName,primaryKey,fields,string.Empty,orderByString))
//            {
//                while(dr.Read())
//                {
//                    EyouSoft.Model.SystemStructure.AfficheType model=new EyouSoft.Model.SystemStructure.AfficheType();
//                    model.ID=dr.GetInt32(0);
//                    model.ClassName=dr.GetString(1);
//                    model.IssueTime=dr.GetDateTime(2);
//                    list.Add(model);
//                    model=null;
//                }
//            }
//            return list;
//        }
//        /// <summary>
//        /// 获取所有新闻类别列表
//        /// </summary>
//        /// <returns></returns>
//        public virtual IList<EyouSoft.Model.SystemStructure.AfficheType> GetAllList()
//        {
//            IList<EyouSoft.Model.SystemStructure.AfficheType> list = new List<EyouSoft.Model.SystemStructure.AfficheType>();
//            DbCommand dc = this._database.GetSqlStringCommand(SQL_AfficheType_SELECT);
//            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
//            {
//                while (dr.Read())
//                {
//                    EyouSoft.Model.SystemStructure.AfficheType model = new EyouSoft.Model.SystemStructure.AfficheType();
//                    model.ID = dr.GetInt32(0);
//                    model.ClassName = dr.GetString(1);
//                    list.Add(model);
//                    model = null;
//                }
//            }
//            return list;
//        }
//    }
//}
