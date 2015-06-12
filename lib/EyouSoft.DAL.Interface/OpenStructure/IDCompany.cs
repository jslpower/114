using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.OpenStructure
{
    /// <summary>
    /// 各平台公司对应关系数据访问接口
    /// </summary>
    /// 周文超 2011-04-02
    public interface IDCompany
    {
        /// <summary>
        /// 添加各平台公司对应关系
        /// </summary>
        /// <param name="model">各平台公司对应关系实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int AddMCompany(Model.OpenStructure.MCompanyInfo model);

        /*/// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="SystemCompanyId">系统公司编号，其它系统（非114平台）时赋值</param>
        /// <param name="SystemType">系统类型</param>
        /// <param name="PlatformCompanyId">平台公司编号</param>
        /// <returns></returns>
        IList<Model.OpenStructure.MCompanyInfo> GetCompanyList(int SystemCompanyId, int SystemType, string PlatformCompanyId);*/
        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="systemCompanyId">系统公司编号</param>
        /// <param name="systemType">系统类型</param>
        /// <param name="platformCompanyId">平台公司编号</param>
        /// <param name="systemCompanyType">系统公司类型</param>
        /// <returns></returns>
        IList<Model.OpenStructure.MCompanyInfo> GetCompanyList(int systemCompanyId, int systemType, string platformCompanyId, int systemCompanyType);
    }
}
