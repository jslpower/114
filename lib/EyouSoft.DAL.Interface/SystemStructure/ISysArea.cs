using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 线路区域 数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    /// -----------------------
    /// 修改人：鲁功源 2011-04-06
    /// 新增内容：获取存在团队信息的线路区域集合方法GetExistsTourAreas()
    public interface ISysArea
    {
        #region 线路区域函数

        /// <summary>
        /// 新增线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>返回新加线路区域ID</returns>
        int AddSysArea(EyouSoft.Model.SystemStructure.SysArea model);

        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>返回受影响行数</returns>
        int UpdateSysArea(EyouSoft.Model.SystemStructure.SysArea model);

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList();

        /// <summary>
        /// 删除线路区域(物理删除，有有效计划时删除失败)
        /// </summary>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSysArea(int AreaId);
        /// <summary>
        /// 获取存在线路数据的线路区域集合
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysArea> GetExistsTourAreas();

        /// <summary>
        /// 获取包含某线路区域的所有分站（销售城市）
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        IList<Model.SystemStructure.CityBase> GetSalesCityByArea(int areaId);

        #endregion

        #region 长短线区域城市关系函数成员

        /// <summary>
        /// 获取所有的长短线区域城市关系
        /// </summary>
        /// <returns>长短线区域城市关系实体集合</returns>
        IList<Model.SystemStructure.SysAreaSiteControl> GetSysAreaSiteControl();

        /// <summary>
        /// 删除长短线区域城市关系
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <param name="AreaType">线路区域类型</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSysAreaSiteControl(int ProvinceId, int CityId, string AreaIds, Model.SystemStructure.AreaType AreaType);

        /// <summary>
        /// 添加长短线区域城市关系
        /// </summary>
        /// <param name="List">长短线区域城市关系实体集合</param>
        /// <returns>返回受影响的行数</returns>
        int AddSysAreaSiteControl(IList<Model.SystemStructure.SysAreaSiteControl> List);

        #endregion
    }
}
