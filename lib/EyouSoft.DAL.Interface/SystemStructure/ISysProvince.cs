using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 省份 数据访问接口
    /// </summary>
    /// 周文超 2010-05-14
    public interface ISysProvince
    {
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="IsEnabled">true(调用所有),false(调用已开通城市所属省份)</param>
        /// <returns>返回省份实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList(bool IsEnabled);
        /// <summary>
        /// 获取存在资讯信息的所有省份列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysProvince> GetExistsNewsProvinceList();
    }
}
