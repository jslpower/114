using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 业务层接口
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
        /// 根据城市获取县区
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns>县区编号</returns>
        IList<SysDistrictCounty> GetDistrictCounty(int cityId);

        /// <summary>
        /// 获取县区实体
        /// </summary>
        /// <param name="districtId">县区编号</param>
        /// <returns>县区实体</returns>
        SysDistrictCounty GetModel(int districtId);

        /// <summary>
        /// 根据县区ID集合获取县区实体集合
        /// </summary>
        /// <param name="districtIds">城市编号集合</param>
        /// <returns>县区编号</returns>
        IList<SysDistrictCounty> GetDistrictCountyList(int[] districtIds);
    }
}
