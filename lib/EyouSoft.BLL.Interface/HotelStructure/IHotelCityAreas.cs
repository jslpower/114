﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-城市行政区域IBLL
    /// </summary>
    /// 创建人：luofx 2010-12-2
    public interface IHotelCityAreas
    {
        /// <summary>
        /// 获取城市行政区域信息集合
        /// </summary>
        /// <param name="CityCode">城市三字码（空或null时，获取所有城市信息；不为空时，根据三字码获取相应城市的区域信息集合）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.HotelStructure.HotelCityAreas> GetList(string CityCode);
        /// <summary>
        /// 获取酒店城市行政区域信息实体
        /// </summary>
        /// <param name="Id">城市行政区域主键Id</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.HotelCityAreas GetModel(int Id);
    }
}