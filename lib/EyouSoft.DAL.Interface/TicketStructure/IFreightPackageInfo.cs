using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 运价套餐接口
    /// </summary>
    /// 鲁功源 2010-10-25
    public interface IFreightPackageInfo
    {
        /// <summary>
        /// 添加运价套餐
        /// </summary>
        /// <param name="model">运价套餐实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model);
        /// <summary>
        /// 修改运价套餐信息
        /// </summary>
        /// <param name="model">运价套餐实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(int Id);
        /// <summary>
        /// 获取套餐运价实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>套餐运价实体</returns>
        EyouSoft.Model.TicketStructure.TicketFreightPackageInfo GetModel(int Id);
        /// <summary>
        /// 分页获取套餐运价信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="PackageType">套餐类型 =null返回全部</param>
        /// <param name="IsDeleted">是否已删除 =null返回全部</param>
        /// <returns>套餐运价信息列表</returns>
        IList<EyouSoft.Model.TicketStructure.TicketFreightPackageInfo> GetList(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.TicketStructure.PackageTypes? PackageType,bool? IsDeleted);
    }
}
