using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.AdvStructure
{
    /// <summary>
    /// 描述：分站旅行社推广业务接口类
    /// 修改记录:
    /// 1. 2011-05-09 AM 曹胡生 创建
    /// </summary>
    public interface ISiteExtend
    {
        /// <summary>
        /// 描述：获得分站旅行社推广列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> GetSiteExtendList(int pageSize, int pageIndex, ref int recordCount);
        
        /// <summary>
        /// 描述：根据城市编号获得分站旅行社推广列表
        /// </summary>
        /// <param name="CityId">城市编号</param>
        /// <param name="IsShow">显示状态,为NULL显示全部</param>  
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> GetSiteExtendList(int CityId, bool? IsShow);

        /// <summary>
        /// 描述：修改分站旅行社推广记录
        /// </summary>
        /// <param name="SiteExtendInfo"></param>
        /// <returns></returns>
        bool UpdateSiteExtend(EyouSoft.Model.AdvStructure.SiteExtendInfo SiteExtendInfo);
        /// <summary>
        /// 描述：修改分站旅行社推广记录（多条）
        /// </summary>
        /// <param name="SiteExtendInfoList"></param>
        /// <returns></returns>
        bool UpdateSiteExtend(IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> SiteExtendInfoList);
        /// <summary>
        /// 描述：添加分站旅行社推广记录
        /// </summary>
        /// <param name="SiteExtendInfo"></param>
        /// <returns></returns>
        bool AddSiteExtend(EyouSoft.Model.AdvStructure.SiteExtendInfo SiteExtendInfo);
        /// <summary>
        /// 描述：添加分站旅行社推广记录（多条）
        /// </summary>
        /// <param name="SiteExtendInfoList"></param>
        /// <returns></returns>
        bool AddSiteExtend(IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> SiteExtendInfoList);
        /// <summary>
        /// 描述：删除分站旅行社推广记录
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        bool DelSiteExtend(int ID);
        /// <summary>
        /// 描述：判断该公司在该城市下是否已推荐
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        bool IsZixst(int CityId, string CompanyID);
        /// <summary>
        /// 描述，得到所有分站
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.SiteExtend> GetALLExtendSite();
        /// <summary>
        /// 描述：得到所有已审核的公司
        /// </summary>
        /// <param name="CityId">城市编号，0为所有城市</param>
        /// <param name="IsPay"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.CompanySelectInfo> GetALLVerifyCompany(int pageSize, int pageIndex, ref int recordCount,int CityId, bool IsPay);
        /// <summary>
        /// 当前城市是否是二类分站
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        bool IsExtendSite(int CityId);
    }
}
