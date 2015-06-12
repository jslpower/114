using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-02
    /// 描述：团队负责人接口
    /// </summary>
    public interface ITourContactInfo
    {
        /// <summary>
        /// 添加团队负责人
        /// </summary>
        /// <param name="model">团队负责人实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.CompanyStructure.TourContactInfo model);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="contactName">联系人姓名(若为空,则不作为查询条件)</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.TourContactInfo> GetList(string companyId, string contactName);
    }
}
