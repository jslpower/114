using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.AirTickets.history
{
    public partial class ExcelInput : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = FileUpload1.PostedFile;
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName) && file.FileName.IndexOf(";") == -1
                && Path.GetExtension(file.FileName).ToLower() == ".xls" || Path.GetExtension(file.FileName).ToLower() == ".xlsx")
            {
                string fileName = "uploadExcel" + Path.GetExtension(file.FileName);//生成将要在服务器保存的文件名
                string filePath = System.Web.HttpContext.Current.Server.MapPath("/AirTickets/history/uploadExcelFile/");//要保存的路径
                if (!Directory.Exists(filePath))//判断路径是否存在，不存在就创建
                {
                    Directory.CreateDirectory(filePath);
                }
                string fullFilePath = filePath + fileName;//文件完整的路径
                file.SaveAs(fullFilePath);//保存操作"

                OleDbConnection oledbCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;"
                    + "Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\";Data Source=" + fullFilePath);
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
                        IList<order> orderList = new List<order>();
                        foreach (string tblName in tblNames)
                        {
                            orderList = this.GetExcelTableData(fullFilePath, tblName, orderList);//根据表名获取数据
                        }
                        File.Delete(fullFilePath);//删除文件
                        if (orderList != null && orderList.Count != 0)
                        {
                            foreach (order o in orderList)
                            {
                                this.Add(o);
                            }
                        }
                    }
                    EyouSoft.Common.Function.MessageBox.Show(this,"导入成功！");
                   
                }
                catch (Exception ex)//捕获异常
                {
                    EyouSoft.Common.Function.MessageBox.Show(this,"导入失败！");
                }
            }
        }

        private IList<order> GetExcelTableData(string fPath, string tableName, IList<order> orderList)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;"
                    + "Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\";Data Source=" + fPath;
            OleDbConnection oledbCon = new OleDbConnection(strConn);
            oledbCon.Open();
            string strExcel = "";
            OleDbDataAdapter oledbCmd = null;
            strExcel = "select * from [" + tableName + "]";
            oledbCmd = new OleDbDataAdapter(strExcel, strConn);
            DataTable table = new DataTable();
            oledbCmd.Fill(table);
            oledbCon.Close();

            foreach (DataRow dr in table.Rows)//遍历数据行
            {
                if (dr != null && dr[0].ToString() != "")
                {
                    order orderObj = new order();//创建客户信息
                    orderObj.state = dr["机票状态"].ToString();
                    orderObj.type = dr["机票类型"].ToString();
                    orderObj.clearCode = dr["结算码"].ToString();
                    orderObj.number = dr["票号"].ToString();
                    orderObj.clearCodeNumber = dr["结算代码票号"].ToString();
                    orderObj.flightNumber = dr["航班号"].ToString();
                    if (dr[6].ToString() != "")
                        orderObj.ticketTime = Convert.ToDateTime(dr["出票时间"].ToString());
                    if (dr[7].ToString() != "")
                        orderObj.payTime = Convert.ToDateTime(dr["支付时间"].ToString());
                    if (dr[8].ToString() != "")
                        orderObj.flightTime = Convert.ToDateTime(dr["乘机时间"].ToString());
                    orderObj.orderPeople = dr["预订员"].ToString();
                    orderObj.Carrier = dr["承运人"].ToString();
                    orderObj.Discount = dr["舱位折扣"].ToString();
                    orderObj.Passenger = dr["乘客"].ToString();
                    orderObj.Voyage = dr["航程"].ToString();
                    orderObj.PNR = dr["PNR"].ToString();
                    orderObj.ticketPrice = dr["票价"].ToString();
                    orderObj.taxPrice = dr["税费"].ToString();
                    orderObj.ranyouFee = dr["燃油费"].ToString();
                    orderObj.jijianFee = dr["基建费"].ToString();
                    orderObj.rebate = dr["返点"].ToString();
                    orderObj.shouxuFee = dr["手续费"].ToString();
                    orderObj.yongjin = dr["佣金"].ToString();
                    orderObj.tgqFee = dr["退改签手续费"].ToString();
                    orderObj.payFee = dr["支付手续费"].ToString();
                    orderObj.actualPayPrice = dr["实付款"].ToString();
                    orderObj.buyer = dr["采购商"].ToString();
                    string[] arryBuyer = dr["采购商"].ToString().Replace("yinuo", ",").Split(',');
                    orderObj.buyername = arryBuyer[0];
                    if (arryBuyer.Length >= 2 )
                        orderObj.buyerOPUserId = arryBuyer[1];
                    else
                        orderObj.buyerOPUserId = "";
                    orderObj.Supplier = dr["供应商"].ToString();
                    orderObj.Policy = dr["政策类型"].ToString();
                    orderObj.payWay = dr["支付公司"].ToString();
                    orderList.Add(orderObj);//添加到集合
                }
            }
            return orderList;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [order](");
            strSql.Append("state,[type],clearCode,[number],clearCodeNumber,flightNumber,ticketTime,payTime,flightTime,orderPeople,Carrier,Discount,Passenger,Voyage,PNR,ticketPrice,taxPrice,ranyouFee,jijianFee,rebate,shouxuFee,yongjin,tgqFee,payFee,actualPayPrice,buyer,buyername,buyerOPUserId,Supplier,Policy,payWay)");
            strSql.Append(" values (");
            strSql.Append("@state,@type,@clearCode,@number,@clearCodeNumber,@flightNumber,@ticketTime,@payTime,@flightTime,@orderPeople,@Carrier,@Discount,@Passenger,@Voyage,@PNR,@ticketPrice,@taxPrice,@ranyouFee,@jijianFee,@rebate,@shouxuFee,@yongjin,@tgqFee,@payFee,@actualPayPrice,@buyer,@buyername,@buyerOPUserId,@Supplier,@Policy,@payWay)");

            OleDbParameter[] parameters = {
                    new OleDbParameter("@state", OleDbType.VarChar,50),
                    new OleDbParameter("@type", OleDbType.VarChar,50),
                    new OleDbParameter("@clearCode", OleDbType.VarChar,50),
                    new OleDbParameter("@number", OleDbType.VarChar,50),
                    new OleDbParameter("@clearCodeNumber", OleDbType.VarChar,50),
                    new OleDbParameter("@flightNumber", OleDbType.VarChar,50),
                    new OleDbParameter("@ticketTime", OleDbType.Date),
                    new OleDbParameter("@payTime", OleDbType.Date),
                    new OleDbParameter("@flightTime", OleDbType.Date),
                    new OleDbParameter("@orderPeople", OleDbType.VarChar,50),
                    new OleDbParameter("@Carrier", OleDbType.VarChar,50),
                    new OleDbParameter("@Discount", OleDbType.VarChar,50),
                    new OleDbParameter("@Passenger", OleDbType.VarChar,50),
                    new OleDbParameter("@Voyage", OleDbType.VarChar,50),
                    new OleDbParameter("@PNR", OleDbType.VarChar,50),
                    new OleDbParameter("@ticketPrice", OleDbType.VarChar,50),
                    new OleDbParameter("@taxPrice", OleDbType.VarChar,50),
                    new OleDbParameter("@ranyouFee", OleDbType.VarChar,50),
                    new OleDbParameter("@jijianFee", OleDbType.VarChar,50),
                    new OleDbParameter("@rebate", OleDbType.VarChar,50),
                    new OleDbParameter("@shouxuFee", OleDbType.VarChar,50),
                    new OleDbParameter("@yongjin", OleDbType.VarChar,50),
                    new OleDbParameter("@tgqFee", OleDbType.VarChar,50),
                    new OleDbParameter("@payFee", OleDbType.VarChar,50),
                    new OleDbParameter("@actualPayPrice", OleDbType.VarChar,50),
                    new OleDbParameter("@buyer", OleDbType.VarChar,50),
                    new OleDbParameter("@buyername", OleDbType.VarChar,255),
                    new OleDbParameter("@buyerOPUserId", OleDbType.VarChar,50),
                    new OleDbParameter("@Supplier", OleDbType.VarChar,50),
                    new OleDbParameter("@Policy", OleDbType.VarChar,50),
                    new OleDbParameter("@payWay", OleDbType.VarChar,50)};
            parameters[0].Value = model.state;
            parameters[1].Value = model.type;
            parameters[2].Value = model.clearCode;
            parameters[3].Value = model.number;
            parameters[4].Value = model.clearCodeNumber;
            parameters[5].Value = model.flightNumber;
            parameters[6].Value = model.ticketTime;
            parameters[7].Value = model.payTime;
            parameters[8].Value = model.flightTime;
            parameters[9].Value = model.orderPeople;
            parameters[10].Value = model.Carrier;
            parameters[11].Value = model.Discount;
            parameters[12].Value = model.Passenger;
            parameters[13].Value = model.Voyage;
            parameters[14].Value = model.PNR;
            parameters[15].Value = model.ticketPrice;
            parameters[16].Value = model.taxPrice;
            parameters[17].Value = model.ranyouFee;
            parameters[18].Value = model.jijianFee;
            parameters[19].Value = model.rebate;
            parameters[20].Value = model.shouxuFee;
            parameters[21].Value = model.yongjin;
            parameters[22].Value = model.tgqFee;
            parameters[23].Value = model.payFee;
            parameters[24].Value = model.actualPayPrice;
            parameters[25].Value = model.buyer;
            parameters[26].Value = model.buyername;
            parameters[27].Value = model.buyerOPUserId;
            parameters[28].Value = model.Supplier;
            parameters[29].Value = model.Policy;
            parameters[30].Value = model.payWay;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}
