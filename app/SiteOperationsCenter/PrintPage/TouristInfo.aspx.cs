using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.PrintPage
{
    /// <summary>
    ///  散拼人员名单打印页面
    ///  创建时间： 2011-12-23   蔡永辉
    /// </summary>
    public partial class TouristInfo : EyouSoft.Common.Control.YunYingPage
    {
        protected int count = 0;
        protected bool isTrue = false;
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "团队人员名单";
            string tourId = Utils.GetQueryStringValue("TeamId");
            InitPage(tourId);
        }

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void InitPage(string tourID)
        {

            EyouSoft.Model.NewTourStructure.MPowderList PowderListModel =
                EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(tourID);

            if (PowderListModel != null)
            {
                //线路名称
                this.RouteName.Text = PowderListModel.RouteName;
                this.LeaveDate.Text = PowderListModel.LeaveDate.ToString("yyyy-MM-dd");
                //出团时间
                this.GroupNo.Text = PowderListModel.TourNo;
                //线路名称
                this.RouteName1.Text = PowderListModel.RouteName;
                //出团时间
                this.LeaveTime.Text = PowderListModel.LeaveDate.ToString("yyyy-MM-dd");
                //实际人数
                //this.FactNo.Text = PowderListModel.TourNum.ToString();
                //备注
                this.Remark.Value = PowderListModel.TourNotes;
            }

            IList<EyouSoft.Model.NewTourStructure.MTourOrderCustomer> list = null;

            //游客信息绑定
            list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderCustomerByTourId(tourID, PowderListModel.Publishers, CompanyType.专线);

            //arryOrderStatus = new PowderOrderStatus?[1];
            //arryOrderStatus[0] = PowderOrderStatus.专线商已确定;
            //list = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderCustomerByTourId(tourID, this.SiteUserInfo.CompanyID, CompanyType.专线, arryOrderStatus);

            if (list != null && list.Count > 0)
            {
                //实际人数
                this.FactNo.Text = list.Count.ToString();
                isTrue = true;
                this.TouristInfomation.DataSource = list;
                this.TouristInfomation.DataBind();
            }

        }
        #endregion

        #region 序号
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns>序号</returns>
        protected int GetCount()
        {
            return ++count;
        }
        #endregion

        protected void btnWordPrint_Click(object sender, EventArgs e)
        {
            string printHtml = Request.Form["txtPrintHTML"];
            string title = Utils.GetString(this.Page.Title.ToString(), "打印单");
            string saveFileName = string.Format("{0}.doc", title);
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", saveFileName));
            Response.ContentType = "application/ms-word";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string wordTemplate = @"
                <html>
                <head>
                    <title>打印预览</title>
                    <style type=""text/css"">
                    body {{color:#000000;font-size:12px;font-family:""宋体"";background:#fff; margin:0px;}}
                    img {{border: thin none;}}
                    table {{border-collapse:collapse;width:710px;}}
                    td {{font-size: 12px; line-height:18px;color: #000000;  }}	
                    .headertitle {{font-family:""黑体""; font-size:25px; line-height:120%; font-weight:bold;}}
                    </style>
                </head>
                <body>
                    <div id=""divPrintPreview"">{0}</div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, printHtml));

            Response.End();
        }

    }
}
