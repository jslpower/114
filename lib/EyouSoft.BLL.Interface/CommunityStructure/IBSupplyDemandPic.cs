using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CommunityStructure;

namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 二次整改供求图片逻辑层接口类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public interface IBSupplyDemandPic
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供求焦点图片</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(List<MSupplyDemandPic> list);

        /// <summary>
        /// 获取供求焦点图片实体集合
        /// </summary>
        /// <returns>焦点图片集合</returns>
        IList<MSupplyDemandPic> GetList();
    }
}
