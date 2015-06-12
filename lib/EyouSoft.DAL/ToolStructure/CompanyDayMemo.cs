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

namespace EyouSoft.DAL.ToolStructure
{
    /// <summary>
    /// 描述：公司备忘录数据访问
    /// </summary>
    /// 创建人：蒋胜蓝 2010-05-13
    public class CompanyDayMemo : DALBase, IDAL.ToolStructure.ICompanyDayMemo
    {
        const string SQL_CompanyDayMemo_ADD = "INSERT INTO tbl_CompanyDayMemo([ID],[CompanyId],[MemoTitle],[MemoText],[UrgentType],[MemoState],[OperatorName],[OperatorId],[MemoTime])VALUES(@ID,@CompanyId,@MemoTitle,@MemoText,@UrgentType,@MemoState,@OperatorName,@OperatorId,@MemoTime)";
        const string SQL_CompanyDayMemo_UPDATE = "UPDATE tbl_CompanyDayMemo SET [MemoTitle] = @MemoTitle,[MemoText] = @MemoText,[UrgentType] = @UrgentType,[MemoState] = @MemoState,[MemoTime] = @MemoTime WHERE ID=@ID";
        const string SQL_CompanyDayMemo_MODEL = "SELECT ID,CompanyId,MemoTitle,MemoText,UrgentType,MemoState,OperatorName,OperatorId,MemoTime,IssueTime FROM tbl_CompanyDayMemo WHERE ID=@ID ";
        const string SQL_CompanyDayMemo_DEL = "DELETE tbl_CompanyDayMemo WHERE ID=@ID";
        const string SQL_CompanyDayMemo_SELECT = "select ID,MemoTitle,MemoText,UrgentType,MemoState,IssueTime,MemoTime FROM tbl_CompanyDayMemo";

        #region ICompanyDayMemo 成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">备忘录实体</param>
        /// <returns></returns>
        public virtual int Add(EyouSoft.Model.ToolStructure.CompanyDayMemo model)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyDayMemo_ADD);
            this.CompanyStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.String, model.CompanyId);
            this.CompanyStore.AddInParameter(dc, "MemoTitle", DbType.String, model.MemoTitle);
            this.CompanyStore.AddInParameter(dc, "MemoText", DbType.String, model.MemoText);
            this.CompanyStore.AddInParameter(dc, "UrgentType", DbType.Int16, model.UrgentType);
            this.CompanyStore.AddInParameter(dc, "MemoState", DbType.Int16, model.MemoState);
            this.CompanyStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this.CompanyStore.AddInParameter(dc, "OperatorId", DbType.String, model.OperatorId);
            this.CompanyStore.AddInParameter(dc, "MemoTime", DbType.DateTime, model.MemoTime);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">备忘录实体</param>
        /// <returns></returns>
        public virtual int Update(EyouSoft.Model.ToolStructure.CompanyDayMemo model)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyDayMemo_UPDATE);
            this.CompanyStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this.CompanyStore.AddInParameter(dc, "MemoTitle", DbType.String, model.MemoTitle);
            this.CompanyStore.AddInParameter(dc, "MemoText", DbType.String, model.MemoText);
            this.CompanyStore.AddInParameter(dc, "UrgentType", DbType.Int16, model.UrgentType);
            this.CompanyStore.AddInParameter(dc, "MemoState", DbType.Int16, model.MemoState);
            this.CompanyStore.AddInParameter(dc, "MemoTime", DbType.DateTime, model.MemoTime);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="MemoId">主键编号</param>
        /// <returns></returns>
        public virtual int Remove(string MemoId)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyDayMemo_DEL);
            this.CompanyStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, MemoId);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }
        /// <summary>
        /// 获取备忘录实体
        /// </summary>
        /// <param name="MemoId">主键编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ToolStructure.CompanyDayMemo GetModel(string MemoId)
        {
            EyouSoft.Model.ToolStructure.CompanyDayMemo model = null;
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyDayMemo_MODEL);
            this.CompanyStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, MemoId);
            DataTable dt = DbHelper.DataTableQuery(dc, this.CompanyStore);
            foreach (DataRow dr in dt.Rows)
            {
                model = new EyouSoft.Model.ToolStructure.CompanyDayMemo();
                model.ID = dr["ID"].ToString();
                model.CompanyId = dr["CompanyId"].ToString();
                model.MemoTitle = dr["MemoTitle"].ToString();
                model.MemoText = dr["MemoText"].ToString();
                if (dr["UrgentType"].ToString() != "")
                {
                    model.UrgentType = (EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType)int.Parse(dr["UrgentType"].ToString());
                }
                if (dr["MemoState"].ToString() != "")
                {
                    model.MemoState = (EyouSoft.Model.ToolStructure.MemoDetailType.MemoState)int.Parse(dr["MemoState"].ToString());
                }
                model.OperatorName = dr["OperatorName"].ToString();
                model.OperatorId = dr["OperatorId"].ToString();
                if (dr["IssueTime"].ToString() != "")
                {
                    model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                }
                if (dr["MemoTime"].ToString() != "")
                {
                    model.MemoTime = DateTime.Parse(dr["MemoTime"].ToString());
                }
                dt.Dispose();
            }            
            return model;
        }
        /// <summary>
        /// 获取指定公司的备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId)
        {
            string strWhere = SQL_CompanyDayMemo_SELECT + " where CompanyId=@CompanyId";
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return GetQueryList(dc);
        }
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="MemoDay">日期</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId, DateTime MemoDay)
        {
            string strWhere = SQL_CompanyDayMemo_SELECT + " where CompanyId=@CompanyId AND DATEDIFF(d,MemoTime,@MemoDay)=0";
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this.CompanyStore.AddInParameter(dc, "MemoDay", DbType.Date, MemoDay);
            return GetQueryList(dc);
        }
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FromDay">开始日期</param>
        /// <param name="ToDay">结束日期</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId, DateTime FromDay, DateTime ToDay)
        {
            string strWhere = SQL_CompanyDayMemo_SELECT + " where CompanyId=@CompanyId AND MemoTime between @FromDay AND @ToDay";
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this.CompanyStore.AddInParameter(dc, "FromDay", DbType.Date, FromDay);
            this.CompanyStore.AddInParameter(dc, "ToDay", DbType.Date, ToDay);
            return GetQueryList(dc);
        }

        /// <summary>
        /// 执行列表查询
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> list = new List<EyouSoft.Model.ToolStructure.CompanyDayMemo>();
            using (IDataReader dr = this.SystemStore.ExecuteReader(dc))
            {
                EyouSoft.Model.ToolStructure.CompanyDayMemo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ToolStructure.CompanyDayMemo();
                    model.ID = dr["ID"].ToString();
                    model.MemoTitle = dr["MemoTitle"].ToString();
                    model.MemoText = dr["MemoText"].ToString();
                    if (dr["UrgentType"].ToString() != "")
                    {
                        model.UrgentType = (EyouSoft.Model.ToolStructure.MemoDetailType.UrgentType)int.Parse(dr["UrgentType"].ToString());
                    }
                    if (dr["MemoState"].ToString() != "")
                    {
                        model.MemoState = (EyouSoft.Model.ToolStructure.MemoDetailType.MemoState)int.Parse(dr["MemoState"].ToString());
                    }
                    if (dr["IssueTime"].ToString() != "")
                    {
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    }
                    if (dr["MemoTime"].ToString() != "")
                    {
                        model.MemoTime = DateTime.Parse(dr["MemoTime"].ToString());
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion
    }
}
