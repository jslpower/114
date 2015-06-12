using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-01
    /// 描述：单位采购目录业务逻辑层
    /// </summary>
    public class CompanyFavor : IBLL.CompanyStructure.ICompanyFavor
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyFavor idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyFavor>();
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyInfo idalcompany = ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyFavor CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyFavor op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyFavor>();
            }
            return op;
        }

        #region ICompanyFavor成员
        /// <summary>
        /// 获取公司采购目录被收藏公司的区域编号集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.AreaBase> GetAllFavorArea(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return null;
            return idal.GetAllFavorArea(CompanyId);
        }
        /// <summary>
        /// 获取公司采购目录总数
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public int GetAllFavorCount(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return 0;
            return idal.GetAllFavorCount(CompanyId, 0);
        }
        /// <summary>
        /// 获取指定公司指定线路区域下的采购目录总数
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AreaId">线路区域编号 =0时返回全部</param>
        /// <returns></returns>
        public int GetAllFavorCount(string CompanyId, int AreaId)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return 0;
            return idal.GetAllFavorCount(CompanyId, AreaId);
        }
        /// <summary>
        /// 获取指定公司指定线路区域下的被收藏公司的编号集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<string> GetListByCompanyId(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return null;
            return idal.GetListByCompanyId(CompanyId, 0);
        }
        /// <summary>
        /// 保存采购目录
        /// </summary>
        /// <param name="model">采购目录列表集合</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SaveCompanyFavor(EyouSoft.Model.CompanyStructure.CompanyFavor model)
        {
            if (model == null)
                return false;
            return idal.SaveCompanyFavor(model);
        }
        /// <summary>
        /// 获取当前登录人所在公司收藏的公司信息
        /// </summary>
        /// <param name="currentCompanyId">公司编号</param>
        /// <returns></returns>
        /// 开发人:张志瑜   开发时间:2010-7-12
        public IList<EyouSoft.Model.CompanyStructure.CompanyInfo> GetListCompany(string currentCompanyId)
        {
            var clist = GetListByCompanyId(currentCompanyId);
            if (clist != null && clist.Any())
                return idalcompany.GetList(clist);

            return null;
        }
        /// <summary>
        /// 删除指定公司的指定区域的采购目录
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="FavorCompanyId">被收藏公司编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string CompanyID, string FavorCompanyId)
        {
            if (string.IsNullOrEmpty(CompanyID) || string.IsNullOrEmpty(FavorCompanyId))
                return false;
            return idal.Delete(CompanyID, FavorCompanyId);
        }
        #endregion
    }
}
