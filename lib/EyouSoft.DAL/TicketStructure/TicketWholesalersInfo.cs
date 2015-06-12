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
    /// 供应商扩展信息
    /// </summary>
    /// 鲁功源 2010-10-28
    public class TicketWholesalersInfo:DALBase,EyouSoft.IDAL.TicketStructure.ITicketWholesalersInfo
    {
        #region 构造函数
        Database _database = null;
        public TicketWholesalersInfo()
        {
            _database = base.TicketStore;
        }
        #endregion

        #region Sql变量定义
        private const string Sql_TicketWholesalersInfo_Add = "INSERT INTO [tbl_TicketWholesalersInfo]([CompanyId],[ProxyLev],[SuccessRate],[ICPNumber],[ContactName],[ContactTel],[HandleNum],[SubmitNum],[ServicePrice],[WorkStartTime],[WorkEndTime],[DeliveryPrice],[OfficeNumber]) VALUES(@CompanyId,@ProxyLev,@SuccessRate,@ICPNumber,@ContactName,@ContactTel,@HandleNum,@SubmitNum,@ServicePrice,@WorkStartTime,@WorkEndTime,@DeliveryPrice,@OfficeNumber)";
        private const string Sql_TicketWholesalersInfo_GETMODEL = "SELECT [CompanyId],[ProxyLev],[SuccessRate],[ICPNumber],[ContactName],[ContactTel],[RefundTime],[NoRefundTime],[HandleNum],[SubmitNum],[ServicePrice],[WorkStartTime],[WorkEndTime],[DeliveryPrice],[RefundCount],[NoRefundCount],[IntoRatio],[OfficeNumber] from [tbl_TicketWholesalersInfo] where CompanyId=@CompanyId";
        private const string Sql_TicketWholesalersInfo_UPDATE = "UPDATE [tbl_TicketWholesalersInfo] SET [ProxyLev] = @ProxyLev,[ICPNumber] = @ICPNumber,[ServicePrice] = @ServicePrice,[WorkStartTime] = @WorkStartTime,[WorkEndTime] = @WorkEndTime,[DeliveryPrice] = @DeliveryPrice,OfficeNumber=@OfficeNumber,ContactName=@ContactName,ContactTel=@ContactTel WHERE CompanyId=@CompanyId;";
        private const string Sql_TicketWholesalersInfo_SetServicePrice = "UPDATE [tbl_TicketWholesalersInfo] SET ServicePrice=@ServicePrice WHERE CompanyId=@CompanyId";
        private const string Sql_TicketWholesalersInfo_SetIntoRatio = "UPDATE [tbl_TicketWholesalersInfo] SET IntoRatio=@IntoRatio WHERE CompanyId=@CompanyId";
        private const string Sql_CompanyInfo_SetBasicInfo = "Update tbl_CompanyInfo set ContactName=@ContactName,ContactTel=@ContactTel,Remark=@Remark,WebSite=@WebSite where Id=@CompanyId;";
        private const string Sql_TicketWholesalersInfo_GetSuccessRate = "Select [HandleNum],[SubmitNum] from [tbl_TicketWholesalersInfo] where CompanyId=@CompanyId";
        #endregion

        #region private members
        /// <summary>
        /// 获取出票成功率
        /// </summary>
        /// <param name="handleCount">处理数(出票数)</param>
        /// <param name="submitCount">提交数(下单数)</param>
        /// <returns></returns>
        private decimal GetSuccessRate(int handleCount,int submitCount)
        {
            if (submitCount == 0) return 0;

            return (decimal)handleCount / (decimal)submitCount;
        }
        #endregion

        #region ITicketWholesalersInfo 成员
        /// <summary>
        /// 添加供应商扩展信息
        /// </summary>
        /// <param name="model">供应商扩展信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.TicketStructure.TicketWholesalersInfo model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketWholesalersInfo_Add);
            this._database.AddInParameter(dc, "CompanyId", DbType.String, model.CompanyId);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "DeliveryPrice", DbType.Decimal, model.DeliveryPrice);
            this._database.AddInParameter(dc, "HandleNum", DbType.Int32, model.HandleNum);
            this._database.AddInParameter(dc, "ICPNumber", DbType.String, model.ICPNumber);
            this._database.AddInParameter(dc, "ProxyLev", DbType.String, model.ProxyLev);
            this._database.AddInParameter(dc, "ServicePrice", DbType.Decimal, model.ServicePrice);
            this._database.AddInParameter(dc, "SubmitNum", DbType.Int32, model.SubmitNum);
            this._database.AddInParameter(dc, "SuccessRate", DbType.Decimal, model.SuccessRate);
            this._database.AddInParameter(dc, "WorkEndTime", DbType.String, model.WorkEndTime);
            this._database.AddInParameter(dc, "WorkStartTime", DbType.String, model.WorkStartTime);
            this._database.AddInParameter(dc, "OfficeNumber", DbType.AnsiString, model.OfficeNumber);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改供应商扩展信息
        /// </summary>
        /// <param name="model">供应商扩展信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.TicketStructure.TicketWholesalersInfo model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketWholesalersInfo_UPDATE+Sql_CompanyInfo_SetBasicInfo);
            this._database.AddInParameter(dc, "ProxyLev", DbType.String, model.ProxyLev);
            this._database.AddInParameter(dc, "ICPNumber", DbType.String, model.ICPNumber);
            this._database.AddInParameter(dc, "ServicePrice", DbType.Decimal, model.ServicePrice);
            this._database.AddInParameter(dc, "WorkStartTime", DbType.String, model.WorkStartTime);
            this._database.AddInParameter(dc, "WorkEndTime", DbType.String, model.WorkEndTime);
            this._database.AddInParameter(dc, "DeliveryPrice", DbType.Decimal, model.DeliveryPrice);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.CompanyRemark);
            this._database.AddInParameter(dc, "WebSite", DbType.String, model.WebSite);
            this._database.AddInParameter(dc, "OfficeNumber", DbType.StringFixedLength, model.OfficeNumber);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取供应商扩展信息
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <returns>供应商扩展信息实体</returns>
        public virtual EyouSoft.Model.TicketStructure.TicketWholesalersInfo GetModel(string CompanyId)
        {
            EyouSoft.Model.TicketStructure.TicketWholesalersInfo model = null;
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketWholesalersInfo_GETMODEL);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketWholesalersInfo();
                    model.CompanyId = dr[0].ToString();
                    model.ProxyLev =dr.IsDBNull(1)?string.Empty:dr[1].ToString();
                    
                    model.ICPNumber = dr.IsDBNull(3) ? string.Empty : dr[3].ToString();
                    model.ContactName = dr.IsDBNull(4) ? string.Empty : dr[4].ToString();
                    model.ContactTel = dr.IsDBNull(5) ? string.Empty : dr[5].ToString();
                    if (dr.GetInt32(6) > 0 && dr.GetInt32(14)>0)
                        model.RefundAvgTime = ((dr.GetInt32(6) / 60) / dr.GetInt32(14));
                    if (dr.GetInt32(7) > 0 && dr.GetInt32(15) > 0)
                        model.NoRefundAvgTime =((dr.GetInt32(7) / 60) / dr.GetInt32(15));
                    model.HandleNum = dr.GetInt32(8);
                    model.SubmitNum = dr.GetInt32(9);
                    model.ServicePrice = dr.IsDBNull(10) ? 0 : dr.GetDecimal(10);
                    model.WorkStartTime = dr.IsDBNull(11) ? string.Empty: dr[11].ToString();
                    model.WorkEndTime = dr.IsDBNull(12) ? string.Empty : dr[12].ToString();
                    model.DeliveryPrice = dr.IsDBNull(13) ? 0 : dr.GetDecimal(13);
                    model.IntoRatio = dr.IsDBNull(16) ? 0 : decimal.Parse(dr[16].ToString());
                    
                    model.OfficeNumber = dr.IsDBNull(17) ? "" : dr[17].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 设置行程单服务费
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <param name="ServicePrice">行程单服务费</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetServicePrice(string CompanyId, decimal ServicePrice)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketWholesalersInfo_SetServicePrice);
            this._database.AddInParameter(dc, "ServicePrice", DbType.Decimal, ServicePrice);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置指定供应商的分成比例
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <param name="IntoRatio">分成比例</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetIntoRatio(string CompanyId, decimal IntoRatio)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketWholesalersInfo_SetIntoRatio);
            this._database.AddInParameter(dc, "IntoRatio", DbType.Decimal, IntoRatio);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取指定公司的出票成功率
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <returns>出票成功率</returns>
        public virtual decimal GetSuccessRate(string CompanyId)
        {
            decimal SuccessRate = 0;
            DbCommand dc = this._database.GetSqlStringCommand(Sql_TicketWholesalersInfo_GetSuccessRate);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    SuccessRate = this.GetSuccessRate(int.Parse(dr[0].ToString()), int.Parse(dr[1].ToString()));
                }
            }
            return SuccessRate;
        }
        #endregion
    }
}
