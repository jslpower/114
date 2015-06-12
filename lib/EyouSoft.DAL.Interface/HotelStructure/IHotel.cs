using System;
using System.Collections.Generic;

namespace EyouSoft.IDAL.HotelStructure
{
    /// <summary>
    /// 酒店数据访问类接口
    /// </summary>
    /// Author:汪奇志 2011-05-13
    public interface IHotel
    {
        /// <summary>
        /// 获取酒店信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HotelStructure.HotelInfo> GetHotels(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HotelStructure.MLocalHotelSearchInfo searchInfo);
    }
}
