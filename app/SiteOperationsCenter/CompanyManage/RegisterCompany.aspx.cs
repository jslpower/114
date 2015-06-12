using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 注册会员审核管理
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class RegisterCompany : EyouSoft.Common.Control.YunYingPage
    {
        protected int intPage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            intPage = Utils.GetInt(Request.QueryString["Page"]);
            if (!Page.IsPostBack)
            {
                bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目);
                if (!isManage
                    || !(
                            CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_专线商审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_组团社审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_地接社审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_景区审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_酒店审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_车队审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_旅游用品店审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_购物店审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_机票供应商审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_其他采购商审核)
                            || CheckMasterGrant(EyouSoft.Common.YuYingPermission.注册审核_随便逛逛)))
                {
                    Utils.ResponseNoPermit();
                    return;
                }

                int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
                if (ProvinceId > 0)
                {
                    this.ProvinceAndCityAndCounty1.SetProvinceId = ProvinceId;
                    int CityId = Utils.GetInt(Request.QueryString["CityId"]);
                    if (CityId > 0)
                    {
                        this.ProvinceAndCityAndCounty1.SetCityId = CityId;
                        int CountyId = Utils.GetInt(Request.QueryString["CountyId"]);
                        if (CountyId > 0)
                        {
                            this.ProvinceAndCityAndCounty1.SetCountyId = CountyId;
                        }
                    }
                }
                int CompanyType = Utils.GetInt(Request.QueryString["CompanyType"]);
                if (CompanyType > 0)
                {
                    this.dropCompanyType.SelectedValue = CompanyType.ToString();
                }

                string CompanyName = Server.UrlDecode(Utils.InputText(Request.QueryString["CompanyName"]));
                if (!string.IsNullOrEmpty(CompanyName))
                {
                    this.txtCompanyName.Value = CompanyName;
                }
                if (ProvinceId > 0 || CompanyType > 0 || !string.IsNullOrEmpty(CompanyName))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "CompanyManage.OnSearch();");
                }

            }
            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                string type = Request.QueryString["Type"].ToString();
                if (type.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Write(this.DeletCompany());
                    Response.End();
                }
                else if (type.Equals("Check", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Write(this.CheckCompany());
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected string DeletCompany()
        {
            string Result = "未选择要删除的公司!";
            string[] strCompanyId = Request.Form.GetValues("ckCompanyId");
            if (strCompanyId != null)
            {
                bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目);
                bool isTure = false;
                //是否拥有删除的权限
                for (int i = 0; i < strCompanyId.Length; i++)
                {
                    EyouSoft.Model.CompanyStructure.CompanyInfo CompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(strCompanyId[i]);
                    if (CompanyInfo != null)
                    {
                        EyouSoft.Model.CompanyStructure.BusinessProperties CompanyType = CompanyInfo.BusinessProperties;
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除旅行社会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.酒店)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.酒店汇总管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除酒店会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.景区)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.景区汇总管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除景区会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.车队)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.车队汇总管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除车队会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.旅游用品店)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅游用品店汇总管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除旅游用品会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.购物店)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.购物店汇总管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除购物点会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.机票供应商)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.机票供应商管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除机票供应商会员！";
                                break;
                            }
                        }
                        if (CompanyType == EyouSoft.Model.CompanyStructure.BusinessProperties.其他采购商)
                        {
                            if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.其他采购商管理_删除))
                            {
                                isTure = true;
                            }
                            else
                            {
                                Result = "您无权删除其他采购商会员！";
                                break;
                            }
                        }
                    }

                }
                if (isTure)
                {
                    if (EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Remove(strCompanyId))
                    {
                        Result = "True";
                    }
                    else
                    {
                        Result = "删除失败!";
                    }
                }
            }
            return Result;
        }

        /// <summary>
        /// 审核
        /// </summary>
        protected bool CheckCompany()
        {
            bool Result = false;
            string CompanyId = Request.QueryString["CompanyId"];
            if (!string.IsNullOrEmpty(CompanyId))
            {
                Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().PassRegister(CompanyId);
            }
            if (Result)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo _detailCompanyInfo =
                    EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);//公司信息
                EyouSoft.Model.CompanyStructure.CompanyUser _companyUser =
                    EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetAdminModel(CompanyId);//公司管理员信息
                if (_detailCompanyInfo != null)
                {
                    //审核通过发送短信
                    bool isSend = Utils.SendSMSForReminderRegPass(_detailCompanyInfo.ContactInfo.Mobile, _detailCompanyInfo.AdminAccount.UserName, _detailCompanyInfo.ContactInfo.MQ, _detailCompanyInfo.CityId);

                    if (isSend)
                    {
                        //发送短信记录
                        EyouSoft.Model.ToolStructure.MsgTipRecord tipModel = new EyouSoft.Model.ToolStructure.MsgTipRecord();
                        tipModel.Email = string.Empty;
                        tipModel.FromMQID = _detailCompanyInfo.ContactInfo.MQ;
                        tipModel.ToMQID = _detailCompanyInfo.ContactInfo.MQ;
                        tipModel.Mobile = _detailCompanyInfo.ContactInfo.Mobile;
                        tipModel.MsgType = EyouSoft.Model.ToolStructure.MsgType.RegPass;
                        tipModel.SendWay = EyouSoft.Model.ToolStructure.MsgSendWay.SMS;
                        EyouSoft.BLL.ToolStructure.MsgTipRecord msgTipBll = new EyouSoft.BLL.ToolStructure.MsgTipRecord();
                        msgTipBll.Add(tipModel);
                    }

                    //审核通过发送邮件
                    EyouSoft.Common.Email.ReminderEmailHelper.SendRegPassEmail(
                        _detailCompanyInfo.AdminAccount.UserName,
                        _detailCompanyInfo.ContactInfo.Email,
                        _companyUser != null ? _companyUser.PassWordInfo.NoEncryptPassword : "");
                    _detailCompanyInfo = null;
                }
            }
            return Result;
        }
    }
}
