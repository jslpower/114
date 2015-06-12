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
    /// 创建人：鲁功源 2010-07-01
    /// 描述：单位采购目录业务层接口
    /// </summary>
    public class CompanyFavor:DALBase,IDAL.CompanyStructure.ICompanyFavor
    {
         #region 构造函数
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
         public CompanyFavor() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

         #region SQL变量定义
         private const string SQL_CompanyFavor_GetAllFavorCount = "SELECT COUNT(1) from tbl_CompanyFavor WHERE CompanyId=@CompanyId {0}";
         private const string SQL_CompanyFavor_GetListByCompanyId = "SELECT FavorCompanyId from tbl_CompanyFavor WHERE CompanyId=@CompanyId {0}";
         private const string SQL_CompanyFavor_DELETE = "DELETE tbl_CompanyFavor WHERE CompanyId=@CompanyId and FavorCompanyId=@FavorCompanyId ";
         private const string SQL_CompanyFavor_INSERT = "INSERT INTO tbl_CompanyFavor(CompanyId,FavorCompanyId,AreaId) VALUES(@CompanyId,@FavorCompanyId,@AreaId) ";
         private const string SQL_CompanyFavor_GetAllFavorArea = "SELECT distinct AreaId,(SELECT AreaName FROM tbl_SysArea WHERE ID=AreaId) as AreaName FROM tbl_CompanyAreaConfig WHERE CompanyId IN (SELECT [FavorCompanyId] FROM [tbl_CompanyFavor] WHERE [CompanyId]=@CompanyId)";
         #endregion

         #region ICompanyFavor成员
         /// <summary>
         /// 获取公司采购目录被收藏公司的区域编号集合
         /// </summary>
         /// <param name="CompanyId">公司编号</param>
         /// <returns></returns>
         public IList<EyouSoft.Model.SystemStructure.AreaBase> GetAllFavorArea(string CompanyId)
         {
             IList<EyouSoft.Model.SystemStructure.AreaBase> FavoraAread = new List<EyouSoft.Model.SystemStructure.AreaBase>();
             DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyFavor_GetAllFavorArea);
             this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
             using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
             {
                 EyouSoft.Model.SystemStructure.AreaBase area = null;
                 while (dr.Read())
                 {
                     area = new EyouSoft.Model.SystemStructure.AreaBase();
                     area.AreaId = dr.GetInt32(0);
                     area.AreaName = dr.GetString(1);
                     if(!FavoraAread.Contains(area))
                        FavoraAread.Add(area);
                 }
             }
             return FavoraAread;
         }
         /// <summary>
         /// 获取指定公司指定线路区域下的采购目录总数
         /// </summary>
         /// <param name="CompanyId">公司编号</param>
         /// <param name="AreaId">线路区域编号 =0时返回全部</param>
         /// <returns></returns>
         public virtual int GetAllFavorCount(string CompanyId, int AreaId)
         {
             string strSql = string.Format(SQL_CompanyFavor_GetAllFavorCount, AreaId > 0 ? " and AreaId=" + AreaId.ToString() : string.Empty);
             DbCommand dc = this._database.GetSqlStringCommand(strSql);
             this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
             return int.Parse(DbHelper.GetSingle(dc, this._database).ToString());
         }
         /// <summary>
         /// 获取指定公司指定线路区域下的被收藏公司的编号集合
         /// </summary>
         /// <param name="CompanyId">公司编号</param>
         /// <param name="AreaId">线路区域编号 =0时返回全部</param>
         /// <returns></returns>
         public virtual IList<string> GetListByCompanyId(string CompanyId, int AreaId)
         {
             IList<string> FavorCompanyIds=new List<string>();
             string strSql = string.Format(SQL_CompanyFavor_GetListByCompanyId, AreaId > 0 ? " and AreaId=" + AreaId.ToString() : string.Empty);
             DbCommand dc = this._database.GetSqlStringCommand(strSql);
             this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
             using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
             {
                 while (dr.Read())
                 {
                     if (!FavorCompanyIds.Contains(dr[0].ToString()))
                     {
                         FavorCompanyIds.Add(dr[0].ToString());
                     }
                 }
             }
             return FavorCompanyIds;
         }
         /// <summary>
         /// 保存采购目录
         /// </summary>
         /// <param name="model">采购目录实体</param>
         /// <returns>true:成功 false:失败</returns>
         public virtual bool SaveCompanyFavor(EyouSoft.Model.CompanyStructure.CompanyFavor model)
         {
             DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyFavor_INSERT);
             this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
             this._database.AddInParameter(dc, "FavorCompanyId", DbType.AnsiStringFixedLength, model.FavorCompanyId);
             this._database.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);
             return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
         }
        /// <summary>
        /// 删除指定公司的指定区域的采购目录
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
         /// <param name="FavorCompanyId">被收藏公司编号</param>
         /// <returns>true:成功 false:失败</returns>
         public virtual bool Delete(string CompanyID, string FavorCompanyId)
         {
             DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyFavor_DELETE);
             this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyID);
             this._database.AddInParameter(dc, "FavorCompanyId", DbType.AnsiStringFixedLength, FavorCompanyId);
             return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
         }
         #endregion
    }
}
