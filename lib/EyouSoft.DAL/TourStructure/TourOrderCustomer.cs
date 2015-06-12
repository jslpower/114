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
    /// 订单游客信息数据访问
    /// </summary>
    /// 周文超 2010-05-11
    public class TourOrderCustomer : DALBase, IDAL.TourStructure.ITourOrderCustomer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrderCustomer()
        { }

        private const string Sql_TourOrderCustomer_Del_OrderId = " delete from [tbl_TourOrderCustomer] where [OrderID] = @OrderID ";
        private const string Sql_TourOrderCustomer_Select = " select [ID],[CompanyID],[OrderID],[VisitorName],[CradType],[CradNumber],[Sex],[VisitorType],[ContactTel],[Remark],[IssueTime],[SiteNo] FROM [tbl_TourOrderCustomer] ";
        private const string Sql_TourOrderCustomer_Insert = " INSERT INTO [tbl_TourOrderCustomer]([ID],[CompanyID],[OrderID],[VisitorName],[CradType],[CradNumber],[Sex],[VisitorType],[ContactTel],[Remark],[IssueTime],[SiteNo]) VALUES ( '{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}') ";

        #region 函数成员

        /// <summary>
        /// 新增订单游客信息
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="allCustomer">游客信息实体集合</param>
        /// <returns>受影响行数</returns>
        public virtual int AddTourOrderCustomer(string OrderId, IList<EyouSoft.Model.TourStructure.TourOrderCustomer> allCustomer)
        {
            if (allCustomer == null || allCustomer.Count <= 0 || string.IsNullOrEmpty(OrderId))
                return 0;

            StringBuilder strSql = new StringBuilder();
            foreach (EyouSoft.Model.TourStructure.TourOrderCustomer model in allCustomer)
            {
                strSql.AppendFormat(Sql_TourOrderCustomer_Insert, Guid.NewGuid().ToString(), model.CompanyID, OrderId, model.VisitorName, (int)model.CradType, model.CradNumber, model.Sex == true ? "1" : "0", model.VisitorType ? "1" : "0", model.ContactTel, model.Remark, model.IssueTime, model.SiteNo);
                strSql.Append(" ; ");
            }

            DbCommand dc = base.TourStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, base.TourStore);
        }

        /// <summary>
        /// 删除某一订单游客信息
        /// </summary>
        /// <param name="TourOrderId">订单ID</param>
        /// <returns>返回受影响行数</returns>
        public virtual int DeleteCustomerByOrderId(string TourOrderId)
        {
            if (string.IsNullOrEmpty(TourOrderId))
                return 0;

            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_TourOrderCustomer_Del_OrderId);
            base.TourStore.AddInParameter(dc, "OrderID", DbType.AnsiStringFixedLength, TourOrderId);

            return DbHelper.ExecuteSql(dc, base.TourStore);
        }

        /// <summary>
        /// 获取某订单的所有的游客信息
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回实体集合</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerListByOrderId(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
                return null;

            string strWhere = Sql_TourOrderCustomer_Select + " where [OrderID] = @OrderID ";

            DbCommand dc = base.TourStore.GetSqlStringCommand(strWhere);
            base.TourStore.AddInParameter(dc, "OrderID", DbType.AnsiStringFixedLength, OrderId);

            return GetQueryList(dc);
        }

        /// <summary>
        /// 获取某公司的所有的游客信息(分页)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数(返回参数)</param>
        /// <param name="OrderIndex">排序方式：0 时间升序；1 时间降序</param>
        /// <param name="CompanyId">游客所属公司ID</param>
        /// <returns>返回游客信息实体集合</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerListByCompanyId(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string CompanyId)
        {
            IList<EyouSoft.Model.TourStructure.TourOrderCustomer> list = new List<EyouSoft.Model.TourStructure.TourOrderCustomer>();

            string strWhere = string.Empty;
            string strFiles = " [ID],[CompanyID],[OrderID],[VisitorName],[CradType],[CradNumber],[Sex],[VisitorType],[ContactTel],[Remark],[IssueTime],[SiteNo] ";
            string strOrder = string.Empty;
            if (!string.IsNullOrEmpty(CompanyId))
                strWhere = " [CompanyID] = " + CompanyId;
            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, "tbl_TourOrderCustomer", "[ID]", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.TourStructure.TourOrderCustomer model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                    model.ID = dr[0].ToString();
                    model.CompanyID = dr[1].ToString();
                    model.OrderID = dr[2].ToString();
                    model.VisitorName = dr[3].ToString();
                    if (!string.IsNullOrEmpty(dr[4].ToString()))
                        model.CradType = (Model.TourStructure.CradType)int.Parse(dr[4].ToString());
                    model.CradNumber = dr[5].ToString();
                    if (!string.IsNullOrEmpty(dr[6].ToString()))
                    {
                        if (dr[6].ToString() == "0")
                            model.Sex = false;
                        else if (dr[6].ToString() == "1")
                            model.Sex = true;
                    }
                    if (string.IsNullOrEmpty(dr[7].ToString()) || dr[7].ToString() == "0")
                        model.VisitorType = false;
                    else
                        model.VisitorType = true;
                    model.ContactTel = dr[8].ToString();
                    model.Remark = dr[9].ToString();
                    if (!string.IsNullOrEmpty(dr[10].ToString()))
                        model.IssueTime = DateTime.Parse(dr[10].ToString());
                    model.SiteNo = dr[11].ToString();

                    list.Add(model);
                }
                model = null;
            }

            return list;
        }

        #region 运营后台函数成员

        /// <summary>
        /// 获取游客信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="BuyCompanyID">零售商公司ID(买家)(为null不作条件)</param>
        /// <param name="SellCompanyName">专线商公司名称(卖家)(为null不作条件)</param>
        /// <param name="CustomerName">游客姓名(为null不作条件)</param>
        /// <param name="Sex">游客性别(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="OrderStates">订单状态</param>
        /// <returns>返回实体集合</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string BuyCompanyID, string SellCompanyName, string CustomerName, bool? Sex, DateTime? StartTime
            , DateTime? EndTime, params EyouSoft.Model.TourStructure.OrderState[] OrderStates)
        {
            Dictionary<string, string> RouteAndTour = new Dictionary<string, string>();
            IList<EyouSoft.Model.TourStructure.TourOrderCustomer> list = new List<EyouSoft.Model.TourStructure.TourOrderCustomer>();

            #region Sql拼接


            string strWhere = " 1 = 1 ";
            string strFiles = " ID,CompanyID,OrderID,VisitorName,CradType,CradNumber,Sex,VisitorType,ContactTel,Remark,IssueTime,SiteNo";
            strFiles += ",(select TourId,RouteName,TourNo,TourCompanyName from tbl_TourOrder where tbl_TourOrder.ID = tbl_TourOrderCustomer.OrderID for xml auto,root('root')) as RouteAndTour ";

            //子查询拼接
            StringBuilder strSubWhere = new StringBuilder(" IsDelete = '0' ");
            if (!string.IsNullOrEmpty(BuyCompanyID))
                strSubWhere.AppendFormat(" and tbl_TourOrder.BuyCompanyID = '{0}' ", BuyCompanyID);
            if (!string.IsNullOrEmpty(SellCompanyName))
                strSubWhere.AppendFormat(" and tbl_TourOrder.TourCompanyName like '%{0}%' ", SellCompanyName);
            if (StartTime != null)
                strSubWhere.AppendFormat(" and datediff(dd,'{0}',tbl_TourOrder.IssueTime) >= 0 ", StartTime);
            if (EndTime != null)
                strSubWhere.AppendFormat(" and datediff(dd,'{0}',tbl_TourOrder.IssueTime) <= 0 ", EndTime);
            if (OrderStates != null && OrderStates.Length > 0)
            {
                if (OrderStates.Length == 1)
                    strSubWhere.AppendFormat(" and tbl_TourOrder.OrderState = {0} ", (int)OrderStates[0]);
                else
                {
                    string strTmp = " and tbl_TourOrder.OrderState in (";
                    foreach (EyouSoft.Model.TourStructure.OrderState tmp in OrderStates)
                    {
                        strTmp += string.Format("{0},", (int)tmp);
                    }
                    strTmp = strTmp.TrimEnd();
                    strSubWhere.Append(strTmp);
                }
            }

            //追缴子查询条件
            if (!string.IsNullOrEmpty(strSubWhere.ToString()))
            {
                strWhere += string.Format(" and OrderID in  (select ID from tbl_TourOrder where {0} ) ", strSubWhere.ToString());
            }
            if (!string.IsNullOrEmpty(CustomerName))
                strWhere += string.Format(" and VisitorName like '%{0}%' ", CustomerName);
            if (Sex != null)
                strWhere += string.Format(" and Sex = '{0}' ", (bool)Sex ? 1 : 0);

            string strOrder = string.Empty;
            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            #endregion

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, "tbl_TourOrderCustomer", "ID", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.TourStructure.TourOrderCustomer model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                    model.ID = dr[0].ToString();
                    model.CompanyID = dr[1].ToString();
                    model.OrderID = dr[2].ToString();
                    model.VisitorName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.CradType = (Model.TourStructure.CradType)dr.GetByte(4);
                    model.CradNumber = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                    {
                        if (dr[6].ToString() == "0")
                            model.Sex = false;
                        else if (dr[6].ToString() == "1")
                            model.Sex = true;
                    }
                    if (dr.IsDBNull(7) || dr[7].ToString() == "0")
                        model.VisitorType = false;
                    else
                        model.VisitorType = true;
                    model.ContactTel = dr[8].ToString();
                    model.Remark = dr[9].ToString();
                    if (!dr.IsDBNull(10))
                        model.IssueTime = dr.GetDateTime(10);
                    model.SiteNo = dr[11].ToString();

                    list.Add(model);
                    RouteAndTour.Add(model.ID, dr[12].ToString());
                }
                model = null;
            }

            if (list == null || list.Count <= 0 || RouteAndTour == null || RouteAndTour.Count <= 0)
                return null;

            System.Xml.XmlAttributeCollection attList = null;
            System.Xml.XmlDocument xml = null;
            System.Xml.XmlNodeList xmlNodeList = null;
            foreach (EyouSoft.Model.TourStructure.TourOrderCustomer model in list)
            {
                if (model == null || string.IsNullOrEmpty(RouteAndTour[model.ID]))
                    continue;

                xml = new System.Xml.XmlDocument();
                xml.LoadXml(RouteAndTour[model.ID]);
                xmlNodeList = xml.GetElementsByTagName("tbl_TourOrder");
                if (xmlNodeList != null)
                {
                    foreach (System.Xml.XmlNode node in xmlNodeList)
                    {
                        attList = node.Attributes;
                        if (attList != null && attList.Count > 0)
                        {
                            model.TourId = attList["TourId"].Value;
                            model.TourNo = attList["TourNo"].Value;
                            model.RouteName = attList["RouteName"].Value;
                            model.CompanyName = attList["TourCompanyName"].Value;

                            break;
                        }
                    }
                }
            }
            if (attList != null) attList.RemoveAll();
            if (RouteAndTour != null) RouteAndTour.Clear();
            attList = null;
            xmlNodeList = null;
            xml = null;

            #endregion

            return list;
        }

        #endregion

        #region 私有函数   执行查询命令返回订单客户信息实体集合

        /// <summary>
        /// 执行查询命令返回订单客户信息实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>订单客户信息实体集合</returns>
        private IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.TourStructure.TourOrderCustomer> list = new List<EyouSoft.Model.TourStructure.TourOrderCustomer>();
            using (IDataReader dr = base.TourStore.ExecuteReader(dc))
            {
                EyouSoft.Model.TourStructure.TourOrderCustomer model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                    model.ID = dr[0].ToString();
                    model.CompanyID = dr[1].ToString();
                    model.OrderID = dr[2].ToString();
                    model.VisitorName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.CradType = (Model.TourStructure.CradType)dr.GetByte(4);
                    model.CradNumber = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                    {
                        if (dr[6].ToString() == "0")
                            model.Sex = false;
                        else if (dr[6].ToString() == "1")
                            model.Sex = true;
                    }
                    if (dr.IsDBNull(7) || dr[7].ToString() == "0")
                        model.VisitorType = false;
                    else
                        model.VisitorType = true;
                    model.ContactTel = dr[8].ToString();
                    model.Remark = dr[9].ToString();
                    if (!dr.IsDBNull(10))
                        model.IssueTime = dr.GetDateTime(10);
                    model.SiteNo = dr[11].ToString();

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
