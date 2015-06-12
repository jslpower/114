using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.NewsStructure;
using EyouSoft.Model.NewsStructure;
using System.Text;
using EyouSoft.Common.Function;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Model.HotelStructure;
using EyouSoft.IBLL.NewsStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.GeneralShop.TongYeNews
{
    /// <summary>
    /// 说明：用户后台—网店管理—我的同业资讯（列表）
    /// 创建人：徐从栎
    /// 创建时间：2011-12-14
    /// </summary>
    public partial class NewsList : EyouSoft.Common.Control.BackPage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;
        protected string tblID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.我的网店_同业资讯))
            {
                Utils.ResponseNoPermit();
                return;
            }

            if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {

            }
            else
            {
                Utils.ResponseNoPermit();
                return;
            }
            string id = Utils.GetQueryStringValue("id");
            if (Utils.GetQueryStringValue("method") == "ajax")
            {
                this.AjaxMethod(Utils.GetQueryStringValue("type"), id);
                return;
            }
            if (!IsPostBack)
            {
                //使用GUID作为页面Table容器的id.
                this.tblID = Guid.NewGuid().ToString();
                this.InitData(id);
                this.InitListData();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected void InitData(string id)
        {
            ////相关专线、相关酒店、相关景区初始化值
            string selInfoValue = string.Empty;
            //分类
            this.ddlType.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.NewsStructure.PeerNewType));
            this.ddlType.DataTextField = "text";
            this.ddlType.DataValueField = "value";
            this.ddlType.DataBind();
            //编辑初始化
            if (!string.IsNullOrEmpty(id))
            {
                IPeerNews BLL = BPeerNews.CreateInstance();
                MPeerNews Model = BLL.GetPeerNews(id);
                if (null == Model)
                    return;
                this.txtTitle.Value = Model.Title;//标题
                //相关专线、相关酒店、相关景区
                selInfoValue = Model.AreaId.ToString();
                this.ddlType.SelectedValue = Convert.ToString((int)Model.TypeId);//分类
                this.txtContent.Value = Model.Content;//内容
                IList<MPeerNewsAttachInfo> lst = Model.AttachInfo;
                if (null != lst && lst.Count > 0)
                {
                    StringBuilder strPic = new StringBuilder();
                    StringBuilder strFile = new StringBuilder();
                    StringBuilder strPicHidden = new StringBuilder();
                    StringBuilder strFileHidden = new StringBuilder();
                    for (int i = 0; i < lst.Count; i++)
                    {
                        switch (lst[i].Type)
                        {
                            case AttachInfoType.图片:
                                strPic.AppendFormat("<a href='{0}' title='{1}' target='_blank'>查看</a><a href=\"javascript:void(0);\" onclick=\"NewsList.delFile(this,'pic');\"><img src='{2}/images/fujian_x.gif' border='0'/></a>", Domain.FileSystem + lst[i].Path, lst[i].FileName, this.ImageServerUrl);
                                strPicHidden.AppendFormat(lst[i].Path + ",");
                                break;
                            case AttachInfoType.文件:
                                strFile.AppendFormat("<a href='{0}' title='{1}' target='blank'>{1}</a><a href=\"javascript:void(0);\" onclick=\"NewsList.delFile(this,'file');\"><img src='{2}/images/fujian_x.gif' border='0'/></a>", Domain.FileSystem + lst[i].Path, lst[i].FileName, this.ImageServerUrl);
                                strFileHidden.AppendFormat("{0}|{1},", lst[i].FileName, lst[i].Path);
                                break;
                        }
                    }
                    if (strPicHidden.Length > 0)
                    {
                        strPicHidden.Remove(strPicHidden.Length - 1, 1);
                    }
                    if (strFileHidden.Length > 0)
                    {
                        strFileHidden.Remove(strFileHidden.Length - 1, 1);
                    }
                    MessageBox.ResponseScript(this, string.Format("NewsList.setUploadHiddenValue('{0}','pic');NewsList.setUploadHiddenValue('{1}','file');", strPicHidden, strFileHidden));
                    this.lbPic.Text = strPic.Length > 0 ? strPic.ToString() : "";//图片
                    this.lbFile.Text = strFile.Length > 0 ? strFile.ToString() : "";//附件下载
                }
            }
            this.getSelectInfo(selInfoValue);
            //this.InitTypeName();
        }
        /// <summary>
        /// 列表初始化
        /// </summary>
        protected void InitListData()
        {
            this.pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            string kw = Server.UrlDecode(Utils.GetQueryStringValue("kw"));//关键字（标题、发布单位、资讯相关）
            MQueryPeerNews Model = new MQueryPeerNews();
            Model.KeyWord = kw.Trim();
            Model.CompanyId = this.SiteUserInfo.CompanyID;
            IPeerNews BLL = BPeerNews.CreateInstance();
            IList<MPeerNews> lst = BLL.GetGetPeerNewsList(this.pageSize, this.pageIndex, ref this.recordCount, Model);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                this.BindPage(kw);
            }
            else
            {
                this.RepList.Controls.Add(new Literal() { Text = "<tr><td colspan='4' align='center'>暂无信息！</td></tr>" });
                this.ExportPageInfo1.Visible = false;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="kw">关键字(标题)</param>
        protected void BindPage(string kw)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("kw", kw);
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        /// <summary>
        /// ajax方法
        /// </summary>
        /// <param name="type">save:保存  del:删除</param>
        /// <param name="id">id</param>
        protected void AjaxMethod(string type, string id)
        {
            if (string.IsNullOrEmpty(type))
            {
                return;
            }
            switch (type)
            {
                case "save":
                    this.PageSave(id);
                    break;
                case "del":
                    this.Del(id);
                    break;
            }
        }
        /// <summary>
        /// ajax_页面保存
        /// </summary>
        protected void PageSave(string id)
        {
            StringBuilder strMsg = new StringBuilder();
            IPeerNews BLL = BPeerNews.CreateInstance();
            MPeerNews Model;
            if (string.IsNullOrEmpty(id))
            { //新增
                Model = new MPeerNews();
                strMsg.Append(this.CommonModel(Model));
                Model.IssueTime = DateTime.Now;
                if (strMsg.Length == 0 && BLL.AddPeerNews(Model) == 1)
                {
                    strMsg.Append("添加成功！");
                }
                else
                {
                    strMsg.Remove(0, strMsg.Length);
                    strMsg.Append("添加失败！");
                };
            }
            else
            { //修改
                Model = BLL.GetPeerNews(id);
                strMsg.Append(this.CommonModel(Model));
                Model.LastUpdateTime = DateTime.Now;
                if (strMsg.Length == 0 && BLL.CustomerUpdatePeerNews(Model) == 1)
                {
                    strMsg.Append("更新成功！");
                }
                else
                {
                    strMsg.Remove(0, strMsg.Length);
                    strMsg.Append("更新失败！");
                }
            }
            Response.Clear();
            Response.Write(strMsg.ToString());
            Response.End();
        }
        /// <summary>
        /// 编辑与添加的公共model部分
        /// </summary>
        protected string CommonModel(MPeerNews Model)
        {
            StringBuilder str = new StringBuilder();
            string txtTitle = Utils.GetFormValue(this.txtTitle.UniqueID).Trim();//标题
            string[] selectInfo = Utils.GetFormValue("selectInfo").Split('|');//相关专线、相关地接、相关景区ID
            string selectInfoName = Utils.GetFormValue("selectInfoName");//相关专线、相关地接、相关景区Name
            int ddlType = Utils.GetInt(Utils.GetFormValue(this.ddlType.UniqueID), -1);//分 类
            string txtContent = Utils.EditInputText(Request.Form[this.txtContent.UniqueID]);//内容
            string[] picPath = Utils.GetFormValue(this.filePic.UniqueID + "$hidFileName").Split(',');//已上传的图片路径 ,多个用逗号隔开
            string[] filePath = Utils.GetFormValue(this.files.UniqueID + "$hidFileName").Split(',');//已上传的附件路径 ,多个用逗号隔开
            /*数据验证开始*/
            if (string.IsNullOrEmpty(txtTitle))
            {
                str.Append("标题不能为空！\\n");
            }
            if (string.IsNullOrEmpty(txtContent))
            {
                str.Append("内容不能为空！\\n");
            }
            if (str.Length > 0)
            {
                return str.ToString();
            }
            /*数据验证结束*/
            Model.CompanyId = this.SiteUserInfo.CompanyID;
            Model.CompanyName = this.SiteUserInfo.CompanyName;
            Model.Content = txtContent;
            Model.Ip = StringValidate.GetRemoteIP();
            if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
            {
                if (selectInfo.Length == 2)
                {
                    Model.AreaId = Utils.GetInt(selectInfo[0]);
                    Model.AreaType = (AreaType)Utils.GetInt(selectInfo[1]);
                }
                else
                {
                    Model.AreaId = Utils.GetInt(selectInfo[0]);
                }
            }
            else if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
            {
                Model.ScenicId = selectInfo[0];
            }
            Model.AreaName = selectInfoName;
            Model.OperatorId = this.SiteUserInfo.ID;
            Model.OperatorName = this.SiteUserInfo.UserName;
            Model.Title = txtTitle;
            Model.TypeId = (PeerNewType)ddlType;
            /*文件上传开始*/
            List<MPeerNewsAttachInfo> lstFile = new List<MPeerNewsAttachInfo>();
            //图片
            if (picPath.Length > 0)
            {
                for (int i = 0; i < picPath.Length; i++)
                {
                    if (string.IsNullOrEmpty(picPath[i]))
                        break;
                    MPeerNewsAttachInfo picModel = new MPeerNewsAttachInfo();
                    picModel.FileName = picPath[i].Substring(picPath[i].LastIndexOf('/') + 1);
                    picModel.Path = picPath[i];
                    picModel.Type = AttachInfoType.图片;
                    lstFile.Add(picModel);
                }
            }
            //附件
            if (filePath.Length > 0)
            {
                for (int i = 0; i < filePath.Length; i++)
                {
                    if (string.IsNullOrEmpty(filePath[i]))
                        break;
                    MPeerNewsAttachInfo fileModel = new MPeerNewsAttachInfo();
                    if (filePath[i].IndexOf('|') > 0)
                    {
                        string[] strFileInfo = filePath[i].Split('|');
                        fileModel.FileName = strFileInfo[0];
                        fileModel.Path = strFileInfo[1];
                    }
                    else
                    {
                        fileModel.FileName = filePath[i].Substring(filePath[i].LastIndexOf('/'));
                        fileModel.Path = filePath[i];
                    }
                    fileModel.Type = AttachInfoType.文件;
                    lstFile.Add(fileModel);
                }
            }
            Model.AttachInfo = lstFile.Count == 0 ? null : lstFile;
            /*文件上传结束*/
            return str.ToString();
        }
        /// <summary>
        /// ajax_删除
        /// </summary>
        protected void Del(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            string str = string.Empty;
            IPeerNews BLL = BPeerNews.CreateInstance();
            if (BLL.DelPeerNews(id.Split(',')) != 1)
            {
                str = "删除失败！";
            }
            else
            {
                str = "删除成功！";
            }
            Response.Clear();
            Response.Write(str);
            Response.End();
        }
        /// <summary>
        /// 显示相关专线、地接、景区
        /// </summary>
        /// <param name="selValue">初始选中项的值</param>
        /// <returns></returns>
        protected void getSelectInfo(string selValue)
        {
            if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店))
            {
                return;
            }
            string s = @"<tr><td bgcolor=""#CCE8F8"" align=""right"">{0}：</td><td align=""left""><select id=""selectInfo"" name=""selectInfo""><option value=""-1"">--请选择--</option>{1}</select><input type=""hidden"" name=""selectInfoName""/></td></tr>";
            StringBuilder str = new StringBuilder();
            StringBuilder strOptions = new StringBuilder();
            //相关专线
            if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
            {
                //用户Id
                string UserID = string.Empty; ;
                if (SiteUserInfo != null)
                {
                    EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = SiteUserInfo;

                    UserID = UserInfoModel.ID ?? "0";
                }
                ICompanyUser companyUserBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
                EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = companyUserBLL.GetModel(UserID);
                List<AreaBase> lstArea = companyUserModel.Area;
                if (null != lstArea && lstArea.Count > 0)
                {
                    for (int i = 0; i < lstArea.Count; i++)
                    {
                        strOptions.AppendFormat("<option value=\"{0}|{3}\"{2}>{1}</option>", lstArea[i].AreaId, lstArea[i].AreaName, selValue == lstArea[i].AreaId.ToString() ? " selected=\"selected\"" : "", (int)lstArea[i].RouteType);//资讯的model中相关信息存的是名字
                    }
                }
                str.AppendFormat(s, "相关专线", strOptions);
            }
            //相关景区
            else if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
            {
                EyouSoft.BLL.ScenicStructure.BScenicArea viewBLL = new EyouSoft.BLL.ScenicStructure.BScenicArea();
                IList<MScenicArea> viewLst = viewBLL.GetList(this.SiteUserInfo.CompanyID);
                if (null != viewLst && viewLst.Count > 0)
                {
                    for (int i = 0; i < viewLst.Count; i++)
                    {
                        strOptions.AppendFormat("<option value=\"{0}\"{2}>{1}</option>", viewLst[i].ScenicId, viewLst[i].ScenicName, selValue == viewLst[i].ScenicId ? " selected=\"selected\"" : "");//资讯的model中相关信息存的是名字
                    }
                }
                str.AppendFormat(s, "相关景区", strOptions);
            }
            this.ltSelectTypeInfo.Text = str.ToString();
        }
        /// <summary>
        /// 返回资讯相关链接
        /// </summary>
        /// <param name="AreaName">区域名</param>
        /// <param name="AreaId">区域ID</param>
        /// <param name="ScenicId">景区ID</param>
        /// <returns></returns>
        protected string getInfoAboutHref(object AreaName, object AreaId, object ScenicId, object CompanyId)
        {
            string str = string.Empty;
            EyouSoft.BLL.CompanyStructure.CompanyInfo companyBLL = new EyouSoft.BLL.CompanyStructure.CompanyInfo();
            CompanyDetailInfo companyModel = companyBLL.GetModel(Convert.ToString(CompanyId));
            if (null != companyModel)
            {
                if (companyModel.CompanyRole.HasRole(CompanyType.地接) || companyModel.CompanyRole.HasRole(CompanyType.专线))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(AreaName)))
                    {
                        //2012-02-10 14:10信息来源：楼 链接到组团菜单中的"旅游线路库"
                        str = string.Format(@"<a href='javascript:void(0)' onclick=""topTab.open('{0}','资讯相关',{{}})"" class='font12_grean' title='{1}'>【{1}】</a>", EyouSoft.Common.Domain.UserBackCenter + "/teamservice/linelibrarylist.aspx?lineId=" + AreaId, Utils.GetText2(Eval("AreaName").ToString(), 6, true));
                    }
                }
                else if (companyModel.CompanyRole.HasRole(CompanyType.景区))
                {
                    EyouSoft.Model.ScenicStructure.MScenicArea Area = new EyouSoft.BLL.ScenicStructure.BScenicArea().GetModel(Convert.ToString(ScenicId));
                    if (null != Area)
                    {
                        str = string.Format(@"<a href=""{0}"" target=""_blank"" class='font12_grean' title='{1}'>【{1}】</a>", EyouSoft.Common.Domain.UserPublicCenter + "/ScenicManage/NewScenicDetails.aspx?ScenicId=" + Area.ScenicId, Utils.GetText2(Area.ScenicName, 6, true));
                    }
                }
            }
            return str;
        }
    }
}