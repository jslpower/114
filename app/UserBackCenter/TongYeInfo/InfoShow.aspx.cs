using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.BLL.NewsStructure;
using EyouSoft.Model.NewsStructure;
using System.Text;
using EyouSoft.IBLL.NewsStructure;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.TongYeInfo
{
    /// <summary>
    /// 说明：用户后台—营销工具—同业资讯（查看）
    /// 创建人：徐从栎
    /// 创建时间：2011-12-15
    /// </summary>
    public partial class InfoShow : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_同业资讯))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!IsPostBack)
            {
                string infoId = Utils.GetQueryStringValue("infoId");
                this.initData(infoId);
            }
        }
        protected void initData(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;
            IPeerNews BLL = BPeerNews.CreateInstance();
            MPeerNews Model = BLL.GetPeerNews(id);
            if (null == Model)
                return;
            this.lbTitle.Text = Model.Title;//标 题
            //资讯相关
            EyouSoft.BLL.CompanyStructure.CompanyInfo companyBLL = new EyouSoft.BLL.CompanyStructure.CompanyInfo();
            CompanyDetailInfo companyModel=companyBLL.GetModel(Model.CompanyId);
            if (null != companyModel)
            {
                string strAboutInfo = string.Empty;
                if (companyModel.CompanyRole.HasRole(CompanyType.地接) || companyModel.CompanyRole.HasRole(CompanyType.专线))
                {
                    if (!string.IsNullOrEmpty(Model.AreaName))
                    {
                        //2012-02-10 14:10信息来源：楼 链接到组团菜单中的"旅游线路库"
                        strAboutInfo = string.Format(@"<a href='javascript:void(0)' onclick=""topTab.open('{0}','资讯相关',{{}})"" class='font12_grean' title='{1}'>{1}</a>", EyouSoft.Common.Domain.UserBackCenter + "/teamservice/linelibrarylist.aspx?lineId=" + Model.AreaId, Model.AreaName);
                    }
                }
                else if (companyModel.CompanyRole.HasRole(CompanyType.景区))
                {
                    EyouSoft.Model.ScenicStructure.MScenicArea Area = new EyouSoft.BLL.ScenicStructure.BScenicArea().GetModel(Model.ScenicId);
                    if (null != Area)
                    {
                        strAboutInfo = string.Format("<a href='{0}' target='_blank'>{1}</a>", EyouSoft.Common.Domain.UserPublicCenter + "/ScenicManage/NewScenicDetails.aspx?ScenicId="+Area.ScenicId, Area.ScenicName);
                    }
                }
                this.lbRoute.Text = strAboutInfo.Length > 0 ? strAboutInfo : "暂无";
            }
            this.lbCompany.Text = Model.CompanyName;//发布企业
            this.lbType.Text = Convert.ToString(Model.TypeId);//类别
            this.lbTime.Text = string.Format("{0:yyyy-MM-dd}", Model.IssueTime);//发布时间
            this.lbContent.Text = Model.Content;//内容
            IList<MPeerNewsAttachInfo> lst = Model.AttachInfo;
            StringBuilder strPic = new StringBuilder();
            StringBuilder strFile = new StringBuilder();
            if (null != lst && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    switch (lst[i].Type)
                    { 
                        case AttachInfoType.图片:
                            strPic.AppendFormat("<a href='{0}' title='{1}' target='_blank'>查看</a>",Domain.FileSystem+lst[i].Path, lst[i].FileName);
                            break;
                        case AttachInfoType.文件:
                            strFile.AppendFormat("<a href='{0}' title='{1}' target='blank'>{2}</a>", Domain.FileSystem + lst[i].Path, lst[i].FileName, lst[i].FileName);
                            break;
                    }
                }
            }
            this.lbPic.Text = strPic.Length > 0 ? strPic.ToString() : "暂无图片";//图片
            this.lbFile.Text = strFile.Length > 0 ? strFile.ToString() : "暂无附件";//附件下载
            //点击次数加1
            BLL.UpdateClickNum(id);
        }
    }
}