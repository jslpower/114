using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Model.SystemStructure;
using System.Transactions;

namespace EyouSoft.BLL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-03
    /// 描述：高级网店基本信息业务逻辑层
    /// </summary>
    public class HighShopList : IBLL.ShopStructure.IHighShopList
    {
        private readonly IDAL.ShopStructure.IHighShopList dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IHighShopList>();
        private readonly IDAL.SystemStructure.ISysCompanyDomain domaindal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysCompanyDomain>();

        #region CreateInstance
        /// <summary>
        /// 创建高级网店基本信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static IBLL.ShopStructure.IHighShopList CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IHighShopList op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IHighShopList>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">高级网店基本信息实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.ShopStructure.HighShopList model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 后台获取高级网店列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当期页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>高级网店列表集合</returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopList> GetList(int pageSize, int pageIndex, ref int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获取高级网店实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>高级网店实体</returns>
        public EyouSoft.Model.ShopStructure.HighShopList GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="OperatorID">操作员ID</param>
        /// <param name="ExpireTime">网店到期时间</param>
        /// <returns>false:失败 true:成功</returns>
        public bool CheckInfo(string ID, bool IsCheck, string OperatorID, DateTime ExpireTime)
        {
            bool returnVal = false;
            using (TransactionScope AddTran = new TransactionScope())
            {
                if (dal.CheckInfo(ID, IsCheck, OperatorID, ExpireTime))
                {
                    EyouSoft.Model.ShopStructure.HighShopList model = GetModel(ID);
                    if (domaindal.IsExist(model.ApplyDomainName))
                    {
                        domaindal.SetDisabled(model.CompanyID, DomainType.网店域名, IsCheck);
                    }
                    else
                    {
                        SysCompanyDomain DomainModel = new SysCompanyDomain();
                        DomainModel.CompanyId = model.CompanyID;
                        DomainModel.CompanyName = model.CompanyName;
                        DomainModel.CompanyType = EyouSoft.Model.CompanyStructure.CompanyType.专线;
                        DomainModel.Domain = model.ApplyDomainName;
                        DomainModel.DomainType = DomainType.网店域名;
                        DomainModel.GoToUrl = "";
                        EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().AddSysCompanyDomain(DomainModel);
                    }
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
                AddTran.Complete();
                return returnVal;
            }
        }
        /// <summary>
        /// 修改高级网店基本信息
        /// </summary>
        /// <param name="model">高级网店实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool UpdateBasicInfo(EyouSoft.Model.ShopStructure.HighShopList model)
        {
            if (model == null)
                return false;
            return dal.UpdateBasicInfo(model);
        }
        /// <summary>
        /// 获取快到期的高级网店
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ProvinceId">所在省份ID >0返回指定省份 =0返回全部</param>
        /// <param name="CityId">所在城市ID >0返回指定城市 =0返回全部</param>
        /// <param name="CompanyName">公司名称 模糊匹配</param>
        /// <param name="StartExpireDate">到期日期起始值 不需要该参数时传入null</param>
        /// <param name="EndExpireDate">到期时间结束值 不需要该参数时传入null</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ShopStructure.HighShopList> GetExpireList(int pageSize, int pageIndex, ref int recordCount, int ProvinceId, int CityId, string CompanyName, DateTime? StartExpireDate, DateTime? EndExpireDate)
        {
            return dal.GetExpireList(pageSize, pageIndex, ref recordCount, ProvinceId, CityId, CompanyName, StartExpireDate, EndExpireDate);
        }
        #endregion
    }
}
