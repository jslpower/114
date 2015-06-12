using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.NewsStructure
{
    /// <summary>
    /// 标签与关键字数据层接口
    /// </summary>
    public interface ITagKeyInfo
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.NewsStructure.TagKeyInfo model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.NewsStructure.TagKeyInfo model);
        /// <summary>
        /// 是否存在同名项目
        /// </summary>
        /// <param name="ItemName">项目名称</param>
        /// <param name="Id">主键编号</param>
        /// <param name="Category">类别</param>
        /// <returns>true:存在 false:不存在</returns>
        bool IsExists(string ItemName, int Id, EyouSoft.Model.NewsStructure.ItemCategory Category);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(params int[] Ids);
        /// <summary>
        /// 根据编号获取关键字/tag对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="cate">类别</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.TagKeyInfo GetModel(int id, EyouSoft.Model.NewsStructure.ItemCategory cate);
        /// <summary>
        /// <summary>
        /// 分页获取标签与关键字列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ItemName">标签或者关键字名称</param>
        /// <param name="ItemUrl">链接</param>
        /// <param name="Category">类别 =null返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string ItemName, string ItemUrl,EyouSoft.Model.NewsStructure.ItemCategory? Category);
    }
}
