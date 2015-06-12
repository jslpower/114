using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 实时信息提醒
    /// </summary>
    public partial class IndexRemind : System.Web.UI.UserControl
    {

        protected int remindCount;//提醒数

        protected void Page_Load(object sender, EventArgs e)
        {
            //绑定信息提醒
            IOrderedEnumerable<EyouSoft.Model.CommunityStructure.Remind> remindList = EyouSoft.BLL.CommunityStructure.Remind.CreateInstance().GetList().OrderByDescending(i => i.EventTime);
            if (remindList != null)
            {
                rptInfo.DataSource = remindList;
                rptInfo.DataBind();
                remindCount = remindList.Count();
            }
        }
    }
}