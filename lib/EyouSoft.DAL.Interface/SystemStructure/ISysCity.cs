using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 城市数据访问接口
    /// </summary>
    /// 周文超 2010-05-14
    public interface ISysCity
    {
        #region 城市函数
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="CityId">城市编号</param>
        /// <param name="DomainName">城市域名</param>
        /// <returns>返回城市实体集合</returns>
        EyouSoft.Model.SystemStructure.SysCity GetModel(int CityId, string DomainName);

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns>返回城市实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList();

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
        IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, int ProvinceId, int CityId, bool? IsEnabled);

        /// <summary>
        /// 设置是否停用
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否停用</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsEnabled(int CityId, bool IsEnabled);

        /// <summary>
        /// 设置是否出港城市
        /// </summary>
        /// <param name="CityIds">城市ID集合</param>
        /// <param name="IsSite">是否出港</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsSite(string CityIds, bool IsSite);

        #endregion

        #region 城市线路区域关系函数

        /// <summary>
        /// 获取城市线路区域关系列表
        /// </summary>
        /// <returns>返回城市线路区域关系列表</returns>
        IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> GetSiteAreaControlList();

        /// <summary>
        /// 添加城市线路区域关系
        /// </summary>
        /// <param name="List">城市线路区域关系集合</param>
        /// <param name="CityId">城市ID(小于等于0则取集合中的)</param>
        /// <returns>返回受影响行数</returns>
        int AddSysSiteAreaControl(IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List, int CityId);

        /// <summary>
        /// 删除城市线路区域关系
        /// </summary>
        /// <param name="CityId">城市ID(必须传值)</param>
        /// <param name="AreaIds">线路区域ID集合(为null删除该城市下所有线路区域)</param>
        /// <returns>返回操作是否成功</returns>
        bool DelSysSiteAreaControl(int CityId, string AreaIds);

        #endregion

        #region 系统IP函数

        /// <summary>
        /// 根据客户端Ip地址返回城市基类实体
        /// </summary>
        /// <param name="IpAddress">客户端Ip地址</param>
        /// <returns>城市基类实体</returns>
        Model.SystemStructure.CityBase GetClientCityByIp(string IpAddress);

        #endregion
    }
}
