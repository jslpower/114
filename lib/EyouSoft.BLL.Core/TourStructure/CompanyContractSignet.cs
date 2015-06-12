using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 盖章业务逻辑
    /// </summary>
    /// Author:汪奇志 2010-07-08
    public class CompanyContractSignet:EyouSoft.IBLL.TourStructure.ICompanyContractSignet
    {
        private readonly IDAL.TourStructure.ICompanyContractSignet dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ICompanyContractSignet>();
        
        #region CreateInstance
        /// <summary>
        /// 创建盖章业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TourStructure.ICompanyContractSignet CreateInstance()
        {
            EyouSoft.IBLL.TourStructure.ICompanyContractSignet op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.TourStructure.ICompanyContractSignet>();
            }
            return op;
        }
        #endregion
        
        /// <summary>
        /// 出团通知书盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="signetPath">图章路径</param>
        /// <returns></returns>
        public bool GroupAdviceToStamp(string orderId, string signetPath)
        {
            return dal.ToStamp(orderId, signetPath, 1, 1);
        }

        /// <summary>
        /// 确认单盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="signetPath">图章路径</param>
        /// <param name="signetType">1:买方 2:卖方</param>
        /// <returns></returns>
        public bool ConfirmationToStamp(string orderId, string signetPath, int signetType)
        {
            return dal.ToStamp(orderId, signetPath, 2, signetType);
        }

        /// <summary>
        /// 取消出团通知书盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public bool CancelGroupAdviceStamp(string orderId)
        {
            return dal.CancelStamp(orderId, 1, 1);
        }

        /// <summary>
        /// 取消确认单盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="signetType">1:买方 2:卖方</param>
        /// <returns></returns>
        public bool CancelConfirmationStamp(string orderId, int signetType)
        {
            return dal.CancelStamp(orderId, 2, signetType);
        }

        /// <summary>
        /// 获取出团通知书盖章信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public string GetGroupAdviceSignet(string orderId)
        {
            return dal.GetGroupAdviceSignet(orderId);
        }

        /// <summary>
        /// 获取确认单盖章信息 string[2] [0]:买方 [1]:卖方
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>string[2] [0]:买方 [1]:卖方</returns>
        public string[] GetConfirmationSignet(string orderId)
        {
            return dal.GetConfirmationSignet(orderId);
        }
    }
}
