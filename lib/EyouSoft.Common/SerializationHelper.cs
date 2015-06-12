using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;

namespace EyouSoft.Common
{
	/// <summary>
	/// SerializationHelper 的摘要说明。
	/// </summary>
	public class SerializationHelper
	{
        private SerializationHelper()
		{}

        /// <summary>
        /// JSON转为对象
        /// </summary>
        /// <param name="json">待转JSON</param>
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
        /// <param name="json">待转对象</param>
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

        private static Dictionary<int, XmlSerializer> serializer_dict = new Dictionary<int, XmlSerializer>();

        public static XmlSerializer GetSerializer(Type t)
        {
            int type_hash = t.GetHashCode();

            if (!serializer_dict.ContainsKey(type_hash))
                serializer_dict.Add(type_hash, new XmlSerializer(t));

            return serializer_dict[type_hash];
        }


		/// <summary>
		/// 反序列化
		/// </summary>
		/// <param name="type">对象类型</param>
		/// <param name="filename">文件路径</param>
		/// <returns></returns>
		public static object Load(Type type, string filename)
		{
			FileStream fs = null;
			try
			{
				// open the stream...
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				XmlSerializer serializer = new XmlSerializer(type);
				return serializer.Deserialize(fs);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(fs != null)
					fs.Close();
			}
		}


		/// <summary>
		/// 序列化
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="filename">文件路径</param>
		public static bool Save(object obj, string filename)
		{
            bool success = false;

			FileStream fs = null;
			// serialize it...
			try
			{
				fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
				serializer.Serialize(fs, obj);
                success = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(fs != null)
					fs.Close();
			}
            return success;

		}

        /// <summary>
        /// xml序列化成字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string Serialize(object obj)
        {
            string returnStr = "";
            XmlSerializer serializer = GetSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = null;
            StreamReader sr = null;
            try
            {
                xtw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                sr = new StreamReader(ms);
                returnStr = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
                if (sr != null)
                    sr.Close(); 
                ms.Close();
            }
            return returnStr;

        }

        public static object DeSerialize(Type type, string s)
        { 
            byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
            try
            {
                XmlSerializer serializer = GetSerializer(type);
                return serializer.Deserialize(new MemoryStream(b));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}
