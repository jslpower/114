using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-05-27
    /// 描述：包含项目 业务逻辑层
    /// </summary>
    public class ServiceStandard : IBLL.CompanyStructure.IServiceStandard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceStandard() { }

        private readonly IDAL.CompanyStructure.IServiceStandard dal = ComponentFactory.CreateDAL<IDAL.CompanyStructure.IServiceStandard>();

        /// <summary>
        /// 创建IBLL对象
        /// </summary>
        /// <returns></returns>
        public static IBLL.CompanyStructure.IServiceStandard CreateInstance()
        {
            IBLL.CompanyStructure.IServiceStandard op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.CompanyStructure.IServiceStandard>();
            }
            return op;
        }

        #region IServiceStandard成员
        /// <summary>
        /// 添加一条包含项目
        /// </summary>
        /// <param name="model">包含项目实体</param>
        /// <returns>true: 操作成功 false: 操作失败</returns>
        public bool Add(EyouSoft.Model.CompanyStructure.ServiceStandard model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改一条包含项目
        /// </summary>
        /// <param name="model">包含项目实体</param>
        /// <returns>true: 操作成功 false: 操作失败</returns>
        public bool Update(EyouSoft.Model.CompanyStructure.ServiceStandard model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 真实删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>true: 操作成功 false: 操作失败</returns>
        public bool Delete(int id)
        {
            if (id <= 0)
                return false;
            return dal.Delete(id);
        }
        /// <summary>
        /// 根据公司ID获取包含项目列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="TypeID">包含项目类型</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.ServiceStandard> GetList(string companyId, EyouSoft.Model.CompanyStructure.ServiceTypes TypeID, int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetList(companyId, TypeID, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获取一条包含项目实体
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>包含项目实体</returns>
        public EyouSoft.Model.CompanyStructure.ServiceStandard GetModel(int id)
        {
            if (id == 0)
                return null;
            return dal.GetModel(id);
        }
        #endregion
    }
}
