using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.NewsStructure
{
    /// <summary>
    /// 新闻管理数据层接口
    /// </summary>
    /// 郑知远 2011-04-01
    public interface INewsDal
    {
        #region 运营后台相关方法
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model">新闻内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddNews(EyouSoft.Model.NewsStructure.NewsModel model);
        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model">新闻内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool UpdateNews(EyouSoft.Model.NewsStructure.NewsModel model);
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="model">新闻内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool DelNews(int Id);
        /// <summary>
        /// 获取指定新闻详细信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.NewsModel GetModel(int Id);
        #endregion

        #region 网站前台相关方法
        /// <summary>
        /// 设置浏览次数
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetClicks(int Id);
        /// <summary>
        /// 获取指定类别指定条数的新闻
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="Category">资讯大类别 =null不做条件</param>
        /// <param name="QueryType">检索类别 =null不做条件</param>
        /// <param name="Position">推荐位置 =null不做条件</param>
        /// <param name="NewsType">新闻类别编号 小于等于0时不做条件</param>
        /// <param name="IsPic">是否图片 =null不做条件</param>
        /// <param name="NewsId">当前新闻信息编号 小于等于0时不做条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetTopNumNews(int TopNum, EyouSoft.Model.NewsStructure.NewsCategory? Category, EyouSoft.Model.NewsStructure.NewsQueryType? QueryType
            , EyouSoft.Model.NewsStructure.RecPosition? Position, int NewsType, bool? IsPic, int CurrNewsId);
        /// <summary>
        /// 获取指定新闻详细信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="pageIndex">当前内容页码</param>
        /// <returns></returns>
        EyouSoft.Model.NewsStructure.WebSiteNews GetModel(int Id, int pageIndex);
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
            EyouSoft.Model.NewsStructure.NewsCategory cate,
            EyouSoft.Model.NewsStructure.RecPosition positioin
            , int? tag
            , int? newsType);
        #endregion

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
    }
}
