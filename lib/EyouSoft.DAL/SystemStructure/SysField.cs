using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 系统类型定义数据访问
    /// </summary>
    /// 周文超 2010-06-22
    public class SysField : DALBase, IDAL.SystemStructure.ISysField
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysField() { }

        private const string Sql_SysField_Select = " SELECT [FieldName],[FieldID],[FieldType],[IsEnabled],[IsDefault] FROM [tbl_SysField] ";
        private const string Sql_SysField_Add = " declare @FieldId int;SELECT @FieldId = max(FieldId)+1 FROM [tbl_SysField] WHERE [FieldType]=@FieldType;IF(@FieldId IS NULL)SET @FieldId=1;if not exists (select 1 from tbl_SysField where FieldName = @FieldName AND FieldType = @FieldType) INSERT INTO [tbl_SysField]([FieldName],[FieldID],[FieldType],[IsEnabled],[IsDefault]) VALUES (@FieldName,@FieldId,@FieldType,@IsEnabled,@IsDefault) ";
        private const string Sql_SysField_Update = " if not exists (select 1 from tbl_SysField where FieldName = @FieldName AND FieldType = @FieldType AND FieldID<>@FieldID) update [tbl_SysField] set [FieldName] = @FieldName where [FieldID] = @FieldID and FieldType = @FieldType ";
        private const string Sql_SysField_Delete = "Delete tbl_SysField where FieldID in(@fields) and FieldType=@FieldType";
        #region ISysField 成员

        /// <summary>
        /// 获取系统类型定义实体集合
        /// </summary>
        /// <param name="FieldType">类型的类型(必须传值)</param>
        /// <param name="IsEnabled">是否启用(为null不作条件)</param>
        /// <param name="IsDefault">是否系统预定义类型(为null不作条件)</param>
        /// <returns>系统类型定义实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysFieldBase> GetSysFieldBaseList(
            EyouSoft.Model.SystemStructure.SysFieldType FieldType, bool? IsEnabled, bool? IsDefault)
        {
            IList<EyouSoft.Model.SystemStructure.SysFieldBase> List = new List<EyouSoft.Model.SystemStructure.SysFieldBase>();

            string strWhere = Sql_SysField_Select + string.Format(" where FieldType = {0} ", (int)FieldType);
            if (IsEnabled != null)
                strWhere += string.Format(" and IsEnabled = '{0}' ", (bool)IsEnabled ? 1 : 0);
            if (IsDefault != null)
                strWhere += string.Format(" and IsDefault = '{0}' ", (bool)IsDefault ? 1 : 0);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.SystemStructure.SysFieldBase model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysFieldBase();
                    model.FieldName = dr["FieldName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("FieldID")))
                        model.FieldId = int.Parse(dr["FieldID"].ToString());
                    List.Add(model);
                    model = null;
                }
            }
            if (FieldType == EyouSoft.Model.SystemStructure.SysFieldType.客户等级)
            {
                EyouSoft.Model.SystemStructure.SysFieldBase model = new EyouSoft.Model.SystemStructure.SysFieldBase();
                List<EyouSoft.Model.SystemStructure.SysFieldBase> newList= List.Where(item => (item.FieldName == "单房差")).ToList();
                if (newList!=null&&newList.Count>0)
                {
                    model = newList[0];
                }
                newList = null;
                List.Remove(model);
                List.Add(model);
                model = null;
            }
            return List;
        }

        /// <summary>
        /// 获取系统类型定义实体集合
        /// </summary>
        /// <param name="FieldType">类型的类型(为null不作条件)</param>
        /// <param name="IsEnabled">是否启用(为null不作条件)</param>
        /// <param name="IsDefault">是否系统预定义类型(为null不作条件)</param>
        /// <returns>系统类型定义实体集合</returns>
        public virtual IList<Model.SystemStructure.SysField> GetSysFieldList(Model.SystemStructure.SysFieldType? FieldType, bool? IsEnabled
            , bool? IsDefault)
        {
            IList<EyouSoft.Model.SystemStructure.SysField> List = new List<EyouSoft.Model.SystemStructure.SysField>();

            string strWhere = Sql_SysField_Select + " where 1 = 1 ";
            if (FieldType != null)
                strWhere += string.Format(" and FieldType = {0} ", (int)FieldType);
            if (IsEnabled != null)
                strWhere += string.Format(" and IsEnabled = '{0}' ", (bool)IsEnabled ? 1 : 0);
            if (IsDefault != null)
                strWhere += string.Format(" and IsDefault = '{0}' ", (bool)IsDefault ? 1 : 0);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.SystemStructure.SysField model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysField();

                    model.FieldName = dr["FieldName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("FieldID")))
                        model.FieldId = int.Parse(dr["FieldID"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnabled")) && dr["IsEnabled"].ToString() == "1")
                        model.IsEnabled = true;
                    else
                        model.IsEnabled = false;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDefault")) && dr["IsDefault"].ToString() == "1")
                        model.IsDefault = true;
                    else
                        model.IsDefault = false;
                    if (!dr.IsDBNull(dr.GetOrdinal("FieldType")))
                        model.FieldType = (Model.SystemStructure.SysFieldType)int.Parse(dr["FieldType"].ToString());

                    List.Add(model);
                }
            }
            return List;
        }

        /// <summary>
        /// 新增系统类型定义
        /// </summary>
        /// <param name="model">系统类型定义实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddSysField(EyouSoft.Model.SystemStructure.SysField model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysField_Add);
            base.SystemStore.AddInParameter(dc, "FieldName", DbType.String, model.FieldName);
            base.SystemStore.AddInParameter(dc, "FieldType", DbType.Byte, (int)model.FieldType);
            base.SystemStore.AddInParameter(dc, "IsEnabled", DbType.AnsiStringFixedLength, model.IsEnabled ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "IsDefault", DbType.AnsiStringFixedLength, model.IsDefault ? "1" : "0");

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 修改系统类型定义(只修改名称)
        /// </summary>
        /// <param name="FieldName">系统类型定义名称</param>
        /// <param name="FieldId">系统类型定义ID</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateSysField(string FieldName, int FieldId, Model.SystemStructure.SysFieldType FieldType)
        {
            if (string.IsNullOrEmpty(FieldName))
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysField_Update);
            base.SystemStore.AddInParameter(dc, "FieldName", DbType.String, FieldName);
            base.SystemStore.AddInParameter(dc, "FieldID", DbType.Int32, FieldId);
            base.SystemStore.AddInParameter(dc, "FieldType", DbType.Byte, (int)FieldType);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="FieldIds">系统类型定义ID集合</param>
        /// <param name="FieldType">系统类型定义类型</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsEnabled(string FieldIds, EyouSoft.Model.SystemStructure.SysFieldType FieldType, bool IsEnabled)
        {
            if (string.IsNullOrEmpty(FieldIds))
                return false;
            string strWhere = " update [tbl_SysField] set [IsEnabled] = '{0}' where [FieldID] in ({1}) and FieldType = {2} ";
            strWhere = string.Format(strWhere, IsEnabled ? "1" : "0", FieldIds, (int)FieldType);
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 真实删除客户等级
        /// </summary>
        /// <param name="FieldIds">客户等级编号字符串</param>
        /// <param name="FieldType">客户等级类型</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string FieldIds, Model.SystemStructure.SysFieldType FieldType)
        {
            StringBuilder strSql=new StringBuilder(Sql_SysField_Delete+";");
            if (FieldType == Model.SystemStructure.SysFieldType.线路主题)
            {
                strSql.Append("Delete tbl_TourThemeControl where ThemeId in(@fields) ;");
                strSql.Append("Delete tbl_RouteThemeControl where ThemeId in(@fields) ;");
            }
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strSql.ToString());
            this.SystemStore.AddInParameter(dc, "fields", DbType.String, FieldIds);
            this.SystemStore.AddInParameter(dc, "FieldType", DbType.Byte, (int)FieldType);
            return DbHelper.ExecuteSqlTrans(dc, this.SystemStore) > 0 ? true : false;
        }
        #endregion
    }
}
