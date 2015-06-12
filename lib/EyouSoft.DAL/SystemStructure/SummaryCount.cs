using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 系统数据统计数据访问
    /// </summary>
    /// 创建人:蒋胜蓝  2010-6-23
    /// 修改人:zhengfj 2011-5-17
    /// 修改内容：SetSummaryCount(),GetSummary()
    public class SummaryCount : EyouSoft.Common.DAL.DALBase ,ISummaryCount
    {
        const string SQL_SummaryCount_SELECT = "SELECT FieldName,FieldValue FROM tbl_SysSummaryCount";
        const string SQL_SummaryCount_SetSummaryCount = "UPDATE tbl_SysSummaryCount set FieldValue={0} WHERE FieldName='{1}' ;";
        #region ISummaryCount 成员
        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SummaryCount GetSummary()
        {
            EyouSoft.Model.SystemStructure.SummaryCount model = new EyouSoft.Model.SystemStructure.SummaryCount();
            NameValueCollection setting = new NameValueCollection();
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_SummaryCount_SELECT);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this.SystemStore))
            {
                while (dr.Read())
                {
                    setting.Add(dr.GetString(dr.GetOrdinal("FieldName")),dr.GetString(dr.GetOrdinal("FieldValue")));
                }
            }
            if (!string.IsNullOrEmpty(setting["Buyers"]))
                model.Buyer = int.Parse(setting["Buyers"]);
            if (!string.IsNullOrEmpty(setting["BuyersVirtual"]))
                model.BuyerVirtual = int.Parse(setting["BuyersVirtual"]);
            if (!string.IsNullOrEmpty(setting["Suppliers"]))
                model.Suppliers = int.Parse(setting["Suppliers"]);
            if (!string.IsNullOrEmpty(setting["SuppliersVirtual"]))
                model.SuppliersVirtual = int.Parse(setting["SuppliersVirtual"]);
            if (!string.IsNullOrEmpty(setting["SupplyInfos"]))
                model.SupplyInfos = int.Parse(setting["SupplyInfos"]);
            if (!string.IsNullOrEmpty(setting["SupplyInfosVirtual"]))
                model.SupplyInfosVirtual = int.Parse(setting["SupplyInfosVirtual"]);
            
            if (!string.IsNullOrEmpty(setting["TravelAgency"]))
                model.TravelAgency = int.Parse(setting["TravelAgency"]);
            if (!string.IsNullOrEmpty(setting["Hotel"]))
                model.Hotel = int.Parse(setting["Hotel"]);
            if (!string.IsNullOrEmpty(setting["Sight"]))
                model.Sight = int.Parse(setting["Sight"]);
            if (!string.IsNullOrEmpty(setting["Car"]))
                model.Car = int.Parse(setting["Car"]);
            if (!string.IsNullOrEmpty(setting["Shop"]))
                model.Shop = int.Parse(setting["Shop"]);
            if (!string.IsNullOrEmpty(setting["Goods"]))
                model.Goods = int.Parse(setting["Goods"]);
            if (!string.IsNullOrEmpty(setting["User"]))
                model.User = int.Parse(setting["User"]);
            if (!string.IsNullOrEmpty(setting["Intermediary"]))
                model.Intermediary = int.Parse(setting["Intermediary"]);
            if (!string.IsNullOrEmpty(setting["Route"]))
                model.Route = int.Parse(setting["Route"]);
            if (!string.IsNullOrEmpty(setting["MQUser"]))
                model.MQUser = int.Parse(setting["MQUser"]);
            if (!string.IsNullOrEmpty(setting["TravelAgencyVirtual"]))
                model.TravelAgencyVirtual = int.Parse(setting["TravelAgencyVirtual"]);
            if (!string.IsNullOrEmpty(setting["HotelVirtual"]))
                model.HotelVirtual = int.Parse(setting["HotelVirtual"]);
            if (!string.IsNullOrEmpty(setting["SightVirtual"]))
                model.SightVirtual = int.Parse(setting["SightVirtual"]);
            if (!string.IsNullOrEmpty(setting["CarVirtual"]))
                model.CarVirtual = int.Parse(setting["CarVirtual"]);
            if (!string.IsNullOrEmpty(setting["ShopVirtual"]))
                model.ShopVirtual = int.Parse(setting["ShopVirtual"]);
            if (!string.IsNullOrEmpty(setting["GoodsVirtual"]))
                model.GoodsVirtual = int.Parse(setting["GoodsVirtual"]);
            if (!string.IsNullOrEmpty(setting["UserVirtual"]))
                model.UserVirtual = int.Parse(setting["UserVirtual"]);
            if (!string.IsNullOrEmpty(setting["IntermediaryVirtual"]))
                model.IntermediaryVirtual = int.Parse(setting["IntermediaryVirtual"]);
            if (!string.IsNullOrEmpty(setting["RouteVirtual"]))
                model.RouteVirtual = int.Parse(setting["RouteVirtual"]);
            if (!string.IsNullOrEmpty(setting["CityRouteVirtual"]))
                model.CityRouteVirtual = int.Parse(setting["CityRouteVirtual"]);
            if (!string.IsNullOrEmpty(setting["MQUserVirtual"]))
                model.MQUserVirtual = int.Parse(setting["MQUserVirtual"]);
            if (!string.IsNullOrEmpty(setting["TicketCompany"]))
                model.TicketCompany = int.Parse(setting["TicketCompany"]);
            if (!string.IsNullOrEmpty(setting["TicketCompanyVirtual"]))
                model.TicketCompanyVirtual = int.Parse(setting["TicketCompanyVirtual"]);
            if (!string.IsNullOrEmpty(setting["TicketFreight"]))
                model.TicketFreight = int.Parse(setting["TicketFreight"]);
            if (!string.IsNullOrEmpty(setting["TicketFreightVirtual"]))
                model.TicketFreightVirtual = int.Parse(setting["TicketFreightVirtual"]);
            if (!string.IsNullOrEmpty(setting["Scenic"]))
                model.Scenic = int.Parse(setting["Scenic"]);
            if (!string.IsNullOrEmpty(setting["ScenicVirtual"]))
                model.ScenicVirtual = int.Parse(setting["ScenicVirtual"]);
            return model;
        }
        /// <summary>
        /// 更新统计信息基数值
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetSummaryCount(EyouSoft.Model.SystemStructure.SummaryCount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount,model.TravelAgencyVirtual,"TravelAgencyVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.HotelVirtual, "HotelVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.SightVirtual, "SightVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.CarVirtual, "CarVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.GoodsVirtual, "GoodsVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.UserVirtual, "UserVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.IntermediaryVirtual, "IntermediaryVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.RouteVirtual, "RouteVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.CityRouteVirtual, "CityRouteVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.ShopVirtual, "ShopVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.MQUserVirtual, "MQUserVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.TicketCompanyVirtual, "TicketCompanyVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.TicketFreightVirtual, "TicketFreightVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.BuyerVirtual, "BuyersVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.SuppliersVirtual, "SuppliersVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.SupplyInfosVirtual, "SupplyInfosVirtual");
            strSql.AppendFormat(SQL_SummaryCount_SetSummaryCount, model.ScenicVirtual, "ScenicVirtual");
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strSql.ToString()); 
            return DbHelper.ExecuteSqlTrans(dc, this.SystemStore)>0?true:false;
        }
        #endregion
    }
}
