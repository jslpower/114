using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-08
    /// 描述：公用价格等级数据层
    /// </summary>
    public class CommonPriceStand : DALBase,IDAL.CompanyStructure.ICommonPriceStand
    {
        #region 构造函数
        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;
        /// <summary>
        /// 构造方法
        /// </summary>
        public CommonPriceStand()
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        private const string SQL_CommonPriceStand_ADD = "if not exists (select 1 from tbl_CommonPriceStand where PriceStandName = @PriceStandName) INSERT INTO tbl_CommonPriceStand(ID,TypeId,PriceStandName) VALUES(@ID,@TypeId,@PriceStandName)";
        private const string SQL_CommonPriceStand_UPDATE = "if not exists (select 1 from tbl_CommonPriceStand where PriceStandName = @PriceStandName AND ID<>@ID) UPDATE tbl_CommonPriceStand set PriceStandName=@PriceStandName WHERE TypeId=@TypeId AND ID=@ID";
        private const string SQL_CommonPriceStand_DELETE = "DELETE tbl_CommonPriceStand WHERE ID=@ID AND TypeId=@TypeId";
        private const string SQL_CommonPriceStand_SELECT = "SELECT ID,TypeId,PriceStandName from tbl_CommonPriceStand ";
        #endregion

        #region ICommonPriceStand成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="PriceStandName">价格等级名称</param>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(string PriceStandName, EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommonPriceStand_ADD);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "PriceStandName", DbType.String, PriceStandName);
            this._database.AddInParameter(dc, "TypeId", DbType.Byte, TypeID != null ? (int)TypeID : 0);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="PriceStandName">价格等级名称</param>
        /// <param name="ID">主键编号</param>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(string PriceStandName, string ID, EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommonPriceStand_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "PriceStandName", DbType.String, PriceStandName);
            this._database.AddInParameter(dc, "TypeId", DbType.Byte, TypeID != null ? (int)TypeID : 0);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string ID, EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommonPriceStand_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "TypeId", DbType.Byte, TypeID != null ? (int)TypeID : 0);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取公用价格等级列表
        /// </summary>
        /// <param name="TypeID">价格等级类型 =null时默认0</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> GetList(EyouSoft.Model.CompanyStructure.CommPriceTypeID? TypeID)
        {
            IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> list = new List<EyouSoft.Model.CompanyStructure.CommonPriceStand>();
            StringBuilder strSql = new StringBuilder(SQL_CommonPriceStand_SELECT);
            if (TypeID != null)
            {
                strSql.AppendFormat(" where TypeId={0} ", (int)TypeID);
            }
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CommonPriceStand model = new EyouSoft.Model.CompanyStructure.CommonPriceStand();
                    model.ID = dr[0].ToString();
                    if (!dr.IsDBNull(1))
                        model.TypeID = (EyouSoft.Model.CompanyStructure.CommPriceTypeID)int.Parse(dr[1].ToString());
                    model.PriceStandName = dr[2].ToString();
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
