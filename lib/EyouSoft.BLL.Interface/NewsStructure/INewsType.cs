using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.NewsStructure
{
    /// <summary>
    /// 资讯类别业务层接口
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
        /// 验证行业资讯类别是否存在同名
        /// </summary>
        /// <param name="NewsTypeName">行业资讯类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true：存在 false:不存在</returns>
        bool InformationExists(string NewsTypeName, int Id);
        /// <summary>
        /// 验证同业学堂类别是否存在同名
        /// </summary>
        /// <param name="NewsTypeName">同业学堂类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true：存在 false:不存在</returns>
        bool SchoolExists(string NewsTypeName, int Id);
        /// <summary>
        /// 验证资讯所有类别是否存在同名
        /// </summary>
        /// <param name="NewsTypeName">资讯类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true：存在 false:不存在</returns>
        bool IsExists(string NewsTypeName, int Id);
        /// <summary>
        /// 获取指定资讯类别的大类别
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.NewsCategory? GetCategoryById(int Id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        bool Delete(params int[] Ids);
        /// <summary>
        /// 获取行业资讯下的所有子类别列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.NewsType> GetInformationType();
        /// <summary>
        /// 获取同业学堂下的所有子类别列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.NewsType> GetSchoolType();
        /// <summary>
        /// 获取所有的类别列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.NewsType> GetAllType();
        /// <summary>
        /// 根据编号获取资讯类别名称
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        string GetNewsTypeName(int Id);
    }
}
