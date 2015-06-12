using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TourUnion.WEB.IM.Card
{
    /// <summary>
    /// 修改个人信息
    /// </summary>
    public partial class UpdateUserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TourUnion.Account.Model.Login.LoginResultInfo resultinfo = TourUnion.WEB.IM.LoginWeb.LoginWebMQPublic(true, false, TourUnion.Account.Enum.SystemMedia.Web);
                //if (resultinfo != null)
                //{
                //    bool isTrue = resultinfo.IsSucceed;  //登录成功      
                //    string gotoUrl = "";

                //    if (isTrue)  //登录成功
                //    {
                //        switch (resultinfo.LoginUserInfo.CompanyTypeId)
                //        {
                //            case 1:  //批发商
                //                gotoUrl = "/RouteAgency/DatumSetup/MemberUserSet.aspx";
                //                break;
                //            case 2:  //零售商
                //                if (resultinfo.LoginUserInfo.IsAdmin == true)//总帐号
                //                {
                //                    gotoUrl = "/TourAgency/Organise/OrganiseMessage.aspx";
                //                }
                //                else //子帐号
                //                {
                //                    gotoUrl = "/TourAgency/Organise/UserInfo.aspx";
                //                }
                //                break;
                //            case 3:  //地接社
                //                gotoUrl = "/LocalAgency/SystemManage/IndividualInformation.aspx";
                //                break;
                //            default://供应商
                //                gotoUrl = "http://www.tongye114.com";
                //                break;
                //        }
                //        Response.Redirect(gotoUrl, true);
                //    }
                //    else
                //    {

                //        Response.Write(resultinfo.ErrorInfo.ToString());
                //        Response.End();
                //    }
                //}
                //resultinfo = null;
                
            }
        }
    }
}
