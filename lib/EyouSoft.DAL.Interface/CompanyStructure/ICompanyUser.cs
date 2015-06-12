using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：用户信息接口
    /// </summary>
    public interface ICompanyUser
    {
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
        EyouSoft.Model.ResultStructure.ResultInfo Add(ref EyouSoft.Model.CompanyStructure.CompanyUser model);
        /// <summary>
        /// 修改个人设置信息(不修改密码,角色,部门)
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        bool UpdatePersonal(EyouSoft.Model.CompanyStructure.CompanyUserBase model);
        /// <summary>
        /// 修改帐号信息
        /// </summary>
        /// <param name="model">帐号信息实体</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.CompanyStructure.CompanyUser model);
        /// <summary>
        /// 真实删除用户表和MQ表信息
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
        bool Delete(params string[] userIdList);
        /// <summary>
        /// 移除用户(即虚拟删除用户)
        /// </summary>
        /// <param name="userIdList">用户ID列表</param>
        /// <returns></returns>
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
        /// <param name="queryParams">查询参数实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> GetList(string companyId, EyouSoft.Model.CompanyStructure.QueryParamsUser queryParams);
        /// <summary>
        /// 状态设置
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isEnable">是否停用,true:停用,false:启用</param>
        /// <returns></returns>
        bool SetEnable(string id, bool isEnable);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="password">密码实体类</param>
        /// <returns></returns>
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
        /// <param name="oPUserID">oPUserID</param>
        /// <returns></returns>
        Model.CompanyStructure.CompanyUser GetModelByOPUserID(int oPUserID);
        /// <summary>
        /// 获取用户列表信息集合
        /// </summary>
        /// <param name="pageSize">每页记录灵敏</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.MLBYNUserInfo> GetUsers(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CompanyStructure.MLBYNUserSearchInfo searchInfo);
        /// <summary>
        /// 是否存在对应的公司及用户编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        bool IsExistsUserId(string companyId, string userId);
    }
}
