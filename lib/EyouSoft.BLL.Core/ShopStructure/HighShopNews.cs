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
    /// 描述：最新旅游动态业务逻辑层
    /// </summary>
    public class HighShopNews : IHighShopNews
    {
        private readonly IDAL.ShopStructure.IHighShopNews dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopNews>();

        #region CreateInstance
        /// <summary>
        /// 创建最新旅游动业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static IHighShopNews CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopNews op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopNews>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">旅游动态实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.ShopStructure.HighShopNews model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改旅游动态
        /// </summary>
        /// <param name="model">旅游动态实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.ShopStructure.HighShopNews model)
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
        /// 获取旅游动态实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>存在返回实体类 不存在返回NUll</returns>
        public EyouSoft.Model.ShopStructure.HighShopNews GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">置顶状态 TRUE或者FALSE</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetTop(string ID, bool IsTop)
        {
            return dal.SetTop(ID, IsTop);
        }
        /// <summary>
        /// 获取前台页面旅游动态列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="KeyWord">关键字</param>
        /// <returns>旅游动态列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopNews> GetWebList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord)
        {
            return dal.GetWebList(pageSize, pageIndex, ref recordCount, CompanyID, KeyWord);
        }
        /// <summary>
        /// 获取指定公司指定条数的记录
        /// </summary>
        /// <param name="TopNumber">需要返回的记录条数 =false返回全部 >false返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>旅游动态列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopNews> GetTopNumberList(int TopNumber, string CompanyID)
        {
            return dal.GetTopNumberList(TopNumber, CompanyID);
        }
        #endregion
    }
}
