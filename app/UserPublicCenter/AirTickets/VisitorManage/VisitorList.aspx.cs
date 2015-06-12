using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Collections.Generic;


namespace UserPublicCenter.AirTickets.VisitorManage
{
    /// <summary>
    /// 页面功能：机票常旅客——机票常旅客列表页
    /// 开发人：刘咏梅     
    /// 开发时间：2010-10-19
    /// </summary>
    public partial class VisitorList : EyouSoft.Common.Control.FrontPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Master.Naviagtion = AirTicketNavigation.机票常旅客管理;
                this.Title = "常旅客查询/修改_机票";
                //绑定旅客类型              
                BindVisitorType();       
            }
        }
        #endregion

        #region 绑定旅客类型 
        private void BindVisitorType()
        {
            this.ddlVisitorType.Items.Add(new ListItem("请选择类型", ""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.TicketVistorType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlVisitorType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketVistorType), str)).ToString()));
                }
            }
            //释放资源
            typeList = null;
        }
        #endregion
    }
}
