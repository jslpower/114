using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 描述:单位经营城市[包括出港城市,销售城市]数据访问层
    /// 创建人：张志瑜 2010-07-08
    /// </summary>
    public class CompanyCity : DALBase, EyouSoft.IDAL.CompanyStructure.ICompanyCity
    {
        /// <summary>
        /// 查询公司所拥有的销售城市列表ID
        /// </summary>
        private const string SQL_CompanyCityControl_SELECT = "SELECT CityId FROM tbl_CompanyCityControl WHERE CompanyId=@CompanyId";
        //SELECT control.ProvinceId,control.CityId,city.CityName FROM tbl_CompanyCityControl AS control,tbl_SysCity AS city WHERE CompanyId=@CompanyId AND control.CityId=city.ID
        /// <summary>
        /// 查询公司所拥有的出港城市列表
        /// </summary>
        //private const string SQL_CompanySiteControl_SELECT = "SELECT site.CityId,city.ProvinceId,city.CityName FROM tbl_CompanySiteControl AS site,tbl_SysCity AS city WHERE site.CompanyId=@CompanyId AND site.CityId=city.ID";
        private const string SQL_CompanySiteControl_SELECT = "SELECT CityId FROM tbl_CompanySiteControl WHERE CompanyId=@CompanyId";

        /// <summary>
        /// 所在数据库
        /// </summary>
        protected Database _database;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyCity() 
        {
            this._database = base.CompanyStore;
        }
        /// <summary>
        /// 获得公司所拥有的销售城市列表[只包含城市ID]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanySaleCity(string companyId)
        {
            IList<EyouSoft.Model.SystemStructure.CityBase> items = new List<EyouSoft.Model.SystemStructure.CityBase>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyCityControl_SELECT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using(IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SystemStructure.CityBase item = new EyouSoft.Model.SystemStructure.CityBase();  
                    item.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    //item.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    items.Add(item);
                }
            }
            return items;
        }
        /// <summary>
        /// 获得公司常用的出港城市列表[只包含城市ID]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanyPortCity(string companyId)
        {
            IList<EyouSoft.Model.SystemStructure.CityBase> items = new List<EyouSoft.Model.SystemStructure.CityBase>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanySiteControl_SELECT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SystemStructure.CityBase item = new EyouSoft.Model.SystemStructure.CityBase();
                    item.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    //item.CityName = rdr.GetString(rdr.GetOrdinal("CityName"));
                    //item.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    items.Add(item);
                }
            }
            return items;
        }
        /// <summary>
        /// 设置公司常用的出港城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="items">常用的出港城市列表</param>
        /// <returns></returns>
        public virtual bool SetCompanyPortCity(string companyId, IList<EyouSoft.Model.SystemStructure.CityBase> items)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanySiteControl_Update");
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            StringBuilder XMLRoot = new StringBuilder();
            XMLRoot.Append("<ROOT>");
            if (items != null && items.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.CityBase item in items)
                {
                    XMLRoot.AppendFormat("<CompanySiteControl CityId='{0}' />", item.CityId);
                }
            }
            XMLRoot.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            this._database.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);

            //执行存储过程
            DbHelper.RunProcedure(dc, this._database);

            int rows = 0;
            object obj = this._database.GetParameterValue(dc, "ReturnValue");
            if (obj != null)
                rows = Convert.ToInt32(obj);
            if (rows > 0)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// 描述:单位线路区域数据访问层
    /// 创建人：张志瑜 2010-07-08
    /// </summary>
    public class CompanyArea : DALBase, EyouSoft.IDAL.CompanyStructure.ICompanyArea
    {
        /// <summary>
        /// 查询公司所拥有的线路区域列表
        /// </summary>
        private const string SQL_CompanyAreaConfig_SELECT = "SELECT AreaId FROM tbl_CompanyAreaConfig WHERE CompanyId=@CompanyId";
        //SELECT ca.AreaId,sa.AreaName,sa.RouteType FROM tbl_CompanyAreaConfig AS ca,tbl_SysArea AS sa WHERE ca.CompanyId=@CompanyId AND ca.AreaId=sa.ID
        private const string SQL_CompanyUserAreaControl_SELECT = "SELECT AreaId FROM tbl_CompanyUserAreaControl WHERE UserId=@UserId";

        /// <summary>
        /// 所在数据库
        /// </summary>
        protected Database _database;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyArea() 
        {
            this._database = base.CompanyStore;
        }

        /// <summary>
        /// 获得公司所拥有的线路区域列表[只包含线路区域ID]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.AreaBase> GetCompanyArea(string companyId)
        {
            IList<EyouSoft.Model.SystemStructure.AreaBase> items = new List<EyouSoft.Model.SystemStructure.AreaBase>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAreaConfig_SELECT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                    item.AreaId = rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    //item.AreaName = rdr.GetString(rdr.GetOrdinal("AreaName"));
                    //item.RouteType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetInt32(rdr.GetOrdinal("RouteType"));
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 获得用户所拥有的线路区域列表[只包含线路区域ID]
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.AreaBase> GetUserArea(string userId)
        {
            IList<EyouSoft.Model.SystemStructure.AreaBase> items = new List<EyouSoft.Model.SystemStructure.AreaBase>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUserAreaControl_SELECT);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, userId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                    item.AreaId = rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    //item.AreaName = rdr.GetString(rdr.GetOrdinal("AreaName"));
                    //item.RouteType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetInt32(rdr.GetOrdinal("RouteType"));
                    items.Add(item);
                }
            }
            return items;
        }
    }

    /// <summary>
    /// 描述:单位待增加的经营城市[包括出港城市,销售城市]数据访问层
    /// 创建人：张志瑜 2010-07-14
    /// </summary>
    public class CompanyUnCheckedCity : DALBase, EyouSoft.IDAL.CompanyStructure.ICompanyUnCheckedCity
    {
        /// <summary>
        /// 查询公司待增加的销售城市
        /// </summary>
        private const string SQL_CompanyUnCheckedCity_SELECT = "SELECT CityText FROM tbl_CompanyUnCheckedCity WHERE CompanyId=@CompanyId";
        /// <summary>
        /// 新增公司待增加的销售城市
        /// </summary>
        private const string SQL_CompanyUnCheckedCity_INSERT = "INSERT INTO tbl_CompanyUnCheckedCity (CompanyId,CityText) VALUES(@CompanyId, @CityText)";
        /// <summary>
        /// 删除公司待增加的销售城市
        /// </summary>
        private const string SQL_CompanyUnCheckedCity_DELETE = "DELETE FROM tbl_CompanyUnCheckedCity WHERE CompanyId=@CompanyId";

        /// <summary>
        /// 所在数据库
        /// </summary>
        protected Database _database;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyUnCheckedCity() 
        {
            this._database = base.CompanyStore;
        }

        /// <summary>
        /// 新增公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="cityText">城市名称(多个城市,则城市名称使用逗号分割)</param>
        /// <returns></returns>
        public virtual bool AddSaleCity(string companyId, string cityText)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUnCheckedCity_INSERT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "CityText", DbType.String, cityText);
            int rows = 0;
            try
            {
                rows = DbHelper.ExecuteSql(dc, this._database);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (rows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获得公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual string GetSaleCity(string companyId)
        {
            string cityText = "";
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUnCheckedCity_SELECT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (rdr.Read())
                {
                    cityText = rdr.IsDBNull(rdr.GetOrdinal("CityText")) == true ? "" : rdr.GetString(rdr.GetOrdinal("CityText"));
                }
            }

            return cityText;
        }
        /// <summary>
        /// 删除公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual bool DeleteSaleCity(string companyId)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyUnCheckedCity_DELETE);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;            
        }
    }
}
