using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.WebControl
{
    public partial class TravelRightControl : System.Web.UI.UserControl
    {
        private int _Cid;

        public int Cid
        {
            get { return _Cid; }
            set { _Cid = value; }
        }
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
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(Cid, EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道旗帜广告1);
            if (model != null)
            {
                if (model.RedirectURL == Utils.EmptyLinkCode)
                {
                    this.ltlImgAdvFalgFrist.Text = "<a  target=\"_self\"  href=\"" + model.RedirectURL + "\"><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
                else
                {
                    this.ltlImgAdvFalgFrist.Text = "<a  target=\"_blank\"  href=\"" + model.RedirectURL + "\"><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
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
            EyouSoft.Model.AdvStructure.AdvInfo model = GetAdvsModel(Cid, EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道旗帜广告2);
            if (model != null)
            {
                if (model.RedirectURL == Utils.EmptyLinkCode)
                {
                    this.ltlImgAdvFlagSecond.Text = "<a  target=\"_self\"  href=\"" + model.RedirectURL + "\"><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
                else
                {
                    this.ltlImgAdvFlagSecond.Text = "<a  target=\"_blank\"  href=\"" + model.RedirectURL + "\"><img src=\"" + Domain.FileSystem + model.ImgPath.Trim() + "\" width=\"220px\" height=\"90px\"/></a>";
                }
                
            }
        }
        #endregion

        #region 最新加入列表
        protected void NewJoin()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> NewJoinList = this.GetList(Cid, EyouSoft.Model.AdvStructure.AdvPosition.旅游用品频道最新加入);
            if (NewJoinList != null && NewJoinList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NewJoinList.Count; i++)
                {
                    if (NewJoinList[i].RedirectURL == Utils.EmptyLinkCode)
                    {
                        sb.Append("<li><a  target=\"_self\"  href=\"" + NewJoinList[i].RedirectURL + "\">" + Utils.GetText(NewJoinList[i].Title, 15) + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a  target=\"_blank\"  href=\"" + NewJoinList[i].RedirectURL + "\">" + Utils.GetText(NewJoinList[i].Title, 15) + "</a></li>");
                    }
                    
                    
                }
                this.lclZxjr.Text = sb.ToString();
            }
            //尚未绑定
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