using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.NewsStructure
{
    /// <summary>
    /// Tag标签与KeyWord关键字业务层
    /// </summary>
    /// 鲁功源 2011-04-01
    public class TagKeyInfo:IBLL.NewsStructure.ITagKeyInfo
    {
        private readonly IDAL.NewsStructure.ITagKeyInfo dal = ComponentFactory.CreateDAL<IDAL.NewsStructure.ITagKeyInfo>();

        /// <summary>
        /// 构造资讯类别接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewsStructure.ITagKeyInfo CreateInstance()
        {
            IBLL.NewsStructure.ITagKeyInfo op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.NewsStructure.ITagKeyInfo>();
            }
            return op;
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TagKeyInfo() { }
        #endregion

        #region ITagKeyInfo 成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.NewsStructure.TagKeyInfo model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(EyouSoft.Model.NewsStructure.TagKeyInfo model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        ///  是否存在同名的Tag标签
        /// </summary>
        /// <param name="TagName">标签名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true:存在 false:不存在</returns>
        public bool TagExists(string TagName, int Id)
        {
            if (string.IsNullOrEmpty(TagName))
                return false;
            return dal.IsExists(TagName, Id, EyouSoft.Model.NewsStructure.ItemCategory.Tag);
        }
        /// <summary>
        /// 是否存在同名的关键字
        /// </summary>
        /// <param name="KeyWordName">关键字名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true:存在 false:不存在</returns>
        public bool KeyWordExists(string KeyWordName, int Id)
        {
            if (string.IsNullOrEmpty(KeyWordName))
                return false;
            return dal.IsExists(KeyWordName, Id, EyouSoft.Model.NewsStructure.ItemCategory.KeyWord);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(params int[] Ids)
        {
            if (Ids == null || Ids.Length == 0)
                return false;
            return dal.Delete(Ids);
        }
        /// <summary>
        /// 获取所有标签列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="TagName">标签名称</param>
        /// <returns>标签列表</returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetTagList(int pageSize, int pageIndex, ref int recordCount, string TagName)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, TagName, "", EyouSoft.Model.NewsStructure.ItemCategory.Tag);
        }
        /// <summary>
        /// 获取所有的标签
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllTag()
        { 
            int recordCount=0;
            return dal.GetList(int.MaxValue - 1, 1, ref recordCount, "", "", EyouSoft.Model.NewsStructure.ItemCategory.Tag);
        }
        /// <summary>
        /// 获取所有关键字列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="KeyName">关键字名称</param>
        /// <param name="KeyUrl">关键字链接</param>
        /// <returns>关键字列表</returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetKeyWordList(int pageSize, int pageIndex, ref int recordCount, string KeyName, string KeyUrl)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, KeyName, KeyUrl, EyouSoft.Model.NewsStructure.ItemCategory.KeyWord);
        }
        /// <summary>
        /// 获取所有关键字列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllKeyWord()
        { 
            int recordCount=0;
            return dal.GetList(int.MaxValue - 1, 1, ref recordCount,"",null, EyouSoft.Model.NewsStructure.ItemCategory.KeyWord);
        }
        /// <summary>
        /// 模糊查询所有关键字列表
        /// </summary>
        /// <param name="KeyWordName">关键字名称</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllKeyWord(string KeyWordName)
        {
            int recordCount = 0;
            return dal.GetList(int.MaxValue - 1, 1, ref recordCount, KeyWordName, null, EyouSoft.Model.NewsStructure.ItemCategory.KeyWord);
        }
        /// <summary>
        /// 模糊查询所有的标签
        /// </summary>
        /// <param name="TagName">标签名称</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetAllTag(string TagName)
        {
            int recordCount = 0;
            return dal.GetList(int.MaxValue - 1, 1, ref recordCount, TagName, null, EyouSoft.Model.NewsStructure.ItemCategory.Tag);
        }
        /// <summary>
        /// 根据编号获取关键字/tag对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="cate">类别</param>
        /// <returns></returns>
        public EyouSoft.Model.NewsStructure.TagKeyInfo GetModel(int id, EyouSoft.Model.NewsStructure.ItemCategory cate)
        {
            return dal.GetModel(id, cate);
        }
        #endregion
    }

}
