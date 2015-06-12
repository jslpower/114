using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：互动交流评论业务逻辑接口
    /// </summary>
    public interface IExchangeComment
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="model">交流评论实体</param>
        /// <returns>-2：修改是否有子级评论失败；-1：新增评论失败；0：实体为空；1：Success</returns>
        int AddExchangeComment(EyouSoft.Model.CommunityStructure.ExchangeComment model);

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="IsCheck">是否审核</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsCheck(bool IsCheck, params string[] CommentIds);

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
        /// 获取供求信息的评论
        /// </summary>
        /// <param name="TopicId">供求信息Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetSupplyComment(string TopicId);
        /// <summary>
        /// 获取供求信息评论的所有回复
        /// </summary>
        /// <param name="CommentId">评论编号</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetCommentByCommentId(string CommentId);
        /// <summary>
        /// 获取指定条数的供求信息的评论
        /// </summary>
        /// <param name="TopNum">top条数</param>
        /// <param name="TopicId">供求信息Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetSupplyComment(int TopNum, string TopicId);

        /// <summary>
        /// 分页获取供求信息的评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">供求信息Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetSupplyComment(int PageSize, int PageIndex, ref int RecordCount
            , string TopicId);

        /// <summary>
        /// 获取嘉宾访谈评论
        /// </summary>
        /// <param name="TopicId">嘉宾访谈Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetGuestInterview(string TopicId);

        /// <summary>
        /// 获取指定条数的嘉宾访谈评论
        /// </summary>
        /// <param name="TopNum">top条数</param>
        /// <param name="TopicId">嘉宾访谈Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetGuestInterview(int TopNum, string TopicId);

        /// <summary>
        /// 分页获取获取嘉宾访谈评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">嘉宾访谈Id（必须传值）</param>
        /// <param name="CommentId">评论Id（为null不作条件）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        IList<Model.CommunityStructure.ExchangeComment> GetGuestInterview(int PageSize, int PageIndex, ref int RecordCount
            , string TopicId);

        /// <summary>
        /// 获取回复总数
        /// </summary>
        /// <param name="TopicId">信息ID</param>
        /// <param name="TopicType">信息类型</param>
        /// <returns>回复总数</returns>
        int GetCommentCountByIdAndType(string TopicId, EyouSoft.Model.CommunityStructure.TopicType TopicType);
        /// <summary>
        /// 根据回复类型分页获取所有回复信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicType">回复类别 =null返回全部</param>
        /// <returns></returns>
        IList<Model.CommunityStructure.ExchangeComment> GetGuestInterview(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.CommunityStructure.TopicType? TopicType);
    }
}
