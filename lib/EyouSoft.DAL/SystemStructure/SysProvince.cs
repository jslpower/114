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
    /// 省份 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SysProvince : DALBase, IDAL.SystemStructure.ISysProvince
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysProvince()
        { }

        private const string Sql_SysProvince_Select = " SELECT [ID],[CountryId],[HeaderLetter],[ProvinceName],[AreaId],[SortId]  FROM [tbl_SysProvince] ";

        #region 函数成员

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="IsEnabled">是否开通</param>
        /// <returns>返回省份实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList(bool IsEnabled)
        {
            IList<Model.SystemStructure.SysProvince> List = new List<Model.SystemStructure.SysProvince>();
            string strSql = Sql_SysProvince_Select;
            if (IsEnabled)
            {
                strSql += " where ID IN(SELECT ProvinceId FROM tbl_SysCity WHERE IsEnabled='1')";
            }
            strSql += " order by SortId,HeaderLetter asc";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysProvince_Select);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysProvince model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysProvince();
                    if (!dr.IsDBNull(0))
                        model.ProvinceId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.CountryId = dr.GetInt32(1);
                    model.HeaderLetter = dr[2].ToString();
                    model.ProvinceName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.AreaId = (Model.SystemStructure.ProvinceAreaType)dr.GetInt32(4);
                    if (!dr.IsDBNull(5))
                        model.SortId = dr.GetInt32(5);

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }
        /// <summary>
        /// 获取存在资讯信息的所有省份列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysProvince> GetExistsNewsProvinceList()
        {
            IList<Model.SystemStructure.SysProvince> List = new List<Model.SystemStructure.SysProvince>();
            string strSql = Sql_SysProvince_Select + " where Id in(select distinct provinceId from tbl_Affiche) order by SortId,HeaderLetter asc";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysProvince model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysProvince();
                    if (!dr.IsDBNull(0))
                        model.ProvinceId = dr.GetInt32(0);
                    if (!dr.IsDBNull(1))
                        model.CountryId = dr.GetInt32(1);
                    model.HeaderLetter = dr[2].ToString();
                    model.ProvinceName = dr[3].ToString();
                    if (!dr.IsDBNull(4))
                        model.AreaId = (Model.SystemStructure.ProvinceAreaType)dr.GetInt32(4);
                    if (!dr.IsDBNull(5))
                        model.SortId = dr.GetInt32(5);

                    List.Add(model);
                }
                model = null;
            }

            return List;
        }
        #endregion
    }
}
