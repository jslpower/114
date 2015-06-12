using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 顾问团队数据访问接口
    /// </summary>
    /// 周文超 2010-07-15
    public interface ICommunityAdvisor
    {
        /// <summary>
        /// 添加顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>返回受影响行数</returns>
        int AddCommunityAdvisor(Model.CommunityStructure.CommunityAdvisor model);

        /// <summary>
        /// 修改顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>返回受影响行数</returns>
        int UpdateCommunityAdvisor(Model.CommunityStructure.CommunityAdvisor model);

        /// <summary>
        /// 删除顾问团队
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteCommunityAdvisor(string AdvisorIds);

        /// <summary>
        /// 审核顾问团队申请
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsCheck(string AdvisorIds, bool IsCheck, int SysOperatorId);

        /// <summary>
        /// 设置顾问团队前台是否显示
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <param name="IsShow">前台是否显示</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        bool SetIsShow(string AdvisorIds, bool IsShow, int SysOperatorId);

        /// <summary>
        /// 获取顾问团队
        /// </summary>
        /// <param name="TopNum">取得顾问团队的数量(小于等于0取所有)</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        IList<Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int TopNum,bool? IsShow);

        /// <summary>
        /// 分页获取顾问团队
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        IList<Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int PageSize, int PageIndex, ref int RecordCount, bool? IsShow);

        /// <summary>
        /// 获取顾问团队实体
        /// </summary>
        /// <param name="CommunityAdvisorId">顾问团队</param>
        /// <returns>获取顾问团队实体</returns>
        Model.CommunityStructure.CommunityAdvisor GetCommunityAdvisor(int CommunityAdvisorId);
    }
}
