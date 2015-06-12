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
    /// 创建人：鲁功源 2010-07-06
    /// 描述：权限大类别数据层
    /// </summary>
    public class SysPermissionCategory:DALBase,ISysPermissionCategory
    {
        #region 构造函数
        /// <summary>
        /// 数据库变量
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermissionCategory() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        private const string SQL_SysPermissionCategory_ADD = "INSERT INTO tbl_SysPermissionCategory(typeid,CategoryName) VALUES(typeid,CategoryName)";
        private const string SQL_SysPermissionCategory_UPDATE = "UPDATE tbl_SysPermissionCategory SET TypeId=@TypeId,CategoryName=@CategoryName,SortId=@SortId,IsEnable=@IsEnable WHERE ID=@ID";
        private const string SQL_SysPermissionCategory_DELETE = "DELETE tbl_SysPermissionCategory WHERE ID in(@IDs)";
        private const string SQL_SysPermissionCategroy_SetEnable = "UPDATE tbl_SysPermissionCategory SET IsEnable=@IsEnable WHERE ID in(@IDs)";
        private const string SQL_SysPermissionCategroy_GetModel = "SELECT ID,TypeId,CategoryName,SortId,IsEnable from tbl_SysPermissionCategory WHERE ID=@ID";
        private const string SQL_SysPermissionCategroy_SELECT = "SELECT ID,TypeId,CategoryName,IsEnable,SortId from tbl_SysPermissionCategory";
        #endregion

        #region ISysPermissionCategory成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">系统权限大类别实体类</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.SystemStructure.SysPermissionCategory model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionCategory_ADD);
            this._database.AddInParameter(dc, "typeid", DbType.Int32, model.TypeId);
            this._database.AddInParameter(dc, "CategroyName", DbType.String, model.CategoryName);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">系统权限大类别实体类</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.SystemStructure.SysPermissionCategory model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionCategory_UPDATE);
            this._database.AddInParameter(dc, "TypeId", DbType.Int32, model.TypeId);
            this._database.AddInParameter(dc, "CategroyName", DbType.String, model.CategoryName);
            this._database.AddInParameter(dc, "SortId", DbType.Int32, model.SortId);
            this._database.AddInParameter(dc, "IsEnable", DbType.String, model.IsEnable ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">主键编号集合(1,2,3,4)</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string Ids)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionCategory_DELETE);
            this._database.AddInParameter(dc, "IDs", DbType.String, Ids);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="Ids">权限编号(1,2,3,4)</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetEnable(string Ids, bool IsEnable)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionCategroy_SetEnable);
            this._database.AddInParameter(dc, "IsEnable", DbType.String, IsEnable ? "1" : "0");
            this._database.AddInParameter(dc, "IDs", DbType.String, Ids);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.SysPermissionCategory GetModel(int ID)
        {
            EyouSoft.Model.SystemStructure.SysPermissionCategory model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionCategroy_GetModel);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysPermissionCategory();
                    model.Id = dr.GetInt32(0);
                    model.TypeId = dr.GetInt32(1);
                    model.CategoryName = dr.GetString(2);
                    model.SortId = dr.GetInt32(3);
                    model.IsEnable = dr.GetString(4) == "1" ? true : false;
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取权限大类别列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="PermissionType">权限类别数组（如组团，地接等等）</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(int pageSize, int pageIndex, ref int recordCount, int[] PermissionType)
        {
            SysPermissionClass permissionClass = new SysPermissionClass();
            IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> list = new List<EyouSoft.Model.SystemStructure.SysPermissionCategory>();
            string tableName = "tbl_SysPermissionCategory";
            string fields = "ID,TypeId,CatagroyName,IsEnable,SortId";
            string primaryKey = "ID";
            string OrderbyString = "SortId asc,ID desc";

            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder();
            if (PermissionType != null && PermissionType.Length > 0)
            {
                strWhere.Append("TypeId in(");
                for (int i = 0; i < PermissionType.Length; i++)
                {
                    strWhere.AppendFormat("{0}{1}", i > 0 ? "," : "", PermissionType[i].ToString());
                }
                strWhere.Append(")");
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), OrderbyString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.SysPermissionCategory model = new EyouSoft.Model.SystemStructure.SysPermissionCategory();
                    model.Id = dr.GetInt32(0);
                    model.TypeId = dr.GetInt32(1);
                    model.CategoryName = dr.GetString(2);
                    model.IsEnable=dr.GetString(3)== "1" ? true : false;
                    model.SortId = dr.GetInt32(4);
                    model.SysPermissionClass = permissionClass.GetList(string.Empty, model.Id, true);
                    list.Add(model);
                    model = null;
                }
            }
            permissionClass = null;
            return list;
        }
        /// <summary>
        /// 获取指定权限类别的权限详细列表
        /// </summary>
        /// <param name="PermissionType">权限类别数组（如组团，地接等等）</param>
        /// <param name="LoadPermission">是否立即读取明细权限</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> GetList(int[] PermissionType, bool LoadPermission)
        {
            IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> list = new List<EyouSoft.Model.SystemStructure.SysPermissionCategory>();
            SysPermissionClass permissionClass = new SysPermissionClass();
            #region 生成查询语句
            StringBuilder strSql = new StringBuilder();
            strSql.Append(SQL_SysPermissionCategroy_SELECT);
            if (PermissionType != null && PermissionType.Length > 0)
            {
                strSql.Append(" where typeId in(");
                for (int i = 0; i < PermissionType.Length; i++)
                {
                    strSql.AppendFormat("{0}{1}", i > 0 ? "," : "", PermissionType[i].ToString());
                }
                strSql.Append(")");
            }
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.SysPermissionCategory model = new EyouSoft.Model.SystemStructure.SysPermissionCategory();
                    model.Id = dr.GetInt32(0);
                    model.TypeId = dr.GetInt32(1);
                    model.CategoryName = dr.GetString(2);
                    model.IsEnable = dr.GetString(3) == "1" ? true : false;
                    model.SortId = dr.GetInt32(4);
                    model.SysPermissionClass = permissionClass.GetList(string.Empty, model.Id, LoadPermission);
                    list.Add(model);
                    model = null;
                }
            }
            permissionClass = null;
            return list;
        }
        #endregion

    }
}
