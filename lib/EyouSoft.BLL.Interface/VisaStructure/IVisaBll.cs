using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.VisaStructure
{
    /// <summary>
    /// 旅游签证业务层接口
    /// </summary>
    /// 郑知远 2011-05-06
    public interface IVisaBll
    {
        #region 网站前台方法

        /// <summary>
        /// 获取所有国家信息实体
        /// </summary>
        /// <returns>国家信息实体</returns>
        IList<EyouSoft.Model.VisaStructure.Country> GetCountryList();

        /// <summary>
        /// 根据热点国家中文名获取国家信息列表实体
        /// </summary>
        /// <param name="strHotCountrys">热点国家中文名</param>
        /// <returns>国家信息列表实体</returns>
        IList<EyouSoft.Model.VisaStructure.Country> GetHotCountryListByName(string[] strHotCountrys);

        /// <summary>
        /// 根据国家ID获取旅游签证信息实体
        /// </summary>
        /// <param name="countryId">国家ID</param>
        /// <returns>旅游签证信息实体</returns>
        EyouSoft.Model.VisaStructure.Visa GetVisaInfoByCountry(int countryId);

        #endregion
    }
}
