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
    /// 创建人：鲁功源 2010-05-24
    /// 描述：首页轮换广告数据层
    /// </summary>
    public class SysIndexAdv:DALBase,ISysIndexAdv
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysIndexAdv() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_SysIndexAdv_ADD = "INSERT INTO tbl_SysIndexAdv(AdvType,SiteId,ImgPath,LinkAdress,ImgRemark) VALUES(@AdvType,@SiteId,@ImgPath,@LinkAdress,@ImgRemark)";
        const string SQL_SysIndexAdv_UPDATE = "UPDATE tbl_SysIndexAdv set AdvType=@AdvType,SiteId=@SiteId,ImgPath=@ImgPath,LinkAdress=@LinkAdress,ImgRemark=@ImgRemark WHERE ID=@ID";
        const string SQL_SysIndexAdv_DELETE = "DELETE tbl_SysIndexAdv WHERE ID=@ID";
        const string SQL_SysIndexAdv_SetSort = "UPDATE tbl_SysIndexAdv set SortID=@SortID WHERE ID=@ID";
        const string SQL_SysIndexAdv_GETMODEL = "SELECT ID,AdvType,SiteId,ImgPath,LinkAdress,ImgRemark,SortID from tbl_SysIndexAdv WHERE ID=@ID";
        const string SQL_SysIndexAdv_SELECTTOPINFO = "SELECT {0} ID,AdvType,ImgPath,LinkAdress,ImgRemark from tbl_SysIndexAdv ";
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.SystemStructure.SysIndexAdv model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysIndexAdv_ADD);
            this._database.AddInParameter(dc, "AdvType", DbType.Int16, model.AdvType);
            this._database.AddInParameter(dc, "SiteId", DbType.Int32, model.SiteId);
            this._database.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            this._database.AddInParameter(dc, "LinkAdress", DbType.String, model.LinkAdress);
            this._database.AddInParameter(dc, "ImgRemark", DbType.String, model.ImgRemark);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">首页广告实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.SystemStructure.SysIndexAdv model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysIndexAdv_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, model.ID);
            this._database.AddInParameter(dc, "AdvType", DbType.Int16, model.AdvType);
            this._database.AddInParameter(dc, "SiteId", DbType.Int32, model.SiteId);
            this._database.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            this._database.AddInParameter(dc, "LinkAdress", DbType.String, model.LinkAdress);
            this._database.AddInParameter(dc, "ImgRemark", DbType.String, model.ImgRemark);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(int id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysIndexAdv_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 运营后台分页获取列表
        /// </summary>
        /// <param name="siteId">分站ID >0时返回该分站下的下广告 =0时返回全部</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码数</param>
        /// <param name="recordeCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysIndexAdv> GetList(int siteId,int pageSize,int pageIndex,ref int recordeCount)
        {
            IList<EyouSoft.Model.SystemStructure.SysIndexAdv> list = new List<EyouSoft.Model.SystemStructure.SysIndexAdv>();
            string tableName = "tbl_SysIndexAdv";
            string fields = "ID,SiteId,ImgPath,LinkAdress,ImgRemark,SortId,IssueTime";
            string primaryKey = "ID";
            string orderByString = " SortId DESC,IssueTime DESC ";
            string strWhere = string.Empty;
            if (siteId > 0)
            {
                strWhere = string.Format("siteid={0}",siteId);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordeCount, tableName, primaryKey, fields, strWhere, orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.SysIndexAdv model = new EyouSoft.Model.SystemStructure.SysIndexAdv();
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.SiteId = dr.GetInt32(dr.GetOrdinal("SiteId"));
                    model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    model.ImgPath = dr.GetString(dr.GetOrdinal("ImgPath"));
                    model.LinkAdress = dr.GetString(dr.GetOrdinal("LinkAdress"));
                    model.ImgRemark = dr.GetString(dr.GetOrdinal("ImgRemark"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取指定条数的前台轮换广告列表
        /// </summary>
        /// <param name="siteId">分站ID >0时返回该分站下的轮换广告信息 =0时返回全部</param>
        /// <param name="topnumber">指定返回条数 >0时返回指定条数记录 =0返回全部</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysIndexAdv> GetTopList(int siteId, int topnumber)
        {
            IList<EyouSoft.Model.SystemStructure.SysIndexAdv> list = new List<EyouSoft.Model.SystemStructure.SysIndexAdv>();
            EyouSoft.Model.SystemStructure.SysIndexAdv model = null;

            #region 格式化查询语句
            StringBuilder strSql = new StringBuilder();
            if (topnumber > 0)
            {
                strSql.AppendFormat(SQL_SysIndexAdv_SELECTTOPINFO, string.Format("top {0} ", topnumber));
            }
            else
            {
                strSql.AppendFormat(SQL_SysIndexAdv_SELECTTOPINFO, string.Empty);
            }
            if (siteId > 0)
            {
                strSql.AppendFormat(" where SiteId={0}", siteId);
            }
            strSql.Append(" order by sortId desc,issuetime desc");
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysIndexAdv();
                    model.ID=dr.GetInt32(dr.GetOrdinal("ID"));
                    model.AdvType=dr.GetInt32(dr.GetOrdinal("AdvType"));
                    model.ImgPath=dr.GetString(dr.GetOrdinal("ImgPath"));
                    model.LinkAdress=dr.GetString(dr.GetOrdinal("LinkAdress"));
                    model.ImgRemark=dr.GetString(dr.GetOrdinal("ImgRemark"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取轮换广告实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.SysIndexAdv GetModel(int id)
        {
            EyouSoft.Model.SystemStructure.SysIndexAdv model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysIndexAdv_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysIndexAdv();
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.AdvType = dr.GetInt16(dr.GetOrdinal("AdvType"));
                    model.SiteId = dr.GetInt32(dr.GetOrdinal("SiteId"));
                    model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    model.ImgPath = dr.GetString(dr.GetOrdinal("ImgPath"));
                    model.LinkAdress = dr.GetString(dr.GetOrdinal("LinkAdress"));
                    model.ImgRemark = dr.GetString(dr.GetOrdinal("ImgRemark"));
                }
            }
            return model;
        }
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <param name="sortnumber">排序值</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetSort(int id, int sortnumber)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysIndexAdv_SetSort);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            this._database.AddInParameter(dc, "SortID", DbType.Int32, sortnumber);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        #endregion
    }
}
