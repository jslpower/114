using System.Collections.Generic;
using EyouSoft.Component.Factory;
using EyouSoft.Model.NewsStructure;

namespace EyouSoft.BLL.NewsStructure
{
    /// <summary>
    /// 同业资讯业务逻辑
    /// </summary>
    public class BPeerNews : IBLL.NewsStructure.IPeerNews
    {
        private readonly IDAL.NewsStructure.IPeerNews _dal = ComponentFactory.CreateDAL<IDAL.NewsStructure.IPeerNews>();

        /// <summary>
        /// 新闻管理接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewsStructure.IPeerNews CreateInstance()
        {
            IBLL.NewsStructure.IPeerNews op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.NewsStructure.IPeerNews>();
            }
            return op;
        }

        /// <summary>
        /// 添加同业资讯信息
        /// </summary>
        /// <param name="model">同业资讯实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddPeerNews(MPeerNews model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Title))
                return 0;

            return _dal.AddPeerNews(model);
        }

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
        public int CustomerUpdatePeerNews(MPeerNews model)
        {
            if (model == null || string.IsNullOrEmpty(model.NewId))
                return 0;

            return _dal.CustomerUpdatePeerNews(model);
        }

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
        public int ManageUpdatePeerNews(MPeerNews model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || string.IsNullOrEmpty(model.OperatorId)
                || string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.NewId))
                return 0;

            return _dal.ManageUpdatePeerNews(model);
        }

        /// <summary>
        /// 删除同业资讯信息
        /// </summary>
        /// <param name="newId">同业资讯编号</param>
        /// <returns>返回1成功，其他失败</returns>
        public int DelPeerNews(params string[] newId)
        {
            if (newId == null || newId.Length < 1)
                return 0;

            return _dal.DelPeerNews(newId);
        }

        /// <summary>
        /// 获取同业资讯信息
        /// </summary>
        /// <param name="newId">资讯编号</param>
        /// <returns>返回同业资讯信息实体</returns>
        public MPeerNews GetPeerNews(string newId)
        {
            if (string.IsNullOrEmpty(newId))
                return null;

            return _dal.GetPeerNews(newId);
        }

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
        public IList<MPeerNews> GetGetPeerNewsList(int pageSize, int pageIndex, ref int recordCount
            , MQueryPeerNews queryModel)
        {
            return _dal.GetGetPeerNewsList(pageSize, pageIndex, ref recordCount, queryModel);
        }

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
        public IList<MPeerNews> GetGetPeerNewsList(int topNum, MQueryPeerNews queryModel)
        {
            return _dal.GetGetPeerNewsList(topNum, queryModel);
        }

        /// <summary>
        /// 更新同业资讯点击量
        /// </summary>
        /// <param name="newId">同业资讯编号</param>
        /// <returns>返回是否成功</returns>
        public bool UpdateClickNum(string newId)
        {
            if (string.IsNullOrEmpty(newId))
                return false;

            return _dal.UpdateClickNum(1, newId);
        }
    }
}
