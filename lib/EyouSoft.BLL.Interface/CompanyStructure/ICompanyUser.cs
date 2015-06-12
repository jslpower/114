using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-02
    /// 描述：用户信息接口
    /// </summary>
    public interface ICompanyUser
    {
        /// <summary>
        /// 根据未加密密码初始化密码信息实体类(若明文密码为空,则返回所有密码字段都为空)
        /// </summary>
        /// <param name="noEncryptPassword">未加密密码</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.PassWord InitPassWordModel(string noEncryptPassword);
        /// <summary>
        /// 判断E-MAIL是否已存在
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns></returns>
        bool IsExistsEmail(string email);
        /// <summary>
        /// 判断E-MAIL是否已存在
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="userId">当前修改Email的用户ID</param>
        /// <returns></returns>
        bool IsExistsEmail(string email, string userId);
        /// <summary>
        /// 判断用户名是否已存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        bool IsExists(string userName);
        /// <summary>
        /// 添加帐号[添加帐号后自动生成MQ号码]
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSER_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSER_ADD, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""UserName"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""CompanyID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSER_ADD_CODE
            )]
        EyouSoft.Model.ResultStructure.ResultInfo Add(EyouSoft.Model.CompanyStructure.CompanyUser model);
        /// <summary>
        /// 更新机票用户的密码
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <param name="md5Pw">MD5密码</param>
        /// <returns></returns>
        bool UpdateTicketUserPwd(string userId, string md5Pw);
        /// <summary>
        /// 修改个人设置信息(密码不进行修改)
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERPERSON_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERPERSON_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""Id"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERPERSON_UPDATE_CODE
            )]
        bool UpdatePersonal(EyouSoft.Model.CompanyStructure.CompanyUserBase model);
        /// <summary>
        /// 修改子帐号信息(公司编号,其它平台用户编号,省份编号,城市编号,用户名,是否停用,是否管理员不做修改)
        /// 若设置的密码为null或为空,则密码不进行修改
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""Id"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_UPDATE_CODE
            )]
        bool UpdateChild(EyouSoft.Model.CompanyStructure.CompanyUser model);
        /// <summary>
        /// 真实删除用户表和MQ表信息
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_DELETE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""userIdList"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_DELETE_CODE
            )]        
        bool Delete(params string[] userIdList);
        /// <summary>
        /// 移除用户(即虚拟删除用户)
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_REOMVE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_REOMVE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""userIdList"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERCHILD_REOMVE_CODE
            )]        
        bool Remove(params string[] userIdList);
        /// <summary>
        /// 获得子帐号数量[不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        int GetSubUserCount(string companyId);
        /// <summary>
        /// 获取指定公司下的所有子帐号用户详细信息列表[注;不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyUser> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取指定公司下的所有帐号用户详细信息列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="queryParams">查询参数实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyUser> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser queryParams, int pageSize, int pageIndex, ref int recordCount);
        /// <summary>
        /// 获取指定公司下的所有子帐号用户基本信息列表[注;不包括总帐号]
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> GetList(string companyId);
        /// <summary>
        /// 获取指定公司下的所有帐号用户基本信息列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="queryParams">查询参数实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser queryParams);
        /// <summary>
        /// 状态设置
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isEnable">是否停用,true:停用,false:启用</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSER_SETENABLE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSER_SETENABLE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""Id"",""AttributeType"":""val""},{""Index"":0,""Attribute"":""isEnable"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSER_SETENABLE_CODE
            )]
        bool SetEnable(string id, bool isEnable);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="password">密码实体类</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERPERSON_UPDATE_PASSWORD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERPERSON_UPDATE_PASSWORD, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""Id"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYUSERPERSON_UPDATE_PASSWORD_CODE
            )]
        bool UpdatePassWord(string id, EyouSoft.Model.CompanyStructure.PassWord password);
        /// <summary>
        /// 获得实体信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyUser GetModel(string id);
        /// <summary>
        /// 获得实体信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyUser GetModelByUserName(string userName);
        /// <summary>
        /// 获得管理员实体信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyUser GetAdminModel(string companyId);
        /// <summary>
        /// 根据MQID获取用户信息
        /// </summary>
        /// <param name="MQId">用户对应MQID</param>
        /// <returns></returns>
        Model.CompanyStructure.CompanyUser GetModel(int MQId);
        /// <summary>
        /// 根据oPUserID获取用户信息
        /// </summary>
        /// <param name="oPUserID">用户对应oPUserID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanyUser GetModelByOPUserID(int oPUserID);
        /// <summary>
        /// 获取用户列表信息集合
        /// </summary>
        /// <param name="pageSize">每页记录灵敏</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.MLBYNUserInfo> GetUsers(int pageSize,int pageIndex,ref int recordCount,EyouSoft.Model.CompanyStructure.MLBYNUserSearchInfo searchInfo);
        /// <summary>
        /// 是否存在对应的公司及用户编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        bool IsExistsUserId(string companyId, string userId);
    }
}
