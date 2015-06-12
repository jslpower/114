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
using System.Collections.Generic;
using EyouSoft.Common;

namespace SiteOperationsCenter.AdManagement
{
    /// <summary>
    /// 广告管理：首页
    /// 功能：查询
    /// 创建人：袁惠
    /// 创建时间： 2010-7-22  
    /// </summary>
    public partial class AdManageHome : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限判断
                if (!this.CheckMasterGrant(YuYingPermission.同业114广告_管理该栏目))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业114广告_管理该栏目,true);
                    return;
                }

                //初始化
                this.Title = "同业114广告管理";
                string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.AdvStructure.AdvCatalog));
                if (typeList.Length > 0)
                {
                    List<string> cateList = new List<string>();
                    for (int i = 0; i < typeList.Length; i++)
                    {
                        cateList.Add(typeList[i]);
                    }
                    crptLocationParent.DataSource = cateList;
                    crptLocationParent.DataBind();
                }
            }
        }        
        protected void crptLocationParent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor anchor = e.Item.FindControl("acatelogo") as HtmlAnchor;
                HtmlAnchor imganchor = e.Item.FindControl("hrefImg") as HtmlAnchor;
                if (anchor != null)
                {
                    if (!string.IsNullOrEmpty(anchor.InnerText))
                    {
                        DataList datalist = e.Item.FindControl("dltLocationChild") as DataList;
                        IList<EyouSoft.Model.AdvStructure.AdvPositionInfo> poslist = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetPositions((EyouSoft.Model.AdvStructure.AdvCatalog)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvCatalog), anchor.InnerText));

                        if (poslist != null && poslist.Count > 0)
                        {
                            if (anchor.InnerText == "首页广告")
                            {
                                IEnumerable<EyouSoft.Model.AdvStructure.AdvPositionInfo> filertPosList = poslist.Where
                               (i => i.Position == EyouSoft.Model.AdvStructure.AdvPosition.焦点图片 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.首页资讯非通栏广告 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.国际长线5张logo图片 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.散拼中心广告普通版 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.首页推荐企业 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.首页金牌企业展示广告 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.周边长线5张logo图片 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.首页资讯通栏广告 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.首页推荐产品广告 ||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.首页广告公告||
                                     i.Position == EyouSoft.Model.AdvStructure.AdvPosition.国内长线5张logo图片);
                                datalist.DataSource = filertPosList;
                                datalist.DataBind();
                            }
                            else
                            {
                                datalist.DataSource = poslist;
                                datalist.DataBind();
                            }
                        }
                    } 
                  
                    if (imganchor != null)
                    {
                        string imgname = "";
                        switch ((EyouSoft.Model.AdvStructure.AdvCatalog)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvCatalog),anchor.InnerText))
                        {
                            case EyouSoft.Model.AdvStructure.AdvCatalog.MQ广告:
                                imgname = "index.jpg1";                  //暂无mq广告图片
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.供求信息频道:
                             imgname = "gq.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.旅游用品频道:
                             imgname = "lyyp.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.景区频道:
                             imgname = "jingqu.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.机票频道:
                             imgname = "jipiao.jpg";
                                break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.线路频道:
                                imgname = "xianlu.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.购物点频道:
                             imgname = "gwd.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.车队频道:
                             imgname = "chedui.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.酒店频道:
                             imgname = "jiudian.jpg";
                             break;
                            case EyouSoft.Model.AdvStructure.AdvCatalog.首页广告:
                             imgname = "index.jpg";
                             break;
                            default:
                             imgname = "index.jpg";
                             break;
                        }
                        imganchor.HRef = Domain.ServerComponents + "/images/yunying/siteimg/" + imgname;
                    }
                }
            }
        }

        protected string GetPostion(string displaytype, string postion)
        {
            string htmlstr = "<a href='{0}'>{1}</a>";
            string PagePath = "";
            if (!string.IsNullOrEmpty(displaytype))
            {
                switch ((EyouSoft.Model.AdvStructure.AdvDisplayType)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvDisplayType), displaytype))
                {
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.单位图片广告:
                        PagePath = "UnitAdList.aspx?dispType=" + (int)EyouSoft.Model.AdvStructure.AdvDisplayType.单位图片广告+"&";
                        break;
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.单位图文广告:
                        PagePath = "UnitAdList.aspx?dispType=" + (int)EyouSoft.Model.AdvStructure.AdvDisplayType.单位图文广告+"&";
                        break;
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.单位文字广告:
                        PagePath = "UnitAdList.aspx?dispType=" + (int)EyouSoft.Model.AdvStructure.AdvDisplayType.单位文字广告+"&";
                        break;
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.图片广告:
                        PagePath = "PhotoAdList.aspx?";
                        break;
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.图文广告:
                        PagePath = "PhotoWritingAdList.aspx?dispType=" + (int)EyouSoft.Model.AdvStructure.AdvDisplayType.图文广告+"&";
                        break;
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.供求图文广告:
                        PagePath = "PhotoWritingAdList.aspx?dispType=" + (int)EyouSoft.Model.AdvStructure.AdvDisplayType.供求图文广告+"&";
                        break;
                    case EyouSoft.Model.AdvStructure.AdvDisplayType.文字广告:
                        PagePath = "WritingAdList.aspx?";
                        break;
                }
                var position = (EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), postion);
                //新版首页推荐产品链接页面
                if (position == EyouSoft.Model.AdvStructure.AdvPosition.首页推荐产品广告)
                    return string.Format(htmlstr, "/AdManagement/RecommendProductList.aspx", "首页推荐产品广告");
                else if(position == EyouSoft.Model.AdvStructure.AdvPosition.散拼中心广告普通版)
                    return string.Format(htmlstr, "/AdManagement/SanPinCenterNormalList.aspx", "散拼中心普通版推荐企业");
               else
                    return string.Format(htmlstr, PagePath + "position=" + (int)position, postion);
            }
            else
            {
                return "";
            }
        }
    }
}
