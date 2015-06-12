using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;


namespace EyouSoft.BLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-服务承诺书业务逻辑类
    /// </summary>
    public class CreditLetter : EyouSoft.IBLL.CreditSystemStructure.ICreditLetter
    {
        private readonly EyouSoft.IDAL.CreditSystemStructure.ICreditLetter idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CreditSystemStructure.ICreditLetter>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CreditSystemStructure.ICreditLetter CreateInstance()
        {
            EyouSoft.IBLL.CreditSystemStructure.ICreditLetter op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CreditSystemStructure.ICreditLetter>();
            }
            return op;
        }

        /// <summary>
        /// 新增服务承诺书信息
        /// </summary>
        /// <param name="info">服务承诺书业务实体</param>
        /// <returns>int 0:操作失败 1:操作成功 2:已存在该公司服务承诺书</returns>
        public int Insert(EyouSoft.Model.CreditSystemStructure.CreditLetter info)
        {
            int result = 0;

            if (!idal.IsExist(info.CompanyId))
            {
                info.CheckedState = 0;
                info.IssueTime = DateTime.Now;
                result = idal.Insert(info) ? 1 : 0;
            }
            else
            {
                result = 2;
            }

            return result;
        }

        /// <summary>
        /// 根据指定的公司编号更新服务承诺书信息
        /// </summary>
        /// <param name="info">服务承诺书业务实体</param>
        /// <returns></returns>
        public bool UpdateByCompanyId(EyouSoft.Model.CreditSystemStructure.CreditLetter info)
        {
            info.CheckedState = 0;
            info.IssueTime = DateTime.Now;
            return idal.UpdateByCompanyId(info);
        }

        /// <summary>
        /// 根据指定的承诺书编号更新服务承诺书信息
        /// </summary>
        /// <param name="info">服务承诺书业务实体</param>
        /// <returns>int 0:操作失败 1:操作成功 2:已存在该公司服务承诺书</returns>
        public int UpdateByCreditId(EyouSoft.Model.CreditSystemStructure.CreditLetter info)
        {
            int result = 0;

            if (!idal.IsExist(info.CompanyId, info.Id))
            {
                info.CheckedState = 0;
                info.IssueTime = DateTime.Now;
                result = idal.UpdateByCreditId(info) ? 1 : 0;
            }
            else
            {
                result = 2;
            }

            return result;
        }

        /// <summary>
        /// 根据指定的公司编号获取服务承诺书状态,0:未审核 1:通过审核 2:未通过审核 3:失效 -1:没有任务服务承诺服务书信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>int 0:未审核 1:通过审核 2:未通过审核 3:失效 -1:没有任务服务承诺服务书信息</returns>
        public int GetCreditState(int companyId)
        {
            return idal.GetCreditState(companyId);
        }

        /// <summary>
        /// 根据指定的公司编号获取服务承诺书信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CreditSystemStructure.CreditLetter GetCreditInfoByCompanyId(int companyId)
        {
            return idal.GetCreditInfoByCompanyId(companyId);
        }

        /// <summary>
        /// 根据指定的服务承诺书编号获取服务承诺书信息
        /// </summary>
        /// <param name="creditId">服务承诺书编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CreditSystemStructure.CreditLetter GetCreditInfoByCreditId(string creditId)
        {
            return idal.GetCreditInfoByCreditId(creditId);
        }

        /// <summary>
        /// 根据指定条件分页获取服务承诺书信息
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="unionId">联盟编号</param>
        /// <param name="provinceId">省份编号 设置成0时不做为查询条件</param>
        /// <param name="siteId">分站编号 设置成0时不做为查询条件</param>
        /// <param name="state">服务承诺书状态 0:未审核 1:通过审核 2:未通过审核 3:失效 设置成区间[0,3]以外的值时不做为查询条件</param>
        /// <param name="companyName">公司名称 设置成string.Empty或null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CreditSystemStructure.CreditLetter> GetCredits(int pageSize, int pageIndex, ref int recordCount, int unionId,int provinceId, int siteId, int state, string companyName)
        {
            return idal.GetCredits(pageSize, pageIndex, ref recordCount, unionId,provinceId, siteId, state, companyName);
        }

        /// <summary>
        /// 根据指定的承诺书编号、承诺书状态更新承诺书状态
        /// </summary>
        /// <param name="state">状态 0:未审核 1:通过审核 2:未通过审核 3:失效</param>
        /// <param name="creditId">服务承诺书编号</param>        
        /// <returns></returns>
        public bool SetCreditState(int state, string creditId)
        {
            bool setResult = idal.SetCreditState(state, DateTime.Now, creditId);

            if (setResult)
            {
                idal.SetCompnayInfoIsAssure(state == 1 ? true : false, creditId);
            }

            return setResult;
        }

        /// <summary>
        /// 根据指定的公司编号、承诺书状态更新承诺书状态
        /// </summary>
        /// <param name="state">状态 0:未审核 1:通过审核 2:未通过审核 3:失效</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool SetCreditState(int state, int companyId)
        {
            bool setResult=idal.SetCreditState(state, DateTime.Now, companyId);

            if (setResult)
            {
                idal.SetCompnayInfoIsAssure(state == 1 ? true : false, companyId);
            }

            return setResult;
        }

        /// <summary>
        /// 根据指定的承诺书编号删除承诺书信息
        /// </summary>
        /// <param name="creditId">承诺书编号</param>
        /// <returns></returns>
        public bool Delete(string creditId)
        {
            idal.SetCompnayInfoIsAssure(false, creditId);
            return idal.Delete(creditId);
        }

    }
}
