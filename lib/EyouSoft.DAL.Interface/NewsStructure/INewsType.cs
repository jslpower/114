using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.NewsStructure
{
    /// <summary>
    /// 资讯类别数据层接口
    /// </summary>
    /// 鲁功源 2011-04-01
    public interface INewsType
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.NewsStructure.NewsType model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.NewsStructure.NewsType model);
        /// <summary>
        /// 验证是否存在同名新闻类别
        /// </summary>
        /// <param name="NewsTypeName">类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <param name="Category">资讯类别 =null检索全部</param>
        /// <returns>true:存在 false：不存在</returns>
        bool IsExists(string NewsTypeName, int Id, EyouSoft.Model.NewsStructure.NewsCategory? Category);
        /// <summary>
        /// 获取指定资讯类别的大类别
        /// </summary>
        /// <param name="Id">资讯类别编号</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.NewsCategory? GetCategoryById(int Id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        bool Delete(params int[] Ids);
        /// <summary>
        /// 获取类别列表
        /// </summary>
        /// <param name="Category">所属大类别 =null返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.NewsType> GetList(EyouSoft.Model.NewsStructure.NewsCategory? Category);
        /// <summary>
        /// 根据编号获取资讯类别名称
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        string GetNewsTypeName(int Id);
    }
}
