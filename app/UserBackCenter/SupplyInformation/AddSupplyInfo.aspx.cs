using System;
using System.Collections;
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
using EyouSoft.Security.Membership;
using System.Text.RegularExpressions;

namespace UserBackCenter.SupplyInformation
{
    /// <summary>
    /// 客户后台-供求信息：发布供求信息
    /// luofx   2010-08-3
    /// </summary>
    public partial class AddSupplyInfo : EyouSoft.Common.Control.BackPage
    {
        #region 变量
        //供求信息ID
        protected string ExchangeID = string.Empty;
        //供求信息 标签HTML
        protected string ExchangeTagHtml = string.Empty;
        //供求类别
        protected string ExchangeTypeHtmlg = string.Empty;
        protected string ExchangeTypeHtmlq = string.Empty;
        //供求信息 省份HTML
        protected string ExchangeProvinceHtml = string.Empty;
        //供求图片
        protected string SupplyInfoImgePath1 = string.Empty;
        protected string SupplyInfoImgePath2 = string.Empty;
        protected string SupplyInfoImgePath3 = string.Empty;
        protected string SupplyInfoImgePath4 = string.Empty;

        protected string imgId1 = string.Empty;
        protected string imgId2 = string.Empty;
        protected string imgId3 = string.Empty;
        protected string imgId4 = string.Empty;
        //附件路径
        protected string AttatchPath = string.Empty;

        protected string Tel = string.Empty;
        protected string ConnectName = string.Empty;
        protected string MQ = string.Empty;
        //供求信息内容
        protected string ExchangeText = string.Empty;
        //新增或修改
        protected string ActionType = "add";

        public int publishCount = 0;

        protected EyouSoft.Model.CommunityStructure.ExchangeList model = null;

        /// <summary>
        /// 过滤非tongye114.com的外站链接
        /// </summary>
        private static Regex removeHref = new Regex(@"href=""http://(?!(([^<>]*tongye114.com)))\b.[^""']*""", RegexOptions.IgnoreCase);

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_供求信息))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (Request.QueryString["ExchangeID"] != null)
            {
                ExchangeID = Utils.GetQueryStringValue("ExchangeID");
            }
            if (Request.QueryString["ActionType"] != null)
            {
                ActionType = Request.QueryString["ActionType"] != "" ? "update" : ActionType;
            }

            string type = Utils.GetQueryStringValue("type");
            string titleInfo = Utils.GetQueryStringValue("titleinfo");
            bool sameTitle = false;
            if (type == "sametitle" && titleInfo != "")//判断供求标题是否相同
            {
                sameTitle = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().ExistsSameTitle(SiteUserInfo.ID, titleInfo, ExchangeID);
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
            if (ActionType == "update")
            {
                publishCount = 0;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                ActionType = Request.QueryString["action"];
                if (ActionType == "update")
                {
                    publishCount = 0;
                }
                if (publishCount < 15 && !sameTitle)//每天发布供求信息不能大于15条。标题不能相同
                {
                    Save();
                }
                else
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:false,ErrorMessage:'发布条数已满！'}]");
                    Response.End();
                }
                return;
            }
            if (!IsPostBack)
            {
                SupplyInfoTab1.CityId = this.SiteUserInfo.CityId;
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            model = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetModel(ExchangeID);
            int proCount = 0;
            if (model != null)
            {
                Tel = model.ContactTel;
                ConnectName = model.ContactName;
                MQ = model.OperatorMQ;
                ExchangeText = model.ExchangeText;
                string fileName = "";
                string filePath = "";
                if (model.AttatchPath.Split('|').Length > 1)
                {
                    fileName = model.AttatchPath.Split('|')[0];
                    filePath = model.AttatchPath.Split('|')[1];
                }
                else
                {
                    filePath = model.AttatchPath;
                }
                AttatchPath = filePath;

                AttatchPathFileUpload1.LastDocFile = filePath;
                //if (model.ExchangePhotoList != null && model.ExchangePhotoList.Count > 0)
                //{
                //    for (int j = 0; j < model.ExchangePhotoList.Count; j++)
                //    {
                //        switch (j)
                //        {
                //            case 0:
                //                SupplyInfoImgePath1 = model.ExchangePhotoList[j].ImgPath;
                //                imgId1 = model.ExchangePhotoList[j].ImgId;
                //                break;
                //            case 1:
                //                SupplyInfoImgePath2 = model.ExchangePhotoList[j].ImgPath;
                //                imgId2 = model.ExchangePhotoList[j].ImgId;
                //                break;
                //            case 2:
                //                SupplyInfoImgePath3 = model.ExchangePhotoList[j].ImgPath;
                //                imgId3 = model.ExchangePhotoList[j].ImgId;
                //                break;
                //            case 3:
                //                SupplyInfoImgePath4 = model.ExchangePhotoList[j].ImgPath;
                //                imgId4 = model.ExchangePhotoList[j].ImgId;
                //                break;
                //        }
                //    }
                //}
            }
            else
            {
                ConnectName = this.SiteUserInfo.ContactInfo.ContactName;
                MQ = this.SiteUserInfo.ContactInfo.MQ;
                Tel = this.SiteUserInfo.ContactInfo.Tel;
                if (string.IsNullOrEmpty(Tel))
                {
                    Tel = this.SiteUserInfo.ContactInfo.Mobile;
                }
            }

            #region  绑定标签枚举
            string[] strExchangeTag = Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag));
            if (strExchangeTag != null && strExchangeTag.Length > 0)
            {
                int Tag = 1;
                if (model != null)
                {
                    Tag = (int)model.ExchangeTag;
                }
                if (Tag == 0)
                {
                    Tag = 1;
                }
                for (int i = 1; i <= strExchangeTag.Length; i++)
                {
                    if (Tag == ((int)(EyouSoft.Model.CommunityStructure.ExchangeTag)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), strExchangeTag[i - 1])))
                    {
                        ExchangeTagHtml += string.Format("<div class=\"gqbqx\"><input type=\"radio\"  checked=\"checked\" id=\"gqbiaoqian{0}\" name=\"rbtExchangeTag\" value=\"{0}\" /></div><div class=\"gqbiaoqian{0}\"><label for=\"gqbiaoqian{0}\">{1}</label></div>", i, strExchangeTag[i - 1]);
                    }
                    else
                    {
                        ExchangeTagHtml += string.Format("<div class=\"gqbqx\"><input type=\"radio\" id=\"gqbiaoqian{0}\" name=\"rbtExchangeTag\" value=\"{0}\" /></div><div class=\"gqbiaoqian{0}\"><label for=\"gqbiaoqian{0}\">{1}</label></div>", i, strExchangeTag[i - 1]);
                    }
                }
            }
            #endregion
            #region  绑定类别枚举
            IList<EyouSoft.Common.EnumObj> listg = GetExchangeTypeListByCat(EyouSoft.Model.CommunityStructure.ExchangeCategory.供);
            IList<EyouSoft.Common.EnumObj> listq = GetExchangeTypeListByCat(EyouSoft.Model.CommunityStructure.ExchangeCategory.求);
            EyouSoft.Model.CommunityStructure.ExchangeCategory curr = EyouSoft.Model.CommunityStructure.ExchangeCategory.供;
            if (model != null)
            {
                curr = model.ExchangeCategory;
            }
            if (listg != null && listg.Count > 0)
            {
                ExchangeTypeHtmlg += string.Format("<select name=\"rbtExchangeTypeg\" id=\"ExchangeTypeg\" {0}>", curr == EyouSoft.Model.CommunityStructure.ExchangeCategory.求 ? "style=\"display:none;\"" : "");
                foreach (EyouSoft.Common.EnumObj e in listg)
                {
                    ExchangeTypeHtmlg += string.Format("<option value=\"{0}\" {2}>{1}</option>", e.Value, e.Text, model == null ? "" : (e.Value == model.TopicClassID.ToString() ? "selected='selected'" : ""));
                }
                ExchangeTypeHtmlg += "</select>";
            }
            if (listq != null && listq.Count > 0)
            {
                ExchangeTypeHtmlq += string.Format("<select name=\"rbtExchangeTypeq\" id=\"ExchangeTypeq\" {0}>", curr == EyouSoft.Model.CommunityStructure.ExchangeCategory.供 ? "style=\"display:none;\"" : "");
                foreach (EyouSoft.Common.EnumObj e in listq)
                {
                    ExchangeTypeHtmlq += string.Format("<option value=\"{0}\" {2}>{1}</option>", e.Value, e.Text, model == null ? "" : (e.Value == model.TopicClassID.ToString() ? "selected='selected'" : ""));
                }
                ExchangeTypeHtmlq += "</select>";
            }

            #endregion
            #region 绑定省份
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                if (model != null && model.CityContactList != null) proCount = model.CityContactList.Count;
                int[] ProLists = new int[proCount];
                if (model != null && model.CityContactList != null)
                {
                    for (int k = 0; k < model.CityContactList.Count; k++)
                    {
                        if (!ProLists.Contains(model.CityContactList[k].ProvinceId))
                        {
                            ProLists[k] = model.CityContactList[k].ProvinceId;
                        }
                    }
                }
                else
                {
                    if (model != null) ProLists[0] = model.ProvinceId;
                }
                ((List<EyouSoft.Model.SystemStructure.SysProvince>)ProvinceList).ForEach(item =>
                {
                    if (model != null && model.CityContactList != null)
                    {
                        if (ProLists.Contains(item.ProvinceId))
                        {
                            ExchangeProvinceHtml += string.Format("<input id=\"ProvinceList_{0}\" type=\"checkbox\" name=\"ProvinceList_forSupply\" value=\"{0}\" {2} /><label for=\"ProvinceList_{0}\">{1}</label>", item.ProvinceId, item.ProvinceName, "checked=\"checked\"");
                        }
                        else
                        {
                            ExchangeProvinceHtml += string.Format("<input id=\"ProvinceList_{0}\" type=\"checkbox\" name=\"ProvinceList_forSupply\" value=\"{0}\" {2} /><label for=\"ProvinceList_{0}\">{1}</label>", item.ProvinceId, item.ProvinceName, "");
                        }
                    }
                    else
                    {
                        ExchangeProvinceHtml += string.Format("<input id=\"ProvinceList_{0}\" type=\"checkbox\" name=\"ProvinceList_forSupply\" value=\"{0}\" {2} /><label for=\"ProvinceList_{0}\">{1}</label>", item.ProvinceId, item.ProvinceName, "");
                    }
                });
            }
            ProvinceList = null;
            #endregion


        }
        /// <summary>
        /// 保存供求信息
        /// </summary>
        private void Save()
        {
            model = new EyouSoft.Model.CommunityStructure.ExchangeList();
            //供求信息标签
            if (!string.IsNullOrEmpty(Request.Form["rbtExchangeTag"]))
            {
                model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), Request.Form["rbtExchangeTag"].ToString(), true);
            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'标签必须选择一个，请选择！'}]");
                Response.End();
                return;
            }
            model.ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Utils.GetInt(Request.Form["applycat"], 1);
            //供求信息类别
            if (model.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.求)
            {
                model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), Request.Form["rbtExchangeTypeq"], true);
            }
            if (model.ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
            {
                model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), Request.Form["rbtExchangeTypeg"], true);
            }

            string exchangeText = Utils.EditInputText(Request.Form["AddSupplyInfo_ExchangeText"]);//供求信息
            if (exchangeText == "")
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'供求信息不能为空！'}]");
                Response.End();
                return;
            }

            if (Utils.GetFormValue("applytitle") == "" || Utils.GetFormValue("applytitle") == "请填写供应信息标题" || Utils.GetFormValue("applytitle") == "请填写求购信息标题")
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'标题不能为空！'}]");
                Response.End();
                return;
            }

            //对供求信息的内容进行过滤，只允许添加tongye114.com的站内链接
            exchangeText = removeHref.Replace(exchangeText, "");

            model.ExchangeTitle = Utils.GetFormValue("applytitle");
            model.ProvinceId = this.SiteUserInfo.ProvinceId;
            model.CityId = this.SiteUserInfo.CityId;
            model.OperatorName = this.SiteUserInfo.UserName;
            model.OperatorId = this.SiteUserInfo.ID;
            model.CompanyName = this.SiteUserInfo.CompanyName;
            model.OperatorMQ = this.SiteUserInfo.ContactInfo.MQ;
            model.ID = Guid.NewGuid().ToString();
            model.CompanyId = this.SiteUserInfo.CompanyID;
            ExchangeID = Utils.GetFormValue("ExchangeID");
            model.ExchangeText = exchangeText;
            model.IsCheck = this.IsCompanyCheck;
            if (model.ExchangeTitle.Length > 26)
            {
                model.ExchangeTitle = model.ExchangeTitle.Substring(0, 26);
            }
            else
            {
                model.ExchangeTitle = model.ExchangeTitle;
            }
            model.ContactName = Utils.GetFormValue("AddSupplyInfo_UserName");
            model.ContactTel = Utils.GetFormValue("AddSupplyInfo_Tel");
            //附件地址
            string path = Utils.GetFormValue("ctl00$ContentPlaceHolder1$AttatchPathFileUpload1$hidFileName");
            if (path.Length > 1)
            {
                model.AttatchPath = path;
            }
            if (string.IsNullOrEmpty(model.AttatchPath))
            {
                model.AttatchPath = Utils.GetFormValue("AttatchPath");
            }
            #region 城市，供求管理信息集合
            string[] ProvinceList_forSupply = Utils.GetFormValues("ProvinceList_forSupply");
            int[] provinceIds = new int[ProvinceList_forSupply.Length];
            for (int m = 0; m < ProvinceList_forSupply.Length; m++)
            {
                if (!string.IsNullOrEmpty(ProvinceList_forSupply[m]))
                {
                    provinceIds[m] = int.Parse(ProvinceList_forSupply[m]);
                }
            }
            #endregion
            #region 供求图片集合
            //IList<EyouSoft.Model.CommunityStructure.ExchangePhoto> PhotoLists = new List<EyouSoft.Model.CommunityStructure.ExchangePhoto>();
            //EyouSoft.Model.CommunityStructure.ExchangePhoto photoModel = null;
            //for (int i = 1; i <= 4; i++)
            //{
            //    photoModel = new EyouSoft.Model.CommunityStructure.ExchangePhoto();
            //    photoModel.ImgPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$Upload_SupplyInfoImge" + i.ToString() + "$hidFileName");
            //    if (string.IsNullOrEmpty(photoModel.ImgPath))
            //    {
            //        photoModel.ImgPath = Utils.GetFormValue("hid_MyOwenerShop_Upload_ProductInfo" + i.ToString());
            //    }
            //    photoModel.ImgId = Utils.GetFormValue("hidImageId" + i.ToString());
            //    photoModel.ExchangeId = ExchangeID;
            //    PhotoLists.Add(photoModel);
            //    photoModel = null;
            //}
            //model.ExchangePhotoList = PhotoLists;
            //PhotoLists = null;
            #endregion
            bool isTrue = false;
            if (ActionType == "add")
            {
                isTrue = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().AddExchangeList(model, provinceIds);
            }
            else
            {
                model.ID = ExchangeID;

                isTrue = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().UpdateExchangeList(model, provinceIds);
            }
            model = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:'保存成功！'}]");
                Response.End();
                return;
            }
            else
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,ErrorMessage:'保存失败！'}]");
                Response.End();
                return;
            }
        }



        /// <summary>
        /// 供求类别健值对列表
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static IList<EyouSoft.Common.EnumObj> GetExchangeTypeListByCat(EyouSoft.Model.CommunityStructure.ExchangeCategory cat)
        {
            List<EyouSoft.Common.EnumObj> exchangeTypelist = new List<EnumObj>();
            //exchangeTypelist = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.CommunityStructure.ExchangeType));
            if (cat == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
            {
                exchangeTypelist = new List<EyouSoft.Common.EnumObj>()
                {
                    new EyouSoft.Common.EnumObj("地接报价","2"),
                    new EyouSoft.Common.EnumObj("直通车","3"),
                    new EyouSoft.Common.EnumObj("车辆","4"),
                    new EyouSoft.Common.EnumObj("机票","10"),
                    new EyouSoft.Common.EnumObj("酒店","5"),
                    new EyouSoft.Common.EnumObj("票务","7"),
                    new EyouSoft.Common.EnumObj("签证","11"),
                    new EyouSoft.Common.EnumObj("招聘","6"),
                    new EyouSoft.Common.EnumObj("其他","8")
                };
            }
            else
            {
                exchangeTypelist = new List<EyouSoft.Common.EnumObj>()
                {
                    new EyouSoft.Common.EnumObj("团队询价","1"),
                    new EyouSoft.Common.EnumObj("找地接","9"),
                    new EyouSoft.Common.EnumObj("车辆","4"),
                    new EyouSoft.Common.EnumObj("机票","10"),
                    new EyouSoft.Common.EnumObj("酒店","5"),
                    new EyouSoft.Common.EnumObj("票务","7"),
                    new EyouSoft.Common.EnumObj("签证","11"),
                    new EyouSoft.Common.EnumObj("招聘","6"),
                    new EyouSoft.Common.EnumObj("其他","8")
                };
            }
            return exchangeTypelist;
        }
    }
}
