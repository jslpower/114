using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-02
    /// 描述：经营单位数据访问接口层
    /// </summary>
    public interface ITendCompanyInfo
    {
        /// <summary>
        /// 添加经营单位
        /// </summary>
        /// <param name="model">经营单位实体类</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.CompanyStructure.TendCompanyInfo model);
        /// <summary>
        /// 修改经营单位
        /// </summary>
        /// <param name="model">经营单位实体类</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.CompanyStructure.TendCompanyInfo model);
        /// <summary>
        /// 删除经营单位
        /// </summary>
        /// <param name="companyId">经营单位所属公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        bool Delete(string companyId, int areaId);
        /// <summary>
        /// 获取经营单位实体
        /// </summary>
        /// <param name="companyid">经营单位所属公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.TendCompanyInfo GetList(string companyid, int areaId);
    }
}
