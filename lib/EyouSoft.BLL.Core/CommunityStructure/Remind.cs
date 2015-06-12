using System;
using System.Collections.Generic;
using EyouSoft.Cache.Facade;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 模块名称：消息提醒
    /// 功能说明：首页登录下面的提醒信息
    /// </summary>
    ///  创建人：华磊 时间：2011-5-9
    public class Remind : IBLL.CommunityStructure.IRemind
    {
        private readonly IDAL.CommunityStructure.IRemind dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.IRemind>();
        /// <summary>
        /// 平台首页右侧滚动显示的用户行为数据缓存标签
        /// </summary>
        private const string StrCacheName = CacheTag.Remind.HeadRollRemind;

        #region CreateInstance
        /// <summary>
        /// 创建IBLL实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.IRemind CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IRemind op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IRemind>();
            }
            return op1;
        }
        #endregion

        #region IRemind 成员
        /// <summary>
        /// 批量添加提醒信息
        /// </summary>
        /// <param name="model">提醒实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddRemind(IList<EyouSoft.Model.CommunityStructure.Remind> model)
        {
            if (model == null && model.Count <= 0)
                return false;
            return dal.AddRemind(model);
        }
        /// <summary>
        /// 修改提醒信息
        /// </summary>
        /// <param name="model">提醒实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool UpdateRemind(EyouSoft.Model.CommunityStructure.Remind model)
        {
            if (model == null)
                return false;
            return dal.UpdateRemind(model);
        }
        /// <summary>
        /// 获取提醒信息来源
        /// </summary>
        /// <returns></returns>
        public IList<Model.CommunityStructure.Remind> GetList()
        {
            var list = (IList<Model.CommunityStructure.Remind>)EyouSoftCache.GetCache(StrCacheName);
            if (list == null || list.Count <= 0)
            {
                list = dal.GetList();
                if (list != null && list.Count > 0)
                    EyouSoftCache.Add(StrCacheName, list, DateTime.Now.AddMinutes(30));
            }

            return list;
        }
        /// <summary>
        /// 删除提醒信息
        /// </summary>
        /// <param name="CommentIds">编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteRemind(params int[] CommentIds)
        {
            if (CommentIds == null || CommentIds.Length <= 0)
                return false;

            return dal.DeleteRemind(CommentIds);
        }
        /// <summary>
        /// 获取消息实体
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns>返回程序集</returns>
        public EyouSoft.Model.CommunityStructure.Remind GetModel(int ID)
        {
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 获取指定条数的信息
        /// </summary>
        /// <param name="TopSum">条数</param>
        /// <param name="IsDisplay">是否显示</param>
        /// <returns>返回程序集</returns>
        public IList<EyouSoft.Model.CommunityStructure.Remind> GetRemindLists(int TopSum, bool? IsDisplay)
        {
            return dal.GetRemindLists(TopSum, IsDisplay);
        }
        /// <summary>
        /// 获取全部信息条数
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="IsDisplay">是否显示</param>
        /// <returns>返回程序集</returns>
        public IList<EyouSoft.Model.CommunityStructure.Remind> GetRemindLists(int PageSize, int PageIndex, ref int RecordCount, bool? IsDisplay)
        {
            return dal.GetRemindLists(PageSize, PageIndex, ref RecordCount, IsDisplay);
        }

        #endregion
    }
}
