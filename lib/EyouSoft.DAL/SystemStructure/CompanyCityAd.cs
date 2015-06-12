using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 单位城市广告关系 数据访问
    /// </summary>
    /// 周文超 2010-07-08
    public class CompanyCityAd : DALBase, IDAL.SystemStructure.ICompanyCityAd
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyCityAd() { }

        private const string Sql_CompanyCityAd_Add = " insert into tbl_CompanyCityAd (CompanyId,CityId,AreaId) select '{0}',{1},{2} where not exists (select 1 from tbl_CompanyCityAd where CompanyId = '{3}' and CityId = {4} and AreaId = {5}); ";
        private const string Sql_CompanyCityAd_Del = " delete from tbl_CompanyCityAd ";
        private const string Sql_CompanyCityAd_QueryCompanyId = " select CompanyId from tbl_CompanyCityAd where CityId = @CityId and AreaId = @AreaId Order by IssueTime";

        /// <summary>
        /// 新增单位城市广告关系
        /// </summary>
        /// <param name="List">单位城市广告关系实体集合</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddCompanyCityAd(IList<Model.SystemStructure.CompanyCityAd> List)
        {
            if (List == null || List.Count <= 0)
                return 0;

            StringBuilder strWhere = new StringBuilder();
            foreach (Model.SystemStructure.CompanyCityAd model in List)
            {
                strWhere.AppendFormat(Sql_CompanyCityAd_Add, model.CompanyId, model.CityId, model.AreaId, model.CompanyId, model.CityId, model.AreaId);
            }
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere.ToString());

            return DbHelper.ExecuteSqlTrans(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="Ids">单位城市广告关系ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteCompanyCityAd(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(string.Format(Sql_CompanyCityAd_Del + " where id in ({0})", Ids));

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="CityId">城市ID（必须传值）</param>
        /// <param name="AreaId">线路区域ID（必须传值）</param>
        /// <param name="CompanyId">公司ID（未null不作条件，删除该城市该线路区域下所有公司）</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteCompanyCityAd(int CityId, int AreaId, string CompanyId)
        {
            if (CityId <= 0 || AreaId <= 0)
                return false;

            string strWhere = Sql_CompanyCityAd_Del + string.Format(" where CityId = {0} and AreaId = {1} ", CityId, AreaId);
            if (!string.IsNullOrEmpty(CompanyId))
                strWhere += string.Format(" and CompanyId = '{0}' ", CompanyId);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 根据城市和线路区域获取单位城市广告关系中的公司ID集合
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>公司ID集合</returns>
        public virtual IList<string> GetCompanyIdsByCityAndArea(int CityId, int AreaId)
        {
            if (CityId <= 0 || AreaId <= 0)
                return null;

            IList<string> Ids = new List<string>();

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_CompanyCityAd_QueryCompanyId);
            base.SystemStore.AddInParameter(dc, "CityId", DbType.Int32, CityId);
            base.SystemStore.AddInParameter(dc, "AreaId", DbType.Int32, AreaId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                while (dr.Read())
                {
                    Ids.Add(dr[0].ToString());
                }
            }
            return Ids;
        }
    }
}
