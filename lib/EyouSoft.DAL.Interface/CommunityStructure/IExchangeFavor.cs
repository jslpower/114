using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-16
    /// 描述：供求收藏数据层接口
    /// </summary>
    public interface IExchangeFavor
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供求收藏实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.CommunityStructure.ExchangeBase model);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号字符（，分割）</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string Ids);
        /// <summary>
        /// 分页获取供求收藏信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">公司编号 =""返回全部</param>
        /// <param name="UserId">用户编号 =""返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyId, string UserId);
    }
}
