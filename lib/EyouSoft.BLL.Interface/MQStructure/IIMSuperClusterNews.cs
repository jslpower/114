using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ同业中心后台公告业务层接口
    /// </summary>
    /// 郑知远 2011/05/26
    public interface IIMSuperClusterNews
    {
        #region 同业中心后台公告管理方法

        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Add(Model.MQStructure.IMSuperClusterNews model);

        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Upd(Model.MQStructure.IMSuperClusterNews model);

        /// <summary>
        /// 根据公告ID删除公告信息
        /// </summary>
        /// <param name="id">公告ID</param>
        /// <returns>True：成功 False：失败</returns>
        bool Del(int[] id);

        /// <summary>
        /// 设置序号
        /// </summary>
        /// <param name="lst">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetNums(IList<Model.MQStructure.IMSuperClusterNews> lst);

        /// <summary>
        /// 根据公告ID获取公告信息
        /// </summary>
        /// <param name="id">公告ID</param>
        /// <returns>公告信息实体</returns>
        Model.MQStructure.IMSuperClusterNews GetModel(int id);

        /// <summary>
        /// 根据同业中心ID获取公告信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>        
        /// <param name="centerId">同业中心ID</param>    
        /// <param name="type">公告类型</param>  
        /// <returns>公告信息列表</returns>
        IList<Model.MQStructure.IMSuperClusterNews> GetList(int pageSize, int pageIndex, ref int recordCount, int centerId,EyouSoft.Model.MQStructure.Type type);

        #endregion

        #region 同业中心前台公告成员方法

        /// <summary>
        /// 根据公告类型、同业中心ID获取指定条数公告
        /// </summary>
        /// <param name="top">指定条数</param>
        /// <param name="typ">公告类型</param>
        /// <param name="clusterId">同业中心ID</param>
        /// <returns>公告信息列表实体</returns>
        IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> GetSuperClusterNews(int top,
                                                                                 EyouSoft.Model.MQStructure.Type typ,
                                                                                 int clusterId);
        #endregion
    }
}
