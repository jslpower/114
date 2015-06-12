using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.Control
{
    /// <summary>
    /// 用户后台页面的父类
    /// 功能：统一添加页面元信息，处理页面标题，处理用户后台页面tab页面与非tab页面的请求
    /// </summary>
    public class BackPage : BasePage
    {
        protected string UrlType
        {
            get
            {
                if (Request.QueryString["urltype"] != null)
                {
                    return Request.QueryString["urltype"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected string RequestSource
        {
            get
            {
                if (Request.QueryString["RequestSource"] != null)
                {
                    return Request.QueryString["RequestSource"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddMetaIE8Compatible();
            //if (UrlType.ToLower() != "tab")
            //{
                //AddMetaContentType();
                
                //AddMetaTag("description", "");
                //AddMetaTag("keywords", "");
                //AddSiteIcon();
            //}
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            string rs = RequestSource.ToLower();
            if (UrlType.ToLower() == "tab")
            {
                if (this.IsLogin)
                {
                    if (IsCompanyCheck == true || rs == "toptab" || rs == "noneedcheck")
                    {
                        this.MasterPageFile = "~/MasterPage/Site1.master";
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("{isCheck:false}");
                        Response.End();
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write("{Islogin:false}");
                    Response.End();
                }
            }
            else
            {
                if (IsLogin == false)
                {
                    EyouSoft.Security.Membership.UserProvider.RedirectLogin();
                    return;
                }
                this.MasterPageFile = "~/MasterPage/Site1.master";
                string url = Request.Url.PathAndQuery.ToLower();
                string s = Server.UrlEncode("page:" + url);
                if (IsTravelUser)//旅行社用户
                {
                    Response.Clear();
                    Response.Write("<script type='text/javascript'>location.href='/Default.aspx#" + s + "';</script><h1><a href='/Default.aspx#" + s + "'>页面会自动跳转到后台系统，如果没有自动跳转，则点此链接进入后台系统</a></h1>");
                    Response.End();
                }
                else if (IsAirTicketSupplyUser)
                {
                    Response.Clear();
                    Response.Write("<script type='text/javascript'>location.href='/TicketsCenter/Default.aspx#" + s + "';</script><h1><a href='/Default.aspx#" + s + "'>页面会自动跳转到后台系统，如果没有自动跳转，则点此链接进入后台系统</a></h1>");
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("<script type='text/javascript'>location.href='/Default.aspx#" + s + "';</script><h1><a href='/Default.aspx#" + s + "'>页面会自动跳转到后台系统，如果没有自动跳转，则点此链接进入后台系统</a></h1>");
                    Response.End();
                }
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            //统一处理网站标题
            //将Web网站名字 加入到页面标题中
            //Page.Title = Page.Title + "_" + ConfigClass.GetConfigString("UsedCar", "WebName");
        }

        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="permissionId">权限ID</param>
        /// <returns></returns>
        //public bool CheckGrant(int permissionId)
        //{
        //    if (this.SiteUserInfo != null)
        //    {
        //        return this.SiteUserInfo.PermissionList.Contains(permissionId);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


    }


}
