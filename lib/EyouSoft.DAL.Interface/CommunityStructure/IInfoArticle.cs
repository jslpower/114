using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-14
    /// 描述：行业资讯文章数据层接口
    /// </summary>
    public interface IInfoArticle
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">行业资讯文章实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.CommunityStructure.InfoArticle model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">行业资讯文章实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.CommunityStructure.InfoArticle model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(params string[] Ids);
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">置顶状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetTop(string ID, bool IsTop);
        /// <summary>
        /// 获取头条行业资讯
        /// </summary>
        /// <param name="TopicClass">资讯大类别 =null返回全部</param>
        /// <param name="TopicArea">资讯小类别 =null返回全部</param>
        /// <returns>行业资讯实体(ID,标题,内容)</returns>
        EyouSoft.Model.CommunityStructure.InfoArticle GetHeadInfo(EyouSoft.Model.CommunityStructure.TopicClass? TopicClass, EyouSoft.Model.CommunityStructure.TopicAreas? TopicArea);
        /// <summary>
        /// 设置首页显示
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsFrontPage">是否首页显示</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetFrontPage(string ID, bool IsFrontPage);

        /// <summary>
        /// 获取行业资讯实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>行业资讯实体</returns>
        EyouSoft.Model.CommunityStructure.InfoArticle GetModel(string ID);
        /// <summary>
        ///获取指定条数的行业资讯列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="IsCurrWeekNew">是否本周最新资讯</param>
        /// <param name="CurrInfoID">是否当前资讯的相关文章 =""返回全部</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param> 
        /// <returns>行业资讯列表</returns>
        IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, bool IsCurrWeekNew, bool? IsFrontPage, string KeyWord, string CurrInfoID,
            bool? IsFocusPic, bool? IsTopPic);
        /// <summary>
        /// 根据大类别集合获取指定条数的行业资讯列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数</param>
        /// <param name="TopicList">大类别集合 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumListByTopicList(int topNumber, List<EyouSoft.Model.CommunityStructure.TopicClass> TopicList, bool? IsPicNew,
            bool? IsFocusPic, bool? IsTopPic);
        /// <summary>
        /// 分业获取行业资讯列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="InfoTag">需要匹配的标签</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetPageList(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord,string InfoTag,bool? IsFocusPic,bool? IsTopPic);
        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetClicks(string ID);

        /// <summary>
        /// 分业获取行业资讯列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="InfoTag">需要匹配的标签</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param> 
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetPageList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord, string InfoTag
            ,bool? IsFocusPic,bool? IsTopPic, params EyouSoft.Model.CommunityStructure.TopicClass?[] topClass);
        /// <summary>
        /// 获取指定类型的资讯列表（以浏览次数倒序排列）
        /// </summary>
        /// <param name="topNumber">要获取的记录行数</param>
        /// <param name="TypeId">资讯大类别 =null返回全部</param>
        /// <param name="areaId">资讯小类别 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopListByReadCount(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? TypeId, EyouSoft.Model.CommunityStructure.TopicAreas? areaId);
    }
}
