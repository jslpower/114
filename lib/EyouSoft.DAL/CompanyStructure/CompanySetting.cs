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
    /// 创建人：张志瑜 2010-05-27
    /// 描述：单位设置数据访问
    /// </summary>
    public class CompanySetting : DALBase, EyouSoft.IDAL.CompanyStructure.ICompanySetting
    {
        #region SQL
        private const string SQL_CompanySetting_SELECT_Model = "SELECT CompanyId,FieldName,FieldValue FROM [tbl_CompanySetting] WHERE [CompanyId]=@CompanyId";
        private const string SQL_CompanySetting_SELECT_String = "SELECT FieldValue FROM [tbl_CompanySetting] WHERE [CompanyId]=@CompanyId and FieldName=@FieldName";
        private const string SQL_CompanySetting_DELETE = "DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName=@FieldName;";
        private const string SQL_CompanySetting_INSERT = "INSERT INTO tbl_CompanySetting (CompanyId,FieldName,FieldValue) VALUES(@CompanyId,@FieldName,@FieldValue);";
        #endregion

        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanySetting()
        {
            this._database = base.CompanyStore;
        }

        #region 成员方法
        /// <summary>
        /// 修改单位设置
        /// </summary>
        /// <param name="model">单位设置实体</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.CompanySetting model)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanySetting_Update");
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "FirstMenu", DbType.Int32, Convert.ToInt32(model.FirstMenu));
            this._database.AddInParameter(dc, "OrderRefresh", DbType.Int32, model.OrderRefresh);
            this._database.AddInParameter(dc, "TourStopTime", DbType.Int32, model.TourStopTime);
            int rows = DbHelper.RunProcedureWithResult(dc, this._database);
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新单位设置--优先展示栏目位置
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="firstMenu">要优先显示的栏目</param>
        /// <returns></returns>
        public virtual bool UpdateFirstMenu(string companyId, EyouSoft.Model.CompanyStructure.MenuSection firstMenu)
        {
            return this.UpdateCompanySetting(companyId, "FirstMenu", Convert.ToInt32(firstMenu).ToString());
        }

        /// <summary>
        /// 更新单位设置--订单刷新时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="minute">刷新时间(单位分钟)</param>
        /// <returns></returns>
        public virtual bool UpdateOrderRefresh(string companyId, int minute)
        {
            return this.UpdateCompanySetting(companyId, "OrderRefresh", minute.ToString());
        }

        /// <summary>
        /// 更新单位设置--团队自动停收时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="day">团队自动停收时间(单位天)</param>
        /// <returns></returns>
        public virtual bool UpdateTourStopTime(string companyId, int day)
        {
            return this.UpdateCompanySetting(companyId, "TourStopTime", day.ToString());
        }

        /// <summary>
        /// 获得单位设置实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanySetting GetModel(string companyid)
        {
            //若数据库中无记录,则返回默认值
            EyouSoft.Model.CompanyStructure.CompanySetting model = new EyouSoft.Model.CompanyStructure.CompanySetting();
            model.CompanyId = companyid;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanySetting_SELECT_Model);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyid);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    string key = rdr.IsDBNull(rdr.GetOrdinal("FieldName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("FieldName"));
                    string value = rdr.IsDBNull(rdr.GetOrdinal("FieldValue")) == true ? "0" : rdr.GetString(rdr.GetOrdinal("FieldValue"));
                    switch (key)
                    {
                        case "FirstMenu":
                            model.FirstMenu = (EyouSoft.Model.CompanyStructure.MenuSection)Convert.ToInt32(value);
                            break;
                        case "OrderRefresh":
                            model.OrderRefresh = Convert.ToInt32(value);
                            break;
                        case "TourStopTime":
                            model.TourStopTime = Convert.ToInt32(value);
                            break;
                        case "OperatorLimit":
                            model.OperatorLimit = Convert.ToInt32(value);
                            break;
                        case "PositionInfo":
                            model.PositionInfo = new EyouSoft.Model.ShopStructure.PositionInfo();
                            string[] strArr = value.Split(',');
                            if (strArr == null || strArr.Length <2)
                                break;
                            model.PositionInfo.Longitude = double.Parse(strArr[0]);
                            model.PositionInfo.Latitude = double.Parse(strArr[1]);
                            if(strArr.Length==3)
                                model.PositionInfo.ZoomLevel = int.Parse(strArr[2]);
                            break;
                    }
                    
                }
            }
            return model;
        }

        /// <summary>
        /// 更新公司地理位置信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="PositionInfo">地理位置信息实体</param>
        /// <returns></returns>
        public virtual bool UpdateCompanyPositionInfo(string companyId, EyouSoft.Model.ShopStructure.PositionInfo PositionInfo)
        {
            if (string.IsNullOrEmpty(companyId) || PositionInfo == null)
                return false;

            return this.UpdateCompanySetting(companyId, "PositionInfo", PositionInfo.Longitude.ToString() + ',' + PositionInfo.Latitude.ToString()+","+
                PositionInfo.ZoomLevel.ToString());
        }

        /// <summary>
        /// 根据公司ID和字段名称获取字段值
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="FieldName">字段名称</param>
        /// <returns>字段值</returns>
        public virtual string GetFieldValueByCompanyId(string companyId, string FieldName)
        {
            string strReturn = string.Empty;
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(FieldName))
                return strReturn;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanySetting_SELECT_String);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "FieldName", DbType.String, FieldName);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (rdr.Read())
                {
                    strReturn = rdr[0].ToString();
                }
            }
            return strReturn;
        }
        
        #endregion 成员方法

        #region 私有方法
        /// <summary>
        /// 修改设置
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="FieldName">字段名称</param>
        /// <param name="FieldValue">字段值</param>
        /// <returns></returns>
        private bool UpdateCompanySetting(string companyId, string FieldName, string FieldValue)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanySetting_DELETE + SQL_CompanySetting_INSERT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "FieldName", DbType.String, FieldName);
            this._database.AddInParameter(dc, "FieldValue", DbType.String, FieldValue);
            int rows = DbHelper.ExecuteSql(dc, this._database);
            if (rows > 0)
                return true;
            else
                return false;
        }
        #endregion 私有方法
    }
}
