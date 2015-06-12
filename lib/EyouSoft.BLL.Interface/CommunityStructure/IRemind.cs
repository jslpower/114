using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 模块名称：消息提醒
    /// 功能说明：首页登录下面的提醒信息
    /// </summary>
    /// 创建人：华磊 时间：2011-5-9
    public interface IRemind
    {
        /// <summary>
        /// 添加消息提醒
        /// </summary>
        /// <param name="model">消息提醒实体</param>
        /// <returns>true:成功 false:失败</returns>     
        bool AddRemind(IList<EyouSoft.Model.CommunityStructure.Remind> model);
        /// <summary>
        /// 更新提醒信息
        /// </summary>
        /// <param name="model">消息提醒实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool UpdateRemind(EyouSoft.Model.CommunityStructure.Remind model);
        /// <summary>
        /// 获取消息提醒的来源数据列表
        /// </summary>
        /// <returns>返回程序集</returns>
        IList<EyouSoft.Model.CommunityStructure.Remind> GetList();
        /// <summary>
        /// 删除消息提醒信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool DeleteRemind(params int[] CommentIds);
        /// <summary>
        /// 获取消息实体
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns>返回实体</returns>
        EyouSoft.Model.CommunityStructure.Remind GetModel(int ID);
        /// <summary>
        /// 获取指定提醒条数
        /// </summary>
        /// <param name="TopSum">数量</param>
        /// <param name="IsDisplay">是否显示</param>
        /// <returns>返回程序集</returns>
        IList<EyouSoft.Model.CommunityStructure.Remind> GetRemindLists(int TopSum, bool? IsDisplay);
        /// <summary>
        /// 获取消息提醒(全部)列表
        /// </summary>
        /// <param name="TopSum">数量</param>
        /// <param name="IsDisplay">是否显示</param>
        /// <returns>返回程序集</returns>
        IList<EyouSoft.Model.CommunityStructure.Remind> GetRemindLists(int PageSize, int PageIndex, ref int RecordCount, bool? IsDisplay);
       
    }
}
