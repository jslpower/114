using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 省份 业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-26
    public interface ISysProvince
    {
        /// <summary>
        /// 获取省份基本信息实体
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns>返回省份实体</returns>
        Model.SystemStructure.ProvinceBase GetProvinceBaseModel(int ProvinceId);

        /// <summary>
        /// 获取省份基本信息实体
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns>返回省份实体</returns>
        EyouSoft.Model.SystemStructure.SysProvince GetProvinceModel(int ProvinceId);

        /// <summary>
        /// 获取已开通省份列表
        /// </summary>
        /// <returns>返回省份实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetEnabledList();

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns>返回省份实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList();

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="ProvinceIds">省份ID集合</param>
        /// <returns>返回省份实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList(int[] ProvinceIds);

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="CountryId">国家编号，小于等于0不作为条件</param>
        /// <param name="HeaderLetter">首字母，为空 不作为条件</param>
        /// <param name="AreaId">区域编号，小于等于0不作为条件</param>
        /// <returns>返回省份实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList(int CountryId, string HeaderLetter, EyouSoft.Model.SystemStructure.ProvinceAreaType? AreaId);
        /// <summary>
        /// 获取存在资讯信息的所有省份列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetExistsNewsProvinceList();
    }
}
