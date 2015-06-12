using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// utility
    /// </summary>
    /// Author:汪奇志 2010-06-30
    public class Utility
    {
        /// <summary>
        /// 获取当前用户(运营后台)用户分管的区域范围 多个用","间隔
        /// </summary>
        /// <returns></returns>
        public string GetCurrentWebMasterArea()
        {
            EyouSoft.SSOComponent.Entity.MasterUserInfo userInfo = new UserProvider().GetMaster();
            StringBuilder areas = new StringBuilder();

            if (userInfo != null && userInfo.AreaId.Length > 0)
            {
                areas.Append(userInfo.AreaId[0].ToString());
                for (int i = 1; i < userInfo.AreaId.Length; i++)
                {
                    areas.AppendFormat(",{0}", userInfo.AreaId[i].ToString());
                }
            }
            else
            {
                areas.Append(-1);
            }

            return areas.ToString();
        }

        /// <summary>
        /// 获取当前用户分管的线路区域 多个线路区域用","间隔 管理员用户返回null
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserArea()
        {
            //EyouSoft.SSOComponent.Entity.UserInfo userInfo = new EyouSoft.Security.Membership.UserProvider().GetUser();
            EyouSoft.SSOComponent.Entity.UserInfo userInfo = UserProvider.GetUser();
            StringBuilder areas = new StringBuilder();

            if (userInfo != null && !userInfo.IsAdmin)
            {
                if (userInfo.AreaId.Length < 1) { areas.Append(-1); }

                areas.Append(userInfo.AreaId[0].ToString());
                for (int i = 1; i < userInfo.AreaId.Length; i++)
                {
                    areas.AppendFormat(",{0}", userInfo.AreaId[i].ToString());
                }
            }

            return areas.ToString();
        }

        /// <summary>
        /// 获取当前登录用户的公司编号 未登录返回null
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserCompanyId()
        {
            EyouSoft.SSOComponent.Entity.UserInfo userInfo = UserProvider.GetUser();//new EyouSoft.Security.Membership.UserProvider().GetUser();

            if (userInfo != null)
            {
                return userInfo.CompanyID;
            }

            return null;
        }

    }
}
