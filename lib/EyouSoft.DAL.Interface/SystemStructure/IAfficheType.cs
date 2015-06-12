//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace EyouSoft.IDAL.SystemStructure
//{
//    /// <summary>
//    /// 创建人：鲁功源  2010-06-30
//    /// 描述：新闻中心-新闻类别数据层接口
//    /// </summary>
//    public interface IAfficheType
//    {
//        /// <summary>
//        /// 验证是否存在同名称的记录
//        /// </summary>
//        /// <param name="ClassName">类别名称</param>
//        /// <param name="ID">主键编号</param>
//        /// <returns>true：存在 false:不存在</returns>
//        bool Exists(string ClassName, int ID);
//        /// <summary>
//        ///新增类别
//        /// </summary>
//        /// <param name="model">新闻类别实体</param>
//        /// <returns>true:成功 false:失败</returns>
//        bool Add(EyouSoft.Model.SystemStructure.AfficheType model);
//        /// <summary>
//        /// 修改类别
//        /// </summary>
//        /// <param name="model">新闻类别实体</param>
//        /// <returns>true:成功 false:失败</returns>
//        bool Update(EyouSoft.Model.SystemStructure.AfficheType model);
//        /// <summary>
//        /// 分页获取所有类别列表
//        /// </summary>
//        /// <param name="pageSize">每页显示条数</param>
//        /// <param name="pageIndex">当前页码</param>
//        /// <param name="recordCount">总记录数</param>
//        /// <returns>新闻类别列表</returns>
//        IList<EyouSoft.Model.SystemStructure.AfficheType> GetList(int pageSize, int pageIndex, ref int recordCount);
//        /// <summary>
//        /// 获取所有新闻类别列表
//        /// </summary>
//        /// <returns></returns>
//        IList<EyouSoft.Model.SystemStructure.AfficheType> GetAllList();
//    }
//}
