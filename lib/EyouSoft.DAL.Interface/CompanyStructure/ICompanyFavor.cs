using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-01
    /// 描述：单位采购目录数据层接口
    /// </summary>
    public interface ICompanyFavor
    {
        #region ICompanyFavor成员
        /// <summary>
        /// 获取公司采购目录被收藏公司的区域编号集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.AreaBase> GetAllFavorArea(string CompanyId);
        /// <summary>
        /// 获取指定公司指定线路区域下的采购目录总数
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AreaId">线路区域编号 =0时返回全部</param>
        /// <returns></returns>
        int GetAllFavorCount(string CompanyId, int AreaId);
        /// <summary>
        /// 获取指定公司指定线路区域下的被收藏公司的编号集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AreaId">线路区域编号 =0时返回全部</param>
        /// <returns></returns>
        IList<string> GetListByCompanyId(string CompanyId, int AreaId);
        /// <summary>
        /// 保存采购目录
        /// </summary>
        /// <param name="list">采购目录列表集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool SaveCompanyFavor(EyouSoft.Model.CompanyStructure.CompanyFavor model);
        /// <summary>
        /// 删除指定公司的指定区域的采购目录
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="FavorCompanyId">被收藏公司编号</param>
       /// <returns>true:成功 false:失败</returns>
        bool Delete(string CompanyID, string FavorCompanyId);
        #endregion
    }
}
