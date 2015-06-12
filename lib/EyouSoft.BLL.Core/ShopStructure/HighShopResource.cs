using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.ShopStructure;

namespace EyouSoft.BLL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-03
    /// 描述：旅游资源推荐业务逻辑层
    /// </summary>
    public class HighShopResource : IHighShopResource
    {
        private readonly IDAL.ShopStructure.IHighShopResource dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopResource>();

        #region CreateInstance
        /// <summary>
        /// 创建旅游资源推荐业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static IHighShopResource CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopResource op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopResource>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">旅游资源推荐实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.ShopStructure.HighShopResource model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">旅游资源推荐实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.ShopStructure.HighShopResource model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.Delete(ID);
        }
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">置顶状态</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetTop(string ID, bool IsTop)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetTop(ID, IsTop);
        }
        /// <summary>
        /// 获取旅游资源推荐实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public EyouSoft.Model.ShopStructure.HighShopResource GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 分页获取旅游资源推荐列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的数据</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>旅游资源推荐列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopResource> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, CompanyID, KeyWord);
        }
        /// <summary>
        /// 获取首页旅游资源推荐列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数 =false返回全部 >false返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>旅游资源推荐列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopResource> GetIndexList(int topNumber, string CompanyID)
        {
            return dal.GetIndexList(topNumber, CompanyID);
        }
        #endregion
    }
}
