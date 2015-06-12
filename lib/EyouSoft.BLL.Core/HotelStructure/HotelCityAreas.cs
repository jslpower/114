using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-城市行政区域BLL
    /// </summary>
    /// 创建人：luofx 2010-12-2
    public class HotelCityAreas : EyouSoft.IBLL.HotelStructure.IHotelCityAreas
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelCityAreas dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelCityAreas>();
        #region CreateInstance
        /// <summary>
        /// 创建城市区域逻辑接口实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelCityAreas CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelCityAreas op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelCityAreas>();
            }
            return op;
        }
        #endregion
        /// <summary>
        /// 获取城市行政区域信息集合
        /// </summary>
        /// <param name="CityCode">城市三字码（空或null时，获取所有城市信息；不为空时，根据三字码获取相应城市的区域信息集合）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelCityAreas> GetList(string CityCode)
        {
            return dal.GetList(CityCode);
        }
        /// <summary>
        /// 获取酒店城市行政区域信息实体
        /// </summary>
        /// <param name="Id">城市区域主键Id</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.HotelCityAreas GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
    }
}
