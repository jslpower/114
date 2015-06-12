using System;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Web;

namespace EyouSoft.OpenRelation
{
    /// <summary>
    /// EyouSoft Open Relation Utils
    /// </summary>
    /// Author:汪奇志 DateTime:20100-04-01
    public class Utils
    {
        #region static constants
        /// <summary>
        /// WebRequest Timeout
        /// </summary>
        private const int WebRequestTimeout = 300000;
        #endregion

        #region private members
        /// <summary>
        /// get app setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetAppSetting(string key)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(key)) return value;
            
            var settings = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("openRelationAppSettings");
            if (settings != null && settings.Count > 0)
            {
                value = settings[key];
            }

            return value;
        }
        #endregion

        #region public members
        /// <summary>
        /// JSON转为对象
        /// </summary>
        /// <param name="json">JSON DATA</param>
        /// <returns></returns>
        public static T InvertJSON<T>(string json)
        {
            T jsonObject;
            DataContractJsonSerializer outDs = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream outMs = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                jsonObject = (T)outDs.ReadObject(outMs);
            }
            return jsonObject;
        }

        /// <summary>
        /// 对象转为JSON
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="TObject">待转对象</param>
        /// <returns></returns>
        public static string ConvertJSON<T>(T TObject)
        {
            string returnVal = "";
            DataContractJsonSerializer outDs = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream outMs = new MemoryStream())
            {
                outDs.WriteObject(outMs, TObject);
                returnVal = Encoding.UTF8.GetString(outMs.ToArray());
            }
            return returnVal;
        }       

        /// <summary>
        /// 发送指令，远程服务器务必返回EyouSoft.OpenRelation.Model.MResponseInfo json data string
        /// </summary>
        /// <param name="requestInfo">http request post info</param>
        /// <returns></returns>
        public static Model.MResponseInfo CreateRequest(Model.MRequestInfo requestInfo)
        {
            Model.MResponseInfo responseInfo = new Model.MResponseInfo() { IsSuccess = true }; 
            StringBuilder responseText = new StringBuilder();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestInfo.RequestUriString);
            Encoding encode = System.Text.Encoding.UTF8;
            HttpWebResponse response = null;

            #region ready
            request.Timeout = WebRequestTimeout;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";            

            byte[] bytes = encode.GetBytes("request=" + ConvertJSON(requestInfo));
            request.ContentLength = bytes.Length;

            Stream oStreamOut = request.GetRequestStream();
            oStreamOut.Write(bytes, 0, bytes.Length);
            oStreamOut.Close();
            #endregion

            #region request 
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
            catch(Exception e)
            {
                response = null;
                responseInfo.IsSuccess = false;
                responseInfo.ErrorCode = e.Message;
                responseInfo.Desc = "处理请求时发生错误";
            }

            if (!responseInfo.IsSuccess) return responseInfo;
            #endregion

            #region response text handling
            if (response != null)
            {
                try
                {
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
                catch (Exception e)
                {
                    responseInfo.IsSuccess = false;
                    responseInfo.ErrorCode = e.Message;
                    responseInfo.Desc = "处理远程服务器返回信息时发生错误";
                }

                if (!responseInfo.IsSuccess) return responseInfo;
            }
            #endregion

            if (string.IsNullOrEmpty(responseText.ToString()))
            {
                responseInfo.IsSuccess = false;
                responseInfo.ErrorCode = string.Empty;
                responseInfo.Desc = "远程服务器返回空白信息";

                return responseInfo;
            }

            responseInfo = InvertJSON<Model.MResponseInfo>(responseText.ToString());

            return responseInfo;
        }

        /// <summary>
        /// get appkey
        /// </summary>
        /// <returns></returns>
        public static string GetAppKey()
        {
            return GetAppSetting("AppKey");
        }

        /// <summary>
        /// get middleware uri 
        /// </summary>
        /// <returns></returns>
        public static string GetMiddlewareURI()
        {
            return GetAppSetting("MiddlewareURI");
        }

        /// <summary>
        /// get uri
        /// </summary>
        /// <param name="systemType">system type</param>
        /// <returns></returns>
        public static string GetURI(Model.SystemType systemType)
        {
            string key = string.Empty;

            switch (systemType)
            {
                case EyouSoft.OpenRelation.Model.SystemType.Platform: key = "PlatformURI"; break;
                case EyouSoft.OpenRelation.Model.SystemType.TYT: key = "TYTURI"; break;
                case EyouSoft.OpenRelation.Model.SystemType.YYT: key = "YYTURI"; break;
            }

            return GetAppSetting(key);
        }

        /// <summary>
        /// 获取是否启用数据同步操作
        /// </summary>
        /// <returns></returns>
        public static bool GetIsEnable()
        {
            return GetAppSetting("IsEnable") == "1" ? true : false;
        }

        /// <summary>
        /// 验证请求信息
        /// </summary>
        /// <param name="requestString">请求指令字符串 MRequestInfo json data</param>
        /// <param name="requestInfo">out EyouSoft.OpenRelation.Model.MRequestInfo</param>
        /// <returns></returns>
        public static EyouSoft.OpenRelation.Model.MResponseInfo ValidateRequest(string requestString,out EyouSoft.OpenRelation.Model.MRequestInfo requestInfo)
        {
            requestInfo=null;
            EyouSoft.OpenRelation.Model.MResponseInfo responseInfo = new EyouSoft.OpenRelation.Model.MResponseInfo() { IsSuccess = true };

            if (string.IsNullOrEmpty(requestString))
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc = "请求指令(MRequestInfo)为空！";
                return responseInfo;
            }

            requestInfo = EyouSoft.OpenRelation.Utils.InvertJSON<EyouSoft.OpenRelation.Model.MRequestInfo>(requestString);

            if (requestInfo == null)
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc="请求指令(MRequestInfo)为null！";
                return responseInfo;
            }

            if (requestInfo.AppKey != EyouSoft.OpenRelation.Utils.GetAppKey())
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc="AppKey错误！";
                return responseInfo;
            }

            if (!Enum.IsDefined(typeof(EyouSoft.OpenRelation.Model.SystemType), requestInfo.RequestSystemType))
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc="错误的请求系统类型！";
                return responseInfo;
            }

            if (!Enum.IsDefined(typeof(EyouSoft.OpenRelation.Model.InstructionType), requestInfo.InstructionType))
            {
                responseInfo.IsSuccess = false;
                responseInfo.Desc="错误的指令类型！";
                return responseInfo;
            }

            return responseInfo;
        }

        /// <summary>
        /// 获取当前系统处理程序路径
        /// </summary>
        /// <returns></returns>
        public static string GetLocalSyncFilePath()
        {
            return GetAppSetting("LocalSyncFilePath");
        }

        /// <summary>
        /// 获取是否执行数据同步操作
        /// </summary>
        /// <returns></returns>
        public static bool GetIsSync()
        {
            bool isEnable = GetIsEnable();
            string currentExecutionFilePath = HttpContext.Current.Request.CurrentExecutionFilePath.ToLower();
            string localSyncFilePath = GetLocalSyncFilePath().ToLower();

            if (isEnable && currentExecutionFilePath.IndexOf(localSyncFilePath)<0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 将字符串转化为数字(有符号整数) 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key">字符串</param>
        /// <param name="defaultValue">默认值</param>
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
        /// 将字符串转化为数字(有符号整数) 若值不是数字返回0
        /// </summary>
        /// <param name="key">字符串</param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        /// <summary>
        /// 获取用户是否存在验证方式
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.OpenRelation.Model.ExistsUserType GetExistsUserType()
        {
            return (EyouSoft.OpenRelation.Model.ExistsUserType)GetInt(GetAppSetting("ExistsUserType"), (int)EyouSoft.OpenRelation.Model.ExistsUserType.Both);
        }
        #endregion
    }
}
