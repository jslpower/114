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
    /// 描述：
    /// </summary>
    public class SysPermissionClass:DALBase,IDAL.SystemStructure.ISysPermissionClass
    {
        #region 构造函数
        /// <summary>
        /// 数据库变量
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermissionClass() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL变量
        private const string SQL_SysPermissionClass_ADD = "INSERT INTO tbl_SysPermissionClass(CategoryId,ClassName) VALUES(@CategoryId,@ClassName)";
        private const string SQL_SysPermissionClass_UPDATE = "UPDATE tbl_SysPermissionClass SET CategoryId=@CategoryId,ClassName=@ClassName,SortId=@SortID,IsEnable=@IsEnable Where ID=@ID";
        private const string SQL_SysPermissionClass_DELETE = "DELETE tbl_SysPermissionClass WHERE ID in({0}) ";
        private const string SQL_SysPermissionClass_SetEnable = "UPDATE tbl_SysPermissionClass SET IsEnable='{0}' WHERE ID in({1})";
        private const string SQL_SysPermissionClass_SELECT = "SELECT ID,CategoryId,ClassName,SortId,IsEnable from tbl_SysPermissionClass ";
        #endregion

        #region ISysPermissionClass成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">系统权限子类别实体类</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.SystemStructure.SysPermissionClass model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionClass_ADD);
            this._database.AddInParameter(dc, "CategoryId", DbType.Int32, model.CategoryId);
            this._database.AddInParameter(dc, "ClassName", DbType.String, model.ClassName);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">系统权限子类别实体类</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.SystemStructure.SysPermissionClass model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionClass_UPDATE);
            this._database.AddInParameter(dc, "CategoryId", DbType.Int32, model.CategoryId);
            this._database.AddInParameter(dc, "ClassName", DbType.String, model.ClassName);
            this._database.AddInParameter(dc, "SortId", DbType.Int32, model.SortId);
            this._database.AddInParameter(dc, "IsEnable", DbType.String, model.IsEnable?"1":"0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">主键编号集合(如：1,2,3,4)</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string Ids)
        {
            DbCommand dc = this._database.GetSqlStringCommand(string.Format(SQL_SysPermissionClass_DELETE, Ids));
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="Ids">权限编号集合(如：1,2,3,4)</param>
        /// <param name="IsEnable">启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetEnable(string Ids, bool IsEnable)
        {
            DbCommand dc = this._database.GetSqlStringCommand(string.Format(SQL_SysPermissionClass_SetEnable, IsEnable ? "1" : "0", Ids));
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 根据权限类别，权限大类别获取数据
        /// </summary>
        /// <param name="PermissionTypes">权限类别集合</param>
        /// <param name="CategroyType">权限大类别 >0返回指定大类别的数据，否则返回全部</param>
        /// <param name="LoadPermission">是否立即读取明细权限</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysPermissionClass> GetList(string PermissionTypes, int CategroyType, bool LoadPermission)
        {
            #region 生成SQL
            StringBuilder strSql = new StringBuilder(SQL_SysPermissionClass_SELECT);
            if (!string.IsNullOrEmpty(PermissionTypes))
            {
                strSql.AppendFormat(" WHERE CategoryId in(select id from tbl_SysPermissionCategory where TypeId in({0}))", PermissionTypes);
            }
            if (CategroyType > 0)
            {
                strSql.AppendFormat(" WHERE CategoryId={0} ", CategroyType);
            }
            #endregion
            SysPermission Permission = new SysPermission();
            IList<EyouSoft.Model.SystemStructure.SysPermissionClass> list = new List<EyouSoft.Model.SystemStructure.SysPermissionClass>();
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.SysPermissionClass model = new EyouSoft.Model.SystemStructure.SysPermissionClass();
                    model.Id = dr.GetInt32(0);
                    model.CategoryId = dr.GetInt32(1);
                    model.ClassName = dr.GetString(2);
                    model.SortId = dr.GetInt32(3);
                    model.IsEnable = dr.GetString(4) == "1" ? true : false;
                    if (!LoadPermission)
                    {
                        model.SysPermission = new List<EyouSoft.Model.SystemStructure.SysPermission>();
                    }
                    else
                    {
                        model.SysPermission = Permission.GetSysPermissionList(0, model.Id);
                    }
                    list.Add(model);
                    model = null;
                }
            }
            Permission = null;
            return list;
        }
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.SysPermissionClass GetModel(int ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SysPermissionClass_SELECT + string.Format(" WHERE ID={0}", ID));
            EyouSoft.Model.SystemStructure.SysPermissionClass model = null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysPermissionClass();
                    model.Id = dr.GetInt32(0);
                    model.CategoryId = dr.GetInt32(1);
                    model.ClassName = dr.GetString(2);
                    model.SortId = dr.GetInt32(3);
                    model.IsEnable = dr.GetString(4) == "1" ? true : false;
                }
            }
            return model;
        }
        #endregion
    }
}
