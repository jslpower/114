using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.SSOComponent.Entity;
using EyouSoft.Security.Membership;

namespace EyouSoft.Common.Control
{
    /// <summary>
    /// 页面属性：运营后台页面基类
    /// 功能：统一处理页面共性，处理用户身份，存储基本的用户信息，页面元信息的处理方法集合
    /// 创建人：杜桂云　　　创建时间：２０１０-０６-２４
    /// </summary>
    public class YunYingPage:System.Web.UI.Page
    {

        private string imageServerUrl;

        public string ImageServerUrl
        {
            get { return imageServerUrl; }

        }

        private bool _isLogin = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _isLogin ;
            }
        }

        private MasterUserInfo _masterUserInfo=null;
        public MasterUserInfo MasterUserInfo
        {
            get
            {
                return _masterUserInfo;
            }
        }

        /// <summary>
        /// 验证管理员权限。
        /// 注：验证权限列表时，权限列表中的一个权限不通过，即返回False.
        /// </summary>
        /// <param name="PermissionID">权限列表</param>
        /// <returns></returns>
        public bool CheckMasterGrant(params YuYingPermission[] permissions)
        {
            if (_masterUserInfo != null)
            {
                bool isCheck = true;
                for (int i = 0; i < permissions.Length; i++)
                {
                    if (_masterUserInfo.PermissionList == null)
                    {
                        _masterUserInfo.PermissionList = new int[] { };
                    }
                    if (_masterUserInfo.PermissionList.Contains((int)permissions[i]) == false)
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

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            _masterUserInfo = new UserProvider().GetMaster();

            if (_masterUserInfo != null)
            {
                if (_masterUserInfo.AreaId == null)
                {
                    _masterUserInfo.AreaId =new int[] { };
                }
            }

            _isLogin = _masterUserInfo != null ? true : false;

            if (_isLogin == false)
            {
                string isAjax = Request.QueryString["isajax"];
                if (!string.IsNullOrEmpty(isAjax))
                {
                    if (isAjax == "yes")
                    {
                        Response.Clear();
                        Response.Write("notLogin");
                        Response.End();
                    }
                    else
                    {
                        UserProvider.RedirectLoginOpenTopPageYunYing();
                    }
                }
                else
                {
                    UserProvider.RedirectLoginOpenTopPageYunYing();
                }
            }

            imageServerUrl = ImageManage.GetImagerServerUrl(1);

            //_masterUserInfo = new MasterUserInfo()
            //{
            //    UserName="tongye114",
            //    ContactFax = "0571-88652510",
            //    ContactMobile = "13522090770",
            //    ContactName = "管理员_测试",
            //    ContactTel = "0571-5521047",
            //    IsDisable = true,
            //    ID = 4,
            //    AreaId=new int[4]{1,2,3,4}
            //};
            
        }
    }
}
