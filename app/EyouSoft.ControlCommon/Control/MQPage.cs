using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.SSOComponent.Entity;
using EyouSoft.Common;
using EyouSoft.Security.Membership;
using System.Web.UI.HtmlControls;

namespace EyouSoft.ControlCommon.Control
{
    /// <summary>
    /// MQ内嵌页面的基类
    /// </summary>
    public class MQPage:System.Web.UI.Page
    {
        public string GetDesPlatformUrl(string desUrl)
        {
            return Utils.GetDesPlatformUrlForMQMsg(desUrl, MQLoginId.ToString(), Password.ToString());
        }

        private int _MQLoginId;
        /// <summary>
        /// 当前MQ号
        /// </summary>
        protected int MQLoginId
        {
            get
            {
                return _MQLoginId;
            }
        }

        private string _password;
        /// <summary>
        /// 当前MQ用户MD5密码
        /// </summary>
        protected string Password
        {
            get
            {
                return _password;
            }
        }

        private bool isLogin = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return isLogin;
            }
        }

        private bool _IsCompanyCheck = false;
        /// <summary>
        /// 公司是否通过审核
        /// </summary>
        public bool IsCompanyCheck
        {
            get
            {
                return _IsCompanyCheck;
            }
        }

        private string imageServerUrl;

        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string ImageServerUrl
        {
            get { return imageServerUrl; }
        }

        private UserInfo _userInfo = null;

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public UserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
        }

        private int _cityId;

        /// <summary>
        /// 当前销售城市
        /// </summary>
        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        private EyouSoft.Model.SystemStructure.SysCity _cityModel = null;

        /// <summary>
        /// 当前销售城市model
        /// </summary>
        public EyouSoft.Model.SystemStructure.SysCity CityModel
        {
            get { return _cityModel; }
            set { _cityModel = value; }
        }
        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="permissionId">权限ID</param>
        /// <returns></returns>
        public bool CheckGrant(params TravelPermission[] permissionIds)
        {
            if (this.SiteUserInfo != null)
            {
                bool isCheck = true;
                for (int i = 0; i < permissionIds.Length; i++)
                {
                    if (SiteUserInfo.PermissionList.Contains((int)permissionIds[i]) == false)
                    {
                        isCheck = false;
                        break;
                    }
                }
                return isCheck;
            }
            else
            {
                return false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           //屏蔽右键
           Page.ClientScript.RegisterClientScriptBlock(this.GetType(),Guid.NewGuid().ToString(),"<script language='JavaScript'>function click(e) { if (document.all) { if (event.button==2||event.button==3) { oncontextmenu='return false'; } } }document.onmousedown=click; document.oncontextmenu = new Function('return false;'); </script>");
         
           
       
            //AddMetaContentType();
            //AddMetaIE8Compatible();
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            imageServerUrl = ImageManage.GetImagerServerUrl(1);

            //初始化当前MQ用户
            _MQLoginId = Utils.GetInt(Request.QueryString[Utils.MQLoginIdKey]);
            _password = Utils.GetQueryStringValue(Utils.MQPwKey);
            if ( _MQLoginId >0  && !String.IsNullOrEmpty(_password))
            {
                _userInfo = new UserProvider().MQLogin(_MQLoginId, _password);
                UserProvider.GenerateIMFrameUserLoginCookies(UserProvider.GenerateSSOToken(_MQLoginId.ToString()), _MQLoginId.ToString());
            }
            else
            {
                _userInfo = new UserProvider().GetMQUser();
            }

            isLogin = _userInfo != null ? true : false;

            if (!isLogin)
            {
                Response.Clear();
                Response.Write("用户名或者密码失效，请重新点击右边的菜单按钮！");
                Response.End();
            }

            _MQLoginId = Convert.ToInt32(_userInfo.ContactInfo.MQ);
            _password = _userInfo.PassWordInfo.MD5Password;


            _IsCompanyCheck = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(_userInfo.CompanyID).IsCheck;

            //_password = "0210cabb07a77e9902382812ae3047b3";
            //_MQLoginId = 6709;
            //UserInfo userInfo = null;
            ////isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out userInfo);
            //isLogin = true;
            ///* UserId:tongye114 */
            //string UserId = "751ae431-8117-4fc3-b9c9-6c4977b57d45";
            ///* UserId :liuyl_test */
            /////UserId = "3f11ad31-4596-403b-b4a8-05f8fdd9dd31";
            ///*test */
            ////UserId = "BC2BAF09-694D-45E0-A5E3-3DDF1E4E2398";
            //Model.CompanyStructure.CompanyUser user = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(UserId);
            //_userInfo = new UserInfo()
            //{
            //    CityId = 1,
            //    CompanyID = user.CompanyID,
            //    UserName = user.UserName,
            //    DepartId = "1",
            //    ProvinceId = 1,
            //    DepartName = "432fds",
            //    IsAdmin = true,
            //    ID = UserId,
            //};
            ////_userInfo.ContactInfo = user.ContactInfo;
            //_userInfo.ContactInfo.ContactName = user.ContactInfo.ContactName;
            //_userInfo.ContactInfo.Email = user.ContactInfo.Email;
            //_userInfo.ContactInfo.MQ = user.ContactInfo.MQ;
            //_userInfo.ContactInfo.QQ = user.ContactInfo.QQ;

            //_IsCompanyCheck = true;
            ////if (userInfo != null)
            ////{
            ////    _userInfo = userInfo;
            ////    _IsCompanyCheck = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(_userInfo.CompanyID).IsCheck;
            ////    if (userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队)
            ////        || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.购物店)
            ////        || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区)
            ////        || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店)
            ////        || userInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
            ////    {
            ////        _IsSupplyUser = true;
            ////    }
            ////    else
            ////    {
            ////        _IsSupplyUser = false;
            ////    }
            ////}
            ////Model.CompanyStructure.CompanyRole r = new EyouSoft.Model.CompanyStructure.CompanyRole();
            ////r.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
            ////r.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.组团);
            //_userInfo.CompanyRole.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
            //_userInfo.CompanyRole.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.组团);
            //_userInfo.CompanyRole.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.地接);
            //_userInfo.AreaId = new int[6] { 1, 2, 3,4,5,241 };
            ////,7,8,9,1,2,3,4,5,6,10,11,12,13,14,15,16,17,18,19,20,21,22,23,2
            //_userInfo.PermissionList = new int[24] { 7, 8, 9, 1, 2, 3, 4, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            ////_userInfo.AreaId = new int[user.Area.Count] { };
            ////string[] arr = user.are.Split(',');
            ////for (int i = 0; i < user.Area.Count; i++)
            ////{
            ////    _userInfo.AreaId[i] = user.Area[i].AreaId;
            ////}

            #region 更新cookie  修改人： 杜桂云
            CityId = Utils.GetInt(Request.QueryString["CityId"]);
            if (CityId > 0 && !string.IsNullOrEmpty(Request.QueryString["isCut"]))
            {
                //存cookie 
                if (Response.Cookies["MQCityId"] == null)
                {
                    System.Web.HttpCookie cookie = new System.Web.HttpCookie("MQCityId");
                    cookie.Value = CityId.ToString();
                    cookie.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Add(cookie);
                }
                else //修改
                {
                    Response.Cookies["MQCityId"].Value = CityId.ToString();
                    Response.Cookies["MQCityId"].Expires = DateTime.Now.AddDays(10);
                }
            }
            else
            {
                if (CityId <= 0)
                {
                    //取cookie 
                    if (Utils.GetInt(Response.Cookies["MQCityId"].Value) > 0)
                    {
                        CityId = Utils.GetInt(Response.Cookies["MQCityId"].Value);
                    }
                    else  //
                    {
                        if (SiteUserInfo.CityId > 0)
                        {
                            CityId = SiteUserInfo.CityId;
                        }
                        else//默认值（杭州）
                        {
                            CityId = 362;
                        }
                    }

                }
            }
#endregion

            #region 城市Model

            EyouSoft.Model.SystemStructure.SysCity thisCityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            if (thisCityModel != null)
            {
                CityModel = new EyouSoft.Model.SystemStructure.SysCity();
                CityModel.CityId = CityId;
                CityModel.CityName = thisCityModel.CityName;
                CityModel.ProvinceId = thisCityModel.ProvinceId;
                CityModel.ProvinceName = thisCityModel.ProvinceName;
            }
            else
            {
                CityId = 362;
                EyouSoft.Model.SystemStructure.SysCity hzCityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
                CityModel = new EyouSoft.Model.SystemStructure.SysCity();
                CityModel.CityId = CityId;
                CityModel.CityName = hzCityModel.CityName;
                CityModel.ProvinceId = hzCityModel.ProvinceId;
                CityModel.ProvinceName = hzCityModel.ProvinceName;
                hzCityModel = null;
            }
            thisCityModel = null;
            #endregion 
        }

        /// <summary>
        /// 添加Content-Type Meta标记到页面头部
        /// </summary>
        protected virtual void AddMetaContentType()
        {
            HtmlMeta meta = new HtmlMeta();
            //meta.HttpEquiv = "content-type";
            //meta.Content = Response.ContentType + "; charset=" + Response.ContentEncoding.HeaderName;
            meta.Attributes["charset"] = Response.ContentEncoding.HeaderName;
            Page.Header.Controls.Add(meta);
        }
        /// <summary>
        /// 添加IE8兼容IE7 Meta Tag.
        /// </summary>
        protected virtual void AddMetaIE8Compatible()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.HttpEquiv = "X-UA-Compatible";
            meta.Content = "IE=EmulateIE7";
            Page.Header.Controls.Add(meta);
        }
    }
}
