using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-地理位置（地标）BLL
    /// 创建人：luofx 2010-12-2
    /// </summary>
    public class HotelLandMarks : EyouSoft.IBLL.HotelStructure.IHotelLandMarks
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelLandMarks dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelLandMarks>();
        #region CreateInstance
        /// <summary>
        /// 创建地标逻辑接口实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelLandMarks CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelLandMarks op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelLandMarks>();
            }
            return op;
        }
        #endregion
        /// <summary>
        /// 获取所有的城市区域信息集合
        /// </summary>
        /// <param name="CityCode">城市三字码（空或null时，获取所有城市信息；不为空时，根据三字码获取相应城市的地理位置信息集合）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelLandMarks> GetList(string CityCode)
        {
            return dal.GetList(CityCode);
        }
        /// <summary>
        /// 获取城市的地理信息集合
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelLandMarks> GetList(int cityId)
        {
            return null;
        }

        /// <summary>
        /// 获取酒店地理位置信息实体
        /// </summary>
        /// <param name="Id">地理位置主键Id</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.HotelLandMarks GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
    }
}
