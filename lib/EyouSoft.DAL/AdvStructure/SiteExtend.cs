using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.AdvStructure
{
    /// <summary>
    /// 描述：分站旅行社推广数据类
    /// 修改记录:
    /// 1. 2011-05-09 AM 曹胡生 创建
    /// </summary>
    public class SiteExtend : DALBase, EyouSoft.IDAL.AdvStructure.ISiteExtend
    {
        #region static constants
        //删除分站旅行社推广记录
        private const string SQL_DELETE_SiteExtend = "DELETE FROM [tbl_FenSiteExtend] WHERE ID={0}";
        //添加分站旅行社推广记录
        private const string SQL_ADD_SiteExtend = "INSERT INTO [tbl_FenSiteExtend](SortID,CompanyID,IsBold,Color,IsShow,ShowCityID,ShowProID) VALUES(@SortID,@CompanyID,@IsBold,@Color,@IsShow,@ShowCityID,@ShowProID)";
        //修改分站旅行社推广记录
        private const string SQL_UPDATE_SiteExtend = "UPDATE [tbl_FenSiteExtend] SET SortID=@SortID,IsBold=@IsBold,Color=@Color,IsShow=@IsShow,ShowCityID=@ShowCityID,ShowProID=@ShowProID WHERE ID=@ID";
        //判断该公司在该城市下是否已推荐
        private const string SQL_SELECT_IsExist = "SELECT COUNT(*) FROM [tbl_FenSiteExtend] WHERE CompanyID='{0}' AND ShowCityID='{1}'";
        //得到所有分站
        private const string SQL_SELECT_AllExtendSite = "SELECT ID,CityName FROM [tbl_SysCity] WHERE IsExtendSite=1";
        //得到所有已审核的公司
        private const string SQL_SELECT_ALLVerifyCompany = "SELECT A.Id,A.IssueTime,A.CompanyName,A.ContactName,A.CityId,(SELECT COUNT(*) FROM [tbl_CompanyPayService] WHERE CompanyId=A.Id AND IsEnabled=1) AS PayItemsCount,(SELECT CityName FROM [tbl_SysCity] WHERE Id=A.CityId) AS CityName FROM [tbl_CompanyInfo] A";
        //判断该城市是否是二类分站
        private const string SQL_SELECT_IsExtendSite = "SELECT IsExtendSite FROM tbl_SysCity WHERE ID={0}";
        #endregion

        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database DB;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SiteExtend()
        {
            this.DB = base.SystemStore;
        }
        #endregion

        /// <summary>
        /// 描述：获得分站旅行社推广列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> GetSiteExtendList(int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> items = new List<EyouSoft.Model.AdvStructure.SiteExtendInfo>();
            EyouSoft.Model.AdvStructure.SiteExtendInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_FenSiteExtend";
            string primaryKey = "Id";
            string orderByString = "SortId ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" *,(SELECT COUNT(*) FROM [tbl_TourList] WHERE CompanyID=tbl_FenSiteExtend.CompanyID) as ProductNum,(SELECT CompanyName FROM [tbl_CompanyInfo] WHERE ID=tbl_FenSiteExtend.CompanyID) as CompanyName,(SELECT CityName FROM [tbl_SysCity] WHERE Id=tbl_FenSiteExtend.ShowCityID) as CityName,(SELECT LoginCount FROM [tbl_CompanyInfo] WHERE Id=tbl_FenSiteExtend.CompanyID) as LoginCount ");
            #endregion
            #region 拼接查询条件
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this.DB, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.AdvStructure.SiteExtendInfo()
                    {
                        ID = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        SortID = rdr.GetInt32(rdr.GetOrdinal("SortID")),
                        CompanyID = rdr["CompanyID"].ToString(),
                        IsBold = rdr.IsDBNull(rdr.GetOrdinal("IsBold")) ? false : rdr.GetString(rdr.GetOrdinal("IsBold")) == "0" ? false : true,
                        Color = rdr["Color"].ToString(),
                        IsShow = rdr.IsDBNull(rdr.GetOrdinal("IsShow")) ? false : rdr.GetString(rdr.GetOrdinal("IsShow")) == "0" ? false : true,
                        ShowCity = rdr["CityName"].ToString(),
                        ShowCityID = rdr.IsDBNull(rdr.GetOrdinal("ShowCityID")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowCityID")),
                        ShowProID = rdr.IsDBNull(rdr.GetOrdinal("ShowProID")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowProID")),
                        CompanyName = rdr["CompanyName"].ToString(),
                        ProductNum = rdr.IsDBNull(rdr.GetOrdinal("ProductNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ProductNum")),
                        LoginCount = rdr.IsDBNull(rdr.GetOrdinal("LoginCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LoginCount"))
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 描述：根据城市编号获得分站旅行社推广列表
        /// </summary>
        /// <param name="CityId">城市编号</param>
        /// <param name="IsShow">显示状态,为NULL显示全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> GetSiteExtendList(int CityId, bool? IsShow)
        {
            DbCommand dc = this.DB.GetStoredProcCommand("proc_SiteExtendList");
            if (IsShow.HasValue)
            {
                if (IsShow == true)
                {
                    this.DB.AddInParameter(dc, "IsShow", DbType.AnsiStringFixedLength, "1");
                }
                else
                {
                    this.DB.AddInParameter(dc, "IsShow", DbType.AnsiStringFixedLength, "0");
                }
            }
            else
            {
                this.DB.AddInParameter(dc, "IsShow", DbType.AnsiStringFixedLength, null);
            }
            this.DB.AddInParameter(dc, "CityId", DbType.Int32, CityId);
            IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> items = new List<EyouSoft.Model.AdvStructure.SiteExtendInfo>();
            EyouSoft.Model.AdvStructure.SiteExtendInfo item = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, DB))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.AdvStructure.SiteExtendInfo()
                    {
                        ID = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        SortID = rdr.GetInt32(rdr.GetOrdinal("SortID")),
                        CompanyID = rdr["CompanyID"].ToString(),
                        IsBold = rdr.IsDBNull(rdr.GetOrdinal("IsBold")) ? false : rdr.GetString(rdr.GetOrdinal("IsBold")) == "0" ? false : true,
                        Color = rdr["Color"].ToString(),
                        IsShow = rdr.IsDBNull(rdr.GetOrdinal("IsShow")) ? false : rdr.GetString(rdr.GetOrdinal("IsShow")) == "0" ? false : true,
                        ShowCity = rdr["CityName"].ToString(),
                        ShowCityID = rdr.IsDBNull(rdr.GetOrdinal("ShowCityID")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowCityID")),
                        ShowProID = rdr.IsDBNull(rdr.GetOrdinal("ShowProID")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowProID")),
                        CompanyName = rdr["CompanyName"].ToString(),
                        ProductNum = rdr.IsDBNull(rdr.GetOrdinal("ProductNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ProductNum")),
                        LoginCount = rdr.IsDBNull(rdr.GetOrdinal("LoginCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LoginCount"))
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 描述：修改分站旅行社推广记录
        /// </summary>
        /// <param name="SiteExtendInfo"></param>
        /// <returns></returns>
        public bool UpdateSiteExtend(EyouSoft.Model.AdvStructure.SiteExtendInfo SiteExtendInfo)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(SQL_UPDATE_SiteExtend);
            this.DB.AddInParameter(dc, "SortID", DbType.Int32, SiteExtendInfo.SortID);
            this.DB.AddInParameter(dc, "IsBold", DbType.AnsiStringFixedLength, SiteExtendInfo.IsBold ? "1" : "0");
            this.DB.AddInParameter(dc, "Color", DbType.String, SiteExtendInfo.Color);
            this.DB.AddInParameter(dc, "IsShow", DbType.AnsiStringFixedLength, SiteExtendInfo.IsShow ? "1" : "0");
            this.DB.AddInParameter(dc, "ID", DbType.Int32, SiteExtendInfo.ID);
            this.DB.AddInParameter(dc, "ShowCity", DbType.String, SiteExtendInfo.ShowCity);
            this.DB.AddInParameter(dc, "ShowCityID", DbType.Int32, SiteExtendInfo.ShowCityID);
            this.DB.AddInParameter(dc, "ShowProID", DbType.Int32, SiteExtendInfo.ShowProID);
            return DbHelper.ExecuteSql(dc, DB) > 0 ? true : false;
        }

        /// <summary>
        /// 描述：添加分站旅行社推广记录
        /// </summary>
        /// <param name="SiteExtendInfo"></param>
        /// <returns></returns>
        public bool AddSiteExtend(EyouSoft.Model.AdvStructure.SiteExtendInfo SiteExtendInfo)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(SQL_ADD_SiteExtend);
            this.DB.AddInParameter(dc, "SortID", DbType.Int32, SiteExtendInfo.SortID);
            this.DB.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, SiteExtendInfo.CompanyID);
            this.DB.AddInParameter(dc, "IsBold", DbType.AnsiStringFixedLength, SiteExtendInfo.IsBold ? "1" : "0");
            this.DB.AddInParameter(dc, "Color", DbType.String, SiteExtendInfo.Color);
            this.DB.AddInParameter(dc, "IsShow", DbType.AnsiStringFixedLength, SiteExtendInfo.IsShow ? "1" : "0");
            this.DB.AddInParameter(dc, "ShowCity", DbType.String, SiteExtendInfo.ShowCity);
            this.DB.AddInParameter(dc, "ShowCityID", DbType.Int32, SiteExtendInfo.ShowCityID);
            this.DB.AddInParameter(dc, "ShowProID", DbType.Int32, SiteExtendInfo.ShowProID);
            return DbHelper.ExecuteSql(dc, DB) > 0 ? true : false;
        }

        /// <summary>
        /// 描述：删除分站旅行社推广记录
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public bool DelSiteExtend(int ID)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(string.Format(SQL_DELETE_SiteExtend, ID));
            return DbHelper.ExecuteSql(dc, DB) > 0 ? true : false;
        }

        /// <summary>
        /// 描述：判断该公司在该城市下是否已推荐
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public bool IsZixst(int CityId, string CompanyID)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(string.Format(SQL_SELECT_IsExist, CompanyID, CityId));
            return DbHelper.Exists(dc, DB);
        }

        /// <summary>
        /// 描述，得到所有分站
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.SiteExtend> GetALLExtendSite()
        {
            DbCommand dc = this.DB.GetSqlStringCommand("SQL_SELECT_AllExtendSite");
            IList<EyouSoft.Model.AdvStructure.SiteExtend> items = new List<EyouSoft.Model.AdvStructure.SiteExtend>();
            EyouSoft.Model.AdvStructure.SiteExtend item = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, DB))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.AdvStructure.SiteExtend()
                    {
                        ID = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        CityName = rdr["CityName"].ToString()
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 描述：得到所有已审核的公司
        /// </summary>
        /// <param name="CityId">城市编号，0为所有城市</param>
        /// <param name="IsPay"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.CompanySelectInfo> GetALLVerifyCompany(int pageSize, int pageIndex, ref int recordCount, int CityId, bool IsPay)
        {
            IList<EyouSoft.Model.AdvStructure.CompanySelectInfo> items = new List<EyouSoft.Model.AdvStructure.CompanySelectInfo>();
            EyouSoft.Model.AdvStructure.CompanySelectInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_CompanyInfo";
            string primaryKey = "Id";
            string orderByString = "Id DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("  Id,IssueTime,CompanyName,ContactName,CityId,(SELECT COUNT(*) FROM [tbl_CompanyPayService] WHERE CompanyId=tbl_CompanyInfo.Id AND IsEnabled=1) AS PayItemsCount,(SELECT CityName FROM [tbl_SysCity] WHERE Id=tbl_CompanyInfo.CityId) AS CityName ");
            #endregion
            #region 拼接查询条件
            cmdQuery.Append(" IsCheck=1 ");
            if (IsPay)
            {
                cmdQuery.Append(" AND Exists(SELECT CompanyId FROM [tbl_CompanyPayService] WHERE CompanyId=tbl_CompanyInfo.Id AND IsEnabled=1)");
            }
            if (CityId != 0)
            {
                cmdQuery.AppendFormat(" AND CityId={0}", CityId);
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this.DB, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.AdvStructure.CompanySelectInfo()
                    {
                        ID = rdr["Id"].ToString(),
                        AddDate = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? System.DateTime.Now : rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        CityID = rdr.IsDBNull(rdr.GetOrdinal("CityId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("CityId")),
                        CityName = rdr["CityName"].ToString(),
                        CompanyName = rdr["CompanyName"].ToString(),
                        ContactName = rdr["ContactName"].ToString(),
                        IsPay = rdr.IsDBNull(rdr.GetOrdinal("PayItemsCount")) ? false : rdr.GetInt32(rdr.GetOrdinal("PayItemsCount")) > 0 ? true : false
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 当前城市是否是二类分站
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public bool IsExtendSite(int CityId)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(string.Format(SQL_SELECT_IsExtendSite, CityId));
            return DbHelper.Exists(dc, DB);
        }
    }
}
