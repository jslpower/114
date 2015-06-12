using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class PostToMiddlewareDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.OpenRelation.Model.MUserInfo userInfo = new EyouSoft.OpenRelation.Model.MUserInfo()
            {
                Email = "wangqz@enowinfo.com",
                Fax = "057188888888",
                Gender = EyouSoft.OpenRelation.Model.Gender.L,
                Mobile = "15868456678",
                MSN = "wangqizhi@live.com",
                Password = "000000",
                PlatformUserId = string.Empty,
                PlatformCompanyId = string.Empty,
                QQ = "1378024159",
                RealName = "汪奇志",
                SystemType = EyouSoft.OpenRelation.Model.SystemType.YYT,
                SystemUserId = 40894,
                Telephone = "057156884616",
                UserName = "40895",
                SystemCompanyId = 1125,
                SystemCompanyType = EyouSoft.OpenRelation.Model.SystemCompanyType.ZX
            };

            EyouSoft.OpenRelation.Model.MRequestInfo requestInfo = new EyouSoft.OpenRelation.Model.MRequestInfo();
            requestInfo.AppKey = EyouSoft.OpenRelation.Utils.GetAppKey();
            requestInfo.InstructionCode = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MUserInfo>(userInfo);
            requestInfo.InstructionType = EyouSoft.OpenRelation.Model.InstructionType.UpdateUser;
            requestInfo.RequestSystemType = EyouSoft.OpenRelation.Model.SystemType.YYT;
            requestInfo.RequestUriString = EyouSoft.OpenRelation.Utils.GetMiddlewareURI();

            this.request.Value = EyouSoft.OpenRelation.Utils.ConvertJSON<EyouSoft.OpenRelation.Model.MRequestInfo>(requestInfo);
        }
    }
}
