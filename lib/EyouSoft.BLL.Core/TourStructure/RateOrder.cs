using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 订单评价信息业务逻辑
    /// </summary>
    /// 周文超 2010-05-25
    public class RateOrder : IBLL.TourStructure.IRateOrder
    {
        private readonly IDAL.TourStructure.IRateOrder dal = ComponentFactory.CreateDAL<IDAL.TourStructure.IRateOrder>();

        /// <summary>
        /// 构造订单评价信息业务逻辑接口
        /// </summary>
        /// <returns>订单评价信息业务逻辑接口</returns>
        public IBLL.TourStructure.IRateOrder CreateInstance()
        {
            IBLL.TourStructure.IRateOrder op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.TourStructure.IRateOrder>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateOrder()
        { }

        #region IRateOrder 成员

        /// <summary>
        /// 增加一条订单评价信息
        /// </summary>
        /// <param name="model">订单评价信息实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddRateOrder(EyouSoft.Model.TourStructure.RateOrder model)
        {
            if (model == null)
                return 0;

            if (dal.AddRateOrder(model) > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 更新一条订单评价信息，评论日期不更新
        /// </summary>
        /// <param name="model">订单评价信息实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateRateOrderById(EyouSoft.Model.TourStructure.RateOrder model)
        {
            if (model == null)
                return 0;

            if (dal.UpdateRateOrderById(model) > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 获取某一公司作为卖家时所有的订单评价数据
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0:IssueTime升序；1:IssueTime降序</param>
        /// <param name="SellCompanyId">卖家公司ID(必须传值)</param>
        /// <param name="RateType">评价类型(小于等于0不作条件)</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.RateOrder> GetRateOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string SellCompanyId, int RateType)
        {
            if (string.IsNullOrEmpty(SellCompanyId))
                return null;

            return dal.GetRateOrderList(PageSize, PageIndex, ref RecordCount, OrderIndex, SellCompanyId, RateType);
        }

        #endregion
    }
}
