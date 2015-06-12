using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.OpenStructure
{
    /// <summary>
    /// 同步各平台数据异常信息业务逻辑
    /// </summary>
    /// 周文超 2011-04-02
    public class BException : IBLL.OpenStructure.IBException
    {
        /// <summary>
        /// 同步各平台数据异常信息数据访问接口
        /// </summary>
        private readonly IDAL.OpenStructure.IDException dal = Component.Factory.ComponentFactory.CreateDAL<IDAL.OpenStructure.IDException>();

        /// <summary>
        /// 构造订单信息业务逻辑接口
        /// </summary>
        /// <returns>订单信息业务逻辑接口</returns>
        public static IBLL.OpenStructure.IBException CreateInstance()
        {
            IBLL.OpenStructure.IBException op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.OpenStructure.IBException>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BException()
        {

        }

        #region IMException 成员

        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="model">异常信息实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddMException(EyouSoft.Model.OpenStructure.MExceptionInfo model)
        {
            if (model == null)
                return 0;

            IList<Model.OpenStructure.MExceptionInfo> list = new List<Model.OpenStructure.MExceptionInfo>();
            list.Add(model);
            return this.AddMException(list);
        }

        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="list">异常信息实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddMException(IList<EyouSoft.Model.OpenStructure.MExceptionInfo> list)
        {
            if (list == null || list.Count <= 0)
                return 0;

            return dal.AddMException(list);
        }

        #endregion
    }
}
