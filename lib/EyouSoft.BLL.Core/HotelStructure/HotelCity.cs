using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-酒店城市信息BLL
    /// </summary>
    /// 创建人：luofx 2010-12-2
    public class HotelCity : EyouSoft.IBLL.HotelStructure.IHotelCity
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelCity dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelCity>();
        #region CreateInstance
        /// <summary>
        /// 创建酒店城市逻辑接口实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelCity CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelCity op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelCity>();
            }
            return op;
        }
        #endregion
        /// <summary>
        /// 获取所有的城市信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelCity> GetList()
        {
            return dal.GetList();
        }
        /// <summary>
        /// 获取酒店城市信息实体
        /// </summary>
        /// <param name="CityId">城市主键Id</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.HotelCity GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 获取酒店城市信息实体
        /// </summary>
        /// <param name="CityCode">城市三字码</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.HotelCity GetModel(string CityCode)
        {
            return dal.GetModel(CityCode);
        }
    }
}
