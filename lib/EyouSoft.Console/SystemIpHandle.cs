using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;

namespace EyouSoft.ConsoleApp
{
    /// <summary>
    /// 系统Ip库处理类
    /// </summary>
    public class SystemIpHandle : DALBase
    {
        #region 周文超测试函数

        /// <summary>
        /// 生成更新系统Ip的Sql语句
        /// </summary>
        public void GetSystemIp()
        {
            BuildSql();
        }

        private void BuildSql()
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand("select IpStartChar,IpEndChar,IpAddress from tbl_SystemIP");

            long IpStart = 0, IpEnd = 0;
            StringBuilder strSql = new StringBuilder();
            int i = 0, page = 0;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                while (dr.Read())
                {
                    if (string.IsNullOrEmpty(dr["IpStartChar"].ToString()) || string.IsNullOrEmpty(dr["IpEndChar"].ToString()) || string.IsNullOrEmpty(dr["IpAddress"].ToString()))
                        continue;

                    IpStart = ipToLong(dr["IpStartChar"].ToString());
                    IpEnd = ipToLong(dr["IpEndChar"].ToString());

                    strSql.Append(" go ");
                    strSql.Append(Environment.NewLine);
                    strSql.Append(" declare @ProvinceId int; ");
                    strSql.Append(Environment.NewLine);
                    strSql.Append(" declare @CityId int; ");
                    strSql.Append(Environment.NewLine);
                    strSql.Append(" set @ProvinceId = 0; ");
                    strSql.Append(Environment.NewLine);
                    strSql.Append(" SET @CityId = 0; ");
                    strSql.Append(Environment.NewLine);
                    strSql.AppendFormat(" select @ProvinceId = Id from tbl_SysProvince where charindex(ProvinceName,'{0}',0) > 0;  ", dr["IpAddress"].ToString().Replace("'", ""));
                    strSql.Append(Environment.NewLine);
                    strSql.AppendFormat(" select @CityId = Id from tbl_SysCity where charindex(CityName,'{0}',0) > 0;  ", dr["IpAddress"].ToString().Replace("'", ""));
                    strSql.Append(Environment.NewLine);
                    strSql.AppendFormat(" UPDATE [tbl_SystemIP] SET [ProvinceId] = @ProvinceId, [CityId] = @CityId, [IpStartNum] = {0},[IpEndNum] = {1} WHERE [IpStartChar] = '{2}' and [IpEndChar] = '{3}' and [IpAddress] = '{4}'; ", IpStart, IpEnd, dr["IpStartChar"].ToString(), dr["IpEndChar"].ToString(), dr["IpAddress"].ToString());
                    strSql.Append(Environment.NewLine);
                    strSql.Append(" go ");
                    strSql.Append(Environment.NewLine);

                    i++;

                    if (i == 50000)
                    {
                        page++;
                        WriteFile(strSql, page);
                        if (strSql != null && strSql.Length > 0) strSql.Remove(0, strSql.Length);
                        i = 0;
                    }
                }
            }
            if (i > 0)
            {
                page++;
                WriteFile(strSql, page);
                if (strSql != null && strSql.Length > 0) strSql.Remove(0, strSql.Length);
                i = 0;
            }

            Console.WriteLine(page);

            GC.Collect();

            //UpdateIp(strSql.ToString());
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="strSql">Sql语句集合</param>
        /// <param name="index">文件索引</param>
        private void WriteFile(StringBuilder strSql, int index)
        {
            using (StreamWriter File = new StreamWriter(System.Environment.CurrentDirectory + "\\SystemIpSql\\Sql语句" + index.ToString() + ".txt", false))
            {
                File.Write(strSql.ToString());
                File.Flush();
                File.Close();
            }
        }

        /// <summary>
        /// 将127.0.0.1 形式的IP地址转换成10进制整数，这里没有进行任何错误处理
        /// </summary>
        /// <param name="strIP">IP地址转换</param>
        /// <returns>返回0进制整数</returns>
        private long ipToLong(string strIP)
        {
            if (string.IsNullOrEmpty(strIP))
                return 0;

            string[] strIPs = strIP.Trim().Split('.');
            if (strIPs.Length != 4)
                return 0;

            long[] ip = new long[4];
            for (int i = 0; i < strIPs.Length; i++)
            {
                ip[i] = long.Parse(strIPs[i]);
            }

            return ip[0] * 256 * 256 * 256 + ip[1] * 256 * 256 + ip[2] * 256 + ip[3];
        }

        #endregion
    }
}
