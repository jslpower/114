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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.BLL.ScenicStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.ScenicManage
{
    /// <summary>
    /// 最热景点用户控件
    /// 功能：显示最热景点
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-13  
    /// </summary>  
    public partial class NewAttrControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 省份
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int? CountyId { get; set; }
        //获取数量
        public int TopNum { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NewAttractions();
            }
        }


        #region 本周最热景点
        protected void NewAttractions()
        {
            //景区搜索实体
            MSearchSceniceArea search = new MSearchSceniceArea()
                                            {
                                                CityId = CityId,
                                                ProvinceId = ProvinceId,
                                                CountyId = CountyId,
                                                B2Bs =
                                                    new ScenicB2BDisplay?[]
                                                        {
                                                            ScenicB2BDisplay.常规, ScenicB2BDisplay.侧边推荐,
                                                            ScenicB2BDisplay.列表置顶, ScenicB2BDisplay.首页推荐
                                                        }
                                            };
            //景区形象图片
            string imgAddress = string.Empty;
            IList<MScenicArea> Scenics = BScenicArea.CreateInstance().GetList(TopNum, string.Empty, search);
            if (Scenics != null && Scenics.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Scenics.Count; i++)
                {
                    if (Scenics[i].Img != null && Scenics[i].Img[0] != null)
                    {
                        //数据层只获取一张景区形象图片
                        imgAddress = Scenics[i].Img[0].Address;
                    }

                    sb.Append("<div class=\"" + (i == 0 ? "show" : "hidden") + "\" id=\"box" + (i + 1) + "\" onmouseover=\"pucker_show('box'," + (i + 1) + ",'hidden','show'," + Scenics.Count + ")\">");
                    sb.Append("<ul><li class=\" class=\"imgli\"\"><a href=\"/jingquinfo_" + Scenics[i].Id + "\" title=\"" + Scenics[i].ScenicName + "\"><img src=\"" + Utils.GetNewImgUrl(imgAddress, 3) + "\" width=\"80\" height=\"70\" border=\"0\" alt=\"" + Scenics[i].ScenicName + "\" style=\"border:1px solid #ccc; padding:1px;\"/></a></li>");
                    sb.Append("<li class=\"" + (i <= 2 ? "lidetail1_3" : "lidetail1_1") + "\"><span class=\"lixu\">" + (i + 1) + "</span><a href=\"/jingquinfo_" + Scenics[i].Id + "\"><span class=\"limc\">" + Utils.GetText2(Scenics[i].ScenicName, 15, false) + "</span></a></li>");
                    sb.Append("<li class=\"jdnei\">" + Utils.GetText2(Utils.InputText(Scenics[i].Description), 17, true) + "</li></ul></div>");
                }
                this.lclZrjd.Text = sb.ToString();
            }

        }
        #endregion
    }
}