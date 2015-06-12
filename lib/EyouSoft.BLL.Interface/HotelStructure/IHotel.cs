using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.HotelStructure
{
    /// <summary>
    /// 酒店业务逻辑接口
    /// </summary>
    /// 周文超 2010-12-03
    public interface IHotel
    {
        /// <summary>
        /// 获取酒店集合
        /// </summary>
        /// <param name="QueryModel">酒店查询实体</param>
        /// <param name="RespPageInfo">酒店查询返回的分页信息</param>
        /// <param name="ErrModel">请求错误信息</param>
        /// <returns>酒店实体集合</returns>
        IList<EyouSoft.Model.HotelStructure.HotelInfo> GetHotelList(HotelBI.MultipleSeach QueryModel, ref EyouSoft.Model.HotelStructure.RespPageInfo RespPageInfo, out HotelBI.ErrorInfo ErrModel);

        /// <summary>
        /// 获取某酒店信息
        /// </summary>
        /// <param name="QueryModel">单酒店查询实体</param>
        /// <param name="ErrModel">请求错误信息</param>
        /// <returns>酒店信息实体</returns>
        EyouSoft.Model.HotelStructure.HotelInfo GetHotelModel(HotelBI.SingleSeach QueryModel, out HotelBI.ErrorInfo ErrModel);
    }
}
