using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.NewsStructure
{
    /// <summary>
    /// 新闻管理数据层接口
    /// </summary>
    /// 郑知远 2011-04-01
    public interface INewsBll
    {
        #region 运营后台方法
        /// <summary>
        /// 新闻添加
        /// </summary>
        /// <param name="model">新闻内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddNews(EyouSoft.Model.NewsStructure.NewsModel model);
        /// <summary>
        /// 新闻修改
        /// </summary>
        /// <param name="model">新闻内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool UpdateNews(EyouSoft.Model.NewsStructure.NewsModel model);
        /// <summary>
        /// 新闻删除
        /// </summary>
        /// <param name="Id">新闻内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool DelNews(int Id);
        /// <summary>
        /// 获取指定新闻详细信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.NewsModel GetModel(int Id);
        #endregion

        #region 网站前台方法
        /// <summary>
        /// 设置浏览次数
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetClicks(int Id);
        /// <summary>
        /// 获取网站首页头部置顶新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetHeadNews(int TopNum);
        /// <summary>
        /// 获取最热门的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetHotNews(int TopNum);
        /// <summary>
        /// 获取指定新闻类别的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="NewTypeId">新闻类别编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetListByNewType(int TopNum, int NewTypeId);
        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetTopNumNews(int TopNum
                                                                            , EyouSoft.Model.NewsStructure.NewsCategory? Category
                                                                            , EyouSoft.Model.NewsStructure.NewsQueryType? QueryType
                                                                            , EyouSoft.Model.NewsStructure.RecPosition? Position
                                                                            , int NewsType
                                                                            , bool? IsPic
                                                                            , int CurrNewsId);
        /// <summary>
        /// 获取指定大类别的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="Category">大类别枚举值</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetListByCategory(int TopNum, EyouSoft.Model.NewsStructure.NewsCategory Category);
        /// <summary>
        /// 获取指定新闻详细信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="pageIndex">当前内容页码</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.WebSiteNews GetModel(int Id, int pageIndex);
        /// <summary>
        /// 获取指定新闻的相关列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="CurrId">当前新闻编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetRelatedList(int TopNum,int CurrId);
        /// <summary>
        /// 获取推荐的新闻列表
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="IsPic">是否图片</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetRecommendList(int TopNum, bool IsPic);
        #endregion

        #region 公用的方法

        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.NewsModel> GetList(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.NewsStructure.SearchOrderInfo info);

        /// <summary>
        /// 指定条数获取新闻列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cate">新闻大类别</param>
        /// <param name="positioin">推荐位置</param>
        /// <param name="tag">Tag标签</param>
        /// <param name="newsType">新闻小类别</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.NewsModel> GetList(int topNum,
            EyouSoft.Model.NewsStructure.NewsCategory cate, EyouSoft.Model.NewsStructure.RecPosition positioin,int? tag,int? newsType);


        /// <summary>
        /// 指定条数获取新闻列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cate">新闻大类别</param>
        /// <param name="positioin">推荐位置</param>
        /// <param name="tag">Tag标签</param>
        /// <param name="newsType">新闻小类别</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetList(int topNum,
                                                              EyouSoft.Model.NewsStructure.NewsCategory? cate,
                                                              EyouSoft.Model.NewsStructure.RecPosition? positioin
                                                              , int? tag
                                                              , int? newsType);

        #endregion
    }
}
