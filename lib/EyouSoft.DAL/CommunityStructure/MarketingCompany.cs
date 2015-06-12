using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 营销公司数据操作
    /// </summary>
    /// Author:zhengfj 2011-5-26
    public class MarketingCompany : DALBase, EyouSoft.IDAL.CommunityStructure.IMarketingCompany
    {
        #region 变量
        Database _database = null;
        #endregion
        #region 构造函数
        public MarketingCompany()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL
        private const string SQL_MarketingCompany_Add = "INSERT INTO tbl_MarketingCompany(CompanyName,ProvinceId,CityId,Contact,ContactWay,Question,Category) VALUES(@CompanyName,@ProvinceId,@CityId,@Contact,@ContactWay,@Question,@Category)";
        private const string SQL_MarketingCompany_Delete = "DELETE FROM tbl_MarketingCompany WHERE CHARINDEX(','+LTRIM(ID)+',',','+@ids+',') > 0";
        #endregion

        #region IMarketingCompany 成员
        /// <summary>
        /// 添加营销公司
        /// </summary>
        /// <param name="item">营销公司实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.CommunityStructure.MarketingCompany item)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_MarketingCompany_Add);
            this._database.AddInParameter(comm, "@CompanyName", DbType.String, item.CompanyName);
            this._database.AddInParameter(comm, "@ProvinceId", DbType.Int32, item.ProvinceId);
            this._database.AddInParameter(comm, "@CityId", DbType.Int32, item.CityId);
            this._database.AddInParameter(comm, "@Contact",DbType.String,item.Contact);
            this._database.AddInParameter(comm, "@ContactWay",DbType.String,item.ContactWay);
            this._database.AddInParameter(comm, "@Question", DbType.String,item.Question);
            this._database.AddInParameter(comm, "@Category", DbType.Byte, (int)item.MarketingCompanyType);

            return DbHelper.ExecuteSql(comm, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 单个/批量删除营销公司
        /// </summary>
        /// <param name="ids">编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string ids)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_MarketingCompany_Delete);
            this._database.AddInParameter(comm, "@ids", DbType.String, ids);

            return DbHelper.ExecuteSql(comm, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 分页获取营销公司数据
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="provinceId">省份,null不作条件</param>
        /// <param name="cityId">城市,null不作条件</param>
        /// <param name="marketingCompany">营销公司类型</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="startDate">开始时间(注册)</param>
        /// <param name="endDate">结束时间(注册)</param>
        /// <returns>营销公司集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.MarketingCompany> GetMarketingCompany(int pageSize, int pageIndex,
            ref int recordCount, int? provinceId, int? cityId, EyouSoft.Model.CommunityStructure.MarketingCompanyType? marketingCompanyType,
            string companyName, DateTime? startDate, DateTime? endDate)
        {
            string tableName = "tbl_MarketingCompany";
            string fields = "ID,CompanyName,ProvinceId,CityId,Contact,ContactWay,Question,Category,RegisterDate";
            string identityColumnName= "ID";
            StringBuilder query = new StringBuilder(" 1=1 ");
            string orderBy = " RegisterDate DESC";

            if (provinceId.HasValue)                                //省份
            {
                query.AppendFormat(" AND ProvinceId = {0}", provinceId);
            }   
            if (cityId.HasValue)                                   //城市
            {
                query.AppendFormat(" AND CityId = {0}", cityId);
            }
            if (marketingCompanyType.HasValue)                     //营销公司类型
            {
                query.AppendFormat(" AND Category = {0}", (int)marketingCompanyType);
            }
            if (!string.IsNullOrEmpty(companyName))                 //单位名称
            {
                query.AppendFormat(" AND CompanyName like'%{0}%'", companyName);
            }
            if (startDate.HasValue)                  //开始时间
            {
                query.AppendFormat(" AND DATEDIFF(dd,RegisterDate,'{0}') <= 0", startDate);
            }
            if (endDate.HasValue)                   //结束时间
            {
                query.AppendFormat(" AND DATEDIFF(dd,RegisterDate,'{0}') >= 0", endDate);
            }

            IList<EyouSoft.Model.CommunityStructure.MarketingCompany> list = new List<EyouSoft.Model.CommunityStructure.MarketingCompany>();

            EyouSoft.Model.CommunityStructure.MarketingCompany item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount,
                tableName, identityColumnName, fields, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new EyouSoft.Model.CommunityStructure.MarketingCompany()
                    {
                        ID = (int)reader["ID"],
                        CompanyName = reader["CompanyName"].ToString(),
                        ProvinceId = (int)reader["ProvinceId"],
                        CityId = (int)reader["CityId"],
                        Contact = reader["Contact"].ToString(),
                        ContactWay = reader["ContactWay"].ToString(),
                        Question = reader.IsDBNull(reader.GetOrdinal("Question")) ? string.Empty : reader["Question"].ToString(),
                        MarketingCompanyType = (EyouSoft.Model.CommunityStructure.MarketingCompanyType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.MarketingCompanyType), reader["Category"].ToString()),
                        RegisterDate = DateTime.Parse(reader["RegisterDate"].ToString())
                    });
                }
            }

            return list;
        }

        #endregion
    }
}
