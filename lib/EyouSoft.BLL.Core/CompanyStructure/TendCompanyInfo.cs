using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 经营单位信息业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-02
    public class TendCompanyInfo : EyouSoft.IBLL.CompanyStructure.ITendCompanyInfo
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ITendCompanyInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ITendCompanyInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ITendCompanyInfo CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ITendCompanyInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ITendCompanyInfo>();
            }
            return op;
        }

        /// <summary>
        /// 添加经营单位
        /// </summary>
        /// <param name="model">经营单位实体类</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.CompanyStructure.TendCompanyInfo model)
        {
            return idal.Add(model);
        }
        /// <summary>
        /// 修改经营单位
        /// </summary>
        /// <param name="model">经营单位实体类</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.CompanyStructure.TendCompanyInfo model)
        {
            return idal.Update(model);
        }
        /// <summary>
        /// 删除经营单位
        /// </summary>
        /// <param name="companyId">经营单位所属公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        public bool Delete(string companyId, int areaId)
        {
            return idal.Delete(companyId, areaId);
        }
        /// <summary>
        /// 获取经营单位实体
        /// </summary>
        /// <param name="companyid">经营单位所属公司ID</param>
        /// <param name="areaId">线路区域ID</param>
        /// <returns></returns>
        public EyouSoft.Model.CompanyStructure.TendCompanyInfo GetList(string companyid, int areaId)
        {
            return idal.GetList(companyid, areaId);
        }
    }
}
