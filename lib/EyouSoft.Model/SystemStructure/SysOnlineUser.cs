using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 在线用户
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysOnlineUser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysOnlineUser()
        { }

        #region Model

        private string _id;
        private string _userid;
        private string _companyid;
        private DateTime _logintime;

        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 子账号ID
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime
        {
            set { _logintime = value; }
            get { return _logintime; }
        }

        #endregion Model
    }
}
