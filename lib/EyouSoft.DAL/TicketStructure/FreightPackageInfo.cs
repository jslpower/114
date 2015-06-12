using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 运价套餐数据层
    /// </summary>
    /// 鲁功源 2010-10-28
    public class FreightPackageInfo : DALBase, EyouSoft.IDAL.TicketStructure.IFreightPackageInfo
    {

        #region 构造函数
        Database _database = null;
        public FreightPackageInfo()
        {
            _database = base.TicketStore;
        }
        #endregion

        #region Sql变量定义
        private const string Sql_FreightPackageInfo_DELETE = "Update tbl_TicketFreightPackageInfo set IsDeleted='1' where id=@Id";
        private const string Sql_FreightPackageInfo_GETMODEL = "SELECT [Id],[PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[DestCityIds],[MonthPrice],[QuarterPrice],[HalfYearPrice],[YearPrice],[IssueTime],[OperatorId],[IsDeleted] FROM [tbl_TicketFreightPackageInfo] WHERE Id=@Id";
        private const string Sql_FreightPackageInfo_UPDATE = "UPDATE [tbl_TicketFreightPackageInfo] set [PackageName]=@PackageName,[PackageType]=@PackageType,[RateType]=@RateType,[FlightId]=@FlightId,[FlightName]=@FlightName,[HomeCityId]=@HomeCityId,[DestCityIds]=@DestCityIds,[MonthPrice]=@MonthPrice,[QuarterPrice]=@QuarterPrice,[HalfYearPrice]=@HalfYearPrice,[YearPrice]=@YearPrice,[IssueTime]=@IssueTime,[OperatorId]=@OperatorId,[IsDeleted]=@IsDeleted where Id=@Id";
        private const string Sql_FreightPackageInfo_INSERT = "INSERT INTO [tbl_TicketFreightPackageInfo]([PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[DestCityIds],[MonthPrice],[QuarterPrice],[HalfYearPrice],[YearPrice],[IssueTime],[OperatorId],[IsDeleted]) VALUES(@PackageName,@PackageType,@RateType,@FlightId,@FlightName,@HomeCityId,@DestCityIds,@MonthPrice,@QuarterPrice,@HalfYearPrice,@YearPrice,@IssueTime,@OperatorId,@IsDeleted)";
        #endregion

        #region IFreightPackageInfo 成员
        /// <summary>
        /// 运营后台-添加套餐信息
        /// </summary>
        /// <param name="model">套餐信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_FreightPackageInfo_INSERT);
            this._database.AddInParameter(dc, "PackageName", DbType.String, model.PackageName);
            this._database.AddInParameter(dc, "PackageType", DbType.Int16, (int)model.PackageType);
            this._database.AddInParameter(dc, "RateType", DbType.Int16, (int)model.RateType);
            this._database.AddInParameter(dc, "FlightId", DbType.Int32, model.FlightId);
            this._database.AddInParameter(dc, "FlightName", DbType.String, model.FlightName);
            this._database.AddInParameter(dc, "HomeCityId", DbType.Int32, model.HomeCityId);
            this._database.AddInParameter(dc, "DestCityIds", DbType.String, model.DestCityIds);
            this._database.AddInParameter(dc, "MonthPrice", DbType.Decimal, model.MonthPrice);
            this._database.AddInParameter(dc, "QuarterPrice", DbType.Decimal, model.QuarterPrice);
            this._database.AddInParameter(dc, "HalfYearPrice", DbType.Decimal, model.HalfYearPrice);
            this._database.AddInParameter(dc, "YearPrice", DbType.Decimal, model.YearPrice);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            this._database.AddInParameter(dc, "IsDeleted",DbType.AnsiStringFixedLength, "1");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 运营后台-修改套餐信息
        /// </summary>
        /// <param name="model">套餐信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_FreightPackageInfo_UPDATE);
            this._database.AddInParameter(dc, "Id", DbType.Int32, model.Id);
            this._database.AddInParameter(dc, "PackageName", DbType.String, model.PackageName);
            this._database.AddInParameter(dc, "PackageType", DbType.Int16, (int)model.PackageType);
            this._database.AddInParameter(dc, "RateType", DbType.Int16, (int)model.RateType);
            this._database.AddInParameter(dc, "FlightId", DbType.Int32, model.FlightId);
            this._database.AddInParameter(dc, "FlightName", DbType.String, model.FlightName);
            this._database.AddInParameter(dc, "HomeCityId", DbType.Int32, model.HomeCityId);
            this._database.AddInParameter(dc, "DestCityIds", DbType.String, model.DestCityIds);
            this._database.AddInParameter(dc, "MonthPrice", DbType.Decimal, model.MonthPrice);
            this._database.AddInParameter(dc, "QuarterPrice", DbType.Decimal, model.QuarterPrice);
            this._database.AddInParameter(dc, "HalfYearPrice", DbType.Decimal, model.HalfYearPrice);
            this._database.AddInParameter(dc, "YearPrice", DbType.Decimal, model.YearPrice);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            this._database.AddInParameter(dc, "IsDeleted",DbType.AnsiStringFixedLength,  "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 虚拟删除套餐信息
        /// </summary>
        /// <param name="Id">套餐主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(int Id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_FreightPackageInfo_DELETE);
            this._database.AddInParameter(dc, "Id", DbType.Int32, Id);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取套餐信息实体
        /// </summary>
        /// <param name="Id">套餐主键编号</param>
        /// <returns>套餐信息实体</returns>
        public virtual EyouSoft.Model.TicketStructure.TicketFreightPackageInfo GetModel(int Id)
        {
            EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model = null;
            DbCommand dc = this._database.GetSqlStringCommand(Sql_FreightPackageInfo_GETMODEL);
            this._database.AddInParameter(dc, "Id", DbType.Int32, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketFreightPackageInfo();
                    model.Id = dr.GetInt32(0);
                    model.PackageName = dr.IsDBNull(1) ? string.Empty : dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.PackageType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(2).ToString());
                    if (!dr.IsDBNull(3))
                        model.RateType = (EyouSoft.Model.TicketStructure.RateType)int.Parse(dr.GetByte(3).ToString());
                    model.FlightId = dr.GetInt32(4);
                    model.FlightName = dr.IsDBNull(5) ? string.Empty : dr[5].ToString();
                    model.HomeCityId = dr.GetInt32(6);
                    model.DestCityIds = dr.IsDBNull(7) ? string.Empty : dr[7].ToString();
                    model.MonthPrice = dr.IsDBNull(8) ? 0 : dr.GetDecimal(8);
                    model.QuarterPrice = dr.IsDBNull(9) ? 0 : dr.GetDecimal(9);
                    model.HalfYearPrice = dr.IsDBNull(10) ? 0 : dr.GetDecimal(10);
                    model.YearPrice = dr.IsDBNull(11) ? 0 : dr.GetDecimal(11);
                    model.IssueTime = dr.IsDBNull(12) ? DateTime.MinValue : dr.GetDateTime(12);
                    model.OperatorId = dr.GetInt32(13);
                    model.IsDeleted = dr[14].ToString() == "1" ? true : false;
                }
            }
            return model;
        }
        /// <summary>
        /// 根据指定条件分页获取套餐信息列表
        /// </summary>
        /// <param name="pageSize">每页显示天数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="PackageType">套餐类型 =null返回全部</param>
        /// <param name="IsDeleted">是否已删除 =null返回全部</param>
        /// <returns>套餐信息列表</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketFreightPackageInfo> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TicketStructure.PackageTypes? PackageType, bool? IsDeleted)
        {
            IList<EyouSoft.Model.TicketStructure.TicketFreightPackageInfo> list = new List<EyouSoft.Model.TicketStructure.TicketFreightPackageInfo>();
            
            #region 分页查询变量
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            string tableName = "tbl_TicketFreightPackageInfo";
            string fields = "[Id],[PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[DestCityIds],[MonthPrice],[QuarterPrice],[HalfYearPrice],[YearPrice],[IssueTime]";
            string primaryKey="Id";
            string orderByString=" id desc";
            if (PackageType.HasValue)
            {
                strWhere.AppendFormat(" and PackageType={0} ", (int)PackageType.Value);
            }
            if (IsDeleted.HasValue)
            {
                strWhere.AppendFormat(" and IsDeleted='{0}' ", IsDeleted.Value ? "1" : "0");
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    #region 实体赋值
                    EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model = new EyouSoft.Model.TicketStructure.TicketFreightPackageInfo();
                    model = new EyouSoft.Model.TicketStructure.TicketFreightPackageInfo();
                    model.Id = dr.GetInt32(0);
                    model.PackageName = dr.IsDBNull(1) ? string.Empty : dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.PackageType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(2).ToString());
                    if (!dr.IsDBNull(3))
                        model.RateType = (EyouSoft.Model.TicketStructure.RateType)int.Parse(dr.GetByte(3).ToString());
                    model.FlightId = dr.GetInt32(4);
                    model.FlightName = dr.IsDBNull(5) ? string.Empty : dr[5].ToString();
                    model.HomeCityId = dr.GetInt32(6);
                    model.DestCityIds = dr.IsDBNull(7) ? string.Empty : dr[7].ToString();
                    model.MonthPrice = dr.IsDBNull(8) ? 0 : dr.GetDecimal(8);
                    model.QuarterPrice = dr.IsDBNull(9) ? 0 : dr.GetDecimal(9);
                    model.HalfYearPrice = dr.IsDBNull(10) ? 0 : dr.GetDecimal(10);
                    model.YearPrice = dr.IsDBNull(11) ? 0 : dr.GetDecimal(11);
                    model.IssueTime = dr.IsDBNull(12) ? DateTime.MinValue : dr.GetDateTime(12);
                    list.Add(model);
                    model = null;
                    #endregion
                }
            }
            return list;
        }

        #endregion
    }
}
