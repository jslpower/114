using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.HotelStructure;

namespace EyouSoft.IBLL.HotelStructure
{
    /// <summary>
    /// 二次整改酒店管理业务逻辑层接口类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public interface IBNewHotel
    {
        #region 后台方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        bool Add(MNewHotelInfo model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(MNewHotelInfo model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(params int[] ids);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MNewHotelInfo GetModel(int id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页编号</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        IList<MNewHotelInfo> GetList(int pageSize, int pageIndex, ref int recordCount);

        #endregion

        #region 前台方法

        /// <summary>
        ///  获取首页显示的酒店信息
        /// </summary>
        /// <param name="cityAreaType">城市区域类型</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="topNumber">个数</param>
        /// <returns>IList</returns>
        IList<MNewHotelInfo> GetList(CityAreaType cityAreaType, string cityName, int topNumber);

        #endregion
    }
}
