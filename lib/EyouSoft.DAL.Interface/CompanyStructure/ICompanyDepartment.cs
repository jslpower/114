using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CompanyStructure;
namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：公司部门接口
    /// </summary>
    public interface ICompanyDepartment
    {
        /// <summary>
        /// 验证是否存在同名部门
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="DepartmentName">部门名称</param>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        bool Exists(string CompanyID, string DepartmentName, string ID);
        /// <summary>
        /// 添加一个部门
        /// </summary>
        /// <param name="model">部门实体信息</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(EyouSoft.Model.CompanyStructure.CompanyDepartment model);
        /// <summary>
        /// 修改一个部门信息
        /// </summary>
        /// <param name="modek">部门实体信息</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(EyouSoft.Model.CompanyStructure.CompanyDepartment model);
        /// <summary>
        /// 删除一个部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(string id);
        /// <summary>
        /// 根据公司编号获取部门列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyDepartment> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取一个部门信息实体
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyDepartment GetModel(string id);

        /// <summary>
        /// 根据公司编号分页获取部门列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<CompanyDepartment> GetList(string companyId);
    }
}
