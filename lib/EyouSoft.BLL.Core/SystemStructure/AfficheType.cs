//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using EyouSoft.Component.Factory;
//using EyouSoft.IBLL.SystemStructure;
//namespace EyouSoft.BLL.SystemStructure
//{
//    /// <summary>
//    /// 创建人：鲁功源  2010-06-30
//    /// 描述：新闻类别业务逻辑层
//    /// </summary>
//    public class AfficheType:IAfficheType
//    {
//        private readonly IDAL.SystemStructure.IAfficheType dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.IAfficheType>();

//        #region CreateInstance
//        /// <summary>
//        /// 创建新闻类别业务逻辑接口的实例
//        /// </summary>
//        /// <returns></returns>
//        public static EyouSoft.IBLL.SystemStructure.IAfficheType CreateInstance()
//        {
//            EyouSoft.IBLL.SystemStructure.IAfficheType op1 = null;
//            if (op1 == null)
//            {
//                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.IAfficheType>();
//            }
//            return op1;
//        }
//        #endregion

//        /// <summary>
//        /// 验证是否存在同名称的记录
//        /// </summary>
//        /// <param name="ClassName">类别名称</param>
//        /// <param name="ID">主键编号</param>
//        /// <returns>true:存在 false:不存在</returns>
//        public bool Exists(string ClassName, int ID)
//        {
//            return dal.Exists(ClassName, ID);
//        }
//        /// <summary>
//        ///新增类别
//        /// </summary>
//        /// <param name="model">新闻类别实体</param>
//        /// <returns>true:成功 false:失败</returns>
//        public bool Add(EyouSoft.Model.SystemStructure.AfficheType model)
//        {
//            if (model == null)
//                return false;
//            return dal.Add(model);
//        }
//        /// <summary>
//        /// 修改类别
//        /// </summary>
//        /// <param name="model">新闻类别实体</param>
//        /// <returns>true:成功 false:失败</returns>
//        public bool Update(EyouSoft.Model.SystemStructure.AfficheType model)
//        {
//            if (model == null)
//                return false;
//            return dal.Update(model);
//        }
//        /// <summary>
//        /// 分页获取所有类别列表
//        /// </summary>
//        /// <param name="pageSize">每页显示条数</param>
//        /// <param name="pageIndex">当前页码</param>
//        /// <param name="recordCount">总记录数</param>
//        /// <returns>新闻类别列表</returns>
//        public IList<EyouSoft.Model.SystemStructure.AfficheType> GetList(int pageSize, int pageIndex, ref int recordCount)
//        {
//            return dal.GetList(pageSize, pageIndex, ref recordCount);
//        }
//        /// <summary>
//        /// 获取所有新闻类别列表
//        /// </summary>
//        /// <returns></returns>
//        public IList<EyouSoft.Model.SystemStructure.AfficheType> GetAllList()
//        {
//            return dal.GetAllList();
//        }
//    }
//}
