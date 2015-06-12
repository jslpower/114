using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.AdvStructure
{
    /// <summary>
    /// 描述：分站旅行社推广业务类
    /// 修改记录:
    /// 1. 2011-05-09 AM 曹胡生 创建
    /// </summary>
    public class SiteExtend : EyouSoft.IBLL.AdvStructure.ISiteExtend
    {
        private readonly EyouSoft.IDAL.AdvStructure.ISiteExtend dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.AdvStructure.ISiteExtend>();
        /// <summary>
        /// 创建分站旅行社推广接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.AdvStructure.ISiteExtend CreateInstance()
        {
            EyouSoft.IBLL.AdvStructure.ISiteExtend op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.AdvStructure.ISiteExtend>();
            }
            return op;
        }

        /// <summary>
        /// 描述：获得分站旅行社推广列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> GetSiteExtendList(int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetSiteExtendList(pageSize, pageIndex, ref recordCount);
        }

        /// <summary>
        /// 描述：根据城市编号获得分站旅行社推广列表
        /// </summary>
        /// <param name="CityId">城市编号</param>
        /// <param name="IsShow">显示状态,为NULL显示全部</param> 
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> GetSiteExtendList(int CityId, bool? IsShow)
        {
            return dal.GetSiteExtendList(CityId, IsShow);
        }

        /// <summary>
        /// 描述：修改分站旅行社推广记录
        /// </summary>
        /// <param name="SiteExtendInfo"></param>
        /// <returns></returns>
        public bool UpdateSiteExtend(EyouSoft.Model.AdvStructure.SiteExtendInfo SiteExtendInfo)
        {
            return dal.UpdateSiteExtend(SiteExtendInfo);
        }

        /// <summary>
        /// 描述：修改分站旅行社推广记录（多条）
        /// </summary>
        /// <param name="SiteExtendInfoList"></param>
        /// <returns></returns>
        public bool UpdateSiteExtend(IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> SiteExtendInfoList)
        {
            foreach (var item in SiteExtendInfoList)
            {
                if (item.ID != 0)
                {
                    this.UpdateSiteExtend(item);
                }
            }
            return true;
        }

        /// <summary>
        /// 描述：添加分站旅行社推广记录
        /// </summary>
        /// <param name="SiteExtendInfo"></param>
        /// <returns></returns>
        public bool AddSiteExtend(EyouSoft.Model.AdvStructure.SiteExtendInfo SiteExtendInfo)
        {
            if (SiteExtendInfo.ShowCityID != 0 && !string.IsNullOrEmpty(SiteExtendInfo.CompanyID))
            {
                if (!IsZixst(SiteExtendInfo.ShowCityID, SiteExtendInfo.CompanyID))
                {
                    return dal.AddSiteExtend(SiteExtendInfo);
                }
            }
            return false;
        }
        /// <summary>
        /// 描述：添加分站旅行社推广记录（多条）
        /// </summary>
        /// <param name="SiteExtendInfoList"></param>
        /// <returns></returns>
        public bool AddSiteExtend(IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> SiteExtendInfoList)
        {
            foreach (var item in SiteExtendInfoList)
            {
                if (item.ShowCityID != 0 && !string.IsNullOrEmpty(item.CompanyID))
                {
                    if (!IsZixst(item.ShowCityID, item.CompanyID))
                    {
                        this.AddSiteExtend(item);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 描述：删除分站旅行社推广记录
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public bool DelSiteExtend(int ID)
        {
            return dal.DelSiteExtend(ID);
        }

        /// <summary>
        /// 描述：判断该公司在该城市下是否已推荐
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public bool IsZixst(int CityId, string CompanyID)
        {
            return dal.IsZixst(CityId, CompanyID);
        }
        /// <summary>
        /// 描述，得到所有分站
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.SiteExtend> GetALLExtendSite()
        {
            return dal.GetALLExtendSite();
        }
        /// <summary>
        /// 描述：得到所有已审核的公司
        /// </summary>
        /// <param name="CityId">城市编号，0为所有城市</param>
        /// <param name="IsPay"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.CompanySelectInfo> GetALLVerifyCompany(int pageSize, int pageIndex, ref int recordCount,int CityId,bool IsPay)
        {
            return dal.GetALLVerifyCompany(pageSize, pageIndex, ref recordCount,CityId, IsPay);
        }

        /// <summary>
        /// 当前城市是否是二类分站
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public bool IsExtendSite(int CityId)
        {
            return dal.IsExtendSite(CityId);
        }
    }
}
