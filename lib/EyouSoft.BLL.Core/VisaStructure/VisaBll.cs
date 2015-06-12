using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.Model.VisaStructure;

namespace EyouSoft.BLL.VisaStructure
{
    /// <summary>
    /// 旅游签证
    /// </summary>
    /// 郑知远 2011-05-06
    public class VisaBll:IBLL.VisaStructure.IVisaBll
    {
        private readonly IDAL.VisaStructure.IVisaDal dal = ComponentFactory.CreateDAL<IDAL.VisaStructure.IVisaDal>();

        /// <summary>
        /// 旅游签证业务层接口接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.VisaStructure.IVisaBll CreateInstance()
        {
            IBLL.VisaStructure.IVisaBll op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.VisaStructure.IVisaBll>();
            }
            return op;
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public VisaBll() { }
        #endregion

        #region 前台运用 VisaBll 成员
        /// <summary>
        /// 获取所有国家信息实体
        /// </summary>
        /// <returns>国家信息实体</returns>
        public IList<EyouSoft.Model.VisaStructure.Country> GetCountryList()
        {
            IList<EyouSoft.Model.VisaStructure.Country> list = null;

            var cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.Visa.CountrySearch);
            if (cachelist != null)
            {
                list = (IList<EyouSoft.Model.VisaStructure.Country>)cachelist;
            }
            else
            {
                list = this.dal.GetCountryList();
                if (list == null)
                {
                    return null;
                }

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.Visa.CountrySearch, list);
            }

            return list;
        }

        /// <summary>
        /// 根据热点国家中文名获取国家信息列表实体
        /// </summary>
        /// <param name="strHotCountrys">热点国家中文名</param>
        /// <returns>国家信息列表实体</returns>
        public IList<EyouSoft.Model.VisaStructure.Country> GetHotCountryListByName(string[] strHotCountrys)
        {
            if (strHotCountrys != null && strHotCountrys.Length > 0)
            {
                return this.dal.GetHotCountryListByName(strHotCountrys);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据国家ID获取旅游签证实体
        /// </summary>
        /// <param name="countryId">国家ID</param>
        /// <returns>旅游签证信息实体</returns>
        public EyouSoft.Model.VisaStructure.Visa GetVisaInfoByCountry(int countryId)
        {
            if (countryId<=0)
                return null;
            return this.dal.GetVisaInfoByCountry(countryId);
        }
        #endregion
    }
}
