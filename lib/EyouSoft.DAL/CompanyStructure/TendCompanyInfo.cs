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
    /// 创建人：张志瑜 2010-05-28
    /// 描述：经营单位数据访问层
    /// </summary>
    public class TendCompanyInfo : DALBase, ITendCompanyInfo
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TendCompanyInfo() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        /// <summary>
        /// 添加经营单位
        /// </summary>
        private const string SQL_TendCompanyInfo_Insert = "INSERT INTO tbl_TendCompanyInfo(Id,CompayId,AreaID,ProvinceId,CityId,CompanyName,CompanyBrand,CompanyAddress,License,ContactName,ContactTel,ContactFax,ContactMobile,ContactEmail,ContactMQ,ContactQQ,ContactMSN,Remark,IsEnabled) VALUES(@Id,@CompayId,@AreaID,@ProvinceId,@CityId,@CompanyName,@CompanyBrand,@CompanyAddress,@License,@ContactName,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactMQ,@ContactQQ,@ContactMSN,@Remark)";
        /// <summary>
        /// 修改经营单位
        /// </summary>
        private const string SQL_TendCompanyInfo_UPDATE = "UPDATE tbl_TendCompanyInfo SET CompanyName=@CompanyName,CompanyBrand=@CompanyBrand,CompanyAddress=@CompanyAddress,License=@License,ContactName=@ContactName,ContactTel=@ContactTel,ContactFax=@ContactFax,ContactMobile=@ContactMobile,ContactEmail=@ContactEmail,ContactMQ=@ContactMQ,ContactQQ=@ContactQQ,ContactMSN=@ContactMSN,Remark=@Remark WHERE ID=@ID";
        /// <summary>
        /// 删除经营单位
        /// </summary>
        private const string SQL_TendCompanyInfo_DELEDTE = "DELEDTE tbl_TendCompanyInfo WHERE companyId=@companyId AND areaId=@areaId";
        /// <summary>
        /// 获取经营单位实体
        /// </summary>
        private const string SQL_TendCompanyInfo_SELECT = "SELECT Id,CompayId,AreaID,ProvinceId,CityId,CompanyName,CompanyBrand,CompanyAddress,License,ContactName,ContactTel,ContactFax,ContactMobile,ContactEmail,ContactMQ,ContactQQ,ContactMSN,Remark,IsEnabled from tbl_TendCompanyInfo WHERE CompanyID=@companyid AND AreaID=@areaid";
        /// <summary>
        /// 经营单位单位是否已存在
        /// </summary>
        private const string SQL_TendCompanyInfo_SELECT_EXISTS = "SELECT COUNT(*) FROM tbl_TendCompanyInfo WHERE CompanyId=@CompanyId AND AreaID=@AreaID";
        #endregion

        /// <summary>
        /// 添加经营单位
        /// </summary>
        /// <param name="model">经营单位实体类</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.TendCompanyInfo model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanyInfo_Insert);
            this._database.AddInParameter(dc, "Id", DbType.String, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "CompayId", DbType.String, model.CompanyID);
            this._database.AddInParameter(dc, "AreaID", DbType.Int32, model.AreaID);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "CompanyName", DbType.String,model.CompanyName);
            this._database.AddInParameter(dc, "CompanyBrand", DbType.String, model.CompanyBrand);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            this._database.AddInParameter(dc, "License", DbType.String, model.License);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactMQ", DbType.String, model.ContactInfo.MQ);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._database.AddInParameter(dc, "IsEnabled", DbType.String, model.IsEnabled);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 修改经营单位
        /// </summary>
        /// <param name="model">经营单位实体类</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.TendCompanyInfo model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanyInfo_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.String, model.ID);            
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "CompanyBrand", DbType.String, model.CompanyBrand);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            this._database.AddInParameter(dc, "License", DbType.String, model.License);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ContactMQ", DbType.String, model.ContactInfo.MQ);
            this._database.AddInParameter(dc, "ContactQQ", DbType.String, model.ContactInfo.QQ);
            this._database.AddInParameter(dc, "ContactMSN", DbType.String, model.ContactInfo.MSN);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除经营单位
        /// </summary>
        /// <param name="companyid">经营单位所属公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        public virtual bool Delete(string companyid, int areaId)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanyInfo_DELEDTE);
            this._database.AddInParameter(dc, "companyId", DbType.String, companyid);
            this._database.AddInParameter(dc, "areaId", DbType.Int32, areaId);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取经营单位实体
        /// </summary>
        /// <param name="companyId">经营单位所属公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.TendCompanyInfo GetList(string companyId, int areaId)
        {
            EyouSoft.Model.CompanyStructure.TendCompanyInfo model = null;
            DbCommand dc=this._database.GetSqlStringCommand(SQL_TendCompanyInfo_SELECT);
            this._database.AddInParameter(dc,"CompanyID", DbType.String, companyId);
            this._database.AddInParameter(dc,"AreaID", DbType.Int32, areaId);
            using(IDataReader dr=DbHelper.ExecuteReader(dc,this._database))
            {
                if(dr.Read())
                {
                    model=new EyouSoft.Model.CompanyStructure.TendCompanyInfo();
                    model.AreaID=dr.GetInt32(dr.GetOrdinal("AreaID"));
                    model.CityId=dr.GetInt32(dr.GetOrdinal("CityId"));
                    model.CompanyAddress=dr.GetString(dr.GetOrdinal("CompanyAddress"));
                    model.CompanyBrand=dr.GetString(dr.GetOrdinal("CompanyBrand"));
                    model.CompanyID=dr.GetString(dr.GetOrdinal("CompanyID"));
                    model.CompanyName=dr.GetString(dr.GetOrdinal("CompanyName"));
                    model.ContactInfo.Email=dr.GetString(dr.GetOrdinal("ContactEmail"));
                    model.ContactInfo.Fax=dr.GetString(dr.GetOrdinal("ContactFax"));
                    model.ContactInfo.Mobile=dr.GetString(dr.GetOrdinal("ContactMobile"));
                    model.ContactInfo.MQ= dr.GetString(dr.GetOrdinal("ContactMQ"));
                    model.ContactInfo.MSN = dr.GetString(dr.GetOrdinal("ContactMSN"));
                    model.ContactInfo.ContactName = dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactInfo.QQ = dr.GetString(dr.GetOrdinal("ContactQQ"));
                    model.ContactInfo.Tel = dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ID=dr.GetString(dr.GetOrdinal("ID"));
                    model.IsEnabled=dr.GetString(dr.GetOrdinal("IsEnabled"))=="1"?true:false;
                    model.License=dr.GetString(dr.GetOrdinal("License"));
                    model.ProvinceId=dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    model.Remark=dr.GetString(dr.GetOrdinal("Remark"));
                }
            }

            return model;
        }

        /// <summary>
        /// 当前公司,线路区域ID下,经营单位是否已存在
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="areaid">线路区域ID</param>
        /// <returns></returns>
        public virtual bool IsExists(string companyid, string areaid)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_TendCompanyInfo_SELECT_EXISTS);
            this._database.AddInParameter(dc, "companyid", DbType.String, companyid);
            this._database.AddInParameter(dc, "areaid", DbType.String, areaid);
            return DbHelper.Exists(dc, this._database);
        }
    }
}
