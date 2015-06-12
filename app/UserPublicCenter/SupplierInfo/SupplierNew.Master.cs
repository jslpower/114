using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// author dj
    /// date 2011-05-11
    /// 供求模块页 继承父模板页
    /// </summary>
    public partial class SupplierNew : System.Web.UI.MasterPage
    {
        protected EyouSoft.Common.Control.FrontPage pa;  

        protected EyouSoft.Model.AdvStructure.AdvInfo adv = null; //供求广告BANNER
        protected void Page_Load(object sender, EventArgs e)
        {

            pa = (EyouSoft.Common.Control.FrontPage)this.Page;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advlist = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(pa.CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道通栏banner);
            if (advlist != null && advlist.Count > 0)
            {
                adv = advlist[0];
            }
            UserPublicCenter.MasterPage.NewPublicCenter masterPage = this.Master as UserPublicCenter.MasterPage.NewPublicCenter;
            if (masterPage != null)
            {
                masterPage.HeadMenuIndex = 6;//选中首页菜单项
            }

            
        }
    }
}
