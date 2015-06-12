using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            imageServerUrl = ImageManage.GetImagerServerUrl(1);
        }
    }
}
