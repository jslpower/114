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

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// MQ消息
    /// </summary>
    /// 周文超 2010-05-11
    /// 郑付杰 2012-2-8
    public class IMMessage : DALBase, IDAL.MQStructure.IIMMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMessage(){ }

        #region static constants
        //static constants
        /*private const string SQL_INSERT_SENDONETOONEMESSAGE = " INSERT INTO [im_message]([num],[src],[dst],[message],[flag],[IssueTime]) VALUES ('{0}',{1},{2},'{3}',{4},'{5}'); ";*/
        private const string Sql_Insert_im_message_url = " INSERT INTO [im_message_url]([ID],[RedirectUrl],[RedirectLoginType],[IssueTime],[uid]) VALUES ('{0}','{1}',{2},'{3}','{4}'); ";
        #endregion

        #region 函数成员

        /*/// <summary>
        /// 发送一对一消息
        /// </summary>
        /// <param name="info">MQ消息一对一消息业务实体</param>
        /// <returns></returns>
        public virtual bool SendOneToOneMessage(Model.MQStructure.OneToOneMessageInfo info)
        {
            if (info == null)
                return false;

            DbCommand dc = base.MQStore.GetSqlStringCommand(string.Format(SQL_INSERT_SENDONETOONEMESSAGE, Guid.NewGuid().ToString()
                , info.SendMQId, info.SendType == 1 ? 0 : info.AcceptMQId, info.MessageContent, 0, DateTime.Now));

            return DbHelper.ExecuteSql(dc, base.MQStore) > 0 ? true : false;
        }        

        /// <summary>
        /// 发送一对多消息(消息中可以包含链接)
        /// </summary>
        /// <param name="info">MQ消息一对多(指定了接收人的MQ编号)消息业务实体</param>
        /// <returns></returns>
        public virtual bool SendOneToManyMessage(Model.MQStructure.OneToManySpecifiedMQMessageInfo info)
        {
            if (info == null || info.AcceptMQInfo == null || info.AcceptMQInfo.Count <= 0)
                return false;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlTmp = new StringBuilder();
            foreach (Model.MQStructure.OneToManySpecifiedMQMessageAccepMQInfo model in info.AcceptMQInfo)
            {
                strSql.AppendFormat(SQL_INSERT_SENDONETOONEMESSAGE, Guid.NewGuid().ToString(), info.SendType == 1 ? 0 : info.SendMQId
                    , model.MQId, model.MessageContent, 0, DateTime.Now);
                if (info.IsLink)
                    strSqlTmp.AppendFormat(Sql_Insert_im_message_url, Guid.NewGuid().ToString(), model.MessageUrlToUrl, 0, DateTime.Now
                        , model.MQId);
            }
            strSql.Append(strSqlTmp.ToString());

            DbCommand dc = base.MQStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSqlTrans(dc, base.MQStore) > 0 ? true : false;
        }*/

        /// <summary>
        /// 发送一对多消息
        /// </summary>
        /// <param name="info">MQ消息一对多(未指定接收人的MQ编号)消息业务实体</param>
        /// <returns>System.Int32 发送的消息数</returns>
        public virtual int SendOneToManyMessage(Model.MQStructure.OneToManyUnspecifiedMQMessageInfo info)
        {
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" INSERT INTO [im_message]([src],[dst],[message],[flag],[IssueTime]) ");
            cmdText.AppendFormat(" SELECT {0},A.im_uid,'{1}',0,GETDATE()  FROM [im_member] AS A ", info.SendType == EyouSoft.Model.MQStructure.SendType.系统消息 ? 0 : info.SendMQId, info.Message);

            if (info.AcceptProvinceId > 0
                || info.AcceptProvinceId > 0
                ||!info.IsAllCompanyType)
            {
                cmdText.Append(" INNER JOIN [tbl_CompanyUser] AS B ");
                cmdText.Append(" ON A.bs_uid=B.Id INNER JOIN tbl_CompanyInfo AS C ");
                cmdText.Append(" ON B.CompanyId=C.Id ");

                #region 省份城市
                if (info.AcceptProvinceId > 0)
                {
                    cmdText.AppendFormat(" AND C.ProvinceId={0} ", info.AcceptProvinceId);
                }

                if (info.AcceptCityId > 0)
                {
                    cmdText.AppendFormat(" AND C.CityId={0} ", info.AcceptCityId);
                }
                #endregion

                #region 用户类型 AND(1=0 OR QUERY OR QUERY)
                if ((info.AcceptCompanyType != null && info.AcceptCompanyType.Count > 0) || info.IsContainsGuest)
                {
                    cmdText.Append(" AND (1=0 ");

                    if (info.AcceptCompanyType != null && info.AcceptCompanyType.Count > 0)
                    {
                        cmdText.Append(" OR EXISTS(SELECT 1 FROM [tbl_CompanyTypeList] WHERE CompanyId=C.Id");
                        cmdText.AppendFormat(" AND TypeId IN({0} ", (int)info.AcceptCompanyType[0]);
                        for (int i = 1; i < info.AcceptCompanyType.Count; i++)
                        {
                            cmdText.AppendFormat(" ,{0} ", (int)info.AcceptCompanyType[i]);
                        }
                        cmdText.Append(" )) ");
                    }

                    if (info.IsContainsGuest)
                    {
                        cmdText.Append(" OR C.CompanyType=9 ");
                    }

                    cmdText.Append(" ) ");
                }
                #endregion
            }

            cmdText.Append(" WHERE 1=1 ");

            #region 用户状态
            switch (info.OnlineState)
            {
                case EyouSoft.Model.MQStructure.OnlineState.登录过用户:
                    cmdText.Append(" AND A.im_latest_time>'0000-00-00 00:00:00' ");
                    break;
                case EyouSoft.Model.MQStructure.OnlineState.所有用户: 
                    break;
                case EyouSoft.Model.MQStructure.OnlineState.在线用户:
                    cmdText.Append(" AND A.im_status>11 ");
                    break;
            }
            #endregion

            DbCommand cmd = base.MQStore.GetSqlStringCommand(cmdText.ToString());

            return DbHelper.ExecuteSql(cmd, base.MQStore);
        }

        /// <summary>
        /// 新增订单后向批发商发送MQ消息
        /// </summary>
        /// <param name="NewOrderId">订单ID</param>
        /// <param name="MQMessageTransitUrl">MQ消息中转页面地址（登录页）</param>
        /// <param name="MQPurposeUrl">MQ消息目的地址</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool AddMessageToWholesalersByNewOrder(string NewOrderId, string MQMessageTransitUrl, 
            string MQPurposeUrl,int orderType)
        {
            if (string.IsNullOrEmpty(NewOrderId) || string.IsNullOrEmpty(MQMessageTransitUrl) || string.IsNullOrEmpty(MQPurposeUrl))
                return false;

            DbCommand dc = base.MQStore.GetStoredProcCommand("proc_im_message_BuildWholesalersMessage");
            base.MQStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, NewOrderId);
            base.MQStore.AddInParameter(dc, "MQMessageTransitUrl", DbType.String, MQMessageTransitUrl);
            base.MQStore.AddInParameter(dc, "MQPurposeUrl", DbType.String, MQPurposeUrl);
            base.MQStore.AddInParameter(dc, "OrderType", DbType.Int32, orderType);
            base.MQStore.AddOutParameter(dc, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(dc, base.MQStore);
            object obj = base.MQStore.GetParameterValue(dc, "Result");
            if (obj.Equals(null))
                return false;
            else
                return int.Parse(obj.ToString()) == 9 ? true : false;
            
        }

        /// <summary>
        /// 订单状态改变后发送消息给预订人
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="MQMessageTransitUrl">MQ消息中转页面地址（登录页）</param>
        /// <param name="MQPurposeUrl">MQ消息目的地址</param>
        /// <param name="orderType">订单类型(1:散拼订单 2：团队订单)</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool AddMessageToRetailersByOrder(string OrderId, string MQMessageTransitUrl, string MQPurposeUrl, int orderType)
        {
            if (string.IsNullOrEmpty(OrderId) || string.IsNullOrEmpty(MQMessageTransitUrl) || string.IsNullOrEmpty(MQPurposeUrl))
                return false;
            DbCommand dc = base.MQStore.GetStoredProcCommand("proc_im_message_BuildRetailersMessage");
            base.MQStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, OrderId);
            base.MQStore.AddInParameter(dc, "MQMessageTransitUrl", DbType.String, MQMessageTransitUrl);
            base.MQStore.AddInParameter(dc, "MQPurposeUrl", DbType.String, MQPurposeUrl);
            base.MQStore.AddInParameter(dc, "OrderType", DbType.Int32, orderType);
            base.MQStore.AddOutParameter(dc, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(dc, base.MQStore);
            object obj = base.MQStore.GetParameterValue(dc, "Result");
            if (obj.Equals(null))
                return false;
            else
                return int.Parse(obj.ToString()) == 9 ? true : false;
        }

        /// <summary>
        /// 供求信息被评论后发送MQ消息给发布人
        /// </summary>
        /// <param name="CommentId">供求评论ID</param>
        /// <param name="MQMessageTransitUrl">MQ消息中转页面地址（登录页）</param>
        /// <param name="MQPurposeUrl">MQ消息目的地址</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool AddMessageToExchangeComment(string CommentId, string MQMessageTransitUrl, string MQPurposeUrl)
        {
            if (string.IsNullOrEmpty(CommentId) || string.IsNullOrEmpty(MQMessageTransitUrl) || string.IsNullOrEmpty(MQPurposeUrl))
                return false;

            DbCommand dc = base.MQStore.GetStoredProcCommand("proc_im_message_BuildExchangeCommentMessage");
            base.MQStore.AddInParameter(dc, "ExchangeCommentId", DbType.AnsiStringFixedLength, CommentId);
            base.MQStore.AddInParameter(dc, "MQMessageTransitUrl", DbType.String, MQMessageTransitUrl);
            base.MQStore.AddInParameter(dc, "MQPurposeUrl", DbType.String, MQPurposeUrl);
            base.MQStore.AddOutParameter(dc, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(dc, base.MQStore);
            object obj = base.MQStore.GetParameterValue(dc, "Result");
            if (obj.Equals(null))
                return false;
            else
                return int.Parse(obj.ToString()) == 9 ? true : false;
        }

        /// <summary>
        /// 获得当天最近的MQ消息
        /// </summary>
        /// <param name="count">要获取的MQ消息条数,若为0,则表示查询所有</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.MQStructure.IMMessage> GetTodayLastMessage(int count)
        {
            IList<EyouSoft.Model.MQStructure.IMMessage> list = new List<EyouSoft.Model.MQStructure.IMMessage>();
            string MYSQL_SELECT_TodayMsg = "SELECT num,dst,src,timenow FROM user_message where src>0 AND DATEDIFF(timenow,CURDATE())=0 group by dst";
            if (count > 0)
                MYSQL_SELECT_TodayMsg = " limit " + count;
            DbCommand dc = base.MySQLStore.GetSqlStringCommand(MYSQL_SELECT_TodayMsg);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.MQStructure.IMMessage model = new EyouSoft.Model.MQStructure.IMMessage();
                    model.num = dr.GetInt32(dr.GetOrdinal("num"));
                    model.dst = dr.GetInt32(dr.GetOrdinal("dst"));
                    model.src = dr.GetInt32(dr.GetOrdinal("src"));
                    //model.message = dr.IsDBNull(dr.GetOrdinal("content")) ? "" : dr.GetString(dr.GetOrdinal("content"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("timenow"));
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion
    }
}
