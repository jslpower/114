/*Author：汪奇志 2010-11-09*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.ToolStructure
{
    /// <summary>
    /// 财务管理-收款付款登记业务逻辑接口
    /// </summary>
    public interface IFundRegister
    {
        /// <summary>
        /// 添加收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Add_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Add,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""RegisterId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Add_CODE)]
        bool Add(EyouSoft.Model.ToolStructure.FundRegisterInfo register);

        /// <summary>
        /// 删除收款或付款登记
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Delete_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Delete,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""registerId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Delete_CODE)]
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
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Update_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Update,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""RegisterId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Update_CODE)]
        bool Update(EyouSoft.Model.ToolStructure.FundRegisterInfo register);

        /// <summary>
        /// 收款或付款登记审核
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <param name="isChecked">审核状态 true:审核 false:未审核</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Check_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Check,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""registerId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_FundRegister_Check_CODE)]
        bool Check(string registerId, bool isChecked);

        /// <summary>
        /// 分布式事务测试
        /// </summary>
        /// <returns></returns>
        bool TestTran();
    }
}
