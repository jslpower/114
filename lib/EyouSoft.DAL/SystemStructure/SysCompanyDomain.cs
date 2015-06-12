using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL;
using EyouSoft.Model.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 客户域名 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SysCompanyDomain : DALBase, IDAL.SystemStructure.ISysCompanyDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCompanyDomain()
        { }

        #region  SqlString

        private const string Sql_SysCompanyDomain_Add = " INSERT INTO [tbl_SysCompanyDomain]([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime]) VALUES (@CompanyId,@CompanyType,@CompanyName,@Domain,@DomainType,@GoToUrl,@IssueTime) ";
        private const string Sql_SysCompanyDomain_Update = " UPDATE [tbl_SysCompanyDomain] SET [CompanyType] = @CompanyType,[CompanyName] = @CompanyName,[Domain] = @Domain,[DomainType] = @DomainType,[GoToUrl] = @GoToUrl WHERE ID = @ID ";
        private const string Sql_SysCompanyDomain_Del = " DELETE FROM [tbl_SysCompanyDomain] ";
        private const string Sql_SysCompanyDomain_Select = " SELECT [ID],[CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled] FROM [tbl_SysCompanyDomain] ";
        private const string Sql_SysCompanyDomain_SetDisabled = " UPDATE [tbl_SysCompanyDomain] SET [IsDisabled] = @IsDisabled WHERE ID = @ID ";
        private const string Sql_SysCompanyDomain_SetDisabledByDomainType = " UPDATE [tbl_SysCompanyDomain] SET [IsDisabled] = @IsDisabled WHERE CompanyId = @CompanyId AND [DomainType]=@DomainType ";
        private const string Sql_SysCompanyDomain_IsExist = " SELECT [ID] FROM [tbl_SysCompanyDomain] WHERE Domain = @Domain";
        private const string SQL_UPDATE_UpdateGotoUrl = "UPDATE tbl_SysCompanyDomain SET GoToUrl=@GotoUrl WHERE CompanyId=@CompanyId AND DomainType=@DomainType";
        #endregion

        #region 函数成员
        /// <summary>
        /// 域名是否存在
        /// </summary>
        /// <param name="DomainName">域名</param>
        /// <returns></returns>
        public virtual bool IsExist(string DomainName)
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCompanyDomain_Add);

            base.SystemStore.AddInParameter(dc, "Domain", DbType.String, DomainName);

            return DbHelper.Exists(dc, base.SystemStore);
        }

        /// <summary>
        /// 新增客户域名
        /// </summary>
        /// <param name="model">客户域名实体</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int AddSysCompanyDomain(EyouSoft.Model.SystemStructure.SysCompanyDomain model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCompanyDomain_Add);

            base.SystemStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            base.SystemStore.AddInParameter(dc, "CompanyType", DbType.Byte, (int)model.CompanyType);
            base.SystemStore.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            base.SystemStore.AddInParameter(dc, "Domain", DbType.String, model.Domain);
            base.SystemStore.AddInParameter(dc, "DomainType", DbType.Byte, (int)model.DomainType);
            base.SystemStore.AddInParameter(dc, "GoToUrl", DbType.String, model.GoToUrl);
            base.SystemStore.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 修改客户域名(只更新公司类型，公司名称，域名，跳转地址)
        /// </summary>
        /// <param name="model">客户域名实体</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int UpdateSysCompanyDomain(EyouSoft.Model.SystemStructure.SysCompanyDomain model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCompanyDomain_Update);
            base.SystemStore.AddInParameter(dc, "CompanyType", DbType.Byte, (int)model.CompanyType);
            base.SystemStore.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            base.SystemStore.AddInParameter(dc, "Domain", DbType.String, model.Domain);
            base.SystemStore.AddInParameter(dc, "DomainType", DbType.Byte, (int)model.DomainType);
            base.SystemStore.AddInParameter(dc, "GoToUrl", DbType.String, model.GoToUrl);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 设置域名状态
        /// </summary>
        /// <param name="DomainId">域名编号</param>
        /// <param name="IsDisabled">状态</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int SetDisabled(int DomainId, bool IsDisabled)
        {
            if (DomainId < 0)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCompanyDomain_SetDisabled);
            base.SystemStore.AddInParameter(dc, "IsDisabled", DbType.String, IsDisabled ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, DomainId);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 设置域名状态
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="domainType">域名类型</param>
        /// <param name="IsDisabled">状态</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int SetDisabled(string CompanyId, DomainType domainType, bool IsDisabled)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCompanyDomain_SetDisabledByDomainType);
            base.SystemStore.AddInParameter(dc, "IsDisabled", DbType.String, IsDisabled ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "DomainType", DbType.Int32, (int)domainType);
            base.SystemStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除客户域名
        /// </summary>
        /// <param name="SysCompanyDomainId">客户域名ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysCompanyDomain(int SysCompanyDomainId)
        {
            if (SysCompanyDomainId <= 0)
                return false;

            string strWhere = Sql_SysCompanyDomain_Del + " where ID = @ID ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, SysCompanyDomainId);
            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据公司ID删除客户域名
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysCompanyDomain(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return false;

            string strWhere = Sql_SysCompanyDomain_Del + " where [CompanyId] = @CompanyId ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据ID获取客户域名信息
        /// </summary>
        /// <param name="SysCompanyDomainId">客户域名ID</param>
        /// <returns>返回客户域名实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SysCompanyDomain GetSysCompanyDomain(int SysCompanyDomainId)
        {
            if (SysCompanyDomainId <= 0)
                return null;

            Model.SystemStructure.SysCompanyDomain model = new EyouSoft.Model.SystemStructure.SysCompanyDomain();

            string strWhere = Sql_SysCompanyDomain_Select + " where ID = @ID ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, SysCompanyDomainId);

            DataTable dt = DbHelper.DataTableQuery(dc, base.SystemStore);
            if (dt != null && dt.Rows.Count > 0)
            {
                GetModelByDataRow(dt.Rows[0], ref model);
            }
            if (dt != null) dt.Dispose();
            dt = null;

            return model;
        }

        /// <summary>
        /// 根据域名获取客户域名信息
        /// </summary>
        /// <param name="domainType">域名类型</param>
        /// <param name="DomainName">域名</param>
        /// <returns>返回客户域名实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SysCompanyDomain GetSysCompanyDomain(DomainType domainType, string DomainName)
        {
            if (string.IsNullOrEmpty(DomainName))
                return null;

            Model.SystemStructure.SysCompanyDomain model = new EyouSoft.Model.SystemStructure.SysCompanyDomain();

            string strWhere = Sql_SysCompanyDomain_Select + " where Domain = @Domain AND DomainType=@DomainType ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "Domain", DbType.String, DomainName);
            base.SystemStore.AddInParameter(dc, "DomainType", DbType.Byte, (int)domainType);

            DataTable dt = DbHelper.DataTableQuery(dc, base.SystemStore);
            if (dt != null && dt.Rows.Count > 0)
            {
                GetModelByDataRow(dt.Rows[0], ref model);
            }
            if (dt != null) dt.Dispose();
            dt = null;

            return model;
        }

        /// <summary>
        /// 分页获取客户域名信息集合
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0为时间升序，1为时间降序</param>
        /// <returns>返回客户域名实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetDomainList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex)
        {
            IList<Model.SystemStructure.SysCompanyDomain> List = new List<Model.SystemStructure.SysCompanyDomain>();

            string strWhere = string.Empty;
            string strFiles = " [ID],[CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled] ";
            string strOrder = string.Empty;

            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_SysCompanyDomain", "[ID]", strFiles, strWhere, strOrder))
            {
                Model.SystemStructure.SysCompanyDomain model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysCompanyDomain();
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    model.CompanyId = dr["CompanyId"].ToString();
                    if (!string.IsNullOrEmpty(dr["CompanyType"].ToString()))
                        model.CompanyType = (Model.CompanyStructure.CompanyType)int.Parse(dr["CompanyType"].ToString());
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.Domain = dr["Domain"].ToString();
                    if (!string.IsNullOrEmpty(dr["DomainType"].ToString()))
                        model.DomainType = (Model.SystemStructure.DomainType)int.Parse(dr["DomainType"].ToString());
                    model.GoToUrl = dr["GoToUrl"].ToString();
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["IsDisabled"].ToString()))
                        model.IsDisabled = bool.Parse(dr["IsDisabled"].ToString());

                    List.Add(model);
                }

                model = null;
            }

            return List;
        }

        /// <summary>
        /// 根据公司ID获取客户域名实体集合
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>客户域名实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetDomainList(string CompanyId)
        {           
            string strWhere = "";
            if(!string.IsNullOrEmpty(CompanyId))
            {
                strWhere = Sql_SysCompanyDomain_Select + string.Format(" where [CompanyId] IN ({0}) ",CompanyId);
            }
            else
            {
                strWhere = Sql_SysCompanyDomain_Select;
            };
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            return GetQueryList(dc);
        }

        /// <summary>
        /// 更新域名跳转URL
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="domainType">域名类型</param>
        /// <param name="gotoUrl">跳转URL</param>
        /// <returns></returns>
        public virtual bool UpdateGotoUrl(string companyId, DomainType domainType, string gotoUrl)
        {
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_UPDATE_UpdateGotoUrl);
            base.SystemStore.AddInParameter(cmd, "GotoUrl", DbType.String, gotoUrl);
            base.SystemStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            base.SystemStore.AddInParameter(cmd, "DomainType", DbType.Byte, domainType);

            DbHelper.ExecuteSql(cmd, base.SystemStore);

            return true;
        }

        #endregion

        #region 私有函数  根据数据行生成实体

        /// <summary>
        /// 根据数据行生成实体
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="model">返回参数，客户域名实体</param>
        private void GetModelByDataRow(DataRow dr, ref Model.SystemStructure.SysCompanyDomain model)
        {
            if (dr != null)
            {
                model = new EyouSoft.Model.SystemStructure.SysCompanyDomain();
                if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                    model.ID = int.Parse(dr["ID"].ToString());
                model.CompanyId = dr["CompanyId"].ToString();
                if (!string.IsNullOrEmpty(dr["CompanyType"].ToString()))
                    model.CompanyType = (Model.CompanyStructure.CompanyType)int.Parse(dr["CompanyType"].ToString());
                model.CompanyName = dr["CompanyName"].ToString();
                model.Domain = dr["Domain"].ToString();
                if (!string.IsNullOrEmpty(dr["DomainType"].ToString()))
                    model.DomainType = (Model.SystemStructure.DomainType)int.Parse(dr["DomainType"].ToString());
                model.GoToUrl = dr["GoToUrl"].ToString();
                if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                    model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                if (!string.IsNullOrEmpty(dr["IsDisabled"].ToString()))
                    model.IsDisabled = dr["IsDisabled"].ToString() == "1" ? true : false;
                    
            }
            dr = null;
        }

        /// <summary>
        /// 将查询数据集转化为实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>实体集合</returns>
        private IList<Model.SystemStructure.SysCompanyDomain> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> list = new List<EyouSoft.Model.SystemStructure.SysCompanyDomain>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.SystemStructure.SysCompanyDomain model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysCompanyDomain();
                    if (!dr.IsDBNull(0))
                        model.ID = dr.GetInt32(0);
                    model.CompanyId = dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.CompanyType = (Model.CompanyStructure.CompanyType)dr.GetByte(2);
                    model.CompanyName = dr[3].ToString();
                    model.Domain = dr[4].ToString();
                    if (!dr.IsDBNull(5))
                        model.DomainType = (Model.SystemStructure.DomainType)dr.GetByte(5);
                    model.GoToUrl = dr[6].ToString();
                    if (!dr.IsDBNull(7))
                        model.IssueTime = dr.GetDateTime(7);
                    if (!dr.IsDBNull(8))
                        model.IsDisabled = dr.GetString(8) == "1" ? true : false; ;
                    list.Add(model);
                }

                model = null;
            }
            return list;
        }

        #endregion
    }
}
