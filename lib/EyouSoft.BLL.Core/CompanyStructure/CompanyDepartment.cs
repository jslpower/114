using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-05-27
    /// 描述：公司部门 业务逻辑层
    /// </summary>
    public class CompanyDepartment:IBLL.CompanyStructure.ICompanyDepartment
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyDepartment() { }

        private readonly IDAL.CompanyStructure.ICompanyDepartment dal = ComponentFactory.CreateDAL<IDAL.CompanyStructure.ICompanyDepartment>();
        /// <summary>
        /// 创建IBLL对象
        /// </summary>
        /// <returns></returns>
        public static IBLL.CompanyStructure.ICompanyDepartment CreateInstance()
        {
            IBLL.CompanyStructure.ICompanyDepartment op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.CompanyStructure.ICompanyDepartment>();
            }
            return op;
        }

        #region ICompanyDepartment成员
        /// <summary>
        /// 验证是否存在同名部门
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="DepartmentName">部门名称</param>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public bool Exists(string CompanyID, string DepartmentName, string ID)
        {
            if (string.IsNullOrEmpty(CompanyID) || string.IsNullOrEmpty(DepartmentName))
                return false;
            return dal.Exists(CompanyID, DepartmentName, ID);
        }
        /// <summary>
        /// 添加一个部门
        /// </summary>
        /// <param name="model">公司部门实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.CompanyStructure.CompanyDepartment model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改一个部门信息
        /// </summary>
        /// <param name="model">公司部门实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.CompanyStructure.CompanyDepartment model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 删除一个部门信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(string id)
        {
            return dal.Delete(id);
        }
        /// <summary>
        /// 根据公司编号获取部门列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>公司部门实体</returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyDepartment> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetList(companyId, pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获取一个部门信息实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>公司部门实体</returns>
        public EyouSoft.Model.CompanyStructure.CompanyDepartment GetModel(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据公司编号分页获取部门列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<Model.CompanyStructure.CompanyDepartment> GetList(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;

            return dal.GetList(companyId); 
        }

        #endregion
    }
}
