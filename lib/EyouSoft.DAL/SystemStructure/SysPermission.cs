using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 系统权限明细数据访问
    /// </summary>
    /// 周文超 2010-07-05
    public class SysPermission : DALBase, IDAL.SystemStructure.ISysPermission
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermission() { }

        #region SqlString

        private const string Sql_SysPermission_Add = " INSERT INTO [tbl_SysPermissionList] ([CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES (@CategoryId,@ClassId,@PermissionName,@SortId,@IsEnable) ";
        private const string Sql_SysPermission_Update = " UPDATE [tbl_SysPermissionList] SET [CategoryId] = @CategoryId,[ClassId] = @ClassId,[PermissionName] = @PermissionName,[SortId] = @SortId,[IsEnable] = @IsEnable WHERE [Id] = @Id ";
        private const string Sql_SysPermission_Set = " UPDATE [tbl_SysPermissionList] SET [IsEnable] = '{0}' WHERE [Id] in ({1}) ";
        private const string Sql_SysPermission_Delete = " DELETE FROM [tbl_SysPermissionList] WHERE [Id] in ({0}) ";
        private const string Sql_SysPermission_Select = " SELECT [Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable] FROM [tbl_SysPermissionList] ";

        #endregion

        #region ISysPermission 成员

        /// <summary>
        /// 新增系统权限数据
        /// </summary>
        /// <param name="model">系统权限数据实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddSysPermission(EyouSoft.Model.SystemStructure.SysPermission model)
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysPermission_Add);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "CategoryId", DbType.Int32, model.CategoryId);
            base.SystemStore.AddInParameter(dc, "ClassId", DbType.Int32, model.ClassId);
            base.SystemStore.AddInParameter(dc, "PermissionName", DbType.String, model.PermissionName);
            base.SystemStore.AddInParameter(dc, "SortId", DbType.Int32, model.SortId);
            base.SystemStore.AddInParameter(dc, "IsEnable", DbType.String, model.IsEnable ? "1" : "0");

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 修改系统权限数据
        /// </summary>
        /// <param name="model">系统权限数据实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateSysPermission(EyouSoft.Model.SystemStructure.SysPermission model)
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysPermission_Update);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "CategoryId", DbType.Int32, model.CategoryId);
            base.SystemStore.AddInParameter(dc, "ClassId", DbType.Int32, model.ClassId);
            base.SystemStore.AddInParameter(dc, "PermissionName", DbType.String, model.PermissionName);
            base.SystemStore.AddInParameter(dc, "SortId", DbType.Int32, model.SortId);
            base.SystemStore.AddInParameter(dc, "IsEnable", DbType.String, model.IsEnable ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "Id", DbType.Int32, model.Id);

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 批量设置是否启用
        /// </summary>
        /// <param name="SysPermissionIds">系统权限ID集合</param>
        /// <param name="IsEnable">是否启用(1：启用；0：禁用)</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsEnable(string SysPermissionIds, bool IsEnable)
        {
            if (string.IsNullOrEmpty(SysPermissionIds))
                return false;

            string strWhere = string.Format(Sql_SysPermission_Set, IsEnable ? "1" : "0", SysPermissionIds);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 批量删除系统权限数据
        /// </summary>
        /// <param name="SysPermissionIds">系统权限ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysPermission(string SysPermissionIds)
        {
            if (string.IsNullOrEmpty(SysPermissionIds))
                return false;

            string strWhere = string.Format(Sql_SysPermission_Delete, SysPermissionIds);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取系统权限数据实体
        /// </summary>
        /// <param name="SysPermissionId">系统权限ID</param>
        /// <returns>返回系统权限数据实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SysPermission GetSysPermission(int SysPermissionId)
        {
            Model.SystemStructure.SysPermission model = new EyouSoft.Model.SystemStructure.SysPermission();

            string strWhere = Sql_SysPermission_Select + " where [Id] = @Id ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "Id", DbType.Int32, SysPermissionId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        model.Id = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.CategoryId = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.ClassId = dr.GetInt32(2);
                    model.PermissionName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.SortId = dr.GetInt32(4);
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")) && dr["IsEnable"].ToString().Equals("1"))
                        model.IsEnable = true;
                    else
                        model.IsEnable = false;
                }
            }

            return model;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="CategoryId">权限大类编号(小于等于0不作条件)</param>
        /// <param name="ClassId">权限类别编号(小于等于0不作条件)</param>
        /// <returns>返回系统权限数据实体集合</returns>
        public virtual IList<Model.SystemStructure.SysPermission> GetSysPermissionList(int CategoryId, int ClassId)
        {
            IList<Model.SystemStructure.SysPermission> List = new List<Model.SystemStructure.SysPermission>();
            string strWhere = Sql_SysPermission_Select + " where 1 = 1 ";
            if (CategoryId > 0)
                strWhere += string.Format(" and CategoryId = {0} ", CategoryId);
            if (ClassId > 0)
                strWhere += string.Format(" and ClassId = {0} ", ClassId);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysPermission model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysPermission();
                    if (!dr.IsDBNull(0))
                        model.Id = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.CategoryId = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.ClassId = dr.GetInt32(2);
                    model.PermissionName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.SortId = dr.GetInt32(4);
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")) && dr["IsEnable"].ToString().Equals("1"))
                        model.IsEnable = true;
                    else
                        model.IsEnable = false;

                    List.Add(model);
                }
            }
            return List;
        }

        /// <summary>
        /// 获取用户后台所有的（已启用的）权限
        /// </summary>
        /// <param name="companyTypes">用户公司类型集合</param>
        /// <returns></returns>
        public Model.SystemStructure.SysPermissionCategory GetAllPermissionByUser(Model.CompanyStructure.CompanyType[] companyTypes)
        {
            var strSql = new StringBuilder();

            strSql.Append(" declare @Category int; ");
            //个人中心（用户后台）
            strSql.AppendFormat(" select @Category = [Id] from tbl_SysPermissionCategory where [TypeId] = {0} and IsEnable = '1'; ", 2);
            strSql.Append(" select * from tbl_SysPermissionCategory where [Id] = @Category; ");
            strSql.Append(" select *  ");
            strSql.Append(
                " ,(select * FROM tbl_SysPermissionList list WHERE list.CategoryId = @Category AND list.IsEnable = '1' and list.ClassId = tbl_SysPermissionClass.Id for xml raw,root('Root')) as PermissionList ");
            strSql.Append(" from tbl_SysPermissionClass where CategoryId = @Category and IsEnable = '1' ");
            //营销工具 = 97,  系统设置 = 98,  我的网店 = 99
            //营销工具、系统设置、我的网店 所有身份公司都有这些权限
            strSql.Append(" and tbl_SysPermissionClass.Id in (97,98,99 ");
            if (companyTypes != null && companyTypes.Any())
            {
                foreach (var t in companyTypes)
                {
                    switch (t)
                    {
                        case Model.CompanyStructure.CompanyType.专线:
                        case Model.CompanyStructure.CompanyType.组团:
                        case Model.CompanyStructure.CompanyType.地接:
                        case Model.CompanyStructure.CompanyType.景区:
                            strSql.AppendFormat(" ,{0} ", (int)t);
                            break;
                        default:
                            continue;
                    }
                }
            }
            strSql.Append(" ) ; ");

            DbCommand dc = SystemStore.GetSqlStringCommand(strSql.ToString());

            var model = new Model.SystemStructure.SysPermissionCategory();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, SystemStore))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeId")))
                        model.TypeId = dr.GetInt32(dr.GetOrdinal("TypeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CategoryName")))
                        model.CategoryName = dr.GetString(dr.GetOrdinal("CategoryName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SortId")))
                        model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")))
                    {
                        if (dr.GetString(dr.GetOrdinal("IsEnable")) == "1" || dr.GetString(dr.GetOrdinal("IsEnable")).ToLower() == "ture")
                            model.IsEnable = true;
                    }
                }

                dr.NextResult();

                model.SysPermissionClass = new List<Model.SystemStructure.SysPermissionClass>();
                Model.SystemStructure.SysPermissionClass classModel;
                while (dr.Read())
                {
                    classModel = new Model.SystemStructure.SysPermissionClass();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        classModel.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CategoryId")))
                        classModel.CategoryId = dr.GetInt32(dr.GetOrdinal("CategoryId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SortId")))
                        classModel.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClassName")))
                        classModel.ClassName = dr.GetString(dr.GetOrdinal("ClassName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")))
                    {
                        if (dr.GetString(dr.GetOrdinal("IsEnable")) == "1" || dr.GetString(dr.GetOrdinal("IsEnable")).ToLower() == "ture")
                            classModel.IsEnable = true;
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("PermissionList")))
                    {
                        var xRoot = XElement.Parse(dr.GetString(dr.GetOrdinal("PermissionList")));
                        var xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            classModel.SysPermission = new List<Model.SystemStructure.SysPermission>();
                            foreach (var t in xRows)
                            {
                                if (t == null)
                                    continue;

                                var tmp = new Model.SystemStructure.SysPermission
                                              {
                                                  CategoryId = model.Id,
                                                  ClassId = classModel.Id,
                                                  Id = Common.Utility.GetInt(Common.Utility.GetXAttributeValue(t, "Id")),
                                                  PermissionName =
                                                      Common.Utility.GetXAttributeValue(t, "PermissionName"),
                                                  SortId =
                                                      Common.Utility.GetInt(Common.Utility.GetXAttributeValue(t,
                                                                                                              "SortId"))
                                              };
                                string strt = Common.Utility.GetXAttributeValue(t, "IsEnable");
                                if (!string.IsNullOrEmpty(strt) && (strt == "1" || strt.ToLower() == "true"))
                                    tmp.IsEnable = true;
                                classModel.SysPermission.Add(tmp);
                            }
                        }
                    }

                    model.SysPermissionClass.Add(classModel);
                }
            }

            return model;
        }

        #endregion
    }
}
