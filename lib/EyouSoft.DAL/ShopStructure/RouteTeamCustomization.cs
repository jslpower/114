using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.ShopStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ShopStructure
{
    /// <summary>
    /// 创建人：luofx 2010-11-09
    /// 描述：高级网店-团队制定DAL
    /// </summary>
    public class RouteTeamCustomization : DALBase, IRouteTeamCustomization
    {
        #region 私有变量
        /// <summary>
        /// 获取model执行SQL 
        /// </summary>
        private const string SQL_RouteTeamCustomization_Select = "SELECT [Id],[OperatorId],[CompanyId],[PlanDate],[PlanPeopleNum],[TravelContent],[ResideContent],[DinnerContent],[CarContent],[GuideContent],[ShoppingInfo],[OtherContent],[ContactName],[ContactCompanyName],[ContactTel],[IssueTime]  FROM tbl_RouteTeamCustomization WHERE [Id]=@Id ";
        /// <summary>
        /// 新增执行SQL 
        /// </summary>
        private const string SQL_RouteTeamCustomization_Add = "INSERT INTO tbl_RouteTeamCustomization ([OperatorId],[CompanyId],[PlanDate],[PlanPeopleNum],[TravelContent],[ResideContent],[DinnerContent],[CarContent],[GuideContent],[ShoppingInfo],[OtherContent],[ContactName],[ContactCompanyName],[ContactTel],[IssueTime])values(@OperatorId,@CompanyId,@PlanDate,@PlanPeopleNum,@TravelContent,@ResideContent,@DinnerContent,@CarContent,@GuideContent,@ShoppingInfo,@OtherContent,@ContactName,@ContactCompanyName,@ContactTel,@IssueTime)";
        /// <summary>
        /// 删除执行SQL
        /// </summary>
        private const string SQL_RouteTeamCustomization_Del = "DELETE FROM tbl_RouteTeamCustomization WHERE [Id]=@Id AND [CompanyId]=@CompanyId";
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _currentBase = null;
        #endregion

        #region Public Methods
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteTeamCustomization()
        {
            _currentBase = this.CompanyStore;
        }
        /// <summary>
        /// 新增团队制定
        /// </summary>
        /// <param name="model">高级网店团队制定实体</param>
        /// <returns>true:成功；false：失败</returns>
        public virtual bool Add(EyouSoft.Model.ShopStructure.RouteTeamCustomization model)
        {
            bool IsTrue = false;
            DbCommand dc = _currentBase.GetSqlStringCommand(SQL_RouteTeamCustomization_Add);
            _currentBase.AddInParameter(dc, "CarContent", DbType.String, model.CarContent);
            _currentBase.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _currentBase.AddInParameter(dc, "ContactCompanyName", DbType.String, model.ContactCompanyName);
            _currentBase.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            _currentBase.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            _currentBase.AddInParameter(dc, "DinnerContent", DbType.String, model.DinnerContent);
            _currentBase.AddInParameter(dc, "GuideContent", DbType.String, model.GuideContent);
            _currentBase.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _currentBase.AddInParameter(dc, "OtherContent", DbType.String, model.OtherContent);
            _currentBase.AddInParameter(dc, "PlanDate", DbType.DateTime, model.PlanDate);
            _currentBase.AddInParameter(dc, "PlanPeopleNum", DbType.Int32, model.PlanPeopleNum);
            _currentBase.AddInParameter(dc, "ResideContent", DbType.String, model.ResideContent);
            _currentBase.AddInParameter(dc, "ShoppingInfo", DbType.String, model.ShoppingInfo);
            _currentBase.AddInParameter(dc, "TravelContent", DbType.String, model.TravelContent);
            _currentBase.AddInParameter(dc, "IssueTime", DbType.String, model.IssueTime);
            int AffectedRows = DbHelper.ExecuteSql(dc, _currentBase);
            IsTrue = AffectedRows > 0 ? true : false;
            return IsTrue;
        }
        /// <summary>
        /// 团队制定信息删除
        /// </summary>
        /// <param name="Id">团队制定编号（主键）</param>
        /// <param name="CompanyId">所属公司编号</param>
        /// <returns>true:成功；false：失败</returns>
        public virtual bool Delete(int Id, string CompanyId)
        {
            bool IsTrue = false;
            DbCommand dc = _currentBase.GetSqlStringCommand(SQL_RouteTeamCustomization_Del);
            _currentBase.AddInParameter(dc, "Id", DbType.Int32, Id);
            _currentBase.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            int AffectedRows = DbHelper.ExecuteSql(dc, _currentBase);
            IsTrue = AffectedRows > 0 ? true : false;
            return IsTrue;
        }
        /// <summary>
        /// 获取团队制定实体
        /// </summary>
        /// <param name="Id">团队制定的编号（主键）</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ShopStructure.RouteTeamCustomization GetModel(int Id)
        {
            EyouSoft.Model.ShopStructure.RouteTeamCustomization model = new EyouSoft.Model.ShopStructure.RouteTeamCustomization();
            DbCommand dc = _currentBase.GetSqlStringCommand(SQL_RouteTeamCustomization_Select);
            _currentBase.AddInParameter(dc, "Id", DbType.Int32, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _currentBase))
            {
                while (dr.Read())
                {
                    model.CarContent = dr.IsDBNull(dr.GetOrdinal("CarContent")) ? "" : dr.GetString(dr.GetOrdinal("CarContent"));
                    model.CompanyId = dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? "" : dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.ContactCompanyName = dr.IsDBNull(dr.GetOrdinal("ContactCompanyName")) ? "" : dr.GetString(dr.GetOrdinal("ContactCompanyName"));
                    model.ContactName = dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactTel = dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.DinnerContent = dr.IsDBNull(dr.GetOrdinal("DinnerContent")) ? "" : dr.GetString(dr.GetOrdinal("DinnerContent"));
                    model.GuideContent = dr.IsDBNull(dr.GetOrdinal("GuideContent")) ? "" : dr.GetString(dr.GetOrdinal("GuideContent"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.OtherContent = dr.IsDBNull(dr.GetOrdinal("OtherContent")) ? "" : dr.GetString(dr.GetOrdinal("OtherContent"));
                    model.PlanDate = dr.GetDateTime(dr.GetOrdinal("PlanDate"));
                    model.PlanPeopleNum = dr.GetInt32(dr.GetOrdinal("PlanPeopleNum"));
                    model.ResideContent = dr.IsDBNull(dr.GetOrdinal("ResideContent")) ? "" : dr.GetString(dr.GetOrdinal("ResideContent"));
                    model.ShoppingInfo = dr.IsDBNull(dr.GetOrdinal("ShoppingInfo")) ? "" : dr.GetString(dr.GetOrdinal("ShoppingInfo"));
                    model.TravelContent = dr.IsDBNull(dr.GetOrdinal("TravelContent")) ? "" : dr.GetString(dr.GetOrdinal("TravelContent"));
                }
            }
            return model;
        }
        /// <summary>
        /// 获取团队制定信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> GetList(string CompanyId)
        {
            IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> ResultList = new List<EyouSoft.Model.ShopStructure.RouteTeamCustomization>();
            EyouSoft.Model.ShopStructure.RouteTeamCustomization model = null;
            string StrSql = "SELECT [Id],[OperatorId],[CompanyId],[PlanDate],[PlanPeopleNum],[TravelContent],[ResideContent],[DinnerContent],[CarContent],[GuideContent],[ShoppingInfo],[OtherContent],[ContactName],[ContactCompanyName],[ContactTel],[IssueTime] FROM tbl_RouteTeamCustomization WHERE [CompanyId]=@CompanyId";
            DbCommand dc = _currentBase.GetSqlStringCommand(StrSql);
            _currentBase.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _currentBase))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ShopStructure.RouteTeamCustomization();
                    model.CarContent = dr.IsDBNull(dr.GetOrdinal("CarContent")) ? "" : dr.GetString(dr.GetOrdinal("CarContent"));
                    model.CompanyId = dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? "" : dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.ContactCompanyName = dr.IsDBNull(dr.GetOrdinal("ContactCompanyName")) ? "" : dr.GetString(dr.GetOrdinal("ContactCompanyName"));
                    model.ContactName = dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactTel = dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.DinnerContent = dr.IsDBNull(dr.GetOrdinal("DinnerContent")) ? "" : dr.GetString(dr.GetOrdinal("DinnerContent"));
                    model.GuideContent = dr.IsDBNull(dr.GetOrdinal("GuideContent")) ? "" : dr.GetString(dr.GetOrdinal("GuideContent"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.OtherContent = dr.IsDBNull(dr.GetOrdinal("OtherContent")) ? "" : dr.GetString(dr.GetOrdinal("OtherContent"));
                    model.PlanDate = dr.GetDateTime(dr.GetOrdinal("PlanDate"));
                    model.PlanPeopleNum = dr.GetInt32(dr.GetOrdinal("PlanPeopleNum"));
                    model.ResideContent = dr.IsDBNull(dr.GetOrdinal("ResideContent")) ? "" : dr.GetString(dr.GetOrdinal("ResideContent"));
                    model.ShoppingInfo = dr.IsDBNull(dr.GetOrdinal("ShoppingInfo")) ? "" : dr.GetString(dr.GetOrdinal("ShoppingInfo"));
                    model.TravelContent = dr.IsDBNull(dr.GetOrdinal("TravelContent")) ? "" : dr.GetString(dr.GetOrdinal("TravelContent"));
                    ResultList.Add(model);
                }
            }
            model = null;
            return ResultList;
        }
        /// <summary>
        /// 获取团队制定信息(分页)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>团队制定信息集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> GetList(int PageSize, int PageIndex, ref int RecordCount, string CompanyId)
        {
            IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> ResultList = new List<EyouSoft.Model.ShopStructure.RouteTeamCustomization>();
            EyouSoft.Model.ShopStructure.RouteTeamCustomization model = null;
            string StrFileds = "[Id],[OperatorId],[CompanyId],[PlanDate],[PlanPeopleNum],[TravelContent],[ResideContent],[DinnerContent],[CarContent],[GuideContent],[ShoppingInfo],[OtherContent],[ContactName],[ContactCompanyName],[ContactTel],[IssueTime]";
            string StrQuery = string.Format("[CompanyId]='{0}' ", CompanyId);
            string StrOrderBy = " IssueTime DESC";            
            using (IDataReader dr = DbHelper.ExecuteReader(_currentBase, PageSize, PageIndex, ref RecordCount, "tbl_RouteTeamCustomization", "[Id]", StrFileds, StrQuery, StrOrderBy))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ShopStructure.RouteTeamCustomization();
                    model.CarContent = dr.IsDBNull(dr.GetOrdinal("CarContent")) ? "" : dr.GetString(dr.GetOrdinal("CarContent"));
                    model.CompanyId = dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? "" : dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.ContactCompanyName = dr.IsDBNull(dr.GetOrdinal("ContactCompanyName")) ? "" : dr.GetString(dr.GetOrdinal("ContactCompanyName"));
                    model.ContactName = dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactTel = dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.DinnerContent = dr.IsDBNull(dr.GetOrdinal("DinnerContent")) ? "" : dr.GetString(dr.GetOrdinal("DinnerContent"));
                    model.GuideContent = dr.IsDBNull(dr.GetOrdinal("GuideContent")) ? "" : dr.GetString(dr.GetOrdinal("GuideContent"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.OtherContent = dr.IsDBNull(dr.GetOrdinal("OtherContent")) ? "" : dr.GetString(dr.GetOrdinal("OtherContent"));
                    model.PlanDate = dr.GetDateTime(dr.GetOrdinal("PlanDate"));
                    model.PlanPeopleNum = dr.GetInt32(dr.GetOrdinal("PlanPeopleNum"));
                    model.ResideContent = dr.IsDBNull(dr.GetOrdinal("ResideContent")) ? "" : dr.GetString(dr.GetOrdinal("ResideContent"));
                    model.ShoppingInfo = dr.IsDBNull(dr.GetOrdinal("ShoppingInfo")) ? "" : dr.GetString(dr.GetOrdinal("ShoppingInfo"));
                    model.TravelContent = dr.IsDBNull(dr.GetOrdinal("TravelContent")) ? "" : dr.GetString(dr.GetOrdinal("TravelContent"));
                    ResultList.Add(model);
                }
            }
            model = null;
            return ResultList;
        }
        #endregion
    }
}
