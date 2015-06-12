using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店系统-首页酒店信息BLL
    /// </summary>
    /// 创建人：luofx 2010-12-2
    public class HotelLocalInfo : EyouSoft.IBLL.HotelStructure.IHotelLocalInfo
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelLocalInfo dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelLocalInfo>();
        #region CreateInstance
        /// <summary>
        /// 创建首页酒店模块逻辑接口实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelLocalInfo CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelLocalInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelLocalInfo>();
            }
            return op;
        }
        #endregion
        /// <summary>
        /// 根据类型获取酒店系统的首页酒店板块信息集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="CityCode">城市三字码(为空时，不做条件)</param>
        /// <param name="topNum">数据条数 0:获取所有</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(EyouSoft.Model.HotelStructure.HotelShowType type, string CityCode, int topNum)
        {
            return dal.GetList(type,CityCode, topNum);
        }
        /// <summary>
        /// 获取特推酒店的城市信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelCity> GetRecommendedCityList()
        {
            return dal.GetList(EyouSoft.Model.HotelStructure.HotelShowType.特推酒店);
        }
        /// <summary>
        /// 根据类型获取所有的酒店信息集合(分页)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.HotelShowType type)
        {
            return dal.GetList(PageSize, PageIndex, ref RecordCount, type);
        }
        /// <summary>
        /// 获取酒店系统-首页酒店板块信息实体
        /// </summary>
        /// <param name="Id">首页酒店板块信息主键Id</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.HotelLocalInfo GetModel(string Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 添加酒店系统首页酒店板块信息
        /// </summary>
        /// <param name="HotelList">酒店信息集合</param>
        /// <returns></returns>
        public int Add(IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> HotelList)
        {
            //IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> HotelList = new List<EyouSoft.Model.HotelStructure.HotelLocalInfo>();          
            return dal.Add(HotelList);
        }
        /// <summary>
        /// 删除首页酒店信息
        /// </summary>
        /// <param name="Ids">酒店信息实体主键编号Id</param>
        /// <returns></returns>
        public int Delete(params string[] Ids)
        {
            return dal.Delete(Ids);
        }
    }
}
