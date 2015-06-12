using System;

namespace IMFrame.MqNotice
{
    /// <summary>
    /// MQ广播列表
    /// 2011-06-13 曹胡生 创建 
    /// </summary>
    public partial class Radio : System.Web.UI.Page
    {
        public string Domain = string.Empty;
        public string Html = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
        }

        public void Bind()
        {
            Domain = EyouSoft.Common.Domain.UserPublicCenter + "/Information/MQNewsDetail.aspx?ID={0}&CityId=362";
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            System.Collections.Generic.IList<EyouSoft.Model.MQStructure.IMSuperClusterNews> list = null;
            int SuperID = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SuperID"));
            if (!SuperID.Equals(0))
            {
                list = EyouSoft.BLL.MQStructure.IMSuperClusterNews.CreateInstance().GetSuperClusterNews(5, EyouSoft.Model.MQStructure.Type.广播, SuperID);
            }
            if (list == null || list.Count==0)
            {
                //this.repList.EmptyText = "<dt style=\"margin: 0; padding: 0; float: left;\">暂无广播信息！</dt><dd style=\"margin: 0; padding: 0; float: left;\">暂无广播信息！</dd>";
                str.Append("<span style=\"color: rgb(1,0,9);\">暂无广播信息！</span>");
            }
            else
            { 
                for (int i = 0; i < list.Count; i++)
                {
                    str.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", GetUrl(list[i].Id), list[i].Title);
                }
                //this.repList.DataSource = list;
                //this.repList.DataBind();
            }
            Html = str.ToString();
        }

        public string GetUrl(object id)
        {
            return string.Format(Domain, id);
        }
    }
}
