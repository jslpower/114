using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.IBLL.ScenicStructure
{
    /// <summary>
    /// 景区主题
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public interface IScenicTheme
    {
        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Add(MScenicTheme item);

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Update(MScenicTheme item);

        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        bool Delete(int themeId);
        /// <summary>
        /// 获取主题
        /// </summary>
        /// <returns></returns>
        IList<MScenicTheme> GetList();
    }
}
