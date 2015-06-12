using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 供应商业务逻辑层
    /// Author:刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public class TicketSupplierInfo : EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo
    {
        #region Private Members 私有成员
        private readonly EyouSoft.IDAL.TicketStructure.ITicketWholesalersInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketWholesalersInfo>();
        #endregion

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo>();
            }
            return op;
        }

        #region ISupplierInfo 成员
        /// <summary>
        /// 获取完整的供应商实体
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo GetSupplierInfo(string CompanyId)
        {
            EyouSoft.Model.TicketStructure.TicketWholesalersInfo ticketWholesalersInfoModel = idal.GetModel(CompanyId);
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo Model = new EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo();
            if (ticketWholesalersInfoModel != null)
            {
                Model.CompanyId = ticketWholesalersInfoModel.CompanyId;
                Model.ProxyLev = ticketWholesalersInfoModel.ProxyLev;
                //Model.SuccessRate = ticketWholesalersInfoModel.SuccessRate;
                Model.ICPNumber = ticketWholesalersInfoModel.ICPNumber;
                Model.ContactName = ticketWholesalersInfoModel.ContactName;
                Model.ContactTel = ticketWholesalersInfoModel.ContactTel;
                Model.RefundAvgTime = ticketWholesalersInfoModel.RefundAvgTime;
                Model.NoRefundAvgTime = ticketWholesalersInfoModel.NoRefundAvgTime;
                Model.HandleNum = ticketWholesalersInfoModel.HandleNum;
                Model.SubmitNum = ticketWholesalersInfoModel.SubmitNum;
                Model.ServicePrice = ticketWholesalersInfoModel.ServicePrice;
                Model.WorkStartTime = ticketWholesalersInfoModel.WorkStartTime;
                Model.WorkEndTime = ticketWholesalersInfoModel.WorkEndTime;
                Model.DeliveryPrice = ticketWholesalersInfoModel.DeliveryPrice;
                Model.IntoRatio = ticketWholesalersInfoModel.IntoRatio;
            }
            if (companyModel != null)
            {
                Model.WebSite = companyModel.WebSite;
                Model.CompanyRemark = companyModel.Remark;
                Model.AttachInfo = companyModel.AttachInfo;
                Model.ContactMQ = companyModel.ContactInfo.MQ;
            }
            if (Model != null)
                return Model;
            else
                return null;
        }
        /// <summary>
        /// 修改供应商基本信息
        /// </summary>
        /// <param name="SupplierInfo">供应商实体</param>
        /// <returns></returns>
        public bool UpatetSupplierInfo(EyouSoft.Model.TicketStructure.TicketWholesalersInfo SupplierInfo)
        {
            return idal.Update(SupplierInfo);
        }
        /// <summary>
        /// 修改供应商详细信息（供应商附属信息）
        /// </summary>
        /// <param name="Model">供应商实体</param>
        /// <returns></returns>
        public bool UpatetSupplierDetailsInfo(EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo Model)
        {
            bool result = true;
            if (Model.AttachInfo != null)//修改公司附件信息
               result= new EyouSoft.DAL.CompanyStructure.CompanyAttachInfo().SetCompanyAttachInfo(Model.AttachInfo);
            if (result)
               result=idal.Update(Model);
            return result;           
        }
        /// <summary>
        /// 获取指定公司的出票成功率
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <returns>出票成功率</returns>
        public decimal GetSuccessRate(string CompanyId)
        {
            decimal SuccessRate = 0;
            object obj= EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.AirTickets.TicketSuccessRate + CompanyId);
            if (obj == null)
            {
                SuccessRate = idal.GetSuccessRate(CompanyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.AirTickets.TicketSuccessRate + CompanyId, SuccessRate, DateTime.Now.AddHours(1));
            }
            else
            {
                SuccessRate = decimal.Parse(obj.ToString());
            }
            return SuccessRate;
        }
        #endregion
    }
}
