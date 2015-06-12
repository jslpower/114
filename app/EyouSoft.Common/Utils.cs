using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using EyouSoft.Common.Function;
using EyouSoft.Common.ConfigModel;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using EyouSoft.Model.CompanyStructure;

namespace EyouSoft.Common
{
    public class Utils
    {
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// 确保用户的输入没有恶意代码
        /// </summary>
        /// <param name="text">要过滤的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>过滤后的字符串</returns>
        public static string InputText(string text, int maxLength)
        {
            if (text == null)
            {
                return string.Empty;
            }
            text = text.Trim();
            if (text == string.Empty)
            {
                return string.Empty;
            }
            if (text.Length > maxLength)
            {
                text = text.Substring(0, maxLength);
            }
            //text = Regex.Replace(text, "[\\s]{2,}", " ");	//将连续的空格转换为一个空格
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            //text = FormatKeyWord(text);//过滤敏感字符
            return text;
        }

        private static Regex RegHtml = new Regex("<\\/*[^<>]*>");
        /// <summary>
        /// 过滤HTML代码
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <returns></returns>
        public static string LoseHtml(string HtmlCode)
        {
            string tmpStr = "";
            if (null != HtmlCode && String.Empty != HtmlCode)
            {
                tmpStr = RegHtml.Replace(HtmlCode, "");
            }
            return tmpStr.Replace("\r", "").Replace("\n", "").Replace(" ", "");
        }

        public static string TextToHtml(string str)
        {
            if (str != null && str != string.Empty)
            {
                str = str.Replace("\n", "<br>").Replace(" ", "&nbsp;").Replace("&quot;", "\"");
            }
            else
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 过滤HTML代码
        /// </summary>
        /// <param name="HtmlCode">HTML代码</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string LoseHtml(string HtmlCode, int maxLength)
        {
            if (string.IsNullOrEmpty(HtmlCode))
            {
                return string.Empty;
            }

            HtmlCode = RegHtml.Replace(HtmlCode, "").Replace("\r", "").Replace("\n", "").Replace(" ", "");

            //截断maxLength
            if (HtmlCode.Length > maxLength)
            {
                HtmlCode = HtmlCode.Substring(0, maxLength);
            }

            return HtmlCode;
        }


        public static string InputText(string text)
        {
            return InputText(text, Int32.MaxValue);
        }

        public static string InputText(object text)
        {
            if (text == null)
            {
                return string.Empty;
            }
            return InputText(text.ToString());
        }
        public static string GetQueryStringValue(string key)
        {
            string tmp = HttpContext.Current.Request.QueryString[key] != null ? HttpContext.Current.Request.QueryString[key].ToString() : "";
            return InputText(tmp);
        }

        /// <summary>
        /// 过滤编辑器输入的恶意代码
        /// </summary>
        /// <param name="key">需要过滤的字符串</param>
        /// <returns></returns>
        public static string EditInputText(string text)
        {
            if (text == null || text.Trim() == string.Empty)
            {
                return string.Empty;
            }
            if (text.Length > Int32.MaxValue)
            {
                text = text.Substring(0, Int32.MaxValue);
            }
            text = text.Replace("'", "''");
            return Microsoft.Security.Application.AntiXss.GetSafeHtmlFragment(text);
        }

        /// <summary>
        /// 获取表单的值
        /// </summary>
        /// <param name="key">表单的key</param>
        /// <returns></returns>
        public static string GetFormValue(string key)
        {
            return GetFormValue(key, Int32.MaxValue);
        }
        /// <summary>
        /// 获取表单的值
        /// </summary>
        /// <param name="key">表单的key</param>
        /// <param name="maxLength">接受的最大长度</param>
        /// <returns></returns>
        public static string GetFormValue(string key, int maxLength)
        {
            string tmp = HttpContext.Current.Request.Form[key] != null ? HttpContext.Current.Request.Form[key].ToString() : "";
            return InputText(tmp, maxLength);
        }

        public static string[] GetFormValues(string key)
        {
            string[] tmps = HttpContext.Current.Request.Form.GetValues(key);
            if (tmps == null)
            {
                return new string[] { };
            }
            for (int i = 0; i < tmps.Length; i++)
            {
                tmps[i] = InputText(tmps[i]);
            }
            return tmps;
        }
        /// <summary>
        /// 若字符串为null或Empty，则返回指定的defaultValue.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetString(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 将字符串转化为数字(无符号整数) 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            if (string.IsNullOrEmpty(key) || !EyouSoft.Common.Function.StringValidate.IsInteger(key))
            {
                return defaultValue;
            }


            int result = 0;
            bool b = Int32.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为数字(无符号整数) 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        /// <summary>
        /// 将字符串转化为数字(有符号整数) 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntSign(string key, int defaultValue)
        {
            if (string.IsNullOrEmpty(key) || !EyouSoft.Common.Function.StringValidate.IsIntegerSign(key))
            {
                return defaultValue;
            }


            int result = 0;
            bool b = Int32.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为数字(有符号整数) 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntSign(string key)
        {
            return GetIntSign(key, 0);
        }

        /// <summary>
        /// 将字符串转换为可空的日期类型，如果字符串不是有效的日期格式，则返回null
        /// </summary>
        /// <param name="s">进行转换的字符串</param>
        /// <returns></returns>
        public static DateTime? GetDateTimeNullable(string s)
        {
            return GetDateTimeNullable(s, null);
        }
        /// <summary>
        /// 将字符串转换为可空的日期类型，如果字符串不是有效的日期格式，则返回defaultValue
        /// </summary>
        /// <param name="s">进行转换的字符串</param>
        /// <param name="defaultValue">要返回的默认值</param

        /// <returns></returns>
        public static DateTime? GetDateTimeNullable(string s, DateTime? defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            if (EyouSoft.Common.Function.StringValidate.IsDateTime(s))
            {
                return new System.Nullable<DateTime>(DateTime.Parse(s));
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将字符串转化为Int可空类型，若不是数字指定的defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? GetIntNull(string key, int? defaultValue)
        {
            if (string.IsNullOrEmpty(key) || !EyouSoft.Common.Function.StringValidate.IsInteger(key))
            {
                return defaultValue;
            }
            int? i = int.Parse(key);
            return i;
        }
        /// <summary>
        /// 将字符串转化为Int可空类型，若不是数字返回null的Int?.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int? GetIntNull(string key)
        {
            return GetIntNull(key, null);
        }
        /// <summary>
        ///  将字符串转化为浮点数 若值不是浮点数返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(key) || !StringValidate.IsDecimal(key))
            {
                return defaultValue;
            }
            return Decimal.Parse(key);
        }
        /// <summary>
        ///  将字符串转化为浮点数 若值不是浮点数返回0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            return GetDecimal(key, 0);
        }

        public static DateTime GetDateTime(string key, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            if (StringValidate.IsDateTime(key))
            {
                DateTime.TryParse(key, out result);
            }
            return result;
        }

        public static DateTime GetDateTime(string key)
        {
            return GetDateTime(key, DateTime.MinValue);
        }
        /// <summary>
        /// 验证用户上传的车辆图片是否在指定的文件类型范围内,或者指定的文件大小内
        /// </summary>
        /// <param name="file"></param>
        /// <returns>验证信息</returns>
        public static string IsValidUploadImage(HttpPostedFile file)
        {
            string msg = string.Empty;
            bool fileExtensionOK = false;
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();

            //String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            string[] allowedExtensions = ConfigClass.GetConfigString("UsedCar", "ImgFlieExt").Split(',');

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileExtensionOK = true;
                }
            }

            bool fileMimeOK = false;
            string fileMime = file.ContentType.ToLower();
            string expectedFileMime = GetMimeTypeByFileExtension(fileExtension);
            string[] allowMime = new string[allowedExtensions.Length];
            for (int i = 0; i < allowMime.Length; i++)
            {
                allowMime[i] = GetMimeTypeByFileExtension(allowedExtensions[i]);
            }

            if (expectedFileMime.IndexOf(fileMime) != -1)
            {
                for (int i = 0; i < allowMime.Length; i++)
                {
                    if (allowMime[i].IndexOf(fileMime) != -1)
                    {
                        fileMimeOK = true;
                    }
                }
            }

            if (!fileExtensionOK || !fileMimeOK)
            {
                msg += "不是预期的文件类型，只能上传.gif,.png,.jpeg,.jpg文件.";
            }

            bool fileSizeOK = false;
            int maxFileSize = 200 * 1024;
            int fileSize = file.ContentLength;
            if (fileSize <= maxFileSize)
            {
                fileSizeOK = true;
            }

            if (!fileSizeOK)
            {
                msg += "文件大小不能超过200KB.";
            }

            return msg;
        }
        /// <summary>
        /// 根据指定的文件扩展名获取相应的文件MIME类型
        /// </summary>
        /// <param name="fileExtension">文件扩展名,带.</param>
        /// <returns>文件MIME类型</returns>
        public static string GetMimeTypeByFileExtension(string fileExtension)
        {
            string mime = "";
            fileExtension = fileExtension.ToLower();
            switch (fileExtension)
            {
                case ".gif":
                    mime = "image/gif";
                    break;
                case ".png":
                    mime = "image/png image/x-png";
                    break;
                case ".jpeg":
                    mime = "image/jpeg";
                    break;
                case ".jpg":
                    mime = "image/pjpeg";
                    break;
                case ".bmp":
                    mime = "image/bmp";
                    break;
            }

            //if (mime == string.Empty)
            //{
            //    throw new Exception(fileExtension+"  未知的扩展名类型.");
            //}

            return mime;
        }
        /// <summary>
        /// 获取客户端验证图片扩展名是否有效的正则表达式
        /// </summary>
        /// <returns></returns>
        public static string GetCheckValidImageRegExp()
        {
            string[] allowedExtensions = ConfigClass.GetConfigString("UsedCar", "ImgFlieExt").Split(',');

            System.Text.StringBuilder regexp = new System.Text.StringBuilder();
            regexp.Append("\\.(");
            if (allowedExtensions.Length > 0)
            {
                string old = null;
                string clone = null;
                string format = "[{0}{1}]";
                foreach (string s in allowedExtensions)
                {
                    old = s.Replace(".", "").ToLower();
                    clone = string.Copy(old.ToUpper());
                    for (int i = 0; i < old.Length; i++)
                    {
                        regexp.AppendFormat(format, old[i], clone[i]);
                    }
                    regexp.Append("|");
                }
                regexp.Remove(regexp.Length - 1, 1);
            }
            else
            {
                regexp.Append("[^.]*");
            }
            regexp.Append(")$");

            return regexp.ToString();
        }
        /// <summary>
        /// 判断当前的时间属于早上，下午，晚上哪个时刻，返回相应的“你好”词句
        /// </summary>
        /// <returns></returns>
        public static string GetHelloByTime()
        {
            int hour = DateTime.Now.Hour;
            string text = null;
            if (hour >= 0 && hour <= 12)
            {
                text = "早上好";
            }
            else if (hour > 12 && hour < 18)
            {
                text = "下午好";
            }
            else
            {
                text = "晚上好";
            }

            return text;
        }
        /// <summary>
        /// 获取下订单页面
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        public static string GetBookOrderUrl(string tourid)
        {
            return GetBookOrderUrl(tourid, 0, 0);
        }
        /// <summary>
        /// 获取下订单页面
        /// </summary>
        /// <param name="tourid"></param>
        /// <param name="adultnum"></param>
        /// <param name="childnum"></param>
        /// <returns></returns>
        public static string GetBookOrderUrl(string tourid, int adultnum, int childnum)
        {
            return Domain.UserBackCenter + "/TeamService/RouteOrder.aspx?tourid=" + tourid + "&adultnum=" + adultnum + "&childnum=" + childnum;
        }
        /// <summary>
        /// 判断输入的字符串是否是有效的电话号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(string input)
        {
            return StringValidate.IsRegexMatch(input, @"^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$");
        }
        /// <summary>
        /// 判断输入的字符串是否是有效的手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobile(string input)
        {
            return StringValidate.IsRegexMatch(input, @"^(13|15|18|14)\d{9}$");
        }
        /// <summary>
        /// 判断输入的字符串是否是有效的电话号码或者手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            return IsPhone(input) || IsMobile(input);
        }
        /// <summary>
        /// 根据指定的消息显示Alert消息对话框，并跳转到指定的url地址
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void ShowAndRedirect(string msg, string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Write("<script>alert('");
            response.Write(msg);
            response.Write("');window.location.href='");
            response.Write(url);
            response.Write("';");
            response.Write("</script>");
            response.End();
        }
        /// <summary>
        /// 根据指定的消息显示Alert消息对话框，并跳转到指定的url地址
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void ShowAndClose(string msg, string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Write("<script>alert('");
            response.Write(msg);
            response.Write("');window.top.location.href='");
            response.Write(url);
            response.Write("';");
            response.Write("</script>");
            response.End();
        }
        /// <summary>
        /// 清空页面，输出指定的字符串
        /// </summary>
        /// <param name="msg"></param>
        public static void Show(string msg)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Write(msg);
            response.End();
        }
        /// <summary>
        /// 判断是否是有效的密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string input)
        {
            return StringValidate.IsRegexMatch(input, @"^[a-zA-Z\W_\d]{6,16}$");
        }
        /// <summary>
        /// 获取文件上传的相对路径
        /// </summary>
        /// <returns></returns>
        public static string GetUploadPath()
        {
            return ConfigClass.GetConfigString("UsedCar", "UploadFilePath");
        }
        /// <summary>
        /// 根据用户ID,获取文件上传的相对路径
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUploadPath(int userId)
        {
            string path = GetUploadPath();
            string userPath = path + userId + "/";
            int res = StringValidate.CreateDirectory(HttpContext.Current.Server.MapPath(userPath));
            if (res == 2)
            {
                return path;
            }
            else
            {
                return userPath;
            }
        }

        private static string _RelativeWebRoot;
        /// <summary>
        /// 获取网站根目录的相对路径。
        /// </summary>
        /// <value>返回的地址以'/'结束.</value>
        public static string RelativeWebRoot
        {
            get
            {
                if (_RelativeWebRoot == null)
                    _RelativeWebRoot = VirtualPathUtility.ToAbsolute("~/");

                return _RelativeWebRoot;
            }
        }

        /// <summary>
        /// 获取网站根目录的绝对地址。
        /// </summary>
        /// <value>返回的地址以'/'结束.</value>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                    throw new System.Net.WebException("The current HttpContext is null");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }

        /// <summary>
        /// 将相对url地址转换为绝对url地址.
        /// </summary>
        public static Uri ConvertToAbsolute(Uri relativeUri)
        {
            return ConvertToAbsolute(relativeUri.ToString()); ;
        }

        /// <summary>
        /// 将相对url地址转换为绝对url地址.
        /// </summary>
        public static Uri ConvertToAbsolute(string relativeUri)
        {
            if (String.IsNullOrEmpty(relativeUri))
                throw new ArgumentNullException("relativeUri");

            string absolute = AbsoluteWebRoot.ToString();
            int index = absolute.LastIndexOf(RelativeWebRoot.ToString());

            return new Uri(absolute.Substring(0, index) + relativeUri);
        }

        /// Retrieves the subdomain from the specified URL.
        /// </summary>
        /// <param name="url">The URL from which to retrieve the subdomain.</param>
        /// <returns>The subdomain if it exist, otherwise null.</returns>
        public static string GetSubDomain(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                string host = url.Host;
                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".");
                    int index = host.LastIndexOf(".", lastIndex - 1);
                    return host.Substring(0, index);
                }
            }

            return null;
        }
        /// <summary>
        /// 获取域名后缀。
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetDomainSuffix(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                string host = url.Host;
                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".");
                    int index = host.LastIndexOf(".", lastIndex - 1);
                    return host.Substring(index + 1);
                }
            }

            return null;
        }
        public static void ResponseMeg(bool isOk, string msg)
        {
            HttpContext.Current.Response.Clear();
            if (isOk)
                HttpContext.Current.Response.Write("{\"success\":\"1\",\"message\":\"" + msg + "\"}");
            else
                HttpContext.Current.Response.Write("{\"success\":\"0\",\"message\":\"" + msg + "\"}");
            HttpContext.Current.Response.End();
        }
        public static void ResponseMegSuccess()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("{\"success\":\"1\",\"message\":\"操作完成\"}");
            HttpContext.Current.Response.End();
        }
        public static void ResponseMegNoComplete()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("{\"success\":\"0\",\"message\":\"请填写完整\"}");
            HttpContext.Current.Response.End();
        }
        public static void ResponseMegError()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("{\"success\":\"0\",\"message\":\"操作失败\"}");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 根据当前的时间和文件扩展名生成文件名
        /// </summary>
        /// <param name="fileExt">文件扩展名 带.</param>
        /// <returns></returns>
        public static string GenerateFileName(string fileExt)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(1, 99).ToString() + fileExt;
        }
        public static string GenerateFileName(string fileExt, string suffix)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(1, 99).ToString() + "_" + suffix + fileExt;
        }

        /// <summary>
        /// 根据指定高级网店模板获取模板的相对路径，相对于SeniorOnlineShop项目
        /// </summary>
        /// <param name="templateId">模板编号</param>
        /// <returns></returns>
        public static string GetEShopTemplatePath(int templateId)
        {
            return GetEShopTemplateVirtualDirectory(templateId) + "default.aspx";
        }

        /// <summary>
        /// 根据指定高级网店模板获取模板的相对目录，相对于SeniorOnlineShop项目
        /// </summary>
        /// <param name="templateId">模板编号</param>
        /// <returns></returns>
        private static string GetEShopTemplateVirtualDirectory(int templateId)
        {
            string s = string.Empty;

            switch (templateId)
            {
                case 1:
                case 2:
                case 3:
                    s = "/seniorshop/"; break;
                case 4:
                    s = "/template4/"; break;
                default:
                    s = "/seniorshop/"; break;
            }

            return s;
        }

        /// <summary>
        /// 根据指定高级网店模板获取模板管理页的相对路径，相对于UserBackCenter项目
        /// </summary>
        /// <param name="templateId">模板编号</param>
        /// <returns></returns>
        public static string GetEshopMTemplatePath(int templateId)
        {
            string s = string.Empty;

            switch (templateId)
            {
                case 1:
                case 2:
                case 3:
                    s = "/Eshop/EShopSet.aspx"; break;
                case 4:
                    s = "/Eshop/EshopSet4.aspx"; break;
                default:
                    s = "/Eshop/EShopSet.aspx"; break;
            }

            return s;
        }

        /// <summary>
        /// 判断是否是高级网店页面
        /// </summary>
        /// <returns></returns>
        public static bool IsEShopPage()
        {
            string[] eShopPages = { "/seniorshop/", "/template4/", "/scenicspots/" };
            string currentExecutionFilePath = HttpContext.Current.Request.CurrentExecutionFilePath.ToLower();
            bool result = false;

            foreach (string s in eShopPages)
            {
                if (currentExecutionFilePath.IndexOf(s.ToLower()) > -1)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 高级网店模板验证，模板编号若与相应的模板不对应，则跳转至相应模板
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="templateId">模板编号</param>
        public static void EShopTemplateValidate(string companyId, int templateId)
        {
            string currentExecutionFilePath = HttpContext.Current.Request.CurrentExecutionFilePath.ToLower();
            string templateVirtualDirectory = GetEShopTemplateVirtualDirectory(templateId);

            if (currentExecutionFilePath.IndexOf(templateVirtualDirectory.ToLower()) < 0)
            {
                HttpContext.Current.Response.Redirect(GenerateShopPageUrl(GetEShopTemplatePath(templateId), companyId));
            }
        }

        /// <summary>
        /// 根据指定的公司ID,获取高级网店URL
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string GetEShopUrl(string companyId)
        {
            var templateId = 1;
            var eshop = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(companyId);

            if (eshop != null)
            {
                templateId = eshop.TemplateId;
            }

            return string.Format("{0}{1}?cid={2}", Domain.SeniorOnlineShop, GetEShopTemplatePath(templateId), companyId);
        }
        /// <summary>
        /// 根据指定的公司ID,获取普通网店URL
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static string GetShopUrl(string companyId)
        {
            return string.Format("{0}/shop_{1}", Domain.SeniorOnlineShop, companyId);
        }
        public static string GetShopUrl(string companyId, int CityId)
        {
            return string.Format("{0}/shop_{1}_{2}", Domain.SeniorOnlineShop, companyId, CityId);
        }

        /// <summary>
        /// 根据公司编号和公司身份获取普通或者高级网店的链接地址
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="companyType">公司类型</param>
        /// <param name="cityId">城市编号（小于等于0不加此参数）</param>
        /// <returns></returns>
        public static string GetShopUrl(string companyId, EyouSoft.Model.CompanyStructure.CompanyType companyType, int cityId)
        {
            if (string.IsNullOrEmpty(companyId))
                return string.Empty;

            string strUrl = string.Empty;
            //是否开通高级网店
            bool isOpenEShop = IsOpenHighShop(companyId);
            var companyDomain =
                        EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetDomainList(new string[] { companyId });
            //独立域名,开通高级网店独立域名才有用
            if (isOpenEShop && companyDomain != null && companyDomain.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysCompanyDomain item in companyDomain)
                {
                    if (item.DomainType == EyouSoft.Model.SystemStructure.DomainType.网店域名)
                    {
                        if (item.Domain.IndexOf("http://") < 0)
                            strUrl = "http://" + item.Domain;
                        else
                            strUrl = item.Domain;
                        break;
                    }
                }
                switch (companyType)
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.专线:
                        if (cityId <= 0)
                            strUrl += string.Format("/shop_{0}", companyId);
                        else
                            strUrl += string.Format("/shop_{0}_{1}", companyId, cityId);
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        if (cityId <= 0)
                            strUrl += string.Format("/jingqu_2_{0}", companyId);
                        else
                            strUrl += string.Format("/jingqu_2_{0}_{1}", companyId, cityId);
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                        strUrl = cityId <= 0
                                         ? URLREWRITE.Car.CarAdvancedShopUrl(companyId)
                                         : URLREWRITE.Car.CarAdvancedShopUrl(cityId, companyId);
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        strUrl = cityId <= 0
                                         ? URLREWRITE.TravelProtects.TravelAdvancedShopUrl(companyId)
                                         : URLREWRITE.TravelProtects.TravelAdvancedShopUrl(cityId, companyId);
                        break;
                }
            }
            else if (companyDomain == null || companyDomain.Count < 0 || string.IsNullOrEmpty(strUrl))
            {
                switch (companyType)
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.专线:
                        //开通高级网店
                        if (isOpenEShop)
                            strUrl = GetEShopUrl(companyId);
                        else
                            strUrl = cityId <= 0 ? GetShopUrl(companyId) : GetShopUrl(companyId, cityId);
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        //开通高级网店
                        if (isOpenEShop)
                        {
                            strUrl = cityId <= 0
                                         ? URLREWRITE.ScenicSpot.ScenicEShop(companyId)
                                         : URLREWRITE.ScenicSpot.ScenicEShop(companyId, cityId);
                        }
                        else
                        {
                            strUrl = cityId <= 0
                                         ? URLREWRITE.ScenicSpot.ScenicGeneralShop(companyId)
                                         : URLREWRITE.ScenicSpot.ScenicGeneralShop(companyId, cityId);
                        }
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                        if (isOpenEShop)
                        {
                            strUrl = cityId <= 0
                                         ? URLREWRITE.Car.CarAdvancedShopUrl(companyId)
                                         : URLREWRITE.Car.CarAdvancedShopUrl(cityId, companyId);
                        }
                        else
                        {
                            strUrl = URLREWRITE.Car.CardGeneralCarShopUrl(cityId, companyId);
                        }
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        if (isOpenEShop)
                        {
                            strUrl = cityId <= 0
                                         ? URLREWRITE.TravelProtects.TravelAdvancedShopUrl(companyId)
                                         : URLREWRITE.TravelProtects.TravelAdvancedShopUrl(cityId, companyId);
                        }
                        else
                        {
                            strUrl = URLREWRITE.TravelProtects.TravelDetailsUrl(cityId, companyId);
                        }
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.地接:
                        strUrl = Domain.UserPublicCenter + "/RouteManage/LocalRouteList.aspx?cid=" + companyId + "&CityId=" + cityId;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.组团:
                    case EyouSoft.Model.CompanyStructure.CompanyType.其他采购商:
                        strUrl = Domain.UserPublicCenter + "/Procurement/ProcureDetails.aspx?cid=" + companyId;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                        strUrl = Domain.UserPublicCenter + "/ShoppingInfo/GeneralShoppingShop.aspx?cid=" + companyId + "&CityId=" + cityId;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                        strUrl = Domain.UserPublicCenter + "/HotelManage/HotelDetails.aspx?cid=" + companyId + "&CityId=" + cityId;
                        break;
                }
            }
            else
            {
                strUrl = "javascript:void(0);return false;";
            }

            return strUrl;
        }
        /// <summary>
        /// 根据指定的公司Id和高级网店页面URL，生成指定公司的高级网店URL
        /// </summary>
        /// <param name="url">高级网店页面URL</param>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string GenerateShopPageUrl(string url, string companyId)
        {
            bool IsIndependentDomain = false;
            string path = HttpContext.Current.Request.ServerVariables["Http_Host"];
            if (Domain.SeniorOnlineShop.IndexOf(path) == -1)
            {
                IsIndependentDomain = true;
            }
            if (!IsIndependentDomain)
            {
                if (url.IndexOf("?") == -1)
                {
                    return string.Format("{0}{1}?cid={2}", Domain.SeniorOnlineShop, url, companyId);
                }
                else
                {
                    return string.Format("{0}{1}&cid={2}", Domain.SeniorOnlineShop, url, companyId);
                }
            }
            else
            {
                return string.Format("http://{0}{1}", path, url);
            }
        }
        public static string GenerateShopPageUrl2(string url, string companyId)
        {
            bool IsIndependentDomain = false;
            string path = HttpContext.Current.Request.ServerVariables["Http_Host"];
            if (Domain.SeniorOnlineShop.IndexOf(path) == -1)
            {
                IsIndependentDomain = true;
            }
            if (!IsIndependentDomain)
            {
                return string.Format("{0}{1}_{2}", Domain.SeniorOnlineShop, url, companyId);
                //if (url.IndexOf("?") == -1)
                //{
                //    return string.Format("{0}{1}?cid={2}", Domain.SeniorOnlineShop, url, companyId);
                //}
                //else
                //{
                //    return string.Format("{0}{1}&cid={2}", Domain.SeniorOnlineShop, url, companyId);
                //}
            }
            else
            {
                if (url == "/")
                {
                    return string.Format("http://{0}{1}", path, url);
                }
                else
                {
                    return string.Format("http://{0}{1}_{2}", path, url, companyId);
                }
            }
        }
        /// <summary>
        /// 获取用户退出的页面地址
        /// </summary>
        /// <param name="backurl">指定退出成功返回的页面地址</param>
        /// <returns></returns>
        public static string GetLogoutUrl(string backurl)
        {
            return string.Format("{0}/logout.aspx?backurl={1}", Domain.UserPublicCenter, HttpUtility.UrlEncode(backurl));
        }
        /// <summary>
        /// 根据MQ号码获取MQ洽谈链接
        /// </summary>
        /// <param name="mq">MQ号码</param>
        /// <returns></returns>
        public static string GetMQLink(string mq)
        {
            return string.Format("http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid={0}", mq);
        }
        /// <summary>
        /// 根据MQ号码获取小图片的MQ洽谈
        /// </summary>
        /// <param name="mq"></param>
        public static string GetMQ(string mq)
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(mq))
            {
                Result = string.Format("<a href=\"javascript:void(0)\" style=\" vertical-align:middle;\" onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid={0}')\" title=\"点击MQ图标洽谈！\"><img src='{1}/images/MQWORD.gif' /></a>", mq, Domain.ServerComponents);
            }
            return Result;
        }
        /// <summary>
        /// 重载MQ号码获取小图片的MQ洽谈方法
        /// </summary>
        /// <param name="mq">mq号码</param>
        /// <param name="userName">mq用户名</param>
        /// <returns></returns>
        public static string GetMQ(string mq, string userName)
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(mq))
            {
                Result = string.Format("{2}<a href=\"javascript:void(0)\" style=\" vertical-align:middle;\" onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid={0}')\" title=\"点击MQ图标洽谈！\"><img src='{1}/images/MQWORD.gif' /></a>", mq, Domain.ServerComponents, userName);
            }
            return Result;
        }
        /// <summary>
        /// 强制QQ聊天
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetForceQQ(string qq, string s)
        {
            string tmp = string.Empty;
            if (!String.IsNullOrEmpty(qq))
            {
                tmp = string.Format("<a href=\"http://wpa.qq.com/msgrd?v=3&uin={0}&site=qq&menu=yes\" target=\"_blank\" title=\"在线即时交谈\"><img src=\"{1}/images/new2011/newqq.jpg\" border=\"0\">{2}</a>", qq, Domain.ServerComponents, s);
            }
            return tmp;
        }

        public static string GetQQ(string qq)
        {
            return GetQQ(qq, string.Empty);
        }
        public static string GetNewQQ(string qq)
        {
            return GetNewQQ(qq, string.Empty);
        }
        /// <summary>
        /// 输出QQ链接
        /// </summary>
        /// <param name="qq">QQ号码</param>
        /// <param name="s">提示文字</param>
        /// <returns></returns>
        public static string GetQQ(string qq, string s)
        {
            string tmp = string.Empty;
            if (!String.IsNullOrEmpty(qq))
            {
                tmp = string.Format("<a href=\"tencent://message/?websitename=qzone.qq.com&menu=yes&uin={0}\" title=\"在线即时交谈\"><img src=\"{1}/images/sqq.gif\" border=\"0\">{2}</a>", qq, Domain.ServerComponents, s);
            }
            return tmp;
        }

        public static string GetNewQQ(string qq, string s)
        {
            string tmp = string.Empty;
            if (!String.IsNullOrEmpty(qq))
            {
                tmp = string.Format("<a href=\"http://wpa.qq.com/msgrd?v=3&uin={0}&site=qq&menu=yes\" target=\"_blank\" title=\"在线即时交谈\"><img src=\"{1}/Images/new2011/newqq.jpg\" border=\"0\">{2}</a>", qq, Domain.ServerComponents, s);
            }
            return tmp;
        }
        /// <summary>
        /// 获取QQ链接Url
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public static string GetQQUrl(string qq)
        {
            return string.Format("tencent://message/?websitename=qzone.qq.com&menu=yes&uin={0}", qq);
        }



        /// <summary>
        /// 根据MQ号码获取大图片的MQ洽谈
        /// </summary>
        /// <param name="mq"></param>
        public static string GetBigImgMQ(string mq)
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(mq))
            {
                Result = string.Format("<a href=\"javascript:void(0)\" style=\"vertical-align:middle;\" onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid={0}')\" title=\"点击MQ图标洽谈！\"><img src='{1}/images/mqonline.gif' /></a>", mq, Domain.ServerComponents);
            }
            return Result;
        }
        /// <summary>
        /// 根据MQ号码获取大图片的MQ洽谈2
        /// </summary>
        /// <param name="mq"></param>
        /// <returns></returns>
        public static string GetBigImgMQ2(string mq)
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(mq))
            {
                Result = string.Format("<a href=\"javascript:void(0)\" style=\"vertical-align:middle;\" onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid={0}')\" title=\"点击MQ图标洽谈！\"><img src='{1}/images/jipiao/MQ-online.jpg' /></a>", mq, Domain.ServerComponents);
            }
            return Result;
        }

        /// <summary>
        /// 生成MSN链接
        /// </summary>
        /// <param name="msn">MSN号码</param>
        /// <returns></returns>
        public static string GetMsn(string msn)
        {
            string strReturn = string.Empty;
            if (!string.IsNullOrEmpty(msn))
            {
                strReturn = string.Format("<a href=\"msnim:chat?contact={0}\" target=\"_blank\" title=\"在线即时交谈\"><img src='{1}/Images/seniorshop/msn1.gif' /></a>", msn, Domain.ServerComponents);
                //strReturn = string.Format("<a href=\"http://go.discuz.com/msn/?linkid=6&msn={0}\" target=\"_blank\" title=\"在线即时交谈\"><img src='{1}/Images/seniorshop/msn1.gif' /></a>", msn, Domain.ServerComponents);
            }

            return strReturn;
        }

        /// <summary>
        /// 将英文星期几转化为中文星期几
        /// </summary>
        /// <param name="DayOfWeek"></param>
        /// <returns></returns>
        public static string ConvertWeekDayToChinese(DateTime time)
        {
            string DayOfWeek = time.DayOfWeek.ToString();
            switch (DayOfWeek)
            {
                case "Monday":
                    DayOfWeek = "周一";
                    break;
                case "Tuesday":
                    DayOfWeek = "周二";
                    break;
                case "Wednesday":
                    DayOfWeek = "周三";
                    break;
                case "Thursday":
                    DayOfWeek = "周四";
                    break;
                case "Friday":
                    DayOfWeek = "周五";
                    break;
                case "Saturday":
                    DayOfWeek = "周六";
                    break;
                case "Sunday":
                    DayOfWeek = "周日";
                    break;
                default:
                    break;
            }
            return DayOfWeek;
        }
        /// <summary>
        /// 如果指定的字符串的长度超过了maxLength，则截取
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetText(string text, int maxLength)
        {
            return GetText(text, maxLength, false);
        }
        /// <summary>
        ///  如果指定的字符串的长度超过了maxLength，则截取
        /// </summary>
        /// <param name="text">要截取的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="isShowEllipsis">是否在字符串结尾显示省略号</param>
        /// <returns></returns>
        public static string GetText(string text, int maxLength, bool isShowEllipsis)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            else
            {
                if (text.Length >= maxLength)
                {
                    if (isShowEllipsis)
                    {
                        return text.Substring(0, maxLength) + "...";
                    }
                    else
                    {
                        return text.Substring(0, maxLength);
                    }
                }
                else
                {
                    return text;
                }
            }
        }
        /// <summary>
        /// 将字符串控制在指定数量的汉字以内，两个字母、数字相当于一个汉字，其他的标点符号算做一个汉字
        /// </summary>
        /// <param name="text">要控制的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="isShowEllipsis">是否在字符串结尾添加【...】</param>
        /// <returns></returns>
        public static string GetText2(string text, int maxLength, bool isShowEllipsis)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            double mlength = (double)maxLength;
            if (text.Length <= mlength)
            {
                return text;
            }
            System.Text.StringBuilder strb = new System.Text.StringBuilder();

            char c;
            for (int i = 0; i < text.Length; i++)
            {
                if (mlength > 0)
                {
                    c = text[i];
                    strb.Append(c);
                    mlength = mlength - GetCharLength(c);
                }
                else
                {
                    break;
                }
            }
            if (isShowEllipsis)
                strb.Append("…");
            return strb.ToString();
        }

        /// <summary>
        /// 判断字符是否是中文字符
        /// </summary>
        /// <param name="c">要判断的字符</param>
        /// <returns>true:是中文字符,false:不是</returns>
        public static bool IsChinese(char c)
        {
            System.Text.RegularExpressions.Regex rx =
                new System.Text.RegularExpressions.Regex("^[\u4e00-\u9fa5]$");
            return rx.IsMatch(c.ToString());
        }

        /// <summary>
        /// 判断字符是否是IP
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static bool isIP(string IP)
        {
            System.Text.RegularExpressions.Regex rx = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            return rx.IsMatch(IP);
        }

        /// <summary>
        /// 判断是否英文字母或数字的C#正则表达式 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNatural_Number(char c)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(c.ToString());
        }

        /// <summary>
        /// 获取字符长度,汉字为1，英文或数字0.5，其余为1
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double GetCharLength(char c)
        {
            if (IsChinese(c) == true)
            {
                return 1;
            }
            else if (IsNatural_Number(c) == true)
            {
                return 0.5;
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// 获取行程单路径
        /// </summary>
        /// <param name="TourID">团队编号</param>
        /// <returns></returns>
        public static string GetTeamInformationPagePath(string TourID)
        {
            return Domain.UserBackCenter + "/tour_" + TourID;
        }
        /// <summary>
        /// 获取指定公司下的所有子帐号用户:只有ID和UserName
        /// </summary>
        /// <param name="dplUserList">绑定子账户的DropDownList</param>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        public static void GetCompanyChildAccount(DropDownList dplUserList, string CompanyID)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> UserLists = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(CompanyID);
            if (UserLists != null && UserLists.Count > 0)
            {
                dplUserList.DataTextField = "UserName";
                dplUserList.DataValueField = "ID";
                dplUserList.DataSource = UserLists;
                dplUserList.DataBind();
            }
            dplUserList.Items.Insert(0, new ListItem("--全部--", ""));
            UserLists = null;
        }

        /// <summary>
        /// 根据公司编号获取公司类型（获取高级网店域名时用）
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>公司类型</returns>
        public static EyouSoft.Model.CompanyStructure.CompanyType? GetCompanyType(string CompanyId)
        {
            EyouSoft.Model.CompanyStructure.CompanyInfo cInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            EyouSoft.Model.CompanyStructure.CompanyType? cType = GetCompanyType(cInfo);
            cInfo = null;

            return cType;
        }

        /// <summary>
        /// 根据公司信息获取公司类型（获取高级网店域名时用）
        /// </summary>
        /// <param name="cInfo">公司信息</param>
        /// <returns></returns>
        public static EyouSoft.Model.CompanyStructure.CompanyType? GetCompanyType(EyouSoft.Model.CompanyStructure.CompanyInfo cInfo)
        {
            EyouSoft.Model.CompanyStructure.CompanyType? cType = null;

            if (cInfo != null)
            {
                if (cInfo.BusinessProperties == EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社)
                {
                    if (cInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                    {
                        cType = EyouSoft.Model.CompanyStructure.CompanyType.专线;
                    }
                    else if (cInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                    {
                        cType = EyouSoft.Model.CompanyStructure.CompanyType.地接;
                    }
                    else if (cInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        cType = EyouSoft.Model.CompanyStructure.CompanyType.组团;
                    }
                }
                else if (cInfo.BusinessProperties == EyouSoft.Model.CompanyStructure.BusinessProperties.其他采购商)
                {
                    cType = EyouSoft.Model.CompanyStructure.CompanyType.其他采购商;
                }
                else
                {
                    if (cInfo.CompanyRole.RoleItems.Length > 0)
                    {
                        cType = cInfo.CompanyRole.RoleItems[0];
                    }
                }
            }

            return cType;
        }

        /// <summary>
        /// 根据公司ID及公司类型返回公司网店URL（有开通高级网店为高级网店URL，未开通为普通网店URL）
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="CompanyStateModel">返回公司状态信息</param>
        /// <param name="isOpenHighShop">返回是否开通高级网店</param>
        /// <param name="CompanyType">公司类型</param>
        /// <param name="CityId">销售城市ID</param>
        /// <returns></returns>
        public static string GetCompanyDomain(string CompanyId, out EyouSoft.Model.CompanyStructure.CompanyState CompanyStateModel, out bool isOpenHighShop, EyouSoft.Model.CompanyStructure.CompanyType CompanyType, int CityId)
        {
            string strCompanyDmainUrl = "";
            //是否开通高级网店服务 
            bool IsOpenHighShop = false;
            EyouSoft.Model.CompanyStructure.CompanyState CompanyState = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(CompanyId);
            if (CompanyState != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyService ServiceModel = CompanyState.CompanyService; ;
                IsOpenHighShop = ServiceModel.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
                ServiceModel = null;
            }

            CompanyStateModel = CompanyState;
            CompanyState = null;

            if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.地接)
            {
                strCompanyDmainUrl = Domain.UserPublicCenter + "/RouteManage/LocalRouteList.aspx?cid=" + CompanyId + "&CityId=" + CityId;
            }
            else if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.组团 || CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
            {
                strCompanyDmainUrl = Domain.UserPublicCenter + "/Procurement/ProcureDetails.aspx?cid=" + CompanyId;
            }
            else
            {
                if (IsOpenHighShop)
                {
                    if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                    {

                        IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> CompanyDomain = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetDomainList(new string[1] { CompanyId });
                        if (CompanyDomain != null && CompanyDomain.Count > 0)
                        {
                            foreach (EyouSoft.Model.SystemStructure.SysCompanyDomain item in CompanyDomain)
                            {
                                if (item.DomainType == EyouSoft.Model.SystemStructure.DomainType.网店域名)
                                {
                                    strCompanyDmainUrl = "http://" + item.Domain;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(strCompanyDmainUrl) && CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                            {
                                strCompanyDmainUrl = EyouSoft.Common.Utils.GetEShopUrl(CompanyId);
                            }
                            else
                                strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicAdvancedShopUrl(CompanyId);// Domain.SeniorOnlineShop + "/scenicspots/T1/Default.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                        }
                        CompanyDomain = null;
                    }
                    else
                    {
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.车队)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.Car.CarAdvancedShopUrl(CityId, CompanyId);
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicAdvancedShopUrl(CompanyId);
                        } //omain.UserPublicCenter + "/AdvancedShop/AdvancedShopDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;车队旅游用品公用的Url 
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelAdvancedShopUrl(CityId, CompanyId);
                        }
                    }

                }
                else
                {
                    switch (CompanyType)
                    {
                        case EyouSoft.Model.CompanyStructure.CompanyType.专线:
                            strCompanyDmainUrl = EyouSoft.Common.Utils.GetShopUrl(CompanyId, CityId);
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.Car.CardGeneralCarShopUrl(CityId, CompanyId);
                            //Domain.UserPublicCenter + "/CarInfo/GeneralCarShop.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/ShoppingInfo/GeneralShoppingShop.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicDetailsUrl(CompanyId, CityId);
                            //Domain.UserPublicCenter + "/ScenicManage/ScenicDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/HotelManage/HotelDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelDetailsUrl(CityId, CompanyId);
                            //Domain.UserPublicCenter + "/TravelManage/TravelDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                    }
                }
            }

            isOpenHighShop = IsOpenHighShop;

            return strCompanyDmainUrl;
        }


        /// <summary>
        /// 根据公司ID及公司类型返回公司网店URL（有开通高级网店为高级网店URL，未开通为普通网店URL）
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="CompanyType">公司类型</param>
        /// <returns>URL</returns>
        public static string GetCompanyDomain(string CompanyId, EyouSoft.Model.CompanyStructure.CompanyType CompanyType)
        {
            string strCompanyDmainUrl = "";
            //是否开通高级网店服务 
            bool IsOpenHighShop = false;
            EyouSoft.Model.CompanyStructure.CompanyState CompanyState = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(CompanyId);
            if (CompanyState != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyService ServiceModel = CompanyState.CompanyService; ;
                IsOpenHighShop = ServiceModel.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
                ServiceModel = null;
            }
            CompanyState = null;

            if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.地接)
            {
                strCompanyDmainUrl = Domain.UserPublicCenter + "/RouteManage/LocalRouteList.aspx?cid=" + CompanyId;
            }
            else if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.组团 || CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
            {
                strCompanyDmainUrl = Domain.UserPublicCenter + "/Procurement/ProcureDetails.aspx?cid=" + CompanyId;
            }
            else
            {
                if (IsOpenHighShop)
                {
                    if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.专线
                        || CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    {

                        IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> CompanyDomain = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetDomainList(new string[1] { CompanyId });
                        if (CompanyDomain != null && CompanyDomain.Count > 0)
                        {
                            foreach (EyouSoft.Model.SystemStructure.SysCompanyDomain item in CompanyDomain)
                            {
                                if (item.DomainType == EyouSoft.Model.SystemStructure.DomainType.网店域名)
                                {
                                    strCompanyDmainUrl = "http://" + item.Domain;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(strCompanyDmainUrl) && CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                            {
                                strCompanyDmainUrl = EyouSoft.Common.Utils.GetEShopUrl(CompanyId);
                            }
                            else
                                strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicAdvancedShopUrl(CompanyId);   //Domain.SeniorOnlineShop + "/scenicspots/T1/Default.aspx?cid=" + CompanyId;
                        }
                        CompanyDomain = null;

                    }
                    else
                    {
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.车队)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.Car.CarAdvancedShopUrl(CompanyId);
                        } //omain.UserPublicCenter + "/AdvancedShop/AdvancedShopDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;车队旅游用品公用的Url 
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelAdvancedShopUrl(CompanyId);
                        }
                    }

                }
                else
                {
                    switch (CompanyType)
                    {
                        case EyouSoft.Model.CompanyStructure.CompanyType.专线:
                            strCompanyDmainUrl = EyouSoft.Common.Utils.GetShopUrl(CompanyId);
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/CarInfo/GeneralCarShop.aspx?cid=" + CompanyId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/ShoppingInfo/GeneralShoppingShop.aspx?cid=" + CompanyId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicDetailsUrl(CompanyId);
                            //Domain.UserPublicCenter + "/ScenicManage/ScenicDetails.aspx?cid=" + CompanyId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/HotelManage/HotelDetails.aspx?cid=" + CompanyId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/TravelManage/TravelDetails.aspx?cid=" + CompanyId;
                            break;
                    }
                }
            }
            return strCompanyDmainUrl;
        }
        /// <summary>
        /// 根据公司编号获取网店URL信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="CityId">销售城市ID</param>
        /// <returns></returns>
        public static string GetDomainByCompanyId(string CompanyId, int CityId)
        {
            EyouSoft.Model.CompanyStructure.CompanyType? CompanyType = GetCompanyType(CompanyId);
            if (CompanyType.HasValue)
                return GetCompanyDomain(CompanyId, CompanyType.Value, CityId);
            else
                return "";
        }
        /// <summary>
        /// 根据公司ID及公司类型返回公司网店URL（有开通高级网店为高级网店URL，未开通为普通网店URL）
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="CompanyType">公司类型</param>
        /// <param name="CityId">销售城市ID</param>
        /// <returns>URL</returns>
        public static string GetCompanyDomain(string CompanyId, EyouSoft.Model.CompanyStructure.CompanyType CompanyType, int CityId)
        {
            string strCompanyDmainUrl = "";
            //是否开通高级网店服务 
            bool IsOpenHighShop = false;
            EyouSoft.Model.CompanyStructure.CompanyState CompanyState = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(CompanyId);
            if (CompanyState != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyService ServiceModel = CompanyState.CompanyService; ;
                IsOpenHighShop = ServiceModel.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
                ServiceModel = null;
            }
            CompanyState = null;

            if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.地接)
            {
                strCompanyDmainUrl = Domain.UserPublicCenter + "/RouteManage/LocalRouteList.aspx?cid=" + CompanyId + "&CityId=" + CityId;
            }
            else if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.组团 || CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
            {
                strCompanyDmainUrl = Domain.UserPublicCenter + "/Procurement/ProcureDetails.aspx?cid=" + CompanyId;
            }
            else
            {
                if (IsOpenHighShop)
                {
                    if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.专线
                        || CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    {

                        IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> CompanyDomain = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetDomainList(new string[1] { CompanyId });
                        if (CompanyDomain != null && CompanyDomain.Count > 0)
                        {
                            foreach (EyouSoft.Model.SystemStructure.SysCompanyDomain item in CompanyDomain)
                            {
                                if (item.DomainType == EyouSoft.Model.SystemStructure.DomainType.网店域名)
                                {
                                    strCompanyDmainUrl = "http://" + item.Domain;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(strCompanyDmainUrl) && CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                            {
                                strCompanyDmainUrl = EyouSoft.Common.Utils.GetEShopUrl(CompanyId);
                            }
                            else
                                strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicAdvancedShopUrl(CompanyId);// Domain.SeniorOnlineShop + "/scenicspots/T1/Default.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                        }
                        CompanyDomain = null;
                    }
                    else
                    {
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.车队)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.Car.CarAdvancedShopUrl(CityId, CompanyId);
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店)
                        {
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelAdvancedShopUrl(CityId, CompanyId);
                        }
                    }

                }
                else
                {
                    switch (CompanyType)
                    {
                        case EyouSoft.Model.CompanyStructure.CompanyType.专线:
                            strCompanyDmainUrl = EyouSoft.Common.Utils.GetShopUrl(CompanyId, CityId);
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.Car.CardGeneralCarShopUrl(CityId, CompanyId);
                            //Domain.UserPublicCenter + "/CarInfo/GeneralCarShop.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/ShoppingInfo/GeneralShoppingShop.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicDetailsUrl(CompanyId, CityId);
                            //Domain.UserPublicCenter + "/ScenicManage/ScenicDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                            strCompanyDmainUrl = Domain.UserPublicCenter + "/HotelManage/HotelDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                            strCompanyDmainUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelDetailsUrl(CityId, CompanyId);
                            //Domain.UserPublicCenter + "/TravelManage/TravelDetails.aspx?cid=" + CompanyId + "&CityId=" + CityId;
                            break;
                    }
                }
            }
            return strCompanyDmainUrl;
        }
        /// <summary>
        /// 根据指定的公司ID,判断其是否开通了高级网店
        /// </summary>
        /// <param name="companyId">要判断的公司ID</param>
        /// <returns></returns>
        public static bool IsOpenHighShop(string companyId)
        {
            //是否开通高级网店服务 
            bool IsOpenHighShop = false;
            EyouSoft.Model.CompanyStructure.CompanyState CompanyState = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState(companyId);
            if (CompanyState != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyService ServiceModel = CompanyState.CompanyService; ;
                IsOpenHighShop = ServiceModel.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
                ServiceModel = null;
            }
            CompanyState = null;
            return IsOpenHighShop;

        }
        /// <summary>
        /// 错误信息提示
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <param name="type">
        ///  SeniorShop:专线商高级网店是否存在/是否开通
        ///  Shop:专线商普通网店是否存在/是否开通
        ///  EShopTour:高级网店团队不存在
        ///  Tour：前台网站团队不存在
        ///  City:销售城市不存在
        ///  InfoArticle:当前资讯不存在
        ///  ticketscenter:机票供应商
        ///  hotel:酒店,
        ///  info:供求信息不存在
        /// </param>
        public static void ShowError(string msg, string type)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Redirect(Domain.UserPublicCenter + "/ErrorPage.aspx?msg=" + HttpUtility.UrlEncode(msg) + "&type=" + type + "");
        }

        public static void ResponseNoPermit()
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("对不起，你没有该权限!");
            HttpContext.Current.Response.End();

        }
        public static void ResponseNoPermit(string message)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(message);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 运营后台没权限输出
        /// </summary>
        /// <param name="permit">权限枚举</param>
        /// <param name="isGoBack">是否返回上一页</param>
        public static void ResponseNoPermit(YuYingPermission permit, bool isGoBack)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("对不起，你没有”" + permit.ToString() + "“的权限!&nbsp;");
            if (isGoBack)
            {
                HttpContext.Current.Response.Write("<a href='javascript:void(0);' onclick='return history.go(-1);'>返回上一页</a>");
            }
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 运营后台广告管理(图片|图文广告)设置图片大小
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <returns>数组（宽，高）</returns>
        public static int[] GetAdImageSize(EyouSoft.Model.AdvStructure.AdvPosition position)
        {
            int width = 0;
            int height = 0;
            switch (position)
            {
                case EyouSoft.Model.AdvStructure.AdvPosition.MQ主窗体广告:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.MQ群窗口右上广告:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.MQ聊天窗口右上广告:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.MQ聊天窗口右侧广告1:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.MQ聊天窗口右侧广告2:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道供求信息文章页图片广告1:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道促销广告:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道同业之星访谈:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道推荐广告:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道文章及列表右侧1:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道文章及列表右侧2:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道最具人气企业推荐:
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道精品推荐图文:
                    width = 150;
                    height = 100;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.景区频道景区主题广告:
                    width = 225;
                    height = 130;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.景区频道特价门票展示图文:
                    width = 80;
                    height = 70;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.线路频道精品推荐:
                    width = 164;
                    height = 164;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.购物点频道精品推荐图文:
                case EyouSoft.Model.AdvStructure.AdvPosition.车队频道精品推荐图文:
                    width = 140;
                    height = 90;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.酒店频道旗帜广告1:
                case EyouSoft.Model.AdvStructure.AdvPosition.酒店频道旗帜广告2:
                case EyouSoft.Model.AdvStructure.AdvPosition.车队频道旗帜广告1:
                case EyouSoft.Model.AdvStructure.AdvPosition.车队频道旗帜广告2:
                case EyouSoft.Model.AdvStructure.AdvPosition.购物点频道旗帜广告1:
                case EyouSoft.Model.AdvStructure.AdvPosition.购物点频道旗帜广告2:
                case EyouSoft.Model.AdvStructure.AdvPosition.景区频道旗帜广告1:
                case EyouSoft.Model.AdvStructure.AdvPosition.景区频道旗帜广告2:
                case EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道旗帜广告1:
                case EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道旗帜广告2:
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道旗帜广告:    //暂无，
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道行业资讯旗帜广告:  //暂无，
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道同业学堂旗帜广告1://暂无，
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道同业学堂旗帜广告2://暂无，
                    width = 220;
                    height = 90;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.酒店频道特价房展示:
                    width = 80;
                    height = 70;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.首页广告供求信息下部广告:
                    width = 225;
                    height = 60;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.首页广告成功故事图文:
                case EyouSoft.Model.AdvStructure.AdvPosition.首页广告旅游动态图文:
                    width = 103;
                    height = 80;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.首页广告精品推荐图文:
                    width = 126;
                    height = 86;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.焦点图片:
                    width = 564;
                    height = 249;
                    break;
                case EyouSoft.Model.AdvStructure.AdvPosition.首页资讯通栏广告:
                case EyouSoft.Model.AdvStructure.AdvPosition.首页广告首页通栏banner2:
                case EyouSoft.Model.AdvStructure.AdvPosition.首页广告首页通栏banner3:
                case EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道通栏banner:
                case EyouSoft.Model.AdvStructure.AdvPosition.景区频道通栏banner1:
                case EyouSoft.Model.AdvStructure.AdvPosition.景区频道通栏banner2:
                case EyouSoft.Model.AdvStructure.AdvPosition.机票频道通栏banner1:
                case EyouSoft.Model.AdvStructure.AdvPosition.机票频道通栏banner2:
                case EyouSoft.Model.AdvStructure.AdvPosition.酒店频道通栏banner1:
                case EyouSoft.Model.AdvStructure.AdvPosition.酒店频道通栏banner2:
                case EyouSoft.Model.AdvStructure.AdvPosition.线路频道通栏banner1:
                    width = 970;
                    height = 70;
                    break;
            }
            if (width == 0 && height == 0)
            {
                return null;
            }
            else
            {
                return new int[] { width, height };
            }
        }

        /// <summary>
        /// 平台前台URL把CityId参数带上
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="CityId">销售城市ID</param>
        public static string GeneratePublicCenterUrl(string url, int CityId)
        {
            if (url.IndexOf("?") == -1)
            {
                return string.Format("{0}?CityId={1}", url, CityId);
            }
            else
            {
                return string.Format("{0}&CityId={1}", url, CityId);
            }
        }
        public static string[] Split(string Content, string SplitString)
        {
            if ((Content != null) && (Content != string.Empty))
            {
                return Regex.Split(Content, SplitString, RegexOptions.IgnoreCase);
            }
            return new string[1];
        }
        /// <summary>
        /// 获取文字广告的链接地址
        /// </summary>
        /// <param name="typeID">0:表示机票链接过去的详细页面 ；1：表示文字广告连接过去的</param>
        /// <param name="advID">广告编号</param>
        /// <param name="planeTypeID">机票下的信息类型,文字广告连接过来的直接传-1</param>
        /// <param name="CityId">销售城市ID</param>
        /// <returns></returns>
        public static string GetWordAdvLinkUrl(int typeID, int advID, int planeTypeID, int CityId)
        {
            if (typeID == 0)
            {
                string url = string.Format("NewsDetailInfo.aspx?TypeID={0}&NewsID={1}&CityId={2}", planeTypeID, advID, CityId);
                return Domain.UserPublicCenter + "/jipiao_" + EyouSoft.Common.URLREWRITE.Plane.PlanUrlRewrite(url);  //机票咨询链接改写后的URl
                //return string.Format("{0}/PlaneInfo/NewsDetailInfo.aspx?TypeID={1}&NewsID={2}&CityId={3}", Domain.UserPublicCenter, planeTypeID, advID, CityId);
            }
            else
            {
                return string.Format("{0}/PlaneInfo/NewsDetailInfo.aspx?NewsID={1}&CityId={2}", Domain.UserPublicCenter, advID, CityId);
            }

        }
        /// <summary>
        /// 根据反馈类型获得URL
        /// </summary>
        /// <param name="type">1:商铺反馈 2:MQ反馈 3:平台反馈 4:个人中心反馈</param>
        /// <param name="CityId">城市ID</param>
        public static string GetUrlByProposal(int type, int CityId)
        {
            if (type > 0 && type < 5)
            {
                return string.Format(Domain.UserPublicCenter + "/AboutUsManage/Proposal.aspx?type={0}&CityId={1}", type, CityId);
            }
            else
            {
                return string.Format(Domain.UserPublicCenter + "/AboutUsManage/Proposal.aspx?type={0}&CityId={1}", 1, CityId);
            }
        }
        /// <summary>
        /// 帮助中心URL
        /// </summary>
        public static string HelpCenterUrl = Domain.UserPublicCenter + "/HelpCenter/help/Help_Index.aspx";
        /// <summary>
        /// 100_55暂无图片链接
        /// </summary>
        public static readonly string NoLogoImage100_55 = Domain.ServerComponents + "/images/nologo/nologo100_55.gif";
        /// <summary>
        /// 126_86暂无图片链接
        /// </summary>
        public static readonly string NoLogoImage126_86 = Domain.ServerComponents + "/images/nologo/nologo126_86.gif";
        /// <summary>
        /// 92_84暂无图片链接
        /// </summary>
        public static readonly string NoLogoImage92_84 = Domain.ServerComponents + "/images/nologo/nologo92_84.gif";
        #region 专线网店暂无图片链接
        /// <summary>
        /// 专线网店顶部图片暂无图片链接
        /// </summary>
        public static readonly string NoTopbannerImage = Domain.ServerComponents + "/images/shopnoimg/notopbanner.jpg";
        /// <summary>
        /// 专线网店电子名片暂无图片链接
        /// </summary>
        public static readonly string NoCardImage = Domain.ServerComponents + "/images/shopnoimg/nocard.gif";
        /// <summary>
        /// 专线网店轮换广告暂无图片链接
        /// </summary>
        public static readonly string NoRotaImage = Domain.ServerComponents + "/images/shopnoimg/norotapic.gif";
        /// <summary>
        /// 专线网店Logo暂无图片链接
        /// </summary>
        public static readonly string NoLogoImage = Domain.ServerComponents + "/images/shopnoimg/nologo.gif";
        /// <summary>
        /// 专线网店旅游资源推荐暂无图片链接
        /// </summary>
        public static readonly string NoResoImage = Domain.ServerComponents + "/images/shopnoimg/noreso.gif";
        /// <summary>
        /// 专线网店目的地指南暂无图片链接
        /// </summary>
        public static readonly string NoGuidImage = Domain.ServerComponents + "/images/shopnoimg/noguid.gif";
        /// <summary>
        /// 专线普通网店宣传图片
        /// </summary>
        public static readonly string NoShopLeftImage = Domain.ServerComponents + "/images/shopnoimg/noshopimage.gif";
        /// <summary>
        /// 判断专线网店图片是否存在
        /// </summary>
        /// <param name="imgPath">用于显示的图片类型（1:卡片 2:Logo 3:轮换图片 4:资源推荐 5:目的地指南 6:普通网店宣传图片 7：高级网的顶部图片）</param>
        /// <returns></returns>
        public static string GetLineShopImgPath(string imgPath, int Imgtype)
        {
            if (!string.IsNullOrEmpty(imgPath))
            {
                return Domain.FileSystem + imgPath.ToString();
            }
            else
            {
                string noImgPath = "";
                switch (Imgtype)
                {
                    case 1:
                        noImgPath = Utils.NoCardImage;
                        break;
                    case 2:
                        noImgPath = Utils.NoLogoImage;
                        break;
                    case 3:
                        noImgPath = Utils.NoRotaImage;
                        break;
                    case 4:
                        noImgPath = Utils.NoResoImage;
                        break;
                    case 5:
                        noImgPath = Utils.NoGuidImage;
                        break;
                    case 6:
                        noImgPath = Utils.NoShopLeftImage;
                        break;
                }
                return noImgPath;
            }
        }
        #endregion
        #region 处理没有设置图片的链接
        /// <summary>
        /// 处理没有设置图片的链接
        /// </summary>
        /// <param name="oldUrl">原图片地址</param>
        /// <param name="type">1：100*55；2：126*86；3：92*84 默认：92*84</param>
        /// <returns></returns>
        public static string GetNewImgUrl(string oldUrl, int type)
        {
            string url = oldUrl;
            if (oldUrl == null || oldUrl.Trim() == "" || oldUrl.Trim() == "广告默认链接" || oldUrl.Trim() == "广告默认图片")
            {
                switch (type)
                {
                    //100*55
                    case 1:
                        url = Utils.NoLogoImage100_55;
                        break;
                    //126*86
                    case 2:
                        url = Utils.NoLogoImage126_86;
                        break;
                    //92*84
                    case 3:
                        url = Utils.NoLogoImage92_84;
                        break;
                    //自定义
                    default:
                        break;
                }
                return url;
            }
            else
            {
                return Domain.FileSystem + url;

            }
        }
        #endregion


        public const string MQLoginIdKey = "loginuid";
        public const string MQPwKey = "passwd";
        public const string ViewOtherMQUserKey = "im_username";
        public const string MQTransitDesUrlKey = "desurl";

        public static string GetDesPlatformUrlForMQMsg(string desUrl, string mqid, string mqPwd)
        {
            return string.Format("{0}?" + MQTransitDesUrlKey + "={1}&{2}={3}&{4}={5}", "/MQTransit.aspx", HttpUtility.UrlEncode(desUrl), MQLoginIdKey, mqid, MQPwKey, mqPwd);
        }


        /// <summary>
        /// 获得字符串的字节长度
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static int GetByteLength(string value)
        {
            int len = 0;
            if (string.IsNullOrEmpty(value))  //字符串为null或空
                return len;
            else
                return Encoding.Default.GetBytes(value).Length;
        }

        private const string CrossDomainScript = "<script type='text/javascript'>document.domain='.com'</script>";

        public static string GetCrossDomainScript()
        {

            if (HttpContext.Current.Request.Url.HostNameType == UriHostNameType.Dns)
            {
                return CrossDomainScript;
            }
            else
            {
                return string.Empty;
            }
        }


        #region 获得banner的图片or flash
        /// <summary>
        /// 获取flash的html代码 ，使用方式string.Format(GetFlashHtml,文件地址,文件地址)
        /// </summary>
        public const string GetFlashHtml = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"970px\" ><param name=\"movie\" value=\"{0}\" /><param name=\"quality\" value=\"high\" /><param name=\"flashvars\" value=\"\" /><param name=\"AllowScriptAccess\" value=\"always\" /><param name=\"wmode\" value=\"transparent\"><embed src=\"{1}\" quality=\"high\" wmode=\"transparent\"  AllowScriptAccess=\"always\"  pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"970px\"  ></embed></object>";

        public const string GetImgHtml = "<a href=\"{0}\" target=\"{2}\"><img src=\"{1}\" width=\"970px\" height=\"70px\"/></a>";
        /// <summary>
        /// banner广告根据文件返回相应的html
        /// </summary>
        /// <param name="relativePath">相对文件路径 </param>
        /// <param name="RedirectURL">点击跳转的URL</param>
        /// <returns></returns>
        public static string GetImgOrFalash(string relativePath, string redirectURL)
        {
            string html = "";
            if (relativePath.Substring(relativePath.LastIndexOf(".") + 1, relativePath.Length - relativePath.LastIndexOf(".") - 1) == "swf")
            {
                html = string.Format(GetFlashHtml, Domain.FileSystem + relativePath, Domain.FileSystem + relativePath);
            }
            else
            {
                if (redirectURL == Utils.EmptyLinkCode)
                {
                    html = string.Format(GetImgHtml, redirectURL, Domain.FileSystem + relativePath, "_self");
                }
                else
                {
                    html = string.Format(GetImgHtml, redirectURL, Domain.FileSystem + relativePath, "_blank");
                }

            }
            return html;
        }
        #endregion

        #region 专线商高级网店头部
        public const string GetEShopFlashHtml = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"{1}\" height=\"{2}\"><param name=\"movie\" value=\"{0}\" /><param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\"><embed src=\"{0}\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"{1}\" height=\"{2}\" wmode=\"transparent\" ></embed></object>";
        public const string GetEShopImgHtml = "<img src=\"{0}\" width=\"{1}\" height=\"{2}\" />";

        /// <summary>
        /// 根据图片或者flash返回相应的html,默认宽度960 高度147 
        /// </summary>
        /// <param name="redirectURL">点击跳转的URL</param>
        /// <returns>html</returns>
        public static string GetEShopImgOrFalash(string relativePath, string redirectURL)
        {
            return GetEShopImgOrFalash(relativePath, redirectURL, 960, 147);
        }

        /// <summary>
        /// 根据图片或者flash返回相应的html
        /// </summary>
        /// <param name="relativePath">图片路径</param>
        /// <param name="redirectURL">点击跳转的URL</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>html</returns>
        public static string GetEShopImgOrFalash(string relativePath, string redirectURL, int width, int height)
        {
            string html = "";
            if (string.IsNullOrEmpty(relativePath))
            {
                return string.Format(GetEShopImgHtml, NoTopbannerImage, width + "px", height == 0 ? "100%" : height + "px");
            }
            if (relativePath.Substring(relativePath.LastIndexOf(".") + 1, relativePath.Length - relativePath.LastIndexOf(".") - 1) == "swf")
            {
                html = string.Format(GetEShopFlashHtml, Domain.FileSystem + relativePath, width, height == 0 ? "100%" : height.ToString());
            }
            else
            {
                if (redirectURL == Utils.EmptyLinkCode)
                {
                    html = string.Format(GetEShopImgHtml, Domain.FileSystem + relativePath, width + "px", height == 0 ? "100%" : height + "px");
                }
                else
                {
                    html = string.Format(GetEShopImgHtml, Domain.FileSystem + relativePath, width + "px", height == 0 ? "100%" : height + "px");
                }

            }
            return html;
        }
        #endregion

        public const string EmptyLinkCode = "javascript:void(0);";

        /// <summary>
        /// 链接之间用【,】分隔,当找不到城市信息的时候，不需要跳转到切换城市页面的URL
        /// </summary>
        public static readonly string NoNeedLinkToCutCityPage_URLS = ConfigClass.GetConfigString("frontpage_nolinktocutcity_url").ToLower();
        /// <summary>
        /// 链接之间用【,】分隔,指定不需要输出跨域脚本的页面URL
        /// </summary>
        public static readonly string NoNeedResponseCrossDomainScript_URLS = ConfigClass.GetConfigString("frontpage_NoResponseCrossDomainScript_url").ToLower();

        /// <summary>
        /// httpwebrequest 字符编码为utf-8
        /// </summary>
        /// <param name="requestUriString">Internet资源的URI</param>
        /// <returns></returns>
        public static string GetWebRequest(string requestUriString)
        {
            return GetWebRequest(requestUriString, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// httpwebrequest
        /// </summary>
        /// <param name="requestUriString">Internet资源的URI</param>
        /// <param name="encoding">System.Text.Encoding</param>
        /// <returns></returns>
        public static string GetWebRequest(string requestUriString, Encoding encoding)
        {
            StringBuilder responseHtml = new StringBuilder();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                System.IO.Stream resStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(resStream, encoding);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);

                while (count > 0)
                {
                    string s = new String(read, 0, count);
                    responseHtml.Append(s);
                    count = readStream.Read(read, 0, 256);
                }

                resStream.Close();
            }
            catch { }

            return responseHtml.ToString();
        }
        /// <summary>
        /// 获取Internet资源的的页面响应内容
        /// </summary>
        /// <param name="requestUriString">Internet资源的URI</param>
        /// <returns></returns>
        public static string GetWebResult(string requestUriString)
        {
            if (string.IsNullOrEmpty(requestUriString))
                return "";
            WebClient wClient = new WebClient();
            try
            {
                byte[] bt = wClient.DownloadData(requestUriString);
                return System.Text.Encoding.UTF8.GetString(bt);
            }
            catch
            {
                return "";
            }
            finally
            {
                wClient = null;
            }
        }

        /// <summary>
        /// 获取Internet资源的的页面响应内容
        /// </summary>
        /// <param name="requestUriString">Internet资源的URI</param>
        /// <returns></returns>
        public static string GetDefaultWebResult(string requestUriString)
        {
            if (string.IsNullOrEmpty(requestUriString))
                return "";
            WebClient wClient = new WebClient();
            try
            {
                byte[] bt = wClient.DownloadData(requestUriString);
                return System.Text.Encoding.Default.GetString(bt);
            }
            catch
            {
                return "";
            }
            finally
            {
                wClient = null;
            }
        }




        /// <summary>
        /// MQ不在线用户短信提醒 ,注册用户审核通过
        /// </summary>
        /// <param name="toMobile">接收方手机号</param>
        /// <param name="toUserName">接收方用户名</param>
        /// <param name="toMQ">接收方MQ</param>
        public static bool SendSMSForReminderRegPass(string toMobile, string toUserName, string toMQ, int cityId)
        {
            bool result = false;


            string sendMsg = "尊敬的同业114用户，您的账户{0}已通过审核！下载同业MQ与10万旅游同行交流心得。www.tongye114.com";
            string content = string.Format(sendMsg, toUserName);      //发送内容

            if (content.Length > 70)               //字数超过70处理
            {
                string subUserName = toUserName.Substring(0, toUserName.Length - (content.Length - 70));
                content = string.Format(sendMsg, subUserName);
            }
            bool isSendMsg = EyouSoft.BLL.ToolStructure.MsgTipRecord.CreateInstance().IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType.RegPass, EyouSoft.Model.ToolStructure.MsgSendWay.SMS, toMQ, cityId);
            if (isSendMsg)
            {
                //发送通道
                EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[1];

                EyouSoft.BLL.SMSStructure.VoSmsServices.Service sms = new EyouSoft.BLL.SMSStructure.VoSmsServices.Service();
                sms.Timeout = 1000;
                try
                {
                    sms.SendSms(string.Empty, toMobile, content, sendChannel.UserName, sendChannel.Pw);
                    result = true;
                }
                catch (System.Exception)
                {
                    result = false;
                }
                sms = null;
            }

            return result;
        }

        /// <summary>
        /// MQ不在线用户短信提醒 MQ加好友
        /// </summary>
        /// <param name="toMobile"></param>
        /// <param name="companyNameOfSender"></param>
        /// <param name="NameOfSender"></param>
        public static bool SendSMSForReminderAddFriend(string toMq, string toMobile, string fromCompanyName, string fromContactName, int fromCityId)
        {
            bool result = false;
            bool isSend = EyouSoft.BLL.ToolStructure.MsgTipRecord.CreateInstance().IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType.AddFriend, EyouSoft.Model.ToolStructure.MsgSendWay.SMS, toMq, fromCityId);
            if (isSend)
            {
                string EnterpriseId = string.Empty;
                //发送通道
                EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[1];

                EyouSoft.BLL.SMSStructure.VoSmsServices.Service sms = new EyouSoft.BLL.SMSStructure.VoSmsServices.Service();

                string msg = "同业MQ提示：{0} {1} 邀请您通话并加为企业好友，MQ及时登录处理，疑问请电询" + "0571-56884627";

                int subLength = 70 - ((msg.Length - 7) + fromCompanyName.Length + fromContactName.Length);

                if (subLength < 0)
                {
                    string newCompanyName = Utils.GetText(fromCompanyName, fromCompanyName.Length + subLength, false);
                    msg = String.Format(msg, newCompanyName, fromContactName);
                }
                else
                {
                    msg = String.Format(msg, fromCompanyName, fromContactName);
                }
                sms.Timeout = 1000;
                try
                {
                    sms.SendSms(EnterpriseId, toMobile, msg, sendChannel.UserName, sendChannel.Pw);
                    result = true;
                }
                catch (System.Exception)
                {
                    result = false;
                }

                sms = null;
            }

            return result;
        }

        /// <summary>
        ///  MQ不在线用户短信提醒 订单消息
        /// </summary>
        /// <param name="ToCompanyId">接受方公司ID</param>
        /// <param name="fromCompanyName">发送方公司名称</param>
        /// <param name="fromContactName">发送方联系人姓名</param>
        /// <param name="fromCityId">发送方用户所在城市ID</param>
        public static bool SendSMSForReminderOrder(string ToCompanyId, string fromCompanyName, string fromContactName, int fromCityId)
        {
            bool result = false;

            string EnterpriseId = string.Empty;
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(ToCompanyId);
            if (Model != null)
            {
                //接受方手机号码
                string strMobile = Model.ContactInfo.Mobile;
                //发送内容
                string msg = "恭喜！您收到1个订单啦，{0}{1}预订，尽快上线处理，高效的订单处理是把握即时商机的最佳途径！";
                string strContext = string.Format(msg, fromCompanyName, fromContactName);
                if (strContext.Length > 70)
                {
                    int sublengt = fromCompanyName.Length - (strContext.Length - 70);
                    string subsCompName = sublengt > 0 ? fromCompanyName.Substring(0, sublengt) : "";
                    strContext = string.Format(msg, subsCompName, fromContactName);
                }
                //接受方MQID
                string strToMqID = Model.ContactInfo.MQ;

                bool isSendMsg = EyouSoft.BLL.ToolStructure.MsgTipRecord.CreateInstance().IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType.NewOrder, EyouSoft.Model.ToolStructure.MsgSendWay.SMS, strToMqID, fromCityId);

                if (isSendMsg)
                {
                    //发送通道
                    EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[1];
                    EyouSoft.BLL.SMSStructure.VoSmsServices.Service sms = new EyouSoft.BLL.SMSStructure.VoSmsServices.Service();
                    sms.Timeout = 1000;
                    try
                    {
                        sms.SendSms(EnterpriseId, strMobile, strContext, sendChannel.UserName, sendChannel.Pw);
                        result = true;
                    }
                    catch (System.Exception)
                    {
                        result = false;
                    }
                    sms = null;

                }
            }
            Model = null;

            return result;
        }

        /// <summary>
        /// 下订单时给专线商发Email
        /// </summary>
        /// <param name="ToCompanyId">专线商公司ID</param>
        /// <param name="fromCompanyName">发送人公司名称</param>
        /// <param name="fromUserName">发送人用户名</param>
        public static void SendEmailForReminderOrder(string ToCompanyId, string fromCompanyName, string fromUserName)
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(ToCompanyId);
            if (Model != null)
            {
                //接受方用户名
                string strToUserName = Model.AdminAccount.UserName;
                //接受方密码
                string strToPassWord = Model.AdminAccount.UserName;
                //接受方Email地址
                string strToEmail = Model.ContactInfo.Email;

                EyouSoft.Common.Email.ReminderEmailHelper.SendOrderEmail(strToUserName, strToEmail, strToPassWord, fromCompanyName, fromUserName);

            }
            Model = null;
        }

        /// <summary>
        /// 获取价格(过滤小数后末尾的0),专门用于在机票系统中显示机票价格的时候调用过滤
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMoney(decimal value)
        {
            string theMoney = value.ToString();
            if (theMoney.Contains('.'))
            {
                theMoney = Regex.Replace(theMoney, @"(?<=\d)\.0+$|0+$", "", RegexOptions.Multiline);
            }
            return theMoney;
        }
        /// <summary>
        /// 过滤小数后末尾的0，字符串处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FilterEndOfTheZeroDecimal(decimal value)
        {
            string result = value.ToString();
            return FilterEndOfTheZeroString(result);
        }
        /// <summary>
        /// 过滤小数后末尾的0，字符串处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FilterEndOfTheZeroString(string value)
        {
            if (value.Contains('.'))
            {
                value = Regex.Replace(value, @"(?<=\d)\.0+$|0+$", "", RegexOptions.Multiline);
            }
            return value;
        }

        /// <summary>
        /// 手机号码显示加密（是否加密为false不加密原值返回）
        /// </summary>
        /// <param name="NoEncryptMobile">没有加密的手机号码</param>
        /// <param name="IsEncrypt">是否加密</param>
        /// <returns></returns>
        public static string GetEncryptMobile(string NoEncryptMobile, bool IsEncrypt)
        {
            string strReturn = NoEncryptMobile;
            if (!IsEncrypt)
                return strReturn;

            if (string.IsNullOrEmpty(NoEncryptMobile))
                return strReturn;

            if (NoEncryptMobile.Length < 9)
                return strReturn;

            //1398*****26
            strReturn = NoEncryptMobile.Substring(0, 4) + NoEncryptMobile.Remove(0, 9).PadLeft(NoEncryptMobile.Length - 4, '*');

            return strReturn;
        }

        /// <summary>
        /// 过滤非www.tongye114.com超链接处理
        /// </summary>
        /// <param name="strContent">过滤内容</param>
        /// <returns>处理结果</returns>
        public static string RemoveHref(string strContent)
        {
            string strRtn = strContent;

            try
            {
                Regex reg = new Regex(@"(?is)</?a\b.*?href=(['""]?)(?!(?:http://)?www\.tongye114\.com)[^'""\s>]+\1[^>]*>(?<text>(?:(?!</?a).)*)</a>");
                strRtn = reg.Replace(strContent, "$2");
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }
            return strRtn;
        }

        /// <summary>
        /// 广告地址转换成站内地址
        /// </summary>
        /// <param name="url">待转地址</param>
        /// <returns></returns>
        public static string SiteAdvUrl(string url)
        {
            string strUrl = url;

            if (url != EmptyLinkCode)
            {
                strUrl = "/loc/?url=" + HttpUtility.UrlEncode(url);
            }
            return strUrl;
        }
        /// <summary>
        /// 添加link 标记到页面头部
        /// </summary>
        /// <summary>
        /// Adds the generic link to the header.
        /// </summary>
        protected static void AddGenericLink(string type, string relation, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = type;
            link.Attributes["rel"] = relation;
            link.Attributes["href"] = href;
            System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;
            if (page != null && page.Header != null)
            {
                page.Header.Controls.Add(link);
            }
        }

        /// <summary>
        /// 添加Javascript外部文件到html页面
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="placeInBottom">是否添加在html代码底部</param>
        /// <param name="addDeferAttribute">是否添加defer属性</param>
        public static void AddJavaScriptInclude(string url, bool placeInBottom, bool addDeferAttribute)
        {
            System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;
            if (placeInBottom)
            {
                string script = "<script type=\"text/javascript\"" + (addDeferAttribute ? " defer=\"defer\"" : string.Empty) + " src=\"" + url + "\"></script>";
                (page).ClientScript.RegisterStartupScript(page.GetType(), url.GetHashCode().ToString(), script);
            }
            else
            {
                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes["type"] = "text/javascript";
                script.Attributes["src"] = url;
                if (addDeferAttribute)
                {
                    script.Attributes["defer"] = "defer";
                }

                page.Header.Controls.Add(script);
            }
        }

        /// <summary>
        /// 添加css外部文件到html页面
        /// </summary>
        /// <param name="url">The relative URL.</param>
        public static void AddStylesheetInclude(string url)
        {
            AddGenericLink("text/css", "stylesheet", url);
        }
        /// <summary>
        /// 将string数组转换为int数组
        /// </summary>
        /// <param name="StrArr"></param>
        /// <returns></returns>
        public static int[] StringArrToIntArr(string[] StrArr)
        {
            if (StrArr == null || StrArr.Length == 0)
                return null;
            int[] IntArr = new int[StrArr.Length];
            for (int i = 0; i < StrArr.Length; i++)
            {
                int result = 0;
                int.TryParse(StrArr[i], out result);
                IntArr[i] = result;
            }
            return IntArr;
        }

        /// <summary>
        ///  This will automatically issue the 301 moved permanently status code and redirect to the target page
        ///  A permanent redirect status code tells a search engine to update their cache and reassign the old url to the new url
        /// </summary>
        /// <param name="url">the target page redirect to</param>
        /// <param name="endResponse"></param>
        public static void RedirectPermanent(string url, bool endResponse)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", url);

            if (endResponse)
            {
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 从配置文件中获取Google Map Key
        /// </summary>
        /// <returns>Google Map Key</returns>
        public static string GetGoogleMapKeyByXml()
        {
            return ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemIndex">数据索引</param>
        /// <param name="recordSum">数据总数</param>
        /// <param name="TdCount">每个TR中TD的数量</param>
        /// <returns></returns>
        public static string IsOutTrOrTd(int itemIndex, int recordSum, int TdCount)
        {
            //先判断当前itemIndex是否是最后一条数据
            if ((itemIndex + 1) == recordSum)
            {
                System.Text.StringBuilder strb = new System.Text.StringBuilder();
                //判断当前itemInex是否已经到一行的末尾(一行显示4个Td)
                if (((itemIndex + 1) % TdCount) == 0)
                {
                    strb.Append("</tr>");
                }
                else
                {
                    int leaveTdCount = (TdCount - ((itemIndex + 1) % TdCount));
                    for (int i = 0; i < leaveTdCount; i++)
                    {
                        if (i + 1 <= leaveTdCount)
                        {
                            strb.Append("<td align='center' >&nbsp;</td>");
                        }
                    }
                    strb.Append("</tr>");
                }

                return strb.ToString();
            }
            //判断当前itemInex是否已经到一行的末尾(一行显示4个Td)
            else if (((itemIndex + 1) % TdCount) == 0)
            {
                return "</td><tr>";
            }
            else
            {
                return "</td>";
            }
        }
        /// <summary>
        /// 根据公司等级获取图片
        /// </summary>
        /// <param name="companyLev">公司等级</param>
        /// <returns></returns>
        public static string GetCompanyLevImg(CompanyLev companyLev)
        {
            int i = (int)companyLev;
            if (i > 1)
            {
                string[] url = { "renzheng.gif", "zuan.gif", "guan.gif" };
                return "<img src=\"" + ImageManage.GetImagerServerUrl(1) + "/images/new2011/xianlu/" + url[i - 2] + "\" class=\"dengji_img\" title=\"" + companyLev + "\"/>";
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取日期所属周几
        /// </summary>
        /// <param name="index">周的枚举值</param>
        /// <returns></returns>
        public static string GetDayOfWeek(int index)
        {
            string weekday = string.Empty;
            switch (index)
            {
                case 0:
                    weekday = "周日";
                    break;
                case 1:
                    weekday = "周一";
                    break;
                case 2:
                    weekday = "周二";
                    break;
                case 3:
                    weekday = "周三";
                    break;
                case 4:
                    weekday = "周四";
                    break;
                case 5:
                    weekday = "周五";
                    break;
                case 6:
                    weekday = "周六";
                    break;
            }
            return weekday;
        }
    }
}
