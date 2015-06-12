using System.Collections.Generic;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ同业中心公告业务层接口
    /// </summary>
    /// 郑知远 2011-05-26
    public class IMSuperClusterNews:IBLL.MQStructure.IIMSuperClusterNews
    {
         private readonly IDAL.MQStructure.IIMSuperClusterNews dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMSuperClusterNews>();

        /// <summary>
        /// MQ同业中心公告业务层接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMSuperClusterNews CreateInstance()
        {
            IBLL.MQStructure.IIMSuperClusterNews op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMSuperClusterNews>();
            }
            return op;
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMSuperClusterNews() { }
        #endregion

        #region 后台运用 IMSuperClusterNews 成员

        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Add(Model.MQStructure.IMSuperClusterNews model)
        {
            if (model != null)
            {
                return this.dal.Add(model);
            }
            else
            {
                return false;
            }
                
        }

        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Upd(Model.MQStructure.IMSuperClusterNews model)
        {
            if (model != null)
            {
                return this.dal.Upd(model);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据公告ID删除公告信息
        /// </summary>
        /// <param name="id">公告ID</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Del(int[] id)
        {
            if (id != null && id.Length > 0)
            {
                return this.dal.Del(id);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置序号
        /// </summary>
        /// <param name="lst">公告信息实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetNums(IList<Model.MQStructure.IMSuperClusterNews> lst)
        {
            if (lst != null && lst.Count > 0)
            {
                return this.dal.SetNums(lst);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据公告ID获取公告信息
        /// </summary>
        /// <param name="id">公告ID</param>
        /// <returns>公告信息实体</returns>
        public Model.MQStructure.IMSuperClusterNews GetModel(int id)
        {
            if (id > 0)
            {
                return this.dal.GetModel(id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据同业中心ID获取公告信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>        
        /// <param name="centerId">同业中心ID</param>
        /// <param name="type">公告类型</param> 
        /// <returns>公告信息列表</returns>
        public IList<Model.MQStructure.IMSuperClusterNews> GetList(int pageSize, int pageIndex, ref int recordCount,int centerId,EyouSoft.Model.MQStructure.Type type)
        {
            return this.dal.GetList(pageSize, pageIndex, ref recordCount, centerId, type);
        }

        #endregion

        #region 前台运用 IMSuperClusterNews 成员

        /// <summary>
        /// 根据公告类型、同业中心ID获取指定条数公告
        /// </summary>
        /// <param name="top">指定条数</param>
        /// <param name="typ">公告类型</param>
        /// <param name="clusterId">同业中心ID</param>
        /// <returns>公告信息列表实体</returns>
        public IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> GetSuperClusterNews(int top,EyouSoft.Model.MQStructure.Type typ,int clusterId)
        {
            if (top != 0 && clusterId != 0)
            {
                return this.dal.GetSuperClusterNews(top, typ, clusterId);
            }
            else
            {
                 return null;
            }           
        }
        #endregion
    }
}
