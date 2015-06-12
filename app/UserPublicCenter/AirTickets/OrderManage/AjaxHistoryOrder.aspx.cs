using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Text;
namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 机票历史订单查询Ajax页
    /// 袁惠
    /// 2010-12-29
    /// </summary>
    public partial class AjaxHistoryOrder : EyouSoft.Common.Control.BasePage
    {
        protected int BarCount = 0; // 总记录数
        private static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/jipiao/historyOrder.mdb");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PageSize = 10; //分页条数                
                int PageIndex = Utils.GetInt(Request.QueryString["Page"],1);
                string Content =Server.HtmlDecode(Utils.InputText(Request.QueryString["Content"]));
                int type = Utils.GetInt(Request.QueryString["TypeName"],1);   //查询类型：Pnr|旅客姓名
                string strdate =Utils.InputText(Request.QueryString["Date"]);   
                StringBuilder sb=new StringBuilder ();
                sb.Append(" where buyerOPUserId='"+SiteUserInfo.OpUserId+"' ");
                if (!string.IsNullOrEmpty(strdate))
                {
                    if(EyouSoft.Common.Function.StringValidate.IsDateTime(strdate))
                    {
                        sb.Append(" and flightTime >= #" + Convert.ToDateTime(strdate) + "# and flightTime< #"+Convert.ToDateTime(strdate).AddDays(1)+" # ");
                    }
                    else
                    {
                        Utils.ResponseMeg(false, "日期格式错误");
                        return;
                    }

                }
                if (!string.IsNullOrEmpty(Content))
                {
                    if (type == 1)
                        sb.Append(" and PNR='" + Content + "'");
                    else
                        sb.Append(" and Passenger like '%" + Content + "%'");
                }
                int pageCount=0; 
                DataTable dtable = new DataTable();//数据集合
                dtable = ExecutePager(PageIndex, PageSize, " Passenger,number,ticketTime,flightTime,Voyage,PNR,ticketPrice,ranyouFee,jijianFee,state,rebate ",
                    " [order]", sb.ToString(), " ticketTime desc ", out pageCount, out BarCount);
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    crptList. DataSource = dtable;
                    crptList.DataBind();
                    //绑定分页控件
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = BarCount;
                    this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                    this.ExporPageInfoSelect1.LinkType = 3;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "HistoryOrder.LoadData(this);", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "HistoryOrder.LoadData(this);", 0);
                
                }
                else
                {
                    crptList.EmptyText = "<tr ><td colspan=\"11\"><div  style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无订单信息</div></td></tr>";
                    this.ExporPageInfoSelect1.Visible = false;
                }
            }
        }
        #region sql
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                       // throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        /// </summary>
         ///获取结果集
         ///</summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">表名</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的DataTable</returns>      
        public static DataTable ExecutePager(int pageIndex, int pageSize, string showString, string queryString, string whereString, string orderString, out int pageCount, out int recordCount)
        {
            DataTable dt = null;
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = "ID desc";
            string myVw = string.Format("{0}", queryString);
            string sqlString = string.Format(" select count(0) as recordCount from {0} {1}", myVw, whereString);
            //获取总记录数
            recordCount = ExecuteScalar(sqlString);
            //如果总记录数每页不能均分
            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            if (pageIndex == 1)//第一页
            {
                dt = Query(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, whereString, orderString)).Tables[0];
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                dt = Query(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString)).Tables[0];
            }
            else
            {
                //获得当页的最大记录数。
                int pageLowerBound = pageSize * pageIndex;
                //获得当页的第一条记录开始处
                int pageUpperBound = pageLowerBound - pageSize;
                string result = string.Empty;
                using (DataTable dt1 = Query(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, "ID", myVw, whereString, orderString)).Tables[0])
                {
                    //获取本次记录的id
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (pageUpperBound < 1)
                        {
                            result += "," + dt1.Rows[i][0].ToString();
                        }
                        pageUpperBound--;
                    }
                }
                string recordIDs = result.Substring(1);
                dt = Query(string.Format("select {0} from {1} where id in ({2}) order by {3} ", showString, myVw, recordIDs, orderString)).Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 执行SQL查询语句，返回总记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        private static int ExecuteScalar(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = Convert.ToInt32(cmd.ExecuteScalar());
                        return rows;
                    }
                    catch (System.Data.OleDb.OleDbException E)
                    {
                        connection.Close();
                       // throw new Exception(E.Message);
                        return 0;
                    }
                }
            }
        }
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion
    }
}
