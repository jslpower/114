using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.OpenStructure
{
    /// <summary>
    /// 各平台公司对应关系业务逻辑
    /// </summary>
    /// 周文超 2011-04-02
    public class BCompany : IBLL.OpenStructure.IBCompany
    {
        /// <summary>
        /// 各平台公司对应关系数据访问接口
        /// </summary>
        private readonly IDAL.OpenStructure.IDCompany dal = Component.Factory.ComponentFactory.CreateDAL<IDAL.OpenStructure.IDCompany>();

        /// <summary>
        /// 构造订单信息业务逻辑接口
        /// </summary>
        /// <returns>订单信息业务逻辑接口</returns>
        public static IBLL.OpenStructure.IBCompany CreateInstance()
        {
            IBLL.OpenStructure.IBCompany op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.OpenStructure.IBCompany>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BCompany()
        {

        }

        #region IMCompany 成员

        /// <summary>
        /// 添加各平台公司对应关系
        /// </summary>
        /// <param name="model">各平台公司对应关系实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddMCompany(Model.OpenStructure.MCompanyInfo model)
        {
            if (model == null)
                return 0;

            return dal.AddMCompany(model);
        }

        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="SystemCompanyId">系统公司编号，其它系统（非114平台）时赋值</param>
        /// <param name="SystemType">系统类型</param>
        /// <param name="PlatformCompanyId">平台公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.OpenStructure.MCompanyInfo> GetCompanyList(int SystemCompanyId, int SystemType, string PlatformCompanyId)
        {  
            return this.GetCompanyList(SystemCompanyId, SystemType, PlatformCompanyId, 0);
        }

        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="systemCompanyId">系统公司编号</param>
        /// <param name="systemType">系统类型</param>
        /// <param name="platformCompanyId">平台公司编号</param>
        /// <param name="systemCompanyType">系统公司类型</param>
        /// <returns></returns>
        public IList<Model.OpenStructure.MCompanyInfo> GetCompanyList(int systemCompanyId, int systemType, string platformCompanyId, int systemCompanyType)
        {
            if (systemType < 1)
            {
                return null;
            }

            EyouSoft.OpenRelation.Model.SystemType sysType = (EyouSoft.OpenRelation.Model.SystemType)systemType;

            if (sysType == EyouSoft.OpenRelation.Model.SystemType.Platform && string.IsNullOrEmpty(platformCompanyId))
            {
                return null;
            }

            if (sysType == EyouSoft.OpenRelation.Model.SystemType.TYT && systemCompanyId < 1)
            {
                return null;
            }

            if (sysType == EyouSoft.OpenRelation.Model.SystemType.YYT && systemCompanyId < 1)
            {
                return null;
            }

            //根据大平台来查询关系时系统类型将不做为查询条件
            if (sysType == EyouSoft.OpenRelation.Model.SystemType.Platform) systemType = 0;

            return dal.GetCompanyList(systemCompanyId, systemType, platformCompanyId, systemCompanyType);
        }
        #endregion
    }
}
