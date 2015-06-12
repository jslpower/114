using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ToolStructure
{
    /// <summary>
    /// 应付账款信息数据访问层
    /// </summary>
    /// 鲁功源 2010-11-09
    public class Payments : DALBase, EyouSoft.IDAL.ToolStructure.IPayments
    {
        #region 构造函数
        private Database _db = null; 

        /// <summary>
        /// 构造函数
        /// </summary>
         public Payments() 
        {
            this._db = SystemStore;
        }
        #endregion

         private const string Sql_Payments_Insert = " INSERT INTO [tbl_Payments]([Id],[CompanyId],[TourId],[TourNo],[RouteName],[LeaveDate],[CompanyName],[CompanyType],[PCount],[SumPrice],[CheckendPrice],[NoCheckedPrice],[ClearTime],[OperatorId],[IssueTime]) Values(@Id,@CompanyId,@TourId,@TourNo,@RouteName,@LeaveDate,@CompanyName,@CompanyType,@PCount,@SumPrice,0,0,null,@OperatorId,getdate())";
         private const string Sql_Payments_Select = " SELECT [Id],[CompanyId],[TourId],[TourNo],[RouteName],[LeaveDate],[CompanyName],[CompanyType],[PCount],[SumPrice],[CheckendPrice],[NoCheckedPrice],[ClearTime]  FROM [tbl_Payments] ";

         #region IReceivables 成员

         /// <summary>
         /// 添加应付账款信息
         /// </summary>
         /// <param name="model">应付账款信息实体</param>
         /// <returns></returns>
         public virtual bool AddPayments(EyouSoft.Model.ToolStructure.Payments model)
         {
             if (model == null)
                 return false;

             model.Id = Guid.NewGuid().ToString();

             DbCommand dc = this._db.GetSqlStringCommand(Sql_Payments_Insert);

             #region 参数赋值

             this._db.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.Id);
             this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
             this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
             this._db.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, model.TourId);
             this._db.AddInParameter(dc, "TourNo", DbType.String, model.TourNo);
             this._db.AddInParameter(dc, "RouteName", DbType.String, model.RouteName);
             this._db.AddInParameter(dc, "LeaveDate", DbType.DateTime, model.LeaveDate);
             this._db.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
             this._db.AddInParameter(dc, "CompanyType", DbType.String, model.CompanyType);
             this._db.AddInParameter(dc, "PCount", DbType.Int32, model.PCount);
             this._db.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);

             #endregion

             return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
         }

         /// <summary>
         /// 根据ID获取对应的应付账款信息
         /// </summary>
         /// <param name="ReceivablesId">应付账款信息ID</param>
         /// <returns></returns>
         public virtual EyouSoft.Model.ToolStructure.Payments GetModel(string PaymentsId)
         {
             if (string.IsNullOrEmpty(PaymentsId))
                 return null;

             EyouSoft.Model.ToolStructure.Payments model = new EyouSoft.Model.ToolStructure.Payments();
             DbCommand dc = this._db.GetSqlStringCommand(Sql_Payments_Select + " where ID = @ID ");
             this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, PaymentsId);

             using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
             {
                 IList<EyouSoft.Model.ToolStructure.Payments> list = GetSelectModel(dr);
                 if (list == null || list.Count <= 0)
                     return model;

                 model = list[0];
                 list.Clear();
                 list = null;
             }

             return model;
         }

         /// <summary>
         /// 获取应付账款信息
         /// </summary>
         /// <param name="PageSize">每页条数</param>
         /// <param name="PageIndex">当前页数</param>
         /// <param name="RecordCount">总记录数</param>
         /// <param name="OrderIndex">排序索引</param>
         /// <param name="TourNo">団号</param>
         /// <param name="RouteName">线路名称</param>
         /// <param name="SupperName">供应商名称</param>
         /// <param name="SupperType">供应商类型</param>
         /// <param name="StartLeaveDate">开始出团时间</param>
         /// <param name="EndLeaveDate">结束出团时间</param>
         /// <param name="IsCheck">是否只查询已付</param>
         /// <param name="CompanyId">所属供应商编号</param>
         /// <returns></returns>
         public virtual IList<EyouSoft.Model.ToolStructure.Payments> GetList(int PageSize, int PageIndex, ref int RecordCount
             , int OrderIndex, string TourNo, string RouteName, string SupperName, string SupperType, DateTime? StartLeaveDate
             , DateTime? EndLeaveDate, bool IsCheck,string CompanyId)
         {
             IList<EyouSoft.Model.ToolStructure.Payments> list = new List<EyouSoft.Model.ToolStructure.Payments>();
             StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
             string strFiles = " [Id],[CompanyId],[TourId],[TourNo],[RouteName],[LeaveDate],[CompanyName],[CompanyType],[PCount],[SumPrice],[CheckendPrice],[NoCheckedPrice],[ClearTime] ";
             string strOrder = string.Empty;
             switch (OrderIndex)
             {
                 case 0: strOrder = " IssueTime asc "; break;
                 case 1: strOrder = " IssueTime desc "; break;
                 case 2: strOrder = " SumPrice asc "; break;
                 case 3: strOrder = " SumPrice desc "; break;
                 case 4: strOrder = " CheckendPrice asc "; break;
                 case 5: strOrder = " CheckendPrice desc "; break;
                 case 6: strOrder = " NoCheckPrice asc "; break;
                 case 7: strOrder = " NoCheckPrice desc "; break;
                 case 8: strOrder = " ClearTime asc "; break;
                 case 9: strOrder = " ClearTime desc "; break;
             }
             if (!string.IsNullOrEmpty(CompanyId))
                 strWhere.AppendFormat(" and CompanyId='{0}' ", CompanyId);
             if (!string.IsNullOrEmpty(TourNo))
                 strWhere.AppendFormat(" and TourNo = '{0}' ", TourNo);
             if (!string.IsNullOrEmpty(RouteName))
                 strWhere.AppendFormat(" and RouteName like '%{0}%' ", RouteName);
             if (!string.IsNullOrEmpty(SupperName))
                 strWhere.AppendFormat(" and CompanyName like '%{0}%' ", SupperName);
             if (!string.IsNullOrEmpty(SupperType))
                 strWhere.AppendFormat(" and CompanyType like '%{0}%' ", SupperType);
             if (StartLeaveDate.HasValue)
                 strWhere.AppendFormat(" and datediff(dd,'{0}',LeaveDate) >= 0 ", StartLeaveDate.Value.ToShortDateString());
             if (EndLeaveDate.HasValue)
                 strWhere.AppendFormat(" and datediff(dd,'{0}',LeaveDate) <= 0 ", EndLeaveDate.Value.ToShortDateString());
             if (IsCheck)
                 strWhere.AppendFormat(" and SumPrice <= CheckendPrice ");
             else
                 strWhere.AppendFormat(" and SumPrice <> CheckendPrice ");
             using (IDataReader dr = DbHelper.ExecuteReader(this._db, PageSize, PageIndex, ref RecordCount, "tbl_Payments", "ID", strFiles, strWhere.ToString(), strOrder))
             {
                 list = GetSelectModel(dr);
             }

             return list;
         }

         #endregion

         #region 私有函数

         /// <summary>
         /// 应付信息实体赋值
         /// </summary>
         /// <param name="dr">IDataReader</param>
         /// <returns>返回应付信息实体集合</returns>
         private IList<EyouSoft.Model.ToolStructure.Payments> GetSelectModel(IDataReader dr)
         {
             if (dr == null)
                 return null;

             IList<EyouSoft.Model.ToolStructure.Payments> list = new List<EyouSoft.Model.ToolStructure.Payments>();
             EyouSoft.Model.ToolStructure.Payments model = null;

             while (dr.Read())
             {
                 model = new EyouSoft.Model.ToolStructure.Payments();

                 model.Id = dr["ID"].ToString();
                 model.CompanyId = dr["CompanyId"].ToString();
                 model.TourId = dr["TourId"].ToString();
                 model.TourNo = dr["TourNo"].ToString();
                 model.RouteName = dr["RouteName"].ToString();
                 if (!dr.IsDBNull(dr.GetOrdinal("LeaveDate")))
                     model.LeaveDate = DateTime.Parse(dr["LeaveDate"].ToString());
                 model.CompanyName = dr["CompanyName"].ToString();
                 model.CompanyType = dr["CompanyType"].ToString();
                 if (!dr.IsDBNull(dr.GetOrdinal("PCount")))
                     model.PCount = int.Parse(dr["PCount"].ToString());
                 if (!dr.IsDBNull(dr.GetOrdinal("SumPrice")))
                     model.SumPrice = decimal.Parse(dr["SumPrice"].ToString());
                 if (!dr.IsDBNull(dr.GetOrdinal("CheckendPrice")))
                     model.CheckendPrice = decimal.Parse(dr["CheckendPrice"].ToString());
                 if (!dr.IsDBNull(dr.GetOrdinal("NoCheckedPrice")))
                     model.NoCheckedPrice = decimal.Parse(dr["NoCheckedPrice"].ToString());
                 if (!dr.IsDBNull(dr.GetOrdinal("ClearTime")))
                     model.ClearTime = DateTime.Parse(dr["ClearTime"].ToString());

                 list.Add(model);
             }

             return list;
         }

         #endregion
    }
}
