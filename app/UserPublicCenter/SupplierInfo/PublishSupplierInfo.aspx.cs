using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Collections;
using System.Text;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    ///首页点击发布供求
    ///孙川 2011-05-12
    /// </summary>
    public partial class PublishSupplierInfo : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 登录页面url
        /// </summary>
        private string strLoginUrl = string.Empty;

        //public string strSDType = string.Empty;
        public string strSDTypeGY = string.Empty;
        public string strUserName = string.Empty;
        public int publishCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            strLoginUrl = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Domain.UserPublicCenter + Request.RawUrl, string.Empty);
            if (!IsLogin)
            {
                Response.Redirect(strLoginUrl);
            }

            string type = Utils.GetQueryStringValue("type");
            string titleInfo = Utils.GetQueryStringValue("titleinfo");
            bool sameTitle = false;
            if (type == "sametitle" && titleInfo != "")
            {
                sameTitle = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().ExistsSameTitle(SiteUserInfo.ID, titleInfo,"");
                if (sameTitle)
                {
                    Response.Clear();
                    Response.Write("true");
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("false");
                    Response.End();
                }
            }

            publishCount = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetExchangeListCount(SiteUserInfo.ID);
            string TypeReach = Utils.GetFormValue("hidReach");  //求购
            string TypeSupply = Utils.GetFormValue("hidSupply"); //供应
            if (TypeReach == "Reach" && TypeSupply == "")       //求购
            {
                if (publishCount < 15 && !sameTitle)
                {
                    btnSave();
                }
                else
                {
                    Utils.ShowAndRedirect("发布条数已满！", Request.RawUrl);
                }
            }
            if (TypeReach == "" && TypeSupply == "Supply")       //供应
            {
                if (publishCount < 15 && !sameTitle)
                {
                    sbtnSave();
                }
                else
                {
                    Utils.ShowAndRedirect("发布条数已满！", Request.RawUrl);
                }
            }

            #region 初始化
            InitSDInfoRelease();
            InitUserInfo();
            BindNewData();
            #endregion
        }

        /// <summary>
        /// 初始化供求信息发布框
        /// </summary>
        private void InitSDInfoRelease()
        {
            //发布联系信息初始化
            if (IsLogin)
            {
                //求购
                txtName.Value = SiteUserInfo.ContactInfo.ContactName;
                txtMQ.Value = SiteUserInfo.ContactInfo.MQ;
                txtTel.Value = string.IsNullOrEmpty(SiteUserInfo.ContactInfo.Tel) ? SiteUserInfo.ContactInfo.Mobile : SiteUserInfo.ContactInfo.Tel;

                //供应
                stxtName.Value = SiteUserInfo.ContactInfo.ContactName;
                stxtMQ.Value = SiteUserInfo.ContactInfo.MQ;
                stxtTel.Value = string.IsNullOrEmpty(SiteUserInfo.ContactInfo.Tel) ? SiteUserInfo.ContactInfo.Mobile : SiteUserInfo.ContactInfo.Tel;
            }

            IList<ListItem> tmpList = null;
            
            //初始化类别
            //IList<EnumObj> TypeList = new List<EnumObj>();
            //TypeList = SupplierCom.GetExchangeTypeListByCat(EyouSoft.Model.CommunityStructure.ExchangeCategory.求);
            //StringBuilder sb = new StringBuilder();
            //strSDType = "<option  value=\"-1\">--无--</option>";
            //if (TypeList != null && TypeList.Count > 0)
            //{
            //    for (int i = 0; i < TypeList.Count; i++)
            //    {
            //        sb.Append("<option  value=\"" + TypeList[i].Value + "\">" + TypeList[i].Text + "</option>");
            //    }
            //    strSDType = sb.ToString();
            //}

            ////供应类别
            //TypeList = new List<EnumObj>();
            //TypeList = SupplierCom.GetExchangeTypeListByCat(EyouSoft.Model.CommunityStructure.ExchangeCategory.供);
            //sb = new StringBuilder();
            //if (TypeList != null && TypeList.Count > 0)
            //{
            //    for (int i = 0; i < TypeList.Count; i++)
            //    {
            //        sb.Append("<option  value=\"" + TypeList[i].Value + "\">" + TypeList[i].Text + "</option>");
            //    }
            //    strSDTypeGY = sb.ToString();
            //}
            //TypeList = null;
            //sb = null;

            //初始化标签
            tmpList = new List<ListItem>();
            tmpList = EnumHandle.GetListEnumValue(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag));
            rptExchangeTag.DataSource = tmpList;
            rptExchangeTag.DataBind();

            //供应
            srptExchangeTag.DataSource = tmpList;
            srptExchangeTag.DataBind();


            //初始化发布到的省份
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetEnabledList();
            rptProvince.DataSource = list;
            rptProvince.DataBind();

            //供应
            srptProvince.DataSource = list;
            srptProvince.DataBind();
        }


        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUserInfo()
        {
            //发布联系信息初始化
            if (IsLogin)
            {
                strUserName = SiteUserInfo.UserName;
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (CompanyModel != null)
                {
                    aSupplyInfo1.HRef = Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx";
                    aSupplyInfo2.HRef = Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx";
                    aSupplyInfo3.HRef = Domain.UserBackCenter + "/supplyinformation/hassupplyfavorites.aspx";
                    aEditUser.HRef = GetCompanySetUrl(SiteUserInfo.CompanyID);

                    //供应
                    saSupplyInfo1.HRef = Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx";
                    saSupplyInfo2.HRef = Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx";
                    saSupplyInfo3.HRef = Domain.UserBackCenter + "/supplyinformation/hassupplyfavorites.aspx";
                    saEditUser.HRef = GetCompanySetUrl(SiteUserInfo.CompanyID);

                    EyouSoft.Model.CompanyStructure.CompanyType? cType = Utils.GetCompanyType(SiteUserInfo.CompanyID);
                    if (cType.HasValue)
                    {
                        aShop.HRef = Utils.GetCompanyDomain(SiteUserInfo.CompanyID, cType.Value);

                        //供应
                        saShop.HRef = Utils.GetCompanyDomain(SiteUserInfo.CompanyID, cType.Value);
                    }
                    else
                    {
                        aShop.Visible = false;

                        //供应
                        saShop.Visible = false;
                    }
                }
                CompanyModel = null;
            }
        }

        /// <summary>
        /// 获取标签图片
        /// </summary>
        /// <returns></returns>
        public string GetTagImg(object value,int Type) 
        {
            string strReturn = string.Empty;
            int ExchangeTagValue = EyouSoft.Common.Utils.GetInt(value.ToString(), -1);
            switch (ExchangeTagValue)
            { 
                case 1:
                    if (Type == 1)
                    {
                        strReturn = "<img src=\"" + ImageServerUrl + "/images/new2011/suplly_72.gif\" alt=\"无\" />";
                    }
                    break;
                case 2:
                    strReturn = "<img src=\"" + ImageServerUrl + "/images/new2011/icons_14.gif\" alt=\"品质\" />";
                    break;
                case 3:
                    strReturn = "<img src=\"" + ImageServerUrl + "/images/new2011/suplly_83.gif\" alt=\"特价\" />";
                    break;
                case 4:
                    strReturn = "<img src=\"" + ImageServerUrl + "/images/new2011/suplly_76.gif\" alt=\"急急急\" />";
                    break;
                case 5:
                    strReturn = "<img src=\"" + ImageServerUrl + "/images/new2011/icons_07.gif\" alt=\"最新报价\" />";
                    break;
                case 6:
                    strReturn = "<img src=\"" + ImageServerUrl + "/images/new2011/icons_09.gif\" alt=\"热\" />";
                    break;
                default: break;
            }
            return strReturn;
        }

        /// <summary>
        /// 保存求购
        /// </summary>
        private void btnSave() 
        {
            #region 验证数据
            string ErrStr = string.Empty;
            int Type = Utils.GetInt(Request.Form["chooseType"], 1);     //类型
            string[] Tags = Utils.GetFormValues("radioTag");    //标签
            string Title = Utils.GetFormValue("txtTitle",50);      //标题
            string TxtInfo = Utils.GetFormValue("txtInfo",500);     //内容
            string TxtName = Utils.InputText(txtName.Value,100);    //姓名
            string TxtMQ = Utils.InputText(txtMQ.Value,20);        //MQ
            string TxtTel = Utils.InputText(txtTel.Value,50);      //联系电话
            string AttatchPath = Utils.GetFormValue("ctl00$ctl00$c1$SupplierBody$SingleFileUpload1$hidFileName",250);   //附件地址
            string[] strProvinceIds = Utils.GetFormValues("ckbProvince");               //发布省份
            if (Title.Trim().Length == 0)
            {
                ErrStr += "请输入求购信息标题\\n";
            }
            if (TxtInfo.Trim().Length == 0)
            {
                ErrStr += "请输入求购信息内容\\n";
            }
            if (TxtMQ.Trim().Length == 0)
            {
                ErrStr += "请输入MQ号码";
            }
            if (!string.IsNullOrEmpty(ErrStr))
            {
                MessageBox.Show(this.Page, ErrStr);
                return;
            }
            #endregion
            EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            model.AttatchPath = AttatchPath;
            model.CityId = SiteUserInfo.CityId;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.ContactName = TxtName;
            model.ContactTel = TxtTel;
            model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(Tags[0]);
            model.ExchangeText = TxtInfo;
            model.ExchangeTitle = Title;
            model.ID = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = TxtMQ;
            model.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            model.ProvinceId = SiteUserInfo.ProvinceId;
            model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)(Type);
            model.IsCheck = IsCompanyCheck;//供求审核状态默认等于当前用户所在公司的审核状态
            model.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.求;


            IList<int> ProvinceIds = null;
            if (strProvinceIds != null && strProvinceIds.Length > 0)
            {
                ProvinceIds = new List<int>();
                for (int i = 0; i < strProvinceIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strProvinceIds[i]) && StringValidate.IsInteger(strProvinceIds[i]))
                        ProvinceIds.Add(int.Parse(strProvinceIds[i]));
                }
            }
            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().AddExchangeList(model, ProvinceIds == null ? null : ProvinceIds.ToArray());
            
            if (Result)
            {
                Utils.ShowAndRedirect("求购发布成功！", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(model.ID, CityId));
            }
            else
            {
                Utils.ShowAndRedirect("求购发布失败！", Request.RawUrl);
            }
            model = null;
        }

        /// <summary>
        /// 保存供应
        /// </summary>
        private void sbtnSave()
        {
            #region 验证数据
            string ErrStr = string.Empty;
            int Type = Utils.GetInt(Request.Form["schooseType"], 1);     //类型
            string[] Tags = Utils.GetFormValues("sradioTag");    //标签
            string Title = Utils.GetFormValue("stxtTitle",50);      //标题
            string TxtInfo = Utils.GetFormValue("stxtInfo",500);     //内容
            string TxtName = Utils.InputText(stxtName.Value,100);    //姓名
            string TxtMQ = Utils.InputText(stxtMQ.Value,20);        //MQ
            string TxtTel = Utils.InputText(stxtTel.Value,50);      //联系电话
            string AttatchPath = Utils.GetFormValue("ctl00$ctl00$c1$SupplierBody$SingleFileUpload2$hidFileName", 250);   //附件地址
            string[] strProvinceIds = Utils.GetFormValues("sckbProvince");               //发布省份
            if (Title.Trim().Length == 0)
            {
                ErrStr += "请输入求购信息标题\\n";
            }
            if (TxtInfo.Trim().Length == 0)
            {
                ErrStr += "请输入求购信息内容\\n";
            }
            if (TxtMQ.Trim().Length == 0)
            {
                ErrStr += "请输入MQ号码";
            }
            if (!string.IsNullOrEmpty(ErrStr))
            {
                MessageBox.Show(this.Page, ErrStr);
                return;
            }
            #endregion
            EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            model.AttatchPath = AttatchPath;
            model.CityId = SiteUserInfo.CityId;
            model.CompanyId = SiteUserInfo.CompanyID;
            model.CompanyName = SiteUserInfo.CompanyName;
            model.ContactName = TxtName;
            model.ContactTel = TxtTel;
            model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(Tags[0]);
            model.ExchangeText = TxtInfo;
            model.ExchangeTitle = Title;
            model.ID = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.ID;
            model.OperatorMQ = TxtMQ;
            model.OperatorName = SiteUserInfo.ContactInfo.ContactName;
            model.ProvinceId = SiteUserInfo.ProvinceId;
            model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)(Type);
            model.IsCheck = IsCompanyCheck;//供求审核状态默认等于当前用户所在公司的审核状态
            model.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.供;

            IList<int> ProvinceIds = null;
            if (strProvinceIds != null && strProvinceIds.Length > 0)
            {
                ProvinceIds = new List<int>();
                for (int i = 0; i < strProvinceIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strProvinceIds[i]) && StringValidate.IsInteger(strProvinceIds[i]))
                        ProvinceIds.Add(int.Parse(strProvinceIds[i]));
                }
            }
            bool Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().AddExchangeList(model, ProvinceIds == null ? null : ProvinceIds.ToArray());
            
            if (Result)
            {
                Utils.ShowAndRedirect("供应发布成功！", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(model.ID, CityId));
            }
            else
            {
                Utils.ShowAndRedirect("供应发布失败！", Request.RawUrl);
            }
            model = null;
        }


        /// <summary>
        /// 绑定最新供求和同类其他供求
        /// </summary>
        private void BindNewData()
        {
            #region 最新供求
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> listExchange = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(5);
            if (listExchange != null && listExchange.Count > 0)
            {
                this.crptNewest.DataSource = listExchange;
                this.crptNewest.DataBind();
            }
            else
            {
                this.crptNewest.EmptyText = "<tr><td colspan=\"6\">暂无数据！</td></tr>";
            }
            #endregion
        }

        /// <summary>
        /// 初始化省份
        /// </summary>
        /// <returns></returns>
        public string InitProvince(int ProvinceId) 
        {
            StringBuilder strTmp = new StringBuilder();
            EyouSoft.Model.SystemStructure.SysProvince modelP = new EyouSoft.Model.SystemStructure.SysProvince();
            modelP = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(ProvinceId);
            if (IsLogin)
            {
                strTmp.AppendFormat("<a target='_blank' href='{0}'>", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,ProvinceId,0,CityId));
                strTmp.AppendFormat("{0}</a>", modelP == null ? "" : "【" + modelP.ProvinceName + "】");
            }
            else
            {
                strTmp.AppendFormat("<a target='_blank' href='{0}'>", EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("", 0, 0, 0, ProvinceId, 0, CityId));
                strTmp.AppendFormat("{0}</a>", modelP == null ? "" : "【" + modelP.ProvinceName + "】");
            }
            modelP = null;
            return strTmp.ToString();
        }
    }
}
