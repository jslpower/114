using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 意见反馈业务逻辑类
    /// </summary>
    /// Author:汪奇志 2010-07-19
    public class ProductSuggestion:EyouSoft.IBLL.SystemStructure.IProductSuggestion
    {
        private readonly EyouSoft.IDAL.SystemStructure.IProductSuggestion dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.SystemStructure.IProductSuggestion>();

        #region CreateInstance
        /// <summary>
        /// 创建意见反馈业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.IProductSuggestion CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.IProductSuggestion op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.IProductSuggestion>();
            }
            return op;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 写入意见反馈信息
        /// </summary>
        /// <param name="info">意见反馈信息业务实体</param>
        /// <returns></returns>
        public bool InsertSuggestionInfo(EyouSoft.Model.SystemStructure.ProductSuggestionInfo info)
        {
            info.SuggestionId = Guid.NewGuid().ToString();

            return dal.InsertSuggestionInfo(info);
        }

        /// <summary>
        /// 获取意见反馈信息
        /// </summary>
        /// <param name="suggestionId">意见编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.ProductSuggestionInfo GetSuggestionInfo(string suggestionId)
        {
            if (string.IsNullOrEmpty(suggestionId)) { return null; }

            return dal.GetSuggestionInfo(suggestionId);
        }

        /// <summary>
        /// 删除意见反馈信息
        /// </summary>
        /// <param name="suggestionId">意见编号</param>
        /// <returns></returns>
        public bool DeleteSuggestionInfo(string suggestionId)
        {
            if (string.IsNullOrEmpty(suggestionId)) { return false; }

            return dal.DeleteSuggestionInfo(suggestionId);
        }

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
        public IList<EyouSoft.Model.SystemStructure.ProductSuggestionInfo> GetSuggestions(int pageSize, int pageIndex, ref int recordCount
            , string companyName, string contactName, string content, EyouSoft.Model.SystemStructure.ProductSuggestionType suggestionType)
        {
            return dal.GetSuggestions(pageSize, pageIndex, ref recordCount, companyName, contactName, content, suggestionType);
        }

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
        public IList<EyouSoft.Model.SystemStructure.ProductSuggestionInfo> GetSuggestions(int pageSize, int pageIndex, ref int recordCount
            , string companyName, string contactName, string content, params EyouSoft.Model.SystemStructure.ProductSuggestionType[] suggestionType)
        {
            return dal.GetSuggestions(pageSize, pageIndex, ref recordCount, companyName, contactName, content, suggestionType);
        }
        #endregion
    }
}
