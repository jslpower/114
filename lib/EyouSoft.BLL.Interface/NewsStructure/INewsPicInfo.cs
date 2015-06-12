using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.NewsStructure
{
    /// <summary>
    /// 资讯焦点图片业务层接口
    /// </summary>
    /// 鲁功源 2011-04-01
    public interface INewsPicInfo
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">焦点图片实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.NewsStructure.NewsPicInfo model);
        /// <summary>
        /// 获取焦点图片实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.NewsPicInfo GetModel();
    }
}
