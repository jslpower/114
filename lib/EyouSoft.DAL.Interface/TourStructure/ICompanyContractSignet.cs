using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 盖章数据访问接口
    /// </summary>
    /// Author:汪奇志 2010-07-08
    public interface ICompanyContractSignet
    {
        /// <summary>
        /// 盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="signetPath">图章路径</param>
        /// <param name="contractType">1:出团通知书 2:确认单</param>
        /// <param name="signetType">1:买方 2:卖方</param>
        /// <returns></returns>
        bool ToStamp(string orderId, string signetPath, int contractType, int signetType);

        /// <summary>
        /// 取消盖章
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="contractType">1:出团通知书 2:确认单</param>
        /// <param name="signetType">1:买方 2:卖方</param>
        /// <returns></returns>
        bool CancelStamp(string orderId, int contractType, int signetType);

        /// <summary>
        /// 获取出团通知书盖章信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        string GetGroupAdviceSignet(string orderId);

        /// <summary>
        /// 获取确认单盖章信息 string[2] [0]:买方 [1]:卖方
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>string[2] [0]:买方 [1]:卖方</returns>
        string[] GetConfirmationSignet(string orderId);
    }
}
