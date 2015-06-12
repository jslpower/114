using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;

namespace TourUnion.WEB.IM.Search
{
    /// <summary>
    /// jhf 2009-9-10 搜索
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        protected int SiteId = 0;
        protected int CityId = 0;

        protected int MQId = 0;
        protected string MD5Password = "";
        protected string parsUserName = "";
        protected string parsMD5Password = "";
        protected string lineurl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    TourUnion.Account.Model.Login.LoginResultInfo resultinfo = TourUnion.WEB.IM.LoginWeb.LoginWebMQPublic(true, false, TourUnion.Account.Enum.SystemMedia.MQ);

            //    bool isTrue = resultinfo.IsSucceed;  //登录成功

            //    if (isTrue)
            //    {
            //        lineurl = TourUnion.WEB.ProceFlow.LoginManage.GetAgencyAttentionTourUrl();
            //        TourUnion.Account.Model.Account operatorModel = resultinfo.LoginUserInfo;
            //        MQId = operatorModel.ContactMQ;
            //        MD5Password = operatorModel.MD5Password;
            //        operatorModel = null;

            //        TourUnion.Account.Model.MQUrlPar mqurlpar = TourUnion.Account.Model.UserManage.MQUrlParModel;
            //        parsUserName = mqurlpar.IDMyUrlName;
            //        parsMD5Password = mqurlpar.PwMyUrlName;
            //        mqurlpar = null;

            //        TourUnion.WEB.ProceFlow.SiteInfo site = TourUnion.WEB.ProceFlow.SiteManage.GetIMSiteInfoByIp();
            //        if (site != null)
            //        {
            //            SiteId = site.SiteId;
            //            CityId = site.Cityid;
            //        }
            //        site = null;
            //        resultinfo = null;
            //    }
            //    else
            //    {
            //        string url = resultinfo.ErrorInfo.ToString();
            //        resultinfo = null;
            //        Response.Write(url);
            //        Response.End();
            //    }
            //}
            
        }
    }
}
