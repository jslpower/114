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
using EyouSoft.Common.Function;
using EyouSoft.Security.Membership;

namespace SiteOperationsCenter.AdManagement
{
    /// <summary>
    /// 广告管理：单位广告
    /// 功能：新增/修改
    /// 创建人：袁惠
    /// 创建时间： 2010-7-22  
    /// </summary>
    public partial class AddUnitAd : EyouSoft.Common.Control.YunYingPage
    {
        string str = "<input type=\"checkbox\" cityname=\"{4}\" name=\"{0}\" {3} value=\"{1}\"/>{2}";
        protected string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
        protected string MaxDate = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd");
        protected bool IsDateUpdate = true;
        protected bool IsInsert = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdfLisUrl.Value = Request.UrlReferrer.ToString();
                int advid = Utils.GetInt(Request.QueryString["AdvId"],0);
                int position = Utils.GetInt(Request.QueryString["position"]);//位置
                int displayType=Utils.GetInt(Request.QueryString["dispType"]);  //单位广告类型

                if (advid != 0)
                {
                    if (!this.CheckMasterGrant(new YuYingPermission[] { YuYingPermission.同业114广告_管理该栏目, YuYingPermission.同业114广告_修改 }))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.同业114广告_管理该栏目, true);
                        return;
                    }
                    IsInsert = false;
                    UpdateShowInfo(advid,position);
                }
                else
                {
                    if (!this.CheckMasterGrant(new YuYingPermission[] { YuYingPermission.同业114广告_管理该栏目, YuYingPermission.同业114广告_新增 }))
                    {
                        Utils.ResponseNoPermit(YuYingPermission.同业114广告_管理该栏目, true);
                        return;
                    }
                    SourceBind(position, "");
                    BindRangeType(null, "", false);
                    Panel1.Visible = false;
                } 
                hdfdisplayType.Value = displayType.ToString();
            }
        }

        private void SourceBind(int position, string category)
        {
            this.ddlCategory.Items.Add(new ListItem("-请选择-", ""));
        
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.AdvStructure.AdvCategory));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlCategory.Items.Add(new ListItem(str, ((EyouSoft.Model.AdvStructure.AdvCategory)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvCategory), str)).ToString()));
                }
            }
            if (!string.IsNullOrEmpty(category))
            {
                ddlCategory.SelectedValue = category;
            }
            typeList = null;
            //广告位置
            ltr_advPostion.Text =((EyouSoft.Model.AdvStructure.AdvPosition)position).ToString();          
        }

        #region 绑定投放城市
        private void BindRangeType(IList<int> relation, string ranges, bool isShow)
        {
            string result = "";
            //所有省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            bool ishave = false;
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysProvince item in list)
                {
                    ishave = false;
                    if (relation == null)
                    {
                        result += string.Format(str, "ckbProvince", item.ProvinceId.ToString(), item.ProvinceName, "","");
                    }
                    else
                    {
                        if (ranges == EyouSoft.Model.AdvStructure.AdvRange.全省.ToString())
                        {
                            foreach (int rel in relation)
                            {
                                if (rel == item.ProvinceId)
                                {
                                    ishave = true;
                                    break;
                                }
                            }
                        }
                        if (ishave)
                            result += string.Format(str, "ckbProvince", item.ProvinceId.ToString(), item.ProvinceName, "checked=checked","");
                        else
                            result += string.Format(str, "ckbProvince", item.ProvinceId.ToString(), item.ProvinceName, "","");
                    }
                }
            }

            if (ranges == EyouSoft.Model.AdvStructure.AdvRange.全省.ToString() || ranges == "" || ranges == EyouSoft.Model.AdvStructure.AdvRange.全国.ToString())
            {
                if (isShow)
                {
                    if (ranges == EyouSoft.Model.AdvStructure.AdvRange.全国.ToString())
                    {
                        MessageBox.ResponseScript(this.Page, "$('#rdoCountry').attr('checked',true);$('#defaultProvince').html('" + result + "');$(\"#defaultProvince\").hide();");
                    }
                    else if (ranges == EyouSoft.Model.AdvStructure.AdvRange.全省.ToString())
                    {
                        MessageBox.ResponseScript(this.Page, "$('#rdoProvince').attr('checked',true);$('#defaultProvince').html('" + result + "');$(\"#defaultProvince\").show();");
                    }

                }
                else
                {
                    MessageBox.ResponseScript(this.Page, "$('#defaultProvince').html('" + result + "');$(\"#defaultProvince\").hide();");
                }
            }
            else if (ranges == EyouSoft.Model.AdvStructure.AdvRange.城市.ToString())
            {
                string result1 = "";
                if (relation != null)
                {
                    foreach (int i in relation)
                    {
                        string a = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(i).CityName;
                        result1 += string.Format(str, "ckSellCity", i.ToString(),a, "checked",a);
                    }
                    if (isShow)
                    {
                        MessageBox.ResponseScript(this.Page, "$('#rdoCity').attr('checked',true);$(\"#spanSellCity\").html('" + result1 + "');$(\"#spanSellCity\").show();$(\"#selectCity\").show();$('#defaultProvince').html('" + result + "');$(\"#defaultProvince\").hide();");

                    }
                }
            }
        }
        #endregion
        #region 修改初始化信息
        private void UpdateShowInfo(int advId,int position)
        {
            EyouSoft.Model.AdvStructure.AdvInfo advinfo = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvInfo(advId);
            if (advinfo != null)
            {
                DatePicker2.Value = advinfo.EndDate.ToString("yyyy-MM-dd");
                txtBuyUnit.Value = advinfo.CompanyName;
                txtLink.Value = advinfo.ContactInfo;
                txtOpearDate.Value = advinfo.IssueTime.ToString("yyyy-MM-dd");
                txtOpearId.Value = advinfo.OperatorName;
                hdfUnitId.Value = advinfo.CompanyId;
                DatePicker1.Value = advinfo.StartDate.ToString("yyyy-MM-dd");
                hdfUnitMQ.Value = advinfo.ContactMQ;
                hdfAdRange.Value = advinfo.Range.ToString();
                if (!string.IsNullOrEmpty(advinfo.ImgPath))
                {
                    ltr_ImagePath.Text = string.Format("<a id=\"imgpath\" href=\"{0}\"target='_blank'  title=\"点击查看\">查看原图</a>", Domain.FileSystem + advinfo.ImgPath);
                    hdfoldimgpath.Value = advinfo.ImgPath;
                }
                if (advinfo.EndDate.Date == DateTime.MaxValue.Date)
                {
                    ckbDate.Checked = true;
                    DatePicker1.Value = advinfo.StartDate.ToString("yyyy-MM-dd");
                    pnlforver.Visible = false;
                }
                else
                {
                    DatePicker1.Value = advinfo.StartDate.ToString("yyyy-MM-dd");
                    DatePicker2.Value = advinfo.EndDate.ToString("yyyy-MM-dd");
                }
                #region 判断是否为展示期==true?时间不能修改：可修改
                if (DateTime.Now >= advinfo.StartDate && DateTime.Now <= advinfo.EndDate)
                {
                    IsDateUpdate = false;
                        ckbDate.Enabled = false;
                        ckbDate.Visible = false;
                    if (advinfo.EndDate.Date == DateTime.MaxValue.Date)
                    {
                        ckbDate.Enabled = true;
                        IsDateUpdate = true;
                        ckbDate.Visible = true;
                    }
                }
                #endregion   
                SourceBind((int)advinfo.Position, advinfo.Category.ToString());
                BindRangeType(advinfo.Relation, advinfo.Range.ToString(), true);    
                advinfo = null;
            }
        }

        #endregion

        #region 保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime StartDate =ckbDate.Checked==true?DateTime.Today:Utils.GetDateTime(DatePicker1.Value);
            DateTime EndDate =ckbDate.Checked==true?DateTime.MaxValue: Utils.GetDateTime( DatePicker2.Value);
            string Position = ltr_advPostion.Text;
            string Category = ddlCategory.SelectedValue;
            string Range = hdfAdRange.Value;
            string Relation = StringValidate.IsIntegerArray(hdfResult.Value) == true ? hdfResult.Value : "-1";  
            string link =Utils.InputText(txtLink.Value);
            string unitname=Utils.InputText(txtBuyUnit.Value);            
            int advid = Utils.GetInt(Request.QueryString["AdvId"], -1);
            string imgpath = Utils.GetFormValue("sfuPhotoImg$hidFileName");
            if (string.IsNullOrEmpty(imgpath))
            {
                imgpath = hdfoldimgpath.Value;
            }
            if (Category.Length > 0 && Relation.Length > 0 && unitname.Length >0)
            {
                if (Range.Length <= 0 && Relation.Length <= 0)
                {
                    MessageBox.ResponseScript(this.Page, "alert('请选择投放范围！')");
                    BindRangeType(null, "", false);
                    IsDateUpdate = advid != -1 ? false : true;
                    return;
                }
                IList<int> intrelas = null;
                if (Range != "全国")
                {
                    intrelas = new List<int>();
                    string[] rels = Relation.Split(',');
                    foreach (string item in rels)
                    {
                        intrelas.Add(Convert.ToInt32(item));
                    }
                }
                if (advid != -1)
                {
                    if (StartDate > EndDate )
                    {
                        IsInsert = false;
                        MessageBox.ResponseScript(this.Page, "alert('有效期填写错误！');");
                        BindRangeType(intrelas, Range, true);
                        return;
                    }
                }
                else
                {
                    if (!(StartDate <= EndDate && StartDate >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))))
                    {
                        MessageBox.ResponseScript(this.Page, "alert('有效期填写错误！');");
                        BindRangeType(intrelas, Range, true);
                        return;
                    }                  
                }
                EyouSoft.Model.AdvStructure.AdvInfo info = new EyouSoft.Model.AdvStructure.AdvInfo();   
                info.EndDate = Convert.ToDateTime(EndDate);
                info.StartDate = Convert.ToDateTime(StartDate);
                info.IssueTime = DateTime.Now;
                info.OperatorId = this.MasterUserInfo.ID;    //操作员编号
                info.OperatorName = this.MasterUserInfo.UserName;
                info.ContactInfo = link;
                info.AdvType = EyouSoft.Model.AdvStructure.AdvType.城市;  //广告类型
                info.Category = (EyouSoft.Model.AdvStructure.AdvCategory)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvCategory), Category);  //广告类别
                info.Position = (EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), Position);
                info.Range = (EyouSoft.Model.AdvStructure.AdvRange)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvRange),Range);   //投放类型
                info.Title = Utils.InputText(txtBuyUnit.Value);
                info.Relation = intrelas;
                info.ImgPath = imgpath;// 上传的Logo
                #region 单位图片|图文广告，添加logo,url        
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(hdfUnitId.Value);
                EyouSoft.Model.CompanyStructure.CompanyType? unitType=null;
                if (companyInfo != null)
                {
                    unitType=Utils.GetCompanyType(companyInfo.ID); //获取公司类型                  
                    info.RedirectURL = unitType.HasValue?Utils.GetCompanyDomain(companyInfo.ID, unitType.Value):string.Empty;
                    //info.ImgPath = companyInfo.AttachInfo.CompanyLogo.ImagePath;

                    info.Title = unitname;
                    info.CompanyId = companyInfo.ID;
                    info.CompanyName = unitname;
                    info.ContactMQ = hdfUnitMQ.Value;
                    info.Remark = unitname;
                    companyInfo = null;
                }
               
                #endregion

                int row = 0;
                int intPostion = (int)((EyouSoft.Model.AdvStructure.AdvPosition)Enum.Parse(typeof(EyouSoft.Model.AdvStructure.AdvPosition), Position));
                if (advid != -1)
                {
                    if (info.Category == EyouSoft.Model.AdvStructure.AdvCategory.同业114广告 || EyouSoft.BLL.AdvStructure.Adv.CreateInstance().IsValid(info.Position, info.StartDate, info.EndDate, info.Range, intrelas, advid))
                    {
                        info.AdvId = advid;
                        row = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().UpdateAdv(info);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "该位置的时间范围内已经存在广告，您不能修改！", hdfLisUrl.Value);
                        return;
                    }
                }
                else
                {
                    if (info.Category == EyouSoft.Model.AdvStructure.AdvCategory.同业114广告 || EyouSoft.BLL.AdvStructure.Adv.CreateInstance().IsValid(info.Position, info.StartDate, info.EndDate, info.Range, intrelas))
                    {
                        row = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().InsertAdv(info);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "该位置的时间范围内已经存在广告，您不能再次插入！", "AddUnitAd.aspx?position=" + intPostion);
                        return;
                    }
                }
                info = null;
                pnlforver.Visible = ckbDate.Checked == true ? false : true;
                if (row == 1)
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作成功！", hdfLisUrl.Value);
                }
                else
                {
                    MessageBox.ShowAndRedirect(this.Page, "操作失败！", hdfLisUrl.Value);
                }
            }
            else
            {
                MessageBox.ResponseScript(this.Page, "alert('内容请填写完整！');");
                BindRangeType(null, "", false);
                IsDateUpdate = advid != -1 ? false :true;
            }
        }
            #endregion
    }
}
