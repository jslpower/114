using System;
using System.Collections.Generic;

namespace IMFrame.EyouSoft1
{
    /// <summary>
    /// 易游通MQ整合中转页面跳转判断
    /// 创建者：郑知远 2011/05/20
    /// </summary>
    public partial class EyouGoto : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.OpenStructure.MUserInfo> lst = new List<EyouSoft.Model.OpenStructure.MUserInfo>();
            string strPlatformUserId = string.Empty;
            int intSystemUserId = 0;
            bool isEyou = false;

            // 获取平台用户信息
            lst = EyouSoft.BLL.OpenStructure.BUser.CreateInstance().GetMUserList(0, (int)EyouSoft.OpenRelation.Model.SystemType.Platform, SiteUserInfo.ID);

            if (lst != null)
            {
                foreach (var mUserInfo in lst)
                {
                    // 遍历判断该用户是否属于易游通用户
                    if (((int)EyouSoft.OpenRelation.Model.SystemType.YYT).Equals(mUserInfo.SystemType))
                    {
                        // 系统用户编号
                        intSystemUserId = mUserInfo.SystemUserId;

                        // 平台用户编号
                        strPlatformUserId = mUserInfo.PlatformUserId;

                        // 易游通用户
                        isEyou = true;

                        break;
                    }
                }
            }

            // 判断该用户是否属于易游通用户
            if (isEyou)
            {
                // 属于易游通用户
                Response.Redirect(string.Format("EyouSoftFunc.aspx?SystemUserId={0}&PlatformCompanyId={1}", intSystemUserId, strPlatformUserId));
            }
            else
            {
                // 不属于易游通用户
                Response.Redirect("EyouSoft.html");
            }
        }
    }
}
