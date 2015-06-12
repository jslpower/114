using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.GeneralShop.GeneralShopControl
{
    /// <summary>
    /// 普通网店二级菜单控件
    /// </summary>
    /// 周文超 2011-11-15
    public partial class SecondMenu : EyouSoft.Common.Control.BaseUserControl
    {
        /// <summary>
        /// 当前选中菜单索引（1：公司介绍；2：公司产品；3：景区；4：酒店；5：同业资讯）
        /// </summary>
        private int _currMenuIndex = 1;

        /// <summary>
        /// 网店所在公司的公司类型（确定网店的链接目录）
        /// </summary>
        private EyouSoft.Model.CompanyStructure.CompanyRole _currCompanyType;
        /// <summary>
        /// 当前选中菜单索引（1：公司介绍；2：公司产品；3：景区；4：酒店；5：同业资讯）
        /// </summary>
        public int CurrMenuIndex
        {
            get { return _currMenuIndex; }
            set { _currMenuIndex = value; }
        }

        /// <summary>
        /// 网店所在公司的公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 当前城市
        /// </summary>
        public int CurrSaleCityId { get; set; }

        /// <summary>
        /// 网店所在公司的公司类型（确定网店的链接目录）
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyRole CurrCompanyType
        {
            get { return _currCompanyType; }
            set { _currCompanyType = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return;

            if (!IsPostBack)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo Companyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
                for (int i = 0; i < CurrCompanyType.RoleItems.Length; i++)
                {

                    switch (CurrCompanyType.RoleItems[i])
                    {
                        case EyouSoft.Model.CompanyStructure.CompanyType.组团:
                            if (Companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接) || Companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                            {
                                hrefproc.HRef = Utils.GenerateShopPageUrl2("/shop/Default", CompanyId);
                                hrefIndustry.HRef = Utils.GenerateShopPageUrl2("/shop/Industry", CompanyId);
                                liSight.Visible = false;
                            }
                            else
                            {
                                liSight.Visible = false;
                                liCompanyProc.Visible = false;
                                liNew.Visible = false;
                            }
                            hrefCompanyInfo.HRef = EyouSoft.Common.URLREWRITE.Tour.TourGeneralShop(CompanyId);
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                            liNew.Visible = false;
                            liCompanyProc.Visible = false;
                            hrefCompanyInfo.HRef = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicGeneralShop(CompanyId);
                            hrefSight.HRef = Utils.GenerateShopPageUrl2("/GeneralShop/SightArea", CompanyId);
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.专线:
                            hrefCompanyInfo.HRef = EyouSoft.Common.URLREWRITE.Tour.TourGeneralShop(CompanyId);
                            hrefproc.HRef = Utils.GenerateShopPageUrl2("/shop/Default", CompanyId);
                            hrefIndustry.HRef = Utils.GenerateShopPageUrl2("/shop/Industry", CompanyId);
                            liSight.Visible = false;
                            break;
                        case EyouSoft.Model.CompanyStructure.CompanyType.地接:
                            hrefCompanyInfo.HRef = EyouSoft.Common.URLREWRITE.Tour.TourGeneralShop(CompanyId);
                            hrefproc.HRef = Utils.GenerateShopPageUrl2("/shop/Default", CompanyId);
                            hrefIndustry.HRef = Utils.GenerateShopPageUrl2("/shop/Industry", CompanyId);
                            //需要开通
                            if (Companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                            {
                                hrefSight.HRef = Utils.GenerateShopPageUrl2("/shop/Scenic", CompanyId);
                            }
                            else
                            {
                                liSight.Visible = false;
                            }
                            break;
                        default:
                            liNew.Visible = false;
                            liCompanyProc.Visible = false;
                            liSight.Visible = false;
                            hrefCompanyInfo.HRef = EyouSoft.Common.URLREWRITE.Tour.TourGeneralShop(CompanyId);
                            break;
                    }
                }

                switch (CurrMenuIndex)
                {
                    case 1:
                        liCompanyInfo.Attributes.Add("class", "x_xuanzhong");
                        break;
                    case 2:
                        liCompanyProc.Attributes.Add("class", "x_xuanzhong");
                        break;
                    case 3:
                        liSight.Attributes.Add("class", "x_xuanzhong");
                        break;
                    case 5:
                        liNew.Attributes.Add("class", "x_xuanzhong");
                        break;
                    default:
                        liCompanyInfo.Attributes.Add("class", "x_xuanzhong");
                        break;
                }
            }
        }
    }
}