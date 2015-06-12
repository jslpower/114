using System;
using System.Collections.Generic;
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
namespace SeniorOnlineShop.scenicspots.usercontrol
{
    /// <summary>
    /// 景区美图-控件
    /// </summary>
    /// 鲁功源 2010-12-09
    public partial class ScenicPic : System.Web.UI.UserControl
    {
        protected SeniorOnlineShop.master.ScenicSpotsT1 cMaster = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            cMaster = (SeniorOnlineShop.master.ScenicSpotsT1)this.Page.Master;
            if (cMaster == null)
            {
                Utils.ShowError("当前页面模板页错误！", "景区网店");
                return;
            }

            var qmodel = new EyouSoft.Model.ScenicStructure.MScenicImgSearch
            {
                ImgType =
                    new EyouSoft.Model.ScenicStructure.ScenicImgType?[]
                                         {
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.景区形象,
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.其他
                                         }
            };
            IList<EyouSoft.Model.ScenicStructure.MScenicImg> list =
                EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(RowSize * CellSize, cMaster.CompanyId,
                                                                                 qmodel);
            if (list != null && list.Count > 0)
            {
                dlPics.RepeatColumns = CellSize;
                dlPics.DataSource = list;
                dlPics.DataBind();
            }
            list = null;
            base.OnPreRender(e);
        }
        #endregion

        #region 属性
        private int _rowsize = 1;
        /// <summary>
        /// 行数
        /// </summary>
        public int RowSize
        {
            get
            {
                return _rowsize;
            }
            set
            {
                _rowsize = value;
            }
        }
        private int _cellsize = 4;
        /// <summary>
        /// 列数
        /// </summary>
        public int CellSize
        {
            get
            {
                return _cellsize;
            }
            set
            {
                _cellsize = value;
            }
        }
        #endregion

        #region 行绑定事件
        protected void dlPics_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                var model = (EyouSoft.Model.ScenicStructure.MScenicImg)e.Item.DataItem;
                Literal ltimg = (Literal)e.Item.FindControl("ltimg");
                if (ltimg != null && model != null && model.ThumbAddress.Trim().Length > 0)
                {
                    ltimg.Text = string.Format("<a href=\"/scenicspots_{0}_{1}\"><img style=\"width:139px;height:93px;\" src=\"{2}\" border=\"0\"/></a>", model.ImgId, cMaster.CompanyId, Domain.FileSystem + model.ThumbAddress.Trim());
                }
                ltimg = null;
                model = null;
            }
        }
        #endregion

    }
}