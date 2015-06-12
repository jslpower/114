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
    /// 各平台公司对应关系数据访问
    /// </summary>
    /// 周文超 2011-04-02
    public class DCompany : DALBase, IDAL.OpenStructure.IDCompany
    {
        /// <summary>
        /// 数据库
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DCompany() { this._db = base.OpenStore; }

        #region Sql

        /// <summary>
        /// 无条件查询Sql
        /// </summary>
        private const string Sql_SystemOpenRelation_Select = " SELECT  [SystemCompanyId],[PlatformCompanyId],[SystemType],[SystemCompanyType] FROM [tbl_SystemOpenRelation] ";

        /// <summary>
        /// 写入Sql
        /// </summary>
        private const string Sql_SystemOpenRelation_Insert = @" INSERT INTO [tbl_SystemOpenRelation]
           ([SystemCompanyId]
           ,[PlatformCompanyId]
           ,[SystemType]
           ,[SystemCompanyType])
     VALUES
           (@SystemCompanyId
           ,@PlatformCompanyId
           ,@SystemType
           ,@SystemCompanyType); ";

        #endregion



        #region IMCompany 成员

        /// <summary>
        /// 添加各平台公司对应关系
        /// </summary>
        /// <param name="model">各平台公司对应关系实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public virtual int AddMCompany(Model.OpenStructure.MCompanyInfo model)
        {
            if (model == null)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(Sql_SystemOpenRelation_Insert);
            this._db.AddInParameter(dc, "SystemCompanyId", DbType.Int32, model.SystemCompanyId);
            this._db.AddInParameter(dc, "PlatformCompanyId", DbType.AnsiStringFixedLength, model.PlatformCompanyId);
            this._db.AddInParameter(dc, "SystemType", DbType.Byte, model.SystemType);
            this._db.AddInParameter(dc, "SystemCompanyType", DbType.Byte, model.SystemCompanyType);

            return DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        /*/// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="SystemCompanyId">系统公司编号，其它系统（非114平台）时赋值</param>
        /// <param name="SystemType">系统类型</param>
        /// <param name="PlatformCompanyId">平台公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OpenStructure.MCompanyInfo> GetCompanyList(int SystemCompanyId, int SystemType, string PlatformCompanyId)
        {
            IList<EyouSoft.Model.OpenStructure.MCompanyInfo> list = new List<EyouSoft.Model.OpenStructure.MCompanyInfo>();
            EyouSoft.Model.OpenStructure.MCompanyInfo model = null;
            string strSql = Sql_SystemOpenRelation_Select + " where 1 = 1 ";
            if (SystemCompanyId > 0)
                strSql += string.Format(" and SystemCompanyId = {0} ", SystemCompanyId);
            if (SystemType > 0)
                strSql += string.Format(" and SystemType = {0} ", SystemType);
            if(!string.IsNullOrEmpty(PlatformCompanyId))
                strSql += string.Format(" and PlatformCompanyId = '{0}' ", PlatformCompanyId);

            DbCommand dc = this._db.GetSqlStringCommand(strSql);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.OpenStructure.MCompanyInfo();
                    if (!dr.IsDBNull(0))
                        model.SystemCompanyId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.PlatformCompanyId = dr.GetString(1);
                    if (!dr.IsDBNull(2))
                        model.SystemType = dr.GetByte(2);
                    if (!dr.IsDBNull(3))
                        model.SystemCompanyType = dr.GetByte(3);

                    list.Add(model);
                }
            }

            return list;
        }*/

        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="systemCompanyId">系统公司编号</param>
        /// <param name="systemType">系统类型</param>
        /// <param name="platformCompanyId">平台公司编号</param>
        /// <param name="systemCompanyType">系统公司类型</param>
        /// <returns></returns>
        public virtual IList<Model.OpenStructure.MCompanyInfo> GetCompanyList(int systemCompanyId, int systemType, string platformCompanyId, int systemCompanyType)
        {
            IList<EyouSoft.Model.OpenStructure.MCompanyInfo> items = new List<EyouSoft.Model.OpenStructure.MCompanyInfo>();
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(Sql_SystemOpenRelation_Select);
            cmdText.Append(" WHERE 1=1 ");

            if (systemCompanyId > 0)
            {
                cmdText.AppendFormat(" AND SystemCompanyId = {0} ", systemCompanyId);
            }
            if (systemType > 0)
            {
                cmdText.AppendFormat(" AND SystemType={0} ", systemType);
            }
            if (!string.IsNullOrEmpty(platformCompanyId))
            {
                cmdText.AppendFormat(" AND PlatformCompanyId='{0}' ", platformCompanyId);
            }
            if (systemCompanyType > 0)
            {
                cmdText.AppendFormat(" AND SystemCompanyType={0} ", systemCompanyType);
            }

            DbCommand cmd = this._db.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    items.Add(new EyouSoft.Model.OpenStructure.MCompanyInfo()
                    {
                        PlatformCompanyId = rdr.GetString(rdr.GetOrdinal("PlatformCompanyId")),
                        SystemCompanyId = rdr.GetInt32(rdr.GetOrdinal("SystemCompanyId")),
                        SystemCompanyType = rdr.GetByte(rdr.GetOrdinal("SystemCompanyType")),
                        SystemType = rdr.GetByte(rdr.GetOrdinal("SystemType"))
                    });                 
                }
            }

            return items;
        }
        #endregion
    }
}
