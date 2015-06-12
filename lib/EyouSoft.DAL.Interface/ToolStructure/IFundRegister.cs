/*Author：汪奇志 2010-11-09*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ToolStructure
{
    /// <summary>
    /// 财务管理-收款付款登记数据访问接口
    /// </summary>
    public interface IFundRegister
    {
        /// <summary>
        /// 添加收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.ToolStructure.FundRegisterInfo register);        

        /// <summary>
        /// 删除收款或付款登记
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <returns></returns>
        bool Delete(string registerId);

        /// <summary>
        /// 获取收款或付款登记信息
        /// </summary>
        /// <param name="registerId">收款付款登记信息业务实体</param>
        /// <returns></returns>
        EyouSoft.Model.ToolStructure.FundRegisterInfo GetInfo(string registerId);

        /// <summary>
        /// 获取收款或付款登记信息集合
        /// </summary>
        /// <param name="itemId">收款或付款登记编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.FundRegisterInfo> GetRegisters(string itemId);

        /// <summary>
        /// 更新收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.ToolStructure.FundRegisterInfo register);

        /// <summary>
        /// 收款或付款登记审核
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <param name="isChecked">审核状态 true:审核 false:未审核</param>
        /// <returns></returns>
        bool Check(string registerId, bool isChecked);

        /// <summary>
        /// 设置收款(已收/未审核已收)或付款(已付/未审核已付)金额
        /// </summary>
        /// <param name="itemId">收款或付款编号</param>
        /// <param name="registerType">登记类型</param>
        /// <returns></returns>
        bool SetAmount(string itemId,EyouSoft.Model.ToolStructure.FundRegisterType registerType);

        bool TestTran1();

        bool TestTran2();
    }
}
