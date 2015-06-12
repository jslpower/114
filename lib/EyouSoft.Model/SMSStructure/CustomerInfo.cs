using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    /// <summary>
    /// 客户信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public class CustomerInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CustomerInfo() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="customerId">客户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="customerCompanyName">客户单位名称</param>
        /// <param name="customerContactName">客户联系人</param>
        /// <param name="categoryId">客户类型编号</param>
        /// <param name="categoryName">客户类型名称</param>
        /// <param name="remark">备注信息</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="issueTime">创建时间</param>
        /// <param name="provinceId">省份ID</param>
        /// <param name="cityId">城市ID</param>
        public CustomerInfo(string customerId, string companyId, string userId, string customerCompanyName
            , string customerContactName, int categoryId, string categoryName
            , string remark, string mobile, DateTime issueTime, int provinceId, int cityId)
        {
            this.CustomerId = customerId;
            this.CompanyId = companyId;
            this.UserId = userId;
            this.CustomerCompanyName = customerCompanyName;
            this.CustomerContactName = customerContactName;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.Remark = remark;
            this.Mobile = mobile;
            this.IssueTime = issueTime;
            this.ProvinceId = provinceId;
            this.CityId = cityId;
        }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CustomerCompanyName { get; set; }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string CustomerContactName { get; set; }
        /// <summary>
        /// 客户类型编号
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 客户类型名称
        /// </summary>
        private string _CategoryName;
        /// <summary>
        /// 客户类型名称
        /// </summary>
        public string CategoryName
        {
            get
            {
                if (CategoryId == 0)
                { return ""; }
                else 
                {
                    string name = "";
                    try
                    {
                        name = ((EyouSoft.Model.CompanyStructure.CompanyType)CategoryId).ToString();
                    }
                    catch { }
                    return name;
                }
            }

            set { this._CategoryName = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 组团社大平台公司ID
        /// </summary>
        public string RetailersCompanyId { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }


        #region 客户来源枚举
        public enum CustomerSource
        { 
            /// <summary>
            /// 所有客户
            /// </summary>
            所有客户,

            /// <summary>
            /// 我的客户
            /// </summary>
            我的客户,

            /// <summary>
            /// 平台组团社客户
            /// </summary>
            平台组团社客户
        }
        #endregion
    }
}
