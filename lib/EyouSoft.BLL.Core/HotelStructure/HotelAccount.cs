using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-结算帐号BLL
    /// </summary>
    /// 创建人：luofx 2010-12-2
    public class HotelAccount : EyouSoft.IBLL.HotelStructure.IHotelAccount
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelAccount dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelAccount>();
        #region CreateInstance
        /// <summary>
        /// 创建结算帐号逻辑接口实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelAccount CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelAccount op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelAccount>();
            }
            return op;
        }
        #endregion
        /// <summary>
        /// 添加结算帐号信息
        /// </summary>
        /// <param name="model">结算帐号信息实体</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.HotelStructure.HotelAccount model)
        {
            bool IsTrue = false;
            if (!this.IsExist(model.CompanyId))
            {
                IsTrue = dal.Add(model);
            }
            return IsTrue;
        }
        /// <summary>
        /// 修改结算帐号信息
        /// </summary>
        /// <param name="model">结算帐号信息实体</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.HotelStructure.HotelAccount model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 获取结算帐号信息实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.HotelAccount GetModel(string CompanyId)
        {
            return dal.GetModel(CompanyId);
        }
        /// <summary>
        /// 判断是否存在账户
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool IsExist(string CompanyId)
        {
            return dal.IsExist(CompanyId);
        }
    }
}
