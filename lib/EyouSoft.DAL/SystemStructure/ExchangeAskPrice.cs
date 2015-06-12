using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-21
    /// 描述：互动交流询价数据层
    /// </summary>
    public class ExchangeAskPrice:DALBase,IExchangeAskPrice
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database=null;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeAskPrice() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_ExchangeAskPrice_INSERT = "INSERT INTO tbl_ExchangeAskPrice(ID,AskCompanyId,AskOperatorId,ToCompanyId,IssueTime) VALUES(@ID,@AskCompanyId,@AskOperatorId,@ToCompanyId,@IssueTime)";
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">询价实体类</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.SystemStructure.ExchangeAskPrice model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeAskPrice_INSERT);
            this._database.AddInParameter(dc, "ID", DbType.String, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "AskCompanyId", DbType.String, model.AskCompanyId);
            this._database.AddInParameter(dc, "AskOperatorId", DbType.String, model.AskOperatorId);
            this._database.AddInParameter(dc, "ToCompanyId", DbType.String, model.ToCompanyId);
            this._database.AddInParameter(dc, "IssueTime", DbType.String, DateTime.Now);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        #endregion
    }
}
