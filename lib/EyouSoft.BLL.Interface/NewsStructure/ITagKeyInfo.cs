using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.NewsStructure
{
    /// <summary>
    /// 标签与关键字业务层接口
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
        ///  是否存在同名的Tag标签
        /// </summary>
        /// <param name="TagName">标签名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true:存在 false:不存在</returns>
        bool TagExists(string TagName, int Id);
        /// <summary>
        /// 是否存在同名的关键字
        /// </summary>
        /// <param name="KeyWordName">关键字名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true:存在 false:不存在</returns>
        bool KeyWordExists(string KeyWordName, int Id);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.NewsStructure.TagKeyInfo model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(params int[] Ids);
        /// <summary>
        /// 分页获取标签列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ItemName">标签名称</param>
        /// <param name="ItemUrl">链接</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetTagList(int pageSize, int pageIndex, ref int recordCount, string TagName);
        /// <summary>
        /// 获取所有的标签
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllTag();
       
        /// <summary>
        /// 分页获取关键字列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ItemName">关键字名称</param>
        /// <param name="ItemUrl">链接</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetKeyWordList(int pageSize, int pageIndex, ref int recordCount, string KeyName, string KeyUrl);
        /// <summary>
        /// 获取所有关键字列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllKeyWord();

        /// <summary>
        /// 模糊查询所有关键字列表
        /// </summary>
        /// <param name="KeyWordName">关键字名称</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllKeyWord(string KeyWordName);

        /// <summary>
        /// 模糊查询所有的标签
        /// </summary>
        /// <param name="TagName">标签名称</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllTag(string TagName);
        /// <summary>
        /// 根据编号获取关键字/tag对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="cate">类别</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.TagKeyInfo GetModel(int id, EyouSoft.Model.NewsStructure.ItemCategory cate);
    }
}
