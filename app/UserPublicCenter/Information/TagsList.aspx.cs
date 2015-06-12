using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.Information
{
    /// <summary>
    /// 描述:tag标签列表
    /// </summary>
    public partial class TagsList : EyouSoft.Common.Control.FrontPage
    {
        public string html = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTags();
        }

        private void BindTags()
        {
            this.Title = "Tags列表";
            EyouSoft.BLL.NewsStructure.TagKeyInfo TagKeyInfoBll = new EyouSoft.BLL.NewsStructure.TagKeyInfo();
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> list = TagKeyInfoBll.GetAllTag();
            if (list != null && list.Count > 0)
            {
                int i = 1;
                foreach (var item in list)
                {
                    html += string.Format("<a class=\"tagc{0}\" href=\"{1}\">{2}</a>", i++ % 2 == 0 ? 1 : 2, EyouSoft.Common.URLREWRITE.Infomation.GetOtherListUrl(item.Id), item.ItemName);
                }
            }
        }
    }
}
