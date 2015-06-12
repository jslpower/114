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
    /// 描述：友情链接业务逻辑层
    /// </summary>
    public class HighShopFriendLink : IHighShopFriendLink
    {
        private readonly IDAL.ShopStructure.IHighShopFriendLink dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopFriendLink>();

        #region CreateInstance
        /// <summary>
        /// 创建友情链接业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static IHighShopFriendLink CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopFriendLink op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopFriendLink>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.ShopStructure.HighShopFriendLink model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.ShopStructure.HighShopFriendLink model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(string ID)
        {
            return dal.Delete(ID);
        }
        /// <summary>
        /// 获取指定公司的友情链接列表
        /// </summary>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的记录</param>
        /// <returns>友情链接列表</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> GetList(string CompanyID)
        {
            return dal.GetList(CompanyID);
        }
        #endregion
    }
}
