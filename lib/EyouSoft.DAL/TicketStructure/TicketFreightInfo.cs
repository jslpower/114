using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 运价信息数据访问层
    /// </summary>
    /// 周文超 2010-10-28
    public class TicketFreightInfo : DALBase, EyouSoft.IDAL.TicketStructure.ITicketFreightInfo
    {
        private Database _dataBase = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketFreightInfo()
        {
            this._dataBase = base.TicketStore;
        }

        #region SqlString

        private const string Sql_FreightInfo_Insert = " INSERT INTO [tbl_TicketFreightInfo]([Id],[CompanyId],[RateType],[FreightType],[ProductType],[BuyType],[FlightId],[FlightName],[NoGadHomeCityId],[NoGadHomeCityIdName],[NoGadDestCityId],[NoGadDestCityName],[GadHomeCityId],[GadHomeCityName],[GadDestCityId],[GadDestCityName],[IsEnabled],[IsCheckPNR],[IsExpired],[FreightStartDate],[FreightEndDate],[MaxPCount],[WorkStartTime],[WorkEndTime],[SupplierRemark],[LastUpdateDate],[FreightBuyId],[OperatorId],[IssueTime]) VALUES (@Id,@CompanyId,@RateType,@FreightType,@ProductType,@BuyType,@FlightId,@FlightName,@NoGadHomeCityId,@NoGadHomeCityIdName,@NoGadDestCityId,@NoGadDestCityName,@GadHomeCityId,@GadHomeCityName,@GadDestCityId,@GadDestCityName,@IsEnabled,@IsCheckPNR,@IsExpired,@FreightStartDate,@FreightEndDate,@MaxPCount,@WorkStartTime,@WorkEndTime,@SupplierRemark,@LastUpdateDate,@FreightBuyId,@OperatorId,@IssueTime); ";
        private const string Sql_FreightInfo_Update = " UPDATE [tbl_TicketFreightInfo] SET [FreightBuyId] = @FreightBuyId,[RateType] = @RateType,[FreightType] = @FreightType,[ProductType] = @ProductType,[BuyType] = @BuyType,[FlightId] = @FlightId,[FlightName] = @FlightName,[NoGadHomeCityId] = @NoGadHomeCityId,[NoGadHomeCityIdName] = @NoGadHomeCityIdName,[NoGadDestCityId] = @NoGadDestCityId,[NoGadDestCityName] = @NoGadDestCityName,[GadHomeCityId] = @GadHomeCityId,[GadHomeCityName] = @GadHomeCityName,[GadDestCityId] = @GadDestCityId,[GadDestCityName] = @GadDestCityName,[IsEnabled] = @IsEnabled,[IsCheckPNR] = @IsCheckPNR,[FreightStartDate] = @FreightStartDate,[FreightEndDate] = @FreightEndDate,[MaxPCount] = @MaxPCount,[WorkStartTime] = @WorkStartTime,[WorkEndTime] = @WorkEndTime,[SupplierRemark] = @SupplierRemark,[LastUpdateDate] = @LastUpdateDate,[OperatorId] = @OperatorId WHERE [Id] = @Id ;";
        private const string Sql_FreightInfo_UpdateIsEnabled = " UPDATE [tbl_TicketFreightInfo] SET [IsEnabled] = @IsEnabled WHERE [Id] = @Id ; ";
        private const string Sql_FreightInfo_UpdateIsExpired = " UPDATE [tbl_TicketFreightInfo] SET [IsExpired] = @IsExpired WHERE [Id] = @Id ; ";
        private const string Sql_FreightInfo_Select = " SELECT [Id],[CompanyId],(select CompanyName from tbl_CompanyInfo where tbl_CompanyInfo.ID = tbl_TicketFreightInfo.CompanyId) as CompanyName,[RateType],[FreightType],[ProductType],[BuyType],[FlightId],[FlightName],[NoGadHomeCityId],[NoGadHomeCityIdName],[NoGadDestCityId],[NoGadDestCityName],[GadHomeCityId],[GadHomeCityName],[GadDestCityId],[GadDestCityName],[IsEnabled],[IsCheckPNR],[IsExpired],[FreightStartDate],[FreightEndDate],[MaxPCount],[WorkStartTime],[WorkEndTime],[SupplierRemark],[LastUpdateDate],[FreightBuyId],[OperatorId],[IssueTime] FROM [tbl_TicketFreightInfo] ";

        private const string Sql_TicketFreightAddition_Insert = " INSERT INTO [tbl_TicketFreightAddition]([FreightId],[FromType],[SelfDate],[ForDay],[ReferPrice],[ReferRate],[SetPrice],[FuelPrice],[BuildPrice]) VALUES (@Id,{0},{1},'{2}',{3},{4},{5},{6},{7}) ;";
        private const string Sql_TicketFreightAddition_Delete = " DELETE FROM [tbl_TicketFreightAddition] WHERE FreightId = @Id ";
        private const string Sql_TicketFreightAddition_Select = " SELECT [FreightId],[FromType],[SelfDate],[ForDay],[ReferPrice],[ReferRate],[SetPrice],[FuelPrice],[BuildPrice] FROM [tbl_TicketFreightAddition] ";

        #endregion

        #region ITicketFreightInfo 成员

        /// <summary>
        /// 添加运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>返回1：添加成功</returns>
        public virtual int AddFreightInfo(EyouSoft.Model.TicketStructure.TicketFreightInfo model)
        {
            if (model == null)
                return 0;

            model.Id = Guid.NewGuid().ToString();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(Sql_FreightInfo_Insert);
            if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
            {
                strSql.AppendFormat(Sql_TicketFreightAddition_Insert, 0, model.FromSelfDate.HasValue ? "'" + model.FromSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.FromForDay, model.FromReferPrice, model.FromReferRate, model.FromSetPrice, model.FromFuelPrice, model.FromBuildPrice);
            }
            else if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
            {
                strSql.AppendFormat(Sql_TicketFreightAddition_Insert, 0, model.FromSelfDate.HasValue ? "'" + model.FromSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.FromForDay, model.FromReferPrice, model.FromReferRate, model.FromSetPrice, model.FromFuelPrice, model.FromBuildPrice);
                strSql.AppendFormat(Sql_TicketFreightAddition_Insert, 1, model.ToSelfDate.HasValue ? "'" + model.ToSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.ToForDay, model.ToReferPrice, model.ToReferRate, model.ToSetPrice, model.ToFuelPrice, model.ToBuildPrice);
            }

            DbCommand dc = this._dataBase.GetSqlStringCommand(strSql.ToString());

            #region 参数赋值

            this._dataBase.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.Id);
            this._dataBase.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.Company.ID);
            this._dataBase.AddInParameter(dc, "RateType", DbType.Byte, (int)model.RateType);
            this._dataBase.AddInParameter(dc, "FreightType", DbType.Byte, (int)model.FreightType);
            this._dataBase.AddInParameter(dc, "ProductType", DbType.Byte, (int)model.ProductType);
            this._dataBase.AddInParameter(dc, "BuyType", DbType.Byte, (int)model.BuyType);
            this._dataBase.AddInParameter(dc, "FlightId", DbType.Int32, model.FlightId);
            this._dataBase.AddInParameter(dc, "FlightName", DbType.String, model.FlightName);
            this._dataBase.AddInParameter(dc, "NoGadHomeCityId", DbType.Int32, model.NoGadHomeCityId);
            this._dataBase.AddInParameter(dc, "NoGadHomeCityIdName", DbType.String, model.NoGadHomeCityIdName);
            this._dataBase.AddInParameter(dc, "NoGadDestCityId", DbType.Int32, model.NoGadDestCityId);
            this._dataBase.AddInParameter(dc, "NoGadDestCityName", DbType.String, model.NoGadDestCityName);
            this._dataBase.AddInParameter(dc, "GadHomeCityId", DbType.Int32, model.GadHomeCityId);
            this._dataBase.AddInParameter(dc, "GadHomeCityName", DbType.String, model.GadHomeCityName);
            this._dataBase.AddInParameter(dc, "GadDestCityId", DbType.Int32, model.GadDestCityId);
            this._dataBase.AddInParameter(dc, "GadDestCityName", DbType.String, model.GadDestCityName);
            this._dataBase.AddInParameter(dc, "IsEnabled", DbType.AnsiStringFixedLength, model.IsEnabled ? "1" : "0");
            this._dataBase.AddInParameter(dc, "IsCheckPNR", DbType.AnsiStringFixedLength, model.IsCheckPNR ? "1" : "0");
            this._dataBase.AddInParameter(dc, "IsExpired", DbType.AnsiStringFixedLength, model.IsExpired ? "1" : "0");
            this._dataBase.AddInParameter(dc, "FreightStartDate", DbType.DateTime, model.FreightStartDate);
            this._dataBase.AddInParameter(dc, "FreightEndDate", DbType.DateTime, model.FreightEndDate);
            this._dataBase.AddInParameter(dc, "MaxPCount", DbType.Int32, model.MaxPCount);
            this._dataBase.AddInParameter(dc, "WorkStartTime", DbType.String, model.WorkStartTime);
            this._dataBase.AddInParameter(dc, "WorkEndTime", DbType.String, model.WorkEndTime);
            this._dataBase.AddInParameter(dc, "SupplierRemark", DbType.String, model.SupplierRemark);
            this._dataBase.AddInParameter(dc, "LastUpdateDate", DbType.DateTime, model.LastUpdateDate);
            this._dataBase.AddInParameter(dc, "FreightBuyId", DbType.AnsiStringFixedLength, model.FreightBuyId);
            this._dataBase.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._dataBase.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            #endregion

            return DbHelper.ExecuteSql(dc, this._dataBase) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 批量添加运价信息
        /// </summary>
        /// <param name="FreightInfoList">运价信息实体集合</param>
        /// <returns>返回1：添加成功</returns>
        public virtual int AddFreightInfo(IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> FreightInfoList)
        {
            if (FreightInfoList == null || FreightInfoList.Count <= 0)
                return 0;

            StringBuilder strSql = new StringBuilder();
            string strFreightAdditionInsert = " INSERT INTO [tbl_TicketFreightAddition]([FreightId],[FromType],[SelfDate],[ForDay],[ReferPrice],[ReferRate],[SetPrice],[FuelPrice],[BuildPrice]) VALUES ('{0}',{1},{2},'{3}',{4},{5},{6},{7},{8}) ;";

            #region 拼接Sql语句

            foreach (EyouSoft.Model.TicketStructure.TicketFreightInfo model in FreightInfoList)
            {
                if (model == null)
                    continue;

                model.Id = Guid.NewGuid().ToString();
                strSql.Append(" INSERT INTO [tbl_TicketFreightInfo] ([Id],[CompanyId],[RateType],[FreightType],[ProductType],[BuyType],[FlightId],[FlightName],[NoGadHomeCityId],[NoGadHomeCityIdName],[NoGadDestCityId],[NoGadDestCityName],[GadHomeCityId],[GadHomeCityName],[GadDestCityId],[GadDestCityName],[IsEnabled],[IsCheckPNR],[IsExpired],[FreightStartDate],[FreightEndDate],[MaxPCount],[WorkStartTime],[WorkEndTime],[SupplierRemark],[LastUpdateDate],[FreightBuyId],[OperatorId],[IssueTime]) VALUES ( ");
                strSql.AppendFormat("'{0}',", model.Id);
                strSql.AppendFormat("'{0}',", model.Company.ID);
                strSql.AppendFormat("{0},", (int)model.RateType);
                strSql.AppendFormat("{0},", (int)model.FreightType);
                strSql.AppendFormat("{0},", (int)model.ProductType);
                strSql.AppendFormat("{0},", (int)model.BuyType);
                strSql.AppendFormat("{0},", model.FlightId);
                strSql.AppendFormat("'{0}',", model.FlightName);
                strSql.AppendFormat("{0},", model.NoGadHomeCityId);
                strSql.AppendFormat("'{0}',", model.NoGadHomeCityIdName);
                strSql.AppendFormat("{0},", model.NoGadDestCityId);
                strSql.AppendFormat("'{0}',", model.NoGadDestCityName);
                strSql.AppendFormat("{0},", model.GadHomeCityId);
                strSql.AppendFormat("'{0}',", model.GadHomeCityName);
                strSql.AppendFormat("{0},", model.GadDestCityId);
                strSql.AppendFormat("'{0}',", model.GadDestCityName);
                strSql.AppendFormat("'{0}',", model.IsEnabled ? "1" : "0");
                strSql.AppendFormat("'{0}',", model.IsCheckPNR ? "1" : "0");
                strSql.AppendFormat("'{0}',", model.IsExpired ? "1" : "0");
                strSql.AppendFormat("{0},", model.FreightStartDate.HasValue ? string.Format("'{0}'", DateTime.Parse(model.FreightStartDate.ToString()).ToString("yyyy-MM-dd hh:mm:ss")) : "null");
                strSql.AppendFormat("{0},", model.FreightEndDate.HasValue ? string.Format("'{0}'", DateTime.Parse(model.FreightEndDate.ToString()).ToString("yyyy-MM-dd hh:mm:ss")) : "null");
                strSql.AppendFormat("{0},", model.MaxPCount);
                strSql.AppendFormat("'{0}',", model.WorkStartTime);
                strSql.AppendFormat("'{0}',", model.WorkEndTime);
                strSql.AppendFormat("'{0}',", model.SupplierRemark);
                strSql.AppendFormat("{0},", model.LastUpdateDate.HasValue ? string.Format("'{0}'", DateTime.Parse(model.LastUpdateDate.ToString()).ToString("yyyy-MM-dd hh:mm:ss")) : "null");
                strSql.AppendFormat("'{0}',", model.FreightBuyId);
                strSql.AppendFormat("'{0}',", model.OperatorId);
                strSql.AppendFormat("'{0}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                strSql.Append("); ");

                //来回程信息
                if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
                {
                    strSql.AppendFormat(strFreightAdditionInsert, model.Id, 0, model.FromSelfDate.HasValue ? "'" + model.FromSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.FromForDay, model.FromReferPrice, model.FromReferRate, model.FromSetPrice, model.FromFuelPrice, model.FromBuildPrice);
                }
                else if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                {
                    strSql.AppendFormat(strFreightAdditionInsert, model.Id, 0, model.FromSelfDate.HasValue ? "'" + model.FromSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.FromForDay, model.FromReferPrice, model.FromReferRate, model.FromSetPrice, model.FromFuelPrice, model.FromBuildPrice);
                    strSql.AppendFormat(strFreightAdditionInsert, model.Id, 1, model.ToSelfDate.HasValue ? "'" + model.ToSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.ToForDay, model.ToReferPrice, model.ToReferRate, model.ToSetPrice, model.ToFuelPrice, model.ToBuildPrice);
                }
            }

            #endregion

            if (string.IsNullOrEmpty(strSql.ToString()))
                return 0;

            DbCommand dc = this._dataBase.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSql(dc, this._dataBase) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 修改运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>返回1：添加成功</returns>
        public virtual int UpdateFreightInfo(EyouSoft.Model.TicketStructure.TicketFreightInfo model)
        {
            if (model == null)
                return 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(Sql_FreightInfo_Update);
            strSql.Append(Sql_TicketFreightAddition_Delete);
            if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
            {
                strSql.AppendFormat(Sql_TicketFreightAddition_Insert, 0, model.FromSelfDate.HasValue ? "'" + model.FromSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.FromForDay, model.FromReferPrice, model.FromReferRate, model.FromSetPrice, model.FromFuelPrice, model.FromBuildPrice);
            }
            else if (model.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
            {
                strSql.AppendFormat(Sql_TicketFreightAddition_Insert, 0, model.FromSelfDate.HasValue ? "'" + model.FromSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.FromForDay, model.FromReferPrice, model.FromReferRate, model.FromSetPrice, model.FromFuelPrice, model.FromBuildPrice);
                strSql.AppendFormat(Sql_TicketFreightAddition_Insert, 1, model.ToSelfDate.HasValue ? "'" + model.ToSelfDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" : "null", model.ToForDay, model.ToReferPrice, model.ToReferRate, model.ToSetPrice, model.ToFuelPrice, model.ToBuildPrice);
            }

            DbCommand dc = this._dataBase.GetSqlStringCommand(strSql.ToString());

            #region 参数赋值

            this._dataBase.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.Id);
            this._dataBase.AddInParameter(dc, "RateType", DbType.Byte, (int)model.RateType);
            this._dataBase.AddInParameter(dc, "FreightType", DbType.Byte, (int)model.FreightType);
            this._dataBase.AddInParameter(dc, "ProductType", DbType.Byte, (int)model.ProductType);
            this._dataBase.AddInParameter(dc, "BuyType", DbType.Byte, (int)model.BuyType);
            this._dataBase.AddInParameter(dc, "FlightId", DbType.Int32, model.FlightId);
            this._dataBase.AddInParameter(dc, "FlightName", DbType.String, model.FlightName);
            this._dataBase.AddInParameter(dc, "NoGadHomeCityId", DbType.Int32, model.NoGadHomeCityId);
            this._dataBase.AddInParameter(dc, "NoGadHomeCityIdName", DbType.String, model.NoGadHomeCityIdName);
            this._dataBase.AddInParameter(dc, "NoGadDestCityId", DbType.Int32, model.NoGadDestCityId);
            this._dataBase.AddInParameter(dc, "NoGadDestCityName", DbType.String, model.NoGadDestCityName);
            this._dataBase.AddInParameter(dc, "GadHomeCityId", DbType.Int32, model.GadHomeCityId);
            this._dataBase.AddInParameter(dc, "GadHomeCityName", DbType.String, model.GadHomeCityName);
            this._dataBase.AddInParameter(dc, "GadDestCityId", DbType.Int32, model.GadDestCityId);
            this._dataBase.AddInParameter(dc, "GadDestCityName", DbType.String, model.GadDestCityName);
            this._dataBase.AddInParameter(dc, "IsEnabled", DbType.AnsiStringFixedLength, model.IsEnabled ? "1" : "0");
            this._dataBase.AddInParameter(dc, "IsCheckPNR", DbType.AnsiStringFixedLength, model.IsCheckPNR ? "1" : "0");
            this._dataBase.AddInParameter(dc, "FreightStartDate", DbType.DateTime, model.FreightStartDate);
            this._dataBase.AddInParameter(dc, "FreightEndDate", DbType.DateTime, model.FreightEndDate);
            this._dataBase.AddInParameter(dc, "MaxPCount", DbType.Int32, model.MaxPCount);
            this._dataBase.AddInParameter(dc, "WorkStartTime", DbType.String, model.WorkStartTime);
            this._dataBase.AddInParameter(dc, "WorkEndTime", DbType.String, model.WorkEndTime);
            this._dataBase.AddInParameter(dc, "SupplierRemark", DbType.String, model.SupplierRemark);
            this._dataBase.AddInParameter(dc, "LastUpdateDate", DbType.DateTime, model.LastUpdateDate);
            this._dataBase.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._dataBase.AddInParameter(dc, "FreightBuyId", DbType.AnsiStringFixedLength, model.FreightBuyId);

            #endregion

            return DbHelper.ExecuteSqlTrans(dc, this._dataBase) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 设置是否启用运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回true表示成功</returns>
        public virtual bool SetIsEnabled(string FreightInfoId, bool IsEnabled)
        {
            if (string.IsNullOrEmpty(FreightInfoId))
                return false;

            DbCommand dc = this._dataBase.GetSqlStringCommand(Sql_FreightInfo_UpdateIsEnabled);

            this._dataBase.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, FreightInfoId);
            this._dataBase.AddInParameter(dc, "IsEnabled", DbType.AnsiStringFixedLength, IsEnabled ? "1" : "0");

            return DbHelper.ExecuteSql(dc, this._dataBase) > 0 ? true : false;
        }

        /// <summary>
        /// 设置运价信息是否过期
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <param name="IsExpired">是否过期</param>
        /// <returns>返回true表示成功</returns>
        public virtual bool SetIsExpired(string FreightInfoId, bool IsExpired)
        {
            if (string.IsNullOrEmpty(FreightInfoId))
                return false;

            DbCommand dc = this._dataBase.GetSqlStringCommand(Sql_FreightInfo_UpdateIsExpired);

            this._dataBase.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, FreightInfoId);
            this._dataBase.AddInParameter(dc, "IsExpired", DbType.AnsiStringFixedLength, IsExpired ? "1" : "0");

            return DbHelper.ExecuteSql(dc, this._dataBase) > 0 ? true : false;
        }

        /// <summary>
        /// 获取运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <returns>返回运价信息实体</returns>
        public virtual EyouSoft.Model.TicketStructure.TicketFreightInfo GetModel(string FreightInfoId)
        {
            if (string.IsNullOrEmpty(FreightInfoId))
                return null;

            EyouSoft.Model.TicketStructure.TicketFreightInfo model = new EyouSoft.Model.TicketStructure.TicketFreightInfo();
            model.Id = FreightInfoId;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(Sql_FreightInfo_Select + " where Id = @Id ;");
            strSql.Append(Sql_TicketFreightAddition_Select + " where FreightId = @Id ;");
            DbCommand dc = this._dataBase.GetSqlStringCommand(strSql.ToString());

            this._dataBase.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, FreightInfoId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._dataBase))
            {
                IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> list = GetSelectModel(dr);
                if (list == null || list.Count <= 0)
                    return model;

                model = list[0];
                list.Clear();
                list = null;

                dr.NextResult();

                #region 运价扩展信息赋值

                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("FromType")))
                    {
                        switch (dr["FromType"].ToString())
                        {
                            case "0":  //去程
                                if (!dr.IsDBNull(dr.GetOrdinal("SelfDate")))
                                    model.FromSelfDate = DateTime.Parse(dr["SelfDate"].ToString());
                                model.FromForDay = dr["ForDay"].ToString();
                                if (!dr.IsDBNull(dr.GetOrdinal("ReferPrice")))
                                    model.FromReferPrice = decimal.Parse(dr["ReferPrice"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("ReferRate")))
                                    model.FromReferRate = decimal.Parse(dr["ReferRate"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("SetPrice")))
                                    model.FromSetPrice = decimal.Parse(dr["SetPrice"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("FuelPrice")))
                                    model.FromFuelPrice = decimal.Parse(dr["FuelPrice"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("BuildPrice")))
                                    model.FromBuildPrice = decimal.Parse(dr["BuildPrice"].ToString());
                                break;
                            case "1":  //回程
                                if (!dr.IsDBNull(dr.GetOrdinal("SelfDate")))
                                    model.ToSelfDate = DateTime.Parse(dr["SelfDate"].ToString());
                                model.ToForDay = dr["ForDay"].ToString();
                                if (!dr.IsDBNull(dr.GetOrdinal("ReferPrice")))
                                    model.ToReferPrice = decimal.Parse(dr["ReferPrice"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("ReferRate")))
                                    model.ToReferRate = decimal.Parse(dr["ReferRate"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("SetPrice")))
                                    model.ToSetPrice = decimal.Parse(dr["SetPrice"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("FuelPrice")))
                                    model.ToFuelPrice = decimal.Parse(dr["FuelPrice"].ToString());
                                if (!dr.IsDBNull(dr.GetOrdinal("BuildPrice")))
                                    model.ToBuildPrice = decimal.Parse(dr["BuildPrice"].ToString());
                                break;
                        }
                    }
                }

                #endregion
            }

            return model;
        }

        /// <summary>
        /// 获取供应商的运价信息集合
        /// </summary>
        /// <param name="PageSize">当前页数</param>
        /// <param name="PageIndex">每页条数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引：0/1运价添加时间升/降序；2/3最后修改时间升/降序；4/5套餐类型升/降序；6/7启用状态升/降序；8/9航空公司编号升/降序；</param>
        /// <param name="QueryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, EyouSoft.Model.TicketStructure.QueryTicketFreightInfo QueryTicketFreightInfo)
        {
            IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> list = new List<EyouSoft.Model.TicketStructure.TicketFreightInfo>();
            StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
            string strFiles = " [Id],[CompanyId],(select CompanyName from tbl_CompanyInfo where tbl_CompanyInfo.ID = tbl_TicketFreightInfo.CompanyId) as CompanyName,[RateType],[FreightType],[ProductType],[BuyType],[FlightId],[FlightName],[NoGadHomeCityId],[NoGadHomeCityIdName],[NoGadDestCityId],[NoGadDestCityName],[GadHomeCityId],[GadHomeCityName],[GadDestCityId],[GadDestCityName],[IsEnabled],[IsCheckPNR],[IsExpired],[FreightStartDate],[FreightEndDate],[MaxPCount],[WorkStartTime],[WorkEndTime],[SupplierRemark],[LastUpdateDate],[FreightBuyId],[OperatorId],[IssueTime] ";
            string strOrder = string.Empty;
            switch (OrderIndex)
            {
                case 0: strOrder = " IssueTime asc "; break;
                case 1: strOrder = " IssueTime desc "; break;
                case 2: strOrder = " LastUpdateDate asc "; break;
                case 3: strOrder = " LastUpdateDate desc "; break;
                case 4: strOrder = " BuyType asc "; break;
                case 5: strOrder = " BuyType desc "; break;
                case 6: strOrder = " IsEnabled asc "; break;
                case 7: strOrder = " IsEnabled desc "; break;
                case 8: strOrder = " FlightId asc "; break;
                case 9: strOrder = " FlightId desc "; break;
            }

            if (QueryTicketFreightInfo != null)
            {
                if (!string.IsNullOrEmpty(QueryTicketFreightInfo.SellCompanyId))
                {
                    if (QueryTicketFreightInfo.SellCompanyId != "14ab1cc4-055e-47b2-87b6-eb51c17a2921")//某家供应商能查看所有运价(暂时保留功能)
                        strWhere.AppendFormat(" and CompanyId = '{0}' ", QueryTicketFreightInfo.SellCompanyId);
                }
                if (QueryTicketFreightInfo.FreightType.HasValue)
                    strWhere.AppendFormat(" and FreightType = {0} ", (int)QueryTicketFreightInfo.FreightType);
                if (QueryTicketFreightInfo.HomeCity > 0)
                    strWhere.AppendFormat(" and NoGadHomeCityId = {0} ", QueryTicketFreightInfo.HomeCity);
                if (QueryTicketFreightInfo.DestCity > 0)
                    strWhere.AppendFormat(" and NoGadDestCityId = {0} ", QueryTicketFreightInfo.DestCity);
                if (QueryTicketFreightInfo.AirportId > 0)
                    strWhere.AppendFormat(" and FlightId = {0} ", QueryTicketFreightInfo.AirportId);
            }

            using (IDataReader dr = DbHelper.ExecuteReader(this._dataBase, PageSize, PageIndex, ref RecordCount, "tbl_TicketFreightInfo", "Id", strFiles, strWhere.ToString(), strOrder))
            {
                list = GetSelectModel(dr);
            }

            return list;
        }

        /// <summary>
        /// 获取采购商查看运价信息集合
        /// </summary>
        /// <param name="QueryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketFreightInfoList> GetFreightInfoList(EyouSoft.Model.TicketStructure.QueryTicketFreightInfo QueryTicketFreightInfo)
        {
            List<EyouSoft.Model.TicketStructure.TicketFreightInfoList> list = new List<EyouSoft.Model.TicketStructure.TicketFreightInfoList>();
            System.Xml.XmlAttributeCollection attList = null;
            System.Xml.XmlDocument xml = null;
            System.Xml.XmlNodeList xmlNodeList = null;

            #region 拼接Sql

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select PayPrice,FreightAddition,Id,FlightId,FlightName,CompanyId,CompanyName,ProductType,MaxPCount,SupplierRemark,FreightType,ContactMQ,FreightStartDate,FreightEndDate from view_TicketFreightInfo where IsEnabled = '1' and IsExpired = '0' ");

            if (QueryTicketFreightInfo != null)
            {
                if (QueryTicketFreightInfo.FreightType.HasValue)
                    strSql.AppendFormat(" and FreightType = {0} ", (int)QueryTicketFreightInfo.FreightType);
                if (QueryTicketFreightInfo.HomeCity > 0)
                    strSql.AppendFormat(" and NoGadHomeCityId = {0} ", QueryTicketFreightInfo.HomeCity);
                if (QueryTicketFreightInfo.DestCity > 0)
                    strSql.AppendFormat(" and NoGadDestCityId = {0} ", QueryTicketFreightInfo.DestCity);
                if (QueryTicketFreightInfo.AirportId > 0)
                    strSql.AppendFormat(" and FlightId = {0} ", QueryTicketFreightInfo.AirportId);
            }

            strSql.Append(" order by PayPrice desc ");

            #endregion

            #region 实体赋值

            DbCommand dc = this._dataBase.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._dataBase))
            {
                EyouSoft.Model.TicketStructure.TicketFreightInfo model = null;

                while (dr.Read())
                {
                    if (dr.IsDBNull(dr.GetOrdinal("FlightId")))
                        continue;

                    EyouSoft.Model.TicketStructure.TicketFreightInfoList modelFreightInfoList = list.Find(
                        (EyouSoft.Model.TicketStructure.TicketFreightInfoList tmp) =>
                        {
                            if (int.Parse(dr["FlightId"].ToString()) == tmp.Id)
                                return true;
                            else
                                return false;
                        });

                    modelFreightInfoList = modelFreightInfoList ?? new EyouSoft.Model.TicketStructure.TicketFreightInfoList();
                    if (modelFreightInfoList.Id <= 0)
                    {
                        modelFreightInfoList.FreightInfoList = new List<EyouSoft.Model.TicketStructure.TicketFreightInfo>();
                        list.Add(modelFreightInfoList);  //集合不存在，加入集合中
                    }

                    #region 航空公司信息

                    modelFreightInfoList.Id = int.Parse(dr["FlightId"].ToString());
                    modelFreightInfoList.AirportName = dr["FlightName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("PayPrice")))
                        modelFreightInfoList.AirportPayPrice = modelFreightInfoList.AirportPayPrice + decimal.Parse(dr["PayPrice"].ToString());

                    #endregion

                    #region 运价信息

                    model = new EyouSoft.Model.TicketStructure.TicketFreightInfo();
                    model.Id = dr.GetString(dr.GetOrdinal("Id"));
                    model.FlightId = dr.GetInt32(dr.GetOrdinal("FlightId"));
                    model.FlightName = dr.GetString(dr.GetOrdinal("FlightName"));
                    model.Company.ID = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.Company.CompanyName = dr["CompanyName"].ToString();
                    model.ContactMQ = dr["ContactMQ"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ProductType")))
                        model.ProductType = (EyouSoft.Model.TicketStructure.ProductType)int.Parse(dr.GetByte(dr.GetOrdinal("ProductType")).ToString());
                    model.MaxPCount = dr.GetInt32(dr.GetOrdinal("MaxPCount"));
                    model.SupplierRemark = dr.GetString(dr.GetOrdinal("SupplierRemark"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FreightType")))
                        model.FreightType = (EyouSoft.Model.TicketStructure.FreightType)int.Parse(dr.GetByte(dr.GetOrdinal("FreightType")).ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("FreightStartDate")))
                        model.FreightStartDate = DateTime.Parse(dr["FreightStartDate"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("FreightEndDate")))
                        model.FreightEndDate = DateTime.Parse(dr["FreightEndDate"].ToString());

                    #endregion

                    #region 运价扩展信息

                    if (!string.IsNullOrEmpty(dr["FreightAddition"].ToString()))
                    {
                        xml = new System.Xml.XmlDocument();
                        xml.LoadXml(dr["FreightAddition"].ToString());
                        xmlNodeList = xml.GetElementsByTagName("tbl_TicketFreightAddition");
                        if (xmlNodeList != null)
                        {
                            for (int i = 0; i < xmlNodeList.Count; i++)
                            {
                                if (i > 1)
                                    break;

                                attList = xmlNodeList[i].Attributes;
                                switch (attList["FromType"].Value)
                                {
                                    case "0":  //去程
                                        if (attList["SelfDate"] != null && !string.IsNullOrEmpty(attList["SelfDate"].Value))
                                            model.FromSelfDate = DateTime.Parse(attList["SelfDate"].Value);
                                        if (attList["ForDay"] != null)
                                            model.FromForDay = attList["ForDay"].Value;
                                        if (attList["ReferPrice"] != null && !string.IsNullOrEmpty(attList["ReferPrice"].Value))
                                            model.FromReferPrice = decimal.Parse(attList["ReferPrice"].Value);
                                        if (attList["ReferRate"] != null && !string.IsNullOrEmpty(attList["ReferRate"].Value))
                                            model.FromReferRate = decimal.Parse(attList["ReferRate"].Value);
                                        if (attList["SetPrice"] != null && !string.IsNullOrEmpty(attList["SetPrice"].Value))
                                            model.FromSetPrice = decimal.Parse(attList["SetPrice"].Value);
                                        if (attList["FuelPrice"] != null && !string.IsNullOrEmpty(attList["FuelPrice"].Value))
                                            model.FromFuelPrice = decimal.Parse(attList["FuelPrice"].Value);
                                        if (attList["BuildPrice"] != null && !string.IsNullOrEmpty(attList["BuildPrice"].Value))
                                            model.FromBuildPrice = decimal.Parse(attList["BuildPrice"].Value);
                                        break;
                                    case "1":  //回程
                                        if (attList["SelfDate"] != null && !string.IsNullOrEmpty(attList["SelfDate"].Value))
                                            model.ToSelfDate = DateTime.Parse(attList["SelfDate"].Value);
                                        if (attList["ForDay"] != null)
                                            model.ToForDay = attList["ForDay"].Value;
                                        if (attList["ReferPrice"] != null && !string.IsNullOrEmpty(attList["ReferPrice"].Value))
                                            model.ToReferPrice = decimal.Parse(attList["ReferPrice"].Value);
                                        if (attList["ReferRate"] != null && !string.IsNullOrEmpty(attList["ReferRate"].Value))
                                            model.ToReferRate = decimal.Parse(attList["ReferRate"].Value);
                                        if (attList["SetPrice"] != null && !string.IsNullOrEmpty(attList["SetPrice"].Value))
                                            model.ToSetPrice = decimal.Parse(attList["SetPrice"].Value);
                                        if (attList["FuelPrice"] != null && !string.IsNullOrEmpty(attList["FuelPrice"].Value))
                                            model.ToFuelPrice = decimal.Parse(attList["FuelPrice"].Value);
                                        if (attList["BuildPrice"] != null && !string.IsNullOrEmpty(attList["BuildPrice"].Value))
                                            model.ToBuildPrice = decimal.Parse(attList["BuildPrice"].Value);
                                        break;
                                }
                            }
                        }
                    }
                    if (attList != null) attList.RemoveAll();
                    attList = null;
                    xmlNodeList = null;
                    xml = null;

                    #endregion

                    modelFreightInfoList.FreightInfoList.Add(model);
                }
            }

            #endregion

            if (attList != null) attList.RemoveAll();
            attList = null;
            xmlNodeList = null;
            xml = null;

            return list.OrderByDescending(Item => (Item.AirportPayPrice)).ToList();
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 运价信息实体赋值
        /// </summary>
        /// <param name="dr">IDataReader</param>
        /// <returns>返回运价信息实体</returns>
        private IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> GetSelectModel(IDataReader dr)
        {
            if (dr == null)
                return null;

            IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> list = new List<EyouSoft.Model.TicketStructure.TicketFreightInfo>();
            EyouSoft.Model.TicketStructure.TicketFreightInfo model = null;

            while (dr.Read())
            {
                model = new EyouSoft.Model.TicketStructure.TicketFreightInfo();

                model.Id = dr.GetString(dr.GetOrdinal("Id"));
                if (!dr.IsDBNull(dr.GetOrdinal("BuyType")))
                    model.BuyType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr["BuyType"].ToString());
                model.Company.ID = dr.GetString(dr.GetOrdinal("CompanyId"));
                model.Company.CompanyName = dr["CompanyName"].ToString();
                model.FlightId = dr.GetInt32(dr.GetOrdinal("FlightId"));
                model.FlightName = dr.GetString(dr.GetOrdinal("FlightName"));
                model.FreightBuyId = dr.GetString(dr.GetOrdinal("FreightBuyId"));
                if (!dr.IsDBNull(dr.GetOrdinal("FreightEndDate")))
                    model.FreightEndDate = dr.GetDateTime(dr.GetOrdinal("FreightEndDate"));
                if (!dr.IsDBNull(dr.GetOrdinal("FreightStartDate")))
                    model.FreightStartDate = dr.GetDateTime(dr.GetOrdinal("FreightStartDate"));
                if (!dr.IsDBNull(dr.GetOrdinal("FreightType")))
                    model.FreightType = (EyouSoft.Model.TicketStructure.FreightType)int.Parse(dr["FreightType"].ToString());
                model.GadDestCityId = dr.GetInt32(dr.GetOrdinal("GadDestCityId"));
                model.GadDestCityName = dr.GetString(dr.GetOrdinal("GadDestCityName"));
                model.GadHomeCityId = dr.GetInt32(dr.GetOrdinal("GadHomeCityId"));
                model.GadHomeCityName = dr.GetString(dr.GetOrdinal("GadHomeCityName"));
                model.IsCheckPNR = dr.GetString(dr.GetOrdinal("IsCheckPNR")) == "1" ? true : false;
                model.IsEnabled = dr.GetString(dr.GetOrdinal("IsEnabled")) == "1" ? true : false;
                model.IsExpired = dr.GetString(dr.GetOrdinal("IsExpired")) == "1" ? true : false;
                model.IssueTime = dr.IsDBNull(dr.GetOrdinal("IssueTime")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                if (!dr.IsDBNull(dr.GetOrdinal("LastUpdateDate")))
                    model.LastUpdateDate = dr.GetDateTime(dr.GetOrdinal("LastUpdateDate"));
                model.MaxPCount = dr.GetInt32(dr.GetOrdinal("MaxPCount"));
                model.NoGadDestCityId = dr.GetInt32(dr.GetOrdinal("NoGadDestCityId"));
                model.NoGadDestCityName = dr.GetString(dr.GetOrdinal("NoGadDestCityName"));
                model.NoGadHomeCityIdName = dr.GetString(dr.GetOrdinal("NoGadHomeCityIdName"));
                model.NoGadHomeCityId = dr.GetInt32(dr.GetOrdinal("NoGadHomeCityId"));
                model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                if (!dr.IsDBNull(dr.GetOrdinal("ProductType")))
                    model.ProductType = (EyouSoft.Model.TicketStructure.ProductType)int.Parse(dr.GetByte(dr.GetOrdinal("ProductType")).ToString());
                if (!dr.IsDBNull(dr.GetOrdinal("RateType")))
                    model.RateType = (EyouSoft.Model.TicketStructure.RateType)int.Parse(dr.GetByte(dr.GetOrdinal("RateType")).ToString());
                model.SupplierRemark = dr.GetString(dr.GetOrdinal("SupplierRemark"));
                model.WorkEndTime = dr.GetString(dr.GetOrdinal("WorkEndTime"));
                model.WorkStartTime = dr.GetString(dr.GetOrdinal("WorkStartTime"));

                list.Add(model);
            }

            return list;
        }

        #endregion
    }
}
