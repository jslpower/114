using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 单位区域设置业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public class CompanyAreaSetting : EyouSoft.IBLL.CompanyStructure.ICompanyAreaSetting
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyAreaSetting idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyAreaSetting>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyAreaSetting CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyAreaSetting op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyAreaSetting>();
            }
            return op;
        }
        /// <summary>
        /// 修改单位区域设置
        /// </summary>
        /// <param name="items">单位设置实体类列表</param>
        /// <returns></returns>
        public bool Update(IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> items)
        {
            return idal.Update(items);
        }
        /// <summary>
        /// 获得单位区域设置列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> GetList(string companyId)
        {
            return idal.GetList(companyId);
        }
        /// <summary>
        /// 获得单位区域设置实体类[若不存在设置信息,则默认返回空值]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.CompanyAreaSetting GetModel(string companyId, int areaId)
        {
            return idal.GetModel(companyId, areaId);
        }
    }
}
