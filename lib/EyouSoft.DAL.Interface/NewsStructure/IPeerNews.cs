using System.Collections.Generic;

namespace EyouSoft.IDAL.NewsStructure
{
    /// <summary>
    /// 同业资讯数据访问接口
    /// </summary>
    public interface IPeerNews
    {
        /// <summary>
        /// 添加同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int AddPeerNews(Model.NewsStructure.MPeerNews model);

        /// <summary>
        /// 用户后台修改同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int CustomerUpdatePeerNews(Model.NewsStructure.MPeerNews model);

        /// <summary>
        /// 运营后台修改同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int ManageUpdatePeerNews(Model.NewsStructure.MPeerNews model);

        /// <summary>
        /// 删除同业资讯信息
        /// </summary>
        /// <param name="newId">同业资讯编号</param>
        /// <returns>返回1成功，其他失败</returns>
        int DelPeerNews(params string[] newId);

        /// <summary>
        /// 获取同业资讯信息
        /// </summary>
        /// <param name="newId">资讯编号</param>
        /// <returns>返回同业资讯信息实体</returns>
        Model.NewsStructure.MPeerNews GetPeerNews(string newId);

        /// <summary>
        /// 获取同业资讯列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回同业资讯信息集合</returns>
        IList<Model.NewsStructure.MPeerNews> GetGetPeerNewsList(int pageSize, int pageIndex, ref int recordCount,
                                                                Model.NewsStructure.MQueryPeerNews queryModel);

        /// <summary>
        /// 获取同业资讯列表
        /// </summary>
        /// <param name="topNum">top数量</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回同业资讯信息集合</returns>
        IList<Model.NewsStructure.MPeerNews> GetGetPeerNewsList(int topNum,
                                                                Model.NewsStructure.MQueryPeerNews queryModel);

        /// <summary>
        /// 更新同业资讯点击量
        /// </summary>
        /// <param name="clickNum">点击量</param>
        /// <param name="newIds">同业资讯编号集合</param>
        /// <returns>返回是否成功</returns>
        bool UpdateClickNum(int clickNum, params string[] newIds);
    }
}
