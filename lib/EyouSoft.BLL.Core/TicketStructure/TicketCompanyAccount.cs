using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 功能描述：平台机票供应商（采购商）公司账户信息业务逻辑层
    /// Author:刘咏梅
    /// CreateTime:2010-11-08
    /// </summary>
    public class TicketCompanyAccount : EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketCompanyAccount idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketCompanyAccount>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount>();
            }
            return op;
        }

        #region ITicketCompanyAccount 成员
        /// <summary>
        /// 新增平台机票供应商
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns></returns>
        public bool AddTicketCompanyAccount(EyouSoft.Model.TicketStructure.TicketCompanyAccount model)
        {
            if (idal.Add(model) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 更新平台机票供应商
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns></returns>
        public bool UpdateTicketCompanyAccount(EyouSoft.Model.TicketStructure.TicketCompanyAccount model)
        {
            if (idal.Update(model) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除平台机票供应商
        /// </summary>
        /// <param name="TicketIds">公司账户信息ID</param>
        /// <returns></returns>
        public bool DeleteTicketCompanyAccount(params int[] TicketIds)
        {
            if (idal.Delete(TicketIds) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取平台机票供应商集合
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> GetTicketCompanyAccountList(string CompanyId)
        {
            IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list = idal.GetList(CompanyId);
            if (list.Count != 0 && list != null)
                return list;
            else
                return null;
        }

        /// <summary>
        /// 根据账户类型和公司ID返回公司账户实体
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="accountType">账户类型</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketCompanyAccount GetModel(string companyId, EyouSoft.Model.TicketStructure.TicketAccountType accountType)
        {
            IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list = GetTicketCompanyAccountList(companyId);
            EyouSoft.Model.TicketStructure.TicketCompanyAccount model=null;

            if (list != null)
            {
                model = list.FirstOrDefault(i => i.InterfaceType == accountType);
            }            
            return model;
        }


        /// <summary>
        /// 根据公司编号及支付接口类型设置是否加入支付圈电子协议签约
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="isSign">是否加入支付圈电子协议签约</param>
        /// <returns></returns>
        public bool SetIsSign(string companyId, EyouSoft.Model.TicketStructure.TicketAccountType accountType, bool isSign)
        {
            return idal.SetIsSign(companyId, accountType, isSign);
        }
        #endregion
    }
}
