using System;

namespace IMFrame.MqNotice
{
    /// <summary>
    /// 描述：MQ公告列表页
    /// 修改记录：
    /// 1、2011-06-13 曹胡生 创建
    /// </summary>
    public partial class Notice : System.Web.UI.Page
    {
        public string Domain = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
        }

        protected void Bind()
        {
            Domain = EyouSoft.Common.Domain.UserPublicCenter + "/Information/MQNewsDetail.aspx?ID={0}&CityId=362";
            System.Collections.Generic.IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> list = null;
            int SuperID = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SuperID"));
            if (!SuperID.Equals(0))
            {
                list = EyouSoft.BLL.MQStructure.IMSuperClusterNews.CreateInstance().GetSuperClusterNews(5, EyouSoft.Model.MQStructure.Type.公告, SuperID);
            }
            if (list == null || list.Count == 0)
            {
                this.repList.EmptyText = "<tr><td>·暂无公告信息！</td></tr>";
            }
            else
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
            }
        }

        public string GetUrl(object id)
        {
            return string.Format(Domain, id);
        }
    }
}
