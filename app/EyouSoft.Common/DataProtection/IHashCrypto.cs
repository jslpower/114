using System;

namespace EyouSoft.Common.DataProtection
{
	/// <summary>
	/// 加密解密基类接口文件
	/// Author:陈志仁 2007-11-06
	/// </summary>
	public interface IHashCrypto
	{
		#region 构造函数
		/// <summary>
		/// 加密 密钥
		/// </summary>
		string Key
		{
			set;
			get;
		}
		/// <summary>
		/// 初始向量
		/// </summary>
		string IV
		{
			set;
			get;
		}
		#endregion
		#region 全局方法
		/// <summary>
		/// 32位MD5 加密
		/// </summary>
		/// <param name="inputString">要加密的字符串</param>
		/// <returns>返回加密后的字符串</returns>
		string MD5Encrypt(string inputString);
		/// <summary>
		/// 16位MD5 加密
		/// </summary>
		/// <param name="inputString">要加密的字符串</param>
		/// <param name="hashLength">加密长度</param>
		/// <returns>返回加密后的字符串</returns>
		string MD5Encrypt16(string inputString);
		/// <summary>
		/// SHA算法，默认为SHA1
		/// </summary>
		/// <param name="inputString">要加密的字符串</param>
		/// <returns>返回加密后的字符串</returns>
		string SHAEncrypt(string inputString);
		/// <summary>
		/// 重载SHA算法 默认 256
		/// </summary>
		/// <param name="inputString">要加密的字符串</param>
		/// <param name="HashLength">加密长度 可分为 128,256,384,512 这几种长度</param>
		/// <returns>返回加密后的字符串</returns>
		string SHAEncrypt(string inputString,int HashLength);
		/// <summary>
		/// DES 加密
		/// </summary>
		/// <param name="Values">要加密的字符串</param>
		/// <returns>要解密的字符串</returns>
		string DESEncrypt(string Values);
		/// <summary>
		/// DES 解密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>解密后的字符串</returns>
		string DeDESEncrypt(string Values);
		/// <summary>
		/// 3 DES数据加密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>加密后的字符串</returns>
		string TripleDesEncrypt(string Values);
		/// <summary>
		/// 3 DES 解密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>解密后的字符串</returns>
		string DeTripleDesEncrypt(string Values);
		/// <summary>
		/// Rijndael数据加密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>加密后的字符串</returns>
		string RijndaelEncrypt(string Values);
		/// <summary>
		/// Rijndael 解密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>解密后的字符串</returns>
		string DeRijndaelEncrypt(string Values);
		/// <summary>
		/// RC2 数据加密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>加密后的字符串</returns>
		string RC2Encrypt(string Values);
		/// <summary>
		/// RC2 解密
		/// </summary>
		/// <param name="Values">加密后的字符串</param>
		/// <returns>解密后的字符串</returns>
		string DeRC2Encrypt(string Values);
		#endregion
	}
}
