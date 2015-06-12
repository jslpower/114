using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 信息反馈页用户控件
    /// 功能：显示头部信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04   
    public partial class AboutUsHeadControl : System.Web.UI.UserControl
    {
        //logo 图片地址
        protected string UnionLogo = "";
        protected string ImageServerPath = "";
        protected string ImageName = "";
        private string _setImgType;
        protected int CityId = 0;
        public string SetImgType
        {
            get { return _setImgType; }
            set { _setImgType = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FrontPage page = this.Page as FrontPage;
                this.CityId = page.CityId;
                ImageServerPath = page.ImageServerUrl;
                GetUnionLogo();
                ChangeImg();
            }
        }

        #region 根据不同的菜单修改图片
        protected void ChangeImg()
        {
            switch (SetImgType)
            {
                case "1": ImageName = "company1.gif"; break; //关于我们
                case "2": ImageName = "kefuzhongxin1.gif"; break; //客服中心
                case "3": ImageName = "kefuzhongxin2.gif"; break; //服务说明
                case "4": ImageName = "kefuzhongxin3.gif"; break; //代理合作
                case "5": ImageName = "kefuzhongxin4.gif"; break; //招聘英才
                case "6": ImageName = "yijian.gif"; break; //提意见
                case "8": ImageName = "wangzdt(1).gif"; break; //网站地图
                default: ImageName = "company1.gif"; break; //默认为关于我们
            }
        }
        #endregion 


        /// <summary>
        /// 获的联盟LOGO
        /// </summary>
        #region 设置头部的logo
        protected void GetUnionLogo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (Model != null)
            {
                UnionLogo = EyouSoft.Common.Domain.FileSystem + Model.UnionLog;
            }
            else
            {
                UnionLogo = EyouSoft.Common.Domain.ServerComponents + "/images/UserPublicCenter/indexlogo.gif";
            }
            Model = null;

        }
        #endregion
    }
}