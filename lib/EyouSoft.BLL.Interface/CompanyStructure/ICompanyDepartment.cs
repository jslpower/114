using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CompanyStructure;
namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-27
    /// 描述：公司部门 业务逻辑接口
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
        /// <param name="model"></param>
        /// <returns>false:失败 true:成功</returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_ADD, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""DepartName"",""AttributeType"":""class""}, {""Index"":0,""Attribute"":""CompanyID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_ADD_CODE
            )]
        bool Add(CompanyDepartment model);
        /// <summary>
        /// 修改一个部门信息
        /// </summary>
        /// <param name="modek"></param>
        /// <returns>false:失败 true:成功</returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}, {""Index"":0,""Attribute"":""DepartName"",""AttributeType"":""class""}, {""Index"":0,""Attribute"":""CompanyID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_UPDATE_CODE
            )]
        bool Update(CompanyDepartment modek);
        /// <summary>
        /// 删除一个部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>false:失败 true:成功</returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_DELETE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""id"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYDEPARTMENT_DELETE_CODE
            )]
        bool Delete(string id);
        /// <summary>
        /// 根据公司编号分页获取部门列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<CompanyDepartment> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取一个部门信息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CompanyDepartment GetModel(string id);

        /// <summary>
        /// 根据公司编号分页获取部门列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<CompanyDepartment> GetList(string companyId);
    }
}
