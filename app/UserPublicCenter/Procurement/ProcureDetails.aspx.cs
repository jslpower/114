using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.Procurement
{
    /// <summary>
    /// 组团网店信息页面 
    /// 功能：详细信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06
    /// </summary>
    public partial class ProcureDetails : EyouSoft.Common.Control.FrontPage
    {
        //MQ
        public string MQ = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //获取公司ID
            string cId = Utils.GetQueryStringValue("cid");

            if (cId != "")
            {
                DataInit(cId);
                //显示网店简介
                this.GeneralShopControl1.SetAgencyId = cId;
            }
        }


        #region
        /// <summary>
        /// 获得组团网店的实体
        /// </summary>
        public void DataInit(string cId)
        {
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Sdll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo model = Sdll.GetModel(cId);
            if (model != null && !model.State.IsDelete)
            {
                this.lblUserName.Text = model.ContactInfo.ContactName;
                this.lblTelPhone.Text = model.ContactInfo.Tel;
                this.lblFax.Text = model.ContactInfo.Fax;
                this.lblAddress.Text = model.CompanyAddress;

                this.MQ = model.ContactInfo.MQ;

                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    this.CityAndMenu1.HeadMenuIndex = 2;
                    this.Page.Title = model.CompanyName + "_组团";
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商))
                {
                    this.CityAndMenu1.HeadMenuIndex = 2;
                    this.Page.Title = model.CompanyName + "_其他采购商";
                }
                model = null;
                Sdll = null;
            }
            else
            {
                Utils.ShowError("单位信息不存在!", "Shop");
                return;
            }
        }
        #endregion
    }


}
