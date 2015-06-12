using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.CommunityStructure;
using EyouSoft.Model.CommunityStructure;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 二次整改供求焦点图片数据访问层类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class DSupplyDemandPic : DALBase, IDSupplyDemandPic
    {
        private const string SQL_DELETE = "DELETE FROM tbl_SupplyDemandRules where Category = {0} ;";
        private const string SQL_INSERT = "INSERT INTO tbl_SupplyDemandRules(PicPath,LinkAddress,Category,OperateId,OperateTime) values('{0}','{1}','{2}','{3}',getdate());";
        private const string SQL_SELECT = "SELECT Id,PicPath,LinkAddress,Category,OperateId,OperateTime from tbl_SupplyDemandRules where Category={0}";
        
        #region 数据库变量

        private Database _db = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSupplyDemandPic()
        {
            this._db = base.SystemStore;
        }

        #endregion

        #region IDSupplyDemandPic 成员

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(List<MSupplyDemandPic> list)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(SQL_DELETE, (int)CategoryEnum.供求图片);
            foreach (MSupplyDemandPic item in list)
            {
                if (item != null)
                {
                    strSql.AppendFormat(SQL_INSERT, item.PicPath, item.LinkAddress, (int)CategoryEnum.供求图片, item.OperateId);
                }
            }
            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSqlTrans(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IList<MSupplyDemandPic> GetList()
        {
            IList<MSupplyDemandPic> list = new List<MSupplyDemandPic>();
            MSupplyDemandPic model = null;
            DbCommand dc = this._db.GetSqlStringCommand(string.Format(SQL_SELECT,(int)CategoryEnum.供求图片));
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new MSupplyDemandPic();
                    if (!dr.IsDBNull(dr.GetOrdinal("Category")))
                        model.Category = (CategoryEnum)int.Parse(dr[dr.GetOrdinal("Category")].ToString());
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath")) ? string.Empty : dr[dr.GetOrdinal("PicPath")].ToString();
                    model.LinkAddress = dr.IsDBNull(dr.GetOrdinal("LinkAddress")) ? string.Empty : dr[dr.GetOrdinal("LinkAddress")].ToString();
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        #endregion
    }
}
