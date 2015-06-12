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
    /// 运价套餐购买记录
    /// </summary>
    /// 鲁功源 2010-10-28
    public class FreightBuyLog:DALBase,EyouSoft.IDAL.TicketStructure.IFreightBuyLog
    {
        #region 构造函数
        Database _database = null;
        public FreightBuyLog()
        {
            _database = base.TicketStore;
        }
        #endregion

        #region Sql变量定义
        private const string Sql_TicketFreightBuyLog_INSERT = "INSERT INTO [tbl_TicketFreightBuyLog]([Id],[OrderNo],[CompanyId],[OperatorId],[PackageId],[PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[HomeCityName],[DestCityIds],[DestCityNames],[BuyCount],[StartMonth],[EndMonth],[BuyTime],[SumPrice],[PayState]) VALUES(@Id,dbo.fn_TicketBuyLog_CreateOrderNo(),@CompanyId,@OperatorId,@PackageId,@PackageName,@PackageType,@RateType,@FlightId,@FlightName,@HomeCityId,@HomeCityName,@DestCityIds,@DestCityNames,@BuyCount,@StartMonth,@EndMonth,@BuyTime,@SumPrice,@PayState)";
        private const string Sql_TicketFreightBuyLog_GETMODEL = "SELECT [Id],[OrderNo],[CompanyId],[OperatorId],[PackageId],[PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[HomeCityName],[DestCityIds],[DestCityNames],[BuyCount],[AvailableCount],[StartMonth],[EndMonth],[BuyTime],[SumPrice],[PayState],[PayTime] from [tbl_TicketFreightBuyLog] where Id=@Id";
        private const string Sql_TicketFreightBuyLog_SETPAYINFO = "update [tbl_TicketFreightBuyLog] set PayState=@PayState,PayTime=@PayTime,AvailableCount=(case when @PayState='1' then BuyCount else 0 end) where Id=@Id";
        private const string Sql_TicketFreightBuyLog_GetAvailableInfo = "select Id,AvailableCount,PackageType from [tbl_TicketFreightBuyLog] where PayState='1' and PackageType=1 and AvailableCount>0  {0} order by BuyTime asc,datediff(dd,startMonth,endMonth) asc,AvailableCount asc";
        private const string Sql_TicketFreightBuyLog_GetAvailableNum = "select sum(AvailableCount) from [tbl_TicketFreightBuyLog] where PayState='1' and PackageType=1 ";
        private const string Sql_TicketFreightBuyLog_SetAvailableNum = "Update [tbl_TicketFreightBuyLog] set AvailableCount=AvailableCount{0} where Id='{1}' ;";
        private const string Sql_TicketFreightBuyLog_GetAvailablePackInfo = "select Id,AvailableCount,PackageType from [tbl_TicketFreightBuyLog] where PayState='1' and PackageType<>1 and AvailableCount>0 {0} order by BuyTime asc,datediff(dd,startMonth,endMonth) asc";
        private const string Sql_TicketFreightBuyLog_PackBuyLogStatistics = "SELECT max(PackageType) as PackType,sum(BuyCount-AvailableCount) as UsedCount,sum(AvailableCount) as AvailableCount FROM tbl_TicketFreightBuyLog where PayState='1' {0} group by PackageType";
        private const string Sql_TicketFreightBuyLog_GETMODELBYORDERNO = "SELECT [Id],[OrderNo],[CompanyId],[OperatorId],[PackageId],[PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[HomeCityName],[DestCityIds],[DestCityNames],[BuyCount],[AvailableCount],[StartMonth],[EndMonth],[BuyTime],[SumPrice],[PayState],[PayTime] from [tbl_TicketFreightBuyLog] where OrderNo=@OrderNo";
        #endregion

        #region IFreightBuyLog 成员
        /// <summary>
        /// 添加套餐购买记录
        /// </summary>
        /// <param name="model">套餐购买记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.TicketStructure.TicketFreightBuyLog model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketFreightBuyLog_INSERT);
            model.Id=Guid.NewGuid().ToString();
            this._database.AddInParameter(dc, "Id", DbType.String, model.Id);
            //this._database.AddInParameter(dc, "OrderNo", DbType.String, model.OrderNo);
            this._database.AddInParameter(dc, "CompanyId", DbType.String, model.CompanyId);
            this._database.AddInParameter(dc, "OperatorId", DbType.String, model.OperatorId);
            this._database.AddInParameter(dc, "PackageId", DbType.Int32, model.PackageId);
            this._database.AddInParameter(dc, "PackageName", DbType.String, model.PackageName);
            this._database.AddInParameter(dc, "PackageType", DbType.Int32, (int)model.PackageType);
            this._database.AddInParameter(dc, "RateType", DbType.Int32, (int)model.RateType);
            this._database.AddInParameter(dc, "FlightId", DbType.Int32, model.FlightId);
            this._database.AddInParameter(dc, "FlightName", DbType.String, model.FlightName);
            this._database.AddInParameter(dc, "HomeCityId", DbType.Int32, model.HomeCityId);
            this._database.AddInParameter(dc, "HomeCityName", DbType.String, model.HomeCityName);
            this._database.AddInParameter(dc, "DestCityIds", DbType.String, model.DestCityIds);
            this._database.AddInParameter(dc, "DestCityNames", DbType.String, model.DestCityNames);
            this._database.AddInParameter(dc, "BuyCount", DbType.Int32, model.PackageType == EyouSoft.Model.TicketStructure.PackageTypes.常规 ? model.BuyCount : (model.DestCityIds.TrimEnd(',').Contains(",") ? model.DestCityIds.TrimEnd(',').Split(',').Length : 0));
            this._database.AddInParameter(dc, "StartMonth", DbType.DateTime, model.StartMonth);
            this._database.AddInParameter(dc, "EndMonth", DbType.DateTime, model.EndMonth);
            this._database.AddInParameter(dc, "BuyTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);
            this._database.AddInParameter(dc, "PayState", DbType.String, model.PayState?"1":"0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 分页获取套餐购买记录
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">供应商编号 =""返回全部</param>
        /// <param name="PackageType">套餐类型 =null返回全部</param>
        /// <param name="RateType">业务类型 =null返回全部</param>
        /// <param name="FlightId">航空公司编号 =0返回全部</param>
        /// <param name="HomeCityId">起始地编号 =0返回全部</param>
        /// <param name="DestCityId">目的地编号 =0返回全部</param>
        /// <param name="StartMonth">开始时间 =null返回全部</param>
        /// <param name="EndMonth">结束时间 =null返回全部</param>
        /// <param name="PayState">支付状态 =null返回全部</param>
        /// <returns>套餐购买记录列表</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketFreightBuyLog> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyId, EyouSoft.Model.TicketStructure.PackageTypes? PackageType, EyouSoft.Model.TicketStructure.RateType? RateType, int FlightId, int HomeCityId, int DestCityId, DateTime? StartMonth, DateTime? EndMonth, bool? PayState)
        {
            IList<EyouSoft.Model.TicketStructure.TicketFreightBuyLog> list =new  List<EyouSoft.Model.TicketStructure.TicketFreightBuyLog>();
            #region 查询参数变量
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            string tableName = "tbl_TicketFreightBuyLog";
            string fields = "[Id],[OrderNo],[CompanyId],[OperatorId],[PackageId],[PackageName],[PackageType],[RateType],[FlightId],[FlightName],[HomeCityId],[HomeCityName],[DestCityIds],[DestCityNames],[BuyCount],[AvailableCount],[StartMonth],[EndMonth],[BuyTime],[SumPrice],[PayState],[PayTime]";
            string primaryKey = "Id";
            string orderByString = " BuyTime Desc ";
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strWhere.AppendFormat(" and CompanyId='{0}' ", CompanyId);
            }
            //套餐类型
            if (PackageType.HasValue)
            {
                strWhere.AppendFormat(" and PackageType={0} ", (int)PackageType.Value);
            }
            //业务类型
            if (RateType.HasValue)
            {
                strWhere.AppendFormat(" and RateType={0} ", (int)RateType.Value);
            }
            //航空公司编号
            if (FlightId > 0)
            {
                strWhere.AppendFormat(" and FlightId={0} ", FlightId);
            }
            //起始地编号
            if (HomeCityId > 0)
            {
                strWhere.AppendFormat(" and charindex('{0}',HomeCityId)>0 ", HomeCityId);
            }
            //开始时间
            if (StartMonth.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',StartMonth)>-1 ", StartMonth.Value.ToString());
            }
            //结束时间
            if (EndMonth.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,EndMonth,'{0}')>-1 ", EndMonth.Value.ToString());
            }
            //支付状态
            if (PayState.HasValue)
            {
                strWhere.AppendFormat(" and PayState='{0}' ", PayState.Value ? "1" : "0");
            }
            #endregion

            #region 列表赋值
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = new EyouSoft.Model.TicketStructure.TicketFreightBuyLog();
                    model.Id = dr.IsDBNull(0) ? string.Empty : dr[0].ToString();
                    model.OrderNo = dr.IsDBNull(1) ? string.Empty : dr[1].ToString();
                    model.OperatorId = dr.IsDBNull(2) ? string.Empty : dr[2].ToString();
                    model.PackageId = dr.IsDBNull(3) ? 0 : int.Parse(dr.GetByte(3).ToString());
                    model.PackageName = dr[4].ToString();
                    if(!dr.IsDBNull(5))
                        model.PackageType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(5).ToString());
                    if (!dr.IsDBNull(6))
                        model.RateType = (EyouSoft.Model.TicketStructure.RateType)int.Parse(dr.GetByte(6).ToString());
                    model.FlightId = dr.GetInt32(7);
                    model.FlightName = dr.IsDBNull(8) ? string.Empty : dr[8].ToString();
                    model.HomeCityId = dr.GetInt32(9);
                    model.HomeCityName = dr[10].ToString();
                    model.DestCityIds = dr[11].ToString();
                    model.DestCityNames = dr[12].ToString();
                    model.BuyCount = dr.GetInt32(13);
                    model.AvailableCount = dr.GetInt32(14);
                    model.StartMonth = dr.IsDBNull(15) ? DateTime.MinValue : dr.GetDateTime(15);
                    model.EndMonth = dr.IsDBNull(16) ? DateTime.MinValue : dr.GetDateTime(16);
                    model.BuyTime = dr.IsDBNull(17) ? DateTime.MinValue : dr.GetDateTime(17);
                    model.SumPrice = dr.IsDBNull(18) ? 0 : dr.GetDecimal(18);
                    model.PayState = dr[19].ToString() == "1" ? true : false;
                    model.PayTime = dr.IsDBNull(20) ? DateTime.MinValue : dr.GetDateTime(20);
                    list.Add(model);
                    model = null;
                }
            }
            #endregion
            return list;
        }
        /// <summary>
        /// 获取套餐购买记录实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>套餐购买记录实体</returns>
        public virtual EyouSoft.Model.TicketStructure.TicketFreightBuyLog GetModel(string Id)
        {
            EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = null;
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketFreightBuyLog_GETMODEL);
            this._database.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketFreightBuyLog();
                    model.Id = dr.IsDBNull(0) ? string.Empty : dr[0].ToString();
                    model.OrderNo = dr.IsDBNull(1) ? string.Empty : dr[1].ToString();
                    model.CompanyId = dr[2].ToString();
                    model.OperatorId = dr.IsDBNull(3) ? string.Empty : dr[3].ToString();
                    model.PackageId = dr.IsDBNull(4) ? 0 : dr.GetInt32(4);
                    model.PackageName = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.PackageType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(6).ToString());
                    if (!dr.IsDBNull(7))
                        model.RateType = (EyouSoft.Model.TicketStructure.RateType)int.Parse(dr.GetByte(7).ToString());
                    model.FlightId = dr.GetInt32(8);
                    model.FlightName = dr.IsDBNull(9) ? string.Empty : dr[9].ToString();
                    model.HomeCityId = dr.GetInt32(10);
                    model.HomeCityName = dr[11].ToString();
                    model.DestCityIds = dr[12].ToString();
                    model.DestCityNames = dr[13].ToString();
                    model.BuyCount = dr.GetInt32(14);
                    model.AvailableCount = dr.GetInt32(15);
                    model.StartMonth = dr.IsDBNull(16) ? DateTime.MinValue : dr.GetDateTime(16);
                    model.EndMonth = dr.IsDBNull(17) ? DateTime.MinValue : dr.GetDateTime(17);
                    model.BuyTime = dr.IsDBNull(18) ? DateTime.MinValue : dr.GetDateTime(18);
                    model.SumPrice = dr.IsDBNull(19) ? 0 : dr.GetDecimal(19);
                    model.PayState = dr[20].ToString() == "1" ? true : false;
                    model.PayTime = dr.IsDBNull(21) ? DateTime.MinValue : dr.GetDateTime(21);
                }
            }
            return model;
        }
        /// <summary>
        /// 获取套餐购买记录实体
        /// </summary>
        /// <param name="Id">订单编号</param>
        /// <returns>套餐购买记录实体</returns>
        public virtual EyouSoft.Model.TicketStructure.TicketFreightBuyLog GetModelByOrderNo(string OrderNo)
        {
            EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = null;
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketFreightBuyLog_GETMODELBYORDERNO);
            this._database.AddInParameter(dc, "OrderNo", DbType.String, OrderNo);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketFreightBuyLog();
                    model.Id = dr.IsDBNull(0) ? string.Empty : dr[0].ToString();
                    model.OrderNo = dr.IsDBNull(1) ? string.Empty : dr[1].ToString();
                    model.CompanyId = dr[2].ToString();
                    model.OperatorId = dr.IsDBNull(3) ? string.Empty : dr[3].ToString();
                    model.PackageId = dr.IsDBNull(4) ? 0 : dr.GetInt32(4);
                    model.PackageName = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.PackageType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(6).ToString());
                    if (!dr.IsDBNull(7))
                        model.RateType = (EyouSoft.Model.TicketStructure.RateType)int.Parse(dr.GetByte(7).ToString());
                    model.FlightId = dr.GetInt32(8);
                    model.FlightName = dr.IsDBNull(9) ? string.Empty : dr[9].ToString();
                    model.HomeCityId = dr.GetInt32(10);
                    model.HomeCityName = dr[11].ToString();
                    model.DestCityIds = dr[12].ToString();
                    model.DestCityNames = dr[13].ToString();
                    model.BuyCount = dr.GetInt32(14);
                    model.AvailableCount = dr.GetInt32(15);
                    model.StartMonth = dr.IsDBNull(16) ? DateTime.MinValue : dr.GetDateTime(16);
                    model.EndMonth = dr.IsDBNull(17) ? DateTime.MinValue : dr.GetDateTime(17);
                    model.BuyTime = dr.IsDBNull(18) ? DateTime.MinValue : dr.GetDateTime(18);
                    model.SumPrice = dr.IsDBNull(19) ? 0 : dr.GetDecimal(19);
                    model.PayState = dr[20].ToString() == "1" ? true : false;
                    model.PayTime = dr.IsDBNull(21) ? DateTime.MinValue : dr.GetDateTime(21);
                }
            }
            return model;
        }
        /// <summary>
        /// 设置支付信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="PayState">支付状态 true:支付成功 false:支付失败</param>
        /// <param name="PayTime">支付时间</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetPayInfo(string Id, bool PayState, DateTime PayTime)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketFreightBuyLog_SETPAYINFO);
            this._database.AddInParameter(dc, "Id", DbType.String, Id);
            this._database.AddInParameter(dc, "PayState", DbType.String, PayState?"1":"0");
            this._database.AddInParameter(dc, "PayTime", DbType.DateTime, PayTime);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取可用的常规运价套餐列表
        /// </summary>
        /// <param name="StartDate">开始时间</param>
        /// <param name="CompanyId">供应商编号 =""返回全部</param>
        /// <param name="RateType">业务类型 =null返回全部</param>
        /// <param name="HomeCityId">起始地编号</param>
        /// <returns>可用的常规运价套餐列表</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> GetAvailableInfo(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType,int HomeCityId)
        {
            DbCommand dc = null;
            IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list = new List<EyouSoft.Model.TicketStructure.AvailablePackInfo>();

            #region 公用查询参数设置
            StringBuilder strWhere = new StringBuilder();
            //当前时间
            if (StartDate.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',startmonth)<=0 and datediff(dd,'{0}',endmonth)>=0 ", StartDate.Value.ToString());
            }
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strWhere.AppendFormat(" and CompanyId='{0}' ", CompanyId);
            }
            if (RateType.HasValue)
            {
                strWhere.AppendFormat(" and RateType={0} ", (int)RateType.Value);
            }
            #endregion

            #region 可用的非常规[套餐，促销]运价套餐信息集合
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(strWhere.ToString());
            dc = this._database.GetSqlStringCommand(string.Format(Sql_TicketFreightBuyLog_GetAvailablePackInfo, strQuery.Length > 0 ? strQuery.ToString() : ""));
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.TicketStructure.AvailablePackInfo model = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
                    model.BuyId = dr[0].ToString();
                    model.AvailableNum = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.PackType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(2).ToString());
                    list.Add(model);
                    model = null;
                }
            }
            dc = null;
            #endregion

            #region 可用的常规运价套餐信息集合
            dc=this._database.GetSqlStringCommand(string.Format(Sql_TicketFreightBuyLog_GetAvailableInfo,strWhere.Length>0?strWhere.ToString():""));
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.TicketStructure.AvailablePackInfo model = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
                    model.BuyId = dr[0].ToString();
                    model.AvailableNum = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.PackType = (EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr.GetByte(2).ToString());
                    list.Add(model);
                    model = null;
                }
            }
            #endregion

            return list;
        }
        /// <summary>
        /// 获取可用的常规运价套餐总数
        /// </summary>
        /// <param name="StartDate">开始时间</param>
        /// <param name="CompanyId">供应商编号 =""返回全部</param>
        /// <param name="RateType">业务类型 =null返回全部</param>
        /// <returns>可用的常规运价套餐总数</returns>
        public virtual int GetAvailableNum(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType)
        {
            #region 查询参数
            StringBuilder strWhere = new StringBuilder();
            //当前时间
            if (StartDate.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',startmonth)<=0 and datediff(dd,'{0}',endmonth)>=0 ", StartDate.Value.ToString());
            }
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strWhere.AppendFormat(" and CompanyId='{0}' ", CompanyId);
            }
            if (RateType.HasValue)
            {
                strWhere.AppendFormat(" and RateType={0} ", (int)RateType.Value);
            }
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketFreightBuyLog_GetAvailableNum + strWhere.ToString());
            object obj= DbHelper.GetSingle(dc, this._database);
            if (obj != null)
                return int.Parse(obj.ToString());
            else
                return 0;
        }
        /// <summary>
        /// 设置运价套餐可用条数[运价添加完成后执行]
        /// </summary>
        /// <param name="list">运价套餐使用记录列表</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetAvailableNum(IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (EyouSoft.Model.TicketStructure.AvailablePackInfo model in list)
            {
                if(model.AvailableNum!=0)
                    strSql.AppendFormat(Sql_TicketFreightBuyLog_SetAvailableNum, model.AvailableNum>0?(model.AvailableNum*-1).ToString():"+1", model.BuyId);
            }
            if (strSql.Length == 0)
                return true;
            DbCommand dc=this._database.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取供应商的套餐购买记录统计信息
        /// </summary>
        /// <param name="StartDate">启用时间</param>
        /// <param name="CompanyId">购买公司编号</param>
        /// <param name="RateType">业务类型</param>
        /// <returns>套餐购买记录统计信息实体</returns>
        public virtual EyouSoft.Model.TicketStructure.PackBuyLogStatistics GetPackBuyLog(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType)
        {
            EyouSoft.Model.TicketStructure.PackBuyLogStatistics model= new EyouSoft.Model.TicketStructure.PackBuyLogStatistics();
            #region 查询参数
            StringBuilder strWhere = new StringBuilder();
            //当前时间
            if (StartDate.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',startmonth)<=0 and datediff(dd,'{0}',endmonth)>=0 ", StartDate.Value.ToString());
            }
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strWhere.AppendFormat(" and CompanyId='{0}' ", CompanyId);
            }
            if (RateType.HasValue)
            {
                strWhere.AppendFormat(" and RateType={0} ", (int)RateType.Value);
            }
            #endregion
            DbCommand dc = this._database.GetSqlStringCommand(string.Format(Sql_TicketFreightBuyLog_PackBuyLogStatistics, strWhere.Length > 0 ? strWhere.ToString() : ""));
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        switch ((EyouSoft.Model.TicketStructure.PackageTypes)int.Parse(dr[0].ToString()))
                        { 
                            case EyouSoft.Model.TicketStructure.PackageTypes.常规:
                                model.GeneralUsedCount = dr.GetInt32(1);
                                model.GeneralAvailableCount = dr.GetInt32(2);
                                break;
                            case EyouSoft.Model.TicketStructure.PackageTypes.促销:
                                model.SaleUsedCount = dr.GetInt32(1);
                                model.SaleAvailableCount = dr.GetInt32(2);
                                break;
                            case EyouSoft.Model.TicketStructure.PackageTypes.套餐:
                                model.PackageUsedCount = dr.GetInt32(1);
                                model.PackageAvailableCount = dr.GetInt32(2);
                                break;
                        }
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 根据运价启用状态与主键编号，设置购买记录可用数
        /// </summary>
        /// <param name="Id">主键编号[对应运价表中的，套餐购买编号]</param>
        /// <param name="FreightEnabled">运价启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetAvailableByFreightState(string Id, bool FreightEnabled)
        {
            #region SQL拼接
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_TicketFreightBuyLog Set ");
            strSql.AppendFormat(" AvailableCount=(case when '{0}'='1' then AvailableCount-1 else AvailableCount+1 end) ", FreightEnabled ? "1" : "0");
            strSql.AppendFormat(" where id='{0}' and datediff(dd,getdate(),startmonth)<=0 and datediff(dd,getdate(),endmonth)>=0 ",Id);
            #endregion
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSql(dc, this._database)>0;
        }
        #endregion
    }
}
