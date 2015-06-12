using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 首页友情链接业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SysFriendLink : IBLL.SystemStructure.ISysFriendLink
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysFriendLink() { }

        private readonly IDAL.SystemStructure.ISysFriendLink dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysFriendLink>();

        /// <summary>
        /// 构造首页友情链接业务逻辑接口
        /// </summary>
        /// <returns>首页友情链接业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysFriendLink CreateInstance()
        {
            IBLL.SystemStructure.ISysFriendLink op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysFriendLink>();
            }
            return op;
        }



        #region ISysFriendLink 成员

        /// <summary>
        /// 新增友情链接
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysFriendLink(EyouSoft.Model.SystemStructure.SysFriendLink model)
        {
            if (model == null)
                return 0;

            int Result = 0;
            Result = dal.AddSysFriendLink(model);
            if (Result > 0)
            {
                model.ID = Result;
                //清空缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemFriendLink + model.LinkType.ToString());

                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 修改友情链接
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateSysFriendLink(EyouSoft.Model.SystemStructure.SysFriendLink model)
        {
            if (model == null)
                return 0;

            int Result = 0;
            Result = dal.UpdateSysFriendLink(model);
            if (Result > 0)
            {
                //清空缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemFriendLink + model.LinkType.ToString());

                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="FriendLinkId">友情链接ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysFriendLink(int FriendLinkId)
        {
            if (FriendLinkId <= 0)
                return false;

            bool IsResult = dal.DeleteSysFriendLink(FriendLinkId.ToString());

            if (IsResult)
            {
                //清空缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemFriendLink + EyouSoft.Model.SystemStructure.FriendLinkType.文字);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemFriendLink + EyouSoft.Model.SystemStructure.FriendLinkType.战略合作);
            }

            return IsResult;
        }

        /// <summary>
        /// 根据ID获取友情链接信息
        /// </summary>
        /// <param name="FriendLinkId">友情链接ID</param>
        /// <returns>友情链接实体</returns>
        public EyouSoft.Model.SystemStructure.SysFriendLink GetSysFriendLinkModel(int FriendLinkId)
        {
            if (FriendLinkId <= 0)
                return null;

            EyouSoft.Model.SystemStructure.SysFriendLink model = dal.GetSysFriendLinkModel(FriendLinkId);

            return model;

        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="LinkType">链接类型，小于等于0不作为条件</param>
        /// <returns>返回友情链接实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType LinkType)
        {
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> List = null;
            object CacheList = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemFriendLink + LinkType.ToString());
            if (CacheList != null)
                List = (IList<EyouSoft.Model.SystemStructure.SysFriendLink>)CacheList;
            else
            {
                List = dal.GetSysFriendLinkList(LinkType);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemFriendLink + LinkType.ToString(), List);
            }

            return List;
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引:0/1添加时间升/降序</param>
        /// <param name="LinkType">类型</param>
        /// <returns>返回友情链接实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetSysFriendLinkList(int PageSize, int PageIndex
            , ref int RecordCount, int OrderIndex, EyouSoft.Model.SystemStructure.FriendLinkType? LinkType)
        {
            return dal.GetSysFriendLinkList(PageSize, PageIndex, ref RecordCount, OrderIndex, LinkType);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="FriendLinkIds">友情链接ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysFriendLink(int[] FriendLinkIds)
        {
            if (FriendLinkIds == null || FriendLinkIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in FriendLinkIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            bool IsResult = dal.DeleteSysFriendLink(strIds);
            if (IsResult)
            {
                //清空缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemFriendLink + EyouSoft.Model.SystemStructure.FriendLinkType.文字);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemFriendLink + EyouSoft.Model.SystemStructure.FriendLinkType.战略合作);
            }

            return IsResult;
        }

        #endregion
    }
}
