using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 意见反馈数据访问类接口
    /// </summary>
    /// Author:汪奇志 2010-07-19
    public interface IProductSuggestion
    {
        /// <summary>
        /// 写入意见反馈信息
        /// </summary>
        /// <param name="info">意见反馈信息业务实体</param>
        /// <returns></returns>
        bool InsertSuggestionInfo(EyouSoft.Model.SystemStructure.ProductSuggestionInfo info);

        /// <summary>
        /// 获取意见反馈信息
        /// </summary>
        /// <param name="suggestionId">意见编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.ProductSuggestionInfo GetSuggestionInfo(string suggestionId);

        /// <summary>
        /// 删除意见反馈信息
        /// </summary>
        /// <param name="suggestionId">意见编号</param>
        /// <returns></returns>
        bool DeleteSuggestionInfo(string suggestionId);

        /// <summary>
        /// 获取意见反馈信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="contactName">联系人 为null时不做为查询条件</param>
        /// <param name="content">内容 为null时不做为查询条件</param>
        /// <param name="suggestionType">意见反馈类别 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.ProductSuggestionInfo> GetSuggestions(int pageSize, int pageIndex, ref int recordCount
            , string companyName, string contactName, string content, params EyouSoft.Model.SystemStructure.ProductSuggestionType[] suggestionType);
    }
}
