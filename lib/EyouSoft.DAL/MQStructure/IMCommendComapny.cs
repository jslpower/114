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
    /// MQ推荐批发商
    /// </summary>
    /// 周文超 2010-05-11
    public class IMCommendComapny : DALBase, IDAL.MQStructure.IIMCommendComapny
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMCommendComapny()
        { }

        private const string Sql_Add = " INSERT INTO [tbl_CommendComapny]([OperatorId],[OperatorName],[CompanyId],[CompanyName],[DescCompanyName],[DescContactName],[DescContactTel],[DescContactMobile],[DescCompanyArea],[IssueTime]) VALUES (@OperatorId,@OperatorName,@CompanyId,@CompanyName,@DescCompanyName,@DescContactName,@DescContactTel,@DescContactMobile,@DescCompanyArea,@IssueTime) ";
        private const string Sql_Del = " DELETE FROM [tbl_CommendComapny] WHERE ID = @ID ";
        private const string Sql_Select = " SELECT [Id],[OperatorId],[OperatorName],[CompanyId],[CompanyName],[DescCompanyName],[DescContactName],[DescContactTel],[DescContactMobile],[DescCompanyArea],[IssueTime] FROM [tbl_CommendComapny] ";

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">MQ推荐批发商实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int Add(Model.MQStructure.IMCommendComapny model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Add);
            base.MQStore.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            base.MQStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            base.MQStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            base.MQStore.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            base.MQStore.AddInParameter(dc, "DescCompanyName", DbType.String, model.DescCompanyName);
            base.MQStore.AddInParameter(dc, "DescContactName", DbType.String, model.DescContactName);
            base.MQStore.AddInParameter(dc, "DescContactTel", DbType.String, model.DescContactTel);
            base.MQStore.AddInParameter(dc, "DescContactMobile", DbType.String, model.DescContactMobile);
            base.MQStore.AddInParameter(dc, "DescCompanyArea", DbType.String, model.DescCompanyArea);
            base.MQStore.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);

            return DbHelper.ExecuteSql(dc, base.MQStore);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="Id">MQ推荐批发商ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool Delete(int Id)
        {
            if (Id <= 0)
                return false;
            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Del);
            base.MQStore.AddInParameter(dc, "ID", DbType.Int32, Id);

            int Result = DbHelper.ExecuteSql(dc, base.MQStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="OperatorId">推荐人姓名(为null不作条件)</param>
        /// <param name="CompanyId">推荐人公司编号(为null不作条件)</param>
        /// <param name="CompanyName">推荐人公司名称(为null不作条件)</param>
        /// <param name="DescCompanyName">被推荐的公司名称(为null不作条件)</param>
        /// <returns>返回MQ推荐批发商实体集合</returns>
        public virtual IList<Model.MQStructure.IMCommendComapny> GetList(string OperatorId, string CompanyId, string CompanyName
            , string DescCompanyName)
        {
            IList<Model.MQStructure.IMCommendComapny> List = new List<Model.MQStructure.IMCommendComapny>();
            string strWhere = " where 1 = 1 ";
            if (!string.IsNullOrEmpty(OperatorId))
                strWhere += string.Format(" and OperatorId = '{0}' ", OperatorId);
            if (!string.IsNullOrEmpty(CompanyId))
                strWhere += string.Format(" and CompanyId = '{0}' ", CompanyId);
            if (!string.IsNullOrEmpty(CompanyName))
                strWhere += string.Format(" and CompanyName like '%{0}%' ", CompanyName);
            if (!string.IsNullOrEmpty(DescCompanyName))
                strWhere += string.Format(" and DescCompanyName like '%{0}%' ", DescCompanyName);

            DbCommand dc = base.MQStore.GetSqlStringCommand(Sql_Select + strWhere);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MQStore))
            {
                Model.MQStructure.IMCommendComapny model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMCommendComapny();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.Id = int.Parse(dr["Id"].ToString());
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.DescCompanyName = dr["DescCompanyName"].ToString();
                    model.DescContactName = dr["DescContactName"].ToString();
                    model.DescContactTel = dr["DescContactTel"].ToString();
                    model.DescContactMobile = dr["DescContactMobile"].ToString();
                    model.DescCompanyArea = dr["DescCompanyArea"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());

                    List.Add(model);
                }
                model = null;
            }
            return List;
        }
        
        #endregion  成员方法
    }
}
