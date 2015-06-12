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

namespace EyouSoft.DAL.OpenStructure
{
    /// <summary>
    /// 各平台用户对应关系数据访问
    /// </summary>
    /// 周文超 2011-04-02
    public class DUser : DALBase, IDAL.OpenStructure.IDUser
    {
        /// <summary>
        /// 数据库
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DUser() { this._db = base.OpenStore; }

        #region Sql

        /// <summary>
        /// 无条件查询sql
        /// </summary>
        private const string Sql_UserOpenRelation_Select = @" SELECT 
        [SystemUserId]
      ,[PlatformUserId]
      ,[SystemType]
      ,[PlatformCompanyId]
  FROM [tbl_CompanyUserOpenRelation] ";

        /// <summary>
        /// 写入Sql
        /// </summary>
        private const string Sql_UserOpenRelation_Insert = @" INSERT INTO [tbl_CompanyUserOpenRelation]
           ([SystemUserId]
           ,[PlatformUserId]
           ,[SystemType]
           ,[PlatformCompanyId])
     VALUES
           (@SystemUserId
           ,@PlatformUserId
           ,@SystemType
           ,@PlatformCompanyId); ";

        #endregion

        #region IMUser 成员

        /// <summary>
        /// 添加各平台用户对应关系
        /// </summary>
        /// <param name="model">各平台用户对应关系实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public virtual int AddMUser(Model.OpenStructure.MUserInfo model)
        {
            if (model == null)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(Sql_UserOpenRelation_Insert);
            this._db.AddInParameter(dc, "SystemUserId", DbType.Int32, model.SystemUserId);
            this._db.AddInParameter(dc, "PlatformUserId", DbType.AnsiStringFixedLength, model.PlatformUserId);
            this._db.AddInParameter(dc, "SystemType", DbType.Byte, model.SystemType);
            this._db.AddInParameter(dc, "PlatformCompanyId", DbType.AnsiStringFixedLength, model.PlatformCompanyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="SystemUserId">系统用户编号，其它系统（非114平台）时赋值</param>
        /// <param name="SystemType">系统类型</param>
        /// <param name="PlatformUserId">平台用户编号，大平台（114平台）时赋值</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OpenStructure.MUserInfo> GetMUserList(int SystemUserId, int SystemType, string PlatformUserId)
        {
            IList<EyouSoft.Model.OpenStructure.MUserInfo> list = new List<EyouSoft.Model.OpenStructure.MUserInfo>();
            EyouSoft.Model.OpenStructure.MUserInfo model = null;
            string strSql = Sql_UserOpenRelation_Select + " where 1 = 1 ";
            if (SystemUserId > 0)
                strSql += string.Format(" and SystemUserId = {0} ", SystemUserId);
            if (SystemType > 0)
                strSql += string.Format(" and SystemType = {0} ", SystemType);
            if (!string.IsNullOrEmpty(PlatformUserId))
                strSql += string.Format(" and PlatformUserId = '{0}' ", PlatformUserId);

            DbCommand dc = this._db.GetSqlStringCommand(strSql);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.OpenStructure.MUserInfo();
                    if (!dr.IsDBNull(0))
                        model.SystemUserId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.PlatformUserId = dr.GetString(1);
                    if (!dr.IsDBNull(2))
                        model.SystemType = dr.GetByte(2);
                    if (!dr.IsDBNull(3))
                        model.PlatformCompanyId = dr.GetString(3);

                    list.Add(model);
                }
            }

            return list;
        }

        #endregion
    }
}
