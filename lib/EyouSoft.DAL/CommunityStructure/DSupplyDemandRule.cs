using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.CommunityStructure;
using EyouSoft.Model.CommunityStructure;
using System.Data.Common;
using EyouSoft.Common.DAL;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 二次整改供求规则数据访问层类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class DSupplyDemandRule : DALBase, IDSupplyDemandRule
    {
        private const string SQL_DELETE = "DELETE FROM tbl_SupplyDemandRules where Category = {0} ;";
        private const string SQL_INSERT = "INSERT INTO tbl_SupplyDemandRules(NewsTitle,LinkAddress,NewsContent,Category,OperateId,OperateTime) values('{0}','{1}','{2}','{3}','{4}',getdate());";
        private const string SQL_SELECT = "SELECT Id,NewsTitle,LinkAddress,NewsContent,Category,OperateId,OperateTime from tbl_SupplyDemandRules where Category={0}";

        #region 数据库变量

        private Database _db = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSupplyDemandRule()
        {
            this._db = base.SystemStore;
        }

        #endregion

        #region IDSupplyDemandRoule 成员

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(List<MSupplyDemandRule> list)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_DELETE, (int)CategoryEnum.供求规则);
            foreach (MSupplyDemandRule item in list)
            {
                if (item != null)
                {
                    strSql.AppendFormat(SQL_INSERT, item.NewsTitle, item.LinkAddress, item.NewsContent, (int)CategoryEnum.供求规则, item.OperateId);
                }
            }
            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSqlTrans(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IList<MSupplyDemandRule> GetList()
        {
            IList<MSupplyDemandRule> list = new List<MSupplyDemandRule>();
            MSupplyDemandRule model = null;
            DbCommand dc = this._db.GetSqlStringCommand(string.Format(SQL_SELECT, (int)CategoryEnum.供求规则));
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new MSupplyDemandRule();
                    if (!dr.IsDBNull(dr.GetOrdinal("Category")))
                        model.Category = (CategoryEnum)int.Parse(dr[dr.GetOrdinal("Category")].ToString());
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.NewsTitle = dr.IsDBNull(dr.GetOrdinal("NewsTitle")) ? string.Empty : dr[dr.GetOrdinal("NewsTitle")].ToString();
                    model.LinkAddress = dr.IsDBNull(dr.GetOrdinal("LinkAddress")) ? string.Empty : dr[dr.GetOrdinal("LinkAddress")].ToString();
                    model.NewsContent = dr.IsDBNull(dr.GetOrdinal("NewsContent")) ? string.Empty : dr[dr.GetOrdinal("NewsContent")].ToString();
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        #endregion
    }
}
