using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.PoolStructure
{
    //Author:周文超 2010-11-26

    #region 易诺用户池适用产品类型数据接口

    /// <summary>
    /// 易诺用户池适用产品类型数据访问
    /// </summary>
    public class SuitProduct : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.PoolStructure.ISuitProduct
    {
        #region 构造函数

        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SuitProduct() 
        {
            this._db = base.PoolStore;
        }

        #endregion

        #region SqlString

        private const string Sql_SuitProduct_Insert = " INSERT INTO [tbl_SuitProduct] ([ProductName]) VALUES (@ProductName); ";
        private const string Sql_SuitProduct_Update = " UPDATE [tbl_SuitProduct] SET [ProductName] = @ProductName WHERE ID = @ID; ";
        private const string Sql_SuitProduct_Delete = " DELETE FROM [tbl_SuitProduct] WHERE ID in ({0}); ";
        private const string Sql_SuitProduct_Select = " SELECT [ID],[ProductName] FROM [tbl_SuitProduct] ";

        #endregion

        #region ISuitProduct 成员

        /// <summary>
        /// 新增适用产品类型
        /// </summary>
        /// <param name="model">适用产品类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public virtual int AddSuitProduct(EyouSoft.Model.PoolStructure.SuitProductInfo model)
        {
            if (model == null)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(Sql_SuitProduct_Insert);
            this._db.AddInParameter(dc, "ProductName", DbType.String, model.ProductName);

            return EyouSoft.Common.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 修改适用产品类型
        /// </summary>
        /// <param name="model">适用产品类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public virtual int UpdateSuitProduct(EyouSoft.Model.PoolStructure.SuitProductInfo model)
        {
            if (model == null || model.ProuctId <= 0)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(Sql_SuitProduct_Update);
            this._db.AddInParameter(dc, "ID", DbType.Int32, model.ProuctId);
            this._db.AddInParameter(dc, "ProductName", DbType.String, model.ProductName);

            return EyouSoft.Common.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 删除适用产品类型
        /// </summary>
        /// <param name="SuitProductIds">适用产品类型ID集合</param>
        /// <returns>1：成功；0：失败</returns>
        public virtual int DeleteSuitProduct(List<int> SuitProductIds)
        {
            if (SuitProductIds == null || SuitProductIds.Count <= 0)
                return 0;

            string strIds = string.Empty;
            foreach (int tmp in SuitProductIds)
            {
                if (tmp > 0)
                    strIds += tmp.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');
            if (string.IsNullOrEmpty(strIds))
                return 0;

            string strSql = string.Format(Sql_SuitProduct_Delete, strIds);
            DbCommand dc = this._db.GetSqlStringCommand(strSql);

            return EyouSoft.Common.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0; 
        }

        /// <summary>
        /// 根据ID获取适用产品类型
        /// </summary>
        /// <param name="SuitProductId">适用产品类型Id</param>
        /// <returns>适用产品类型实体</returns>
        public virtual EyouSoft.Model.PoolStructure.SuitProductInfo GetSuitProduct(int SuitProductId)
        {
            EyouSoft.Model.PoolStructure.SuitProductInfo model = new EyouSoft.Model.PoolStructure.SuitProductInfo();

            if (SuitProductId <= 0)
                return model;

            string strSql = Sql_SuitProduct_Select + " where ID = @ID ;";
            DbCommand dc = this._db.GetSqlStringCommand(strSql);
            this._db.AddInParameter(dc, "ID", DbType.Int32, SuitProductId);

            using (IDataReader dr = EyouSoft.Common.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        model.ProuctId = int.Parse(dr["ID"].ToString());
                    model.ProductName = dr["ProductName"].ToString();
                }
            }

            return model;
        }

        /// <summary>
        /// 获取适用产品类型
        /// </summary>
        /// <returns>适用产品类型实体集合</returns>
        public virtual IList<EyouSoft.Model.PoolStructure.SuitProductInfo> GetSuitProductList()
        {
            IList<EyouSoft.Model.PoolStructure.SuitProductInfo> list = new List<EyouSoft.Model.PoolStructure.SuitProductInfo>();
            DbCommand dc = this._db.GetSqlStringCommand(Sql_SuitProduct_Select);
            using (IDataReader dr = EyouSoft.Common.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                EyouSoft.Model.PoolStructure.SuitProductInfo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.PoolStructure.SuitProductInfo();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        model.ProuctId = int.Parse(dr["ID"].ToString());
                    model.ProductName = dr["ProductName"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        #endregion
    }

    #endregion

    #region 易诺用户池适用产品类型数据接口

    /// <summary>
    /// 易诺用户池适用产品类型数据接口
    /// </summary>
    public class CustomerType : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.PoolStructure.ICustomerType
    {
        #region 构造函数

        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerType() 
        {
            this._db = base.PoolStore;
        }

        #endregion

        #region SqlString

        private const string Sql_CustomerType_Insert = " INSERT INTO [tbl_CustomerType] ([TypeName]) VALUES (@TypeName); ";
        private const string Sql_CustomerType_Update = " UPDATE [tbl_CustomerType] SET [TypeName] = @TypeName WHERE ID = @ID; ";
        private const string Sql_CustomerType_Delete = " if (not exists (select 1 from tbl_CompanyCustomerTypeConfig where TypeId in ({0}))) and (not exists (select 1 from tbl_UserCustomerType where CustomerId in ({0}))) DELETE FROM [tbl_CustomerType] WHERE ID in ({0});";
        private const string Sql_CustomerType_Select = " SELECT [ID],[TypeName] FROM [tbl_CustomerType] ";

        #endregion

        #region ICustomerType 成员

        /// <summary>
        /// 新增客户类型
        /// </summary>
        /// <param name="model">客户类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public virtual int AddCustomerType(EyouSoft.Model.PoolStructure.CustomerTypeInfo model)
        {
            if (model == null)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(Sql_CustomerType_Insert);
            this._db.AddInParameter(dc, "TypeName", DbType.String, model.TypeName);

            return EyouSoft.Common.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 修改客户类型
        /// </summary>
        /// <param name="model">客户类型实体</param>
        /// <returns>1：成功；0：失败</returns>
        public virtual int UpdateCustomerType(EyouSoft.Model.PoolStructure.CustomerTypeInfo model)
        {
            if (model == null || model.TypeId <= 0)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(Sql_CustomerType_Update);
            this._db.AddInParameter(dc, "ID", DbType.Int32, model.TypeId);
            this._db.AddInParameter(dc, "TypeName", DbType.String, model.TypeName);

            return EyouSoft.Common.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 删除客户类型
        /// </summary>
        /// <param name="CustomerTypeIds">客户类型ID集合</param>
        /// <returns>1：成功；0：失败</returns>
        public virtual int DeleteCustomerType(List<int> CustomerTypeIds)
        {
            if (CustomerTypeIds == null || CustomerTypeIds.Count <= 0)
                return 0;

            string strIds = string.Empty;
            foreach (int tmp in CustomerTypeIds)
            {
                if (tmp > 0)
                    strIds += tmp.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');
            if (string.IsNullOrEmpty(strIds))
                return 0;

            string strSql = string.Format(Sql_CustomerType_Delete, strIds);
            DbCommand dc = this._db.GetSqlStringCommand(strSql);

            return EyouSoft.Common.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0; 
        }

        /// <summary>
        /// 获取客户类型
        /// </summary>
        /// <param name="CustomerTypeId">客户类型Id</param>
        /// <returns>客户类型实体</returns>
        public virtual EyouSoft.Model.PoolStructure.CustomerTypeInfo GetCustomerType(int CustomerTypeId)
        {
            EyouSoft.Model.PoolStructure.CustomerTypeInfo model = new EyouSoft.Model.PoolStructure.CustomerTypeInfo();

            if (CustomerTypeId <= 0)
                return model;

            string strSql = Sql_CustomerType_Select + " where ID = @ID ;";
            DbCommand dc = this._db.GetSqlStringCommand(strSql);
            this._db.AddInParameter(dc, "ID", DbType.Int32, CustomerTypeId);

            using (IDataReader dr = EyouSoft.Common.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        model.TypeId = int.Parse(dr["ID"].ToString());
                    model.TypeName = dr["TypeName"].ToString();
                }
            }

            return model;
        }

        /// <summary>
        /// 获取客户类型
        /// </summary>
        /// <returns>客户类型实体集合</returns>
        public virtual IList<EyouSoft.Model.PoolStructure.CustomerTypeInfo> GetCustomerTypeList()
        {
            IList<EyouSoft.Model.PoolStructure.CustomerTypeInfo> list = new List<EyouSoft.Model.PoolStructure.CustomerTypeInfo>();
            DbCommand dc = this._db.GetSqlStringCommand(Sql_CustomerType_Select);
            using (IDataReader dr = EyouSoft.Common.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                EyouSoft.Model.PoolStructure.CustomerTypeInfo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.PoolStructure.CustomerTypeInfo();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                        model.TypeId = int.Parse(dr["ID"].ToString());
                    model.TypeName = dr["TypeName"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        #endregion
    }

    #endregion
}
