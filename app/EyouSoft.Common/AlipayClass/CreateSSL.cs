using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

/// <summary>
/// CreateSSL 的摘要说明
/// </summary>
public class CreateSSL
{
    private string url = string.Empty;
	public  CreateSSL(string url)
	{
        this.url = url;
	}

    public string GetResponse()
    {
        WebRequest request = WebRequest.Create(url);
        request.Proxy = null;
        request.Credentials = CredentialCache.DefaultCredentials;

        //allows for validation of SSL certificates 

        ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(
            delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            });

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();

        return responseFromServer;
    }
}
