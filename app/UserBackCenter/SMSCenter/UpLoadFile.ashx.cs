using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UpLoadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files[0];
            IList<ImportMobileInfo> mobiles = new List<ImportMobileInfo>();

            if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName) && file.FileName.IndexOf(";") == -1 && this.IsAllowedExt(file.FileName))
            {
                //获取文件扩展名
                string fileExtension = Path.GetExtension(file.FileName);

                //设置文件名
                Random rnd = new Random();
                string saveFileName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + fileExtension;
                rnd = null;

                //保存文件
                string dPath = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/SMS/");
                if (!Directory.Exists(dPath))
                {
                    Directory.CreateDirectory(dPath);
                }
                string fPath = dPath + saveFileName;
                file.SaveAs(fPath);

                mobiles = this.processFile(fPath);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(mobiles);

                context.Response.Write(output);
            }
        }

        /// <summary>
        /// 是否允许的文件类型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsAllowedExt(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            string fileExtension = Path.GetExtension(path).ToLower();

            if (fileExtension == ".txt" || fileExtension == ".xls")
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 文件处理
        /// </summary>
        /// <param name="fPath">文件路径</param>
        private IList<ImportMobileInfo> processFile(string fPath)
        {
            IList<ImportMobileInfo> mobiles = new List<ImportMobileInfo>();
            string fileExtension = Path.GetExtension(fPath).ToLower();

            switch (fileExtension)
            {
                case ".xls":
                    mobiles = this.processXLS(fPath);
                    break;
                case ".txt":
                    mobiles = this.processTXT(fPath);
                    break;
            }

            File.Delete(fPath);

            return mobiles;
        }

        /// <summary>
        /// 处理Microsoft Office Excel文件
        /// </summary>
        /// <param name="fPath"></param>
        private IList<ImportMobileInfo> processXLS(string fPath)
        {
            IList<string> tblNames = this.GetExcelTableName(fPath);
            IList<ImportMobileInfo> mobiles = new List<ImportMobileInfo>();

            if (tblNames != null && tblNames.Count > 0)
            {
                foreach (string tblName in tblNames)
                {
                    mobiles = this.GetExcelTableData(fPath, tblName, mobiles);
                }
            }

            return mobiles;
        }

        /// <summary>
        /// 处理文本文件
        /// </summary>
        /// <param name="fPath"></param>
        private IList<ImportMobileInfo> processTXT(string fPath)
        {
            //读文件
            StreamReader fileStream = new StreamReader(fPath, Encoding.Default);
            string s = fileStream.ReadToEnd();
            fileStream.Close();

            IList<ImportMobileInfo> mobiles = new List<ImportMobileInfo>();

            s = s.Replace("\t", "~@~").Replace(" ", "~@~").Replace("\r\n", "~||~");
            s = Regex.Replace(s, @"(~@~)+", "~@~");
            string[] separatorRow = { "~||~" };
            string[] separatorCol = { "~@~" };
            string[] arrRow = s.Split(separatorRow, StringSplitOptions.RemoveEmptyEntries);

            foreach (string row in arrRow)
            {
                string[] arrCol = row.Split(separatorCol, StringSplitOptions.RemoveEmptyEntries);

                if (arrCol.Length < 1) { continue; }

                try
                {
                    ImportMobileInfo mobileInfo = new ImportMobileInfo();
                    mobileInfo.Mobile = arrCol[0].ToString();

                    if (arrCol.Length > 1)
                    {
                        mobileInfo.CompanyName = arrCol[1].ToString();
                    }

                    if (arrCol.Length > 2)
                    {
                        mobileInfo.FullName = arrCol[2].ToString();
                    }

                    mobiles.Add(mobileInfo);
                }
                catch { }
            }

            return mobiles;
        }

        /// <summary>  
        /// 获取EXCEL的表 表名字列   
        /// </summary>  
        /// <param name="fPath">Excel文件</param>  
        /// <returns>数据表</returns>  
        private IList<string> GetExcelTableName(string fPath)
        {
            IList<string> tblNames = new List<string>();

            try
            {
                if (System.IO.File.Exists(fPath))
                {
                    OleDbConnection excelConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + fPath);
                    excelConn.Open();
                    DataTable tb = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    excelConn.Close();
                    excelConn.Dispose();

                    if (tb != null)
                    {
                        foreach (DataRow dr in tb.Rows)
                        {
                            tblNames.Add(dr[2].ToString());
                        }
                    }
                }
            }
            catch { }

            return tblNames;
        }


        /// <summary>
        /// 获取EXCEL表里的数据
        /// </summary>
        /// <param name="fPath"></param>
        /// <param name="tableName"></param>
        /// <param name="mobiles"></param>
        /// <returns></returns>
        private IList<ImportMobileInfo> GetExcelTableData(string fPath, string tableName, IList<ImportMobileInfo> mobiles)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fPath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [" + tableName + "]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            myCommand.Dispose();
            conn.Close();
            conn.Dispose();

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                //第一个工作表时取列名
                if (mobiles.Count == 0)
                {
                    ImportMobileInfo mobileInfo = new ImportMobileInfo();

                    if (dt.Columns.Count > 0)
                    {
                        mobileInfo.Mobile = dt.Columns[0].ColumnName;
                    }

                    if (dt.Columns.Count > 1)
                    {
                        mobileInfo.CompanyName = dt.Columns[1].ColumnName;
                    }

                    if (dt.Columns.Count > 2)
                    {
                        mobileInfo.FullName = dt.Columns[2].ColumnName;
                    }

                    mobiles.Add(mobileInfo);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    ImportMobileInfo mobileInfo = new ImportMobileInfo();

                    if (dr.ItemArray.Length > 0)
                    {
                        mobileInfo.Mobile = dr[0].ToString();
                    }

                    if (dr.ItemArray.Length > 1)
                    {
                        mobileInfo.CompanyName = dr[1].ToString();
                    }

                    if (dr.ItemArray.Length > 2)
                    {
                        mobileInfo.FullName = dr[2].ToString();
                    }

                    if (!(string.IsNullOrEmpty(mobileInfo.Mobile) && string.IsNullOrEmpty(mobileInfo.CompanyName) && string.IsNullOrEmpty(mobileInfo.FullName)))
                    {
                        mobiles.Add(mobileInfo);
                    }
                }
            }

            return mobiles;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 导入手机信息业务实体
    /// </summary>
    public class ImportMobileInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ImportMobileInfo() { }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }
    }
}
