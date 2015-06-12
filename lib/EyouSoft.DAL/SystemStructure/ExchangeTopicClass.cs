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
    /// 创建人：鲁功源 2010-05-24
    /// 描述：互动交流栏目数据层
    /// </summary>
    public class ExchangeTopicClass:DALBase,IExchangeTopicClass
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeTopicClass() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_ExchangeTopicClass_SELECT = "SELECT ID,TopicClassName,TopicCssClass from tbl_ExchangeTopicClass order by SortId desc";
        #endregion

        #region 成员方法
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.ExchangeTopicClass> GetList()
        {
            IList<EyouSoft.Model.SystemStructure.ExchangeTopicClass> list = new List<EyouSoft.Model.SystemStructure.ExchangeTopicClass>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeTopicClass_SELECT);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.ExchangeTopicClass model = new EyouSoft.Model.SystemStructure.ExchangeTopicClass();
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.TopicClassName = dr.GetString(dr.GetOrdinal("TopicClassName"));
                    model.TopicCssClass = dr.GetString(dr.GetOrdinal("TopicCssClass"));
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion
    }
}
