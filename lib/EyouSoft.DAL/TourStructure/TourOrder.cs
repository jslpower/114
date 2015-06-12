using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 团队订单信息数据访问
    /// </summary>
    /// 周文超 2010-05-11
    public class TourOrder : DALBase, IDAL.TourStructure.ITourOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrder()
        { }

        #region SqlString

        private const string Sql_TourOrder_Del = " exec proc_TourOrder_SetOrderState @OrderId = @ID,@SaveSeatDate = null,@OrderState = 4,@Result = 0; IF EXISTS (SELECT 1 FROM [tbl_TourOrder] WHERE ID = @ID AND OrderState = 4)  DELETE FROM [tbl_TourOrder] WHERE ID = @ID; ";
        private const string Sql_TourOrder_Select = " select [ID],[OrderNo],[TourId],[RouteName],[TourNo],[LeaveDate],[SiteId],[AreaId],[OrderType],[OrderState],[BuyCompanyID],[BuyCompanyName],[ContactName],[ContactTel],[ContactFax],[ContactMQ],[ContactQQ],[OperatorID],[OperatorName],[CompanyID],[PriceStandId],[PersonalPrice],[ChildPrice],[MarketPrice],[AdultNumber],[ChildNumber],[MarketNumber],[PeopleNumber],[OtherPrice],[SaveSeatDate],[OperatorContent],[LeaveTraffic],[SpecialContent],[SumPrice],[SeatList],[LastDate],[LastOperatorID],[IsDelete],[IssueTime],[TourCompanyName],[TourCompanyId],[TourClassId],[SuccessTime],[ExpireTime],[RateType],[OrderScore],[OrderSource] from [tbl_TourOrder] ";

        #endregion

        #region 函数成员

        #region 普通函数成员

        /// <summary>
        /// 新增一条订单信息
        /// </summary>
        /// <param name="model">订单信息实体</param>
        /// <returns>Error：返回1表示传入参数有错误(总人数或者订单来源或者团队ID)-- Error：返回2表示该团队的状态不能收客--Error：返回3、4表示订单人数超过剩余人数-- Error：返回5表示写入订单信息有异常-- Success：返回9表示所有操作成功</returns>
        public virtual int AddTourOrder(EyouSoft.Model.TourStructure.TourOrder model)
        {
            if (model == null || string.IsNullOrEmpty(model.ID))
                return 0;

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_AddTourOrder");

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, model.ID);
            base.TourStore.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            base.TourStore.AddInParameter(dc, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(dc, "TourNo", DbType.String, model.TourNo);
            base.TourStore.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(dc, "LeaveDate", DbType.DateTime, model.LeaveDate);
            base.TourStore.AddInParameter(dc, "OrderType", DbType.String, model.OrderType);
            base.TourStore.AddInParameter(dc, "OrderState", DbType.Byte, (int)model.OrderState);
            base.TourStore.AddInParameter(dc, "BuyCompanyID", DbType.AnsiStringFixedLength, model.BuyCompanyID);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            base.TourStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.TourStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.TourStore.AddInParameter(dc, "ContactFax", DbType.String, model.ContactFax);
            base.TourStore.AddInParameter(dc, "ContactMQ", DbType.String, model.ContactMQ);
            base.TourStore.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactQQ);
            base.TourStore.AddInParameter(dc, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            base.TourStore.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            base.TourStore.AddInParameter(dc, "PriceStandId", DbType.String, model.PriceStandId);
            base.TourStore.AddInParameter(dc, "PersonalPrice", DbType.Decimal, model.PersonalPrice);
            base.TourStore.AddInParameter(dc, "ChildPrice", DbType.Decimal, model.ChildPrice);
            base.TourStore.AddInParameter(dc, "MarketPrice", DbType.Decimal, model.MarketPrice);
            base.TourStore.AddInParameter(dc, "AdultNumber", DbType.Int32, model.AdultNumber);
            base.TourStore.AddInParameter(dc, "ChildNumber", DbType.Int32, model.ChildNumber);
            base.TourStore.AddInParameter(dc, "MarketNumber", DbType.Int32, model.MarketNumber);
            base.TourStore.AddInParameter(dc, "PeopleNumber", DbType.Int32, model.PeopleNumber);
            base.TourStore.AddInParameter(dc, "OtherPrice", DbType.Decimal, model.OtherPrice);
            base.TourStore.AddInParameter(dc, "SaveSeatDate", DbType.DateTime, model.SaveSeatDate);
            base.TourStore.AddInParameter(dc, "OperatorContent", DbType.String, model.OperatorContent);
            base.TourStore.AddInParameter(dc, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            base.TourStore.AddInParameter(dc, "SpecialContent", DbType.String, model.SpecialContent);
            base.TourStore.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);
            base.TourStore.AddInParameter(dc, "SeatList", DbType.String, model.SeatList);
            base.TourStore.AddInParameter(dc, "TourCompanyName", DbType.String, model.TourCompanyName);
            base.TourStore.AddInParameter(dc, "TourCompanyId", DbType.AnsiStringFixedLength, model.TourCompanyId);
            base.TourStore.AddInParameter(dc, "TourClassId", DbType.Byte, (int)model.TourType);
            base.TourStore.AddInParameter(dc, "OrderSource", DbType.Byte, (int)model.OrderSource);
            StringBuilder strSqlRoot = new StringBuilder();
            if (model.TourOrderCustomer != null && model.TourOrderCustomer.Count > 0)
            {
                strSqlRoot.Append("<ROOT>");
                foreach (EyouSoft.Model.TourStructure.TourOrderCustomer modelCus in model.TourOrderCustomer)
                {
                    if (modelCus == null)
                        continue;

                    strSqlRoot.AppendFormat("<OrderCustomer_Add VisitorName='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.VisitorName));
                    strSqlRoot.AppendFormat(" CradType='{0}' ", (int)modelCus.CradType);
                    strSqlRoot.AppendFormat(" CradNumber='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.CradNumber));
                    strSqlRoot.AppendFormat(" Sex='{0}' ", modelCus.Sex ? "1" : "0");
                    strSqlRoot.AppendFormat(" VisitorType='{0}' ", modelCus.VisitorType ? "1" : "0");
                    strSqlRoot.AppendFormat(" ContactTel='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.ContactTel));
                    strSqlRoot.AppendFormat(" Remark='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.Remark));
                    strSqlRoot.AppendFormat(" SiteNo='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.SiteNo));
                    strSqlRoot.Append(" />");
                }
                strSqlRoot.Append("</ROOT>");
            }
            base.TourStore.AddInParameter(dc, "OrderCustomerXML", DbType.String, strSqlRoot.ToString());
            base.TourStore.AddOutParameter(dc, "Result", DbType.Int32, 4);

            #endregion

            DbHelper.RunProcedure(dc, base.TourStore);
            object obj = base.TourStore.GetParameterValue(dc, "Result");
            if (obj.Equals(null))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        /// <summary>
        /// 更新订单信息
        /// </summary>
        /// <param name="model">订单信息实体</param>
        /// <returns>Error：返回1表示传入参数错误（订单ID、订单来源编号、总人数、座位号）-- Error：返回2表示此订单目前的状态不允许修改-- Error：返回3该订单所在的团队的状态不允许增加订单人数-- Error：返回4订单增加的人数大于团队的剩余人数不能修改-- Error：返回5表示团队人数不足，不能继续留位或者完成交易-- Error：返回6订单增加的人数大于团队的实际剩余人数-- Error：返回7表示修改订单的过程中出现异常-- Success：返回9表示所有操作已成功完成</returns>
        public virtual int UpdateTourOrder(EyouSoft.Model.TourStructure.TourOrder model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_UpdateTourOrder");

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, model.ID);
            base.TourStore.AddInParameter(dc, "OrderType", DbType.AnsiStringFixedLength, model.OrderType == 1 ? "1" : "0");
            base.TourStore.AddInParameter(dc, "OrderState", DbType.Byte, (int)model.OrderState);
            base.TourStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.TourStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.TourStore.AddInParameter(dc, "ContactFax", DbType.String, model.ContactFax);
            base.TourStore.AddInParameter(dc, "ContactMQ", DbType.String, model.ContactMQ);
            base.TourStore.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactQQ);
            base.TourStore.AddInParameter(dc, "PriceStandId", DbType.String, model.PriceStandId);
            base.TourStore.AddInParameter(dc, "PersonalPrice", DbType.Decimal, model.PersonalPrice);
            base.TourStore.AddInParameter(dc, "ChildPrice", DbType.Decimal, model.ChildPrice);
            base.TourStore.AddInParameter(dc, "MarketPrice", DbType.Decimal, model.MarketPrice);
            base.TourStore.AddInParameter(dc, "AdultNumber", DbType.Int32, model.AdultNumber);
            base.TourStore.AddInParameter(dc, "ChildNumber", DbType.Int32, model.ChildNumber);
            base.TourStore.AddInParameter(dc, "MarketNumber", DbType.Int32, model.MarketNumber);
            base.TourStore.AddInParameter(dc, "PeopleNumber", DbType.Int32, model.PeopleNumber);
            base.TourStore.AddInParameter(dc, "OtherPrice", DbType.Decimal, model.OtherPrice);
            base.TourStore.AddInParameter(dc, "SaveSeatDate", DbType.DateTime, model.SaveSeatDate);
            base.TourStore.AddInParameter(dc, "OperatorContent", DbType.String, model.OperatorContent);
            base.TourStore.AddInParameter(dc, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            base.TourStore.AddInParameter(dc, "SpecialContent", DbType.String, model.SpecialContent);
            base.TourStore.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);
            base.TourStore.AddInParameter(dc, "SeatList", DbType.String, model.SeatList);
            base.TourStore.AddInParameter(dc, "LastOperatorID", DbType.AnsiStringFixedLength, model.LastOperatorID);
            base.TourStore.AddInParameter(dc, "OrderSource", DbType.Byte, (int)model.OrderSource);
            StringBuilder strSqlRoot = new StringBuilder();
            if (model.TourOrderCustomer != null && model.TourOrderCustomer.Count > 0)
            {
                strSqlRoot.Append("<ROOT>");
                foreach (EyouSoft.Model.TourStructure.TourOrderCustomer modelCus in model.TourOrderCustomer)
                {
                    if (modelCus == null)
                        continue;

                    strSqlRoot.AppendFormat("<OrderCustomer_Add VisitorName='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.VisitorName));
                    strSqlRoot.AppendFormat(" CradType='{0}' ", (int)modelCus.CradType);
                    strSqlRoot.AppendFormat(" CradNumber='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.CradNumber));
                    strSqlRoot.AppendFormat(" Sex='{0}' ", modelCus.Sex ? "1" : "0");
                    strSqlRoot.AppendFormat(" VisitorType='{0}' ", modelCus.VisitorType ? "1" : "0");
                    strSqlRoot.AppendFormat(" ContactTel='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.ContactTel));
                    strSqlRoot.AppendFormat(" Remark='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.Remark));
                    strSqlRoot.AppendFormat(" SiteNo='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(modelCus.SiteNo));
                    strSqlRoot.Append(" />");
                }
                strSqlRoot.Append("</ROOT>");
            }
            base.TourStore.AddInParameter(dc, "OrderCustomerXML", DbType.String, strSqlRoot.ToString());
            base.TourStore.AddOutParameter(dc, "Result", DbType.Int32, 4);

            #endregion

            DbHelper.RunProcedure(dc, base.TourStore);
            object obj = base.TourStore.GetParameterValue(dc, "Result");
            if (obj.Equals(null))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        /// <summary>
        /// 更新订单删除状态(假删除)
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="IsDelete">删除状态</param>
        /// <returns>返回操作是否成功，true操作成功</returns>
        public virtual bool UpdateIsDelete(string OrderId, bool IsDelete)
        {
            if (string.IsNullOrEmpty(OrderId))
                return false;

            string strSql = " exec proc_TourOrder_SetOrderState @OrderId = @ID,@SaveSeatDate = null,@OrderState = 4,@Result = 0; IF EXISTS (SELECT 1 FROM [tbl_TourOrder] WHERE ID = @ID AND OrderState = 4) update [tbl_TourOrder] set [IsDelete] = @IsDelete where [ID] = @ID ";
            DbCommand dc = base.TourStore.GetSqlStringCommand(strSql);

            base.TourStore.AddInParameter(dc, "IsDelete", DbType.AnsiStringFixedLength, IsDelete ? "1" : "0");
            base.TourStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, OrderId);

            int Result = DbHelper.ExecuteSql(dc, base.TourStore);

            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除订单信息(物理删除)
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回是否删除成功，true删除成功</returns>
        public virtual bool DeleteRealityTourOrder(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
                return false;

            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_TourOrder_Del);

            base.TourStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, OrderId);

            int Result = DbHelper.ExecuteSql(dc, base.TourStore);

            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="OrderState">订单状态</param>
        /// <param name="SaveSeatDate">留位过期时间（改为已留位状态时必须传值）</param>
        /// <returns>返回操作是否成功，true操作成功</returns>
        public virtual bool SetTourOrderState(string OrderId, Model.TourStructure.OrderState OrderState, DateTime? SaveSeatDate)
        {
            if (string.IsNullOrEmpty(OrderId) || (OrderState == EyouSoft.Model.TourStructure.OrderState.已留位 && SaveSeatDate.Equals(null)))
                return false;

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_SetOrderState");
            base.TourStore.AddInParameter(dc, "OrderState", DbType.Byte, (int)OrderState);
            base.TourStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, OrderId);
            base.TourStore.AddInParameter(dc, "SaveSeatDate", DbType.DateTime, SaveSeatDate);
            base.TourStore.AddOutParameter(dc, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(dc, base.TourStore);
            object obj = base.TourStore.GetParameterValue(dc, "Result");
            if (obj.Equals(null))
                return false;
            else
                return int.Parse(obj.ToString()) == 9 ? true : false;
        }

        /// <summary>
        ///  根据订单ID获取订单实体(同时获取该订单的游客信息)
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回订单实体</returns>
        public virtual EyouSoft.Model.TourStructure.TourOrder GetOrderModel(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
                return null;

            string strWhere = " select [ID],[CompanyID],[OrderID],[VisitorName],[CradType],[CradNumber],[Sex],[VisitorType],[ContactTel],[Remark],[IssueTime],[SiteNo] FROM [tbl_TourOrderCustomer] where [OrderID] = @OrderID; ";
            strWhere += Sql_TourOrder_Select + " where [ID] = @ID and [IsDelete] = '0' ";

            DbCommand dc = base.TourStore.GetSqlStringCommand(strWhere);

            base.TourStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, OrderId);
            base.TourStore.AddInParameter(dc, "OrderID", DbType.AnsiStringFixedLength, OrderId);

            EyouSoft.Model.TourStructure.TourOrder model = new EyouSoft.Model.TourStructure.TourOrder();

            #region 实体赋值

            List<Model.TourStructure.TourOrderCustomer> AllCustomer = new List<Model.TourStructure.TourOrderCustomer>();
            using (IDataReader dr = base.TourStore.ExecuteReader(dc))
            {
                EyouSoft.Model.TourStructure.TourOrderCustomer CustomerModel = null;
                while (dr.Read())
                {
                    CustomerModel = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                    CustomerModel.ID = dr[0].ToString();
                    CustomerModel.CompanyID = dr[1].ToString();
                    CustomerModel.OrderID = dr[2].ToString();
                    CustomerModel.VisitorName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        CustomerModel.CradType = (Model.TourStructure.CradType)int.Parse(dr[4].ToString());
                    CustomerModel.CradNumber = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                    {
                        if (dr[6].ToString() == "0")
                            CustomerModel.Sex = false;
                        else if (dr[6].ToString() == "1")
                            CustomerModel.Sex = true;
                    }
                    if (dr.IsDBNull(7) || dr[7].ToString() == "0")
                        CustomerModel.VisitorType = false;
                    else
                        CustomerModel.VisitorType = true;
                    CustomerModel.ContactTel = dr[8].ToString();
                    CustomerModel.Remark = dr[9].ToString();
                    if (!dr.IsDBNull(10))
                        model.IssueTime = dr.GetDateTime(10);
                    CustomerModel.SiteNo = dr[11].ToString();

                    AllCustomer.Add(CustomerModel);
                }

                CustomerModel = null;
                dr.NextResult();

                while (dr.Read())
                {
                    model = new Model.TourStructure.TourOrder();
                    model.ID = dr["ID"].ToString();
                    model.OrderNo = dr["OrderNo"].ToString();
                    model.TourId = dr["TourId"].ToString();
                    model.RouteName = dr["RouteName"].ToString();
                    model.TourNo = dr["TourNo"].ToString();
                    if (!string.IsNullOrEmpty(dr["LeaveDate"].ToString()))
                        model.LeaveDate = DateTime.Parse(dr["LeaveDate"].ToString());
                    //if (!string.IsNullOrEmpty(dr["SiteId"].ToString()))
                    //model.SiteId = int.Parse(dr["SiteId"].ToString());
                    if (!string.IsNullOrEmpty(dr["AreaId"].ToString()))
                        model.AreaId = int.Parse(dr["AreaId"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderType"].ToString()))
                        model.OrderType = int.Parse(dr["OrderType"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderState"].ToString()))
                        model.OrderState = (Model.TourStructure.OrderState)int.Parse(dr["OrderState"].ToString());
                    model.BuyCompanyID = dr["BuyCompanyID"].ToString();
                    model.BuyCompanyName = dr["BuyCompanyName"].ToString();
                    model.ContactName = dr["ContactName"].ToString();
                    model.ContactTel = dr["ContactTel"].ToString();
                    model.ContactFax = dr["ContactFax"].ToString();
                    model.ContactMQ = dr["ContactMQ"].ToString();
                    model.ContactQQ = dr["ContactQQ"].ToString();
                    model.OperatorID = dr["OperatorID"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.CompanyID = dr["CompanyID"].ToString();
                    model.PriceStandId = dr["PriceStandId"].ToString();
                    if (!string.IsNullOrEmpty(dr["PersonalPrice"].ToString()))
                        model.PersonalPrice = decimal.Parse(dr["PersonalPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["ChildPrice"].ToString()))
                        model.ChildPrice = decimal.Parse(dr["ChildPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["MarketPrice"].ToString()))
                        model.MarketPrice = decimal.Parse(dr["MarketPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["AdultNumber"].ToString()))
                        model.AdultNumber = int.Parse(dr["AdultNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["ChildNumber"].ToString()))
                        model.ChildNumber = int.Parse(dr["ChildNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["MarketNumber"].ToString()))
                        model.MarketNumber = int.Parse(dr["MarketNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["PeopleNumber"].ToString()))
                        model.PeopleNumber = int.Parse(dr["PeopleNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["OtherPrice"].ToString()))
                        model.OtherPrice = decimal.Parse(dr["OtherPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["SaveSeatDate"].ToString()))
                        model.SaveSeatDate = DateTime.Parse(dr["SaveSeatDate"].ToString());
                    model.OperatorContent = dr["OperatorContent"].ToString();
                    model.LeaveTraffic = dr["LeaveTraffic"].ToString();
                    model.SpecialContent = dr["SpecialContent"].ToString();
                    if (!string.IsNullOrEmpty(dr["SumPrice"].ToString()))
                        model.SumPrice = decimal.Parse(dr["SumPrice"].ToString());
                    model.SeatList = dr["SeatList"].ToString();
                    if (!string.IsNullOrEmpty(dr["LastDate"].ToString()))
                        model.LastDate = DateTime.Parse(dr["LastDate"].ToString());
                    model.LastOperatorID = dr["LastOperatorID"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsDelete"].ToString()))
                    {
                        if (dr["IsDelete"].ToString() == "1")
                            model.IsDelete = true;
                        else
                            model.IsDelete = false;
                    }
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.TourCompanyName = dr["TourCompanyName"].ToString();
                    model.TourCompanyId = dr["TourCompanyId"].ToString();
                    if (!string.IsNullOrEmpty(dr["TourClassId"].ToString()))
                        model.TourType = (Model.TourStructure.TourType)int.Parse(dr["TourClassId"].ToString());
                    if (!string.IsNullOrEmpty(dr["SuccessTime"].ToString()))
                        model.SuccessTime = DateTime.Parse(dr["SuccessTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["ExpireTime"].ToString()))
                        model.ExpireTime = DateTime.Parse(dr["ExpireTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["RateType"].ToString()))
                        model.RateType = (Model.TourStructure.RateType)int.Parse(dr["RateType"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderScore"].ToString()))
                        model.OrderScore = int.Parse(dr["OrderScore"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderSource"].ToString()))
                        model.OrderSource = (Model.TourStructure.TourOrderOperateType)int.Parse(dr["OrderSource"].ToString());

                    model.TourOrderCustomer = AllCustomer;

                    break;
                }
            }

            #endregion

            return model;
        }

        /// <summary>
        /// 获取所有订单信息(不含游客信息)
        /// </summary>
        /// <param name="SellCompanyId">卖家公司ID(批发商)(为null不作条件)</param>
        /// <param name="SellCompanyName">卖家公司名称(批发商)(为null不作条件)</param>
        /// <param name="BuyCompanyId">买家公司ID(零售商)(为null不作条件)</param>
        /// <param name="BuyCompanyName">买家公司名称(零售商)(为null不作条件)</param>
        /// <param name="UserId">用户ID(订单最后操作人，为null不作条件)</param>
        /// <param name="TourId">团队ID(为null不作条件)</param>
        /// <param name="TourNo">团号(为null不作条件)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="CityId">城市ID(小于等于0不作条件)</param>
        /// <param name="AreaId">区域ID(小于等于0不作条件)</param>
        /// <param name="OrderStates">订单状态集合(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <param name="LeaveDateStart">出团开始日期(为null不作条件)</param>
        /// <param name="LeaveDateEnd">出团结束日期(为null不作条件)</param>
        /// <returns>返回订单实体</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrder> GetOrderList(string SellCompanyId, string SellCompanyName
            , string BuyCompanyId, string BuyCompanyName, string UserId, string TourId, string TourNo, string RouteName, int CityId
            , int AreaId, Model.TourStructure.OrderState[] OrderStates, DateTime? StartTime, DateTime? EndTime, DateTime? LeaveDateStart
            , DateTime? LeaveDateEnd)
        {

            #region Sql拼接

            string strWhere = Sql_TourOrder_Select + " where [IsDelete] = '0' ";
            if (!string.IsNullOrEmpty(SellCompanyId))
                strWhere += string.Format(" and [TourCompanyId] = '{0}' ", SellCompanyId);
            if (!string.IsNullOrEmpty(SellCompanyName))
                strWhere += string.Format(" and [TourCompanyName] like '%{0}%' ", SellCompanyName);
            if (!string.IsNullOrEmpty(BuyCompanyId))
                strWhere += string.Format(" and [BuyCompanyID] = '{0}' ", BuyCompanyId);
            if (!string.IsNullOrEmpty(BuyCompanyName))
                strWhere += string.Format(" and [BuyCompanyName] like '%{0}%' ", BuyCompanyName);
            if (!string.IsNullOrEmpty(UserId))
                strWhere += string.Format(" and [LastOperatorID] = '{0}' ", UserId);
            if (!string.IsNullOrEmpty(TourId))
                strWhere += string.Format(" and [TourId] = '{0}' ", TourId);
            if (!string.IsNullOrEmpty(TourNo))
                strWhere += string.Format(" and [TourNo] like '%{0}%' ", TourNo);
            if (!string.IsNullOrEmpty(RouteName))
                strWhere += string.Format(" and [RouteName] like '%{0}%' ", RouteName);
            if (CityId > 0)
            {
                strWhere += string.Format(" and EXISTS (SELECT 1 FROM tbl_SysCityAreaControl WHERE tbl_SysCityAreaControl.AreaId = tbl_TourOrder.AreaId AND CityId = {0} ", CityId);
                strWhere += " ) ";
            }
            if (AreaId > 0)
                strWhere += string.Format(" and [AreaId] = {0} ", AreaId);
            if (OrderStates != null && OrderStates.Length > 0)
            {
                strWhere += " and [OrderState] in (";
                for (int i = 0; i < OrderStates.Length; i++)
                {
                    strWhere += (int)OrderStates[i] + ",";
                }
                strWhere = strWhere.TrimEnd(',');
                strWhere += ") ";
            }
            if (StartTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[IssueTime]) >= 0 ", StartTime);
            if (EndTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[IssueTime]) <= 0 ", EndTime);
            if (LeaveDateStart != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[LeaveDate]) >= 0) ", LeaveDateStart);
            if (LeaveDateEnd != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[LeaveDate]) <= 0) ", LeaveDateEnd);

            strWhere += " order by  [IssueTime] desc  ";

            #endregion

            DbCommand dc = base.TourStore.GetSqlStringCommand(strWhere);

            return GetQueryList(dc);
        }

        /// <summary>
        /// 获取所有订单信息(不含游客信息)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="SellCompanyId">卖家公司ID(批发商)(为null不作条件)</param>
        /// <param name="SellCompanyName">卖家公司名称(批发商)(为null不作条件)</param>
        /// <param name="BuyCompanyId">买家公司ID(零售商)(为null不作条件)</param>
        /// <param name="BuyCompanyName">买家公司名称(零售商)(为null不作条件)</param>
        /// <param name="UserId">用户ID(订单最后操作人，为null不作条件)</param>
        /// <param name="TourId">团队ID(为null不作条件)</param>
        /// <param name="TourNo">团号(为null不作条件)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="CityId">城市ID(小于等于0不作条件)</param>
        /// <param name="AreaId">区域ID(小于等于0不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <param name="LeaveDateStart">出团开始日期(为null不作条件)</param>
        /// <param name="LeaveDateEnd">出团结束日期(为null不作条件)</param>
        /// <param name="OrderSource">订单来源(0 组团下单;1:专线代客预订)</param>
        /// <returns>返回订单实体</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrder> GetOrderList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string SellCompanyId, string SellCompanyName, string BuyCompanyId, string BuyCompanyName, string UserId
            , string TourId, string TourNo, string RouteName, int CityId, int AreaId, Model.TourStructure.OrderState? OrderState
            , DateTime? StartTime, DateTime? EndTime, DateTime? LeaveDateStart, DateTime? LeaveDateEnd, int OrderSource)
        {
            #region sql语句拼接

            string strFiles = " [ID],[OrderNo],[TourId],[RouteName],[TourNo],[LeaveDate],[SiteId],[AreaId],[OrderType],[OrderState],[BuyCompanyID],[BuyCompanyName],[ContactName],[ContactTel],[ContactFax],[ContactMQ],[ContactQQ],[OperatorID],[OperatorName],[CompanyID],[PriceStandId],[PersonalPrice],[ChildPrice],[MarketPrice],[AdultNumber],[ChildNumber],[MarketNumber],[PeopleNumber],[OtherPrice],[SaveSeatDate],[OperatorContent],[LeaveTraffic],[SpecialContent],[SumPrice],[SeatList],[LastDate],[LastOperatorID],[IsDelete],[IssueTime],[TourCompanyName],[TourCompanyId],[TourClassId],[SuccessTime],[ExpireTime],[RateType],[OrderScore],[OrderSource] ";
            string strWhere = " [IsDelete] = '0' ";
            string strOrder = string.Empty;

            if (!string.IsNullOrEmpty(SellCompanyId))
                strWhere += string.Format(" and [TourCompanyId] = '{0}' ", SellCompanyId);
            if (!string.IsNullOrEmpty(SellCompanyName))
                strWhere += string.Format(" and [TourCompanyName] like '%{0}%' ", SellCompanyName);
            if (!string.IsNullOrEmpty(BuyCompanyId))
                strWhere += string.Format(" and [BuyCompanyID] = '{0}' ", BuyCompanyId);
            if (!string.IsNullOrEmpty(BuyCompanyName))
                strWhere += string.Format(" and [BuyCompanyName] like '%{0}%' ", BuyCompanyName);
            if (!string.IsNullOrEmpty(UserId))
                strWhere += string.Format(" and [LastOperatorID] = '{0}' ", UserId);
            if (!string.IsNullOrEmpty(TourId))
                strWhere += string.Format(" and [TourId] = '{0}' ", TourId);
            if (!string.IsNullOrEmpty(TourNo))
                strWhere += string.Format(" and [TourNo] like '%{0}%' ", TourNo);
            if (!string.IsNullOrEmpty(RouteName))
                strWhere += string.Format(" and [RouteName] like '%{0}%' ", RouteName);
            if (CityId > 0)
            {
                strWhere += string.Format(" and EXISTS (SELECT 1 FROM tbl_SysCityAreaControl WHERE tbl_SysCityAreaControl.AreaId = tbl_TourOrder.AreaId AND CityId = {0} ", CityId);
                strWhere += " ) ";
            }
            if (AreaId > 0)
                strWhere += string.Format(" and [AreaId] = {0} ", AreaId);
            if (OrderState != null)
                strWhere += string.Format(" and [OrderState] = {0} ", (int)OrderState);
            if (StartTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[IssueTime]) >= 0 ", StartTime);
            if (EndTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[IssueTime]) <= 0 ", EndTime);
            if (LeaveDateStart != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[LeaveDate]) >= 0 ", LeaveDateStart);
            if (LeaveDateEnd != null)
                strWhere += string.Format(" and datediff(dd,'{0}',[LeaveDate]) <= 0 ", LeaveDateEnd);
            if (OrderSource >= 0)
            {
                strWhere += string.Format(" and OrderSource = {0} ", OrderSource);
            }

            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            #endregion

            return GetQueryList(PageSize, PageIndex, ref RecordCount, strFiles, strWhere, strOrder);
        }

        /// <summary>
        /// 设置订单已成交后更新交易成功时间和交易评价到期日期
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool UpdateTimeByOrderState(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
                return false;

            string strSql = " UPDATE tbl_TourOrder SET SuccessTime = @SuccessTime,ExpireTime = @ExpireTime WHERE ID = @ID ";

            DbCommand dc = base.TourStore.GetSqlStringCommand(strSql);
            base.TourStore.AddInParameter(dc, "SuccessTime", DbType.DateTime, DateTime.Now);
            base.TourStore.AddInParameter(dc, "ExpireTime", DbType.DateTime, DateTime.Now.AddDays(15));
            base.TourStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, OrderId);

            return DbHelper.ExecuteSql(dc, base.TourStore) > 0 ? true : false;
        }

        #endregion

        #region 我的客户

        /// <summary>
        /// 根据公司ID统计订单数
        /// </summary>
        /// <param name="CurrentCompanyId">当前公司ID(必须传值)</param>
        /// <param name="CustomerCompanyId">客户公司ID(必须传值)</param>
        /// <param name="CompanyType">当前公司类型(组团统计买家，专线统计卖家，为null统计所有)</param>
        /// <param name="OrderState">订单状态(为null统计所有)</param>
        /// <returns>订单数</returns>
        public virtual int GetRetailersOrderNumByState(string CurrentCompanyId, string CustomerCompanyId, Model.CompanyStructure.CompanyType? CompanyType, Model.TourStructure.OrderState? OrderState)
        {
            if (string.IsNullOrEmpty(CurrentCompanyId) || string.IsNullOrEmpty(CustomerCompanyId))
                return 0;

            string strWhere = " SELECT count(Id) FROM tbl_TourOrder WHERE IsDelete = '0' ";

            #region Sql拼接

            switch (CompanyType)
            {
                case null:
                    strWhere += string.Format(" and ((BuyCompanyID = '{0}' and TourCompanyId = '{1}') or (BuyCompanyID = '{2}' and TourCompanyId = '{3}')) ", CurrentCompanyId, CustomerCompanyId, CustomerCompanyId, CurrentCompanyId);
                    break;
                case Model.CompanyStructure.CompanyType.专线:
                    strWhere += string.Format(" and (BuyCompanyID = '{0}' and TourCompanyId = '{1}') ", CustomerCompanyId, CurrentCompanyId);
                    break;
                case Model.CompanyStructure.CompanyType.组团:
                    strWhere += string.Format(" and (BuyCompanyID = '{0}' and TourCompanyId = '{1}') ", CurrentCompanyId, CustomerCompanyId);
                    break;
            }
            switch (OrderState)
            {
                case null:
                    strWhere += string.Empty;
                    break;
                default:
                    strWhere += string.Format(" and OrderState = {0} ", (int)OrderState);
                    break;
            }

            #endregion

            DbCommand dc = base.TourStore.GetSqlStringCommand(strWhere);
            object obj = DbHelper.GetSingle(dc, base.TourStore);
            if (obj == null)
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        /// <summary>
        /// 获取所有订单信息(不含游客信息)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:下单时间(IssueTime)升/降序;)</param>
        /// <param name="CurrentCompanyId">当前公司ID(必须传值)</param>
        /// <param name="CustomerCompanyId">客户公司ID(必须传值)</param>
        /// <param name="CompanyType">当前公司类型(组团统计买家，专线统计卖家，为null统计所有)</param>
        /// <param name="OrderState">订单状态(为null统计所有)</param>
        /// <returns>订单信息集合</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrder> GetRetailersOrderList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string CurrentCompanyId, string CustomerCompanyId, Model.CompanyStructure.CompanyType? CompanyType
            , Model.TourStructure.OrderState? OrderState)
        {
            if (string.IsNullOrEmpty(CurrentCompanyId) || string.IsNullOrEmpty(CustomerCompanyId))
                return null;

            #region sql语句拼接

            string strFiles = " [ID],[OrderNo],[TourId],[RouteName],[TourNo],[LeaveDate],[SiteId],[AreaId],[OrderType],[OrderState],[BuyCompanyID],[BuyCompanyName],[ContactName],[ContactTel],[ContactFax],[ContactMQ],[ContactQQ],[OperatorID],[OperatorName],[CompanyID],[PriceStandId],[PersonalPrice],[ChildPrice],[MarketPrice],[AdultNumber],[ChildNumber],[MarketNumber],[PeopleNumber],[OtherPrice],[SaveSeatDate],[OperatorContent],[LeaveTraffic],[SpecialContent],[SumPrice],[SeatList],[LastDate],[LastOperatorID],[IsDelete],[IssueTime],[TourCompanyName],[TourCompanyId],[TourClassId],[SuccessTime],[ExpireTime],[RateType],[OrderScore],[OrderSource] ";
            string strWhere = " [IsDelete] = '0' ";
            string strOrder = string.Empty;

            switch (CompanyType)
            {
                case null:
                    strWhere += string.Format(" and ((BuyCompanyID = '{0}' and TourCompanyId = '{1}') or (BuyCompanyID = '{2}' and TourCompanyId = '{3}')) ", CurrentCompanyId, CustomerCompanyId, CustomerCompanyId, CurrentCompanyId);
                    break;
                case Model.CompanyStructure.CompanyType.专线:
                    strWhere += string.Format(" and (BuyCompanyID = '{0}' and TourCompanyId = '{1}') ", CustomerCompanyId, CurrentCompanyId);
                    break;
                case Model.CompanyStructure.CompanyType.组团:
                    strWhere += string.Format(" and (BuyCompanyID = '{0}' and TourCompanyId = '{1}') ", CurrentCompanyId, CustomerCompanyId);
                    break;
            }
            switch (OrderState)
            {
                case null:
                    strWhere += string.Empty;
                    break;
                default:
                    strWhere += string.Format(" and OrderState = {0} ", (int)OrderState);
                    break;
            }

            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            #endregion

            return GetQueryList(PageSize, PageIndex, ref RecordCount, strFiles, strWhere, strOrder);
        }

        #endregion

        #region 组团函数成员

        /// <summary>
        /// 某个组团社的订单数统计
        /// </summary>
        /// <param name="BuyCompanyId">组团社ID(必须传值)</param>
        /// <param name="SellCompanyId">批发商ID(为null不作条件)</param>
        /// <param name="RouteName">线路区域(为null不作条件)</param>
        /// <param name="AreaId">线路区域ID(小于等于0不作条件)</param>
        /// <param name="LeaveDateStart">出团开始时间(为null不作条件)</param>
        /// <param name="LeaveDateEnd">出团介绍时间(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单介绍时间(为null不作条件)</param>
        /// <returns>组团订单统计实体集合</returns>
        public virtual IList<Model.TourStructure.AreaAndOrderNum> GetAreaAndOrderNum(string BuyCompanyId, string SellCompanyId
            , string RouteName, int AreaId, DateTime? LeaveDateStart, DateTime? LeaveDateEnd, DateTime? StartTime, DateTime? EndTime)
        {
            if (string.IsNullOrEmpty(BuyCompanyId))
                return null;

            IList<Model.TourStructure.AreaAndOrderNum> List = new List<Model.TourStructure.AreaAndOrderNum>();
            StringBuilder strSubWhere = new StringBuilder(" where IsDelete = '0' ");
            strSubWhere.AppendFormat(" and BuyCompanyID = '{0}' ", BuyCompanyId);
            if (!string.IsNullOrEmpty(SellCompanyId))
                strSubWhere.AppendFormat(" and TourCompanyId = '{0}' ", SellCompanyId);
            if (!string.IsNullOrEmpty(RouteName))
                strSubWhere.AppendFormat(" and RouteName like '%{0}%' ", RouteName);
            if (StartTime.HasValue)
                strSubWhere.AppendFormat(" and [IssueTime] >= '{0}' ", StartTime);
            if (EndTime.HasValue)
                strSubWhere.AppendFormat(" and [IssueTime] <= '{0}' ", EndTime);
            if (LeaveDateStart.HasValue)
                strSubWhere.AppendFormat(" and [LeaveDate] >= '{0}' ", LeaveDateStart);
            if (LeaveDateEnd.HasValue)
                strSubWhere.AppendFormat(" and [LeaveDate] <= '{0}' ", LeaveDateEnd);

            StringBuilder strSql = new StringBuilder(" select AreaId  ");
            strSql.Append(" ,(SELECT AreaName FROM tbl_SysArea WHERE tbl_SysArea.ID = tbl_TourOrder.AreaId) AS AreaName ");
            strSql.AppendFormat(", (SELECT count(ID) FROM tbl_TourOrder as a {0} and a.AreaId = tbl_TourOrder.AreaId and OrderState = {1}) AS UntreatedNum ", strSubWhere.ToString(), (int)Model.TourStructure.OrderState.未处理);
            strSql.AppendFormat(", (SELECT count(ID) FROM tbl_TourOrder as b {0} and b.AreaId = tbl_TourOrder.AreaId and OrderState = {1}) AS TreatNum ", strSubWhere.ToString(), (int)Model.TourStructure.OrderState.处理中);
            strSql.AppendFormat(", (SELECT count(ID) FROM tbl_TourOrder as c {0} and c.AreaId = tbl_TourOrder.AreaId and OrderState = {1}) AS SaveSeatNum ", strSubWhere.ToString(), (int)Model.TourStructure.OrderState.已留位);
            strSql.AppendFormat(", (SELECT count(ID) FROM tbl_TourOrder as d {0} and d.AreaId = tbl_TourOrder.AreaId and OrderState = {1}) AS SaveSeatExpiredNum ", strSubWhere.ToString(), (int)Model.TourStructure.OrderState.留位过期);
            strSql.AppendFormat(", (SELECT count(ID) FROM tbl_TourOrder as e {0} and e.AreaId = tbl_TourOrder.AreaId and OrderState = {1}) AS OrdainNum ", strSubWhere.ToString(), (int)Model.TourStructure.OrderState.已成交);
            strSql.AppendFormat(", (SELECT count(ID) FROM tbl_TourOrder as f {0} and f.AreaId = tbl_TourOrder.AreaId and OrderState = {1}) AS NotAcceptedNum ", strSubWhere.ToString(), (int)Model.TourStructure.OrderState.不受理);

            if (AreaId > 0)
                strSubWhere.AppendFormat(" and [AreaId] = {0} ", AreaId);
            strSubWhere.Append(" group by AreaId ");
            strSql.AppendFormat(" from tbl_TourOrder {0} ", strSubWhere.ToString());

            DbCommand dc = base.TourStore.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TourStore))
            {
                Model.TourStructure.AreaAndOrderNum model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.AreaAndOrderNum();
                    if (!dr.IsDBNull(0))
                        model.AreaId = dr.GetInt32(0);
                    model.AreaName = dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.UntreatedNum = dr.GetInt32(2);
                    if (!dr.IsDBNull(3))
                        model.TreatNum = dr.GetInt32(3);
                    if (!dr.IsDBNull(4))
                        model.SaveSeatNum = dr.GetInt32(4);
                    if (!dr.IsDBNull(5))
                        model.SaveSeatExpiredNum = dr.GetInt32(5);
                    if (!dr.IsDBNull(6))
                        model.OrdainNum = dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.NotAcceptedNum = dr.GetInt32(7);

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }

        /// <summary>
        /// 统计下过单的组团社数量
        /// </summary>
        /// <param name="SellCompanyId">批发商ID（为null统计整个平台下）</param>
        /// <returns>组团社数量</returns>
        public virtual int GetRetailersNumByOrder(string SellCompanyId)
        {
            string strSql = " select count(BuyCompanyID) from (SELECT BuyCompanyID FROM tbl_TourOrder {0} GROUP BY BuyCompanyID) a ";
            if (string.IsNullOrEmpty(SellCompanyId))
                strSql = string.Format(strSql, string.Empty);
            else
                strSql = string.Format(strSql, string.Format(" where TourCompanyId = '{0}' ", SellCompanyId));

            DbCommand dc = base.TourStore.GetSqlStringCommand(strSql);

            object obj = DbHelper.GetSingle(dc, base.TourStore);
            if (obj.Equals(null))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        #endregion

        #region 专线统计函数成员

        /// <summary>
        /// 已成交客户统计
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:买家公司升/降序;2/3:预定成功次数升/降序;4/5:预定人数升/降序;6/7:金额升/降序;8/9:留位过期次数升/降序;10/11:不受理次数升/降序;)</param>
        /// <param name="SellCompanyId">卖家公司ID(为null不作条件)</param>
        /// <param name="UserId">用户ID(为null不作条件)</param>
        /// <param name="BuyCompanyName">买家公司名称(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <returns>返回已成交客户统计信息实体集合</returns>
        public virtual IList<Model.TourStructure.OrderStatistics> GetOrderStatistics(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string SellCompanyId, string UserId, string BuyCompanyName, DateTime? StartTime, DateTime? EndTime)
        {
            Dictionary<string, string> ContactNameAndTel = new Dictionary<string, string>();
            IList<Model.TourStructure.OrderStatistics> List = new List<Model.TourStructure.OrderStatistics>();

            #region Sql拼接

            string strWhere = " IsDelete = '0' ";
            string strFiles = " BuyCompanyID ";
            strFiles += ",(select CompanyName,ContactName,ContactTel from tbl_CompanyInfo where tbl_CompanyInfo.Id = tbl_TourOrder.BuyCompanyID for xml auto,root('root')) as ContactNameAndTel ";
            strFiles += ",(select count(ID) from tbl_TourOrder as a where a.BuyCompanyID = tbl_TourOrder.BuyCompanyID and a.OrderState = 5 AND a.IsDelete = '0' {0} ) as OrdainNum ";
            strFiles += ", (select sum(PeopleNumber) from tbl_TourOrder as b where b.BuyCompanyID = tbl_TourOrder.BuyCompanyID and b.OrderState = 5 AND b.IsDelete = '0' {0} ) as OrdainPeopleNum ";
            strFiles += ",(select sum(SumPrice) from tbl_TourOrder as c where c.BuyCompanyID = tbl_TourOrder.BuyCompanyID and c.OrderState = 5 AND c.IsDelete = '0' {0} ) as TotalMoney ";
            strFiles += ", (select count(ID) from tbl_TourOrder as d where d.BuyCompanyID = tbl_TourOrder.BuyCompanyID and d.OrderState = 3 AND d.IsDelete = '0' {0} ) as SaveSeatExpiredNum ";
            strFiles += ",(select count(ID) from tbl_TourOrder as e where e.BuyCompanyID = tbl_TourOrder.BuyCompanyID and e.OrderState = 4 AND e.IsDelete = '0' {0} ) as NotAcceptedNum ";
            strFiles = string.Format(strFiles, string.IsNullOrEmpty(SellCompanyId) ? "" : string.Format("AND TourCompanyId='{0}' ", SellCompanyId));

            string strOrder = string.Empty;

            if (!string.IsNullOrEmpty(SellCompanyId))
            {
                strWhere += string.Format(" and TourCompanyId = '{0}' ", SellCompanyId);
            }
            if (!string.IsNullOrEmpty(UserId))
                strWhere += string.Format(" and LastOperatorID = '{0}' ", UserId);//最后操作人            
            if (StartTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',IssueTime) >= 0 ", StartTime);
            if (EndTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',IssueTime) <= 0 ", EndTime);
            if (!string.IsNullOrEmpty(BuyCompanyName))
                strWhere += string.Format(" and exists (select 1 from tbl_CompanyInfo where tbl_CompanyInfo.Id = tbl_TourOrder.BuyCompanyID and tbl_CompanyInfo.CompanyName like '%{0}%') ", BuyCompanyName);

            strWhere += " GROUP BY BuyCompanyID ";
            switch (OrderIndex)
            {
                case 0: strOrder = " BuyCompanyID asc "; break;
                case 1: strOrder = " BuyCompanyID desc "; break;
                case 2: strOrder = " OrdainNum asc "; break;
                case 3: strOrder = " OrdainNum desc "; break;
                case 4: strOrder = " OrdainPeopleNum asc "; break;
                case 5: strOrder = " OrdainPeopleNum desc "; break;
                case 6: strOrder = " TotalMoney asc "; break;
                case 7: strOrder = " TotalMoney desc "; break;
                case 8: strOrder = " SaveSeatExpiredNum asc "; break;
                case 9: strOrder = " SaveSeatExpiredNum desc "; break;
                case 10: strOrder = " NotAcceptedNum asc "; break;
                case 11: strOrder = " NotAcceptedNum desc "; break;
                default: strOrder = " OrdainNum desc "; break;
            }

            #endregion

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, "tbl_TourOrder", "ID", strFiles, strWhere, strOrder, true))
            {
                Model.TourStructure.OrderStatistics model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.OrderStatistics();
                    model.CompanyId = dr[0].ToString();
                    if (!dr.IsDBNull(2))
                        model.OrdainNum = dr.GetInt32(2);
                    if (!dr.IsDBNull(3))
                        model.OrdainPeopleNum = dr.GetInt32(3);
                    if (!dr.IsDBNull(4))
                        model.TotalMoney = dr.GetDecimal(4);
                    if (!dr.IsDBNull(5))
                        model.SaveSeatExpiredNum = dr.GetInt32(5);
                    if (!dr.IsDBNull(6))
                        model.NotAcceptedNum = dr.GetInt32(6);

                    List.Add(model);
                    ContactNameAndTel.Add(model.CompanyId, dr[1].ToString());
                }
                model = null;
            }

            if (List == null || List.Count <= 0 || ContactNameAndTel == null || ContactNameAndTel.Count <= 0)
                return null;

            System.Xml.XmlAttributeCollection attList = null;
            System.Xml.XmlDocument xml = null;
            System.Xml.XmlNodeList xmlNodeList = null;
            foreach (Model.TourStructure.OrderStatistics model in List)
            {
                if (model == null || string.IsNullOrEmpty(ContactNameAndTel[model.CompanyId]))
                    continue;

                xml = new System.Xml.XmlDocument();
                xml.LoadXml(ContactNameAndTel[model.CompanyId]);
                xmlNodeList = xml.GetElementsByTagName("tbl_CompanyInfo");
                if (xmlNodeList != null)
                {
                    foreach (System.Xml.XmlNode node in xmlNodeList)
                    {
                        attList = node.Attributes;
                        if (attList != null && attList.Count > 0)
                        {
                            model.CompanyName = attList["CompanyName"].Value;
                            model.ContactName = attList["ContactName"].Value;
                            model.ContactTel = attList["ContactTel"].Value;

                            break;
                        }
                    }
                }
            }
            if (attList != null) attList.RemoveAll();
            attList = null;
            xml = null;
            if (ContactNameAndTel != null) ContactNameAndTel.Clear();
            ContactNameAndTel = null;
            xmlNodeList = null;

            #endregion

            return List;
        }

        /// <summary>
        /// 获取历史订单及其人数
        /// </summary>
        /// <param name="CompanyId">批发商ID(为null不作条件)</param>
        /// <param name="AreaIds">线路区域ID集合(为null不作条件)</param>
        /// <param name="EndTime">日期截止时间(出团日期小于等于此时间的为历史订单，为null取当前时间)</param>
        /// <returns>索引0：历史订单数；索引1：历史订单成人数之和；索引2： 历史订单儿童数之和；</returns>
        public virtual IList<int> GetHistoryOrderNum(string CompanyId, string AreaIds, DateTime? EndTime)
        {
            IList<int> List = new List<int>();
            //string strWhere = " SELECT Count(Id) as OrderNum,sum(AdultNumber) as AdultNumber,sum(ChildNumber) as ChildNumber FROM tbl_TourOrder WHERE IsDelete = '0' ";
            //if (!string.IsNullOrEmpty(CompanyId))
            //    strWhere += string.Format(" and TourCompanyId = '{0}' ", CompanyId);
            //if (!string.IsNullOrEmpty(AreaIds))
            //    strWhere += string.Format(" and AreaId in ({0}) ", AreaIds);
            //if (EndTime == null)
            //    EndTime = DateTime.Now;
            //strWhere += string.Format(" and datediff(dd,LeaveDate,'{0}') >= 0 ", ((DateTime)EndTime).ToShortDateString());
            //DbCommand dc = base.TourStore.GetSqlStringCommand(strWhere);

            EndTime = EndTime ?? DateTime.Now;
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT COUNT(*) AS OrderNum,SUM(A.AdultNumber) AS AdultNumber,SUM(A.ChildNumber) AS ChildNumber ");
            cmdText.Append(" FROM tbl_TourOrder AS A INNER JOIN tbl_TourList AS B ");
            cmdText.AppendFormat(" ON A.TourId=B.Id AND B.ComeBackDate <='{0}' ", ((DateTime)EndTime).ToShortDateString());
            cmdText.Append(" WHERE A.IsDelete = '0' ");

            if (!string.IsNullOrEmpty(CompanyId))
            {
                cmdText.AppendFormat(" AND A.TourCompanyId='{0}' ", CompanyId);
            }

            if (!string.IsNullOrEmpty(AreaIds))
            {
                cmdText.AppendFormat(" AND A.AreaId in ({0})", AreaIds);
            }

            DbCommand dc = base.TourStore.GetSqlStringCommand(cmdText.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TourStore))
            {
                if (dr.Read())
                {
                    List.Insert(0, dr.GetInt32(0));
                    if (dr.IsDBNull(1))
                        List.Insert(1, 0);
                    else
                        List.Insert(1, dr.GetInt32(1));
                    if (dr.IsDBNull(2))
                        List.Insert(2, 0);
                    else
                        List.Insert(2, dr.GetInt32(2));
                }
            }
            return List;
        }

        #endregion

        #region 运营后台统计函数成员

        /// <summary>
        /// 统计每月每个订单状态的订单数量的实体(flash图表统计用)
        /// </summary>
        /// <param name="StartTime">开始时间(必须传值)</param>
        /// <param name="EndTime">结束时间(必须传值)</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth> GetTourOrderStatisticsByMonth(DateTime StartTime
            , DateTime EndTime)
        {
            IList<EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth> List = new List<EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth>();
            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_GetTourOrderStatisticsByMonth");
            base.TourStore.AddInParameter(dc, "StartTime", DbType.DateTime, StartTime);
            base.TourStore.AddInParameter(dc, "EndTime", DbType.DateTime, EndTime);

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TourStore))
            {
                EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth();
                    model.CurrYearAndMonth = dr["PlotTime"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("UntreatedNum")))
                        model.UntreatedNum = int.Parse(dr["UntreatedNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("TreatNum")))
                        model.TreatNum = int.Parse(dr["TreatNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("SaveSeatNum")))
                        model.SaveSeatNum = int.Parse(dr["SaveSeatNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("SaveSeatExpiredNum")))
                        model.SaveSeatExpiredNum = int.Parse(dr["SaveSeatExpiredNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("NotAcceptedNum")))
                        model.NotAcceptedNum = int.Parse(dr["NotAcceptedNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("OrdainNum")))
                        model.OrdainNum = int.Parse(dr["OrdainNum"].ToString());

                    List.Add(model);
                }
                model = null;
            }

            #endregion

            return List;
        }

        /// <summary>
        /// 获取Top零售商预定
        /// </summary>
        /// <param name="TopNum">Top数量</param>
        /// <param name="BuyCompanyName">零售商名称(为null或者空不作条件)</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <param name="OrderIndex">排序索引(0/1:预定次数升/降序;2/3:预定人数升/降序;)</param>
        /// <returns>返回已成交客户统计信息实体集合</returns>
        public virtual IList<Model.TourStructure.OrderStatistics> GetOrderStatistics(int TopNum, string BuyCompanyName
            , string ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime, Model.TourStructure.OrderState? OrderState, int OrderIndex)
        {
            IList<Model.TourStructure.OrderStatistics> List = new List<Model.TourStructure.OrderStatistics>();

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_GetRetailersStatistics");
            base.TourStore.AddInParameter(dc, "TopNum", DbType.Int32, TopNum);
            base.TourStore.AddInParameter(dc, "PageSize", DbType.Int32, 0);
            base.TourStore.AddInParameter(dc, "PageIndex", DbType.Int32, 0);
            base.TourStore.AddInParameter(dc, "OrderIndex", DbType.Int32, OrderIndex);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, BuyCompanyName);
            base.TourStore.AddInParameter(dc, "ManagerUserCityIds", DbType.String, ManagerUserCityIds);
            base.TourStore.AddInParameter(dc, "StartTime", DbType.DateTime, StartTime);
            base.TourStore.AddInParameter(dc, "EndTime", DbType.DateTime, EndTime);
            base.TourStore.AddInParameter(dc, "OrderState", DbType.Byte, OrderState);

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TourStore))
            {
                Model.TourStructure.OrderStatistics model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.OrderStatistics();
                    model.CompanyId = dr["BuyCompanyID"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("OrdainNum")))
                        model.OrdainNum = int.Parse(dr["OrdainNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("OrdainPeopleNum")))
                        model.OrdainPeopleNum = int.Parse(dr["OrdainPeopleNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("SumPrice")))
                        model.TotalMoney = decimal.Parse(dr["SumPrice"].ToString());
                    model.OrderState = OrderState;

                    List.Add(model);
                }
                model = null;
            }

            #endregion

            return List;
        }

        /// <summary>
        /// 零售商预定情况统计
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:买家公司升/降序;2/3:预定成功次数升/降序;4/5:预定人数升/降序;)</param>
        /// <param name="BuyCompanyName">零售商公司名称(为null或者空不作条件)</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="OrderState">订单状态(为null不作条件)</param>
        /// <returns>返回已成交客户统计信息实体集合</returns>
        public virtual IList<Model.TourStructure.OrderStatistics> GetOrderStatistics(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string BuyCompanyName, string ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime
            , Model.TourStructure.OrderState? OrderState)
        {
            IList<Model.TourStructure.OrderStatistics> List = new List<Model.TourStructure.OrderStatistics>();

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_GetRetailersStatistics");
            base.TourStore.AddInParameter(dc, "TopNum", DbType.Int32, -1);
            base.TourStore.AddInParameter(dc, "PageSize", DbType.Int32, PageSize);
            base.TourStore.AddInParameter(dc, "PageIndex", DbType.Int32, PageIndex);
            base.TourStore.AddInParameter(dc, "OrderIndex", DbType.Int32, OrderIndex);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, BuyCompanyName);
            base.TourStore.AddInParameter(dc, "ManagerUserCityIds", DbType.String, ManagerUserCityIds);
            base.TourStore.AddInParameter(dc, "StartTime", DbType.DateTime, StartTime);
            base.TourStore.AddInParameter(dc, "EndTime", DbType.DateTime, EndTime);
            base.TourStore.AddInParameter(dc, "OrderState", DbType.Byte, OrderState);

            #region 实体赋值

            using (IDataReader dr = DbHelper.RunReaderProcedure(dc, base.TourStore))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        RecordCount = dr.GetInt32(0);
                }

                dr.NextResult();

                Model.TourStructure.OrderStatistics model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.OrderStatistics();
                    model.CompanyId = dr["BuyCompanyID"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("OrdainNum")))
                        model.OrdainNum = int.Parse(dr["OrdainNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("OrdainPeopleNum")))
                        model.OrdainPeopleNum = int.Parse(dr["OrdainPeopleNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("SumPrice")))
                        model.TotalMoney = decimal.Parse(dr["SumPrice"].ToString());
                    model.OrderState = OrderState;

                    List.Add(model);
                }
                model = null;
            }

            #endregion

            return List;
        }

        /// <summary>
        /// 公司登录情况统计
        /// </summary>
        /// <param name="TopNum">top条数</param>
        /// <param name="CompanyName">零售商公司名称</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CompanyType">公司类型</param>
        /// <param name="OrderIndex">排序索引(0/1:登录次数升/降序;2/3:访问数量升/降序;)</param>
        /// <returns>返回公司登录统计实体集合</returns>
        public virtual IList<Model.TourStructure.RetailLoginStatistics> GetRetailLoginStatistics(int TopNum, string CompanyName
            , string ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime, Model.CompanyStructure.CompanyType? CompanyType
            , int OrderIndex)
        {
            IList<Model.TourStructure.RetailLoginStatistics> List = new List<Model.TourStructure.RetailLoginStatistics>();

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_GetRetailersLoginStatistics");
            base.TourStore.AddInParameter(dc, "TopNum", DbType.Int32, TopNum);
            base.TourStore.AddInParameter(dc, "PageSize", DbType.Int32, 0);
            base.TourStore.AddInParameter(dc, "PageIndex", DbType.Int32, 0);
            base.TourStore.AddInParameter(dc, "OrderIndex", DbType.Int32, OrderIndex);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, CompanyName);
            base.TourStore.AddInParameter(dc, "ManagerUserCityIds", DbType.String, ManagerUserCityIds);
            base.TourStore.AddInParameter(dc, "StartTime", DbType.DateTime, StartTime);
            base.TourStore.AddInParameter(dc, "EndTime", DbType.DateTime, EndTime);
            base.TourStore.AddInParameter(dc, "CompanyType", DbType.Byte, (int)CompanyType);

            #region 实体赋值

            using (IDataReader dr = DbHelper.RunReaderProcedure(dc, base.TourStore))
            {
                Model.TourStructure.RetailLoginStatistics model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.RetailLoginStatistics();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("VisitNum")))
                        model.VisitNum = int.Parse(dr["VisitNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("LoginNum")))
                        model.LoginNum = int.Parse(dr["LoginNum"].ToString());

                    List.Add(model);
                }
                model = null;
            }

            #endregion

            return List;
        }

        /// <summary>
        /// 公司登录情况统计
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:登录次数升/降序;2/3:访问数量升/降序;)</param>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CompanyType">公司类型</param>
        /// <returns>返回公司登录统计实体集合</returns>
        public virtual IList<Model.TourStructure.RetailLoginStatistics> GetRetailLoginStatistics(int PageSize, int PageIndex
            , ref int RecordCount, int OrderIndex, string CompanyName, string ManagerUserCityIds, DateTime? StartTime, DateTime? EndTime
            , Model.CompanyStructure.CompanyType? CompanyType)
        {
            IList<Model.TourStructure.RetailLoginStatistics> List = new List<Model.TourStructure.RetailLoginStatistics>();

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_GetRetailersLoginStatistics");
            base.TourStore.AddInParameter(dc, "TopNum", DbType.Int32, 0);
            base.TourStore.AddInParameter(dc, "PageSize", DbType.Int32, PageSize);
            base.TourStore.AddInParameter(dc, "PageIndex", DbType.Int32, PageIndex);
            base.TourStore.AddInParameter(dc, "OrderIndex", DbType.Int32, OrderIndex);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, CompanyName);
            base.TourStore.AddInParameter(dc, "ManagerUserCityIds", DbType.String, ManagerUserCityIds);
            base.TourStore.AddInParameter(dc, "StartTime", DbType.DateTime, StartTime);
            base.TourStore.AddInParameter(dc, "EndTime", DbType.DateTime, EndTime);
            base.TourStore.AddInParameter(dc, "CompanyType", DbType.Byte, (int)CompanyType);

            #region 实体赋值

            using (IDataReader dr = DbHelper.RunReaderProcedure(dc, base.TourStore))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        RecordCount = dr.GetInt32(0);
                }

                dr.NextResult();

                Model.TourStructure.RetailLoginStatistics model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.RetailLoginStatistics();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("VisitNum")))
                        model.VisitNum = int.Parse(dr["VisitNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("LoginNum")))
                        model.LoginNum = int.Parse(dr["LoginNum"].ToString());

                    List.Add(model);
                }
                model = null;
            }

            #endregion

            return List;
        }

        /// <summary>
        /// 返回公司登录明细(登录时间、登录IP数组)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:登录时间升/降序;)</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="StartTime">登录开始时间</param>
        /// <param name="EndTime">登录结束时间</param>
        /// <param name="LoginTime">登录时间数组</param>
        /// <param name="LoginIP">登录IP数组</param>
        public virtual void GetRetailLoginList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string CompanyId
            , DateTime? StartTime, DateTime? EndTime, ref IList<DateTime?> LoginTime, ref IList<string> LoginIP)
        {
            #region Sql拼接

            bool IsWhere = false;
            string strWhere = string.Empty;
            string strFiles = " EventIP,EventTime ";
            string strOrder = string.Empty;
            switch (OrderIndex)
            {
                case 0: strOrder = " EventTime asc "; break;
                case 1: strOrder = " EventTime desc "; break;
                default: strOrder = " EventTime desc "; break;
            }
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strWhere += string.Format(" CompanyId = '{0}' ", CompanyId);
                IsWhere = true;
            }
            if (StartTime != null)
            {
                if (IsWhere)
                    strWhere += " and ";

                strWhere += string.Format(" datediff(dd,'{0}',EventTime) >= 0 ", StartTime);
                IsWhere = true;
            }
            if (EndTime != null)
            {
                if (IsWhere)
                    strWhere += " and ";

                strWhere += string.Format(" datediff(dd,'{0}',EventTime) <= 0 ", EndTime);
            }

            #endregion

            #region 返回数组赋值

            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, "tbl_LogUserLogin", "EventID", strFiles, strWhere, strOrder))
            {
                if (LoginTime == null)
                    LoginTime = new List<DateTime?>();
                if (LoginIP == null)
                    LoginIP = new List<string>();
                LoginTime.Clear();
                LoginIP.Clear();
                while (dr.Read())
                {
                    LoginIP.Add(dr[0].ToString());
                    if (dr.IsDBNull(1))
                        LoginTime.Add(null);
                    else
                        LoginTime.Add(dr.GetDateTime(1));
                }
            }

            #endregion
        }

        /// <summary>
        /// 根据订单状态获取订单数
        /// </summary>
        /// <param name="AreaIds">区域集合(为null不作条件，逗号分割模式用户直接in)</param>
        /// <param name="CompanyId">公司ID(专线，为null不作条件)</param>
        /// <param name="UserId">当前用户ID</param>
        /// <param name="StartTime">下单开始时间(订单IssueTime为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单IssueTime为null不作条件)</param>
        /// <returns>返回订单数集合(索引对应订单状态枚举)</returns>
        public virtual IList<int> GetOrderNumByState(string AreaIds, string CompanyId, string UserId, DateTime? StartTime, DateTime? EndTime)
        {
            IList<int> List = new List<int>();

            #region Sql拼接

            string strWhere = " select count(*) from tbl_TourOrder WHERE IsDelete = '0' {0} {1} ";
            string strSubWhere = string.Empty;

            if (!string.IsNullOrEmpty(AreaIds))
            {
                strSubWhere += string.Format(" and AreaId in ({0}) ", AreaIds);
            }
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strSubWhere = string.Format(" AND TourCompanyId = '{0}' ", CompanyId);
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSubWhere = string.Format(" AND LastOperatorID = '{0}' ", UserId);
            }
            if (StartTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',IssueTime) >= 0 ", StartTime);
            if (EndTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',IssueTime) <= 0 ", EndTime);

            string strSql = " select ";
            strSql += " ( " + string.Format(strWhere, " and OrderState = " + (int)Model.TourStructure.OrderState.未处理, strSubWhere) + " ) as UntreatedNum ";
            strSql += ", ( " + string.Format(strWhere, " and OrderState = " + (int)Model.TourStructure.OrderState.处理中, strSubWhere) + " ) as TreatNum ";
            strSql += ", ( " + string.Format(strWhere, " and OrderState = " + (int)Model.TourStructure.OrderState.已留位, strSubWhere) + " ) as SaveSeatNum ";
            strSql += ", ( " + string.Format(strWhere, " and OrderState = " + (int)Model.TourStructure.OrderState.留位过期, strSubWhere) + " ) as SaveSeatExpiredNum ";
            strSql += ", ( " + string.Format(strWhere, " and OrderState = " + (int)Model.TourStructure.OrderState.不受理, strSubWhere) + " ) as NotAcceptedNum ";
            strSql += ", ( " + string.Format(strWhere, " and OrderState = " + (int)Model.TourStructure.OrderState.已成交, strSubWhere) + " ) as OrdainNum ";

            #endregion

            DbCommand dc = base.TourStore.GetSqlStringCommand(strSql);

            #region 集合赋值

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TourStore))
            {
                if (dr.Read())
                {
                    if (dr.IsDBNull(0))
                        List.Insert((int)Model.TourStructure.OrderState.未处理, 0);
                    else
                        List.Insert((int)Model.TourStructure.OrderState.未处理, dr.GetInt32(0));

                    if (dr.IsDBNull(1))
                        List.Insert((int)Model.TourStructure.OrderState.处理中, 0);
                    else
                        List.Insert((int)Model.TourStructure.OrderState.处理中, dr.GetInt32(1));

                    if (dr.IsDBNull(2))
                        List.Insert((int)Model.TourStructure.OrderState.已留位, 0);
                    else
                        List.Insert((int)Model.TourStructure.OrderState.已留位, dr.GetInt32(2));

                    if (dr.IsDBNull(3))
                        List.Insert((int)Model.TourStructure.OrderState.留位过期, 0);
                    else
                        List.Insert((int)Model.TourStructure.OrderState.留位过期, dr.GetInt32(3));

                    if (dr.IsDBNull(4))
                        List.Insert((int)Model.TourStructure.OrderState.不受理, 0);
                    else
                        List.Insert((int)Model.TourStructure.OrderState.不受理, dr.GetInt32(4));

                    if (dr.IsDBNull(5))
                        List.Insert((int)Model.TourStructure.OrderState.已成交, 0);
                    else
                        List.Insert((int)Model.TourStructure.OrderState.已成交, dr.GetInt32(5));
                }
            }

            #endregion

            return List;
        }

        /// <summary>
        /// 获取批发商行为分析列表
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:登录时间升/降序;2/3:成交订单升/降序;4/5:留位订单升/降序;6/7:留位过期订单升/降序;8/9:不受理订单升/降序;10/11:登录次数升/降序;12/13:被查看次数升/降序;)</param>
        /// <param name="SellCompanyName">批发商名称(为null不作条件)</param>
        /// <param name="ManagerUserCityIds">管理员用户城市区域Id集合(为null或者空不作条件)</param>
        /// <param name="SellCityId">销售城市ID(小于等于0不作条件，且售城市ID小于等于0,那么区域ID不论为何值均不作条件)</param>
        /// <param name="AreaId">区域ID(小于等于0不作条件，且售城市ID小于等于0,那么区域ID不论为何值均不作条件)</param>
        /// <param name="StartTime">下单开始时间(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(为null不作条件)</param>
        /// <returns>返回批发商行为分析实体集合</returns>
        public virtual IList<Model.TourStructure.WholesalersStatistics> GetWholesalersStatistics(int PageSize, int PageIndex
            , ref int RecordCount, int OrderIndex, string SellCompanyName, string ManagerUserCityIds, int SellCityId, int AreaId
            , DateTime? StartTime, DateTime? EndTime)
        {
            Dictionary<string, string> AreaIdAndProduceNum = new Dictionary<string, string>();
            IList<Model.TourStructure.WholesalersStatistics> List = new List<Model.TourStructure.WholesalersStatistics>();

            #region 实体赋值

            DbCommand dc = base.TourStore.GetStoredProcCommand("proc_TourOrder_GetWholesalersStatistics");
            base.TourStore.AddInParameter(dc, "PageSize", DbType.Int32, PageSize);
            base.TourStore.AddInParameter(dc, "PageIndex", DbType.Int32, PageIndex);
            base.TourStore.AddInParameter(dc, "OrderIndex", DbType.Int32, OrderIndex);
            base.TourStore.AddInParameter(dc, "SellCompanyName", DbType.String, SellCompanyName);
            base.TourStore.AddInParameter(dc, "ManagerUserCityIds", DbType.String, ManagerUserCityIds);
            base.TourStore.AddInParameter(dc, "SellCityId", DbType.Int32, SellCityId);
            base.TourStore.AddInParameter(dc, "AreaId", DbType.Int32, AreaId);
            base.TourStore.AddInParameter(dc, "StartTime", DbType.DateTime, StartTime);
            base.TourStore.AddInParameter(dc, "EndTime", DbType.DateTime, EndTime);

            using (IDataReader dr = DbHelper.RunReaderProcedure(dc, base.TourStore))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        RecordCount = dr.GetInt32(0);
                }

                dr.NextResult();

                Model.TourStructure.WholesalersStatistics model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.WholesalersStatistics();
                    model.CompanyId = dr["TourCompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("NotAcceptedNum")))
                        model.NotAcceptedNum = int.Parse(dr["NotAcceptedNum"].ToString()); 
                    if (!dr.IsDBNull(dr.GetOrdinal("SaveSeatNum")))
                        model.SaveSeatNum = int.Parse(dr["SaveSeatNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("SaveSeatExpiredNum")))
                        model.SaveSeatExpiredNum = int.Parse(dr["SaveSeatExpiredNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("OrdainNum")))
                        model.OrdainNum = int.Parse(dr["OrdainNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("LoginNum")))
                        model.LoginNum = int.Parse(dr["LoginNum"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("VisitedNum")))
                        model.VisitedNum = int.Parse(dr["VisitedNum"].ToString());

                    List.Add(model);
                    AreaIdAndProduceNum.Add(model.CompanyId, dr["AreaIdAndProduceNum"].ToString());
                }
                model = null;
            }

            if (List == null || List.Count <= 0 || AreaIdAndProduceNum == null || AreaIdAndProduceNum.Count <= 0)
                return null;

            Model.TourStructure.AreaStatInfo tmpModel = null;
            System.Xml.XmlAttributeCollection attList = null;
            System.Xml.XmlDocument xml = null;
            System.Xml.XmlNodeList xmlNodeList = null;
            foreach (Model.TourStructure.WholesalersStatistics model in List)
            {
                if (model == null || string.IsNullOrEmpty(AreaIdAndProduceNum[model.CompanyId]))
                    continue;
                model.AreaStatinfo = new List<EyouSoft.Model.TourStructure.AreaStatInfo>();
                xml = new System.Xml.XmlDocument();
                xml.LoadXml(AreaIdAndProduceNum[model.CompanyId]);
                xmlNodeList = xml.GetElementsByTagName("tbl_TourList");
                if (xmlNodeList != null)
                {
                    foreach (System.Xml.XmlNode node in xmlNodeList)
                    {
                        tmpModel = new EyouSoft.Model.TourStructure.AreaStatInfo();
                        attList = node.Attributes;
                        if (attList != null && attList.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(attList["AreaId"].Value))
                                tmpModel.AreaId = int.Parse(attList["AreaId"].Value);
                            if (!string.IsNullOrEmpty(attList["ProduceNum"].Value))
                                tmpModel.Number = int.Parse(attList["ProduceNum"].Value);
                        }
                        model.AreaStatinfo.Add(tmpModel);
                    }
                }
            }
            tmpModel = null;
            if (attList != null) attList.RemoveAll();
            if (AreaIdAndProduceNum != null) AreaIdAndProduceNum.Clear();
            AreaIdAndProduceNum = null;
            attList = null;
            xmlNodeList = null;
            xml = null;

            #endregion

            return List;
        }

        /// <summary>
        /// 获取批发商产品(所有的没有被删除且审核通过的)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1:成交订单升/降序;2/3:留位订单升/降序;4/5:留位过期订单升/降序;6/7:不受理订单升/降序;8/9:被查看次数升/降序;10/11:产品创建时间升/降序)</param>
        /// <param name="TourDisplayType">团队展示类型(必须传值)</param>
        /// <param name="SellCompanyId">批发商ID(为null不作条件)</param>
        /// <param name="AreaId">线路区域ID(小于等于0不作条件)</param>
        /// <param name="RouteName">线路名称(为null不作条件)</param>
        /// <param name="StartTime">开始出团时间(为null不作条件)</param>
        /// <param name="EndTime">结束出团时间(为null不作条件)</param>
        /// <returns></returns>
        public virtual IList<Model.TourStructure.TourStatByOrderInfo> GetTourStatByOrderInfo(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, EyouSoft.Model.TourStructure.TourDisplayType TourDisplayType, string SellCompanyId, int AreaId, string RouteName
            , DateTime? StartTime, DateTime? EndTime)
        {
            IList<Model.TourStructure.TourStatByOrderInfo> List = new List<Model.TourStructure.TourStatByOrderInfo>();

            #region Sql拼接

            string strWhere = " 1 = 1 ";
            string strOrder = string.Empty;
            string strSubWhere = string.Empty;
            string strTableName = string.Empty;
            StringBuilder strFiles = new StringBuilder(" [ID],[CompanyID],[RouteName],[TourNo],[AreaId],[ShowCount],[AreaName],[OrdainNum],[SaveSeatNum],[SaveSeatExpiredNum],[NotAcceptedNum] ");
            if (TourDisplayType == EyouSoft.Model.TourStructure.TourDisplayType.线路产品)
                strTableName = "View_TourOrder_Wholesalers_Model_Product";  //模板团视图
            else
                strTableName = "View_TourOrder_Wholesalers_Product"; //子团视图

            if (!string.IsNullOrEmpty(SellCompanyId))
                strWhere += string.Format(" AND CompanyID = '{0}' ", SellCompanyId);
            if (AreaId > 0)
                strWhere += string.Format(" AND AreaId = {0} ", AreaId);
            if (!string.IsNullOrEmpty(RouteName))
                strWhere += string.Format(" AND RouteName like '%{0}%' ", RouteName);
            if (StartTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',LeaveDate) >= 0 ", StartTime);
            if (EndTime != null)
                strWhere += string.Format(" and datediff(dd,'{0}',LeaveDate) <= 0 ", EndTime);

            switch (OrderIndex)
            {
                case 0: strOrder = " OrdainNum asc "; break;
                case 1: strOrder = " OrdainNum desc "; break;
                case 2: strOrder = " SaveSeatNum asc "; break;
                case 3: strOrder = " SaveSeatNum desc "; break;
                case 4: strOrder = " SaveSeatExpiredNum asc "; break;
                case 5: strOrder = " SaveSeatExpiredNum desc "; break;
                case 6: strOrder = " NotAcceptedNum asc "; break;
                case 7: strOrder = " NotAcceptedNum desc "; break;
                case 8: strOrder = " ShowCount asc "; break;
                case 9: strOrder = " ShowCount desc "; break;
                case 10: strOrder = " LeaveDate asc "; break;
                case 11: strOrder = " LeaveDate desc "; break;
                default: strOrder = " LeaveDate desc "; break;
            }

            #endregion

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, strTableName, "ID", strFiles.ToString(), strWhere, strOrder))
            {
                Model.TourStructure.TourStatByOrderInfo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.TourStatByOrderInfo();
                    model.ID = dr["ID"].ToString();
                    model.CompanyID = dr["CompanyID"].ToString();
                    model.RouteName = dr["RouteName"].ToString();
                    model.TourNo = dr["TourNo"].ToString();
                    if (!string.IsNullOrEmpty(dr["AreaId"].ToString()))
                        model.AreaId = int.Parse(dr["AreaId"].ToString());
                    if (!string.IsNullOrEmpty(dr["ShowCount"].ToString()))
                        model.Clicks = int.Parse(dr["ShowCount"].ToString());
                    model.AreaName = dr["AreaName"].ToString();
                    if (!string.IsNullOrEmpty(dr["OrdainNum"].ToString()))
                        model.OrdainNum = int.Parse(dr["OrdainNum"].ToString());
                    if (!string.IsNullOrEmpty(dr["SaveSeatNum"].ToString()))
                        model.SaveSeatNum = int.Parse(dr["SaveSeatNum"].ToString());
                    if (!string.IsNullOrEmpty(dr["SaveSeatExpiredNum"].ToString()))
                        model.SaveSeatExpiredNum = int.Parse(dr["SaveSeatExpiredNum"].ToString());
                    if (!string.IsNullOrEmpty(dr["NotAcceptedNum"].ToString()))
                        model.NotAcceptedNum = int.Parse(dr["NotAcceptedNum"].ToString());

                    List.Add(model);
                }
                model = null;
            }

            #endregion

            return List;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 根据查询命令返回订单实体集合(不含游客信息)
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>返回订单实体集合(不含游客信息)</returns>
        private IList<EyouSoft.Model.TourStructure.TourOrder> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.TourStructure.TourOrder> list = new List<EyouSoft.Model.TourStructure.TourOrder>();
            using (IDataReader dr = base.TourStore.ExecuteReader(dc))
            {
                EyouSoft.Model.TourStructure.TourOrder model = null;
                while (dr.Read())
                {
                    model = new Model.TourStructure.TourOrder();
                    model.ID = dr["ID"].ToString();
                    model.OrderNo = dr["OrderNo"].ToString();
                    model.TourId = dr["TourId"].ToString();
                    model.RouteName = dr["RouteName"].ToString();
                    model.TourNo = dr["TourNo"].ToString();
                    if (!string.IsNullOrEmpty(dr["LeaveDate"].ToString()))
                        model.LeaveDate = DateTime.Parse(dr["LeaveDate"].ToString());
                    //if (!string.IsNullOrEmpty(dr["SiteId"].ToString()))
                    //model.SiteId = int.Parse(dr["SiteId"].ToString());
                    if (!string.IsNullOrEmpty(dr["AreaId"].ToString()))
                        model.AreaId = int.Parse(dr["AreaId"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderType"].ToString()))
                        model.OrderType = int.Parse(dr["OrderType"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderState"].ToString()))
                        model.OrderState = (Model.TourStructure.OrderState)int.Parse(dr["OrderState"].ToString());
                    model.BuyCompanyID = dr["BuyCompanyID"].ToString();
                    model.BuyCompanyName = dr["BuyCompanyName"].ToString();
                    model.ContactName = dr["ContactName"].ToString();
                    model.ContactTel = dr["ContactTel"].ToString();
                    model.ContactFax = dr["ContactFax"].ToString();
                    model.ContactMQ = dr["ContactMQ"].ToString();
                    model.ContactQQ = dr["ContactQQ"].ToString();
                    model.OperatorID = dr["OperatorID"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.CompanyID = dr["CompanyID"].ToString();
                    model.PriceStandId = dr["PriceStandId"].ToString();
                    if (!string.IsNullOrEmpty(dr["PersonalPrice"].ToString()))
                        model.PersonalPrice = decimal.Parse(dr["PersonalPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["ChildPrice"].ToString()))
                        model.ChildPrice = decimal.Parse(dr["ChildPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["MarketPrice"].ToString()))
                        model.MarketPrice = decimal.Parse(dr["MarketPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["AdultNumber"].ToString()))
                        model.AdultNumber = int.Parse(dr["AdultNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["ChildNumber"].ToString()))
                        model.ChildNumber = int.Parse(dr["ChildNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["MarketNumber"].ToString()))
                        model.MarketNumber = int.Parse(dr["MarketNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["PeopleNumber"].ToString()))
                        model.PeopleNumber = int.Parse(dr["PeopleNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["OtherPrice"].ToString()))
                        model.OtherPrice = decimal.Parse(dr["OtherPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["SaveSeatDate"].ToString()))
                        model.SaveSeatDate = DateTime.Parse(dr["SaveSeatDate"].ToString());
                    model.OperatorContent = dr["OperatorContent"].ToString();
                    model.LeaveTraffic = dr["LeaveTraffic"].ToString();
                    model.SpecialContent = dr["SpecialContent"].ToString();
                    if (!string.IsNullOrEmpty(dr["SumPrice"].ToString()))
                        model.SumPrice = decimal.Parse(dr["SumPrice"].ToString());
                    model.SeatList = dr["SeatList"].ToString();
                    if (!string.IsNullOrEmpty(dr["LastDate"].ToString()))
                        model.LastDate = DateTime.Parse(dr["LastDate"].ToString());
                    model.LastOperatorID = dr["LastOperatorID"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsDelete"].ToString()))
                    {
                        if (dr["IsDelete"].ToString() == "1")
                            model.IsDelete = true;
                        else
                            model.IsDelete = false;
                    }
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.TourCompanyName = dr["TourCompanyName"].ToString();
                    model.TourCompanyId = dr["TourCompanyId"].ToString();
                    if (!string.IsNullOrEmpty(dr["TourClassId"].ToString()))
                        model.TourType = (Model.TourStructure.TourType)int.Parse(dr["TourClassId"].ToString());
                    if (!string.IsNullOrEmpty(dr["SuccessTime"].ToString()))
                        model.SuccessTime = DateTime.Parse(dr["SuccessTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["ExpireTime"].ToString()))
                        model.ExpireTime = DateTime.Parse(dr["ExpireTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["RateType"].ToString()))
                        model.RateType = (Model.TourStructure.RateType)int.Parse(dr["RateType"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderScore"].ToString()))
                        model.OrderScore = int.Parse(dr["OrderScore"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderSource"].ToString()))
                        model.OrderSource = (Model.TourStructure.TourOrderOperateType)int.Parse(dr["OrderSource"].ToString());

                    list.Add(model);
                }

                model = null;
            }
            return list;
        }

        /// <summary>
        /// 根据查询命令返回订单实体集合(不含游客信息)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="strFiles">返回字段集合</param>
        /// <param name="strWhere">查询条件组合</param>
        /// <param name="strOrder">排序字段组合</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.TourOrder> GetQueryList(int PageSize, int PageIndex, ref int RecordCount, string strFiles, string strWhere, string strOrder)
        {
            IList<EyouSoft.Model.TourStructure.TourOrder> list = new List<EyouSoft.Model.TourStructure.TourOrder>();
            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, "tbl_TourOrder", "ID", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.TourStructure.TourOrder model = null;
                while (dr.Read())
                {
                    model = new Model.TourStructure.TourOrder();
                    model.ID = dr["ID"].ToString();
                    model.OrderNo = dr["OrderNo"].ToString();
                    model.TourId = dr["TourId"].ToString();
                    model.RouteName = dr["RouteName"].ToString();
                    model.TourNo = dr["TourNo"].ToString();
                    if (!string.IsNullOrEmpty(dr["LeaveDate"].ToString()))
                        model.LeaveDate = DateTime.Parse(dr["LeaveDate"].ToString());
                    //if (!string.IsNullOrEmpty(dr["SiteId"].ToString()))
                    //model.SiteId = int.Parse(dr["SiteId"].ToString());
                    if (!string.IsNullOrEmpty(dr["AreaId"].ToString()))
                        model.AreaId = int.Parse(dr["AreaId"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderType"].ToString()))
                        model.OrderType = int.Parse(dr["OrderType"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderState"].ToString()))
                        model.OrderState = (Model.TourStructure.OrderState)int.Parse(dr["OrderState"].ToString());
                    model.BuyCompanyID = dr["BuyCompanyID"].ToString();
                    model.BuyCompanyName = dr["BuyCompanyName"].ToString();
                    model.ContactName = dr["ContactName"].ToString();
                    model.ContactTel = dr["ContactTel"].ToString();
                    model.ContactFax = dr["ContactFax"].ToString();
                    model.ContactMQ = dr["ContactMQ"].ToString();
                    model.ContactQQ = dr["ContactQQ"].ToString();
                    model.OperatorID = dr["OperatorID"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.CompanyID = dr["CompanyID"].ToString();
                    model.PriceStandId = dr["PriceStandId"].ToString();
                    if (!string.IsNullOrEmpty(dr["PersonalPrice"].ToString()))
                        model.PersonalPrice = decimal.Parse(dr["PersonalPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["ChildPrice"].ToString()))
                        model.ChildPrice = decimal.Parse(dr["ChildPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["MarketPrice"].ToString()))
                        model.MarketPrice = decimal.Parse(dr["MarketPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["AdultNumber"].ToString()))
                        model.AdultNumber = int.Parse(dr["AdultNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["ChildNumber"].ToString()))
                        model.ChildNumber = int.Parse(dr["ChildNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["MarketNumber"].ToString()))
                        model.MarketNumber = int.Parse(dr["MarketNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["PeopleNumber"].ToString()))
                        model.PeopleNumber = int.Parse(dr["PeopleNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["OtherPrice"].ToString()))
                        model.OtherPrice = decimal.Parse(dr["OtherPrice"].ToString());
                    if (!string.IsNullOrEmpty(dr["SaveSeatDate"].ToString()))
                        model.SaveSeatDate = DateTime.Parse(dr["SaveSeatDate"].ToString());
                    model.OperatorContent = dr["OperatorContent"].ToString();
                    model.LeaveTraffic = dr["LeaveTraffic"].ToString();
                    model.SpecialContent = dr["SpecialContent"].ToString();
                    if (!string.IsNullOrEmpty(dr["SumPrice"].ToString()))
                        model.SumPrice = decimal.Parse(dr["SumPrice"].ToString());
                    model.SeatList = dr["SeatList"].ToString();
                    if (!string.IsNullOrEmpty(dr["LastDate"].ToString()))
                        model.LastDate = DateTime.Parse(dr["LastDate"].ToString());
                    model.LastOperatorID = dr["LastOperatorID"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsDelete"].ToString()))
                    {
                        if (dr["IsDelete"].ToString() == "1")
                            model.IsDelete = true;
                        else
                            model.IsDelete = false;
                    }
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.TourCompanyName = dr["TourCompanyName"].ToString();
                    model.TourCompanyId = dr["TourCompanyId"].ToString();
                    if (!string.IsNullOrEmpty(dr["TourClassId"].ToString()))
                        model.TourType = (Model.TourStructure.TourType)int.Parse(dr["TourClassId"].ToString());
                    if (!string.IsNullOrEmpty(dr["SuccessTime"].ToString()))
                        model.SuccessTime = DateTime.Parse(dr["SuccessTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["ExpireTime"].ToString()))
                        model.ExpireTime = DateTime.Parse(dr["ExpireTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["RateType"].ToString()))
                        model.RateType = (Model.TourStructure.RateType)int.Parse(dr["RateType"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderScore"].ToString()))
                        model.OrderScore = int.Parse(dr["OrderScore"].ToString());
                    if (!string.IsNullOrEmpty(dr["OrderSource"].ToString()))
                        model.OrderSource = (Model.TourStructure.TourOrderOperateType)int.Parse(dr["OrderSource"].ToString());

                    list.Add(model);
                }

                model = null;
            }
            return list;
        }

        #endregion

        #endregion
    }
}
