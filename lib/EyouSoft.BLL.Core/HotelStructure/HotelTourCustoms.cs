using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店团队定制-业务逻辑层
    /// </summary>
    /// 鲁功源  2010-12-02
    public class HotelTourCustoms:EyouSoft.IBLL.HotelStructure.IHotelTourCustoms
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelTourCustoms dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelTourCustoms>();

        #region CreateInstance
        /// <summary>
        /// 创建团队定制业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelTourCustoms CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelTourCustoms op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelTourCustoms>();
            }
            return op;
        }
        #endregion        

        #region IHotelTourCustoms 成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">团队定制实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.HotelStructure.HotelTourCustoms model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="model">团队定制回复实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddAsk(EyouSoft.Model.HotelStructure.HotelTourCustomsAsk model)
        {
            if (model == null)
                return false;
            return dal.AddAsk(model);
        }
        /// <summary>
        /// 获取团队定制实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>团队预定实体</returns>
        public EyouSoft.Model.HotelStructure.HotelTourCustoms GetModel(int Id)
        {
            if (Id <= 0)
                return null;
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 获取团队定制列表
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">团队定制查询实体</param>
        /// <returns>团队定制列表</returns>
        public IList<EyouSoft.Model.HotelStructure.HotelTourCustoms> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.SearchTourCustomsInfo SearchInfo)
        {
            return dal.GetList(PageSize, PageIndex, ref RecordCount, SearchInfo);
        }
        /// <summary>
        /// 获取团队定制回复列表
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TourOrderId">团队定制订单ID</param>
        /// <returns>团队定制回复列表</returns>
        public IList<EyouSoft.Model.HotelStructure.HotelTourCustomsAsk> GetListAsk(int PageSize, int PageIndex, ref int RecordCount, int TourOrderId)
        {
            if (TourOrderId <= 0)
                return null;
            return dal.GetListAsk(PageSize, PageIndex, ref RecordCount, TourOrderId);
        }
        /// <summary>
        /// 设置处理状态
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="TreatState">处理状态</param>
        /// <param name="OperatorId">操作人编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetTreatState(int Id, EyouSoft.Model.HotelStructure.OrderStateList TreatState, int OperatorId)
        {
            if (Id <= 0)
                return false;
            return dal.SetTreatState(Id, TreatState, OperatorId);
        }

        #endregion
    }
}
