using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-酒店城市信息IBLL
    /// </summary>
    /// 创建人：luofx 2010-12-2
    public interface IHotelCity
    {
        /// <summary>
        /// 获取所有的城市信息集合
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.HotelStructure.HotelCity> GetList();
        /// <summary>
        /// 获取酒店城市信息实体
        /// </summary>
        /// <param name="CityId">城市主键Id</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.HotelCity GetModel(int Id);
        /// <summary>
        /// 获取酒店城市信息实体
        /// </summary>
        /// <param name="CityCode">城市三字码</param>
        /// <returns></returns>
        EyouSoft.Model.HotelStructure.HotelCity GetModel(string CityCode);
    }
}
