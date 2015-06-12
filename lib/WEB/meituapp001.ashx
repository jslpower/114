<%@ WebHandler Language="C#" Class="meituapp001"  %>
using System;
using System.Web;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Net;

//meituapp wangqizhi 2011-12-21

public class meituapp001 : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (GetInt(context.Request.Form["t"]) == 1)
        {
            string cookies = string.Empty;
            string s = create("http://4.app.meitu.com/mkimg.php", string.Empty, Method.POST, "username=" + context.Request.Form["name"], ref cookies, false);
            string output = "{{\"success\":{0},\"url\":\"{1}\",\"msg\":\"{2}\"}}";
            context.Response.Clear();

            if (string.IsNullOrEmpty(s))
            {
                output = string.Format(output, "false", string.Empty, "该名字不在河蟹范围内，无法生成");
            }
            else if (s == "false")
            {
                output = string.Format(output, "false", string.Empty, "该名字不在河蟹范围内，无法生成");
            }
            else if (s == "false1")
            {
                output = string.Format(output, "false", string.Empty, "输入的名字太长");
            }
            else
            {
                output = string.Format(output, "true", s, string.Empty);
            }


            context.Response.Write(output);
            context.Response.End();
        }
    }	
	
	public bool IsReusable {
        get {
            return false;
        }
	}

    /// <summary>
    /// 将字符串转化为数字 若值不是数字返回defaultValue
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(string key, int defaultValue)
    {
        if (string.IsNullOrEmpty(key))
        {
            return defaultValue;
        }

        int result = 0;
        bool b = Int32.TryParse(key, out result);

        return result;
    }

    /// <summary>
    /// 将字符串转化为数字 若值不是数字返回0
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public enum Method
    {
        GET, POST
    }

    /// <summary>
    /// create http request
    /// </summary>
    /// <param name="requestUriString">request uri</param>
    /// <param name="referer">request referer</param>
    /// <param name="method">request method</param>
    /// <param name="data">post data</param>
    /// <param name="cookies">request cookies</param>
    /// <param name="keepAlive">request keepalive</param>
    /// <returns></returns>
    public static string create(string requestUriString, string referer, Method method, string data, ref string cookies, bool keepAlive)
    {
        StringBuilder responseText = new StringBuilder();

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
        request.Timeout = 300000;
        request.Method = method.ToString();
        request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        request.KeepAlive = keepAlive;
        request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:8.0) Gecko/20100101 Firefox/8.0";
        request.Accept = "*/*";
        request.Referer = referer;
        request.Headers.Set("Cookie", cookies);

        Encoding encode = System.Text.Encoding.UTF8;

        if (method == Method.POST && !string.IsNullOrEmpty(data))
        {
            byte[] bytes = encode.GetBytes(data);
            request.ContentLength = bytes.Length;

            Stream oStreamOut = request.GetRequestStream();
            oStreamOut.Write(bytes, 0, bytes.Length);
            oStreamOut.Close();
        }

        HttpWebResponse response = null;

        try
        {
            int i = 1;
            while (i > 0)
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK) break;
                else response = null;
                i--;
            }
        }
        catch { response = null; }

        if (response != null)
        {
            try
            {
                string rcookies = response.Headers["Set-Cookie"];

                if (!string.IsNullOrEmpty(rcookies))
                {
                    StringBuilder sb = new StringBuilder();
                    string[] arr = rcookies.Split(';');
                    foreach (string s in arr)
                    {
                        if (string.IsNullOrEmpty(s)
                            || s.ToLower().IndexOf("domain=") > -1
                            || s.ToLower().IndexOf("path=") > -1
                            || s.ToLower().IndexOf("expires=") > -1) continue;

                        sb.Append(s.Trim(','));
                        sb.Append(";");
                    }

                    cookies += sb.ToString();
                }

                Stream resStream = null;
                resStream = response.GetResponseStream();

                StreamReader readStream = new StreamReader(resStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    string s = new String(read, 0, count);
                    responseText.Append(s);
                    count = readStream.Read(read, 0, 256);
                }

                resStream.Close();
            }
            catch { }
        }

        return responseText.ToString();
    }
}