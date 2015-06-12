using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 县区接口
    /// 创建人：郑付杰
    /// 创建时间：2011/10/24
    /// </summary>
    public interface ISysDistrictCounty
    {
        /// <summary>
        /// 获取所有县区
        /// </summary>
        /// <returns>县区集合</returns>
        IList<SysDistrictCounty> GetDistrictCounty();

        /// <summary>
        /// 获取县区实体
        /// </summary>
        /// <param name="districtId">县区编号</param>
        /// <returns>县区实体</returns>
        SysDistrictCounty GetModel(int districtId);
    }
}
