using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景区美图 详细页
    /// Create:luofx  Date:2010-12-8
    /// </summary>
    public partial class ScenicBeautiesDetail : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 图片路径字符串
        /// </summary>
        protected string AllImagePathString = string.Empty;
        /// <summary>
        /// 当前图片在集合中数据中属于第几个
        /// </summary>
        protected int index = 0;
        /// <summary>
        /// 当前图片路径
        /// </summary>
        protected string CurrImagePath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CTAB = SeniorOnlineShop.master.SPOTT1TAB.景区美图;
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            int intRecordCount = 0;
            int count = 0;
            string CompanyId = ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CompanyId;
            string NeedId = Request.QueryString["id"];
            var qmodel = new EyouSoft.Model.ScenicStructure.MScenicImgSearch
            {
                ImgType =
                    new EyouSoft.Model.ScenicStructure.ScenicImgType?[]
                                         {
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.景区导游地图,
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.景区形象,
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.其他
                                         }
            };
            IList<EyouSoft.Model.ScenicStructure.MScenicImg> list =
                EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(0, CompanyId, qmodel);
            if (list != null && list.Count > 0)
            {
                AllImagePathString = "[";
                ((List<EyouSoft.Model.ScenicStructure.MScenicImg>)list).ForEach(item =>
                {
                    count++;
                    if (NeedId == item.ImgId)
                    {
                        index = count;
                        CurrImagePath = EyouSoft.Common.Domain.FileSystem + item.Address;
                    }
                    AllImagePathString += "'" + item.ThumbAddress + "',";
                });
                AllImagePathString = AllImagePathString.TrimEnd(',');
                AllImagePathString += "]";
            }
            list = null;
        }
    }
}
