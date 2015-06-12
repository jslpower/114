using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common
{
    public class QuotedPrintable
    {

        private const byte EQUALS = 61;
        private const byte CR = 13;
        private const byte LF = 10;
        private const byte SPACE = 32;
        private const byte TAB = 9;

        #region 编码
        /// <summary>  
        /// Encodes a string to QuotedPrintable  
        /// </summary>  
        /// <param name="_ToEncode">String to encode</param>  
        /// <returns>QuotedPrintable encoded string</returns>  
        public static string Encode(string _ToEncode)
        {
            StringBuilder Encoded = new StringBuilder();
            string hex = string.Empty;

            byte[] bytes = Encoding.UTF8.GetBytes(_ToEncode);


            for (int i = 0; i < bytes.Length; i++)
            {
                //these characters must be encoded  
                if ((bytes[i] < 33 || bytes[i] > 126 || bytes[i] == EQUALS) && bytes[i] != CR && bytes[i] != LF && bytes[i] != SPACE)
                {
                    if (bytes[i].ToString("X").Length < 2)
                    {
                        hex = "0" + bytes[i].ToString("X");
                        Encoded.Append("=" + hex);
                    }
                    else
                    {
                        hex = bytes[i].ToString("X");
                        Encoded.Append("=" + hex);
                    }
                }
                else
                {
                    //check if index out of range  
                    if ((i + 1) < bytes.Length)
                    {
                        //if TAB is at the end of the line - encode it!  
                        if ((bytes[i] == TAB && bytes[i + 1] == LF) || (bytes[i] == TAB && bytes[i + 1] == CR))
                        {
                            Encoded.Append("=0" + bytes[i].ToString("X"));
                        }
                        //if SPACE is at the end of the line - encode it!  
                        else if ((bytes[i] == SPACE && bytes[i + 1] == LF) || (bytes[i] == SPACE && bytes[i + 1] == CR))
                        {
                            Encoded.Append("=" + bytes[i].ToString("X"));
                        }
                        else
                        {
                            Encoded.Append(System.Convert.ToChar(bytes[i]));
                        }
                    }
                    else
                    {
                        Encoded.Append(System.Convert.ToChar(bytes[i]));
                    }
                }
            }

            return Encoded.ToString();
        }

        #endregion

        #region 解码
        /// <summary>  
        /// Decodes a QuotedPrintable encoded string   
        /// </summary>  
        /// <param name="_ToDecode">The encoded string to decode</param>  
        /// <returns>Decoded string</returns>
        public static string Decode(string _ToDecode)
        {

            char[] chars = _ToDecode.ToCharArray();

            byte[] bytes = new byte[chars.Length];

            int bytesCount = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '=')
                {
                    bytes[bytesCount++] = System.Convert.ToByte(int.Parse(chars[i + 1].ToString() + chars[i + 2].ToString(), System.Globalization.NumberStyles.HexNumber));

                    i += 2;
                }
                else
                {
                    bytes[bytesCount++] = System.Convert.ToByte(chars[i]);
                }
            }

            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytesCount);
        }
        #endregion
    }
}
