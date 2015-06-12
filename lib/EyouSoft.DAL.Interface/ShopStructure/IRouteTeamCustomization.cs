using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ShopStructure
{
    /// <summary>
    /// 创建人：luofx 2010-11-09
    /// 描述：高级网店-团队制定IDAL
    /// </summary>
    public interface IRouteTeamCustomization
    {
        /// <summary>
        /// 新增团队制定
        /// </summary>
        /// <param name="model">高级网店团队制定实体</param>
        /// <returns>true:成功；false：失败</returns>
        bool Add(EyouSoft.Model.ShopStructure.RouteTeamCustomization model);
        /// <summary>
        /// 团队制定信息删除
        /// </summary>
        /// <param name="Id">团队制定编号（主键）</param>
        /// <param name="CompanyId">所属公司编号</param>
        /// <returns>true:成功；false：失败</returns>
        bool Delete(int Id,string CompanyId);
        /// <summary>
        /// 获取团队制定实体
        /// </summary>
        /// <param name="Id">团队制定的编号（主键）</param>
        /// <returns></returns>
        EyouSoft.Model.ShopStructure.RouteTeamCustomization GetModel(int Id);
        /// <summary>
        /// 获取团队制定信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> GetList(string CompanyId);
        /// <summary>
        /// 获取团队制定信息(分页)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>团队制定信息集合</returns>
        IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> GetList(int PageSize, int PageIndex, ref int RecordCount, string CompanyId);
    }
}
