using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.Control
{
    /// <summary>
    /// 前台页面的父类
    /// 功能：统一添加页面元信息，处理页面标题
    /// </summary>
    public class FrontPage:BasePage
    {
        private string _imageServerPath;

        protected string ImageServerPath
        {
            get { return _imageServerPath; }
            set { _imageServerPath = value; }
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddMetaContentType();
            AddMetaIE8Compatible();
            AddMetaTag("description", "");
            AddMetaTag("keywords", "");
            //add Head Cache Contorl
            AddSiteIcon();
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            //统一处理网站标题
            //将Web网站名字 加入到页面标题中
            //Page.Title = Page.Title + "_" + ConfigClass.GetConfigString("UsedCar", "WebName");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);

            ImageServerPath = ImageManage.GetImagerServerUrl(1);
        }
    }
}
