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
    /// MQ订单提醒设置
    /// </summary>
    /// 周文超 2010-05-11
    public class IMAcceptMsgPeople : DALBase, IDAL.MQStructure.IIMAcceptMsgPeople
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMAcceptMsgPeople()
        { }

        #region Sql

        private const string Sql_Exists = " select count(1) from Im_AcceptMsgPeople where ID=@ID ";
        private const string Sql_Add = " insert into Im_AcceptMsgPeople(ID,CompanyId,OperatorId,TourAreaId,IssueTime) values (@ID,@CompanyId,@OperatorId,@TourAreaId,@IssueTime) ";
        private const string Sql_Update = " update Im_AcceptMsgPeople set CompanyId=@CompanyId,OperatorId=@OperatorId,TourAreaId=@TourAreaId where ID=@ID  ";
        private const string Sql_Del = " delete Im_AcceptMsgPeople where ID=@ID ";
        private const string Sql_Del_ByAll = " delete im_AcceptMsgPeople where CompanyId=@CompanyId and OperatorId=@OperatorId and TourAreaId=@TourAreaId ";
        private const string Sql_Select = " select ID,CompanyId,OperatorId,TourAreaId from Im_AcceptMsgPeople ";

        #endregion

        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="ID">订单提醒设置ID</param>
        /// <returns></returns>
        public virtual bool Exists(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Exists);
            base.MQStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);

            return DbHelper.Exists(dc, base.MQStore);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">MQ订单提醒设置实体</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool Add(Model.MQStructure.IMAcceptMsgPeople model)
        {
            if (model == null)
                return false;

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Add);
            base.MQStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            base.MQStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            base.MQStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            base.MQStore.AddInParameter(dc, "TourAreaId", DbType.Int32, model.TourAreaId);
            base.MQStore.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            if (DbHelper.ExecuteSql(dc, base.MQStore) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新一条数据(时间不更新)
        /// </summary>
        /// <param name="model">MQ订单提醒设置实体</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool Update(Model.MQStructure.IMAcceptMsgPeople model)
        {
            if (model == null)
                return false;

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Update);
            base.MQStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            base.MQStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            base.MQStore.AddInParameter(dc, "TourAreaId", DbType.Int32, model.TourAreaId);
            base.MQStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);

            if (DbHelper.ExecuteSql(dc, base.MQStore) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">MQ订单提醒设置ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Del);
            base.MQStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);

            int Result = DbHelper.ExecuteSql(dc, base.MQStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="OperatorId">操作员编号</param>
        /// <param name="TourAreaId">区域ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool Delete(string CompanyId, string OperatorId, int TourAreaId)
        {
            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Del_ByAll);
            base.MQStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            base.MQStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, OperatorId);
            base.MQStore.AddInParameter(dc, "TourAreaId", DbType.Int32, TourAreaId);

            int Result = DbHelper.ExecuteSql(dc, base.MQStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ID">MQ订单提醒设置ID</param>
        /// <returns>MQ订单提醒设置对象实体</returns>
        public virtual Model.MQStructure.IMAcceptMsgPeople GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;

            Model.MQStructure.IMAcceptMsgPeople model = null;
            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Select + " where ID=@ID  ");
            base.MQStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMAcceptMsgPeople();
                    model.ID = dr["ID"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.OperatorId = dr["OperatorId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TourAreaId")))
                        model.TourAreaId = int.Parse(dr["TourAreaId"].ToString());
                }
            }

            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="OperatorId">操作员ID</param>
        /// <param name="TourAreaId">区域ID</param>
        /// <returns>返回MQ订单提醒设置实体集合</returns>
        public virtual IList<EyouSoft.Model.MQStructure.IMAcceptMsgPeople> GetAcceptMsgList(string CompanyId, string OperatorId
            , int TourAreaId)
        {
            string strWhere = Sql_Select + " where 1 = 1 ";
            if (!string.IsNullOrEmpty(CompanyId))
                strWhere += string.Format(" and CompanyId = '{0}' ", CompanyId);
            if (!string.IsNullOrEmpty(OperatorId))
                strWhere += string.Format(" and OperatorId = '{0}' ", OperatorId);
            if (TourAreaId > 0)
                strWhere += string.Format(" and TourAreaId = {0} ", TourAreaId);

            DbCommand dc = base.MQStore.GetSqlStringCommand(strWhere);
            return GetQueryList(dc);
        }

        #endregion  成员方法

        #region 私有函数

        /// <summary>
        /// 执行查询返回实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>返回实体集合</returns>
        private IList<EyouSoft.Model.MQStructure.IMAcceptMsgPeople> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.MQStructure.IMAcceptMsgPeople> List = new List<EyouSoft.Model.MQStructure.IMAcceptMsgPeople>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                EyouSoft.Model.MQStructure.IMAcceptMsgPeople model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMAcceptMsgPeople();
                    model.ID = dr["ID"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.OperatorId = dr["OperatorId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TourAreaId")))
                        model.TourAreaId = int.Parse(dr["TourAreaId"].ToString());

                    List.Add(model);
                }
                model = null;
            }
            return List;
        }

        #endregion
    }
}
