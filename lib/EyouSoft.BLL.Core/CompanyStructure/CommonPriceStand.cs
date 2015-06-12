using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-08
    /// 描述：公用价格等级业务层
    /// </summary>
    public class CommonPriceStand:IBLL.CompanyStructure.ICommonPriceStand
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICommonPriceStand idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICommonPriceStand>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICommonPriceStand CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICommonPriceStand op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICommonPriceStand>();
            }
            return op;
        }

        #region ICommonPriceStand成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="PriceStandName">价格等级名称</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(string PriceStandName)
        {
            if (string.IsNullOrEmpty(PriceStandName))
                return false;
            return idal.Add(PriceStandName, EyouSoft.Model.CompanyStructure.CommPriceTypeID.其他);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return idal.Delete(ID, EyouSoft.Model.CompanyStructure.CommPriceTypeID.其他);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="PriceStandName">报价等级名称</param>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(string PriceStandName, string ID)
        {
            if (string.IsNullOrEmpty(PriceStandName) || string.IsNullOrEmpty(ID))
                return false;
            return idal.Update(PriceStandName, ID,EyouSoft.Model.CompanyStructure.CommPriceTypeID.其他);
        }
        /// <summary>
        /// 获取自定义公用价格等级列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> GetList()
        {
            return idal.GetList(EyouSoft.Model.CompanyStructure.CommPriceTypeID.其他);
        }
        /// <summary>
        /// 获取所有公用价格等级列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> GetSysList()
        {
            return idal.GetList(null);
        }
        #endregion
    }
}
