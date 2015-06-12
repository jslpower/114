using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 单位城市广告关系 数据访问接口
    /// </summary>
    /// 周文超 2010-07-08
    public interface ICompanyCityAd
    {
        /// <summary>
        /// 新增单位城市广告关系
        /// </summary>
        /// <param name="List">单位城市广告关系</param>
        /// <returns>返回受影响行数</returns>
        int AddCompanyCityAd(IList<Model.SystemStructure.CompanyCityAd> List);

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="Ids">单位城市广告关系ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteCompanyCityAd(string Ids);

        /// <summary>
        /// 根据城市和线路区域获取单位城市广告关系中的公司ID集合
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>公司ID集合</returns>
        IList<string> GetCompanyIdsByCityAndArea(int CityId, int AreaId);

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="CityId">城市ID（必须传值）</param>
        /// <param name="AreaId">线路区域ID（必须传值）</param>
        /// <param name="CompanyId">公司ID（未null不作条件，删除该城市该线路区域下所有公司）</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteCompanyCityAd(int CityId, int AreaId, string CompanyId);
    }
}
