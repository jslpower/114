using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.NewsStructure
{
    /// <summary>
    /// 资讯类别业务层
    /// </summary>
    /// 鲁功源 2011-04-01
    public class NewsType:IBLL.NewsStructure.INewsType
    {
        private readonly IDAL.NewsStructure.INewsType dal = ComponentFactory.CreateDAL<IDAL.NewsStructure.INewsType>();

        /// <summary>
        /// 构造资讯类别接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewsStructure.INewsType CreateInstance()
        {
            IBLL.NewsStructure.INewsType op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.NewsStructure.INewsType>();
            }
            return op;
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsType() { }
        #endregion

        #region INewsType 成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.NewsStructure.NewsType model)
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
        public bool Update(EyouSoft.Model.NewsStructure.NewsType model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
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
        /// 验证行业资讯类别是否存在同名
        /// </summary>
        /// <param name="NewsTypeName">行业资讯类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true：存在 false:不存在</returns>
        public bool InformationExists(string NewsTypeName, int Id)
        {
            if (string.IsNullOrEmpty(NewsTypeName))
                return false;
            return dal.IsExists(NewsTypeName, Id, EyouSoft.Model.NewsStructure.NewsCategory.行业资讯);
        }
        /// <summary>
        /// 获取指定资讯类别的大类别
        /// </summary>
        /// <param name="Id">资讯类别编号</param>
        /// <returns></returns>
        public EyouSoft.Model.NewsStructure.NewsCategory? GetCategoryById(int Id)
        {
            if (Id <= 0)
                return null;
            return dal.GetCategoryById(Id);
        }
        /// <summary>
        /// 验证同业学堂类别是否存在同名
        /// </summary>
        /// <param name="NewsTypeName">同业学堂类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true：存在 false:不存在</returns>
        public bool SchoolExists(string NewsTypeName, int Id)
        {
            if (string.IsNullOrEmpty(NewsTypeName))
                return false;
            return dal.IsExists(NewsTypeName, Id, EyouSoft.Model.NewsStructure.NewsCategory.同业学堂);
        }
        /// <summary>
        /// 验证资讯所有类别是否存在同名
        /// </summary>
        /// <param name="NewsTypeName">资讯类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <returns>true：存在 false:不存在</returns>
        public bool IsExists(string NewsTypeName, int Id)
        {
            if (string.IsNullOrEmpty(NewsTypeName))
                return false;
            return dal.IsExists(NewsTypeName, Id, null);
        }
        /// <summary>
        /// 获取行业资讯下的所有子类别列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsType> GetInformationType()
        {
            return dal.GetList(EyouSoft.Model.NewsStructure.NewsCategory.行业资讯);
        }
        /// <summary>
        /// 获取同业学堂下的所有子类别列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsType> GetSchoolType()
        {
            return dal.GetList(EyouSoft.Model.NewsStructure.NewsCategory.同业学堂);
        }
        /// <summary>
        /// 获取所有的类别列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsType> GetAllType()
        {
            return dal.GetList(null);
        }
        /// <summary>
        /// 根据编号获取资讯类别名称
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public string GetNewsTypeName(int Id)
        {
            if (Id <= 0)
                return string.Empty;
            return dal.GetNewsTypeName(Id);
        }
        #endregion
    }


}
