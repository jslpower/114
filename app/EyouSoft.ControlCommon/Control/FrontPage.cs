using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EyouSoft.Common.Function;

namespace EyouSoft.Common.Control
{
    /// <summary>
    /// 前台页面的父类
    /// 功能：统一添加页面元信息，处理页面标题
    /// </summary>
    public class FrontPage:BasePage
    {
        private const string COOKIE_KEY_CITY = "Front_CityId";
        private string _imageServerPath;

        protected string ImageServerPath
        {
            get { return _imageServerPath; }
        }

        private int _cityId;

        /// <summary>
        /// 当前销售城市
        /// </summary>
        public int CityId
        {
            get { return _cityId; }
        }
        private EyouSoft.Model.SystemStructure.SysCity _cityModel = null;

        /// <summary>
        /// 当前销售城市model
        /// </summary>
        public EyouSoft.Model.SystemStructure.SysCity CityModel
        {
            get { return _cityModel; }
        }

        private bool _IsSeniorShopPage = false;
        /// <summary>
        /// 当前页面是否属于高级网店页面
        /// </summary>
        public bool IsSeniorShopPage
        {
            get
            {
                return _IsSeniorShopPage;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddMetaContentType();
            AddMetaIE8Compatible();
            //AddMetaTag("description", "");
            //AddMetaTag("keywords", "");
            //add Head Cache Contorl
            //AddSiteIcon();
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region 初始化_IsSeniorShopPage属性     
            /*if (Request.CurrentExecutionFilePath.ToLower().IndexOf("/seniorshop/") != -1)
            {
                _IsSeniorShopPage = true;
            }*/
            this._IsSeniorShopPage = Utils.IsEShopPage();
            #endregion 

            #region 取销售城市
            _cityId = Utils.GetInt(Request.QueryString["CityId"]);
            int isCut = Utils.GetInt(Request.QueryString["isCut"]);
            HttpCookie cok = Request.Cookies[COOKIE_KEY_CITY];
            if (_cityId > 0 && isCut == 1)
            {
                //string domainSuffix = Utils.GetDomainSuffix(Utils.AbsoluteWebRoot);
                //if (!String.IsNullOrEmpty(domainSuffix))
                //{
                //    response.Cookies[LoginCookie_UserName].Domain = domainSuffix;
                //}
                //存cookie 
                if (cok==null)
                {
                    cok = new System.Web.HttpCookie(COOKIE_KEY_CITY);
                    cok.Value = _cityId.ToString();
                    cok.Expires = DateTime.Now.AddDays(10);
                    //cok.Domain = ".tongye114.com";
                    Response.Cookies.Add(cok);
                }
                else //修改
                {
                    Response.Cookies.Remove(COOKIE_KEY_CITY);
                    Response.Cookies[COOKIE_KEY_CITY].Expires = DateTime.Now.AddDays(-1);
                    cok = new HttpCookie(COOKIE_KEY_CITY);
                    cok.Value = _cityId.ToString();
                    cok.Expires = DateTime.Now.AddDays(10);
                    //cok.Domain = ".tongye114.com";
                    Response.Cookies.Add(cok);
                }
            }
            else
            {
                if (_cityId <= 0)
                {
                    //取cookie
                    int cokV = Utils.GetInt(cok != null ? cok.Value : "");
                    if (cokV > 0)
                    {
                        _cityId = cokV;
                    }
                    else  //IP
                    {
                        string Remote_IP = StringValidate.GetRemoteIP();

                        EyouSoft.Model.SystemStructure.CityBase cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetClientCityByIp(Remote_IP);
                        if (cityModel != null && cityModel.CityId>0)
                        {
                            _cityId = cityModel.CityId;
                        }
                        else//默认值（杭州）
                        {
                            //_cityId = 362;
                            _cityId = 0;
                        }
                        cityModel = null;
                    }

                }
            }
            #endregion 
            _imageServerPath = ImageManage.GetImagerServerUrl(1);

            //如果取不到城市信息，则跳转到 切换城市页面
            if (_cityId == 0)
            {
                if (IsNeedLinkToCutCityPage()==true)
                {
                    Response.Redirect(Domain.UserPublicCenter + "/ToCutCity.aspx", true);
                }
            }
            #region 城市Model

            //如果取不到城市信息，则跳转到 切换城市页面
            EyouSoft.Model.SystemStructure.SysCity thisCityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(_cityId);
            if (thisCityModel != null)//找到
            {
                _cityModel = new EyouSoft.Model.SystemStructure.SysCity();
                _cityModel = thisCityModel;
            }
            else {//没有找到
                //_cityId = 362;
                //EyouSoft.Model.SystemStructure.SysCity hzCityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
                //_cityModel = new EyouSoft.Model.SystemStructure.SysCity();
                //_cityModel = hzCityModel;
                //hzCityModel = null;
                //判断 是否需要 跳转到 选择城市 页面
                if (IsNeedLinkToCutCityPage() == true)//需要
                {
                    Response.Redirect(Domain.UserPublicCenter + "/ToCutCity.aspx", true);
                }
                else//不需要
                {
                    //如果找不到城市信息，则默认初始化 为 杭州
                    _cityId = 362;
                    _cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(_cityId);
                }
            }

            
           

             thisCityModel = null;
            #endregion 
           
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            //统一处理网站标题
            //将Web网站名字 加入到页面标题中
            //Page.Title = Page.Title + "_同业114旅游交易平台";
            //判断当前URL类型
            if (HttpContext.Current.Request.Url.HostNameType == UriHostNameType.Dns)//是DNS类型
            {
                string currentHost = HttpContext.Current.Request.ServerVariables["Http_Host"].ToLower();//当前请求HOST
                //判断请求类型 是否是本地测试
                if (!currentHost.Contains("localhost"))//不包含localhost
                {
                    if (currentHost.IndexOf("tongye114.com") != -1)//包含tongye114.com,线上发布环境
                    {
                        //判断是否是高级网店请求
                        //if (/*Request.Url.AbsolutePath.ToLower().IndexOf("/seniorshop/") == -1*/ _IsSeniorShopPage==false)//不是高级网店请求
                        //{
                        //    //输出跨域处理JS
                        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>document.domain='tongye114.com';</script>");
                        //}
                        if (IsNeedResponseCrossDomainScript() == true)
                        {
                            //输出跨域处理JS
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>document.domain='tongye114.com';</script>");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当未找到城市信息的时候，判断当前页面是否需要切换到 城市切换页面。
        /// </summary>
        /// <returns></returns>
        private bool IsNeedLinkToCutCityPage()
        {
            string url = Request.Url.AbsolutePath.ToLower();

            return !_IsSeniorShopPage && !Utils.NoNeedLinkToCutCityPage_URLS.Contains(url);
        }

        /// <summary>
        /// 判断是否需要在当前页面输出 跨域脚本
        /// </summary>
        /// <returns></returns>
        private bool IsNeedResponseCrossDomainScript()
        {
            string url = Request.Url.AbsolutePath.ToLower();

            //判断页面请求属于什么模块
            if (_IsSeniorShopPage == true)//是高级网店
            {
                //不用输出
                return false;
            }
            //判断页面 是否在 不需要输出跨域脚本的页面集合中
            if (Utils.NoNeedResponseCrossDomainScript_URLS.Contains(url) == true)//在
            {
                //不用输出
                return false;
            }

            return true;
        }
    }
}
