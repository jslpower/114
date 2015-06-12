using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ同业中心业务层接口
    /// </summary>
    /// 郑知远 2011/05/27
    public interface IIMSuperCluster
    {
        #region 同业中心前台管理方法

        /// <summary>
        /// 根据聊天内容、超级群Id获取该同业中心消息记录
        /// </summary>
        /// <param name="days">最近几天</param>
        /// <param name="content">聊天内容</param>
        /// <param name="clusterId">超级群ID</param>
        /// <returns>超级群消息列表</returns>
        IList<Model.MQStructure.IMSuperClusterMsg> GetSuperClusterMsg(int days,string content, int clusterId);

        /// <summary>
        /// 根据同业MQ号获取同业中心会员名片信息
        /// </summary>
        /// <param name="clusterId">同业中心ID</param>
        /// <param name="mq">MQ帐号</param>
        /// <returns>会员名片信息实体</returns>
        Model.MQStructure.IMClusterUserCard GetUserCardInfoByMq(int clusterId,int mq);

        /// <summary>
        /// 根据同业中心ID获取该中心登录者除外的会员名片信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="clusterId">同业中心ID</param>
        /// <param name="mq">登录者MQ</param>
        /// <returns>会员名片实体列表</returns>
        IList<Model.MQStructure.IMClusterUserCard> GetUserCardListByClusterId(int pageSize, int pageIndex, ref int recordCount, int clusterId, int mq);
        #endregion

        #region 同业中心后台管理方法

        /// <summary>
        /// 添加同业中心
        /// </summary>
        /// <param name="model">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Add(EyouSoft.Model.MQStructure.IMSuperCluster model);

        /// <summary>
        /// 更新同业中心
        /// </summary>
        /// <param name="model">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Upd(EyouSoft.Model.MQStructure.IMSuperCluster model);

        /// <summary>
        /// 删除同业中心
        /// </summary>
        /// <param name="id">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Del(int[] id);

        /// <summary>
        /// 根据同业中心名判断该同业中心是否存在
        /// </summary>
        /// <param name="name">同业中心名</param>
        /// <param name="id">同业中心ID：0（新增）其他（修改）</param>
        /// <returns>True：存在 False：不存在</returns>
        bool IsExist(string name,int id);

        /// <summary>
        /// 根据同业中心ID和序号设置序号
        /// </summary>
        /// <param name="lst">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetNums(IList<EyouSoft.Model.MQStructure.IMSuperCluster> lst);

        /// <summary>
        /// 根据同业中心ID获取同业中心实体
        /// </summary>
        /// <param name="id">同业中心ID</param>
        /// <returns>同业中心实体</returns>
        EyouSoft.Model.MQStructure.IMSuperCluster GetSuperClusterByID(int id);

        /// <summary>
        /// 根据同业中心ID获取其他同业中心已选省市
        /// </summary>
        /// <param name="id">同业中心ID(id==0 新建)</param>
        /// <returns>已选省市实体列表</returns>
        IList<EyouSoft.Model.SystemStructure.ProvinceBase> GetSelectedProvincesByID(int id);

        /// <summary>
        /// 根据同业中心ID获取同业中心实体列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>        
        /// <returns>同业中心实体列表</returns>
        IList<Model.MQStructure.IMSuperCluster> GetList(int pageSize, int pageIndex, ref int recordCount);

        /// <summary>
        /// 获取所有的同业中心
        /// </summary>
        /// <returns>同业中心列表</returns>
        IList<EyouSoft.Model.MQStructure.IMSuperCluster> GetAllClusters();

        #endregion
    }
}
