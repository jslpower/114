using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Data;
using Newtonsoft.Json;

namespace UserPublicCenter.AirTickets.TeamBook
{

    public class TourInfo
    {
        string tourName;

        public string TourName
        {
            get { return tourName; }
            set { tourName = value; }
        }

       

      

     
        string cerNo;

        public string CerNo
        {
            get { return cerNo; }
            set { cerNo = value; }
        }
    
    }
    /// <summary>
    /// 页面功能：从Excel导入旅客信息
    /// 开发人:xuty 开发时间:2010-10-21
    /// </summary>
    public class LoadTourist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file=context.Request.Files[0];
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName) && file.FileName.IndexOf(";") == -1 && ".xls,.xlsx".Split(',').Contains(Path.GetExtension(file.FileName).ToLower()))
            {
               
                string fileName =  Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);//生成将要在服务器保存的文件名
                string filePath = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/AirTicket/");//要保存的路径
                if (!Directory.Exists(filePath))//判断路径是否存在，不存在就创建
                {
                    Directory.CreateDirectory(filePath); 
                }
                string fullFilePath = filePath + fileName;//文件完整的路径
                file.SaveAs(fullFilePath);//保存操作
                OleDbConnection oledbCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\";Data Source=" + fullFilePath);
                oledbCon.Open();
                DataTable table = oledbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);//获取Excel的架构，为了获取表名
                oledbCon.Close();
                if (table != null)
                {
                    List<string> tblNames = new List<string>();
                    foreach (DataRow dr in table.Rows)
                    {
                        tblNames.Add(dr[2].ToString());//添加表名
                    }
                    IList<TourInfo> tourList = new List<TourInfo>();
                    foreach (string tblName in tblNames)
                    {
                       this.GetExcelTableData(fullFilePath, tblName, tourList);//根据表名获取数据
                    }
                    string tourStr = JsonConvert.SerializeObject(tourList); //序列化旅客信息数据
                    File.Delete(fullFilePath);//删除文件
                    context.Response.Write(tourStr);
                }
                
            }
        }
        //获取表中的数据
        private void  GetExcelTableData(string fPath, string tableName, IList<TourInfo> tourList)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\";Data Source=" + fPath;
            OleDbConnection oledbCon = new OleDbConnection(strConn);
            oledbCon.Open();
            string strExcel = "";
            OleDbDataAdapter oledbCmd = null;
            strExcel = "select * from [" + tableName + "]";
            oledbCmd = new OleDbDataAdapter(strExcel, strConn);
            DataTable table = new DataTable();
            oledbCmd.Fill(table);  
            oledbCon.Close();

            int rowIndex = 1;
            foreach (DataRow dr in table.Rows)//遍历数据行
            {
                if (rowIndex == 1)
                {
                    rowIndex++;
                    continue;
                }
               
                TourInfo tourObj = new TourInfo();//创建旅游对象
                int itemlength = dr.ItemArray.Length;
                if(itemlength>0)
                tourObj.TourName = dr[0].ToString();
                if (itemlength > 1)
                    tourObj.CerNo = dr[1].ToString();
                 
                if(!string.IsNullOrEmpty(tourObj.TourName)&&!string.IsNullOrEmpty(tourObj.CerNo))
                tourList.Add(tourObj);//添加到集合
            }
           
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        
    }
}
