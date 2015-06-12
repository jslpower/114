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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (UrlType.ToLower() != "tab")
            {
                //AddMetaContentType();
                //AddMetaIE8Compatible();
                //AddMetaTag("description", "");
                //AddMetaTag("keywords", "");
                //AddSiteIcon();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (UrlType.ToLower() == "tab")
            {

                if (this.IsLogin)
                {
                    this.MasterPageFile = "~/MasterPage/Site1.master";
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
                this.MasterPageFile = "~/MasterPage/Site1.master";
                string url = Request.Url.AbsolutePath.ToLower();
                string s = Server.UrlEncode("page:" + url);
                //Response.Redirect(,true);
                //Response.Redirect("/Default.aspx#page%3A%2Fpage%2Fpage2.aspx");
                Response.Clear();
                Response.Write("<script type='text/javascript'>location.href='/Default.aspx#" + s + "';</script><h1><a href='/Default.aspx#" + s + "'>页面会自动跳转到后台系统，如果没有自动跳转，则点此链接进入后台系统</a></h1>");
                Response.End();
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
        /// 输出结果
        /// </summary>
        /// <param name="isOk"></param>
        /// <param name="msg"></param>
        protected void ResponseMeg(bool isOk, string msg)
        {
            Response.Clear();
            if (isOk)
                Response.Write("{success:'1',message:'" + msg + "'}");
            else
                Response.Write("{success:'0',message:'" + msg + "'}");
            Response.End();
        }
        protected void ResponseMegSuccess()
        {
            Response.Clear();
            Response.Write("{success:'1',message:'操作完成'}");
            Response.End();
        }
        protected void ResponseMegNoComplete()
        {
            Response.Clear();
            Response.Write("{success:'0',message:'请填写完整'}");
            Response.End();
        }
        protected void ResponseMegError()
        {
            Response.Clear();
            Response.Write("{success:'0',message:'操作失败'}");
            Response.End();
        }
    }
}
