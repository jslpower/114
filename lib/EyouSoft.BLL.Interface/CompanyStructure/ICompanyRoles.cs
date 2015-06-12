using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CompanyStructure;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-27
    /// 描述：公司角色 业务逻辑接口
    /// </summary>
    public interface ICompanyUserRoles
    {
        /// <summary>
        /// 添加一条公司角色
        /// </summary>
        /// <param name="model">公司角色实体</param>
        /// <returns>false:失败 true:成功</returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_ADD, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""RoleName"",""AttributeType"":""class""}, {""Index"":0,""Attribute"":""CompanyID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_ADD_CODE
            )]
        bool Add(CompanyUserRoles model);
        /// <summary>
        /// 修改一条公司角色
        /// </summary>
        /// <param name="model">公司角色实体</param>
        /// <returns>false:失败 true:成功</returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}, {""Index"":0,""Attribute"":""RoleName"",""AttributeType"":""class""}, {""Index"":0,""Attribute"":""CompanyID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_UPDATE_CODE
            )]
        bool Update(CompanyUserRoles model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_DELETE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""id"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYROLES_DELETE_CODE
            )]
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
        /// <param name="id">主键ID</param>
        /// <returns>公司角色实体</returns>
        CompanyUserRoles GetModel(string id);
        /// <summary>
        /// 验证指定公司是否重名角色
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="rolename">角色名称</param>
        /// <param name="id">编号 新增时=""</param>
        /// <returns></returns>
        bool Exists(string companyid, string rolename, string id);
    }
}
