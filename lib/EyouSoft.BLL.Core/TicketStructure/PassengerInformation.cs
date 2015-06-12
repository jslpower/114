using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 乘客信息业务处理类
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class PassengerInformation:EyouSoft.IBLL.TicketStructure.IPassengerInformation
    {
        #region 私有变量
        private readonly EyouSoft.DAL.TicketStructure.PassengerInformation dal = new EyouSoft.DAL.TicketStructure.PassengerInformation();
        #endregion
        #region 构造函数
        public PassengerInformation() { }
        #endregion
        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.IPassengerInformation CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.IPassengerInformation op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.IPassengerInformation>();
            }
            return op;
        }


        #region IPassengerInformation 成员

        /// <summary>
        /// 根据团队票编号获取乘客信息
        /// </summary>
        /// <param name="groupTicketID">团队票编号</param>
        /// <returns>乘客信息集合</returns>
        public IList<EyouSoft.Model.TicketStructure.PassengerInformation> GetPassengerInformation(int groupTicketID)
        {
            return dal.GetPassengerInformation(groupTicketID);
        }

        #endregion
    }
}
