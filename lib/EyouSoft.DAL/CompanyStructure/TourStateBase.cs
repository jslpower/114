using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-18
    /// 描述：推广类型数据层
    /// </summary>
    public class TourStateBase:DALBase,ITourStateBase
    {

        #region 构造函数
        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourStateBase() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_TourStateBase_UPDATE = "UPDATE tbl_TourStateBase set PromoText=@PromoText WHERE ID=@ID";
        const string SQL_TourStateBase_EXISTS = "SELECT count(0) from tbl_TourStateBase where PromoText=@PromoText and CompanyID=@CompanyID";
        const string SQL_TourStateBase_DELETE = "DELETE tbl_TourStateBase where ID=@ID";
        const string SQL_TourStateBase_SELECT = "SELECT Id,CompanyId,TypeId,PromoText from tbl_TourStateBase";
        #endregion

        #region 成员方法
        /// <summary>
        /// 修改推广类型
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="text">类型描述</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(int id, string text)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TourStateBase_UPDATE);
            this._database.AddInParameter(dc, "PromoText", DbType.String, text);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取推广类型实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>推广类型实体</returns>
        public virtual EyouSoft.Model.CompanyStructure.TourStateBase GetModel(int id)
        {
            EyouSoft.Model.CompanyStructure.TourStateBase model = null;
            string strSql = SQL_TourStateBase_SELECT;
            if (id>0)
            {
                strSql += " WHERE ID=@ID";
            }
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.TourStateBase();
                    model.ID = dr.GetInt32(0);
                    model.CompanyID = dr.GetString(1);
                    model.TypeID = dr.GetByte(2);
                    model.PromoText = dr.GetString(3);
                    model.PromoTextHTML = string.Format("<a href=\"javascript:void(0)\" class=\"state{0}\"><nobr>{1}</nobr></a>", (int)model.TypeID, model.PromoText); 
                }
            }
            return model;
        }
        /// <summary>
        /// 验证指定公司是否存在指定的推广类型
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="text"></param>
        /// <returns>true:存在 false:不存在</returns>
        public virtual bool Exists(string CompanyID, string text)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TourStateBase_EXISTS);
            this._database.AddInParameter(dc, "PromoText", DbType.String, text);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            return DbHelper.Exists(dc, this._database);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(int id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TourStateBase_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取指定公司的所有推广类型
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.TourStateBase> GetList(string CompanyID)
        {
            IList<EyouSoft.Model.CompanyStructure.TourStateBase> list = new List<EyouSoft.Model.CompanyStructure.TourStateBase>();
            string strSql = SQL_TourStateBase_SELECT;
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strSql += " WHERE CompanyID=@CompanyID";
            }
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            this._database.AddInParameter(dc, "CompanyID", DbType.String, CompanyID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.TourStateBase model = new EyouSoft.Model.CompanyStructure.TourStateBase();
                    model.ID = dr.GetInt32(0);
                    model.CompanyID = dr.GetString(1);
                    model.TypeID = dr.GetByte(2);
                    model.PromoText = dr.GetString(3);
                    model.PromoTextHTML = string.Format("<a href=\"javascript:void(0)\" class=\"state{0}\"><nobr>{1}</nobr></a>", (int)model.TypeID, model.PromoText);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
