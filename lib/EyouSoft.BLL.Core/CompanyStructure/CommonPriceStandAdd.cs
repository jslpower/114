using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-03
    /// 描述：待新增公用价格等级业务逻辑
    /// </summary>
    public class CommonPriceStandAdd : EyouSoft.IBLL.CompanyStructure.ICommonPriceStandAdd
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICommonPriceStandAdd idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICommonPriceStandAdd>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICommonPriceStandAdd CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICommonPriceStandAdd op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICommonPriceStandAdd>();
            }
            return op;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">待新增公用价格等级实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.CommonPriceStandAdd model)
        {
            return idal.Add(model);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CommonPriceStandAdd> GetList()
        {
            return idal.GetList();
        }
    }
}
