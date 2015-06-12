using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using System.Xml.Linq;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 线路区域 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    /// -----------------------
    /// 修改人：鲁功源 2011-04-06
    /// 新增内容：获取存在团队信息的线路区域集合方法GetExistsTourAreas()
    public class SysArea : DALBase, IDAL.SystemStructure.ISysArea
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysArea()
        { }

        #region SqlString

        private const string Sql_SysArea_Add =
            @" if not exists (select 1 from tbl_SysArea where AreaName = @AreaName AND RouteType = @RouteType) 
                    INSERT INTO [tbl_SysArea]([AreaName],[RouteType])  
                    VALUES (@AreaName,@RouteType);
                    select @AreaId = @@IDENTITY; ";
        private const string Sql_SysArea_Update =
            @" if not exists (select 1 from tbl_SysArea where AreaName = @AreaName AND RouteType = @RouteType AND [ID] <> @AreaId) 
                UPDATE [tbl_SysArea] SET [AreaName] = @AreaName,[RouteType] = @RouteType,
                WHERE [ID] = @AreaId; ";
        private const string Sql_SysArea_Select =
            @" SELECT [ID],[AreaName],[RouteType],
                   (SELECT [AreaId],[CountryId],[ProvinceId],[CityId],[CountyId] 
                    FROM [tbl_SysAreaVisitCity] where [tbl_SysAreaVisitCity].[AreaId] = [tbl_SysArea].[ID]
                    for xml raw,root('Root')) as VisitCity
                FROM [tbl_SysArea] ";
        private const string Sql_SysArea_Del =
            @" if not exists (select 1 from tbl_NewRouteBasicInfo where AreaId = @AreaId and IsDeleted = '0') 
                    begin   
                        DELETE FROM [tbl_CompanyCityAd] WHERE AreaId = @AreaId; 
                        DELETE FROM [tbl_SysCityAreaControl] where AreaId = @AreaId; 
                        DELETE FROM [tbl_SysAreaSiteControl] where AreaId = @AreaId; 
                        DELETE FROM [tbl_CompanyAreaConfig] WHERE AreaId = @AreaId; 
                        DELETE FROM [tbl_CompanyUserAreaControl] where AreaId = @AreaId; 
                        DELETE FROM [tbl_SysAreaVisitCity] WHERE AreaId = @AreaId;
                        DELETE FROM [tbl_SysArea] WHERE ID = @AreaId;
                    end ";

        private const string Sql_SysAreaSiteControl_Select = " SELECT ProvinceId,CityId,AreaId,AreaType,(SELECT AreaName FROM tbl_SysArea WHERE tbl_SysArea.ID = tbl_SysAreaSiteControl.AreaId) AS AreaName FROM tbl_SysAreaSiteControl ";
        private const string Sql_SysAreaSiteControl_Del = " DELETE FROM [tbl_SysAreaSiteControl] ";
        private const string Sql_SysAreaSiteControl_Add = " if not exists (select 1 from tbl_SysAreaSiteControl where ProvinceId = {0} and CityId = {1} and AreaId = {2} and AreaType = {3}) begin INSERT INTO [tbl_SysAreaSiteControl]([ProvinceId],[CityId],[AreaId],[AreaType]) VALUES ({4},{5},{6},{7}) end; ";

        /// <summary>
        /// 线路区域主要游览城市关系添加Sql
        /// </summary>
        private const string SqlSysAreaVisitCityAdd = @" INSERT INTO [tbl_SysAreaVisitCity]
                       ([AreaId]
                       ,[CountryId]
                       ,[ProvinceId]
                       ,[CityId]
                       ,[CountyId])
                 VALUES
                       (@AreaId
                       ,{0}
                       ,{1}
                       ,{2}
                       ,{3}); ";

        /// <summary>
        /// 线路区域主要游览城市关系删除Sql
        /// </summary>
        private const string SqlSysAreaVisitCityDel = @" DELETE FROM [tbl_SysAreaVisitCity] WHERE AreaId = @AreaId; ";

        #endregion

        #region   线路区域函数成员

        /// <summary>
        /// 新增线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>返回新加线路区域ID</returns>
        public virtual int AddSysArea(EyouSoft.Model.SystemStructure.SysArea model)
        {
            if (model == null)
                return 0;

            var strSql = new StringBuilder();
            strSql.Append(" declare @AreaId int ; set @AreaId = 0 ");
            strSql.Append(@" if not exists (select 1 from tbl_SysArea where AreaName = @AreaName AND RouteType = @RouteType) ");
            strSql.Append(" begin ");
            strSql.Append(
                @" INSERT INTO [tbl_SysArea]([AreaName],[RouteType]) VALUES (@AreaName,@RouteType); ");
            strSql.Append(" set @AreaId = @@IDENTITY; ");
            if (model.VisitCity != null && model.VisitCity.Count > 0)
            {
                foreach (var t in model.VisitCity)
                {
                    if (t == null)
                        continue;

                    strSql.AppendFormat(SqlSysAreaVisitCityAdd, t.CountryId, t.ProvinceId, t.CityId, t.CountyId);
                }
            }
            strSql.Append(" end ");
            strSql.Append(" select @AreaId; ");

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());
            base.SystemStore.AddInParameter(dc, "AreaName", DbType.String, model.AreaName);
            base.SystemStore.AddInParameter(dc, "RouteType", DbType.Byte, (int)model.RouteType);

            object obj = DbHelper.GetSingle(dc, base.SystemStore);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateSysArea(EyouSoft.Model.SystemStructure.SysArea model)
        {
            if (model == null)
                return 0;

            var strSql = new StringBuilder();
            strSql.Append(
                @" if not exists (select 1 from tbl_SysArea where AreaName = @AreaName AND RouteType = @RouteType AND [ID] <> @AreaId) ");
            strSql.Append(" begin ");
            strSql.Append(@" UPDATE [tbl_SysArea] SET [AreaName] = @AreaName,[RouteType] = @RouteType WHERE [ID] = @AreaId; ");
            if (model.VisitCity != null && model.VisitCity.Count > 0)
            {
                strSql.Append(SqlSysAreaVisitCityDel);
                foreach (var t in model.VisitCity)
                {
                    if (t == null)
                        continue;

                    strSql.AppendFormat(SqlSysAreaVisitCityAdd, t.CountryId, t.ProvinceId, t.CityId, t.CountyId);
                }
            }
            strSql.Append(" end ");
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());
            base.SystemStore.AddInParameter(dc, "AreaName", DbType.String, model.AreaName);
            base.SystemStore.AddInParameter(dc, "RouteType", DbType.Byte, (int)model.RouteType);
            base.SystemStore.AddInParameter(dc, "AreaId", DbType.Int32, model.AreaId);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList()
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysArea_Select);
            return GetQueryList(dc);
        }

        /// <summary>
        /// 删除线路区域(物理删除，有有效计划时删除失败)
        /// </summary>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysArea(int AreaId)
        {
            if (AreaId <= 0)
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysArea_Del);
            base.SystemStore.AddInParameter(dc, "AreaId", DbType.Int32, AreaId);

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }
        /// <summary>
        /// 获取存在线路数据的线路区域集合
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysArea> GetExistsTourAreas()
        {
            string strSql = Sql_SysArea_Select + " where id in(select distinct areaid from tbl_tourlist where  leavedate>=getdate() and IsDelete='0' AND IsRecentLeave='1') ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            return GetQueryList(dc);
        }

        /// <summary>
        /// 获取包含某线路区域的所有分站（销售城市）
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        public IList<Model.SystemStructure.CityBase> GetSalesCityByArea(int areaId)
        {
            IList<Model.SystemStructure.CityBase> list = null;

            if (areaId <= 0)
                return list;

            var strSql = new StringBuilder();
            strSql.Append(
                @" select sa.CityId,sc.CityName,sc.ProvinceId,sp.ProvinceName
                    from tbl_SysCityAreaControl as sa left join tbl_SysCity as sc on sa.CityId = sc.Id
                    left join tbl_SysProvince as sp on sa.ProvinceId = sp.Id
                    where sa.AreaId = @AreaId ");

            DbCommand dc = SystemStore.GetSqlStringCommand(strSql.ToString());
            SystemStore.AddInParameter(dc, "AreaId", DbType.Int32, areaId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, SystemStore))
            {
                list = new List<Model.SystemStructure.CityBase>();
                while (dr.Read())
                {
                    list.Add(new Model.SystemStructure.CityBase
                                 {
                                     CityId = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                                     CityName = dr.IsDBNull(1) ? string.Empty : dr.GetString(1),
                                     ProvinceId = dr.IsDBNull(2) ? 0 : dr.GetInt32(2),
                                     ProvinceName = dr.IsDBNull(3) ? string.Empty : dr.GetString(3)
                                 });
                }
            }

            return list;
        }

        #endregion

        #region 长短线区域城市关系函数成员

        /// <summary>
        /// 获取所有的长短线区域城市关系
        /// </summary>
        /// <returns>长短线区域城市关系实体集合</returns>
        public virtual IList<Model.SystemStructure.SysAreaSiteControl> GetSysAreaSiteControl()
        {
            IList<Model.SystemStructure.SysAreaSiteControl> List = new List<Model.SystemStructure.SysAreaSiteControl>();
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysAreaSiteControl_Select);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysAreaSiteControl model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysAreaSiteControl();

                    if (!dr.IsDBNull(dr.GetOrdinal("ProvinceId")))
                        model.ProvinceId = int.Parse(dr["ProvinceId"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("CityId")))
                        model.CityId = int.Parse(dr["CityId"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaId")))
                        model.AreaId = int.Parse(dr["AreaId"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("AreaType")))
                        model.RouteType = (Model.SystemStructure.AreaType)int.Parse(dr["AreaType"].ToString());
                    model.AreaName = dr["AreaName"].ToString();

                    List.Add(model);
                }
            }
            return List;
        }

        /// <summary>
        /// 添加长短线区域城市关系
        /// </summary>
        /// <param name="List">长短线区域城市关系实体集合</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int AddSysAreaSiteControl(IList<Model.SystemStructure.SysAreaSiteControl> List)
        {
            if (List == null || List.Count <= 0)
                return 0;

            StringBuilder strSql = new StringBuilder();
            foreach (Model.SystemStructure.SysAreaSiteControl model in List)
            {
                strSql.AppendFormat(Sql_SysAreaSiteControl_Add, model.ProvinceId, model.CityId, model.AreaId, (int)model.RouteType, model.ProvinceId, model.CityId, model.AreaId, (int)model.RouteType);
            }

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除长短线区域城市关系
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <param name="AreaType">线路区域类型</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysAreaSiteControl(int ProvinceId, int CityId, string AreaIds, Model.SystemStructure.AreaType AreaType)
        {
            if (AreaType == Model.SystemStructure.AreaType.国内长线 && ProvinceId <= 0)
                return false;
            if (AreaType == Model.SystemStructure.AreaType.国内短线 && CityId <= 0)
                return false;

            StringBuilder strWhere = new StringBuilder(Sql_SysAreaSiteControl_Del);
            strWhere.AppendFormat(" where AreaType = {0} ", (int)AreaType);
            if (ProvinceId > 0)
                strWhere.AppendFormat(" and ProvinceId = {0} ", ProvinceId);
            if (CityId > 0)
                strWhere.AppendFormat(" and CityId = {0} ", CityId);
            if (!string.IsNullOrEmpty(AreaIds))
                strWhere.AppendFormat(" and AreaId in ({0}) ", AreaIds);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere.ToString());

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        #endregion

        #region 私有函数，通过查询命令返回实体集合

        /// <summary>
        /// 通过查询命令返回实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.SystemStructure.SysArea> GetQueryList(DbCommand dc)
        {
            IList<Model.SystemStructure.SysArea> List = new List<Model.SystemStructure.SysArea>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysArea model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysArea();
                    if (!dr.IsDBNull(0))
                        model.AreaId = dr.GetInt32(0);
                    model.AreaName = dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.RouteType = (Model.SystemStructure.AreaType)((int)dr.GetByte(2));
                    if (!dr.IsDBNull(3))
                    {
                        model.VisitCity = new List<Model.SystemStructure.SysAreaVisitCity>();
                        var xRoot = XElement.Parse(dr.GetString(3));
                        var xRows = Common.Utility.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                model.VisitCity.Add(new Model.SystemStructure.SysAreaVisitCity
                                                        {
                                                            CountryId =
                                                                Common.Utility.GetInt(
                                                                    Common.Utility.GetXAttributeValue(t, "CountryId")),
                                                            ProvinceId =
                                                                Common.Utility.GetInt(
                                                                    Common.Utility.GetXAttributeValue(t, "ProvinceId")),
                                                            CityId =
                                                                Common.Utility.GetInt(
                                                                    Common.Utility.GetXAttributeValue(t, "CityId")),
                                                            CountyId =
                                                                Common.Utility.GetInt(
                                                                    Common.Utility.GetXAttributeValue(t, "CountyId"))
                                                        });
                            }
                        }
                    }

                    List.Add(model);
                }
                model = null;
            }
            return List;
        }

        #endregion
    }
}
