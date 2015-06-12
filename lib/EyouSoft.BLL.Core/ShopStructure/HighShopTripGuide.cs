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
    /// 描述：出游指南业务逻辑层
    /// </summary>
    public class HighShopTripGuide : IHighShopTripGuide
    {
        private readonly IDAL.ShopStructure.IHighShopTripGuide dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopTripGuide>();

        #region CreateInstance
        /// <summary>
        /// 创建出游指南业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static IHighShopTripGuide CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopTripGuide op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopTripGuide>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">出游指南实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.ShopStructure.HighShopTripGuide model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">出游指南实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.ShopStructure.HighShopTripGuide model)
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
        /// 获取出游指南实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>存在返回出游指南实体,不存在返回NULL</returns>
        public EyouSoft.Model.ShopStructure.HighShopTripGuide GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 获取后台出游指南列表集合
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的所有记录</param>
        /// <param name="KeyWord">需要匹配的关键字</param> 
        /// <param name="typeList">类别ID,若不包含ID,则返回全部</param>
        /// <returns>出游指南列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord, params EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] typeList)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, CompanyID, KeyWord, typeList);
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">置顶状态 TRUE或FALSE</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetTop(string ID, bool IsTop)
        {
            return dal.SetTop(ID, IsTop);
        }
        /// <summary>
        /// 获取前台指定条数的出游指南列表
        /// </summary>
        /// <param name="TopNumber">需要返回的条数 =false返回全部 >false返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="TypeID">类别编号 =false返回所有类别 >false返回指定类别的数据</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> GetWebList(int TopNumber, string CompanyID, int TypeID,string KeyWord)
        {
            return dal.GetWebList(TopNumber, CompanyID, TypeID,KeyWord);
        }
        #endregion
    }
}
