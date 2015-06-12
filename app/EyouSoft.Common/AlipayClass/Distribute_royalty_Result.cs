using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace AlipayClass
{
    /// <summary>
    /// 分润返回XML结果解析
    /// </summary>
    public class Distribute_royalty_Result
    {
        private bool _IsSuccess = false;
        private string _ErrorCode = string.Empty;
        public bool IsSuccess
        {
            get { return _IsSuccess; }
        }
        public string ErrorCode
        {
            get { return _ErrorCode; }
        }
        public Distribute_royalty_Result(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlElement document = doc.DocumentElement;

            XmlNode isSuccess = document.ChildNodes[0];
            if (isSuccess.InnerText == "T")
            {
                _IsSuccess = true;
            }
            else
            {
                _IsSuccess = false;
                _ErrorCode = document.ChildNodes[1].InnerText;
            }
        }
    }
}
