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
    /// 描述：轮换广告业务逻辑层
    /// </summary>
    public class HighShopAdv : IHighShopAdv
    {
        private readonly IDAL.ShopStructure.IHighShopAdv dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopAdv>();

        #region CreateInstance
        /// <summary>
        /// 创建轮换广告业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.ShopStructure.IHighShopAdv CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopAdv op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopAdv>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 修改网店轮换广告
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="list">轮换广告列表集合</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(string CompanyID, IList<EyouSoft.Model.ShopStructure.HighShopAdv> list)
        {
            return dal.Update(CompanyID, list);
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool DeletePic(string ID)
        {
            return dal.DeletePic(ID);
        }
        /// <summary>
        /// 获取后台轮换广告列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部，否则返回指定公司的轮换广告集合</param>
        /// <returns>轮换广告列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopAdv> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, CompanyID);
        }
        /// <summary>
        /// 前台获取指定公司的轮换广告列表
        /// </summary>
        /// <param name="TopNumber">需要获取的记录数</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>轮换广告列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopAdv> GetWebList(int TopNumber,string CompanyID)
        {
            return dal.GetWebList(TopNumber,CompanyID);
        }
        #endregion
    }
}
