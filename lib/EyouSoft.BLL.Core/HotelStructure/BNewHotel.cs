using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL.HotelStructure;
using EyouSoft.Model.HotelStructure;
using EyouSoft.IDAL.HotelStructure;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 二次整改酒店首页管理业务逻辑层
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class BNewHotel : IBNewHotel
    {
        private readonly IDNewHotel dal = ComponentFactory.CreateDAL<IDNewHotel>();

        #region CreateInstance

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static IBNewHotel CreateInstance()
        {
            IBNewHotel op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBNewHotel>();
            }
            return op;
        }

        #endregion

        #region 后台调用方法

        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(MNewHotelInfo model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }

        /// <summary>
        /// 修改实体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(MNewHotelInfo model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(params int[] ids)
        {
            if (ids == null && ids.Length == 0)
                return false;
            return dal.Delete(ids);
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MNewHotelInfo GetModel(int id)
        {
            if (id <= 0)
                return null;
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页编号</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public IList<MNewHotelInfo> GetList(int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount);
        }

        #endregion

        #region 前台调用方法

        /// <summary>
        ///  获取首页显示的酒店信息
        /// </summary>
        /// <param name="cityAreaType">城市区域类型</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="topNumber">个数</param>
        /// <returns>IList</returns>
        public IList<MNewHotelInfo> GetList(CityAreaType cityAreaType, string cityName, int topNumber)
        {
            return dal.GetList(cityAreaType, cityName, topNumber);
        }

        #endregion


    }
}
