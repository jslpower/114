using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SMSStructure
{
    /// <summary>
    /// 短信中心-用户账户信息数据访问类接口
    /// </summary>
    /// Author:汪奇志 2010-03-26
    public interface IAccount
    {
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        decimal GetAccountMoney(string companyId);

        /// <summary>
        /// 获取账户剩余短信条数
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetAccountSMSNumber(string companyId);

        /// <summary>
        /// 获取指定公司的账户信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>账户信息</returns>
        EyouSoft.Model.SMSStructure.AccountInfo GetAccountInfo(string companyId);
        /// <summary>
        /// 是否存在未审核的充值
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool HasNoCheckPay(string companyId);

        /// <summary>
        /// 扣除账户余额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="money">扣除金额</param>
        /// <param name="smscount">扣除短信条数</param>
        /// <param name="tempFeeTakeId">金额扣除临时表编号</param>
        /// <param name="sendTotalId">短信发送统计表编号</param>
        /// <returns></returns>
        bool DeductAccountMoney(string companyId, string userId, decimal money, int smscount, string tempFeeTakeId, string sendTotalId);

        /// <summary>
        /// 账户充值
        /// </summary>
        /// <param name="payMoneyInfo">充值支付信息业务实体</param>
        /// <returns></returns>
        bool InsertPayMoney(EyouSoft.Model.SMSStructure.PayMoneyInfo payMoneyInfo);

        /// <summary>
        /// 获取账户充值明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号 为null时取所有公司</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="payStartTime">充值开始时间 为null时不做为查询条件</param>
        /// <param name="payFinishTime">充值截止时间 为null时不做为查询条件</param>
        /// <param name="operatorStartTime">操作开始时间 为null时不做为查询条件</param>
        /// <param name="operatorFinishTime">操作截止时间 为null时不做为查询条件</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="checkStatus">审核状态 -1:所有状态 0:未审核 1:审核通过  2:审核未通过</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> GetPayMoneys(int pageSize, int pageIndex, ref int recordCount, string companyId, string companyName, 
            DateTime? payStartTime, DateTime? payFinishTime, DateTime? operatorStartTime, DateTime? operatorFinishTime,
            int? provinceId, int? cityId, int checkStatus, string userAreas);

        /// <summary>
        /// 获得已充值过的公司汇总
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyName">公司名称 为空时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> GetAllPayedCompanys(int pageSize, int pageIndex, ref int recordCount, string companyName);

        /// <summary>
        /// 获取账户消费明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.SendTotalInfo> GetExpenseDetails(int pageSize, int pageIndex, ref int recordCount, string companyId);

        /// <summary>
        /// 充值审核
        /// </summary>
        /// <param name="checkPayMoneyInfo">账户充值业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 实体需要设置如下信息：
        /// PayMoneyId：充值支付编号
        /// IsChecked：审核状态 0:未审核 1:审核通过  2:审核未通过
        /// CheckTime：审核时间
        /// CheckUserName：审核人用户名
        /// CheckUserFullName：审核人姓名
        /// </remarks>
        bool CheckPayMoney(EyouSoft.Model.SMSStructure.PayMoneyInfo checkPayMoneyInfo);

        /// <summary>
        /// 删除未通过审核[未审核/审核未通过]的充值记录
        /// </summary>
        /// <param name="PayMoneyId">充值记录ID</param>
        /// <returns></returns>
        bool DeleteNoPassCheckPayMoney(string PayMoneyId);

        /// <summary>
        /// 获取消费明细汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.SMSStructure.AccountExpenseCollectInfo GetAccountExpenseCollectInfo(string companyId);

        /// <summary>
        /// 设置短信中心账户基础数据
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool SetAccountBaseInfo(string companyId);

        /// <summary>
        /// 判断是否存在公司账户
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool IsExistsAccount(string companyId);

        /// <summary>
        /// 短信剩余统计
        /// </summary>
        /// <param name="pageSize">每页显示记录数量</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="expression">剩余金额的数值表达式</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> RemnantStats(int pageSize, int pageIndex, ref int recordCount, double expression, int? provinceId, int? cityId, string companyName, string userAreas);
    }
}
