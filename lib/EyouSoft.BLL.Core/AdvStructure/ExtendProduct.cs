using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.AdvStructure
{
    /// <summary>
    /// 描述:首页推荐产品业务类
    /// 修改记录:
    /// 1 2011-05-10 曹胡生 创建
    /// </summary>
    public class ExtendProduct:EyouSoft.IBLL.AdvStructure.IExtendProduct
    {
        private readonly EyouSoft.IDAL.AdvStructure.IExtendProduct dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.AdvStructure.IExtendProduct>();
        /// <summary>
        /// 创建首页推荐产品接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.AdvStructure.IExtendProduct CreateInstance()
        {
            EyouSoft.IBLL.AdvStructure.IExtendProduct op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.AdvStructure.IExtendProduct>();
            }
            return op;
        }

        /// <summary>
        /// 得到所有首页推荐产品
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> GetExtendProductList(int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetExtendProductList(pageSize,pageIndex,ref recordCount);
        }

        /// <summary>
        /// 得到某城市下的所有推荐产品
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> GetExtendProductList(int CityId)
        {
            return dal.GetExtendProductList(CityId);
        }

        /// <summary>
        /// 修改推荐产品
        /// </summary>
        /// <param name="ExtendProductInfo"></param>
        /// <returns></returns>
        public bool UpdateExtendProduct(EyouSoft.Model.AdvStructure.ExtendProductInfo ExtendProductInfo)
        {
            return dal.UpdateExtendProduct(ExtendProductInfo);
        }

        /// <summary>
        /// 批量修改推荐产品
        /// </summary>
        /// <param name="ExtendProductInfoList"></param>
        /// <returns></returns>
        public bool UpdateExtendProduct(IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> ExtendProductInfoList)
        {
            foreach (var item in ExtendProductInfoList)
            {
                if (item.ID != 0)
                {
                    dal.UpdateExtendProduct(item);
                }
            }
            return true;
        }

        /// <summary>
        /// 添加推荐产品
        /// </summary>
        /// <param name="ExtendProductInfo"></param>
        /// <returns></returns>
        public bool AddExtendProduct(EyouSoft.Model.AdvStructure.ExtendProductInfo ExtendProductInfo)
        {
            if (!string.IsNullOrEmpty(ExtendProductInfo.ProductID) && ExtendProductInfo.ShowCityId != 0 && !IsZixst(ExtendProductInfo.ShowCityId, ExtendProductInfo.ProductID))
            {
                return dal.AddExtendProduct(ExtendProductInfo);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量添加推荐产品
        /// </summary>
        /// <param name="ExtendProductInfoList"></param>
        /// <returns></returns>
        public bool AddExtendProduct(IList<EyouSoft.Model.AdvStructure.ExtendProductInfo> ExtendProductInfoList) 
        {
            foreach (var item in ExtendProductInfoList)
            {
                if (!string.IsNullOrEmpty(item.ProductID) && item.ShowCityId!=0)
                {
                    if (!IsZixst(item.ShowCityId, item.ProductID))
                    {
                        dal.AddExtendProduct(item);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除推荐产品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DelExtendProduct(int ID) 
        {
            return dal.DelExtendProduct(ID);
        }

        /// <summary>
        /// 判断某个产品是否推荐
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="ProdcutID"></param>
        /// <returns></returns>
        public bool IsZixst(int CityId, string ProdcutID) 
        {
            return dal.IsZixst(CityId,ProdcutID);
        }
    }
}
