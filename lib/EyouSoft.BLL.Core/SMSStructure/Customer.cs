using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SMSStructure
{
    /// <summary>
    /// 短信中心-客户信息及客户类型业务逻辑类
    /// </summary>
    /// Author:汪奇志 2010-06-10
    /// 增加GetMobiles函数 ，根据条件获取接收短信号--李焕超--2010-12-17
    public class Customer:EyouSoft.IBLL.SMSStructure.ICustomer
    {
        private readonly IDAL.SMSStructure.ICustomer dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.SMSStructure.ICustomer>();

        #region CreateInstance
        /// <summary>
        /// 创建短信中心-客户信息及客户类型业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SMSStructure.ICustomer CreateInstance()
        {
            EyouSoft.IBLL.SMSStructure.ICustomer op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SMSStructure.ICustomer>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 插入客户类型信息,返回0插入失败,>0时为插入的类型编号
        /// </summary>
        /// <param name="categoryInfo">客户类型业务实体</param>
        /// <returns></returns>
        public int InsertCategory(EyouSoft.Model.SMSStructure.CustomerCategoryInfo categoryInfo)
        {
            return dal.InsertCategory(categoryInfo);
        }

        /// <summary>
        /// 删除客户类型信息
        /// </summary>
        /// <param name="CategoryId">类型编号</param>
        /// <returns></returns>
        public bool DeleteCategory(int CategoryId)
        {
            return dal.DeleteCategory(CategoryId);
        }

        /// <summary>
        /// 根据指定的公司编号获取公司的所有客户类型信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.CustomerCategoryInfo> GetCategorys(string companyId)
        {
            return dal.GetCategorys(companyId);
        }

        /// <summary>
        /// 插入客户信息
        /// </summary>
        /// <param name="customerInfo">客户信息业务实体</param>
        /// <returns>返回处理结果</returns>
        public bool InsertCustomer(EyouSoft.Model.SMSStructure.CustomerInfo customerInfo)
        {
            if (dal.IsExistsCustomerMobile(customerInfo.CompanyId, customerInfo.Mobile, null))
            {
                return false;
            }

            customerInfo.CustomerId = Guid.NewGuid().ToString();
            return dal.InsertCustomer(customerInfo);
        }

        /// <summary>
        /// 根据指定条件获取客户信息
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="customerCompanyName">客户公司名称 为空时不做为查询条件</param>
        /// <param name="customerUserFullName">客户联系人姓名 为空时不做为查询条件</param>
        /// <param name="mobile">手机号码 为空时不做为查询条件</param>
        /// <param name="categoryId">客户类型编号 -1时不做为查询条件</param>
        /// <param name="source">客户来源</param>
        /// <param name="provinceId">省份ID</param>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.CustomerInfo> GetCustomers(int pageSize, int pageIndex, ref int recordCount, string companyId, string customerCompanyName, string customerUserFullName, string mobile, int categoryId, EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource source, int provinceId, int cityId)
        {
            return dal.GetCustomers(pageSize, pageIndex, ref recordCount, companyId, customerCompanyName, customerUserFullName, mobile, categoryId, source, provinceId, cityId);
        }

        /// <summary>
        /// 获取客户总数
        /// </summary>
        /// <returns></returns>
        public int GetCustomersCount()
        {
            return dal.GetCustomersCount();
     
        }

        /// <summary>
        /// 根据指定条件获取接收短信号信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mobile">手机号码 为空时不做为查询条件</param>
        /// <param name="categoryId">客户类型编号 -1时不做为查询条件</param>
        /// <param name="source">客户来源</param>
        /// <param name="provinceId">省份ID</param>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public  IList<EyouSoft.Model.SMSStructure.AcceptMobileInfo> GetMobiles(string companyId, string mobile, int categoryId, EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource source, int provinceId, int cityId)
        {
          return dal.GetMobiles(companyId,mobile,categoryId,source,provinceId,cityId);
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="customerId">客户编号</param>
        /// <returns></returns>
        public bool DeleteCustomer(string customerId)
        {
            return dal.DeleteCustomer(customerId);
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="customerId">客户编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SMSStructure.CustomerInfo GetCustomerInfo(string customerId)
        {
            return dal.GetCustomerInfo(customerId);
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="customerInfo">客户信息业务实体</param>
        /// <returns>返回处理结果</returns>
        public bool UpdateCustomer(EyouSoft.Model.SMSStructure.CustomerInfo customerInfo)
        {
            if (dal.IsExistsCustomerMobile(customerInfo.CompanyId, customerInfo.Mobile, customerInfo.CustomerId))
            {
                return false;
            }

            return dal.UpdateCustomer(customerInfo);
        }

        /// <summary>
        /// 判断客户手机号码是否存在
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mobile">客户号码</param>
        /// <returns></returns>
        public bool IsExistsCustomerMobile(string companyId, string mobile)
        {
            return this.IsExistsCustomerMobile(companyId, mobile, null);
        }

        /// <summary>
        /// 判断客户手机号码是否存在
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mobile">客户号码</param>
        /// <param name="customerId">客户编号</param>
        /// <returns></returns>
        public bool IsExistsCustomerMobile(string companyId, string mobile, string customerId)
        {
            return dal.IsExistsCustomerMobile(companyId, mobile, customerId);
        }
        #endregion
    }
}
