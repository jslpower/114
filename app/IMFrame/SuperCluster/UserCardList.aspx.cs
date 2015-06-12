using System;
using System.Collections;
using EyouSoft.BLL.MQStructure;
using EyouSoft.ControlCommon.Control;
using System.Collections.Generic;

namespace IMFrame.SuperCluster
{
    public partial class UserCardList : MQPage
    {
        private int pageSize = 40;//页大小
        private int pageIndex = 1;//当前页索引
        private int recordCount;//记录总数

        /// <summary>
        ///  卡片URL
        /// </summary>
        protected string ModeUrl
        {
            get 
            {
                return ListUrl.Replace("List", "Mode");
            }
        }

        /// <summary>
        ///  列表URL
        /// </summary>
        protected string ListUrl
        {
            get
            {
                string srcUrl = Request.Url.ToString();
                if (srcUrl.Contains("&Page"))
                {
                    int index = srcUrl.IndexOf("&Page");
                    return srcUrl.Substring(0, index);
                }
                return srcUrl;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            BindData();
            BindPage();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            //同中中心编号
            int superID = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SuperID"));
            int mqId = MQLoginId;
            //int superID = 20009;
            //int mqId = 15183;
            IList<EyouSoft.Model.MQStructure.IMClusterUserCard> list = IMSuperCluster.CreateInstance().GetUserCardListByClusterId(pageSize, pageIndex, ref recordCount, superID, mqId);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    Repeater1.DataSource = list;
                    Repeater1.DataBind();
                }
                else
                {
                    this.ExportPageInfo1.Visible = false;
                }
            }
            else
            {
                this.ExportPageInfo1.Visible = false;
            }
        }

        /// <summary>
        /// 绑定分页控件
        /// </summary>
        private void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
    }
}
