using System;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.AdvStructure
{
    /// <summary>
    /// 描述:首页推荐产品数据类
    /// 修改记录:
    /// 1 2011-05-10 曹胡生 创建
    /// </summary>
    public class ExtendProduct : DALBase, EyouSoft.IDAL.AdvStructure.IExtendProduct
    {
        #region static constants
        //修改推荐产品
        private const string SQL_UPDATE_ExtendProduct = "update tbl_ExtendProduct set SortID=@SortID,ShowCityID=@ShowCityID,ShowProID=@ShowProID where ID=@ID";
        //删除推荐产品
        private const string SQL_DELETE_ExtendProduct = "delete tbl_ExtendProduct where ID=@ID";
        //添加推荐产品
        private const string SQL_ADD_ExtendProduct = "insert into tbl_ExtendProduct(SortID,CompanyID,ProductID,ProductName,Price,ShowCityID,ShowProID) values(@SortID,@CompanyID,@ProductID,@ProductName,@Price,@ShowCityID,@ShowProID)";
        //判断某个产品在某个城市下是否推荐
        private const string SQL_SELECT_ExtendProductIsZxist = "select count(*) from tbl_ExtendProduct where ShowCityID=@ShowCityID and ProductID=@ProductID";
        #endregion
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database DB;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExtendProduct()
        {
            this.DB = base.SystemStore;
        }
        #endregion
        /// <summary>
        /// 得到所有首页推荐产品
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> GetExtendProductList(int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> items = new List<EyouSoft.Model.AdvStructure.ExtendProductInfo>();
            EyouSoft.Model.AdvStructure.ExtendProductInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_ExtendProduct";
            string primaryKey = "Id";
            string orderByString = "SortId ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("  *,(select CityName from tbl_SysCity where ID=tbl_ExtendProduct.ShowCityID) as cityname,(select RouteName,CompanyName,TourContacMQ,RetailAdultPrice from tbl_TourList where ID=tbl_ExtendProduct.ProductID FOR XML RAW,ROOT('ROOT')) as ProductInfo ");
            #endregion
            #region 拼接查询条件
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this.DB, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.AdvStructure.ExtendProductInfo()
                    {
                        ID = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        SortID = rdr.GetInt32(rdr.GetOrdinal("SortID")),
                        ShowCity = rdr["CityName"].ToString(),
                        ShowCityId = rdr.IsDBNull(rdr.GetOrdinal("ShowCityID")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowCityID")),
                        ShowProId = rdr.IsDBNull(rdr.GetOrdinal("ShowProId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowProId")),
                        CompanyID = rdr["CompanyID"].ToString(),
                        ProductID = rdr["ProductID"].ToString()
                    };
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ProductInfo")))
                    {
                        InputProductInfo(item, rdr.GetString(rdr.GetOrdinal("ProductInfo")));
                    }
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 得到某城市下的所有推荐产品
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> GetExtendProductList(int CityId)
        {
            DbCommand dc = this.DB.GetStoredProcCommand("proc_SelectExtendProduct");
            this.DB.AddInParameter(dc, "CityId", DbType.Int32, CityId);
            IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> items = new List<EyouSoft.Model.AdvStructure.ExtendProductInfo>();
            EyouSoft.Model.AdvStructure.ExtendProductInfo item = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, DB))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.AdvStructure.ExtendProductInfo()
                    {
                        ID = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        SortID = rdr.GetInt32(rdr.GetOrdinal("SortID")),
                        ShowCity = rdr["CityName"].ToString(),
                        ShowCityId = rdr.IsDBNull(rdr.GetOrdinal("ShowCityID")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowCityID")),
                        ShowProId = rdr.IsDBNull(rdr.GetOrdinal("ShowProId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ShowProId")),
                        CompanyID = rdr["CompanyID"].ToString(),
                        ProductID = rdr["ProductID"].ToString()
                    };
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ProductInfo")))
                    {
                        InputProductInfo(item, rdr.GetString(rdr.GetOrdinal("ProductInfo")));
                    }
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 修改推荐产品
        /// </summary>
        /// <param name="ExtendProductInfo"></param>
        /// <returns></returns>
        public bool UpdateExtendProduct(EyouSoft.Model.AdvStructure.ExtendProductInfo ExtendProductInfo)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(SQL_UPDATE_ExtendProduct);
            this.DB.AddInParameter(dc, "ID", DbType.Int32, ExtendProductInfo.ID);
            this.DB.AddInParameter(dc, "SortID", DbType.Int32, ExtendProductInfo.SortID);
            this.DB.AddInParameter(dc, "ShowCityID", DbType.Int32, ExtendProductInfo.ShowCityId);
            this.DB.AddInParameter(dc, "ShowProID", DbType.Int32, ExtendProductInfo.ShowProId);
            return DbHelper.ExecuteSql(dc, DB) > 0 ? true : false;
        }

        /// <summary>
        /// 添加推荐产品
        /// </summary>
        /// <param name="ExtendProductInfo"></param>
        /// <returns></returns>
        public bool AddExtendProduct(EyouSoft.Model.AdvStructure.ExtendProductInfo ExtendProductInfo)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(SQL_ADD_ExtendProduct);
            this.DB.AddInParameter(dc, "SortID", DbType.Int32, ExtendProductInfo.SortID);
            this.DB.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, ExtendProductInfo.CompanyID);
            this.DB.AddInParameter(dc, "ProductID", DbType.AnsiStringFixedLength, ExtendProductInfo.ProductID);
            this.DB.AddInParameter(dc, "ProductName", DbType.String, ExtendProductInfo.ProductName);
            this.DB.AddInParameter(dc, "Price", DbType.Decimal, ExtendProductInfo.Price);
            this.DB.AddInParameter(dc, "ShowCityID", DbType.Int32, ExtendProductInfo.ShowCityId);
            this.DB.AddInParameter(dc, "ShowProID", DbType.Int32, ExtendProductInfo.ShowProId);

            return DbHelper.ExecuteSql(dc, DB) > 0 ? true : false;
        }

        /// <summary>
        /// 删除推荐产品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DelExtendProduct(int ID)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(SQL_DELETE_ExtendProduct);
            this.DB.AddInParameter(dc, "ID", DbType.Int32, ID);
            return DbHelper.ExecuteSql(dc, DB) > 0 ? true : false;
        }

        /// <summary>
        /// 判断某个产品是否推荐
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public bool IsZixst(int CityId, string ProductID)
        {
            DbCommand dc = this.DB.GetSqlStringCommand(SQL_SELECT_ExtendProductIsZxist);
            this.DB.AddInParameter(dc, "ShowCityID", DbType.Int32, CityId);
            this.DB.AddInParameter(dc, "ProductID", DbType.AnsiStringFixedLength, ProductID);
            return DbHelper.Exists(dc, DB);
        }

        /// <summary>
        /// 设置产品信息
        /// </summary>
        /// <param name="model"></param>
        private void InputProductInfo(EyouSoft.Model.AdvStructure.ExtendProductInfo model, string ProductInfo)
        {
            XElement root = XElement.Parse(ProductInfo).Element("row");
            if (root != null)
            {
                model.ProductName = EyouSoft.Common.Utility.GetXAttributeValue(root, "RouteName");
                model.CompanyName = EyouSoft.Common.Utility.GetXAttributeValue(root, "CompanyName");
                model.ContactMQ = EyouSoft.Common.Utility.GetXAttributeValue(root, "TourContacMQ");
                model.Price = EyouSoft.Common.Utility.GetDecimal(EyouSoft.Common.Utility.GetXAttributeValue(root, "RetailAdultPrice"));
            }
        }
    }
}
