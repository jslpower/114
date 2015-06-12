using System.Collections.Generic;

namespace EyouSoft.IBLL.NewsStructure
{
    /// <summary>
    /// 同业资讯业务逻辑接口
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
        /// <remarks>
        /// 图片和附件通过集合的传值与否来修改：
        /// 图片附件集合为空或者长度小于1，不修改原来的附件信息，
        /// 否则删除原来的信息，保存新的图片附件信息
        /// </remarks>
        int CustomerUpdatePeerNews(Model.NewsStructure.MPeerNews model);

        /// <summary>
        /// 运营后台修改同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        /// <remarks>
        /// 图片和附件通过集合的传值与否来修改：
        /// 图片附件集合为空或者长度小于1，不修改原来的附件信息，
        /// 否则删除原来的信息，保存新的图片附件信息
        /// </remarks>
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
        /// <remarks>
        /// 1.用户后台->我的网店->我的同业资讯 这个列表查询实体中的公司编号(CompanyId)必须传值
        /// 2.网店（高级、普通）同业资讯列表 查询实体中的公司编号(CompanyId)必须传值
        /// 3.用户后台->营销工具->同业资讯 这个列表查询实体中的公司编号(CompanyId)不要传值
        /// </remarks>
        IList<Model.NewsStructure.MPeerNews> GetGetPeerNewsList(int pageSize, int pageIndex, ref int recordCount,
                                                                Model.NewsStructure.MQueryPeerNews queryModel);

        /// <summary>
        /// 获取同业资讯列表
        /// </summary>
        /// <param name="topNum">top数量</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回同业资讯信息集合</returns>
        /// <remarks>
        /// 1.用户后台->我的网店->我的同业资讯 这个列表查询实体中的公司编号(CompanyId)必须传值
        /// 2.网店（高级、普通）同业资讯列表 查询实体中的公司编号(CompanyId)必须传值
        /// 3.用户后台->营销工具->同业资讯 这个列表查询实体中的公司编号(CompanyId)不要传值
        /// </remarks>
        IList<Model.NewsStructure.MPeerNews> GetGetPeerNewsList(int topNum,
                                                                Model.NewsStructure.MQueryPeerNews queryModel);

        /// <summary>
        /// 更新同业资讯点击量
        /// </summary>
        /// <param name="newId">同业资讯编号</param>
        /// <returns>返回是否成功</returns>
        bool UpdateClickNum(string newId);
    }
}
