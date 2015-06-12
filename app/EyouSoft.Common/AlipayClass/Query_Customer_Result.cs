using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

/// <summary>
///  查询电子协议接口 ，返回参数
/// </summary>
public class Query_Customer_Result
{

    private bool _IsSuccess_Refund_Charge = false;
    private string _ErrorCode = string.Empty;
    public bool IsSuccess_Refund_Charge
    {
        get { return _IsSuccess_Refund_Charge; }
    }
    public string ErrorCode
    {
        get { return _ErrorCode; }
    }
    public Query_Customer_Result(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);

        XmlElement document = doc.DocumentElement;

        XmlNode isSuccess = document.ChildNodes[0];
        if (isSuccess.InnerText == "T")
        {
            XmlNode _Refund_Charge = document.ChildNodes[2];
            if (_Refund_Charge.InnerText == "T")
            {
                _IsSuccess_Refund_Charge = true;
            }
            else
            {
                _IsSuccess_Refund_Charge = false;
            }
        }
        else
        {
            _IsSuccess_Refund_Charge = false;
            _ErrorCode = document.ChildNodes[1].InnerText;
        }
    }
}
