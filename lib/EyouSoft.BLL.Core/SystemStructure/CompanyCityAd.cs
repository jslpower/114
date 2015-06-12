using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 单位城市广告关系 业务逻辑接口
    /// </summary>
    /// 周文超 2010-07-08
    public class CompanyCityAd : IBLL.SystemStructure.ICompanyCityAd
    {
        private readonly IDAL.SystemStructure.ICompanyCityAd dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ICompanyCityAd>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyCityAd() { }

        /// <summary>
        /// 构造单位城市广告关系业务逻辑接口
        /// </summary>
        /// <returns>单位城市广告关系业务逻辑接口</returns>
        public static IBLL.SystemStructure.ICompanyCityAd CreateInstance()
        {
            IBLL.SystemStructure.ICompanyCityAd op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ICompanyCityAd>();
            }
            return op;
        }


        #region ICompanyCityAd 成员

        /// <summary>
        /// 新增单位城市广告关系
        /// </summary>
        /// <param name="model">单位城市广告关系</param>
        /// <returns>返回受影响行数</returns>
        public int AddCompanyCityAd(Model.SystemStructure.CompanyCityAd model)
        {
            if (model == null)
                return 0;

            IList<EyouSoft.Model.SystemStructure.CompanyCityAd> tmp = new List<Model.SystemStructure.CompanyCityAd>();
            tmp.Add(model);
            int Result = dal.AddCompanyCityAd(tmp);

            if (Result > 0)
            {
                EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().ClearCache(model.CityId);
            }

            return Result;
        }

        /// <summary>
        /// 新增单位城市广告关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <param name="CompanyId">公司ID集合</param>
        /// <returns>返回受影响行数</returns>
        /// <returns>大于0：Success;小于等于0：Error</returns>
        public int AddCompanyCityAd(int CityId, int AreaId, IList<string> CompanyId)
        {

            if (CompanyId == null || CompanyId.Count <= 0 || CityId <= 0 || AreaId <= 0)
                return 0;

            IList<Model.SystemStructure.CompanyCityAd> List = new List<Model.SystemStructure.CompanyCityAd>();

            foreach (string c in CompanyId)
            {
                List.Add(new EyouSoft.Model.SystemStructure.CompanyCityAd()
                {
                    AreaId = AreaId,
                    CityId = CityId,
                    CompanyId = c
                });
            }

            int Result = dal.AddCompanyCityAd(List);     
           
            if(Result>0)
                EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().ClearCache(CityId);

            return Result;
        }

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="Id">单位城市广告关系ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteCompanyCityAd(int CityId, int Id)
        {
            if (Id <= 0)
                return false;

            bool Result = dal.DeleteCompanyCityAd(Id.ToString());
            if (Result)
            {
                EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().ClearCache(CityId);
            }

            return Result;
        }

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="Ids">单位城市广告关系ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteCompanyCityAd(int CityId, int[] Ids)
        {
            if (Ids == null || Ids.Length <= 0)
                return false;

            string strTmpIds = string.Empty;
            for (int i = 0; i < Ids.Length; i++)
            {
                strTmpIds += Ids[i].ToString() + ",";
            }
            strTmpIds = strTmpIds.TrimEnd(',');

            bool Result = dal.DeleteCompanyCityAd(strTmpIds);
            if (Result)
            {
                EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().ClearCache(CityId);
            }

            return Result;
        }

        /// <summary>
        /// 根据城市和线路区域获取单位城市广告关系中的公司ID集合
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>公司ID集合</returns>
        public IList<string> GetCompanyIdsByCityAndArea(int CityId, int AreaId)
        {
            if (CityId <= 0 || AreaId <= 0)
                return null;

            return dal.GetCompanyIdsByCityAndArea(CityId, AreaId);
        }

        /// <summary>
        /// 删除单位城市广告关系
        /// </summary>
        /// <param name="CityId">城市ID（必须传值）</param>
        /// <param name="AreaId">线路区域ID（必须传值）</param>
        /// <param name="CompanyId">公司ID（未null不作条件，删除该城市该线路区域下所有公司）</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteCompanyCityAd(int CityId, int AreaId, string CompanyId)
        {
            if (CityId <= 0 || AreaId <= 0)
                return false;

            bool Result = dal.DeleteCompanyCityAd(CityId, AreaId, CompanyId);
            if (Result)
            {
                EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().ClearCache(CityId);
            }

            return Result;
        }

        #endregion
    }
}
