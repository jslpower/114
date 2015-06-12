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
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.ScenicStructure;
using System.Collections.Generic;
using EyouSoft.Common.Control;

namespace UserPublicCenter.HomeControl
{
    public partial class HotScenic : System.Web.UI.UserControl
    {
        public int cityid;
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.Model.ScenicStructure.MSearchSceniceArea model = new EyouSoft.Model.ScenicStructure.MSearchSceniceArea();
            FrontPage page = this.Page as FrontPage;
            cityid = page.CityId;
            model.IsQH = false;
            model.B2B = (ScenicB2BDisplay)0;
            IList<MScenicArea> HotSceniclist = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(6, "", model);
            if (HotSceniclist != null && HotSceniclist.Count > 0)
            {
                //IList<MScenicImg> imglist = new List<MScenicImg>();

                //Img    ScenicName   ScenicId
                this.rptHotScenic.DataSource = HotSceniclist;
                this.rptHotScenic.DataBind();
            }
        }
        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetScenicImg(object o)
        {
            IList<MScenicImg> imglist = (IList<MScenicImg>)o;
            if (imglist != null && imglist.Count > 0)
            {
                string imgaddress = string.Empty;

                imgaddress += imglist[0].ThumbAddress;

                return EyouSoft.Common.Utils.GetNewImgUrl(imgaddress, 2);
            }
            else
            {
                return EyouSoft.Common.Utils.GetNewImgUrl("", 2);
            }
        }
    }
}