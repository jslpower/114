using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 团队负责人业务逻辑层
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public class TourContactInfo : EyouSoft.IBLL.CompanyStructure.ITourContactInfo
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ITourContactInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ITourContactInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ITourContactInfo CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ITourContactInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ITourContactInfo>();
            }
            return op;
        }

        /// <summary>
        /// 添加团队负责人[若负责人已存在,则修改负责人信息]
        /// </summary>
        /// <param name="model">团队负责人实体</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.CompanyStructure.TourContactInfo model)
        {
            return idal.Add(model);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="contactName">联系人姓名(若为空,则不作为查询条件)</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.TourContactInfo> GetList(string companyId, string contactName)
        {
            return idal.GetList(companyId, contactName);
        }
    }
}
