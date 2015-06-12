using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.BLL.ScenicStructure;
using EyouSoft.Model.ScenicStructure;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 最新加入(包括2个广告位)
    /// 修改人：郑付杰
    /// 时间：2011-11-21
    /// </summary>
    public partial class ViewRightControl : System.Web.UI.UserControl
    {
        private int _Cid;

        public int Cid
        {
            get { return _Cid; }
            set { _Cid = value; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int? CountyId { get; set; }
        /// <summary>
        /// 显示条数
        /// </summary>
        public int TopNum { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScenicAdvFlagFrist();

                ScenicAdvFlagSecond();

                NewJoin();
            }
        }



        #region 景区频道旗帜广告1
        /// <summary>
        /// 景区频道旗帜广告
        /// </summary>
        protected void ScenicAdvFlagFrist()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(Cid, EyouSoft.Model.AdvStructure.AdvPosition.景区频道旗帜广告1);
            if (model != null)
            {
                if (model.RedirectURL == Utils.EmptyLinkCode)
                {
                    this.ltlImgAdvFalgFrist.Text = "<a  target=\"_self\"  href=\"" + model.RedirectURL + "\" ><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
                else
                {
                    this.ltlImgAdvFalgFrist.Text = "<a  target=\"_blank\"  href=\"" + model.RedirectURL + "\" ><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
                
               
            }
        }
        #endregion

        #region 景区频道旗帜广告2
        /// <summary>
        /// 景区频道旗帜广告
        /// </summary>
        protected void ScenicAdvFlagSecond()
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(Cid, EyouSoft.Model.AdvStructure.AdvPosition.景区频道旗帜广告2);
            if (model != null)
            {
                if (model.RedirectURL == Utils.EmptyLinkCode)
                {
                    this.ltlImgAdvFlagSecond.Text = "<a  target=\"_self\"  href=\"" + model.RedirectURL + "\" ><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
                else
                {
                    this.ltlImgAdvFlagSecond.Text = "<a  target=\"_blank\"  href=\"" + model.RedirectURL + "\" ><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
               
            }
        }
        #endregion

        #region 最新加入列表
        protected void NewJoin()
        {
            //最新加入
            IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> list = 
                EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetNewComapnyScenic(10, EyouSoft.Model.CompanyStructure.BusinessProperties.景区);
            
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append("<li><a href=\"javascript:void(0);\" title=\"" + list[i].CompanyName + "\">" + Utils.GetText2(list[i].CompanyName, 16, false) + "</a></li>");

                }
                this.lclZxjr.Text = sb.ToString();
            }
        }
        #endregion

        #region 景区广告或者对象方法
        /// <summary>
        /// 景区广告或者对象方法
        /// </summary>
        protected EyouSoft.Model.AdvStructure.AdvInfo GetAdvsModel(int relationId, EyouSoft.Model.AdvStructure.AdvPosition advPosition)
        {
            EyouSoft.Model.AdvStructure.AdvInfo model = null;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advInfoList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(relationId, advPosition);
            if (advInfoList.Count > 0)
            {
                model = advInfoList[0];
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }
        #endregion

        #region 景区集合方法
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
            return null;
        }
        #endregion
    }
}