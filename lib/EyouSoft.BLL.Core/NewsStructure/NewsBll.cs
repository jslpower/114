using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.Model.NewsStructure;

namespace EyouSoft.BLL.NewsStructure
{
    /// <summary>
    /// 新闻管理
    /// </summary>
    /// 郑知远 2011-04-01
    public class NewsBll : IBLL.NewsStructure.INewsBll
    {
        private readonly IDAL.NewsStructure.INewsDal dal = ComponentFactory.CreateDAL<IDAL.NewsStructure.INewsDal>();

        /// <summary>
        /// 新闻管理接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewsStructure.INewsBll CreateInstance()
        {
            IBLL.NewsStructure.INewsBll op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.NewsStructure.INewsBll>();
            }
            return op;
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsBll() { }
        #endregion

        #region 后台管理 NewsBll 成员
        /// <summary>
        /// 设置浏览次数
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetClicks(int Id)
        {
            return dal.SetClicks(Id);
        }
        /// <summary>
        /// 后台管理新闻添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddNews(NewsModel model)
        {
            if (model == null)
                return false;
            return dal.AddNews(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool UpdateNews(NewsModel model)
        {
            if (model == null)
                return false;
            return dal.UpdateNews(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>true:成功 false:失败</returns>
        public bool DelNews(int Id)
        {
            return dal.DelNews(Id);
        }
        /// <summary>
        /// 获新闻内容实体
        /// </summary>
        /// <returns>新闻实体</returns>
        public EyouSoft.Model.NewsStructure.NewsModel GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
        #endregion

        #region 前台运用 NewsBll 成员
        /// <summary>
        /// 获取网站首页头部置顶新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetHeadNews(int TopNum)
        {
            if (TopNum > 0)
            {
                return dal.GetTopNumNews(TopNum
                                        , null
                                        , null
                                        , EyouSoft.Model.NewsStructure.RecPosition.资讯头条, 0, null, 0);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取最热门的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetHotNews(int TopNum)
        {
            if (TopNum > 0)
            {
                return dal.GetTopNumNews(TopNum
                                        , EyouSoft.Model.NewsStructure.NewsCategory.行业资讯
                                        , null
                                        , EyouSoft.Model.NewsStructure.RecPosition.其他
                                        , 0
                                        , null
                                        , 0);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取指定新闻类别的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="NewTypeId">新闻类别编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetListByNewType(int TopNum, int NewTypeId)
        {
            if (TopNum > 0 && NewTypeId > 0)
            {
                return dal.GetTopNumNews(TopNum, null, null, EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯, NewTypeId, null, 0);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据咨询类别获取指定记录数新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetListByNewCate(int TopNum)
        {
            if (TopNum > 0)
            {
                return dal.GetTopNumNews(TopNum
                                         , EyouSoft.Model.NewsStructure.NewsCategory.行业资讯
                                         , EyouSoft.Model.NewsStructure.NewsQueryType.按普通推荐
                                         , EyouSoft.Model.NewsStructure.RecPosition.其他
                                         , 0
                                         , null
                                         , 0);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取指定大类别的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="Category">大类别枚举值</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetListByCategory(int TopNum, EyouSoft.Model.NewsStructure.NewsCategory Category)
        {
            if (TopNum > 0)
            {
                return dal.GetTopNumNews(TopNum, Category, null, EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯, 0, null, 0);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取指定新闻详细信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="pageIndex">当前内容页码</param>
        /// <returns></returns>
        public EyouSoft.Model.NewsStructure.WebSiteNews GetModel(int Id, int pageIndex)
        {
            return dal.GetModel(Id, pageIndex);
        }
        /// <summary>
        /// 获取指定新闻的相关列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="CurrId">当前新闻编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetRelatedList(int TopNum, int CurrId)
        {
            if (TopNum > 0)
            {
                return dal.GetTopNumNews(TopNum
                                         , null
                                         , EyouSoft.Model.NewsStructure.NewsQueryType.按普通推荐
                                         , EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯
                                         , 0
                                         , null
                                         , CurrId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定类别指定条数的新闻
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="Category">资讯大类别 =null不做条件</param>
        /// <param name="QueryType">检索类别 =null不做条件</param>
        /// <param name="Position">推荐位置</param>
        /// <param name="NewsType">新闻类别编号 小于等于0时不做条件</param>
        /// <param name="IsPic">是否图片 =null不做条件</param>
        /// <param name="CurrNewsId">当前新闻信息编号 小于等于0时不做条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetTopNumNews(int TopNum
                                                                            , EyouSoft.Model.NewsStructure.NewsCategory? Category
                                                                            , EyouSoft.Model.NewsStructure.NewsQueryType? QueryType
                                                                            , EyouSoft.Model.NewsStructure.RecPosition? Position
                                                                            , int NewsType
                                                                            , bool? IsPic
                                                                            , int CurrNewsId)
        {
            if (TopNum > 0)
                return dal.GetTopNumNews(TopNum, Category, QueryType, Position, NewsType, IsPic, CurrNewsId);
            return null;
        }
        /// <summary>
        /// 指定条数获取新闻列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cate">新闻大类别</param>
        /// <param name="positioin">推荐位置</param>
        /// <param name="tag">Tag标签</param>
        /// <param name="newsType">新闻小类别</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsModel> GetList(int topNum,
            EyouSoft.Model.NewsStructure.NewsCategory cate,
            EyouSoft.Model.NewsStructure.RecPosition positioin
            , int? tag
            , int? newsType)
        {
            if (topNum > 0)
                return dal.GetList(topNum, cate, positioin, tag, newsType);
            return null;
        }

        /// <summary>
        /// 获取推荐的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="IsPic">是否图片</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetRecommendList(int TopNum, bool IsPic)
        {
            if (TopNum > 0)
            {
                return dal.GetTopNumNews(TopNum
                                         , null
                                         , EyouSoft.Model.NewsStructure.NewsQueryType.按普通推荐
                                         , EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯
                                         , 0
                                         , IsPic
                                         , 0);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsModel> GetList(int pageSize
                                                                      , int pageIndex
                                                                      , ref int recordCount
                                                                      , EyouSoft.Model.NewsStructure.SearchOrderInfo info)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, info);
        }
        /// <summary>
        /// 指定条数获取新闻列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cate">新闻大类别</param>
        /// <param name="positioin">推荐位置</param>
        /// <param name="tag">Tag标签</param>
        /// <param name="newsType">新闻小类别</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetList(int topNum,
            EyouSoft.Model.NewsStructure.NewsCategory? cate,
            EyouSoft.Model.NewsStructure.RecPosition? positioin
            , int? tag
            , int? newsType)
        {
            return dal.GetList(topNum, cate, positioin, tag, newsType);
        }

        #endregion
    }

}
