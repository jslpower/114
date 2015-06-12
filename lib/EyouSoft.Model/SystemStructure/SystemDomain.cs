using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统域名
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SystemDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemDomain()
        { }

        #region Model

        private int _id;
        private int _systemid;
        private string _domainname;
        private int _domaintype;
        private int _companyid;
        private string _gotourl;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId
        {
            set { _systemid = value; }
            get { return _systemid; }
        }
        /// <summary>
        /// 系统域名
        /// </summary>
        public string DomainName
        {
            set { _domainname = value; }
            get { return _domainname; }
        }
        /// <summary>
        /// 域名类型
        /// </summary>
        public int DomainType
        {
            set { _domaintype = value; }
            get { return _domaintype; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string GoToUrl
        {
            set { _gotourl = value; }
            get { return _gotourl; }
        }

        #endregion Model
    }
}
