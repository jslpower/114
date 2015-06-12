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
using EyouSoft.Common.Function;
using System.IO;
using System.Collections.Generic;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店后台设置 添加目的地指南
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    /// history: zhouwc   2010-12-10  整合"景点高级网店旅游资讯"到"出游指南"栏目中
    public partial class SetTravelGuid : EyouSoft.Common.Control.BasePage
    {
        public string img_Path = "";
        protected int GuideType = 0;
        protected int intTypeId = 0;
        //protected int ddlGuidTypeIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            txtGuidTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            GuideType = Utils.GetIntSign(Request.QueryString["GuideType"], 0);
            intTypeId = Utils.GetIntSign(Request.QueryString["TypeId"], 0);
            if (!Page.IsPostBack)
            {
                if (GuideType == 0)
                {
                    ltrEditTitle.Text = "出游指南添加";
                    ltrListTitle.Text = "出游指南列表";
                }
                else if (GuideType == 1)
                {
                    SetTitle(intTypeId);
                }

                //初始化类别下拉框
                BindDropDownList(GuideType, intTypeId);

                if (string.IsNullOrEmpty(StringValidate.SafeRequest(Request.QueryString["guid_Id"])))
                {
                    //根据类别参数默认选中类别下拉框的值
                    if (intTypeId > 0 && ddlGuidType.Items.FindByValue(intTypeId.ToString()) != null)
                        ddlGuidType.Items.FindByValue(intTypeId.ToString()).Selected = true;
                }
                else
                {
                    //初始化旅游指南信息
                    InitGuidInfo();
                }
            }
        }

        #region 修改，显示信息
        private void InitGuidInfo()
        {
            EyouSoft.Model.ShopStructure.HighShopTripGuide guide = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetModel(StringValidate.SafeRequest(Request.QueryString["guid_Id"]));
            if (guide != null)
            {
                txtGuidTitle.Value = guide.Title;
                editGuid.Value = guide.ContentText;
                txtGuidTime.Value = guide.IssueTime.ToString("yyyy-MM-dd HH:mm");
                if (!string.IsNullOrEmpty(guide.ImagePath))
                {
                    img_Path = string.Format("<a href=\"{0}\" target='_blank'  title=\"点击查看\">查看原图</a>", Domain.FileSystem + guide.ImagePath);
                    hdfAgoImgPath.Value = guide.ImagePath;
                }

                int TypeId = 0;
                if (guide.TypeID != null)
                    TypeId = (int)guide.TypeID.Value;
                if (ddlGuidType.Items.FindByValue(TypeId.ToString()) != null)
                    ddlGuidType.Items.FindByValue(TypeId.ToString()).Selected = true;
            }
            guide = null;
        }
        #endregion

        # region 设置旅游指南信息
        protected void Submit1_Click(object sender, EventArgs e)
        {
            //判断页面数据是否填写完毕
            if (string.IsNullOrEmpty(txtGuidTitle.Value.Trim()) &&txtGuidTitle.Value.Trim().Length > 30)
            {
                MessageBox.ShowAndRedirect(this.Page, "标题不能为空并且不能大于30个字符！", Request.Url.ToString());
                return;
            }
            if(string.IsNullOrEmpty(editGuid.Value.Trim()))
            {
                MessageBox.ShowAndRedirect(this.Page, "内容不能为空！", Request.Url.ToString());
                return;
            }
            bool result=false;
            EyouSoft.Model.ShopStructure.HighShopTripGuide guide = new EyouSoft.Model.ShopStructure.HighShopTripGuide();
            guide.ContentText = Utils.EditInputText(editGuid.Value);
            guide.Title =Utils.InputText(txtGuidTitle.Value);
            guide.TypeID = (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)Enum.Parse(typeof(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType), Request.Form[this.ddlGuidType.ClientID]);
            guide.IssueTime = DateTime.Now;
            guide.OperatorID = this.SiteUserInfo.ID;//编辑者ID
            guide.CompanyID = this.SiteUserInfo.CompanyID;
            string imgpath = Utils.GetFormValue("sfuguidpic$hidFileName");
            if (imgpath.Length>0)
            {
                guide.ImagePath = imgpath;
            }
            else
            {
                guide.ImagePath = Utils.GetFormValue(hdfAgoImgPath.UniqueID);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["guid_Id"]))//修改
            {
                guide.ID = StringValidate.SafeRequest(Request.QueryString["guid_Id"]);
                result = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().Update(guide);
            }
            else
            {
                result = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().Add(guide);
            }
            if (result)
            {
                MessageBox.ShowAndRedirect(this.Page, "操作成功", "SetTravelGuidList.aspx?GuideType=" + GuideType + "&TypeId=" + intTypeId);
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "操作失败", "SetTravelGuidList.aspx?GuideType=" + GuideType + "&TypeId=" + intTypeId);
            }
            guide = null;         
        }
        #endregion

        #region 根据页面参数绑定类型

        /// <summary>
        /// 根据页面参数绑定类型
        /// </summary>
        private void BindDropDownList(int GuideType, int TypeId)
        {
            List<EnumObj> list = EnumObj.GetList(typeof(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType));
            if (list == null || list.Count <= 0)
                return;
            switch (GuideType)
            {
                case -1:
                    list = list.Where(Item => (int.Parse(Item.Value.Trim()) <= 3)).ToList(); //目的地指南
                    break;
                case 0:
                    list = list.Where(Item => (int.Parse(Item.Value.Trim()) <= 4)).ToList(); //出游指南
                    break;
                case 1:
                    //景区旅游资讯   门票政策单独页面
                    if (TypeId > 0 && TypeId != 8)
                        list = list.Where(Item => (int.Parse(Item.Value.Trim()) > 4) && Item.Value.Trim() == TypeId.ToString()).ToList();
                    else if (TypeId == 8)
                    {
                        //景区攻略包含  景区攻略 8、景区美食 9、景区住宿 10、景区交通 11、景区购物 12
                        int[] typeArr = new int[] { 8, 9, 10, 11, 12 };
                        list = list.Where(Item => (typeArr.Contains(int.Parse(Item.Value.Trim())))).ToList();
                    }
                    else
                        list = list.Where(Item => (int.Parse(Item.Value.Trim()) > 4 && int.Parse(Item.Value.Trim()) != (int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.门票政策)).ToList();
                    break;
            }

            if (list == null || list.Count <= 0)
                return;

            ddlGuidType.DataSource = list;
            ddlGuidType.DataTextField = "Text";
            ddlGuidType.DataValueField = "Value";
            ddlGuidType.DataBind();
            ddlGuidType.Items.Insert(0, new ListItem("请选择类别", "0"));
        }

        #endregion

        #region 初始化页面标题

        /// <summary>
        /// 设置标题头
        /// </summary>
        /// <param name="intTypeId">类型ID</param>
        private void SetTitle(int intTypeId)
        {
            if (intTypeId > 0)
            {
                EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType e = (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)intTypeId;

                ltrEditTitle.Text = e.ToString() + "添加";
                ltrListTitle.Text = e.ToString() + "列表";
            }
        }

        #endregion
    }
}