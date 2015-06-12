using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EyouSoft.Common.ValidateNumberAndChar;
namespace TourUnion.WEB
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        private string CookieName = "Tongye114ValidateCheck";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ValidateCodeName"]))
            {
                CookieName = Request.QueryString["ValidateCodeName"];
            }
            getNumbers(4);
        }

        //呈现页面后台
        private void getNumbers(int len)
        {
            ValidateNumberAndChar.CurrentLength = len;
            ValidateNumberAndChar.CurrentNumber = ValidateNumberAndChar.CreateValidateNumber(ValidateNumberAndChar.CurrentLength);
            string number = ValidateNumberAndChar.CurrentNumber;
            ValidateNumberAndChar.CreateValidateGraphic(this, number);
            //HttpCookie a = new HttpCookie("Tongye114ValidateCheck", number);
            //Response.Cookies.Add(a);
            Response.Cookies[CookieName].Value = number;
        }


    }
}
