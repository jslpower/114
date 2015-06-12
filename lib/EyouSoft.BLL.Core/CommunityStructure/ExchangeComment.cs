using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.SystemStructure;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：互动交流评论业务逻辑层
    /// </summary>
    public class ExchangeComment : IBLL.CommunityStructure.IExchangeComment
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeComment() { }

        private readonly IDAL.CommunityStructure.IExchangeComment dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.IExchangeComment>();

        #region CreateInstance

        /// <summary>
        /// 创建互动交流评论业务逻辑接口的实例
        /// </summary>
        /// <returns>互动交流评论业务逻辑接口</returns>
        public static EyouSoft.IBLL.CommunityStructure.IExchangeComment CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IExchangeComment op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IExchangeComment>();
            }
            return op1;
        }

        #endregion

        #region IExchangeComment 成员

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="model">交流评论实体</param>
        /// <returns>-2：修改是否有子级评论失败；-1：新增评论失败；0：实体为空；1：Success</returns>
        public int AddExchangeComment(EyouSoft.Model.CommunityStructure.ExchangeComment model)
        {
            if (model == null && string.IsNullOrEmpty(model.TopicId))
                return 0;

            int Result = 0;
            using (System.Transactions.TransactionScope AddTran = new System.Transactions.TransactionScope())
            {
                model.ID = Guid.NewGuid().ToString();
                Result = dal.AddExchangeComment(model);
                if (Result <= 0)
                    return -1;

                if (!string.IsNullOrEmpty(model.CommentId))
                {
                    Result = dal.SetIsHasNextLevel(model.CommentId, true) ? 1 : -2;
                    if (Result <= 0)
                        return -2;
                }
                if (model.TopicType == EyouSoft.Model.CommunityStructure.TopicType.供求)
                {
                    if(!BLL.CommunityStructure.ExchangeList.CreateInstance().SetWriteBackCount(model.TopicId))
                        return -4;
                }
                if (model.TopicType == EyouSoft.Model.CommunityStructure.TopicType.供求 && string.IsNullOrEmpty(model.CommentId))
                {
                    Result = BLL.MQStructure.IMMessage.CreateInstance().AddMessageToExchangeComment(model.ID) ? 1 : -3;
                }

                AddTran.Complete();
            }
            return Result;
        }

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="IsCheck">是否审核</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsCheck(bool IsCheck, params string[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            return dal.SetIsCheck(IsCheck, CommentIds);
        }

        /// <summary>
        /// 虚拟删除评论
        /// </summary>
        /// <param name="IsDelete">是否虚拟删除</param>
        /// <param name="CommentIds">评论ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsDelete(bool IsDelete, params string[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            return dal.SetIsDelete(IsDelete, CommentIds);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="CommentIds">评论ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(params string[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            return dal.Delete(CommentIds);
        }

        /// <summary>
        /// 根据互动交流删除其下所有评论
        /// </summary>
        /// <param name="TopicId">互动交流ID</param>
        /// <param name="TopicType">评论主题类型</param>
        /// <returns>false:失败 true:成功</returns>
        public bool DeleteByExchange(string TopicId, EyouSoft.Model.CommunityStructure.TopicType TopicType)
        {
            if (string.IsNullOrEmpty(TopicId))
                return false;

            return dal.DeleteByExchange(TopicId,TopicType);
        }

        /// <summary>
        /// 获取供求信息的评论
        /// </summary>
        /// <param name="TopicId">供求信息Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetSupplyComment(string TopicId)
        {
            if (string.IsNullOrEmpty(TopicId))
                return null;

            return dal.GetExchangeCommentList(-1, TopicId, Model.CommunityStructure.TopicType.供求, string.Empty);
        }
        /// <summary>
        /// 获取评论的所有回复
        /// </summary>
        /// <param name="CommentId">评论编号</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<Model.CommunityStructure.ExchangeComment> GetCommentByCommentId(string CommentId)
        {
            if (string.IsNullOrEmpty(CommentId))
                return null;

            return dal.GetExchangeCommentList(-1, string.Empty,null, CommentId);
        }
        /// <summary>
        /// 获取指定条数的供求信息的评论
        /// </summary>
        /// <param name="TopNum">top条数</param>
        /// <param name="TopicId">供求信息Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetSupplyComment(int TopNum, string TopicId)
        {
            if (string.IsNullOrEmpty(TopicId) || TopNum <= 0)
                return null;

            return dal.GetExchangeCommentList(TopNum, TopicId, Model.CommunityStructure.TopicType.供求, string.Empty);
        }

        /// <summary>
        /// 分页获取供求信息的评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicId">供求信息Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetSupplyComment(int PageSize, int PageIndex, ref int RecordCount, string TopicId)
        {
            if (string.IsNullOrEmpty(TopicId))
                return null;

            return dal.GetExchangeCommentList(PageSize, PageIndex, ref RecordCount, TopicId, Model.CommunityStructure.TopicType.供求);
        }

        /// <summary>
        /// 获取嘉宾访谈评论
        /// </summary>
        /// <param name="TopicId">嘉宾访谈Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetGuestInterview(string TopicId)
        {
            if (string.IsNullOrEmpty(TopicId))
                return null;

            return dal.GetExchangeCommentList(-1, TopicId, Model.CommunityStructure.TopicType.嘉宾, string.Empty);
        }

        /// <summary>
        /// 获取指定条数的嘉宾访谈评论
        /// </summary>
        /// <param name="TopNum">top条数</param>
        /// <param name="TopicId">嘉宾访谈Id（必须传值）</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetGuestInterview(int TopNum, string TopicId)
        {
            if (string.IsNullOrEmpty(TopicId) || TopNum <= 0)
                return null;

            return dal.GetExchangeCommentList(TopNum, TopicId, Model.CommunityStructure.TopicType.嘉宾, string.Empty);
        }

        /// <summary>
        /// 分页获取嘉宾访谈评论
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <returns>返回互动交流评论实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeComment> GetGuestInterview(int PageSize, int PageIndex, ref int RecordCount, string TopicId)
        {
            if (string.IsNullOrEmpty(TopicId))
                return null;

            return dal.GetExchangeCommentList(PageSize, PageIndex, ref RecordCount, TopicId, Model.CommunityStructure.TopicType.嘉宾);
        }

        /// <summary>
        /// 获取回复总数
        /// </summary>
        /// <param name="TopicId">信息ID</param>
        /// <param name="TopicType">信息类型</param>
        /// <returns>回复总数</returns>
        public virtual int GetCommentCountByIdAndType(string TopicId, EyouSoft.Model.CommunityStructure.TopicType TopicType)
        {
            if (string.IsNullOrEmpty(TopicId))
                return 0;

            return dal.GetCommentCountByIdAndType(TopicId, TopicType);
        }
        /// <summary>
        /// 根据回复类型分页获取所有回复信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="TopicType">回复类别 =null返回全部</param>
        /// <returns></returns>
        public IList<Model.CommunityStructure.ExchangeComment> GetGuestInterview(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.CommunityStructure.TopicType? TopicType)
        {
            return dal.GetExchangeCommentList(PageSize, PageIndex, ref RecordCount, string.Empty,TopicType, string.Empty);
        }
        #endregion
    }
}
