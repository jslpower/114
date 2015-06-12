using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using Newtonsoft.Json;
using EyouSoft.Common;


namespace UserPublicCenter.AirTickets.VisitorManage
{
    #region 常旅客信息
    public class TicketVistorInfo
    {       
        /// <summary>
        /// 中文名
        /// </summary>
        string cName;
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }
        /// <summary>
        /// 英文名
        /// </summary>
        string eName;
        public string EName
        {
            get { return eName; }
            set { eName = value; }
        }
        /// <summary>
        /// 游客类型
        /// </summary>
        string visitorType;
        public string VisitorType
        {
            get { return visitorType; }
            set { visitorType = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        string sex;
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        string papersType;
        public string PapersType
        {
            get { return papersType; }
            set { papersType = value; }
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        string papersCode;
        public string PapersCode
        {
            get { return papersCode; }
            set { papersCode = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        string telephon;
        public string Telephone
        {
            get { return telephon; }
            set { telephon = value; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

    }
    #endregion
    /// <summary>
    /// 页面功能：从Excels中导入常旅客，批量增加
    /// author:刘咏梅
    /// createTime:2010-10-22
    /// </summary>
    public class GetVisitorList : IHttpHandler
    {
        #region Public Method
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string cid=context.Request["CompanyId"];
            HttpPostedFile file = context.Request.Files[0];
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName) && file.FileName.IndexOf(";") == -1
                && Path.GetExtension(file.FileName).ToLower() == ".xls" || Path.GetExtension(file.FileName).ToLower() == ".xlsx")
            {

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//生成将要在服务器保存的文件名
                string filePath = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/AirTicket/");//要保存的路径
                if (!Directory.Exists(filePath))//判断路径是否存在，不存在就创建
                {
                    Directory.CreateDirectory(filePath);
                }
                string fullFilePath = filePath + fileName;//文件完整的路径
                file.SaveAs(fullFilePath);//保存操作"
                //OleDbConnection oledbCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + fullFilePath);
                OleDbConnection oledbCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\";Data Source=" + fullFilePath);
                oledbCon.Open();
                DataTable table = oledbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);//获取Excel的架构，为了获取表名
                oledbCon.Close();
                try
                {
                    if (table != null)
                    {
                        List<string> tblNames = new List<string>();
                        foreach (DataRow dr in table.Rows)
                        {
                            tblNames.Add(dr[2].ToString());//添加表名
                        }
                        IList<TicketVistorInfo> visitorList = new List<TicketVistorInfo>();
                        foreach (string tblName in tblNames)
                        {
                            visitorList = this.GetExcelTableData(fullFilePath, tblName, visitorList);//根据表名获取数据
                        }
                        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> list = new List<EyouSoft.Model.TicketStructure.TicketVistorInfo>();
                        if (visitorList != null && visitorList.Count != 0)
                        {
                            for (int i =1 ; i < visitorList.Count; i++)//从1开始，过滤掉第一条数据（表头）
                            {                                
                                EyouSoft.Model.TicketStructure.TicketVistorInfo visitorModel = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                                visitorModel.Id = Guid.NewGuid().ToString();
                                visitorModel.CompanyId = cid;
                                visitorModel.CardNo = visitorList[i].PapersCode;
                                visitorModel.NationInfo.NationId = 0;
                                if (visitorList[i].PapersType != null && visitorList[i].PapersType.ToString() != "")
                                    visitorModel.CardType = (EyouSoft.Model.TicketStructure.TicketCardType)int.Parse(visitorList[i].PapersType.ToString());
                                if (visitorList[i].CName != "")
                                    visitorModel.ChinaName = visitorList[i].CName;
                                if (visitorList[i].EName != "")
                                    visitorModel.EnglishName = visitorList[i].EName.ToUpper();
                                visitorModel.IssueTime = DateTime.Now;
                                //if (visitorList[i].Country != "")
                                //    visitorModel.NationInfo.CountryName = visitorList[i].Country;
                                if (visitorList[i].Remark != "")
                                    visitorModel.Remark = visitorList[i].Remark;
                                if (visitorList[i].Sex.ToString() != "")
                                    visitorModel.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)(int.Parse(visitorList[i].Sex.ToString()));
                                if (visitorList[i].Telephone != "")
                                    visitorModel.ContactTel = visitorList[i].Telephone;
                                if (visitorList[i].VisitorType.ToString() != "")
                                    visitorModel.VistorType = (EyouSoft.Model.TicketStructure.TicketVistorType)int.Parse(visitorList[i].VisitorType.ToString());
                                list.Add(visitorModel);
                            }
                        }
                        File.Delete(fullFilePath);//删除文件
                        if (visitorList != null && visitorList.Count != 0)
                        {
                            int result = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().AddTicketVisitorList(list);
                            if (result > 0)
                            {
                                if (result ==Convert.ToInt32(visitorList.Count-1))
                                    context.Response.Write("001");
                                else
                                    context.Response.Write(result.ToString());
                            }
                            else
                                context.Response.Write("0");                           
                        }
                    }
                }catch(Exception ex)//捕获异常
                {
                    context.Response.Write("-1");
                }               
            }
        }
        #endregion

        #region 获取表中的数据
        /// <summary>
        /// 获取Excel表中的数据
        /// </summary>
        /// <param name="fPath">文件路径</param>
        /// <param name="tableName">表名</param>
        /// <param name="visitorList">数据源集合</param>
        /// <returns></returns>
        private IList<TicketVistorInfo> GetExcelTableData(string fPath, string tableName, IList<TicketVistorInfo> visitorList)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\";Data Source=" + fPath;
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fPath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbCon = new OleDbConnection(strConn);
            oledbCon.Open();
            string strExcel = "";
            OleDbDataAdapter oledbCmd = null;
            strExcel = "select * from [" + tableName + "]";
            oledbCmd = new OleDbDataAdapter(strExcel, strConn);
            DataTable table = new DataTable();
            oledbCmd.Fill(table);
            oledbCon.Close();
            List<string> visitorsType = new List<string>();
            visitorsType.Add("成人");
            visitorsType.Add("儿童");
            visitorsType.Add("婴儿");
            List<string> genders = new List<string>();
            genders.Add("男");
            genders.Add("女");
            List<string> papersType = new List<string>();
            papersType.Add("身份证");
            papersType.Add("护照");
            papersType.Add("军官证");
            papersType.Add("台胞证");
            papersType.Add("港澳通行证");
            foreach (DataRow dr in table.Rows)//遍历数据行
            {
                TicketVistorInfo visitorObj = new TicketVistorInfo();//创建常旅客对象
                int itemlength = dr.ItemArray.Length;
              
                if (itemlength > 0)
                    visitorObj.CName =Utils.InputText(dr[0].ToString(),13);
                if (itemlength > 1)
                    visitorObj.EName =Utils.InputText(dr[1].ToString().ToString(),27);
                if (itemlength > 2)
                    visitorObj.VisitorType = visitorsType.IndexOf(dr[2].ToString()).ToString();

                if (itemlength > 3)
                    visitorObj.Sex = genders.IndexOf(dr[3].ToString()).ToString();

                if (itemlength > 4)
                    visitorObj.PapersType = papersType.IndexOf(dr[4].ToString()).ToString();

                if (itemlength > 5)
                    visitorObj.PapersCode = dr[5].ToString();
                //dr[5].ToString().Replace("\"", "");
                if (itemlength > 6)
                    visitorObj.Telephone = dr[6].ToString();
                //if (itemlength > 7)
                //    visitorObj.Country = dr[7].ToString();
                if (itemlength > 7)
                    visitorObj.Remark =Utils.InputText(dr[7].ToString(),450);

                if (!string.IsNullOrEmpty(visitorObj.CName)
                    || !string.IsNullOrEmpty(visitorObj.EName)                   
                    && !string.IsNullOrEmpty(visitorObj.VisitorType)
                    && !string.IsNullOrEmpty(visitorObj.Sex)
                    && !string.IsNullOrEmpty(visitorObj.PapersType)
                    && !string.IsNullOrEmpty(visitorObj.PapersCode)
                    && !string.IsNullOrEmpty(visitorObj.Telephone)
                    //&& !string.IsNullOrEmpty(visitorObj.Country)
                    && !string.IsNullOrEmpty(visitorObj.Remark))
                    visitorList.Add(visitorObj);//添加到集合
            }
            return visitorList;            
        }
        #endregion
        
        #region Public Members
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
