using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 最热酒店用户控件
    /// 功能：显示最热酒店
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-13  
    /// </summary> 
    public partial class NewAttrHotelControl : System.Web.UI.UserControl
    {
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NewAttractions();
            }
        }

        #region 酒店集合方法
        /// <summary>
        /// 景区集合方法
        /// </summary>
        protected IList<EyouSoft.Model.AdvStructure.AdvInfo> GetList(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> List = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (List.Count > 0)
            {
                return List;
            }
            List = null;
            return null;
        }
        #endregion

        #region 本周最热酒店
        protected void NewAttractions()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> NewAttractions = this.GetList(this.CityId, EyouSoft.Model.AdvStructure.AdvPosition.酒店频道本周最热酒店);
            if (NewAttractions != null && NewAttractions.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NewAttractions.Count; i++)
                {
                    if (i > 4)
                    {
                        continue;
                    }
                    string target = "";
                    if (NewAttractions[i].RedirectURL == Utils.EmptyLinkCode || NewAttractions[i].RedirectURL=="")
                    {
                        target = "target=_self";
                    }
                    else
                    {
                        target = "target=_blank";
                    }
                    if (i == 0)
                    {
                        sb.Append("<div class=\"show\" id=\"box" + (i + 1) + "\" onmouseover=\"pucker_show('box'," + (i + 1) + ",'hidden','show')\">");
                    }
                    else
                    {
                        sb.Append("<div class=\"hidden\" id=\"box" + (i + 1) + "\" onmouseover=\"pucker_show('box'," + (i + 1) + ",'hidden','show')\">");
                    }
                    sb.Append("<ul>");
                    sb.Append("<li class=\"imgli\" style='margin-right:5px;'><a " + target + " href='" + NewAttractions[i].RedirectURL + "' >");
                    sb.Append("<img src=" + Utils.GetNewImgUrl(NewAttractions[i].ImgPath, 0) + " width='75px' height='65px' style='border: 1px solid #ccc;' />");
                    sb.Append("</a></li>");
                    sb.Append("<li class=\"lidetail1\" ><span class=\"lixu\">" + (i + 1) + "</span><span class=\"lidz\"></span>");
                    sb.Append("<span class=\"limc\"><a " + target + " title='" + NewAttractions[i].Title + "' href='" + NewAttractions[i].RedirectURL + "'>" + Utils.GetText(NewAttractions[i].Title, 7, true) + "</a></span></li>");
                    sb.Append("<li class=\"jdnei\"></li></ul>");
                    //" + EyouSoft.Common.Utils.GetText(NewAttractions[i].Remark, 20) + "
                    sb.Append("</div>");
                }
                this.lclZrjd.Text = sb.ToString();
            }
            NewAttractions = null;

        }
        #endregion

     
    }
}