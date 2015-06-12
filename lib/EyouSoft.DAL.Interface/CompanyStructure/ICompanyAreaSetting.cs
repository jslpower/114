using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 单位区域数据访问接口层
    /// </summary>
    /// 创建人：张志瑜 2010-06-01
    public interface ICompanyAreaSetting
    {
        /// <summary>
        /// 修改单位区域设置
        /// </summary>
        /// <param name="items">单位设置实体类列表</param>
        /// <returns></returns>
        bool Update(IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> items);
        /// <summary>
        /// 获得单位区域设置列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> GetList(string companyId);
        /// <summary>
        /// 获得单位区域设置实体类[若不存在设置信息,则默认返回空值]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyAreaSetting GetModel(string companyId, int areaId);
    }
}
