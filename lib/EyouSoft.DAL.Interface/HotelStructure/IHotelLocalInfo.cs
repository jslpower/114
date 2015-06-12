using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HotelStructure
{
    /// <summary>
    /// 酒店系统-首页酒店信息IDAL
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public interface IHotelLocalInfo
    {
        /// <summary>
        /// 根据类型获取酒店系统的首页酒店板块信息集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="CityCode">城市三字码(为空时，不做条件)</param>
        /// <param name="topNum">数据条数 0:获取所有</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(EyouSoft.Model.HotelStructure.HotelShowType type, string CityCode, int topNum);
        /// <summary>
        /// 根据类型获取所有的酒店信息集合(分页)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.HotelShowType type);
        /// <summary>
        /// 根据酒店模块类型获取城市信息集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HotelStructure.HotelCity> GetList(EyouSoft.Model.HotelStructure.HotelShowType type);
        /// <summary>
        ///  获取酒店系统-首页酒店板块信息实体
        /// </summary>
        /// <param name="Id">首页酒店板块信息主键Id</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.HotelLocalInfo GetModel(string Id);
        /// <summary>
        /// 添加酒店系统首页酒店板块信息
        /// </summary>
        /// <param name="model">首页酒店信息IList集合</param>
        /// <returns></returns>
        int Add(IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> HotelList);
        /// <summary>
        /// 删除首页酒店信息
        /// </summary>
        /// <param name="Ids">酒店信息实体主键编号Id</param>
        /// <returns></returns>
        int Delete(params string[] Ids);
    }
}
