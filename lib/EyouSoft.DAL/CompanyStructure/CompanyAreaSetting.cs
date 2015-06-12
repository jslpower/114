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
    /// 创建人：张志瑜 2010-06-01
    /// 描述：单位区域设置数据访问层
    /// </summary>
    public class CompanyAreaSetting : DALBase, EyouSoft.IDAL.CompanyStructure.ICompanyAreaSetting
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyAreaSetting() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL语句
        /// <summary>
        /// 新增单位区域设置
        /// </summary>
        private const string SQL_TendCompanySetting_INSERT = "INSERT INTO [tbl_CompanyAreaConfig] ([CompanyId],[AreaId],[PrefixText],[IsShowOrderSite]) VALUES (@CompanyId,@AreaId,@PrefixText,@IsShowOrderSite)";
        /// <summary>
        /// 修改单位区域设置
        /// </summary>
        private const string SQL_TendCompanySetting_UPDATE = "UPDATE [tbl_CompanyAreaConfig] SET [PrefixText]=@PrefixText,[IsShowOrderSite]=@ShowOrderSite WHERE [CompanyId]=@CompanyId,[AreaId]=@AreaId";
        /// <summary>
        /// 删除单位区域设置
        /// </summary>
        private const string SQL_TendCompanySetting_DELETE = "DELETE FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyId,[AreaId]=@AreaId";
        /// <summary>
        /// 查询单位区域设置是否存在
        /// </summary>
        private const string SQL_TendCompanySetting_SELECT_IsExists = "SELECT COUNT(*) FROM [tbl_TendCompanyConfig] WHERE [CompanyId]=@CompanyId,[AreaId]=@AreaId";
        /// <summary>
        /// 查询单位区域设置实体
        /// </summary>
        private const string SQL_TendCompanySetting_SELECT_Model = "SELECT [CompanyId],[AreaId],[PrefixText],[IsShowOrderSite] FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyId AND [AreaId]=@AreaId";
        /// <summary>
        /// 查询单位区域设置实体
        /// </summary>
        private const string SQL_TendCompanySetting_SELECT_List = "SELECT [CompanyId],[AreaId],[PrefixText],[IsShowOrderSite] FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyId";
        #endregion SQL语句


        /// <summary>
        /// 修改单位区域设置
        /// </summary>
        /// <param name="items">单位设置实体类列表</param>
        /// <returns></returns>
        public virtual bool Update(IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> items)
        {
            if (items == null || items.Count == 0)
                return false;
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyAreaSetting_Update");

            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, items[0].CompanyID);

            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            if (items != null && items.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyAreaSetting item in items)
                {
                    XMLRoot.AppendFormat("<CompanyAreaSetting AreaId='{0}' ", item.AreaID);
                    XMLRoot.AppendFormat(" PrefixText='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(item.PrefixText));
                    XMLRoot.AppendFormat(" IsShowOrderSite='{0}' ", item.IsShowOrderSite);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            int rows = DbHelper.RunProcedureWithResult(dc, this._database);
            if (rows > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获得单位区域设置实体类[若不存在设置信息,则默认返回空值]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyAreaSetting GetModel(string companyId, int areaId)
        {
            EyouSoft.Model.CompanyStructure.CompanyAreaSetting model = new EyouSoft.Model.CompanyStructure.CompanyAreaSetting();
            model.CompanyID = companyId;
            model.AreaID = areaId;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanySetting_SELECT_Model);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "AreaId", DbType.Int32, areaId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (rdr.Read())
                {
                    model.AreaID = rdr.GetInt32(rdr.GetOrdinal("AreaID"));
                    model.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    model.IsShowOrderSite = rdr.GetString(rdr.GetOrdinal("IsShowOrderSite")) == "1" ? true : false;
                    model.PrefixText = rdr.IsDBNull(rdr.GetOrdinal("PrefixText")) == true ? "" : rdr.GetString(rdr.GetOrdinal("PrefixText"));
                }
            }
            return model;
        }       
        /// <summary>
        /// 获得单位区域设置列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> GetList(string companyId)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> items = new List<EyouSoft.Model.CompanyStructure.CompanyAreaSetting>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanySetting_SELECT_List);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyAreaSetting model = new EyouSoft.Model.CompanyStructure.CompanyAreaSetting();
                    model.AreaID = rdr.GetInt32(rdr.GetOrdinal("AreaID"));
                    model.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    model.IsShowOrderSite = rdr.GetString(rdr.GetOrdinal("IsShowOrderSite")) == "1" ? true : false;
                    model.PrefixText = rdr.IsDBNull(rdr.GetOrdinal("PrefixText")) == true ? "" : rdr.GetString(rdr.GetOrdinal("PrefixText"));
                    items.Add(model);
                }
            }

            return items;        
        }

        #region 不要的代码
        ///// <summary>
        ///// 新增经营单位设置
        ///// </summary>
        ///// <param name="model">经营单位设置实体类</param>
        ///// <returns></returns>
        //public virtual bool Add(EyouSoft.Model.CompanyStructure.CompanyAreaSetting model)
        //{
        //    DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanySetting_INSERT);
        //    this._database.AddInParameter(dc, "CompanyId", DbType.String, model.CompanyID);
        //    this._database.AddInParameter(dc, "AreaId", DbType.String, model.AreaID);
        //    this._database.AddInParameter(dc, "PrefixText", DbType.String, model.PrefixText);
        //    this._database.AddInParameter(dc, "IsShowOrderSite", DbType.String, model.IsShowOrderSite);
        //    if (DbHelper.ExecuteSql(dc, this._database) > 0)
        //        return true;
        //    else
        //        return false;
        //}

        ///// <summary>
        ///// 删除经营单位设置
        ///// </summary>
        ///// <param name="companyid">经营单位所属公司ID</param>
        ///// <param name="areaid">线路区域ID</param>
        ///// <returns></returns>
        //public virtual bool Delete(string companyid, string areaid)
        //{
        //    DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanySetting_DELETE);
        //    this._database.AddInParameter(dc, "CompanyId", DbType.String, companyid);
        //    this._database.AddInParameter(dc, "AreaId", DbType.String, areaid);
        //    if (DbHelper.ExecuteSql(dc, this._database) > 0)
        //        return true;
        //    else
        //        return false;
        //}

        ///// <summary>
        ///// 当前公司,线路区域ID下,经营单位设置是否已存在
        ///// </summary>
        ///// <param name="companyid">公司ID</param>
        ///// <param name="areaid">线路区域ID</param>
        ///// <returns></returns>
        //public virtual bool IsExists(string companyid, string areaid)
        //{
        //    DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanySetting_SELECT_IsExists);
        //    this._database.AddInParameter(dc, "CompanyId", DbType.String, companyid);
        //    this._database.AddInParameter(dc, "AreaId", DbType.String, areaid);
        //    return DbHelper.Exists(dc, this._database);
        //}
        #endregion 不要的代码
    }
}
