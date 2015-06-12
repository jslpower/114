using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 城市地标
    /// 创建者：郑付杰
    /// 创建时间：2011-11-28
    /// </summary>
    public interface ISystemLandMark
    {
        /// <summary>
        /// 获取所有地标
        /// </summary>
        /// <returns></returns>
        IList<MSystemLandMark> GetList();

        /// <summary>
        /// 获取所有地标
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        IList<MSystemLandMark> GetList(int cityId);
    }
}
