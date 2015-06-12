using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 运价套餐购买记录
    /// </summary>
    /// Author:罗丽娥  2010-11-01
    public class FreightBuyLog : EyouSoft.IBLL.TicketStructure.IFreightBuyLog
    {
        private readonly EyouSoft.IDAL.TicketStructure.IFreightBuyLog idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.IFreightBuyLog>();

        private readonly EyouSoft.IDAL.TicketStructure.ITicketFreightInfo ifdal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketFreightInfo>();

        private readonly EyouSoft.IDAL.TicketStructure.ITicketPayList idalTicketPayList = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketPayList>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.IFreightBuyLog CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.IFreightBuyLog op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.IFreightBuyLog>();
            }
            return op;
        }

        /// <summary>
        /// 购买运价航线记录
        /// </summary>
        /// <param name="model">运价套餐购买记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.TicketStructure.TicketFreightBuyLog model)
        {
            return idal.Add(model);
        }

        /// <summary>
        /// 修改支付状态
        /// </summary>
        /// <param name="TicketFreightId">主键编号</param>
        /// <param name="PayState">支付状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetPayInfo(string TicketFreightId, bool PayState)
        {
            return idal.SetPayInfo(TicketFreightId, PayState, DateTime.Now);
        }

        /// <summary>
        /// 获取可用运价条数
        /// </summary>
        /// <param name="CompanyID">供应商ID</param>
        /// <param name="RateType">业务类型</param>
        /// <returns></returns>
        public int GetAvailableCount(string CompanyID, EyouSoft.Model.TicketStructure.RateType? RateType)
        {
            return idal.GetAvailableNum(DateTime.Now, CompanyID, RateType);
        }
        /// <summary>
        /// 获取供应商的套餐购买记录统计信息
        /// </summary>
        /// <param name="StartDate">启用时间</param>
        /// <param name="CompanyId">购买公司编号</param>
        /// <param name="RateType">业务类型</param>
        /// <returns>套餐购买记录统计信息实体</returns>
        public EyouSoft.Model.TicketStructure.PackBuyLogStatistics GetPackBuyLog(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType)
        {
            return idal.GetPackBuyLog(StartDate, CompanyId, RateType);
        }
        /// <summary>
        /// 获取套餐购买记录实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>套餐购买记录实体</returns>
        public EyouSoft.Model.TicketStructure.TicketFreightBuyLog GetModel(string Id)
        {
            return idal.GetModel(Id);
        }

        /// <summary>
        /// 购买运价套餐支付前添加支付流水明细信息
        /// </summary>
        /// <param name="orderid">订单ID</param>
        /// <param name="orderno">订单号</param>
        /// <param name="userid">当前操作人ID</param>
        /// <param name="companyid">当前操作公司ID</param>
        /// <param name="payprice">支付金额</param>
        /// <param name="paytype">支付接口类型</param>
        /// <param name="batch_no">批次号</param>
        /// <returns></returns>
        public bool AddTicketPay(string orderid, string orderno, string userid, string companyid, decimal payprice, EyouSoft.Model.TicketStructure.TicketAccountType paytype, out string batch_no)
        {
            EyouSoft.Model.TicketStructure.TicketPay model = new EyouSoft.Model.TicketStructure.TicketPay();
            model.ItemId = orderid;
            model.TradeNo = orderno;
            model.CurrCompanyId = companyid;
            model.CurrUserId = userid;
            model.IssueTime = DateTime.Now;
            model.PayPrice = payprice;
            model.PayState = EyouSoft.Model.TicketStructure.PayState.未提交到支付接口;
            model.PayType = paytype;
            model.Remark = string.Empty;
            model.ItemType = EyouSoft.Model.TicketStructure.ItemType.供应商付款到平台_购买运价;
            bool Isresult = idalTicketPayList.AddTicketPay(model);
            batch_no = model.BatchNo;
            return Isresult;
        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="payNumber">支付接口返回的交易流水号</param>
        /// <param name="payprice">支付金额</param>
        /// <param name="paystate">支付状态</param>
        /// <param name="paytype">支付接口类型</param>
        /// <param name="payaccount">支付账号</param>
        /// <param name="paytradeno">提交到支付接口的交易号</param>
        /// <param name="paytime">支付时间</param>
        /// <param name="batchno">批次号</param>
        /// <returns></returns>
        public bool PayAfter(string payNumber, decimal? payprice, EyouSoft.Model.TicketStructure.PayState paystate, EyouSoft.Model.TicketStructure.TicketAccountType paytype, string payaccount, string paytradeno, DateTime? paytime, string batchno)
        {
            bool result = false;
            string itemId = string.Empty;//项目编号
            string currCompanyId = string.Empty;//当前公司ID
            string currUserId = string.Empty;//当前用户ID
            EyouSoft.Model.TicketStructure.ItemType? itemType = null;//流水明细记录项类型
            //回调后修改支付明细
            idalTicketPayList.PayCallback(payNumber, paystate,string.Empty, paytradeno, batchno, paytype, out itemId, out itemType, out currCompanyId, out currUserId);
            if (paystate == EyouSoft.Model.TicketStructure.PayState.交易完成 && itemType.Value == EyouSoft.Model.TicketStructure.ItemType.供应商付款到平台_购买运价)
            {
                result = idal.SetPayInfo(itemId, true, DateTime.Now);
            }

            #region 回写运价信息
            EyouSoft.Model.TicketStructure.TicketFreightBuyLog LogModel = idal.GetModelByOrderNo(paytradeno);
            if (LogModel != null)
            {
                if (LogModel.PackageType != EyouSoft.Model.TicketStructure.PackageTypes.常规 && !String.IsNullOrEmpty(LogModel.DestCityIds.Trim()))
                {
                    string[] tmpDestCityIdList = LogModel.DestCityIds.Split(',');
                    string[] tmpDestCityNameList = LogModel.DestCityNames.Split(',');

                    IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> FList = new List<EyouSoft.Model.TicketStructure.TicketFreightInfo>();
                    if (tmpDestCityIdList.Length > 0)
                    {
                        for (int i = 0; i < tmpDestCityIdList.Length; i++)
                        {
                            EyouSoft.Model.TicketStructure.TicketSeattle seattlemodel = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattleById(Convert.ToInt32(tmpDestCityIdList[i]));
                            if (seattlemodel != null)
                            {
                                EyouSoft.Model.TicketStructure.TicketFreightInfo FModel = new EyouSoft.Model.TicketStructure.TicketFreightInfo();
                                FModel.Id = Guid.NewGuid().ToString();
                                EyouSoft.Model.CompanyStructure.Company CompanyModel = new EyouSoft.Model.CompanyStructure.Company();
                                CompanyModel.ID = LogModel.CompanyId;
                                FModel.Company = CompanyModel;
                                CompanyModel = null;
                                FModel.FlightId = LogModel.FlightId;
                                FModel.FlightName = LogModel.FlightName;
                                FModel.RateType = LogModel.RateType;
                                FModel.NoGadHomeCityId = LogModel.HomeCityId;
                                FModel.NoGadHomeCityIdName = LogModel.HomeCityName;
                                FModel.NoGadDestCityId = int.Parse(tmpDestCityIdList[i]);
                                FModel.NoGadDestCityName = seattlemodel.Seattle;
                                FModel.BuyType = LogModel.PackageType;
                                FModel.LastUpdateDate = DateTime.Now;
                                FModel.IsEnabled = false;
                                FModel.FreightBuyId = LogModel.Id;
                                //FModel.FreightStartDate = LogModel.StartMonth;
                                //FModel.FreightEndDate = LogModel.EndMonth;
                                FList.Add(FModel);
                                FModel = null;
                            }
                        }
                        result = ifdal.AddFreightInfo(FList) > 0 ? true : false;
                        FList = null;
                    }
                }
                LogModel = null;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 根据运价启用状态与主键编号，设置购买记录可用数
        /// </summary>
        /// <param name="Id">主键编号[对应运价表中的，套餐购买编号]</param>
        /// <param name="FreightEnabled">运价启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetAvailableByFreightState(string Id, bool FreightEnabled)
        {
            return idal.SetAvailableByFreightState(Id, FreightEnabled);
        }
    }
}
