using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.SSOComponent.Entity
{
    /// <summary>
    /// 管理员信息
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-5-31
    [Serializable]
    public class MasterUserInfo
    {
        #region Model
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get;
            set; 
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set; 
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ContactName
        {
            get;
            set; 
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            get;
            set; 
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string ContactFax
        {
            get;
            set; 
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string ContactMobile
        {
            get;
            set; 
        }

        /// <summary>
        /// 角色权限列表
        /// </summary>
        public int[] PermissionList
        {
            get;
            set; 
        }

        /// <summary>
        /// 是否停用 0：启用；1：停用  默认为0
        /// </summary>
        public bool IsDisable
        {
            get; set; 
        }

        /// <summary>
        /// 是否管理员，默认为否
        /// </summary>
        public bool IsAdmin
        {
            get;
            set;
        }

        /// <summary>
        /// 系统用户区域ID
        /// </summary>
        public int[] AreaId
        {
            get; set;
        }

        /// <summary>
        /// 系统用户所能查看的易诺用户池客户类型
        /// </summary>
        public IList<int> CustomerTypeIds { get; set; }
        
        /// <summary>
        /// 登录凭据值
        /// </summary>
        public string LoginTicket { get; set; }

        #endregion Model
    }
}
