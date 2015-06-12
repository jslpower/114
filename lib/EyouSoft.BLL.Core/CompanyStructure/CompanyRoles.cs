using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：公司角色 业务逻辑层
    /// </summary>
    public class CompanyUserRoles : IBLL.CompanyStructure.ICompanyUserRoles
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyUserRoles() { }

        private readonly IDAL.CompanyStructure.ICompanyUserRoles dal = ComponentFactory.CreateDAL<IDAL.CompanyStructure.ICompanyUserRoles>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static IBLL.CompanyStructure.ICompanyUserRoles CreateInstance()
        {
            IBLL.CompanyStructure.ICompanyUserRoles op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.CompanyStructure.ICompanyUserRoles>();
            }
            return op;
        }

        #region ICompanyRoles 成员
        /// <summary>
        /// 添加一条公司角色
        /// </summary>
        /// <param name="model">公司角色实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.CompanyStructure.CompanyUserRoles model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改一条公司角色
        /// </summary>
        /// <param name="model">公司角色实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.CompanyStructure.CompanyUserRoles model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            return dal.Delete(id);
        }
        /// <summary>
        /// 获取指定公司下的所有角色列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>公司角色列表</returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyUserRoles> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;
            return dal.GetList(companyId,pageSize,pageIndex,ref recordCount);
        }
        /// <summary>
        /// 获取角色信息实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>公司角色实体</returns>
        public EyouSoft.Model.CompanyStructure.CompanyUserRoles GetModel(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            return dal.GetModel(id);
        }
        /// <summary>
        /// 验证指定公司是否重名角色
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="rolename">角色名称</param>
        /// <param name="id">编号 新增时=""</param>
        /// <returns></returns>
        public bool Exists(string companyid, string rolename, string id)
        {
            if (string.IsNullOrEmpty(companyid))
                return false;
            return dal.Exists(companyid, rolename, id);
        }
        #endregion

    }
}
