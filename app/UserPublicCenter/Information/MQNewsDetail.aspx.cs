using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.Information
{
    /// <summary>
    /// MQ公告，广播详细页
    /// </summary>
    public partial class MQNewsDetail : EyouSoft.Common.Control.FrontPage
    {
        protected string Class = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            int ID=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("ID"));

            if (!ID.Equals(0))
            {
                EyouSoft.Model.MQStructure.IMSuperClusterNews model=EyouSoft.BLL.MQStructure.IMSuperClusterNews.CreateInstance().GetModel(ID);
                if (model != null)
                {
                    Class = model.Category.ToString();
                    this.Page.Title = Class;
                    lblTitle.Text = model.Title;
                    lblDate.Text = model.OperateTime.ToString();
                    content.InnerHtml = model.NewsContent; 
                }
            }
        }
    }
}
