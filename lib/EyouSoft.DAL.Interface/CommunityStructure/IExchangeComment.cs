using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：互动交流评论接口
    /// </summary>
    public interface IExchangeComment
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="model">交流评论实体</param>
        /// <returns>返回受影响行数</returns>
        int AddExchangeComment(Model.CommunityStructure.ExchangeComment model);

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="IsCheck">是否审核</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsCheck(bool IsCheck, params string[] CommentIds);

        /// <summary>
        /// 更新是否有子级评论
        /// </summary>
        /// <param name="CommentId">评论ID</param>
        /// <param name="IsHasNextLevel">否有子级评论</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsHasNextLevel(string CommentId, bool IsHasNextLevel);

        /// <summary>
        /// 虚拟删除评论
        /// </summary>
        /// <param name="IsDelete">是否虚拟删除</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsDelete(bool IsDelete, params string[] CommentIds);

         /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="CommentIds">评论ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(params string[] CommentIds);

        /// <summary>
        /// 根据互动交流删除其下所有评论
        /// </summary>
        /// <param name="TopicId">互动交流ID</param>
        /// <param name="TopicType">评论主题类型</param>
        /// <returns>false:失败 true:成功</returns>
        bool DeleteByExchange(string TopicId, EyouSoft.Model.CommunityStructure.TopicType TopicType);

        /// <summary>
        /// 获取互动交流下的评论
        /// </summary>
        /// <param name="TopNum">top条数(小于等于0取所有)</param>
        /// <param name="TopicId">互动交流ID(必须传值)</param>
        /// <param name="TopicType">评论类型(为null不作条件)</param>
        /// <param name="CommentId">所回复的评论编号(小于等于0不作条件)</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetExchangeCommentList(int TopNum, string TopicId
            , Model.CommunityStructure.TopicType? TopicType, string CommentId);
         /// <summary>
        /// 分页获取互动交流下的评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">互动交流ID(必须传值)</param>
        /// <param name="TopicType">评论类型(为null不作条件)</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetExchangeCommentList(int PageSize, int PageIndex, ref int RecordCount, string TopicId, 
            EyouSoft.Model.CommunityStructure.TopicType? TopicType);
        /// <summary>
        /// 分页获取互动交流下的评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">互动交流ID(必须传值)</param>
        /// <param name="TopicType">评论类型(为null不作条件)</param>
        /// <param name="CommentId">所回复的评论编号(小于等于0不作条件)</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetExchangeCommentList(int PageSize, int PageIndex, ref int RecordCount
            , string TopicId, Model.CommunityStructure.TopicType? TopicType, string CommentId);

        /// <summary>
        /// 获取回复总数
        /// </summary>
        /// <param name="TopicId">信息ID</param>
        /// <param name="TopicType">信息类型</param>
        /// <returns>回复总数</returns>
        int GetCommentCountByIdAndType(string TopicId, EyouSoft.Model.CommunityStructure.TopicType TopicType);
    }
}
