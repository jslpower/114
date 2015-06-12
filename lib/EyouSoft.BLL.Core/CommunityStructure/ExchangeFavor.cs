using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Cache.Facade;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.CommunityStructure;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-16
    /// 描述：供求收藏业务层
    /// </summary>
    public class ExchangeFavor:IBLL.CommunityStructure.IExchangeFavor
    {
        private readonly IDAL.CommunityStructure.IExchangeFavor dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.IExchangeFavor>();

        #region CreateInstance
        /// <summary>
        /// 创建IBLL实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.IExchangeFavor CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IExchangeFavor op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IExchangeFavor>();
            }
            return op1;
        }
        #endregion

        #region IExchangeFavor成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供求收藏实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.CommunityStructure.ExchangeBase model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号字符（，分割）</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string[] Ids)
        {
            if (Ids==null || Ids.Length==0)
                return false;
            string strIds = string.Empty;
            for (int i = 0; i < Ids.Length; i++)
            {
                strIds += Ids[i] + ",";
            }
            strIds = strIds.TrimEnd(',');
            return dal.Delete(strIds);
        }
        /// <summary>
        /// 分页获取所有供求收藏信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetList(int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, string.Empty, string.Empty);
        }
        /// <summary>
        /// 分页获取指定公司的所有供求收藏信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetListbyCompanyID(int pageSize, int pageIndex, ref int recordCount, string CompanyId)
        {
            return dal.GetList(pageSize,pageIndex,ref recordCount,CompanyId,string.Empty);
        }
        /// <summary>
        /// 分页获取指定用户的所有供求信息收藏列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="UserId">用户编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetListByUserID(int pageSize, int pageIndex, ref int recordCount, string UserId)
        {
            return dal.GetList(pageSize,pageIndex,ref recordCount,string.Empty,UserId);
        }
        #endregion

    }
}
