using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SMSStructure
{
    /// <summary>
    /// 短信中心-用户账户信息业务逻辑类
    /// </summary>
    /// Author:汪奇志 2010-06-10
    public class Account : EyouSoft.IBLL.SMSStructure.IAccount
    {
        private readonly IDAL.SMSStructure.IAccount dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.SMSStructure.IAccount>();

        #region CreateInstance
        /// <summary>
        /// 创建短信中心-用户账户信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SMSStructure.IAccount CreateInstance()
        {
            EyouSoft.IBLL.SMSStructure.IAccount op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SMSStructure.IAccount>();
            }
            return op1;
        }
        #endregion

        #region private member
        /// <summary>
        /// 获取当前用户(运营后台)用户分管的区域范围 多个用","间隔
        /// </summary>
        /// <returns></returns>
        private string GetWebMasterArea()
        {
            return new EyouSoft.Security.Membership.Utility().GetCurrentWebMasterArea();
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public decimal GetAccountMoney(string companyId)
        {
            return dal.GetAccountMoney(companyId);
        }

        /// <summary>
        /// 获取账户剩余短信条数
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetAccountSMSNumber(string companyId)
        {
            return dal.GetAccountSMSNumber(companyId);
        }
        /// <summary>
        /// 获取指定公司的账户信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>账户信息</returns>
        public EyouSoft.Model.SMSStructure.AccountInfo GetAccountInfo(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;
            return dal.GetAccountInfo(companyId);
        }
        /// <summary>
        /// 是否存在未审核的充值
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool HasNoCheckPay(string companyId)
        {
            return dal.HasNoCheckPay(companyId);
        }

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
        public bool DeductAccountMoney(string companyId, string userId, decimal money, int smscount, string tempFeeTakeId, string sendTotalId)
        {
            return dal.DeductAccountMoney(companyId, userId, money, smscount, tempFeeTakeId, sendTotalId);
        }

        /// <summary>
        /// 账户充值
        /// </summary>
        /// <param name="payMoneyInfo">充值支付信息业务实体</param>
        /// <returns></returns>
        public bool InsertPayMoney(EyouSoft.Model.SMSStructure.PayMoneyInfo payMoneyInfo)
        {
            payMoneyInfo.PayMoneyId = Guid.NewGuid().ToString();
            /*
            if (payMoneyInfo.PayMoney > 0)
            {
                //单条短信的价格
                decimal OneSMSPrice = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["SMS_COSTOFSINGLE"]);
                if (OneSMSPrice <= 0)
                    return false;
                //设置短信充值条数
                payMoneyInfo.PaySMSNumber = Convert.ToInt32(payMoneyInfo.PayMoney / OneSMSPrice);
            }
             */
            return dal.InsertPayMoney(payMoneyInfo);
        }

        /// <summary>
        /// 获取公司账户充值明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> GetPayMoneys(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) { return null; }

            return dal.GetPayMoneys(pageSize, pageIndex, ref recordCount, companyId,
                null, null, null, null, null, null, null, -1, null);
        }

        /// <summary>
        /// 获取账户充值明细(运营后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="payTime">充值时间 为null时不做为查询条件</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="checkStatus">审核状态 -1:所有状态 0:未审核 1:审核通过  2:审核未通过</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> GetPayMoneys(int pageSize, int pageIndex, ref int recordCount, string companyName, DateTime? payTime, int? provinceId, int? cityId, int checkStatus)
        {
            DateTime? payFinishTime = null;

            if (payTime.HasValue)
            {
                payFinishTime = payTime.Value.AddDays(1).AddMilliseconds(-1);
            }

            return dal.GetPayMoneys(pageSize, pageIndex, ref recordCount, null, companyName
                , payTime, payFinishTime, null, null, provinceId, cityId, checkStatus, this.GetWebMasterArea());
        }

        /*/// <summary>
        /// 获取账户充值明细(运营后台)
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
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> GetPayMoneys(int pageSize, int pageIndex, ref int recordCount, string companyId, string companyName, DateTime? payStartTime, DateTime? payFinishTime, DateTime? operatorStartTime, DateTime? operatorFinishTime, int? provinceId, int? cityId, int checkStatus)
        {
            return dal.GetPayMoneys(pageSize,pageIndex,ref recordCount,companyId,companyName,
                payStartTime,payFinishTime,operatorStartTime,operatorFinishTime,
                provinceId, cityId, checkStatus, this.GetWebMasterArea());
        }*/

        /// <summary>
        /// 获得已充值过的公司汇总
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyName">公司名称 为空时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> GetAllPayedCompanys(int pageSize, int pageIndex, ref int recordCount, string companyName)
        {
            return dal.GetAllPayedCompanys(pageSize, pageIndex, ref recordCount, companyName);
        }

        /// <summary>
        /// 获取账户消费明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.SendTotalInfo> GetExpenseDetails(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            return dal.GetExpenseDetails(pageSize, pageIndex, ref recordCount, companyId);
        }

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
        public bool CheckPayMoney(EyouSoft.Model.SMSStructure.PayMoneyInfo checkPayMoneyInfo)
        {
            return dal.CheckPayMoney(checkPayMoneyInfo);
        }

        /// <summary>
        /// 删除未通过审核[未审核/审核未通过]的充值记录
        /// </summary>
        /// <param name="PayMoneyId">充值记录ID</param>
        /// <returns></returns>
        public bool DeleteNoPassCheckPayMoney(string PayMoneyId)
        {
            return dal.DeleteNoPassCheckPayMoney(PayMoneyId);
        }

        /// <summary>
        /// 获取消费明细汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SMSStructure.AccountExpenseCollectInfo GetAccountExpenseCollectInfo(string companyId)
        {
            return dal.GetAccountExpenseCollectInfo(companyId);
        }

        /// <summary>
        /// 设置短信中心账户基础数据
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool SetAccountBaseInfo(string companyId)
        {
            if (this.IsExistsAccount(companyId)) { return true; }

            return dal.SetAccountBaseInfo(companyId);
        }

        /// <summary>
        /// 判断是否存在公司账户
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool IsExistsAccount(string companyId)
        {
            return dal.IsExistsAccount(companyId);
        }

        /// <summary>
        /// 按当前登录的用户分管的线路区域统计短信剩余
        /// </summary>
        /// <param name="pageSize">每页显示记录数量</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> RemnantStats(int pageSize, int pageIndex, ref int recordCount, int? provinceId, int? cityId, string companyName)
        {
            IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> items=dal.RemnantStats(pageSize, pageIndex, ref recordCount, 10, provinceId, cityId, companyName, this.GetWebMasterArea());

            return items;
        }
        #endregion
    }
}
