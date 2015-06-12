using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 顾问团队业务逻辑层
    /// </summary>
    /// 周文超 2010-07-15
    public class CommunityAdvisor : IBLL.CommunityStructure.ICommunityAdvisor
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunityAdvisor() { }

        private readonly IDAL.CommunityStructure.ICommunityAdvisor dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.ICommunityAdvisor>();

        /// <summary>
        /// 构造顾问团队业务逻辑接口 
        /// </summary>
        /// <returns>顾问团队业务逻辑接口 </returns>
        public static IBLL.CommunityStructure.ICommunityAdvisor CreateInstance()
        {
            IBLL.CommunityStructure.ICommunityAdvisor op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.CommunityStructure.ICommunityAdvisor>();
            }
            return op;
        }

        #region ICommunityAdvisor 成员

        /// <summary>
        /// 添加顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddCommunityAdvisor(EyouSoft.Model.CommunityStructure.CommunityAdvisor model)
        {
            if (model == null)
                return 0;

            return dal.AddCommunityAdvisor(model) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 修改顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateCommunityAdvisor(EyouSoft.Model.CommunityStructure.CommunityAdvisor model)
        {
            if (model == null)
                return 0;

            return dal.UpdateCommunityAdvisor(model) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 删除顾问团队
        /// </summary>
        /// <param name="AdvisorId">顾问团队ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteCommunityAdvisor(int AdvisorId)
        {
            if (AdvisorId <= 0)
                return false;

            return dal.DeleteCommunityAdvisor(AdvisorId.ToString());
        }

        /// <summary>
        /// 删除顾问团队
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteCommunityAdvisor(int[] AdvisorIds)
        {
            if (AdvisorIds == null || AdvisorIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in AdvisorIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            return dal.DeleteCommunityAdvisor(strIds);
        }

        /// <summary>
        /// 审核顾问团队申请
        /// </summary>
        /// <param name="AdvisorId">顾问团队ID</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsCheck(int AdvisorId, bool IsCheck, int SysOperatorId)
        {
            if (AdvisorId <= 0 || SysOperatorId <= 0)
                return false;

            return dal.SetIsCheck(AdvisorId.ToString(), IsCheck, SysOperatorId);
        }

        /// <summary>
        /// 审核顾问团队申请
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsCheck(int[] AdvisorIds, bool IsCheck, int SysOperatorId)
        {
            if (AdvisorIds == null || AdvisorIds.Length <= 0 || SysOperatorId <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in AdvisorIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            return dal.SetIsCheck(strIds, IsCheck, SysOperatorId);
        }

        /// <summary>
        /// 设置顾问团队前台是否显示
        /// </summary>
        /// <param name="IsShow">前台是否显示</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsShow(bool IsShow, int SysOperatorId, params int[] AdvisorIds)
        {
            if (AdvisorIds == null || AdvisorIds.Length <= 0 || SysOperatorId <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in AdvisorIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            return dal.SetIsShow(strIds, IsShow, SysOperatorId);
        }

        /// <summary>
        /// 获取顾问团队
        /// </summary>
        /// <param name="TopNum">取得顾问团队的数量(小于等于0取所有)</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int TopNum,bool? IsShow)
        {
            return dal.GetCommunityAdvisorList(TopNum,IsShow);
        }

        /// <summary>
        /// 分页获取顾问团队
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int PageSize, int PageIndex, ref int RecordCount,bool? IsShow)
        {
            return dal.GetCommunityAdvisorList(PageSize, PageIndex, ref RecordCount,IsShow);
        }

        /// <summary>
        /// 获取顾问团队实体
        /// </summary>
        /// <param name="CommunityAdvisorId">顾问团队</param>
        /// <returns>获取顾问团队实体</returns>
        public Model.CommunityStructure.CommunityAdvisor GetCommunityAdvisor(int CommunityAdvisorId)
        {
            if (CommunityAdvisorId <= 0)
                return null;

            return dal.GetCommunityAdvisor(CommunityAdvisorId);
        }

        #endregion
    }
}
