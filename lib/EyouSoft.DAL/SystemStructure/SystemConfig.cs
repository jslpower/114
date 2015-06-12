using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 系统设置 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SystemConfig : DALBase, IDAL.SystemStructure.ISystemConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemConfig()
        { }

        private const string Sql_SystemConfig_Update = " UPDATE [tbl_SystemConfig] SET [TourViewType] = @TourViewType,[VisitViewType] = @VisitViewType WHERE [SystemId] = @SystemId ";
        private const string Sql_SystemConfig_Select = " SELECT top 1 [SystemId],[TourViewType],[VisitViewType] FROM [tbl_SystemConfig] ";

        #region 函数成员

        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="model">系统设置实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateSystemConfig(EyouSoft.Model.SystemStructure.SystemConfig model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SystemConfig_Update);
            base.SystemStore.AddInParameter(dc, "TourViewType", DbType.Byte, model.TourViewType);
            base.SystemStore.AddInParameter(dc, "VisitViewType", DbType.Byte, model.VisitViewType);
            base.SystemStore.AddInParameter(dc, "SystemId", DbType.Int32, model.SystemId);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 获取系统设置实体
        /// </summary>
        /// <returns>返回系统设置实体</returns>
        public virtual Model.SystemStructure.SystemConfig GetSystemConfig()
        {
            Model.SystemStructure.SystemConfig model = new EyouSoft.Model.SystemStructure.SystemConfig();

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SystemConfig_Select);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        model.SystemId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.TourViewType = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.VisitViewType = dr.GetInt32(2);
                }
            }
            return model;
        }

        #endregion
    }
}
