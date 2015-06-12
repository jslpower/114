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
using System.IO;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 城市 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SysCity : DALBase, IDAL.SystemStructure.ISysCity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCity()
        { }

        private const string Sql_SysCity_GetModel = " SELECT TOP 1 [ID],[ProvinceId],(SELECT ProvinceName FROM tbl_SysProvince WHERE ID = [tbl_SysCity].ProvinceId) as ProvinceName,[CityName],[CenterCityId],[HeaderLetter],[ParentTourCount],[TourCount],[IsSite],[DomainName],[IsEnabled],(SELECT AreaId,AreaName,SortId,IsDefaultShow,(SELECT RouteType FROM tbl_SysArea WHERE tbl_SysArea.ID = tbl_SysCityAreaControl.AreaId) as RouteType FROM tbl_SysCityAreaControl WHERE tbl_SysCityAreaControl.CityId = [tbl_SysCity].ID order by SortId for xml auto,root('root')) AS SysAreaByCity,(SELECT AreaId,CompanyId FROM tbl_CompanyCityAd WHERE tbl_CompanyCityAd.CityId = tbl_SysCity.ID order by IssueTime for xml auto,root('root')) as AreaCompany,RewriteCode FROM [tbl_SysCity] WHERE 1=1 ";
        private const string Sql_SysCity_Select = " SELECT [ID],[ProvinceId],(SELECT ProvinceName FROM tbl_SysProvince WHERE ID = [tbl_SysCity].ProvinceId) as ProvinceName,[CityName],[CenterCityId],[HeaderLetter],[ParentTourCount],[TourCount],[IsSite],[DomainName],[IsEnabled],[RewriteCode] FROM [tbl_SysCity] order by [HeaderLetter]";
        private const string Sql_SysCity_Set = " UPDATE [tbl_SysCity] SET [IsEnabled] = @IsEnabled where [ID] = @ID ";
        private const string Sql_SysCity_SetIsSite = " UPDATE [tbl_SysCity] SET [IsSite] = @IsSite where [ID] in (@ID);if(@IsSite=0) DELETE tbl_CompanySiteControl WHERE CityId IN (@ID);";

        private const string Sql_SysSiteAreaControl_Select = " SELECT [AreaId],[AreaName],[CityId],[CityName],[ProvinceId],[ProvinceName],[SortId],[TourCount],[IsDefaultShow] FROM [tbl_SysCityAreaControl] order by SortId ";

        private const string Sql_SysSiteAreaControl_Insert = " if not exists (select 1 from [tbl_SysCityAreaControl] where [AreaId] = {0} and [CityId] = {2}) begin INSERT INTO [tbl_SysCityAreaControl] ([AreaId],[AreaName],[CityId],[CityName],[ProvinceId],[ProvinceName],[SortId],[TourCount],[IsDefaultShow]) VALUES ({0},'{1}',{2},'{3}',{4},'{5}',{6},{7},'{8}'); end";
        private const string Sql_SysSiteAreaControl_Delete = " DELETE FROM [tbl_SysCityAreaControl] ";

        #region 城市函数

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="CityId">城市编号</param>
        /// <param name="DomainName">城市域名</param>
        /// <returns>返回城市实体集合</returns>
        public virtual EyouSoft.Model.SystemStructure.SysCity GetModel(int CityId, string DomainName)
        {
            string strSql = Sql_SysCity_GetModel;
            Dictionary<int, string> SiteArea = new Dictionary<int, string>();
            Dictionary<int, string> AreaCompany = new Dictionary<int, string>();
            EyouSoft.Model.SystemStructure.SysCity model = null;
            if (CityId > 0)
                strSql = Sql_SysCity_GetModel + " AND ID=@CityId ";
            if (!string.IsNullOrEmpty(DomainName))
                strSql = Sql_SysCity_GetModel + " AND DomainName=@DomainName ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            if (CityId > 0)
                base.SystemStore.AddInParameter(dc, "CityId", DbType.Int32, CityId);
            if (!string.IsNullOrEmpty(DomainName))
                base.SystemStore.AddInParameter(dc, "DomainName", DbType.AnsiString, DomainName);

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysCity();
                    if (!dr.IsDBNull(0))
                        model.CityId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.ProvinceId = dr.GetInt32(1);
                    model.ProvinceName = dr[2].ToString();
                    model.CityName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.CenterCityId = dr.GetInt32(4);
                    model.HeaderLetter = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.ParentTourCount = dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.TourCount = dr.GetInt32(7);
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSite")) && dr["IsSite"].ToString().Equals("1"))
                        model.IsSite = true;
                    else
                        model.IsSite = false;
                    model.DomainName = dr["DomainName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnabled")) && dr["IsEnabled"].ToString().Equals("1"))
                        model.IsEnabled = true;
                    else
                        model.IsEnabled = false;

                    model.RewriteCode = dr["RewriteCode"].ToString();

                    SiteArea.Add(model.CityId, dr["SysAreaByCity"].ToString());
                    AreaCompany.Add(model.CityId, dr["AreaCompany"].ToString());
                }
            }

            if (model==null)
                return model;

            int AreaId = 0;
            IList<string> strTmpCompanyId = null;
            string CompanyId = string.Empty;
            System.Xml.XmlAttributeCollection attList = null;
            System.Xml.XmlDocument xml = null;
            System.Xml.XmlNodeList xmlNodeList = null;

           
            #region 线路区域

            if (string.IsNullOrEmpty(SiteArea[model.CityId]))
                return model;

            EyouSoft.Model.SystemStructure.SysCityArea SysAreaModel = null;
            if (model.CityAreaControls != null) model.CityAreaControls.Clear();
            if (model.AreaAdvNum != null) model.AreaAdvNum.Clear();

            xml = new System.Xml.XmlDocument();
            xml.LoadXml(SiteArea[model.CityId]);
            xmlNodeList = xml.GetElementsByTagName("tbl_SysCityAreaControl");
            if (xmlNodeList != null)
            {
                foreach (System.Xml.XmlNode node in xmlNodeList)
                {
                    SysAreaModel = new EyouSoft.Model.SystemStructure.SysCityArea();
                    attList = node.Attributes;
                    if (attList == null && attList.Count <= 0)
                        continue;

                    if (!string.IsNullOrEmpty(attList["AreaId"].Value))
                        SysAreaModel.AreaId = int.Parse(attList["AreaId"].Value);
                    SysAreaModel.AreaName = attList["AreaName"].Value;
                    if (!string.IsNullOrEmpty(attList["SortId"].Value))
                        SysAreaModel.SortId = int.Parse(attList["SortId"].Value);
                    if (!string.IsNullOrEmpty(attList["IsDefaultShow"].Value) && attList["IsDefaultShow"].Value.Equals("1"))
                        SysAreaModel.IsDefaultShow = true;
                    else
                        SysAreaModel.IsDefaultShow = false;
                    if (!string.IsNullOrEmpty(attList["RouteType"].Value))
                        SysAreaModel.RouteType = (EyouSoft.Model.SystemStructure.AreaType)int.Parse(attList["RouteType"].Value);

                    model.CityAreaControls.Add(SysAreaModel);
                }
            }

            #endregion

            #region 线路区域公司广告

            if (string.IsNullOrEmpty(AreaCompany[model.CityId]))
                return model;

            xml = null;
            xml = new System.Xml.XmlDocument();
            xml.LoadXml(AreaCompany[model.CityId]);
            xmlNodeList = xml.GetElementsByTagName("tbl_CompanyCityAd");
            if (xmlNodeList != null)
            {
                foreach (System.Xml.XmlNode node in xmlNodeList)
                {
                    attList = node.Attributes;
                    if (attList == null && attList.Count <= 0)
                        continue;

                    if (!string.IsNullOrEmpty(attList["AreaId"].Value))
                        AreaId = int.Parse(attList["AreaId"].Value);
                    CompanyId = attList["CompanyId"].Value;
                    if (model.AreaAdvNum.ContainsKey(AreaId))
                        model.AreaAdvNum[AreaId].Add(CompanyId);
                    else
                    {
                        strTmpCompanyId = new List<string>();
                        strTmpCompanyId.Add(CompanyId);
                        model.AreaAdvNum.Add(AreaId, strTmpCompanyId);
                    }
                }
            }

            #endregion

            if (attList != null) attList.RemoveAll();
            attList = null;
            xml = null;
            if (SiteArea != null) SiteArea.Clear();
            SiteArea = null;
            if (AreaCompany != null) AreaCompany.Clear();
            AreaCompany = null;
            xmlNodeList = null;

            #endregion

            return model;
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns>返回城市实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList()
        {
            IList<EyouSoft.Model.SystemStructure.SysCity> List = new List<Model.SystemStructure.SysCity>();
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCity_Select);

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysCity model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysCity();
                    if (!dr.IsDBNull(0))
                        model.CityId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.ProvinceId = dr.GetInt32(1);
                    model.ProvinceName = dr[2].ToString();
                    model.CityName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.CenterCityId = dr.GetInt32(4);
                    model.HeaderLetter = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.ParentTourCount = dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.TourCount = dr.GetInt32(7);
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSite")) && dr["IsSite"].ToString().Equals("1"))
                        model.IsSite = true;
                    else
                        model.IsSite = false;
                    model.DomainName = dr["DomainName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnabled")) && dr["IsEnabled"].ToString().Equals("1"))
                        model.IsEnabled = true;
                    else
                        model.IsEnabled = false;

                    model.RewriteCode = dr["RewriteCode"].ToString();

                    List.Add(model);
                }
                model = null;
            }     

            #endregion

            return List;
        }

        /// <summary>
        /// 获取城市列表(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1：城市首字母升/降序；2/3：城市ID升/降序；)</param>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否停用</param>
        /// <returns>城市列表</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, int ProvinceId, int CityId, bool? IsEnabled)
        {

            IList<EyouSoft.Model.SystemStructure.SysCity> List = new List<EyouSoft.Model.SystemStructure.SysCity>();
            Dictionary<int, string> SiteArea = new Dictionary<int, string>();
            Dictionary<int, string> AreaCompany = new Dictionary<int, string>();
            string strWhere = " 1 = 1 ";
            string strFiles = " [ID],[ProvinceId],(SELECT ProvinceName FROM tbl_SysProvince WHERE ID = [tbl_SysCity].ProvinceId) as ProvinceName,[CityName],[CenterCityId],[HeaderLetter],[ParentTourCount],[TourCount],[IsSite],[DomainName],[IsEnabled],[RewriteCode],(SELECT AreaId,AreaName,SortId,IsDefaultShow,(SELECT RouteType FROM tbl_SysArea WHERE tbl_SysArea.ID = tbl_SysCityAreaControl.AreaId) as RouteType FROM tbl_SysCityAreaControl WHERE tbl_SysCityAreaControl.CityId = [tbl_SysCity].ID order by SortId for xml auto,root('root')) AS SysAreaByCity,(SELECT AreaId,CompanyId FROM tbl_CompanyCityAd WHERE tbl_CompanyCityAd.CityId = tbl_SysCity.ID for xml auto,root('root')) as AreaCompany ";
            string strOrder = string.Empty;
            switch (OrderIndex)
            {
                case 0: strOrder = " HeaderLetter asc "; break;
                case 1: strOrder = " HeaderLetter desc "; break;
                case 2: strOrder = " ID asc "; break;
                case 3: strOrder = " ID desc "; break;
                default: strOrder = " HeaderLetter asc "; break;
            }
            if (ProvinceId > 0)
                strWhere += string.Format(" and ProvinceId = {0} ", ProvinceId);
            if (CityId > 0)
                strWhere += string.Format(" and ID = {0} ", CityId);
            if (IsEnabled != null)
                strWhere += string.Format(" and IsEnabled = '{0}' ", (bool)IsEnabled ? "1" : "0");

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_SysCity", "ID", strFiles, strWhere, strOrder))
            {
                Model.SystemStructure.SysCity model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysCity();
                    if (!dr.IsDBNull(0))
                        model.CityId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.ProvinceId = dr.GetInt32(1);
                    model.ProvinceName = dr[2].ToString();
                    model.CityName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.CenterCityId = dr.GetInt32(4);
                    model.HeaderLetter = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.ParentTourCount = dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.TourCount = dr.GetInt32(7);
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSite")) && dr["IsSite"].Equals("1"))
                        model.IsSite = true;
                    else
                        model.IsSite = false;
                    model.DomainName = dr["DomainName"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnabled")) && dr["IsEnabled"].ToString().Equals("1"))
                        model.IsEnabled = true;
                    else
                        model.IsEnabled = false;
                    model.RewriteCode = dr["RewriteCode"].ToString();

                    List.Add(model);
                    SiteArea.Add(model.CityId, dr["SysAreaByCity"].ToString());
                    AreaCompany.Add(model.CityId, dr["AreaCompany"].ToString());
                }
                model = null;
            }

            if (List == null || List.Count <= 0 || SiteArea == null || SiteArea.Count <= 0)
                return List;

            int AreaId = 0;
            string CompanyId = string.Empty;
            IList<string> strTmpCompanyId = null;
            System.Xml.XmlAttributeCollection attList = null;
            System.Xml.XmlDocument xml = null;
            System.Xml.XmlNodeList XmlNodeList = null;
            foreach (EyouSoft.Model.SystemStructure.SysCity model in List)
            {
                #region 线路区域

                if (string.IsNullOrEmpty(SiteArea[model.CityId]))
                    continue;

                EyouSoft.Model.SystemStructure.SysCityArea SysAreaModel = null;
                if (model.CityAreaControls != null) model.CityAreaControls.Clear();
                if (model.AreaAdvNum != null) model.AreaAdvNum.Clear();

                xml = new System.Xml.XmlDocument();
                xml.LoadXml(SiteArea[model.CityId]);
                XmlNodeList = xml.GetElementsByTagName("tbl_SysCityAreaControl");
                if (XmlNodeList != null)
                {
                    foreach (System.Xml.XmlNode node in XmlNodeList)
                    {
                        SysAreaModel = new EyouSoft.Model.SystemStructure.SysCityArea();
                        attList = node.Attributes;
                        if (attList == null && attList.Count <= 0)
                            continue;

                        if (!string.IsNullOrEmpty(attList["AreaId"].Value))
                            SysAreaModel.AreaId = int.Parse(attList["AreaId"].Value);
                        SysAreaModel.AreaName = attList["AreaName"].Value;
                        if (!string.IsNullOrEmpty(attList["SortId"].Value))
                            SysAreaModel.SortId = int.Parse(attList["SortId"].Value);
                        if (!string.IsNullOrEmpty(attList["IsDefaultShow"].Value) && attList["IsDefaultShow"].Value.Equals("1"))
                            SysAreaModel.IsDefaultShow = true;
                        else
                            SysAreaModel.IsDefaultShow = false;
                        if (!string.IsNullOrEmpty(attList["RouteType"].Value))
                            SysAreaModel.RouteType = (EyouSoft.Model.SystemStructure.AreaType)int.Parse(attList["RouteType"].Value);

                        model.CityAreaControls.Add(SysAreaModel);
                    }
                }

                #endregion

                #region 线路区域公司广告

                if (string.IsNullOrEmpty(AreaCompany[model.CityId]))
                    continue;

                xml = null;
                xml = new System.Xml.XmlDocument();
                xml.LoadXml(AreaCompany[model.CityId]);
                XmlNodeList = xml.GetElementsByTagName("tbl_CompanyCityAd");
                foreach (System.Xml.XmlNode node in XmlNodeList)
                {
                    attList = node.Attributes;
                    if (attList == null && attList.Count <= 0)
                        continue;

                    if (!string.IsNullOrEmpty(attList["AreaId"].Value))
                        AreaId = int.Parse(attList["AreaId"].Value);
                    CompanyId = attList["CompanyId"].Value;
                    if (model.AreaAdvNum.ContainsKey(AreaId))
                        model.AreaAdvNum[AreaId].Add(CompanyId);
                    else
                    {
                        strTmpCompanyId = new List<string>();
                        strTmpCompanyId.Add(CompanyId);
                        model.AreaAdvNum.Add(AreaId, strTmpCompanyId);
                    }
                }

                #endregion
            }
            if (attList != null) attList.RemoveAll();
            attList = null;
            xml = null;
            if (SiteArea != null) SiteArea.Clear();
            SiteArea = null;
            if (AreaCompany != null) AreaCompany.Clear();
            AreaCompany = null;

            #endregion

            return List;
        }

        /// <summary>
        /// 设置是否停用
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否停用</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsEnabled(int CityId, bool IsEnabled)
        {
            if (CityId <= 0)
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCity_Set);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, CityId);
            base.SystemStore.AddInParameter(dc, "IsEnabled", DbType.String, IsEnabled ? "1" : "0");

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 设置是否出港城市
        /// </summary>
        /// <param name="CityIds">城市ID集合</param>
        /// <param name="IsSite">是否出港</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsSite(string CityIds, bool IsSite)
        {
            if (string.IsNullOrEmpty(CityIds))
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysCity_SetIsSite);
            base.SystemStore.AddInParameter(dc, "ID", DbType.String, CityIds);
            base.SystemStore.AddInParameter(dc, "IsSite", DbType.String, IsSite ? "1" : "0");

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        #endregion

        #region 城市线路区域关系函数

        /// <summary>
        /// 获取城市线路区域关系列表
        /// </summary>
        /// <returns>返回城市线路区域关系列表</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> GetSiteAreaControlList()
        {
            IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List = new List<EyouSoft.Model.SystemStructure.SysCityAreaControl>();
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysSiteAreaControl_Select);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.SystemStructure.SysCityAreaControl model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysCityAreaControl();
                    if (!dr.IsDBNull(0))
                        model.AreaId = dr.GetInt32(0);
                    model.AreaName = dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.CityId = dr.GetInt32(2);
                    model.CityName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.ProvinceId = dr.GetInt32(4);
                    model.ProvinceName = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.SortId = dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.TourCount = dr.GetInt32(7);
                    if (!string.IsNullOrEmpty(dr["IsDefaultShow"].ToString()) && dr["IsDefaultShow"].ToString().Equals("1"))
                        model.IsDefaultShow = true;
                    else
                        model.IsDefaultShow = false;

                    List.Add(model);
                }
            }
            return List;
        }

        /// <summary>
        /// 添加城市线路区域关系
        /// </summary>
        /// <param name="List">城市线路区域关系集合</param>
        /// <param name="CityId">城市ID(小于等于0则取集合中的)</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddSysSiteAreaControl(IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List, int CityId)
        {
            if (List == null || List.Count <= 0)
                return 0;
            StringBuilder strSql = new StringBuilder();
            int tmpCityId = 0;
            foreach (EyouSoft.Model.SystemStructure.SysCityAreaControl tmp in List)
            {
                if (tmp != null)
                {
                    if (CityId > 0)
                        tmpCityId = CityId;
                    else
                        tmpCityId = tmp.CityId;

                    strSql.AppendFormat(Sql_SysSiteAreaControl_Insert, tmp.AreaId, tmp.AreaName, tmpCityId, tmp.CityName, tmp.ProvinceId
                        , tmp.ProvinceName, tmp.SortId, tmp.TourCount, tmp.IsDefaultShow ? "1" : "0");
                }
            }
            if (string.IsNullOrEmpty(strSql.ToString()))
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除城市线路区域关系
        /// </summary>
        /// <param name="CityId">城市ID(必须传值)</param>
        /// <param name="AreaIds">线路区域ID(为null删除该城市下所有线路区域)</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DelSysSiteAreaControl(int CityId, string AreaIds)
        {
            if (CityId <= 0)
                return false;

            string strWhere = string.Format(Sql_SysSiteAreaControl_Delete + " where CityId = {0} ", CityId);
            if (!string.IsNullOrEmpty(AreaIds))
            {
                strWhere += string.Format(" and AreaId in ({0})  ", AreaIds);
            }
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 系统IP函数

        /// <summary>
        /// 根据客户端Ip地址返回城市基类实体
        /// </summary>
        /// <param name="IpAddress">客户端Ip地址</param>
        /// <returns>城市基类实体</returns>
        public virtual Model.SystemStructure.CityBase GetClientCityByIp(string IpAddress)
        {
            if (string.IsNullOrEmpty(IpAddress))
                return null;

            long Ip = EyouSoft.Common.Utility.IpToLong(IpAddress);
            if (Ip <= 0)
                return null;

            Model.SystemStructure.CityBase model = null;
            string strSql = " SELECT  ProvinceId,CityId,(select CityName from tbl_SysCity as a where a.id = CityId) as CityName,(SELECT ProvinceName FROM tbl_SysProvince as b WHERE b.ID = ProvinceId) AS ProvinceName FROM tbl_SystemIP WHERE @Ip BETWEEN IpStartNum AND IpEndNum ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            base.SystemStore.AddInParameter(dc, "Ip", DbType.Decimal, Ip);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.CityBase();
                    if (!dr.IsDBNull(0))
                        model.ProvinceId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.CityId = dr.GetInt32(1);
                    if (!dr.IsDBNull(2))
                        model.CityName = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                        model.ProvinceName = dr.GetString(3);
                }
            }
            return model;
        }
        #endregion
    }
}
