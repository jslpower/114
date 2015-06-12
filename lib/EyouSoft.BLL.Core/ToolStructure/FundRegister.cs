/*Author：汪奇志 2010-11-09*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EyouSoft.BLL.ToolStructure
{
    /// <summary>
    /// 财务管理-收款付款登记
    /// </summary>
    public class FundRegister:EyouSoft.IBLL.ToolStructure.IFundRegister
    {
        private readonly IDAL.ToolStructure.IFundRegister dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ToolStructure.IFundRegister>();

        #region CreateInstance
        /// <summary>
        /// 创建团队信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.ToolStructure.IFundRegister CreateInstance()
        {
            EyouSoft.IBLL.ToolStructure.IFundRegister op1 = null;
            if (op1 == null)
            {
                op1 = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.ToolStructure.IFundRegister>();
            }
            return op1;
        }
        #endregion

        #region private member
        /// <summary>
        /// 设置收款(已收/未审核已收)或付款(已付/未审核已付)金额
        /// </summary>
        /// <param name="itemId">收款或付款编号</param>
        /// <param name="registerType">登记类型</param>
        /// <returns></returns>
        private bool SetAmount(string itemId, EyouSoft.Model.ToolStructure.FundRegisterType registerType)
        {
            return dal.SetAmount(itemId, registerType);
        }
        #endregion

        #region IFundRegister 成员
        /// <summary>
        /// 添加收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.ToolStructure.FundRegisterInfo register)
        {
            register.RegisterId = Guid.NewGuid().ToString();
            bool result = true;

            using (TransactionScope AddTran = new TransactionScope())
            {
                result = dal.Add(register);

                if (!result) return false;

                result = this.SetAmount(register.ItemId, register.RegisterType);

                if (!result) return false;

                AddTran.Complete();
            }

            return true;
        }

        /// <summary>
        /// 删除收款或付款登记
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <returns></returns>
        public bool Delete(string registerId)
        {            
            EyouSoft.Model.ToolStructure.FundRegisterInfo register = this.GetInfo(registerId);
            if (register == null) return true;

            bool result = true;

            using (TransactionScope AddTran = new TransactionScope())
            {
                result = dal.Delete(registerId);

                if (!result) return false;
                
                result=this.SetAmount(register.ItemId, register.RegisterType);

                if (!result) return false;

                AddTran.Complete();
            }

            return true;
        }

        /// <summary>
        /// 获取收款或付款登记信息
        /// </summary>
        /// <param name="registerId">收款付款登记信息业务实体</param>
        /// <returns></returns>
        public EyouSoft.Model.ToolStructure.FundRegisterInfo GetInfo(string registerId)
        {
            if (string.IsNullOrEmpty(registerId)) return null;

            return dal.GetInfo(registerId);
        }

        /// <summary>
        /// 获取收款或付款登记信息集合
        /// </summary>
        /// <param name="itemId">收款或付款登记编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.FundRegisterInfo> GetRegisters(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) return null;

            return dal.GetRegisters(itemId);
        }

        /// <summary>
        /// 更新收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.ToolStructure.FundRegisterInfo register)
        {
            EyouSoft.Model.ToolStructure.FundRegisterInfo cRegister = this.GetInfo(register.RegisterId);
            if (cRegister == null) return false;

            bool result = true;

            using (TransactionScope AddTran = new TransactionScope())
            {
                result = dal.Update(register);

                if (!result) return false;

                result = this.SetAmount(cRegister.ItemId, cRegister.RegisterType);

                if (!result) return false;

                AddTran.Complete();
            }

            return true;
        }

        /// <summary>
        /// 收款或付款登记审核
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <param name="isChecked">审核状态 true:审核 false:未审核</param>
        /// <returns></returns>
        public bool Check(string registerId, bool isChecked)
        {            
            EyouSoft.Model.ToolStructure.FundRegisterInfo register=this.GetInfo(registerId);
            if(register==null) return true;

            bool result = true;

            using (TransactionScope AddTran = new TransactionScope())
            {
                result = dal.Check(registerId, isChecked);

                if (!result) return false;

                result = this.SetAmount(register.ItemId, register.RegisterType);

                if (!result) return false;

                AddTran.Complete();
            }

            return true;
        }

        public bool TestTran()
        {
            bool result = true;
            using (TransactionScope AddTran = new TransactionScope())
            {
                result=dal.TestTran1();

                if (!result) return false;

                result = dal.TestTran2();

                if (!result) return false;

                //return false;

                AddTran.Complete();
            }

            return true;
        }
        #endregion
    }
}
