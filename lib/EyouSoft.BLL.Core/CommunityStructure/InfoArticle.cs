using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Cache.Facade;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.CommunityStructure;
namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-14
    /// 描述：行业资讯文章业务层
    /// </summary>
    public class InfoArticle:IBLL.CommunityStructure.IInfoArticle
    {
        private readonly IDAL.CommunityStructure.IInfoArticle dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.IInfoArticle>();

        #region CreateInstance
        /// <summary>
        /// 创建IBLL实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.IInfoArticle CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IInfoArticle op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IInfoArticle>();
            }
            return op1;
        }
        #endregion

        #region IInfoArticle成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">行业资讯文章实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.CommunityStructure.InfoArticle model)
        {
            if (model == null)
                return false;
            if (model.ImgPath.Trim().Length > 0 || model.ImgThumb.Trim().Length > 0)
                model.IsImage = true;
            else
                model.IsImage = false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">行业资讯文章实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(EyouSoft.Model.CommunityStructure.InfoArticle model)
        {
            if (model == null)
                return false;
            if (model.ImgPath.Trim().Length > 0 || model.ImgThumb.Trim().Length > 0)
                model.IsImage = true;
            else
                model.IsImage = false;
            return dal.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.Delete(ID);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号集合</param>
        /// <returns></returns>
        public bool DeleteByIds(params string[] Ids)
        {
            if (Ids==null || Ids.Length==0)
                return false;
            return dal.Delete(Ids);
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">置顶状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetTop(string ID, bool IsTop)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetTop(ID, IsTop);
        }
        /// <summary>
        /// 设置首页显示
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsFrontPage">是否首页显示</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetFrontPage(string ID, bool IsFrontPage)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetFrontPage(ID, IsFrontPage);
        }
        /// <summary>
        /// 获取头条行业资讯
        /// </summary>
        /// <param name="TopicClass">资讯大类别 =null返回全部</param>
        /// <param name="TopicArea">资讯小类别 =null返回全部</param>
        /// <returns>行业资讯实体(ID,标题,内容)</returns>
        public EyouSoft.Model.CommunityStructure.InfoArticle GetHeadInfo(EyouSoft.Model.CommunityStructure.TopicClass? TopicClass, EyouSoft.Model.CommunityStructure.TopicAreas? TopicArea)
        {
            return dal.GetHeadInfo(TopicClass, TopicArea);        
        }
        /// <summary>
        /// 获取行业资讯实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>行业资讯实体</returns>
        public EyouSoft.Model.CommunityStructure.InfoArticle GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 获取指定条数的图片资讯
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsFocusPic">是否焦点图片 =null返回全部</param>
        /// <param name="IsTopPic">是否推荐图片 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumPicList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId,
            bool? IsFocusPic,bool? IsTopPic)
        {
            return dal.GetTopNumList(topNumber, topClass, areaId, true,false,null, string.Empty, string.Empty,IsFocusPic,IsTopPic);
        }
        /// <summary>
        /// 获取指定条数的本周最新资讯列表
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumCurrWeekList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew)
        {
            return dal.GetTopNumList(topNumber, topClass, areaId, IsPicNew, true,null, string.Empty, string.Empty,null,null);
        }
        /// <summary>
        /// 获取指定条数的资讯列表（包含模糊查询条件）
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="IsFrontPage">是否首页显示 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, bool? IsFrontPage, string KeyWord)
        {
            return dal.GetTopNumList(topNumber, topClass, areaId, IsPicNew, false, IsFrontPage, KeyWord, string.Empty,null,null);
        }
        /// <summary>
        /// 根据大类别集合获取指定条数的行业资讯列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数</param>
        /// <param name="TopicList">大类别集合 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumListByTopicList(int topNumber, List<EyouSoft.Model.CommunityStructure.TopicClass> TopicList, bool? IsPicNew)
        {
            return dal.GetTopNumListByTopicList(topNumber, TopicList, IsPicNew,null,null);
        }
        /// <summary>
        /// 获取指定条数的资讯列表（包含模糊查询条件）
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord)
        {
            return dal.GetTopNumList(topNumber, topClass, areaId, IsPicNew, false, null, KeyWord, string.Empty, null, null);
        }
        /// <summary>
        /// 获取指定条数的指定编号相关的资讯
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <param name="CurrInfoID">是否当前资讯的相关文章 =""返回全部</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopNumTagList(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, string CurrInfoID)
        {
            return dal.GetTopNumList(topNumber, topClass, areaId, null, false, null, string.Empty, CurrInfoID, null, null);
        }
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
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetPageList(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.CommunityStructure.TopicClass? topClass, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord, string InfoTag)
        {
            return dal.GetPageList(pageSize, pageIndex, ref recordCount, topClass, areaId, IsPicNew, KeyWord, InfoTag, null, null);
        }

        /// <summary>
        /// 分业获取行业资讯列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="topClass">大类别 =null返回全部</param>
        /// <param name="IsPicNew">是否图片资讯 =null返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="InfoTag">需要匹配的标签</param>
        /// <param name="areaId">子类别 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetPageList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.TopicAreas? areaId, bool? IsPicNew, string KeyWord, string InfoTag
            , params EyouSoft.Model.CommunityStructure.TopicClass?[] topClass)
        {
            return dal.GetPageList(pageSize, pageIndex, ref recordCount, areaId, IsPicNew, KeyWord, InfoTag, null,null,topClass);
        }

        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetClicks(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetClicks(ID);
        }
        /// <summary>
        /// 获取指定类型的资讯列表（以浏览次数倒序排列）
        /// </summary>
        /// <param name="topNumber">要获取的记录行数</param>
        /// <param name="TypeId">资讯大类别 =null返回全部</param>
        /// <param name="areaId">资讯小类别 =null返回全部</param>
        /// <returns>行业资讯列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.InfoArticle> GetTopListByReadCount(int topNumber, EyouSoft.Model.CommunityStructure.TopicClass? TypeId, EyouSoft.Model.CommunityStructure.TopicAreas? areaId)
        {
            return dal.GetTopListByReadCount(topNumber, TypeId, areaId);
        }
        #endregion

    }
}
