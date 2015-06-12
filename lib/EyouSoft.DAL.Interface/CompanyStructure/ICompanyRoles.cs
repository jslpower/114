using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CompanyStructure;
namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：公司角色接口
    /// </summary>
    public interface ICompanyUserRoles
    {
        /// <summary>
        /// 添加一条公司角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(CompanyUserRoles model);
        /// <summary>
        /// 修改一条公司角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(CompanyUserRoles model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(string id);
        /// <summary>
        /// 分页获取指定公司下的所有角色列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当期页码</param>
        /// <param name="recordCount">总记录</param>
        /// <returns></returns>
        IList<CompanyUserRoles> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取角色信息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CompanyUserRoles GetModel(string id);
        /// <summary>
        /// 验证指定公司是否重名角色
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="rolename">角色名称</param>
        /// <param name="id">编号 新增时=""</param>
        /// <returns></returns>
        bool Exists(string companyid, string rolename, string id);
        /// <summary>
        /// 获取公司管理员角色信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyUserRoles GetAdminRoleInfo(string companyId);
    }
}
